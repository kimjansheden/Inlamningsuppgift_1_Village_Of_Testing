namespace Inlamningsuppgift_1_Village_Of_Testing;

public class Building
{
    private int _costMetal;
    private int _costWood;
    private int _daysToComplete;
    private int _daysSpent;
    private bool _isComplete;
    private Type _type;
    private Action<Village> _buildAction;
    public Action<Village> BuildAction => _buildAction;

    public Type BuildingType
    {
        get => _type;
        set => _type = value;
    }

    public bool IsComplete => _isComplete;

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
    // private delegate (int, int, int) ResourcesCost(Building building);
    
    private Dictionary<Type, Func<Building, (int, int, int, Action<Village> action)>> _buildingTypeProperties =
        new Dictionary<Type, Func<Building, (int, int, int, Action<Village> action)>>()
        {
            { Type.House, b => (5, 0, 3, village => village.AddHouse()) },
            { Type.Woodmill, b => (5, 1, 5, village => village.AddWoodmill()) },
            { Type.Quarry, b => (3, 5, 7, village => village.AddQuarry()) },
            { Type.Farm, b => (5, 2, 5, village => village.AddFarm()) },
            { Type.Castle, b => (50, 50, 50, village => village.AddCastle()) },
        };
    
    public Building(Type type, Village village)
    {
        _buildingTypeProperties.TryGetValue(type, out var typeProperties);
        (_costWood, _costMetal, _daysToComplete, _buildAction) = typeProperties!(this);
        BuildingType = type;
        village.RemoveWood(_costWood);
        village.RemoveMetal(_costMetal);
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
    public void WorkOn()
    {
        _daysSpent += 1;

        if (_daysSpent == _daysToComplete) _isComplete = true;
    }
}