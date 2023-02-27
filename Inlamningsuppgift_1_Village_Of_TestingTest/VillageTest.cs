using Inlamningsuppgift_1_Village_Of_Testing;
using Xunit.Abstractions;

namespace Inlamningsuppgift_1_Village_Of_TestingTest;

public class VillageTest : IClassFixture<VillageFixture>
{
    private VillageFixture _villageFixture;
    private ITestOutputHelper _testOutputHelper;

    public VillageTest(VillageFixture villageFixture, ITestOutputHelper testOutputHelper)
    {
        _villageFixture = villageFixture;
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void VillageConstructorCreates10Food()
    {
        //Arrange
        Village village = _villageFixture.Village;
        var expectedFood = 10;
        
        //Act
        var actualFood = village.GetFood();
        
        //Assert
        Assert.Equal(expectedFood, actualFood);
    }
    [Fact]
    public void VillageConstructorBuilds3Houses()
    {
        //Arrange
        Village village = _villageFixture.Village;
        var expectedHouses = 3;
        
        //Act
        var actualHouses = village.GetBuildings().Count;
        
        //Assert
        Assert.Equal(expectedHouses, actualHouses);
    }
    [Fact]
    public void VillageStartsWith6MaxWorkers()
    {
        //Arrange
        Village village = _villageFixture.Village;
        var expectedMaxWorkers = 6;
        
        //Act
        var actualMaxWorkers = village.GetMaxWorkers();
        
        //Assert
        Assert.Equal(expectedMaxWorkers, actualMaxWorkers);
    }
    public void AddFood()
    {
        
    }
    public void AddMetal()
    {
        
    }
    public void AddProject(string name)
    {
        
    }
    public void AddWood()
    {

    }
    public void BuryDead()
    {

    }
    public void Day()
    {

    }

    [Fact]
    public void Feed4WorkersWith10FoodConsumes4FoodGivesNoHungryWorkersNoDaysHungryAndAllWorkersAreAlive()
    {
        //Arrange
        Village village = _villageFixture.Village;
        village.AddWorker("Sven the Farmer", () => village.AddFood(5));
        village.AddWorker("Bob the Quarry Man", () => village.AddMetal(1));
        village.AddWorker("Olof the Lumberjack", () => village.AddWood(1));
        village.AddWorker("Daisy the Builder", () => village.AddProject(Building.Type.House));
        
        var expectedFoodBeforeFeeding = 10;
        _testOutputHelper.WriteLine($"Expected food before feeding: {expectedFoodBeforeFeeding}");
        var expectedFoodAfterFeeding = expectedFoodBeforeFeeding - village.GetWorkers().Count;
        _testOutputHelper.WriteLine($"Expected food after feeding: {expectedFoodAfterFeeding}");
        var expectedHungryWorkers = 0;
        _testOutputHelper.WriteLine($"Expected hungry workers: {expectedHungryWorkers}");
        var expectedDaysHungry = 0;
        _testOutputHelper.WriteLine($"Expected days hungry: {expectedDaysHungry}");
        var expectedAliveWorkers = village.GetWorkers().Count;
        _testOutputHelper.WriteLine($"Expected workers alive: {expectedAliveWorkers}");

        _testOutputHelper.WriteLine("");
        
        //Act
        var actualFoodBeforeFeeding = village.GetFood();
        _testOutputHelper.WriteLine($"Actual food before feeding: {actualFoodBeforeFeeding}");
        
        village.FeedWorkers(1);
        
        var actualFoodAfterFeeding = village.GetFood();
        _testOutputHelper.WriteLine($"Actual food after feeding: {actualFoodAfterFeeding}");
        
        var actualDaysHungry = 0;
        List<Worker> actualHungryWorkers = new List<Worker>();
        List<Worker> actualAliveWorkers = new List<Worker>();
        
        foreach (var worker in village.GetWorkers())
        {
            actualDaysHungry += worker.DaysHungry;
        }
        _testOutputHelper.WriteLine($"Actual days hungry: {actualDaysHungry}");
        
        foreach (var worker in village.GetWorkers())
        {
            if (worker.Alive == true)
            {
                actualAliveWorkers.Add(worker);
            }
        }

        foreach (var worker in village.GetWorkers())
        {
            if (worker.Hungry == true)
            {
                actualHungryWorkers.Add(worker);
            }
        }

        var actualHungryWorkersCount = actualHungryWorkers.Count;
        _testOutputHelper.WriteLine($"Actual hungry workers: {actualHungryWorkersCount}");
        
        var actualAliveWorkersCount = actualAliveWorkers.Count;
        _testOutputHelper.WriteLine($"Actual workers alive: {actualAliveWorkersCount}");

        //Assert
        Assert.Equal(expectedFoodBeforeFeeding, actualFoodBeforeFeeding);
        Assert.Equal(expectedFoodAfterFeeding, actualFoodAfterFeeding);
        Assert.Equal(expectedHungryWorkers, actualHungryWorkersCount);
        Assert.Equal(expectedDaysHungry, actualDaysHungry);
        Assert.Equal(expectedAliveWorkers, actualAliveWorkersCount);
    }
    
    [Fact]
    public void Feed4WorkersWith2FoodConsumes2FoodGives2HungryWorkers2DaysHungryInTotalAndAllWorkersAreAlive()
    {
        //Arrange
        Village village = _villageFixture.Village;
        village.AddWorker("Sven the Farmer", () => village.AddFood(5));
        village.AddWorker("Bob the Quarry Man", () => village.AddMetal(1));
        village.AddWorker("Olof the Lumberjack", () => village.AddWood(1));
        village.AddWorker("Daisy the Builder", () => village.AddProject(Building.Type.House));
        
        village.FeedWorkers(1);
        village.FeedWorkers(1);
        var expectedFoodBeforeFeeding = 2;
        _testOutputHelper.WriteLine($"Expected food before feeding: {expectedFoodBeforeFeeding}");
        var expectedFoodAfterFeeding = 0;
        _testOutputHelper.WriteLine($"Expected food after feeding: {expectedFoodAfterFeeding}");
        var expectedHungryWorkers = 2;
        _testOutputHelper.WriteLine($"Expected hungry workers: {expectedHungryWorkers}");
        var expectedDaysHungry = 2;
        _testOutputHelper.WriteLine($"Expected days hungry: {expectedDaysHungry}");
        var expectedAliveWorkers = village.GetWorkers().Count;
        _testOutputHelper.WriteLine($"Expected workers alive: {expectedAliveWorkers}");

        _testOutputHelper.WriteLine("");
        
        //Act
        var actualFoodBeforeFeeding = village.GetFood();
        _testOutputHelper.WriteLine($"Actual food before feeding: {actualFoodBeforeFeeding}");
        
        village.FeedWorkers(1);
        
        var actualFoodAfterFeeding = village.GetFood();
        _testOutputHelper.WriteLine($"Actual food after feeding: {actualFoodAfterFeeding}");
        
        var actualDaysHungry = 0;
        List<Worker> actualHungryWorkers = new List<Worker>();
        List<Worker> actualAliveWorkers = new List<Worker>();
        
        foreach (var worker in village.GetWorkers())
        {
            if (worker.Alive == true)
            {
                actualAliveWorkers.Add(worker);
            }
        }

        foreach (var worker in village.GetWorkers())
        {
            if (worker.Hungry == true)
            {
                actualHungryWorkers.Add(worker);
            }
        }

        var actualHungryWorkersCount = actualHungryWorkers.Count;
        _testOutputHelper.WriteLine($"Actual hungry workers: {actualHungryWorkersCount}");
        
        foreach (var worker in village.GetWorkers())
        {
            actualDaysHungry += worker.DaysHungry;
        }
        _testOutputHelper.WriteLine($"Actual days hungry: {actualDaysHungry}");
        
        
        
        var actualAliveWorkersCount = actualAliveWorkers.Count;
        _testOutputHelper.WriteLine($"Actual workers alive: {actualAliveWorkersCount}");

        //Assert
        Assert.Equal(expectedFoodBeforeFeeding, actualFoodBeforeFeeding);
        Assert.Equal(expectedFoodAfterFeeding, actualFoodAfterFeeding);
        Assert.Equal(expectedHungryWorkers, actualHungryWorkersCount);
        Assert.Equal(expectedDaysHungry, actualDaysHungry);
        Assert.Equal(expectedAliveWorkers, actualAliveWorkersCount);
    }
    
    [Fact]
    public void Feed5WorkersWith0StartFoodFor40DaysGives0HungryWorkers0DaysHungryAndNoWorkerIsAliveThusItsGameOver()
    {
        //Arrange
        
        // Add the village and the workers.
        Village village = _villageFixture.Village;
        village.AddWorker("Sven the Farmer", () => village.AddFood(5));
        village.AddWorker("Bob the Quarry Man", () => village.AddMetal(1));
        village.AddWorker("Olof the Lumberjack", () => village.AddWood(1));
        village.AddWorker("Olof II the Lumberjack", () => village.AddWood(1));
        village.AddWorker("Daisy the Builder", () => village.AddProject(Building.Type.House));
        
        // Deplete the village of all food before the test begins.
        village.FeedWorkers(1);
        village.FeedWorkers(1);
        
        
        var expectedFoodBeforeFeeding = 0;
        _testOutputHelper.WriteLine($"Expected food before feeding: {expectedFoodBeforeFeeding}");
        var expectedFoodAfterFeeding = 0;
        _testOutputHelper.WriteLine($"Expected food after feeding: {expectedFoodAfterFeeding}");
        var expectedHungryWorkers = 0; // They don't feel hunger anymore …
        _testOutputHelper.WriteLine($"Expected hungry workers: {expectedHungryWorkers}");
        var expectedDaysHungry = 0; // They don't feel hunger anymore …
        _testOutputHelper.WriteLine($"Expected days hungry: {expectedDaysHungry}");
        var expectedAliveWorkers = 0;
        _testOutputHelper.WriteLine($"Expected workers alive: {expectedAliveWorkers}");
        var expectedGameOver = true;
        _testOutputHelper.WriteLine($"Expected Game Over: {expectedGameOver}");

        _testOutputHelper.WriteLine("");
        
        //Act
        var actualFoodBeforeFeeding = village.GetFood();
        _testOutputHelper.WriteLine($"Actual food before feeding: {actualFoodBeforeFeeding}");

        // 40 days passing by.
        for (int i = 0; i < 40; i++)
        {
            village.Day();
        }
        
        var actualFoodAfterFeeding = village.GetFood();
        _testOutputHelper.WriteLine($"Actual food after feeding: {actualFoodAfterFeeding}");
        
        var actualDaysHungry = 0;
        List<Worker> actualHungryWorkers = new List<Worker>();
        List<Worker> actualAliveWorkers = new List<Worker>();
        
        foreach (var worker in village.GetWorkers())
        {
            actualDaysHungry += worker.DaysHungry;
        }
        _testOutputHelper.WriteLine($"Actual days hungry: {actualDaysHungry}");
        
        foreach (var worker in village.GetWorkers())
        {
            if (worker.Alive == true)
            {
                actualAliveWorkers.Add(worker);
            }
        }

        foreach (var worker in village.GetWorkers())
        {
            if (worker.Hungry == true)
            {
                actualHungryWorkers.Add(worker);
            }
        }

        var actualHungryWorkersCount = actualHungryWorkers.Count;
        _testOutputHelper.WriteLine($"Actual hungry workers: {actualHungryWorkersCount}");
        
        var actualAliveWorkersCount = actualAliveWorkers.Count;
        _testOutputHelper.WriteLine($"Actual workers alive: {actualAliveWorkersCount}");

        var actualGameOver = village.GameOver;
        _testOutputHelper.WriteLine($"Actual Game Over: {actualGameOver}");

        //Assert
        Assert.Equal(expectedFoodBeforeFeeding, actualFoodBeforeFeeding);
        Assert.Equal(expectedFoodAfterFeeding, actualFoodAfterFeeding);
        Assert.Equal(expectedHungryWorkers, actualHungryWorkersCount);
        Assert.Equal(expectedDaysHungry, actualDaysHungry);
        Assert.Equal(expectedAliveWorkers, actualAliveWorkersCount);
        Assert.Equal(expectedGameOver, actualGameOver);
    }
    [Fact]
    public void Feed5WorkersWith0StartFoodFor39DaysGives5HungryWorkers39DaysHungryAndAllWorkerIsAliveThusItsNotGameOver()
    {
        //Arrange
        
        // Add the village and the workers.
        Village village = _villageFixture.Village;
        village.AddWorker("Sven the Farmer", () => village.AddFood(5));
        village.AddWorker("Bob the Quarry Man", () => village.AddMetal(1));
        village.AddWorker("Olof the Lumberjack", () => village.AddWood(1));
        village.AddWorker("Olof II the Lumberjack", () => village.AddWood(1));
        village.AddWorker("Daisy the Builder", () => village.AddProject(Building.Type.House));
        
        // Deplete the village of all food but 2 before the test begins.
        village.FeedWorkers(1);
        village.FeedWorkers(1);
        
        
        var expectedFoodBeforeFeeding = 0;
        _testOutputHelper.WriteLine($"Expected food before feeding: {expectedFoodBeforeFeeding}");
        var expectedFoodAfterFeeding = 0;
        _testOutputHelper.WriteLine($"Expected food after feeding: {expectedFoodAfterFeeding}");
        var expectedHungryWorkers = 5;
        _testOutputHelper.WriteLine($"Expected hungry workers: {expectedHungryWorkers}");
        var expectedDaysHungry = 39;
        _testOutputHelper.WriteLine($"Expected days hungry: {expectedDaysHungry}");
        var expectedAliveWorkers = 5;
        _testOutputHelper.WriteLine($"Expected workers alive: {expectedAliveWorkers}");
        var expectedGameOver = false;
        _testOutputHelper.WriteLine($"Expected Game Over: {expectedGameOver}");

        _testOutputHelper.WriteLine("");
        
        //Act
        var actualFoodBeforeFeeding = village.GetFood();
        _testOutputHelper.WriteLine($"Actual food before feeding: {actualFoodBeforeFeeding}");

        // 40 days passing by.
        for (int i = 0; i < 39; i++)
        {
            village.Day();
        }
        
        var actualFoodAfterFeeding = village.GetFood();
        _testOutputHelper.WriteLine($"Actual food after feeding: {actualFoodAfterFeeding}");
        
        var actualDaysHungry = 0;
        List<Worker> actualHungryWorkers = new List<Worker>();
        List<Worker> actualAliveWorkers = new List<Worker>();
        
        foreach (var worker in village.GetWorkers())
        {
            actualDaysHungry += worker.DaysHungry;
        }
        _testOutputHelper.WriteLine($"Actual days hungry: {actualDaysHungry}");
        
        foreach (var worker in village.GetWorkers())
        {
            if (worker.Alive == true)
            {
                actualAliveWorkers.Add(worker);
            }
        }

        foreach (var worker in village.GetWorkers())
        {
            if (worker.Hungry == true)
            {
                actualHungryWorkers.Add(worker);
            }
        }

        var actualHungryWorkersCount = actualHungryWorkers.Count;
        _testOutputHelper.WriteLine($"Actual hungry workers: {actualHungryWorkersCount}");
        
        var actualAliveWorkersCount = actualAliveWorkers.Count;
        _testOutputHelper.WriteLine($"Actual workers alive: {actualAliveWorkersCount}");

        var actualGameOver = village.GameOver;
        _testOutputHelper.WriteLine($"Actual Game Over: {actualGameOver}");

        //Assert
        Assert.Equal(expectedFoodBeforeFeeding, actualFoodBeforeFeeding);
        Assert.Equal(expectedFoodAfterFeeding, actualFoodAfterFeeding);
        Assert.Equal(expectedHungryWorkers, actualHungryWorkersCount);
        Assert.Equal(expectedDaysHungry, actualDaysHungry);
        Assert.Equal(expectedAliveWorkers, actualAliveWorkersCount);
        Assert.Equal(expectedGameOver, actualGameOver);
    }
        [Fact]
    public void Feed5WorkersWith2StartFoodFor40DaysGives3HungryWorkers40DaysHungryAnd3WorkerIsAliveThusItsNotGameOver()
    {
        //Arrange
        
        // Add the village and the workers.
        Village village = _villageFixture.Village;
        village.AddWorker("Sven the Farmer", () => village.AddFood(5));
        village.AddWorker("Bob the Quarry Man", () => village.AddMetal(1));
        village.AddWorker("Olof the Lumberjack", () => village.AddWood(1));
        village.AddWorker("Olof II the Lumberjack", () => village.AddWood(1));
        village.AddWorker("Daisy the Builder", () => village.AddProject(Building.Type.House));
        
        // Deplete the village of all food before the test begins.
        village.AddFood(1);
        village.AddFood(1);
        village.FeedWorkers(1);
        village.FeedWorkers(1);
        
        
        var expectedFoodBeforeFeeding = 2;
        _testOutputHelper.WriteLine($"Expected food before feeding: {expectedFoodBeforeFeeding}");
        var expectedFoodAfterFeeding = 0;
        _testOutputHelper.WriteLine($"Expected food after feeding: {expectedFoodAfterFeeding}");
        var expectedHungryWorkers = 3;
        _testOutputHelper.WriteLine($"Expected hungry workers: {expectedHungryWorkers}");
        var expectedDaysHungry = 39;
        _testOutputHelper.WriteLine($"Expected days hungry: {expectedDaysHungry}");
        var expectedAliveWorkers = 3;
        _testOutputHelper.WriteLine($"Expected workers alive: {expectedAliveWorkers}");
        var expectedGameOver = false;
        _testOutputHelper.WriteLine($"Expected Game Over: {expectedGameOver}");

        _testOutputHelper.WriteLine("");
        
        //Act
        var actualFoodBeforeFeeding = village.GetFood();
        _testOutputHelper.WriteLine($"Actual food before feeding: {actualFoodBeforeFeeding}");

        // 40 days passing by.
        for (int i = 0; i < 40; i++)
        {
            village.Day();
        }
        
        var actualFoodAfterFeeding = village.GetFood();
        _testOutputHelper.WriteLine($"Actual food after feeding: {actualFoodAfterFeeding}");
        
        var actualDaysHungry = 0;
        List<Worker> actualHungryWorkers = new List<Worker>();
        List<Worker> actualAliveWorkers = new List<Worker>();
        
        foreach (var worker in village.GetWorkers())
        {
            actualDaysHungry += worker.DaysHungry;
        }
        _testOutputHelper.WriteLine($"Actual days hungry: {actualDaysHungry}");
        
        foreach (var worker in village.GetWorkers())
        {
            if (worker.Alive == true)
            {
                actualAliveWorkers.Add(worker);
            }
        }

        foreach (var worker in village.GetWorkers())
        {
            if (worker.Hungry == true)
            {
                actualHungryWorkers.Add(worker);
            }
        }

        var actualHungryWorkersCount = actualHungryWorkers.Count;
        _testOutputHelper.WriteLine($"Actual hungry workers: {actualHungryWorkersCount}");
        
        var actualAliveWorkersCount = actualAliveWorkers.Count;
        _testOutputHelper.WriteLine($"Actual workers alive: {actualAliveWorkersCount}");

        var actualGameOver = village.GameOver;
        _testOutputHelper.WriteLine($"Actual Game Over: {actualGameOver}");

        //Assert
        Assert.Equal(expectedFoodBeforeFeeding, actualFoodBeforeFeeding);
        Assert.Equal(expectedFoodAfterFeeding, actualFoodAfterFeeding);
        Assert.Equal(expectedHungryWorkers, actualHungryWorkersCount);
        Assert.Equal(expectedDaysHungry, actualDaysHungry);
        Assert.Equal(expectedAliveWorkers, actualAliveWorkersCount);
        Assert.Equal(expectedGameOver, actualGameOver);
    }
    
    [Fact]
    public void VillageStartsWith0DaysGone()
    {
        //Arrange
        Village village = _villageFixture.Village;
        var expectedDaysGone = 0;
        
        //Act
        var actualDaysGone = village.GetDaysGone();
        
        //Assert
        Assert.Equal(expectedDaysGone, actualDaysGone);
    }

    [Fact]
    public void VillageStartsWith0FoodPerDay()
    {
        //Arrange
        Village village = _villageFixture.Village;
        var expectedFoodPerDay = 0;
        
        //Act
        var actualFoodPerDay = village.GetFoodPerDay();
        
        //Assert
        Assert.Equal(expectedFoodPerDay, actualFoodPerDay);
    }

    [Fact]
    public void VillageStartsWith0Metal()
    {
        //Arrange
        Village village = _villageFixture.Village;
        var expectedMetal = 0;
        
        //Act
        var actualMetal = village.GetMetal();
        
        //Assert
        Assert.Equal(expectedMetal, actualMetal);
    }

    [Fact]
    public void VillageStartsWith0MetalPerDay()
    {
        //Arrange
        Village village = _villageFixture.Village;
        var expectedMetalPerDay = 0;
        
        //Act
        var actualMetalPerDay = village.GetMetalPerDay();
        
        //Assert
        Assert.Equal(expectedMetalPerDay, actualMetalPerDay);
    }

    [Fact]
    public void VillageStartsWith0Projects()
    {
        //Arrange
        Village village = _villageFixture.Village;
        var expectedProjects = 0;
        
        //Act
        var actualProjects = village.GetProjects().Count;
        
        //Assert
        Assert.Equal(expectedProjects, actualProjects);
    }

    [Fact]
    public void VillageStartsWith0Wood()
    {
        //Arrange
        Village village = _villageFixture.Village;
        var expectedWood = 0;
        
        //Act
        var actualWood = village.GetWood();
        
        //Assert
        Assert.Equal(expectedWood, actualWood);
    }

    [Fact]
    public void VillageStartsWith0WoodPerDay()
    {
        //Arrange
        Village village = _villageFixture.Village;
        var expectedWoodPerDay = 0;
        
        //Act
        var actualWoodPerDay = village.GetWoodPerDay();
        
        //Assert
        Assert.Equal(expectedWoodPerDay, actualWoodPerDay);
    }

    [Fact]
    public void VillageStartsWith0Workers()
    {
        //Arrange
        Village village = _villageFixture.Village;
        var expectedWorkers = 0;
        
        //Act
        var actualWorkers = village.GetWorkers().Count;
        
        //Assert
        Assert.Equal(expectedWorkers, actualWorkers);
    }
}