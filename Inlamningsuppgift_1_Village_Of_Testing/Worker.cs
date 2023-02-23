namespace Inlamningsuppgift_1_Village_Of_Testing;

public class Worker
{
    private bool _alive;
    private int _daysHungry;
    private bool _hungry;
    private string _job;
    private string _name;

    public delegate void Work(Worker worker);

    public void DoWork()
    {

    }
    public void FeedWorker()
    {

    }
}