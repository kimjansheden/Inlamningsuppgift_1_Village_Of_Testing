namespace Inlamningsuppgift_1_Village_Of_Testing;

public class Strings
{
    public readonly IDictionary<Message, string> Messages;
    public enum Message
    {
        AddWorkerNoName,
        AddWorkerNoRoom,
        MenuEnterValidNumber,
        Menu,
        BuildNoProjects,
        BuildNotEnoughResources,
        GameIsWon,
        GameIsOver
    }

    // Store a reference to the Village object.
    private Village _village;
    public Strings(Village village) : this()
    {
        _village = village;
        {
            Messages[Message.GameIsWon] = $"The castle is complete! You won! You took {_village.GetDaysGone()} days.";
        }

        village.DaysGoneUpdated += OnDaysGoneUpdated;
    }

    private void OnDaysGoneUpdated(object? sender, EventArgs e)
    {
        Messages[Message.GameIsWon] = $"The castle is complete! You won! You took {_village.GetDaysGone()} days.";
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
                Message.AddWorkerNoRoom, "The maximum number of workers that can live in the village is reached. No new workers can live in the village until more houses are built."
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
            },
            {
                Message.BuildNotEnoughResources, "Not enough resources to build this type of building."
            }
        };
    }
}