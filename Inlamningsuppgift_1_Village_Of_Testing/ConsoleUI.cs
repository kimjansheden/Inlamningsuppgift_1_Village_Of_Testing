using System.Runtime.InteropServices;

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
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.Clear();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");  
        }
    }
}