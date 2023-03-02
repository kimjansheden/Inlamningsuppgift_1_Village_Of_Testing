namespace Inlamningsuppgift_1_Village_Of_Testing;

public class Strings
{
    public readonly IDictionary<Message, string> Messages;
    public enum Message
    {
        AddWorkerNoName,
        AddWorkerNoRoom,
        MenuEnterValidNumber,
        MenuStart,
        MenuAddWorker,
        MenuAddWorkerGiveName,
        MenuAddProject,
        MenuDay,
        MenuViewVillage,
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
            Messages[Message.MenuDay] = $"A day goes by and day {_village.GetDaysGone() + 1} begins.";
        }

        village.DaysGoneUpdated += OnDaysGoneUpdated;
    }

    private void OnDaysGoneUpdated(object? sender, EventArgs e)
    {
        Messages[Message.GameIsWon] = $"The castle is complete! You won! You took {_village.GetDaysGone()} days.";
        Messages[Message.MenuDay] = $"A day goes by and day {_village.GetDaysGone()} begins.";
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
                Message.MenuStart, "What would you like to do?\n" +
                              $"{menuChoice += 1}. View your village\n" +
                              $"{menuChoice += 1}. Add worker\n" +
                              $"{menuChoice += 1}. Add project\n" +
                              $"{menuChoice += 1}. Next day\n" +
                              $"{menuChoice += 1}. Quit"
            },
            {
                Message.MenuAddWorker, "What kind of worker?\n" +
                              $"{menuChoice += 1}. Lumberjack\n" +
                              $"{menuChoice += 1}. Farmer\n" +
                              $"{menuChoice += 1}. Quarry worker\n" +
                              $"{menuChoice += 1}. Builder"
            },
            {
                Message.MenuAddWorkerGiveName, "Give the worker a name."
            },
            {
                Message.MenuAddProject, "What kind of project would you like to add?\n" +
                                        $"{menuChoice += 1}. House\n" +
                                        $"{menuChoice += 1}. Farm\n" +
                                        $"{menuChoice += 1}. Quarry\n" +
                                        $"{menuChoice += 1}. Woodmill\n" +
                                        $"{menuChoice += 1}. Castle"
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