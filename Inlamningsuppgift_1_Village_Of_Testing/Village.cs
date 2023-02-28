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
    private int _maxWorkers = 0;

    private readonly IUI _ui;
    
    private readonly Strings _strings = new Strings();
    
    public bool GameOver => _gameOver;

    public Village(IUI ui)
    {
        // Initialize the UI
        _ui = ui;

        // Create three houses.
        for (int i = 0; i < 3; i++)
        {
            //_buildings.Add((new Building(Building.Type.House)));
            Build(Building.Type.House);
        }
    }
    
    public void Day()
    {
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
        
        // A day goes by.
        _daysGone += 1;
    }

    public void AddFood(int amount)
    {
        _food += amount;
    }
    public void AddMetal(int amount)
    {
        _metal += amount;
    }
    public void AddProject(Building.Type type)
    {
        _projects.Add(new Building(type));
    }
    public void AddWood(int amount)
    {
        _wood += amount;
    }
    public void AddWorker(string name, Worker.WorkDelegate workDelegate)
    {
        if (!string.IsNullOrEmpty(name))
        {
            Worker worker = new Worker(name, workDelegate);
            _workers.Add(worker);
        }
        else
        {
            _ui.WriteLine(_strings.Messages[AddWorkerNoName]);
            // _ui.WriteLine(Strings.MessagesAddWorkerNoName);
        }
        
    }
    public void Build(Building.Type type)
    {
        _buildings.Add((new Building(type)));

        // Checks how many houses the village has after the latest building has been built.
        var numberOfHouses = 0;
        foreach (var building in _buildings)
        {
            if (building.BuildingType == Building.Type.House)
            {
                numberOfHouses += 1;
            }
        }
        
        // For every house that exists in the village, allow 2 workers to live in the village.
        _maxWorkers = 0; // Reset the counter first.
        for (int i = 0; i < numberOfHouses; i++)
        {
            _maxWorkers += 2;
        }
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
                worker.Hungry = false;
                worker.DaysHungry = 0;
            }
            else
            {
                // The village has no food left, thus the worker gets hungry.
                worker.Hungry = true;
                worker.DaysHungry += 1;
                if (worker.DaysHungry == 40)
                {
                    worker.Alive = false;
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
        return this._daysGone;
    }
    public int GetFoodPerDay()
    {
        return this._foodPerDay;
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