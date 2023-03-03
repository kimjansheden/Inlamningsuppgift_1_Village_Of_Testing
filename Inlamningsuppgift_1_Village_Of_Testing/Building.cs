namespace Inlamningsuppgift_1_Village_Of_Testing;

public class Building
{
    private readonly int _costMetal;
    private readonly int _costWood;
    private readonly int _daysToComplete;
    private int _daysSpent;
    private bool _isComplete;
    private Type _type;
    private readonly Action<Village> _buildAction;
    public Action<Village> BuildAction => _buildAction;

    public Type BuildingType
    {
        get => _type;
        set => _type = value;
    }

    public bool IsComplete => _isComplete;

    public int DaysToComplete => _daysToComplete;

    public int DaysSpent => _daysSpent;

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
    
    private static readonly Dictionary<Type, (int costWood, int costMetal, int daysToComplete, Action<Village> action)> BuildingTypeProperties =
        new Dictionary<Type, (int costWood, int costMetal, int daysToComplete, Action<Village> action)>()
        {
            { Type.House, (5, 0, 3, village => village.AddHouse()) },
            { Type.Woodmill, (5, 1, 5, village => village.AddWoodmill()) },
            { Type.Quarry, (3, 5, 7, village => village.AddQuarry()) },
            { Type.Farm, (5, 2, 5, village => village.AddFarm()) },
            { Type.Castle, (50, 50, 50, village => village.AddCastle()) }
        };
    
    public Building(Type type, Village village)
    {
        BuildingTypeProperties.TryGetValue(type, out var typeProperties);
        (_costWood, _costMetal, _daysToComplete, _buildAction) = typeProperties;
        BuildingType = type;
        
        // The costs are deducted from the village's resources when the building is added to the list of projects.
        village.RemoveWood(_costWood);
        village.RemoveMetal(_costMetal);
    }

    public int GetCostWood()
    {
        return _costWood;
    }
    public int GetDaysToComplete()
    {
        return DaysToComplete;
    }
    public static int GetDaysToComplete(Type type)
    {
        BuildingTypeProperties.TryGetValue(type, out var typeProperties);
        var (_, _, daysToComplete, _) = typeProperties;
        return daysToComplete;
    }
    public int GetCostMetal()
    {
        return _costMetal;
    }

    public static (int costWood, int costMetal) GetCosts(Type type)
    {
        BuildingTypeProperties.TryGetValue(type, out var typeProperties);
        var (costWood, costMetal, _, _) = typeProperties;
        return (costWood, costMetal);
    }
    public void WorkOn()
    {
        _daysSpent = DaysSpent + 1;

        if (DaysSpent == DaysToComplete) _isComplete = true;
    }
}