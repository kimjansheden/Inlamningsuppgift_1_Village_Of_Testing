namespace Inlamningsuppgift_1_Village_Of_Testing;

public class DayEventArgs : EventArgs
{
    private readonly string _message;

    public string Message => _message;

    public DayEventArgs(string message)
    {
        _message = message;
    }

    public override string ToString()
    {
        return _message;
    }
}