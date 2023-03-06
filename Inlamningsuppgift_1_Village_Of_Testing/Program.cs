namespace Inlamningsuppgift_1_Village_Of_Testing;
class Program
{
    public static void Main(string[] args)
    {
        Game game = new Game(new ConsoleUI());
        //Game game = new Game(new ConsoleUI(), 1, 100, 100, 100, 2);
        game.Run();
    }
}