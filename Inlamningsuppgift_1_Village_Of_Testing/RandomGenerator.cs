namespace Inlamningsuppgift_1_Village_Of_Testing;

public class RandomGenerator
{
    private Random _random = new Random();

    public virtual int Next(int min, int max)
    {
        return _random.Next(min, max);
    }
}