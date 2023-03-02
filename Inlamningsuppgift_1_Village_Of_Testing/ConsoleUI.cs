namespace Inlamningsuppgift_1_Village_Of_Testing;

public class ConsoleUI : IUI
{
    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }

    public string ReadLine()
    {
        return Console.ReadLine() ?? string.Empty;
    }

    public void Clear()
    {
        Console.Clear();
    }
}