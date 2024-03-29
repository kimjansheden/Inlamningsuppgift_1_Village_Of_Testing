using static GameLib.Building;

namespace GameLib;

public class Strings
{
    public readonly IDictionary<Message, string> Messages;
    public enum Message
    {
        MenuEnterValidNumber,
        MenuStart,
        MenuAddWorker,
        MenuAddWorkerGiveName,
        MenuAddProject,
        MenuBanishWorker,
        MenuBanishWorkerWorkerStats,
        MenuDay,
        MenuQuit,
        MenuSaved,
        MenuLoaded,
        BuildNoProjects,
        BuildNotEnoughResources,
        GameIsWon,
        GameIsOver,
        WorkerAddNoName,
        WorkerAddNoRoom,
        WorkerCantBanishStarving,
        WorkerHasBeenBanished
    }

    // Store a reference to the Village object.
    private Village _village;
    private IUI _ui;
    
    private string _workerStats = "";
    private string _newLine;
    public Strings(Village village, IUI ui) : this(ui)
    {
        _village = village;
        var workerNumber = 0;
        foreach (var worker in village.GetWorkers())
        {
            _workerStats += $"{workerNumber += 1}. {worker.Name}, {worker.Job}. Days hungry: {worker.DaysHungry}{_newLine}";
        }
        {
            Messages[Message.GameIsWon] = $"The castle is complete! You won! You took {_village.GetDaysGone()} days.";
            Messages[Message.MenuDay] = $"A day goes by and day {_village.GetDaysGone() + 1} begins.";
            Messages[Message.MenuBanishWorkerWorkerStats] = _workerStats;
        }

        village.VillageUpdated += OnVillageUpdated;
    }

    public void OnVillageUpdated(object? sender, EventArgs e)
    {
        Messages[Message.GameIsWon] = $"The castle is complete! You won! You took {_village.GetDaysGone()} days.";
        Messages[Message.MenuDay] = $"A day goes by and day {_village.GetDaysGone() + 1} begins. {_newLine}";

        var workerNumber = 0;
        _workerStats = "";
        foreach (var worker in _village.GetWorkers())
        {
            {
                _workerStats += $"{workerNumber += 1}. {worker.Name}, {worker.Job}. Days hungry: {worker.DaysHungry}{_newLine}";
            }
        }
        Messages[Message.MenuBanishWorkerWorkerStats] = _workerStats;
    }

    public Strings(IUI ui)
    {
        if (ui is ConsoleUI)
        {
            _newLine = Environment.NewLine;
        }
        else if (ui is WebUI)
        {
            _newLine = "<br>";
        }
        var menuChoice = 0;
        Messages = new Dictionary<Message, string>();
        
        Messages[Message.WorkerAddNoName] =  "Please enter a name for the worker.";
        Messages[Message.WorkerAddNoRoom] =
            "The maximum number of workers that can live in the village is reached. No new workers can live in the village until more houses are built.";
        Messages[Message.MenuEnterValidNumber] = "Please enter a valid option.";
        Messages[Message.MenuStart] = $"What would you like to do?{_newLine}" +
          $"{menuChoice += 1}. View your village{_newLine}" +
          $"{menuChoice += 1}. Add worker{_newLine}" +
          $"{menuChoice += 1}. Banish worker{_newLine}" +
          $"{menuChoice += 1}. Add project{_newLine}" +
          $"{menuChoice += 1}. Next day{_newLine}" +
          $"{menuChoice += 1}. Save progress{_newLine}" +
          $"{menuChoice += 1}. Load progress{_newLine}" +
          $"{menuChoice += 1}. Quit";
        
        menuChoice = 0;
        Messages[Message.MenuAddWorker] = $"What kind of worker?{_newLine}" +
          $"{menuChoice += 1}. Lumberjack{_newLine}" +
          $"{menuChoice += 1}. Farmer{_newLine}" +
          $"{menuChoice += 1}. Quarry worker{_newLine}" +
          $"{menuChoice += 1}. Builder{_newLine}";
        Messages[Message.MenuAddWorkerGiveName] = "Give the worker a name.";
        
        menuChoice = 0;
        Messages[Message.MenuAddProject] = $"What kind of project would you like to add?{_newLine}" +
           $"{menuChoice += 1}. House    –    Costs {GetCosts(Building.Type.House).costWood} wood & {GetCosts(Building.Type.House).costMetal} metal, takes {GetDaysToComplete(Building.Type.House)} days to build.{_newLine}" +
           $"{menuChoice += 1}. Farm    –    Costs {GetCosts(Building.Type.Farm).costWood} wood & {GetCosts(Building.Type.Farm).costMetal} metal, takes {GetDaysToComplete(Building.Type.Farm)} days to build.{_newLine}" +
           $"{menuChoice += 1}. Quarry    –    Costs {GetCosts(Building.Type.Quarry).costWood} wood & {GetCosts(Building.Type.Quarry).costMetal} metal, takes {GetDaysToComplete(Building.Type.Quarry)} days to build.{_newLine}" +
           $"{menuChoice += 1}. Woodmill    –    Costs {GetCosts(Building.Type.Woodmill).costWood} wood & {GetCosts(Building.Type.Woodmill).costMetal} metal, takes {GetDaysToComplete(Building.Type.Woodmill)} days to build.{_newLine}" +
           $"{menuChoice += 1}. Castle    –    Costs {GetCosts(Building.Type.Castle).costWood} wood & {GetCosts(Building.Type.Castle).costMetal} metal, takes {GetDaysToComplete(Building.Type.Castle)} days to build.";
        Messages[Message.MenuBanishWorker] =
            $"So you wish to reduce your workforce, eh? Please write the number of the poor soul you would like to banish.{_newLine}";
        Messages[Message.BuildNoProjects] = "You must add a project first.";
        Messages[Message.MenuSaved] =
            "Your progress has been saved. (Not really though, because this function is not implemented.)";
        Messages[Message.MenuLoaded] =
            "Your progress has been loaded. (Not really though, because this function is not implemented.)";
        Messages[Message.BuildNotEnoughResources] = "Not enough resources to build this type of building.";
        Messages[Message.WorkerCantBanishStarving] =
            "You monster! Trying to banish a starving worker! At least have the decency to give them a meal before you kick them out.";
        Messages[Message.WorkerHasBeenBanished] =
            $" gathers the few things they have in a little bundle and looks at you with sadness in their eyes. It's almost as if the eyes speak to you, and you can see the unspoken question in them: \"What did I do wrong?\"{_newLine}";
        Messages[Message.MenuQuit] = "Thank you for playing, see you next time!";
        Messages[Message.GameIsOver] = "Game Over!";
    }
}