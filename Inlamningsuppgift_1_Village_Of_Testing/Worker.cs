namespace Inlamningsuppgift_1_Village_Of_Testing;

public class Worker
{
    private bool _alive = true;
    private int _daysHungry = 0;
    private bool _hungry = false;
    private string _job;
    private string _name;
    public delegate void WorkDelegate();
    private WorkDelegate _workDelegate;

    public string Name
    {
        get => _name;
        set => _name = value;
    }
    public bool Hungry
    {
        get => _hungry;
        set => _hungry = value;
    }
    public int DaysHungry
    {
        get => _daysHungry;
        set => _daysHungry = value;
    }
    public bool Alive
    {
        get => _alive;
        set => _alive = value;
    }

    public Worker(string name, WorkDelegate workDelegate)
    {
        this.Name = name;
        this._workDelegate = workDelegate;
    }
    public void DoWork()
    {
        _workDelegate();
    }
}