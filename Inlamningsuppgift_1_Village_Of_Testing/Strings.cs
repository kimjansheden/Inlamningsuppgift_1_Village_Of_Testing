namespace Inlamningsuppgift_1_Village_Of_Testing;

public class Strings
{
    public readonly IDictionary<Message, string> Messages;
    public enum Message
    {
        AddWorkerNoName,
        MenuEnterValidNumber,
        Menu,
        BuildNoProjects
    }
    
    public Strings()
    {
        var menuChoice = 0;
        Messages = new Dictionary<Message, string>
        {
            {
                Message.AddWorkerNoName, "Please enter a name for the worker."
            },
            {
                Message.MenuEnterValidNumber, "Please enter a valid option."
            },
            {
                Message.Menu, "What would you like to do?\n" +
                              $"{menuChoice += 1}. Add worker\n" +
                              $"{menuChoice += 1}. Add project\n" +
                              $"{menuChoice += 1}. Next day\n" +
                              $"{menuChoice += 1}. Quit"
            },
            {
                Message.BuildNoProjects, "You must add a project first."
            }
        };
    }
}