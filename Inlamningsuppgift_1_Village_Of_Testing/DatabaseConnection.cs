namespace Inlamningsuppgift_1_Village_Of_Testing;

public class DatabaseConnection
{
    private int _daysGone;
    private int _food;
    private int _foodPerDay;
    private int _foodPerDayBonus;
    private int _metal;
    private int _metalPerDay;
    private int _metalPerDayBonus;
    private int _wood;
    private int _woodPerDay;
    private int _woodPerDayBonus;
    private List<Building> _buildings = new List<Building>();
    private List<Building> _projects = new List<Building>();
    private List<Worker> _workers = new List<Worker>();
    private bool _gameOver;
    private bool _gameWon;
    private int _maxWorkers;
    private int _farmers;
    private int _lumberjacks;
    private int _quarryworkers;
    private int _builders;
    private int _farms;
    private int _woodmills;
    private int _quarries;
    private int _houses;
    private bool _starvation;
    private List<DayEventArgs> _dayEventsList = new List<DayEventArgs>();
    private List<Worker> _starvingWorkers = new List<Worker>();

    public virtual void Save()
    {
        // Saves all variables to the database.
    }

    public virtual void Load()
    {
        // Loads the variables' values from the database and fills them to recreates the saved state.
        // E.g.
        // reader.GetInt32("daysGone")
        
        // _daysGone = daysGone;
        // Etc. …
        throw new NotImplementedException();
    }

    public virtual int GetWood()
    {
        throw new NotImplementedException();
    }

    public virtual int GetDaysGone()
    {
        throw new NotImplementedException();
    }

    public virtual int GetFood()
    {
        throw new NotImplementedException();
    }

    public virtual int GetFoodPerDay()
    {
        throw new NotImplementedException();
    }

    public virtual int GetFoodPerDayBonus()
    {
        throw new NotImplementedException();
    }

    public virtual int GetMetal()
    {
        throw new NotImplementedException();
    }

    public virtual int GetMetalPerDay()
    {
        throw new NotImplementedException();
    }

    public virtual int GetMetalPerDayBonus()
    {
        throw new NotImplementedException();
    }

    public virtual int GetWoodPerDay()
    {
        throw new NotImplementedException();
    }

    public virtual int GetWoodPerDayBonus()
    {
        throw new NotImplementedException();
    }

    public virtual List<Building> GetBuildings()
    {
        throw new NotImplementedException();
    }

    public virtual List<Building> GetProjects()
    {
        throw new NotImplementedException();
    }

    public virtual List<Worker> GetWorkers()
    {
        throw new NotImplementedException();
    }

    public virtual bool GetGameOver()
    {
        throw new NotImplementedException();
    }

    public virtual bool GetGameWon()
    {
        throw new NotImplementedException();
    }

    public virtual int GetMaxWorkers()
    {
        throw new NotImplementedException();
    }

    public virtual int GetFarmers()
    {
        throw new NotImplementedException();
    }

    public virtual int GetLumberjacks()
    {
        throw new NotImplementedException();
    }

    public virtual int GetQuarryWorkers()
    {
        throw new NotImplementedException();
    }

    public virtual int GetBuilders()
    {
        throw new NotImplementedException();
    }

    public virtual int GetFarms()
    {
        throw new NotImplementedException();
    }

    public virtual int GetWoodmills()
    {
        throw new NotImplementedException();
    }

    public virtual int GetQuarries()
    {
        throw new NotImplementedException();
    }

    public virtual int GetHouses()
    {
        throw new NotImplementedException();
    }

    public virtual bool GetStarvation()
    {
        throw new NotImplementedException();
    }

    public virtual List<DayEventArgs> GetDayEventsList()
    {
        throw new NotImplementedException();
    }

    public virtual List<Worker> GetStarvingWorkers()
    {
        throw new NotImplementedException();
    }
}