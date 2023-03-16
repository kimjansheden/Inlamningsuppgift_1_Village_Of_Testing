using Microsoft.JSInterop;
using static GameLib.Strings.Message;

namespace GameLib;
public class Village
{
    private int _daysGone = 0;
    private int _food = 10; //Village starts with 10 food.
    private int _foodPerDay = 0;
    private int _foodPerDayBonus = 0;
    private int _metal = 0;
    private int _metalPerDay = 0;
    private int _metalPerDayBonus = 0;
    private int _wood = 0;
    private int _woodPerDay = 0;
    private int _woodPerDayBonus = 0;
    private List<Building> _buildings = new List<Building>();
    private List<Building> _projects = new List<Building>();
    private List<Worker> _workers = new List<Worker>();
    private bool _gameOver = false;
    private bool _gameWon;
    private int _maxWorkers = 0;
    private int _farmers = 0;
    private int _lumberjacks = 0;
    private int _quarryworkers = 0;
    private int _builders = 0;
    private int _farms = 0;
    private int _woodmills = 0;
    private int _quarries = 0;
    private int _houses = 0;
    private bool _starvation = false;
    private List<DayEventArgs> _dayEventsList = new List<DayEventArgs>();
    private List<Worker> _starvingWorkers = new List<Worker>();

    private readonly DatabaseConnection _databaseConnection;
    private IJSRuntime _jsRuntime;

    private readonly IUI _ui;

    private readonly Strings _strings;
    private string _newLine;

    public bool GameOver => _gameOver;
    public bool GameWon => _gameWon;
    public bool Starvation => _starvation;

    public event EventHandler VillageUpdated;
    private event EventHandler DayEvents;
    public event EventHandler EndGame;

    private RandomGenerator _random = new RandomGenerator();

    public Village(IUI ui)
    {
        // Initialize the UI and the Strings
        _ui = ui;
        _strings = new Strings(this, _ui);

        // A new village starts with three houses.
        for (int i = 0; i < 3; i++)
        {
            BuildingCompleted((new Building(Building.Type.House, this)));
            
            // The 3 houses come for free, so we must give the wood back that they cost, otherwise the amount of wood will be negative.
            AddWood(5);
        }
        
        // Subscribe to the day's events.
        DayEvents += OnDayEvent;
        
        // Initialize the DatabaseConnection.
        _databaseConnection = new DatabaseConnection();
        
        // Choose newlines based on UI.
        if (ui is ConsoleUI)
        {
            _newLine = Environment.NewLine;
        }
        else if (ui is WebUI)
        {
            _newLine = "<br>";
        }
    }

    public Village(IUI ui, IJSRuntime jsRuntime) : this(ui)
    {
        _jsRuntime = jsRuntime;
    }

    // Constructor to make testing easier.
    public Village(IUI ui, int startFood = 10, int startWood = 0, int startMetal = 0, int startMaxWorkers = 0, int startBuilders = 0, DatabaseConnection databaseConnection = null, RandomGenerator random = default, IJSRuntime jsRuntime = null) : this(ui)
    {
        _food = startFood;
        _wood = startWood;
        _metal = startMetal;
        if (startMaxWorkers != 0) _maxWorkers = startMaxWorkers;
        for (int i = 0; i < startBuilders; i++)
        {
            AddWorker("Test", Worker.Type.Builder, () => Build());
        }

        if (databaseConnection != null) _databaseConnection = databaseConnection;
        else _databaseConnection = new DatabaseConnection();
        _random = random;
        _jsRuntime = jsRuntime;
    }
    
    public void Day()
    {
        // A day goes by.
        _daysGone += 1;
        VillageUpdated?.Invoke(this, EventArgs.Empty);
        
        // Clears the old events.
        _dayEventsList.Clear();
        
        for (var i = 0; i < _workers.Count; i++)
        {
            var worker = _workers[i];
            // Make the workers do the day's work. Only if they're not hungry, though.
            if (!worker.Hungry && worker.Alive)
            {
                worker.DoWork();
            }
        }
        // Make the workers eat the amount each day.
        FeedWorkers(1);
        
        // Update the current workers.
        _farmers = _workers.Count(w => w.Job == Worker.Type.Farmer);
        _lumberjacks = _workers.Count(w => w.Job == Worker.Type.Lumberjack);
        _quarryworkers = _workers.Count(w => w.Job == Worker.Type.QuarryWorker);
        _builders = _workers.Count(w => w.Job == Worker.Type.Builder);
        
        // Get the current resource income.
        _foodPerDay = (_farmers * 5) + (_foodPerDayBonus * _farmers);
        _woodPerDay = _lumberjacks + (_woodPerDayBonus * _lumberjacks);
        _metalPerDay = _quarryworkers + (_metalPerDayBonus * _quarryworkers);
    }

    public void BuildingCompleted(Building project)
    {
        _projects.Remove(project);
        _buildings.Add(project);
        
        // The building's special action is invoked.
        project.BuildAction.Invoke(this);

        // An event is fired.
        DayEvents?.Invoke(this, new DayEventArgs($"The construction of a {project.BuildingType} was completed.{_newLine}"));
    }

    public void AddHouse()
    {
        // For every house that exists in the village, allow 2 workers to live in the village.
        _maxWorkers += 2;
        _houses += 1;
    }

    public void AddWoodmill()
    {
        if (_lumberjacks > 0) _woodPerDay += 2 * _lumberjacks;
        else _woodPerDay += 2;
        _woodPerDayBonus += 2;
        _woodmills += 1;
    }
    public void AddQuarry()
    {
        if (_quarryworkers > 0) _metalPerDay += 2 * _quarryworkers;
        else _metalPerDay += 2;
        _metalPerDayBonus += 2;
        _quarries += 1;
    }
    public void AddFarm()
    {
        if (_farmers > 0) _foodPerDay += 10 * _farmers;
        else _foodPerDay += 10;
        _foodPerDayBonus += 10;
        _farms += 1;
    }
    public void AddCastle()
    {
        _ui.WriteLine(_strings.Messages[GameIsWon]);
        _gameWon = true;
        SaveProgress();

        if (_ui is WebUI && _jsRuntime is not null)
        {
            _jsRuntime.InvokeVoidAsync("playAudio", "/Audio/wololo.mp3");
            EndGame?.Invoke(this, EventArgs.Empty);
        }
    }
    public void AddFarmer()
    {
        if (_farms > 0) _foodPerDay += 5 + _foodPerDayBonus;
        else _foodPerDay += 5;
        _farmers += 1;
    }
    public void AddLumberjack()
    {
        if (_woodmills > 0) _woodPerDay += 1 + _woodPerDayBonus;
        else _woodPerDay += 1;
        _lumberjacks += 1;
    }
    public void AddQuarryWorker()
    {
        if (_quarries > 0) _metalPerDay += 1 + _metalPerDayBonus;
        else _metalPerDay += 1;
        _quarryworkers += 1;
    }
    public void AddBuilder()
    {
        _builders += 1;
    }
    public void AddFood()
    {
        _food += 5 + _foodPerDayBonus;
    }
    public void AddFood(int amount)
    {
        _food += amount;
    }
    public void AddMetal()
    {
        _metal += 1 + _metalPerDayBonus;
    }
    public void AddMetal(int amount)
    {
        _metal += amount;
    }
    public void RemoveMetal(int amount)
    {
        _metal -= amount;
    }

    public void Quit()
    {
        EndGame?.Invoke(this, EventArgs.Empty);
    }
    public bool AddProject(Building.Type type)
    {
        // Here we need to check the costs of the building type against what resources we have in the village, before we create a building object.
        if (_wood < Building.GetCosts(type).costWood || _metal < Building.GetCosts(type).costMetal)
        {
            _ui.WriteLine(_strings.Messages[BuildNotEnoughResources]);
            return false;
        }
        _projects.Add(new Building(type, this));
        return true;
    }
    public void AddWood()
    {
        _wood += 1 + _woodPerDayBonus;
    }
    public void AddWood(int amount)
    {
        _wood += amount;
    }
    public void RemoveWood(int amount)
    {
        _wood -= amount;
    }

    private void RemoveWorker(Worker worker)
    {
        var job = worker.Job;
        switch (job)
        {
            case Worker.Type.Builder:
                _builders -= 1;
                break;
            case Worker.Type.Farmer:
                if (_farms > 0) _foodPerDay -= 5 + _foodPerDayBonus;
                else _foodPerDay -= 5;
                _farmers -= 1;
                break;
            case Worker.Type.Lumberjack:
                if (_woodmills > 0) _woodPerDay -= 1 + _woodPerDayBonus;
                else _woodPerDay -= 1;
                _lumberjacks -= 1;
                break;
            case Worker.Type.QuarryWorker:
                if (_quarries > 0) _metalPerDay -= 1 + _metalPerDayBonus;
                else _metalPerDay -= 1;
                _quarryworkers -= 1;
                break;
        }
        _workers.Remove(worker);
        VillageUpdated?.Invoke(this, EventArgs.Empty);
    }
    public bool AddWorker(string name, Worker.Type job, Worker.WorkDelegate workDelegate)
    {
        if (!HelperValidWorkerName(name)) return false;
        
        // If the number of workers in the village is equal to the maximum workers allowed, then the max is reached and no new workers can live in the village until more houses are built.
        if (HelperMaxWorkersReached()) return false;

        return HelperCreateNewWorker(name, job, workDelegate);
    }
    
    public bool AddRandomWorker(string name)
    {
        if (!HelperValidWorkerName(name)) return false;
        if (HelperMaxWorkersReached()) return false;

        Worker.Type? job = null;
        Worker.WorkDelegate workDelegate = null;
        
        var randomNumber = _random.Next(0, Enum.GetValues(typeof(Worker.Type)).Length);

        switch (randomNumber)
        {
            case 1:
                job = Worker.Type.QuarryWorker;
                workDelegate = AddMetal;
                break;
            case 2:
                job = Worker.Type.Builder;
                workDelegate = Build;
                break;
            case 3:
                job = Worker.Type.Lumberjack;
                workDelegate = AddWood;
                break;
            case 4:
                job = Worker.Type.Farmer;
                workDelegate = AddFood;
                break;
        }

        return workDelegate != null && job != null && HelperCreateNewWorker(name, job.Value, workDelegate);
    }
    public void Build()
    {
        if (_projects.Count == 0)
        {
            return;
        }
        
        // Work on the project at the top of the list.
        var project = _projects[0];
        project.WorkOn();

        if (project.IsComplete) BuildingCompleted(project);
    }

    public void Banish(Worker worker)
    {
        if (worker.Hungry)
        {
            _ui.WriteLine(_strings.Messages[WorkerCantBanishStarving]);
            return;
        }
        RemoveWorker(worker);
        _ui.WriteLine(worker.Name + _strings.Messages[WorkerHasBeenBanished]);
    }
    private void BuryDead(Worker worker)
    {
        RemoveWorker(worker);
        
        // If all workers of the village are dead and buried, the game is over.
        if (_workers.Count != 0) return;
        _gameOver = true;
        _ui.WriteLine(_strings.Messages[GameIsOver]);
    }

    public void FeedWorkers(int amount)
    {
        for (var i = 0; i < _workers.Count; i++)
        {
            var worker = _workers[i];
            // If the village has got any food left, the worker can be fed. Only if they're alive, though.
            if (_food > 0 && worker.Alive)
            {
                // The amount of food each worker consumes is subtracted from the village's resources.
                _food -= amount;

                // The worker is not hungry after having been fed.
                worker.Eat();
                
                // If the worker has been starving, remove from the list of starving workers.
                if (!_starvingWorkers.Contains(worker)) continue;
                _starvingWorkers.Remove(worker);

                // If this was the only worker left starving, there is no longer any starvation in the village.
                if (_starvingWorkers.Count != 0) continue;
                _starvation = false;
                DayEvents?.Invoke(this, new DayEventArgs("There is no longer any starvation in the village."));
            }
            else
            {
                // The village has no food left, thus the worker gets hungry.
                worker.Starve();
                
                // If the worker has not been starving before, add to the list of starving workers.
                if (!_starvingWorkers.Contains(worker))
                {
                    _starvingWorkers.Add(worker);    
                }
                
                // If this is the first worker to be starving, change the starvation status of the village to true and fire an event.
                if (_starvingWorkers.Count == 1)
                {
                    _starvation = true;
                    DayEvents?.Invoke(this, new DayEventArgs("YOUR VILLAGE IS STARVING! You need to either banish a worker from your village, add a farmer or build more farms."));   
                }

                if (worker.DaysHungry == 40)
                {
                    worker.Die();
                    BuryDead(worker);
                    DayEvents?.Invoke(this, new DayEventArgs($"{worker.Name} has starved to death and is now buried in the ground."));
                    
                    // When a worker is removed from the list,
                    // the size of the list decreases by 1.
                    // Thus the counter also needs to be decremented
                    // by 1. Otherwise the loop will skip the next
                    // worker in the list.
                    i--;
                }
            }
        }
    }
    public int GetDaysGone()
    {
        return _daysGone;
    }
    public int GetFoodPerDay()
    {
        return _foodPerDay;
    }
    public int GetFoodPerDayBonus()
    {
        return _foodPerDayBonus;
    }
    public int GetMetal()
    {
        return _metal;
    }
    public int GetMetalPerDay()
    {
        return _metalPerDay;
    }
    public List<Building> GetProjects()
    {
        return _projects;
    }
    public int GetWood()
    {
        return _wood;
    }
    public int GetWoodPerDay()
    {
        return _woodPerDay;
    }
    public List<Worker> GetWorkers()
    {
        return _workers;
    }
    public int GetMaxWorkers()
    {
        return _maxWorkers;
    }

    public int GetFood()
    {
        return _food;
    }
    public int GetFarmers()
    {
        return _farmers;
    }
    public List<Building> GetBuildings()
    {
        return _buildings;
    }
    private void OnDayEvent(object? sender, EventArgs e)
    {
        // Every time a new event has happened, add it to today's list of events.
        if (e is DayEventArgs dayEventArgs)
        {
            _dayEventsList.Add(dayEventArgs);
        }
    }

    public string GetEvents()
    {
        var statsString = "";
        if (_dayEventsList.Count == 0) return statsString;
        foreach (var dayEvent in _dayEventsList)
        {
            statsString += dayEvent + "\n";
        }

        return statsString;
    }
    public string GetStats()
    {
        // This string concatenation by the "+" operator is allocating a lot of memory. I've read that using a StringBuilder object could be a way to reduce memory allocation but I didn't have time to try it out. Something to think about if I revisit this code some day or for the future.
        
        var statsString = "";

        statsString += $"Day: {_daysGone + 1}.{_newLine}{_newLine}" +
                       $"Food: {_food}.{_newLine}" +
                       $"Metal: {_metal}.{_newLine}" +
                       $"Wood: {_wood}.{_newLine}" +
                       $"{_newLine}" +
                       $"Food Per Day: {_foodPerDay}.{_newLine}" +
                       $"Metal Per Day: {_metalPerDay}.{_newLine}" +
                       $"Wood Per Day: {_woodPerDay}.{_newLine}" +
                       $"{_newLine}" +
                       $"~*~*~WORKERS~*~*~{_newLine}" +
                       $"{_newLine}" +
                       $"Total Workers: {_workers.Count}/{_maxWorkers}.{_newLine}" +
                       $"Farmers: {_farmers}.{_newLine}" +
                       $"Quarry Workers: {_quarryworkers}.{_newLine}" +
                       $"Lumberjacks: {_lumberjacks}.{_newLine}" +
                       $"Builders: {_builders}.{_newLine}" +
                       $"{_newLine}";

        statsString += $"~*~*~PROJECTS~*~*~{_newLine}" +
                       $"{_newLine}" +
                       $"Total Projects: {_projects.Count}{_newLine}" +
                       $"Houses: {_projects.Count(p => p.BuildingType == Building.Type.House)}{_newLine}";
        
        var counter = 0;
        foreach (var project in _projects)
        {
            if (project.BuildingType == Building.Type.House)
            {
                statsString += $"    House {counter += 1}: {project.DaysSpent}/{project.DaysToComplete} days worked on.{_newLine}";
            }
        }

        statsString += 
                        $"Farms: {_projects.Count(p => p.BuildingType == Building.Type.Farm)}{_newLine}";
        
        counter = 0;
        foreach (var project in _projects)
        {
            if (project.BuildingType == Building.Type.Farm)
            {
                statsString += $"    Farm {counter += 1}: {project.DaysSpent}/{project.DaysToComplete} days worked on.{_newLine}";
            }
        }

        statsString +=
            $"Woodmills: {_projects.Count(p => p.BuildingType == Building.Type.Woodmill)}{_newLine}";
        
        counter = 0;
        foreach (var project in _projects)
        {
            if (project.BuildingType == Building.Type.Woodmill)
            {
                statsString += $"    Woodmill {counter += 1}: {project.DaysSpent}/{project.DaysToComplete} days worked on.{_newLine}";
            }
        }

        statsString +=
            $"Quarries: {_projects.Count(p => p.BuildingType == Building.Type.Quarry)}{_newLine}";
        
        counter = 0;
        foreach (var project in _projects)
        {
            if (project.BuildingType == Building.Type.Quarry)
            {
                statsString += $"    Quarry {counter += 1}: {project.DaysSpent}/{project.DaysToComplete} days worked on.{_newLine}";
            }
        }

        statsString +=
            $"Castle: {_projects.Count(p => p.BuildingType == Building.Type.Castle)}{_newLine}";
        
        counter = 0;
        foreach (var project in _projects)
        {
            if (project.BuildingType == Building.Type.Castle)
            {
                statsString += $"    Castle: {project.DaysSpent}/{project.DaysToComplete} days worked on.{_newLine}";
            }
        }
        
        statsString +=
                       $"{_newLine}" +
                       $"~*~*~BUILDINGS~*~*~{_newLine}" +
                       $"{_newLine}" +
                       $"Total Buildings: {_buildings.Count}{_newLine}" +
                       $"Houses: {_buildings.Count(b => b.BuildingType == Building.Type.House)}{_newLine}" +
                       $"Farms: {_buildings.Count(b => b.BuildingType == Building.Type.Farm)}{_newLine}" +
                       $"Woodmills: {_buildings.Count(b => b.BuildingType == Building.Type.Woodmill)}{_newLine}" +
                       $"Quarries: {_buildings.Count(b => b.BuildingType == Building.Type.Quarry)}{_newLine}" +
                       $"Castle: {_buildings.Count(b => b.BuildingType == Building.Type.Castle)}{_newLine}{_newLine}";
        return statsString;
        }

    public void SaveProgress()
    {
        _databaseConnection.Save();
    }

    public void LoadProgress()
    {
        // Load the variables from the database into the DatabaseConnection object.
        _databaseConnection.Load();
        
        // Recreate the saved state.
        _daysGone = _databaseConnection.GetDaysGone();
        _food = _databaseConnection.GetFood();
        _foodPerDay = _databaseConnection.GetFoodPerDay();
        _foodPerDayBonus = _databaseConnection.GetFoodPerDayBonus();
        _metal = _databaseConnection.GetMetal();
        _metalPerDay = _databaseConnection.GetMetalPerDay();
        _metalPerDayBonus = _databaseConnection.GetMetalPerDayBonus();
        _wood = _databaseConnection.GetWood();
        _woodPerDay = _databaseConnection.GetWoodPerDay();
        _woodPerDayBonus = _databaseConnection.GetWoodPerDayBonus();
        _buildings = _databaseConnection.GetBuildings();
        _projects = _databaseConnection.GetProjects();
        _workers = _databaseConnection.GetWorkers();
        _gameOver = _databaseConnection.GetGameOver();
        _gameWon = _databaseConnection.GetGameWon();
        _maxWorkers = _databaseConnection.GetMaxWorkers();
        _farmers = _databaseConnection.GetFarmers();
        _lumberjacks = _databaseConnection.GetLumberjacks();
        _quarryworkers = _databaseConnection.GetQuarryWorkers();
        _builders = _databaseConnection.GetBuilders();
        _farms = _databaseConnection.GetFarms();
        _woodmills = _databaseConnection.GetWoodmills();
        _quarries = _databaseConnection.GetQuarries();
        _houses = _databaseConnection.GetHouses();
        _starvation = _databaseConnection.GetStarvation();
        _dayEventsList = _databaseConnection.GetDayEventsList();
        _starvingWorkers = _databaseConnection.GetStarvingWorkers();
    }

    public int GetMetalPerDayBonus()
    {
        return _metalPerDayBonus;
    }

    public int GetWoodPerDayBonus()
    {
        return _woodPerDayBonus;
    }

    public int GetLumberjacks()
    {
        return _lumberjacks;
    }

    public int GetQuarryWorkers()
    {
        return _quarryworkers;
    }

    public int GetBuilders()
    {
        return _builders;
    }

    public int GetFarms()
    {
        return _farms;
    }

    public int GetWoodmills()
    {
        return _woodmills;
    }

    public int GetQuarries()
    {
        return _quarries;
    }

    public int GetHouses()
    {
        return _houses;
    }

    public IEnumerable<DayEventArgs> GetDayEventsList()
    {
        return _dayEventsList;
    }

    public IEnumerable<Worker> GetStarvingWorkers()
    {
        return _starvingWorkers;
    }
    
    // Helper methods
    private bool HelperCreateNewWorker(string name, Worker.Type job, Worker.WorkDelegate workDelegate)
    {
        Worker worker = new Worker(name, job, workDelegate);
        _workers.Add(worker);
        worker.JobAction.Invoke(this);
        VillageUpdated?.Invoke(this, EventArgs.Empty);
        return true;
    }

    private bool HelperMaxWorkersReached()
    {
        if (_workers.Count == _maxWorkers)
        {
            _ui.WriteLine(_strings.Messages[WorkerAddNoRoom]);
            return true;
        }
        return false;
    }

    private bool HelperValidWorkerName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            _ui.WriteLine(_strings.Messages[WorkerAddNoName]);
            return false;
        }
        return true;
    }
}