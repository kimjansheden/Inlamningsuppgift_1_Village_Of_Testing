using Microsoft.JSInterop;
using static GameLib.Strings.Message;

namespace GameLib;

public class Game : IGame
{
    private readonly IUI _ui;
    public readonly Village Village;
    private readonly Strings _strings;
    public Game(IUI ui)
    {
        // Initialize the UI.
        _ui = ui;

        // Initialize the village.
        Village = new Village(_ui);
        
        // Initialize the strings.
        _strings = new Strings(Village, _ui);
    }

    public Game(IUI ui, IJSRuntime jsRuntime)
    {
        _ui = ui;
        Village = new Village(_ui, jsRuntime);
        _strings = new Strings(Village, _ui);
    }

    // For testing purposes.
    public Game(IUI ui, int startFood = 10, int startWood = 0, int startMetal = 0, int startMaxWorkers = 0, int startBuilders = 0, IJSRuntime jsRuntime = null)
    {
        _ui = ui;
        Village = new Village(_ui, startFood, startWood, startMetal, startMaxWorkers, startBuilders, jsRuntime: jsRuntime);
        _strings = new Strings(Village, _ui);
    }
    public void Run()
    {
        _ui.Clear(); //To start the console application without the extra output in the beginning (file path, exit code)
        Menu();
    }

    public void Menu()
    {
        while (Village is { GameOver: false, GameWon: false })
        {
            _ui.WriteLine(Village.GetEvents());
            _ui.WriteLine(_strings.Messages[MenuStart]);
            var input = _ui.ReadLine();
            _ui.Clear();
            switch(input)
            {
                case "1":
                    _ui.Clear();
                    _ui.WriteLine(Village.GetStats());
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
                    Village.Day();
                    if (Village.GameWon)
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
                    Village.Quit();
                    return;
                default:
                    _ui.Clear();
                    _ui.WriteLine(_strings.Messages[MenuEnterValidNumber]);
                    break;
            }
        }
    }

    public void BanishWorkerInput()
    {
        _ui.WriteLine(_strings.Messages[MenuBanishWorker]);
        _ui.WriteLine(_strings.Messages[MenuBanishWorkerWorkerStats]);
        var workerNumberSuccess = int.TryParse(_ui.ReadLine(), out  int workerNumber);
        if (!workerNumberSuccess)
        {
            _ui.WriteLine(_strings.Messages[MenuEnterValidNumber]);
        }
        for (int i = 1; i < Village.GetWorkers().Count + 1; i++)
        {
            if (workerNumber == i)
            {
                Village.Banish(Village.GetWorkers()[i - 1]); 
            }
        }
    }

    public void AddProjectInput()
    {
        _ui.WriteLine(_strings.Messages[MenuAddProject]);
        var input = _ui.ReadLine();
        switch(input)
        {
            case "1":
                _ui.Clear();
                Village.AddProject(Building.Type.House);
                break;
            case "2":
                _ui.Clear();
                Village.AddProject(Building.Type.Farm);
                break;
            case "3":
                _ui.Clear();
                Village.AddProject(Building.Type.Quarry);
                break;
            case "4":
                _ui.Clear();
                Village.AddProject(Building.Type.Woodmill);
                break;
            case "5":
                _ui.Clear();
                Village.AddProject(Building.Type.Castle);
                break;
            default:
                _ui.WriteLine(_strings.Messages[MenuEnterValidNumber]);
                break;
        }
    }

    public void AddWorkerInput()
    {
        _ui.WriteLine(_strings.Messages[MenuAddWorker]);
        var input = _ui.ReadLine();
        _ui.WriteLine(_strings.Messages[MenuAddWorkerGiveName]);
        var workerName = _ui.ReadLine();
        switch(input)
        {
            case "1":
                _ui.Clear();
                Village.AddWorker(workerName, Worker.Type.Lumberjack, () => Village.AddWood());
                break;
            case "2":
                _ui.Clear();
                Village.AddWorker(workerName, Worker.Type.Farmer, () => Village.AddFood());
                break;
            case "3":
                _ui.Clear();
                Village.AddWorker(workerName, Worker.Type.QuarryWorker, () => Village.AddMetal());
                break;
            case "4":
                _ui.Clear();
                Village.AddWorker(workerName, Worker.Type.Builder, () => Village.Build());
                break;
            default:
                _ui.WriteLine(_strings.Messages[MenuEnterValidNumber]);
                break;
        }
    }
}