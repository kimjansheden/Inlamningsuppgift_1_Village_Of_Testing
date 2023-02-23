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
    public Village()
    {
        // Create three houses.
        for (int i = 0; i < 3; i++)
        {
            _buildings.Add((new Building(Building.Type.House)));
        }
    }

    public void AddFood()
    {
        
    }
    public void AddMetal()
    {
        
    }
    public void AddProject(string name)
    {
        
    }
    public void AddWood()
    {

    }
    public void AddWorker(string name, Worker.Work work)
    {

    }
    public void Build()
    {

    }
    public void BuryDead()
    {

    }
    public void Day()
    {

    }
    public void FeedWorkers()
    {

    }
    public void GetDaysGone()
    {

    }
    public void GetFoodPerDay()
    {

    }
    public void GetMetal()
    {

    }
    public void GetMetalPerDay()
    {

    }
    public void GetProjects()
    {

    }
    public void GetWood()
    {

    }
    public void GetWoodPerDay()
    {

    }
    public void GetWorkers()
    {

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