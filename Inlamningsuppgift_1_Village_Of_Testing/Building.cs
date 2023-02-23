namespace Inlamningsuppgift_1_Village_Of_Testing;

public class Building
{
    private bool _complete;
    private int _costMetal;
    private int _costWood;
    private int _daysToComplete;
    private int _daysSpent;
    private int _daysToBuild;
    private int _daysWorkedOn;
    private bool _isBuilt;
    private string _name;
    public enum Type {
        House = 1,
        Woodmill = 2,
        Quarry = 3,
        Farm = 4,
        Castle = 5
    }

    // Denna delegate behövs inte skrivas ut; den är samma som
    // Func<Building, (int, int, int)>. Func of Building tar en Building som parameter och
    // returnerar en tupel med tre ints.
    //private delegate (int, int, int) ResourcesCost(Building building);
    
    private Dictionary<Type, Func<Building, (int, int, int)>> _buildingTypeCosts =
        new Dictionary<Type, Func<Building, (int, int, int)>>()
        {
            { Type.House, b => (5, 0, 3) },
            { Type.Woodmill, b => (5, 1, 5) },
            { Type.Quarry, b => (3, 5, 7) },
            { Type.Farm, b => (5, 2, 5) },
            { Type.Castle, b => (50, 50, 50) },
        };
    
    public Building(Type type)
    {
        _buildingTypeCosts.TryGetValue(type, out var typeCost);
        (_costWood, _costMetal, _daysToComplete) = typeCost!(this);
    }

    public int GetCostWood()
    {
        return _costWood;
    }
    public int GetDaysToComplete()
    {
        return _daysToComplete;
    }
    public int GetCostMetal()
    {
        return _costMetal;
    }
    public void AddFood()
    {

    }
    public void AddMetal()
    {

    }
    public void AddWood()
    {

    }
    public void Build()
    {

    }
    public void IsBuilt()
    {

    }
}