namespace Inlamningsuppgift_1_Village_Of_Testing;
class Program
{
    public static void Main(string[] args)
    {
        Game game = new Game(new ConsoleUI());
        game.Run();
    }
}