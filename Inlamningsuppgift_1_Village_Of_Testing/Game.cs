using static Inlamningsuppgift_1_Village_Of_Testing.Strings;
using static Inlamningsuppgift_1_Village_Of_Testing.Strings.Message;

namespace Inlamningsuppgift_1_Village_Of_Testing;

public class Game
{
    private readonly IUI _ui;
    private readonly Village _village;
    private readonly Strings _strings;
    public Game(IUI ui)
    {
        // Initialize the UI.
        _ui = ui;

        // Initialize the village.
        _village = new Village(_ui);
        
        // Initialize the strings.
        _strings = new Strings(_village);
    }

    // For testing purposes.
    public Game(IUI ui, int startFood = 10, int startWood = 0, int startMetal = 0, int startMaxWorkers = 0, int startBuilders = 0)
    {
        _ui = ui;
        _village = new Village(_ui, startFood, startWood, startMetal, startMaxWorkers, startBuilders);
        _strings = new Strings(_village);
    }
    public void Run()
    {
        _ui.Clear(); //To start the console application without the extra output in the beginning (file path, exit code)
        Menu();
    }

    public void Menu()
    {
        while (_village is { GameOver: false, GameWon: false })
        {
            _ui.WriteLine(_village.GetEvents());
            _ui.WriteLine(_strings.Messages[MenuStart]);
            var input = _ui.ReadLine();
            _ui.Clear();
            switch(input)
            {
                case "1":
                    _ui.Clear();
                    _ui.WriteLine(_village.GetStats());
                    break;
                case "2":
                    _ui.Clear();
                    AddWorkerInput();
                    break;
                case "3":
                    _ui.Clear();
                    BanishWorkerInput();
                    break;
                case "4":
                    _ui.Clear();
                    AddProjectInput();
                    break;
                case "5":
                    _ui.Clear();
                    _village.Day();
                    if (_village.GameWon)
                        break;
                    _ui.WriteLine(_strings.Messages[MenuDay]);
                    break;
                case "6":
                    _ui.WriteLine(_strings.Messages[MenuSaved]);
                    break;
                case "7":
                    _ui.WriteLine(_strings.Messages[MenuLoaded]);
                    break;
                case "8":
                    _ui.Clear();
                    _ui.WriteLine(_strings.Messages[MenuQuit]);
                    return;
                default:
                    _ui.Clear();
                    _ui.WriteLine(_strings.Messages[MenuEnterValidNumber]);
                    break;
            }
        }
    }

    private void BanishWorkerInput()
    {
        _ui.WriteLine(_strings.Messages[MenuBanishWorker]);
        _ui.WriteLine(_strings.Messages[MenuBanishWorkerWorkerStats]);
        var workerNumberSuccess = int.TryParse(_ui.ReadLine(), out  int workerNumber);
        if (!workerNumberSuccess)
        {
            _ui.WriteLine(_strings.Messages[MenuEnterValidNumber]);
        }
        for (int i = 1; i < _village.GetWorkers().Count + 1; i++)
        {
            if (workerNumber == i)
            {
                _village.Banish(_village.GetWorkers()[i - 1]); 
            }
        }
    }

    private void AddProjectInput()
    {
        _ui.WriteLine(_strings.Messages[MenuAddProject]);
        var input = _ui.ReadLine();
        switch(input)
        {
            case "1":
                _ui.Clear();
                _village.AddProject(Building.Type.House);
                break;
            case "2":
                _ui.Clear();
                _village.AddProject(Building.Type.Farm);
                break;
            case "3":
                _ui.Clear();
                _village.AddProject(Building.Type.Quarry);
                break;
            case "4":
                _ui.Clear();
                _village.AddProject(Building.Type.Woodmill);
                break;
            case "5":
                _ui.Clear();
                _village.AddProject(Building.Type.Castle);
                break;
            default:
                _ui.WriteLine(_strings.Messages[MenuEnterValidNumber]);
                break;
        }
    }

    private void AddWorkerInput()
    {
        _ui.WriteLine(_strings.Messages[MenuAddWorker]);
        var input = _ui.ReadLine();
        _ui.WriteLine(_strings.Messages[MenuAddWorkerGiveName]);
        var workerName = _ui.ReadLine();
        switch(input)
        {
            case "1":
                _ui.Clear();
                _village.AddWorker(workerName, Worker.Type.Lumberjack, () => _village.AddWood());
                break;
            case "2":
                _ui.Clear();
                _village.AddWorker(workerName, Worker.Type.Farmer, () => _village.AddFood());
                break;
            case "3":
                _ui.Clear();
                _village.AddWorker(workerName, Worker.Type.QuarryWorker, () => _village.AddMetal());
                break;
            case "4":
                _ui.Clear();
                _village.AddWorker(workerName, Worker.Type.Builder, () => _village.Build());
                break;
            default:
                _ui.WriteLine(_strings.Messages[MenuEnterValidNumber]);
                break;
        }
    }
}