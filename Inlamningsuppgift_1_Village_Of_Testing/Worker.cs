namespace Inlamningsuppgift_1_Village_Of_Testing;

public class Worker
{
    private bool _alive = true;
    private int _daysHungry = 0;
    private bool _hungry = false;
    private readonly Type _job;
    private readonly string _name;
    public delegate void WorkDelegate();
    private WorkDelegate _workDelegate;
    
    private Action<Village> _jobAction;
    public Action<Village> JobAction => _jobAction;
    
    public enum Type {
        Farmer,
        Lumberjack,
        QuarryWorker,
        Builder
    }
    
    private Dictionary<Type, Func<Worker, Action<Village>>> _jobProperties =
        new Dictionary<Type, Func<Worker, Action<Village>>>()
        {
            { Type.Farmer, w => village => village.AddFarmer() },
            { Type.Lumberjack, b => village => village.AddLumberjack() },
            { Type.QuarryWorker, b => village => village.AddQuarryWorker() },
            { Type.Builder, b => village => village.AddBuilder() }
        };

    public string Name => _name;

    public bool Hungry => _hungry;

    public int DaysHungry => _daysHungry;

    public bool Alive => _alive;

    public Type Job => _job;

    public Worker(string name, Type job, WorkDelegate workDelegate)
    {
        this._name = name;
        this._job = job;
        this._workDelegate = workDelegate;
        _jobProperties.TryGetValue(job, out var jobProperties);
        _jobAction = jobProperties!(this);
    }
    public void DoWork()
    {
        _workDelegate();
    }

    public void Eat()
    {
        _hungry = false;
        _daysHungry = 0;
    }
    public void Starve()
    {
        _hungry = true;
        _daysHungry += 1;
    }
    public void Die()
    {
        _alive = false;
    }
}