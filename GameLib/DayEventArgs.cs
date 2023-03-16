namespace GameLib;

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