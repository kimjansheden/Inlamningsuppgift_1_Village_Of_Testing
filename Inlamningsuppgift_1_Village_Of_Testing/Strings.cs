namespace Inlamningsuppgift_1_Village_Of_Testing;

public class Strings
{
    public readonly IDictionary<Message, string> Messages;
    public enum Message
    {
        AddWorkerNoName,
        MenuEnterValidNumber
    }
    
    public Strings()
    {
        Messages = new Dictionary<Message, string>
        {
            {
                Message.AddWorkerNoName, "Please enter a name for the worker."
            },
            {
                Message.MenuEnterValidNumber, "Please enter a valid option."
            }
        };
    }
}