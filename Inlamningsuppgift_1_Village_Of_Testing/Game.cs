using static Inlamningsuppgift_1_Village_Of_Testing.Strings;
using static Inlamningsuppgift_1_Village_Of_Testing.Strings.Message;

namespace Inlamningsuppgift_1_Village_Of_Testing;

public class Game
{
    private readonly IUI _ui;
    private readonly Village _village;
    private readonly Strings _strings = new Strings();
    public Game(IUI ui)
    {
        // Initialize the UI
        // Maybe delete it from village if it's enough to have it on the Game level?
        // But even if I have it there, I still only have to change it in one place, because I send the right ui on to Village down here.
        _ui = ui;
        _village = new Village(_ui);
        // Initialize the village
        
    }
    public void Run()
    {
        while (!_village.GameOver)
        {
            Menu();
        }
    }

    public void Menu()
    {
        _ui.WriteLine(_strings.Messages[Message.Menu]);
        var input = _ui.ReadLine();
        switch(input)
        {
            case "1":
                AddWorkerInput();
                break;
            case "2":
                AddProjectInput();
                break;
            case "3":
                _village.Day();
                break;
            case "4":
                return;
            default:
                _ui.WriteLine(_strings.Messages[MenuEnterValidNumber]);
                break;
        }
    }

    private void AddProjectInput()
    {
        throw new NotImplementedException();
    }

    private void AddWorkerInput()
    {
        _ui.WriteLine("What kind of worker?");
        _ui.WriteLine("1. Lumberjack");
        _ui.WriteLine("2. Farmer");
        _ui.WriteLine("3. Quarry worker");
        _ui.WriteLine("4. Builder");
        var input = _ui.ReadLine();
        _ui.WriteLine("Give the worker a name.");
        var workerName = _ui.ReadLine();
        switch(input)
        {
            case "1":
                _village.AddWorker(workerName, Worker.Type.Lumberjack, () => _village.AddWood());
                break;
            case "2":
                _village.AddWorker(workerName, Worker.Type.Farmer, () => _village.AddFood());
                break;
            case "3":
                _village.AddWorker(workerName, Worker.Type.QuarryWorker, () => _village.AddMetal());
                break;
            case "4":
                // Kommer behöva prompta efter byggnadstyp också.
                _village.AddWorker(workerName, Worker.Type.Builder, () => _village.AddProject(Building.Type.House));
                break;
            default:
                _ui.WriteLine(_strings.Messages[MenuEnterValidNumber]);
                break;
        }
    }
}