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
        // Maybe delete it from village if it's enough to have it on the Game level?
        // But even if I have it there, I still only have to change it in one place, because I send the right ui on to Village down here.
        // Yes, the ui is created one time in Program class when the Game object is created. Then that same ui is just passed along to the village and the places that need it.
        _ui = ui;

        // Initialize the village.
        _village = new Village(_ui);
        
        // Initialize the strings.
        _strings = new Strings(_village);
    }
    public void Run()
    {
        _ui.Clear(); //To start the console application without the extra output in the beginning (file path, exit code)
        Menu();
    }

    public void Menu()
    {
        while (!_village.GameOver || !_village.GameWon)
        {
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
                    _ui.WriteLine(_strings.Messages[MenuDay]);
                    break;
                case "6":
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