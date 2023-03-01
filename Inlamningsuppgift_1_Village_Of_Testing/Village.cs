using static Inlamningsuppgift_1_Village_Of_Testing.Strings.Message;

namespace Inlamningsuppgift_1_Village_Of_Testing;
public class Village
{
    private List<Building> _buildings = new List<Building>();
    private int _daysGone = 0;
    private int _food = 10; //Village starts with 10 food.
    private int _foodPerDay = 0;
    private int _metal = 0;
    private int _metalPerDay = 0;
    private List<Building> _projects = new List<Building>();
    private int _wood = 0;
    private int _woodPerDay = 0;
    private List<Worker> _workers = new List<Worker>();
    private bool _gameOver = false;
    private bool _gameWon;
    private int _maxWorkers = 0;

    private readonly IUI _ui;

    private readonly Strings _strings;

    public bool GameOver => _gameOver;
    public bool GameWon => _gameWon;

    public int FoodPerDay => _foodPerDay;
    
    public event EventHandler DaysGoneUpdated;

    public Village(IUI ui)
    {
        // Initialize the UI and the Strings
        _ui = ui;
        _strings = new Strings(this);

        // A new village starts with three houses.
        for (int i = 0; i < 3; i++)
        {
            BuildingCompleted((new Building(Building.Type.House, this)));
            
            // The 3 houses come for free, so we must give the wood back that they cost.
            AddWood(5);
        }
    }
    
    public void Day()
    {
        // A day goes by.
        _daysGone += 1;
        DaysGoneUpdated?.Invoke(this, EventArgs.Empty);
        
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
    }

    private void BuildingCompleted(Building project)
    {
        _projects.Remove(project);
        _buildings.Add(project);
        
        // The building's special action is invoked.
        project.BuildAction.Invoke(this);
    }

    public void AddHouse()
    {
        // For every house that exists in the village, allow 2 workers to live in the village.
        _maxWorkers += 2;
    }

    public void AddWoodmill()
    {
        _woodPerDay += 2;
    }
    public void AddQuarry()
    {
        _metalPerDay += 2;
    }
    public void AddFarm()
    {
        _foodPerDay += 10;
    }
    public void AddCastle()
    {
        _ui.WriteLine(_strings.Messages[GameIsWon]);
        _gameWon = true;
    }
    public void AddFarmer()
    {
        _foodPerDay += 5;
    }
    public void AddLumberjack()
    {
        _woodPerDay += 1;
    }
    public void AddQuarryWorker()
    {
        _metalPerDay += 1;
    }
    public void AddBuilder()
    {
        
    }
    public void AddFood()
    {
        _food += FoodPerDay;
    }
    public void AddFood(int amount)
    {
        _food += amount;
    }
    public void AddMetal()
    {
        _metal += _metalPerDay;
    }
    public void AddMetal(int amount)
    {
        _metal += amount;
    }
    public void RemoveMetal(int amount)
    {
        _metal -= amount;
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
        _wood += _woodPerDay;
    }
    public void AddWood(int amount)
    {
        _wood += amount;
    }
    public void RemoveWood(int amount)
    {
        _wood -= amount;
    }
    public void AddWorker(string name, Worker.Type job, Worker.WorkDelegate workDelegate)
    {
        if (!string.IsNullOrEmpty(name))
        {
            Worker worker = new Worker(name, job, workDelegate);
            _workers.Add(worker);
            worker.JobAction.Invoke(this);
        }
        else
        {
            _ui.WriteLine(_strings.Messages[AddWorkerNoName]);
            // _ui.WriteLine(Strings.MessagesAddWorkerNoName);
        }
        
    }
    public void Build()
    {
        if (_projects.Count == 0)
        {
            _ui.WriteLine(_strings.Messages[BuildNoProjects]);
            return;
        }
        
        // Work on the project at the top of the list.
        var project = _projects[0];
        project.WorkOn();

        if (project.IsComplete) BuildingCompleted(project);
    }

    private void BuryDead(Worker worker)
    {
        _workers.Remove(worker);
        
        // If all workers of the village are dead and buried, the game is over.
        if (_workers.Count != 0) return;
        _gameOver = true;
        _ui.WriteLine("Game Over!");
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
            }
            else
            {
                // The village has no food left, thus the worker gets hungry.
                worker.Starve();
                if (worker.DaysHungry == 40)
                {
                    worker.Die();
                    BuryDead(worker);
                    
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
        return this.FoodPerDay;
    }
    public int GetMetal()
    {
        return this._metal;
    }
    public int GetMetalPerDay()
    {
        return this._metalPerDay;
    }
    public List<Building> GetProjects()
    {
        return this._projects;
    }
    public int GetWood()
    {
        return this._wood;
    }
    public int GetWoodPerDay()
    {
        return this._woodPerDay;
    }
    public List<Worker> GetWorkers()
    {
        return this._workers;
    }
    public int GetMaxWorkers()
    {
        return this._maxWorkers;
    }

    public int GetFood()
    {
        return this._food;
    }
    public List<Building> GetBuildings()
    {
        return this._buildings;
    }
    
}