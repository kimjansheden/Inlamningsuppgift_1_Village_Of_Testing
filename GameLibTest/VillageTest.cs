using GameLib;
using Xunit.Abstractions;
using static GameLib.Strings.Message;

namespace GameLibTest;

public class VillageTest : IClassFixture<VillageFixture>
{
    private VillageFixture _villageFixture;
    private ITestOutputHelper _testOutputHelper;
    private IUI _ui;

    public VillageTest(VillageFixture villageFixture, ITestOutputHelper testOutputHelper)
    {
        _villageFixture = villageFixture;
        _testOutputHelper = testOutputHelper;
        _ui = new ConsoleUI();
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

    [Fact]
    public void Day_Without_Workers_Does_Not_Change_Village_Except_For_Days_Gone()
    {
        //Arrange
        Village village = _villageFixture.Village;
        var expectedDaysGone = 1;
        var expectedBuildings = 3;
        var expectedFood = 10;
        var expectedFoodPerDay = 0;
        var expectedMetal = 0;
        var expectedMetalPerDay = 0;
        var expectedProjects = 0;
        var expectedWood = 0;
        var expectedWoodPerDay = 0;
        var expectedWorkers = 0;
        var expectedGameOver = false;
        var expectedGameWon = false;
        var expectedMaxWorkers = 6;

        // Act
        village.Day();

        var actualDaysGone = village.GetDaysGone();
        var actualBuildings = village.GetBuildings().Count;
        var actualFood = village.GetFood();
        var actualFoodPerDay = village.GetFoodPerDay();
        var actualMetal = village.GetMetal();
        var actualMetalPerDay = village.GetMetalPerDay();
        var actualProjects = village.GetProjects().Count;
        var actualWood = village.GetWood();
        var actualWoodPerDay = village.GetWoodPerDay();
        var actualWorkers = village.GetWorkers().Count;
        var actualGameOver = village.GameOver;
        var actualGameWon = village.GameWon;
        var actualMaxWorkers = village.GetMaxWorkers();

        // Assert
        Assert.Equal(expectedDaysGone, actualDaysGone);
        Assert.Equal(expectedBuildings, actualBuildings);
        Assert.Equal(expectedFood, actualFood);
        Assert.Equal(expectedFoodPerDay, actualFoodPerDay);
        Assert.Equal(expectedMetal, actualMetal);
        Assert.Equal(expectedMetalPerDay, actualMetalPerDay);
        Assert.Equal(expectedProjects, actualProjects);
        Assert.Equal(expectedWood, actualWood);
        Assert.Equal(expectedWoodPerDay, actualWoodPerDay);
        Assert.Equal(expectedWorkers, actualWorkers);
        Assert.Equal(expectedGameOver, actualGameOver);
        Assert.Equal(expectedGameWon, actualGameWon);
        Assert.Equal(expectedMaxWorkers, actualMaxWorkers);
    }
    
    [Fact]
    public void Day_Village_Has_4_Workers_And_10_Food_Consumes_4_Food_Gives_No_Hungry_Workers_No_Days_Hungry_And_All_Workers_Are_Alive()
    {
        //Arrange
        Village village = _villageFixture.Village;
        village.AddWorker("Sven the Lumberjack", Worker.Type.Lumberjack, () => village.AddWood());
        village.AddWorker("Bob the Quarry Man", Worker.Type.QuarryWorker, () => village.AddMetal());
        village.AddWorker("Olof the Lumberjack", Worker.Type.Lumberjack, () => village.AddWood());
        village.AddWorker("Daisy the Builder", Worker.Type.Builder, () => village.AddProject(Building.Type.House));

        var expectedFoodBeforeDay = 10;
        _testOutputHelper.WriteLine($"Expected food before a day: {expectedFoodBeforeDay}");
        var expectedDaysGone = 1;
        _testOutputHelper.WriteLine($"Expected days gone: {expectedDaysGone}");
        var expectedFoodAfterDay = expectedFoodBeforeDay - village.GetWorkers().Count;
        _testOutputHelper.WriteLine($"Expected food after a day: {expectedFoodAfterDay}");
        var expectedHungryWorkers = 0;
        _testOutputHelper.WriteLine($"Expected hungry workers: {expectedHungryWorkers}");
        var expectedDaysHungry = 0;
        _testOutputHelper.WriteLine($"Expected days hungry: {expectedDaysHungry}");
        var expectedAliveWorkers = village.GetWorkers().Count;
        _testOutputHelper.WriteLine($"Expected workers alive: {expectedAliveWorkers}");

        _testOutputHelper.WriteLine("");
        
        //Act
        var actualFoodBeforeDay = village.GetFood();
        _testOutputHelper.WriteLine($"Actual food before a day: {actualFoodBeforeDay}");
        
        village.Day();

        var actualDaysGone = village.GetDaysGone();
        _testOutputHelper.WriteLine($"Actual days gone: {actualDaysGone}");

        var actualFoodAfterDay = village.GetFood();
        _testOutputHelper.WriteLine($"Actual food after a day: {actualFoodAfterDay}");
        
        var actualDaysHungry = 0;
        List<Worker> actualHungryWorkers = new List<Worker>();
        List<Worker> actualAliveWorkers = new List<Worker>();
        
        foreach (var worker in village.GetWorkers())
        {
            if (worker.Hungry)
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

        foreach (var worker in village.GetWorkers())
        {
            if (worker.Alive)
            {
                actualAliveWorkers.Add(worker);
            }
        }

        var actualAliveWorkersCount = actualAliveWorkers.Count;
        _testOutputHelper.WriteLine($"Actual workers alive: {actualAliveWorkersCount}");

        //Assert
        Assert.Equal(expectedFoodBeforeDay, actualFoodBeforeDay);
        Assert.Equal(expectedFoodAfterDay, actualFoodAfterDay);
        Assert.Equal(expectedHungryWorkers, actualHungryWorkersCount);
        Assert.Equal(expectedDaysHungry, actualDaysHungry);
        Assert.Equal(expectedAliveWorkers, actualAliveWorkersCount);
    }
    
    [Fact]
    public void Feed_4_Workers_With_10_Food_Consumes_4_Food_Gives_No_Hungry_Workers_No_Days_Hungry_And_All_Workers_Are_Alive()
    {
        //Arrange
        Village village = _villageFixture.Village;
        village.AddWorker("Sven the Farmer", Worker.Type.Farmer, () => village.AddFood());
        village.AddWorker("Bob the Quarry Man", Worker.Type.QuarryWorker, () => village.AddMetal());
        village.AddWorker("Olof the Lumberjack", Worker.Type.Lumberjack, () => village.AddWood());
        village.AddWorker("Daisy the Builder", Worker.Type.Builder, () => village.AddProject(Building.Type.House));
        
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
            if (worker.Alive)
            {
                actualAliveWorkers.Add(worker);
            }
        }

        foreach (var worker in village.GetWorkers())
        {
            if (worker.Hungry)
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
        village.AddWorker("Sven the Farmer", Worker.Type.Farmer, () => village.AddFood());
        village.AddWorker("Bob the Quarry Man", Worker.Type.QuarryWorker, () => village.AddMetal());
        village.AddWorker("Olof the Lumberjack", Worker.Type.Lumberjack, () => village.AddWood());
        village.AddWorker("Daisy the Builder", Worker.Type.Builder, () => village.AddProject(Building.Type.House));
        
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
            if (worker.Alive)
            {
                actualAliveWorkers.Add(worker);
            }
        }

        foreach (var worker in village.GetWorkers())
        {
            if (worker.Hungry)
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
        village.AddWorker("Sven the Lumberjack", Worker.Type.Lumberjack, () => village.AddWood());
        village.AddWorker("Bob the Quarry Man", Worker.Type.QuarryWorker, () => village.AddMetal());
        village.AddWorker("Olof the Lumberjack", Worker.Type.Lumberjack, () => village.AddWood());
        village.AddWorker("Olof II the Lumberjack", Worker.Type.Lumberjack, () => village.AddWood());
        village.AddWorker("Daisy the Builder", Worker.Type.Builder, () => village.AddProject(Building.Type.House));
        
        // Deplete the village of all food before the test begins.
        village.FeedWorkers(1);
        village.FeedWorkers(1);
        
        
        var expectedFoodBeforeFeeding = 0;
        _testOutputHelper.WriteLine($"Expected food before feeding: {expectedFoodBeforeFeeding}");
        var expectedFoodAfterFeeding = 0;
        _testOutputHelper.WriteLine($"Expected food after feeding: {expectedFoodAfterFeeding}");
        var expectedDaysGone = 40;
        _testOutputHelper.WriteLine($"Expected days passed by: {expectedDaysGone}");
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
        for (int i = 0; i < expectedDaysGone; i++)
        {
            village.Day();
        }
        
        var actualFoodAfterFeeding = village.GetFood();
        _testOutputHelper.WriteLine($"Actual food after feeding: {actualFoodAfterFeeding}");
        
        var actualDaysGone = village.GetDaysGone();
        _testOutputHelper.WriteLine($"Actual days passed by: {actualDaysGone}");
        
        var actualDaysHungry = 0;
        List<Worker> actualHungryWorkers = new List<Worker>();
        List<Worker> actualAliveWorkers = new List<Worker>();
        
        foreach (var worker in village.GetWorkers())
        {
            if (worker.Hungry)
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

        foreach (var worker in village.GetWorkers())
        {
            if (worker.Alive)
            {
                actualAliveWorkers.Add(worker);
            }
        }

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
    public void Feed5WorkersWith0StartFoodFor39DaysGives5HungryWorkers39DaysHungry39DaysGoneAndAllWorkerIsAliveThusItsNotGameOver()
    {
        //Arrange
        
        // Add the village and the workers.
        Village village = _villageFixture.Village;
        village.AddWorker("Sven the Lumberjack", Worker.Type.Lumberjack, () => village.AddWood());
        village.AddWorker("Bob the Quarry Man", Worker.Type.QuarryWorker, () => village.AddMetal());
        village.AddWorker("Olof the Lumberjack", Worker.Type.Lumberjack, () => village.AddWood());
        village.AddWorker("Olof II the Lumberjack", Worker.Type.Lumberjack, () => village.AddWood());
        village.AddWorker("Daisy the Builder", Worker.Type.Builder, () => village.AddProject(Building.Type.House));
        
        // Deplete the village of all food but 2 before the test begins.
        village.FeedWorkers(1);
        village.FeedWorkers(1);
        
        
        var expectedFoodBeforeFeeding = 0;
        _testOutputHelper.WriteLine($"Expected food before feeding: {expectedFoodBeforeFeeding}");
        var expectedFoodAfterFeeding = 0;
        _testOutputHelper.WriteLine($"Expected food after feeding: {expectedFoodAfterFeeding}");
        var expectedHungryWorkers = 5;
        _testOutputHelper.WriteLine($"Expected hungry workers: {expectedHungryWorkers}");
        var expectedDaysGone = 39;
        _testOutputHelper.WriteLine($"Expected days passed by: {expectedDaysGone}");
        var expectedDaysHungry = expectedDaysGone * village.GetWorkers().Count; // 39 days for each worker.
        _testOutputHelper.WriteLine($"Expected days hungry: {expectedDaysHungry}");
        var expectedAliveWorkers = 5;
        _testOutputHelper.WriteLine($"Expected workers alive: {expectedAliveWorkers}");
        var expectedGameOver = false;
        _testOutputHelper.WriteLine($"Expected Game Over: {expectedGameOver}");

        _testOutputHelper.WriteLine("");
        
        //Act
        var actualFoodBeforeFeeding = village.GetFood();
        _testOutputHelper.WriteLine($"Actual food before feeding: {actualFoodBeforeFeeding}");

        // 39 days passing by.
        for (int i = 0; i < expectedDaysGone; i++)
        {
            village.Day();
        }
        
        var actualFoodAfterFeeding = village.GetFood();
        _testOutputHelper.WriteLine($"Actual food after feeding: {actualFoodAfterFeeding}");

        var actualDaysGone = village.GetDaysGone();
        _testOutputHelper.WriteLine($"Actual days passed by: {actualDaysGone}");
        
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
            if (worker.Alive)
            {
                actualAliveWorkers.Add(worker);
            }
        }

        foreach (var worker in village.GetWorkers())
        {
            if (worker.Hungry)
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
    public void Feed5WorkersWith2StartFoodFor40DaysGives2HungryWorkers39DaysHungryPerWorkerAnd2WorkerIsAliveThusItsNotGameOver()
    {
        //Arrange
        
        // Add the village and the workers.
        Village village = _villageFixture.Village;
        village.AddWorker("Sven the Lumberjack", Worker.Type.Lumberjack, () => village.AddWood());
        village.AddWorker("Bob the Quarry Man", Worker.Type.QuarryWorker, () => village.AddMetal());
        village.AddWorker("Olof the Lumberjack", Worker.Type.Lumberjack, () => village.AddWood());
        village.AddWorker("Olof II the Lumberjack", Worker.Type.Lumberjack, () => village.AddWood());
        village.AddWorker("Daisy the Builder", Worker.Type.Builder, () => village.AddProject(Building.Type.House));
        
        // Deplete the village of all food but 2 before the test begins.
        village.AddFood(1);
        village.AddFood(1);
        village.FeedWorkers(1);
        village.FeedWorkers(1);
        
        var expectedFoodBeforeFeeding = 2;
        _testOutputHelper.WriteLine($"Expected food before feeding: {expectedFoodBeforeFeeding}");
        var expectedFoodAfterFeeding = 0;
        _testOutputHelper.WriteLine($"Expected food after feeding: {expectedFoodAfterFeeding}");
        var expectedDaysGone = 40;
        _testOutputHelper.WriteLine($"Expected days passed by: {expectedDaysGone}");
        var expectedAliveWorkers = 2;
        _testOutputHelper.WriteLine($"Expected workers alive: {expectedAliveWorkers}");
        var expectedHungryWorkers = expectedAliveWorkers; // As many as expected to be alive.
        _testOutputHelper.WriteLine($"Expected hungry workers: {expectedHungryWorkers}");
        var expectedDaysHungry = (expectedDaysGone - 1) * expectedAliveWorkers; // 39 days for each worker expected to be alive.
        _testOutputHelper.WriteLine($"Expected days hungry: {expectedDaysHungry}");
        var expectedGameOver = false;
        _testOutputHelper.WriteLine($"Expected Game Over: {expectedGameOver}");

        _testOutputHelper.WriteLine("");
        
        //Act
        var actualFoodBeforeFeeding = village.GetFood();
        _testOutputHelper.WriteLine($"Actual food before feeding: {actualFoodBeforeFeeding}");

        // 40 days passing by.
        for (int i = 0; i < expectedDaysGone; i++)
        {
            village.Day();
        }
        
        var actualFoodAfterFeeding = village.GetFood();
        _testOutputHelper.WriteLine($"Actual food after feeding: {actualFoodAfterFeeding}");
        
        var actualDaysGone = village.GetDaysGone();
        _testOutputHelper.WriteLine($"Actual days passed by: {actualDaysGone}");
        
        List<Worker> actualAliveWorkers = new List<Worker>();
        foreach (var worker in village.GetWorkers())
        {
            if (worker.Alive)
            {
                actualAliveWorkers.Add(worker);
            }
        }
        
        var actualAliveWorkersCount = actualAliveWorkers.Count;
        _testOutputHelper.WriteLine($"Actual workers alive: {actualAliveWorkersCount}");
        
        var actualDaysHungry = 0;
        List<Worker> actualHungryWorkers = new List<Worker>();
        
        foreach (var worker in village.GetWorkers())
        {
            if (worker.Hungry)
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
    
    [Fact]
    public void BuildAWoodMillCompleteAfter5DaysIncreasesWoodPerDayBy2DecreasesMetalBy1AndWoodBy5()
    {
        //Arrange
        Village village = _villageFixture.Village;
        village.AddWorker("Leif", Worker.Type.Builder, () => village.Build());
        village.AddWorker("Tor", Worker.Type.Lumberjack, () => village.AddWood());
        
        // The village starts with the resources expected to build the building being tested.
        var expectedMetalDeducted = 1;
        var expectedWoodDeducted = 5;
        village.AddMetal(expectedMetalDeducted);
        village.AddWood(expectedWoodDeducted);
        
        village.AddProject(Building.Type.Woodmill);
        var project = village.GetProjects()[0]; 
        
        var expectedWoodPerDayBeforeWoodMill = 1;
        _testOutputHelper.WriteLine($"Expected wood per day before the woodmill is built: {expectedWoodPerDayBeforeWoodMill}");
        
        var expectedDaysPassed = 5;
        _testOutputHelper.WriteLine($"Expected days having passed (the required days needed to build the building): {expectedDaysPassed}");
        
        var expectedComplete = true;
        _testOutputHelper.WriteLine($"Expected completion status of the building after the days required to build it have passed: {expectedComplete}");
        
        var expectedProjectListCount = 0;
        _testOutputHelper.WriteLine($"Expected unfinished projects after the woodmill is built: {expectedProjectListCount}");
        
        var expectedBuildingListCount = 4; // Because the village starts with 3 houses.
        _testOutputHelper.WriteLine($"Expected number of buildings in the village after the woodmill is built: {expectedBuildingListCount}");
        
        var expectedBuildingTypeInList = Building.Type.Woodmill;
        _testOutputHelper.WriteLine($"Expected building type of the building we just built: {expectedBuildingTypeInList}");
        
        var expectedWoodPerDayAfterWoodMill = expectedWoodPerDayBeforeWoodMill + 2;
        _testOutputHelper.WriteLine($"Expected wood per day after the woodmill is built: {expectedWoodPerDayAfterWoodMill}");
        
        var expectedMetalAfterComplete = 0;
        _testOutputHelper.WriteLine($"Expected metal in the village after the building is built: {expectedMetalAfterComplete}");
        
        // The builder is able to finish the woodmill before it's the lumberjack's turn to work, so on the fifth day, when the woodmill is complete, the lumberjack is able to harness the woodmill and bring in 3 wood instead of 1 like the other days.
        var expectedWoodAfterComplete = (expectedWoodPerDayBeforeWoodMill * expectedDaysPassed - 1) + expectedWoodPerDayAfterWoodMill;
        _testOutputHelper.WriteLine($"Expected wood in the village after the building is built: {expectedWoodAfterComplete}");
        
        _testOutputHelper.WriteLine("");

        //Act
        var actualWoodPerDayBeforeWoodMill = village.GetWoodPerDay();
        _testOutputHelper.WriteLine($"Actual wood per day before the woodmill is built: {actualWoodPerDayBeforeWoodMill}");

        for (int i = 0; i < expectedDaysPassed; i++) village.Day();

        var actualDaysPassed = village.GetDaysGone();
        _testOutputHelper.WriteLine($"Actual days having passed (the required days needed to build the building): {actualDaysPassed}");
        
        var actualComplete = project.IsComplete;
        _testOutputHelper.WriteLine($"Actual completion status of the building after the days required to build it have passed: {actualComplete}");
        
        var actualProjectListCount = village.GetProjects().Count;
        _testOutputHelper.WriteLine($"Actual unfinished projects after the woodmill is built: {actualProjectListCount}");
        
        var actualBuildingListCount = village.GetBuildings().Count;
        _testOutputHelper.WriteLine($"Actual number of buildings in the village after the woodmill is built: {actualBuildingListCount}");
        
        var actualBuildingTypeInList = village.GetBuildings()[village.GetBuildings().Count - 1].BuildingType; // The new building will end up as the last element of the list.
        _testOutputHelper.WriteLine($"Actual building type of the building we just built: {actualBuildingTypeInList}");
        
        var actualWoodPerDayAfterWoodMill = village.GetWoodPerDay();
        _testOutputHelper.WriteLine($"Actual wood per day after the woodmill is built: {actualWoodPerDayAfterWoodMill}");
        
        var actualMetalAfterComplete = village.GetMetal();
        _testOutputHelper.WriteLine($"Actual metal in the village after the building is built: {actualMetalAfterComplete}");
        
        var actualWoodAfterComplete = village.GetWood();
        _testOutputHelper.WriteLine($"Actual wood in the village after the building is built: {actualWoodAfterComplete}");
        
        //Assert
        Assert.Equal(expectedWoodPerDayBeforeWoodMill, actualWoodPerDayBeforeWoodMill);
        Assert.Equal(expectedDaysPassed, actualDaysPassed);
        Assert.Equal(expectedComplete, actualComplete);
        Assert.Equal(expectedProjectListCount, actualProjectListCount);
        Assert.Equal(expectedProjectListCount, actualProjectListCount);
        Assert.Equal(expectedBuildingListCount, actualBuildingListCount);
        Assert.Equal(expectedBuildingTypeInList, actualBuildingTypeInList);
        Assert.Equal(expectedWoodPerDayAfterWoodMill, actualWoodPerDayAfterWoodMill);
        Assert.Equal(expectedMetalAfterComplete, actualMetalAfterComplete);
        Assert.Equal(expectedWoodAfterComplete, actualWoodAfterComplete);
    }
    [Fact]
    public void Build_A_House_With_1_Builder_Complete_After_3_Days_Decreases_Wood_By_5()
    {
        //Arrange
        Village village = _villageFixture.Village;
        village.AddWorker("Leif", Worker.Type.Builder, () => village.Build());
        
        // The village starts with the resources expected to build the building being tested.
        var expectedWoodDeducted = 5;
        village.AddWood(expectedWoodDeducted);
        
        village.AddProject(Building.Type.House);
        var project = village.GetProjects()[0]; 
        
        var expectedDaysPassed = 3;
        _testOutputHelper.WriteLine($"Expected days having passed (the required days needed to build the building): {expectedDaysPassed}");
        
        var expectedComplete = true;
        _testOutputHelper.WriteLine($"Expected completion status of the building after the days required to build it have passed: {expectedComplete}");
        
        var expectedProjectListCount = 0;
        _testOutputHelper.WriteLine($"Expected unfinished projects after the woodmill is built: {expectedProjectListCount}");
        
        var expectedBuildingListCount = 4; // Because the village starts with 3 houses.
        _testOutputHelper.WriteLine($"Expected number of buildings in the village after the house is built: {expectedBuildingListCount}");
        
        var expectedBuildingTypeInList = Building.Type.House;
        _testOutputHelper.WriteLine($"Expected building type of the building we just built: {expectedBuildingTypeInList}");
        
        var expectedWoodAfterComplete = 0;
        _testOutputHelper.WriteLine($"Expected wood in the village after the building is built: {expectedWoodAfterComplete}");
        
        _testOutputHelper.WriteLine("");

        //Act
        for (int i = 0; i < expectedDaysPassed; i++)
        {
            village.Day();
        }
        
        var actualDaysPassed = village.GetDaysGone();
        _testOutputHelper.WriteLine($"Actual days having passed (the required days needed to build the building): {actualDaysPassed}");
        
        var actualComplete = project.IsComplete;
        _testOutputHelper.WriteLine($"Actual completion status of the building after the days required to build it have passed: {actualComplete}");
        
        var actualProjectListCount = village.GetProjects().Count;
        _testOutputHelper.WriteLine($"Actual unfinished projects after the woodmill is built: {actualProjectListCount}");
        
        var actualBuildingListCount = village.GetBuildings().Count;
        _testOutputHelper.WriteLine($"Actual number of buildings in the village after the house is built: {actualBuildingListCount}");
        
        var actualBuildingTypeInList = village.GetBuildings()[village.GetBuildings().Count - 1].BuildingType; // The new building will end up as the last element of the list.
        _testOutputHelper.WriteLine($"Actual building type of the building we just built: {actualBuildingTypeInList}");
        
        var actualWoodAfterComplete = village.GetWood();
        _testOutputHelper.WriteLine($"Actual wood in the village after the building is built: {actualWoodAfterComplete}");
        
        //Assert
        Assert.Equal(expectedDaysPassed, actualDaysPassed);
        Assert.Equal(expectedComplete, actualComplete);
        Assert.Equal(expectedProjectListCount, actualProjectListCount);
        Assert.Equal(expectedProjectListCount, actualProjectListCount);
        Assert.Equal(expectedBuildingListCount, actualBuildingListCount);
        Assert.Equal(expectedBuildingTypeInList, actualBuildingTypeInList);
        Assert.Equal(expectedWoodAfterComplete, actualWoodAfterComplete);
    }
    [Fact]
    public void Build_A_House_With_3_Builders_Complete_After_1_Day_Decreases_Wood_By_5()
    {
        //Arrange
        Village village = _villageFixture.Village;
        village.AddWorker("Leif II", Worker.Type.Builder, () => village.Build());
        village.AddWorker("Leif III", Worker.Type.Builder, () => village.Build());
        village.AddWorker("Leif", Worker.Type.Builder, () => village.Build());

        // The village starts with the resources expected to build the building being tested.
        var expectedWoodDeducted = 5;
        village.AddWood(expectedWoodDeducted);
        
        village.AddProject(Building.Type.House);
        var project = village.GetProjects()[0]; 
        
        var expectedDaysPassed = 1;
        _testOutputHelper.WriteLine($"Expected days having passed (the required days needed to build the building): {expectedDaysPassed}");
        
        var expectedComplete = true;
        _testOutputHelper.WriteLine($"Expected completion status of the building after the days required to build it have passed: {expectedComplete}");
        
        var expectedProjectListCount = 0;
        _testOutputHelper.WriteLine($"Expected unfinished projects after the woodmill is built: {expectedProjectListCount}");
        
        var expectedBuildingListCount = 4; // Because the village starts with 3 houses.
        _testOutputHelper.WriteLine($"Expected number of buildings in the village after the house is built: {expectedBuildingListCount}");
        
        var expectedBuildingTypeInList = Building.Type.House;
        _testOutputHelper.WriteLine($"Expected building type of the building we just built: {expectedBuildingTypeInList}");
        
        var expectedWoodAfterComplete = 0;
        _testOutputHelper.WriteLine($"Expected wood in the village after the building is built: {expectedWoodAfterComplete}");
        
        _testOutputHelper.WriteLine("");

        //Act
        for (int i = 0; i < expectedDaysPassed; i++)
        {
            village.Day();
        }
        
        var actualDaysPassed = village.GetDaysGone();
        _testOutputHelper.WriteLine($"Actual days having passed (the required days needed to build the building): {actualDaysPassed}");
        
        var actualComplete = project.IsComplete;
        _testOutputHelper.WriteLine($"Actual completion status of the building after the days required to build it have passed: {actualComplete}");
        
        var actualProjectListCount = village.GetProjects().Count;
        _testOutputHelper.WriteLine($"Actual unfinished projects after the woodmill is built: {actualProjectListCount}");
        
        var actualBuildingListCount = village.GetBuildings().Count;
        _testOutputHelper.WriteLine($"Actual number of buildings in the village after the house is built: {actualBuildingListCount}");
        
        var actualBuildingTypeInList = village.GetBuildings()[village.GetBuildings().Count - 1].BuildingType; // The new building will end up as the last element of the list.
        _testOutputHelper.WriteLine($"Actual building type of the building we just built: {actualBuildingTypeInList}");
        
        var actualWoodAfterComplete = village.GetWood();
        _testOutputHelper.WriteLine($"Actual wood in the village after the building is built: {actualWoodAfterComplete}");
        
        //Assert
        Assert.Equal(expectedDaysPassed, actualDaysPassed);
        Assert.Equal(expectedComplete, actualComplete);
        Assert.Equal(expectedProjectListCount, actualProjectListCount);
        Assert.Equal(expectedProjectListCount, actualProjectListCount);
        Assert.Equal(expectedBuildingListCount, actualBuildingListCount);
        Assert.Equal(expectedBuildingTypeInList, actualBuildingTypeInList);
        Assert.Equal(expectedWoodAfterComplete, actualWoodAfterComplete);
    }
    [Fact]
    public void Build_A_Quarry_Complete_After_7_Days_Increases_Metal_Per_Day_By_2_Decreases_Metal_By_5_And_Wood_By_3()
    {
        //Arrange
        Village village = _villageFixture.Village;
        village.AddWorker("Leif", Worker.Type.Builder, () => village.Build());
        village.AddWorker("Bob", Worker.Type.QuarryWorker, () => village.AddMetal());
        village.AddWorker("Sven", Worker.Type.Farmer, () => village.AddFood());
        
        // The village starts with the resources expected to build the building being tested.
        var expectedMetalDeducted = 5;
        var expectedWoodDeducted = 3;

        village.AddMetal(expectedMetalDeducted);
        village.AddWood(expectedWoodDeducted);
        village.AddProject(Building.Type.Quarry);
        var project = village.GetProjects()[0]; 
        
        var expectedMetalPerDayBeforeQuarry = 1;
        _testOutputHelper.WriteLine($"Expected metal per day before the quarry is built: {expectedMetalPerDayBeforeQuarry}");
        
        var expectedDaysPassed = 7;
        _testOutputHelper.WriteLine($"Expected days having passed (the required days needed to build the building): {expectedDaysPassed}");
        
        var expectedComplete = true;
        _testOutputHelper.WriteLine($"Expected completion status of the building after the days required to build it have passed: {expectedComplete}");
        
        var expectedProjectListCount = 0;
        _testOutputHelper.WriteLine($"Expected unfinished projects after the woodmill is built: {expectedProjectListCount}");
        
        var expectedBuildingListCount = 4; // Because the village starts with 3 houses.
        _testOutputHelper.WriteLine($"Expected number of buildings in the village after the woodmill is built: {expectedBuildingListCount}");
        
        var expectedBuildingTypeInList = Building.Type.Quarry;
        _testOutputHelper.WriteLine($"Expected building type of the building we just built: {expectedBuildingTypeInList}");
        
        var expectedMetalPerDayAfterQuarry = expectedMetalPerDayBeforeQuarry + 2;
        _testOutputHelper.WriteLine($"Expected metal per day after the quarry is built: {expectedMetalPerDayAfterQuarry}");

        var expectedMetalAfterComplete = (expectedMetalPerDayBeforeQuarry * expectedDaysPassed - 1) + expectedMetalPerDayAfterQuarry;
        _testOutputHelper.WriteLine($"Expected metal in the village after the building is built: {expectedMetalAfterComplete}");

        var expectedWoodAfterComplete = 0;
        _testOutputHelper.WriteLine($"Expected wood in the village after the building is built: {expectedWoodAfterComplete}");
        
        _testOutputHelper.WriteLine("");

        //Act
        var actualMetalPerDayBeforeQuarry = village.GetMetalPerDay();
        _testOutputHelper.WriteLine($"Actual metal per day before the quarry is built: {actualMetalPerDayBeforeQuarry}");
        
        for (int i = 0; i < expectedDaysPassed; i++)
        {
            village.Day();
        }
        
        var actualDaysPassed = village.GetDaysGone();
        _testOutputHelper.WriteLine($"Actual days having passed (the required days needed to build the building): {actualDaysPassed}");
        
        var actualComplete = project.IsComplete;
        _testOutputHelper.WriteLine($"Actual completion status of the building after the days required to build it have passed: {actualComplete}");
        
        var actualProjectListCount = village.GetProjects().Count;
        _testOutputHelper.WriteLine($"Actual unfinished projects after the woodmill is built: {actualProjectListCount}");
        
        var actualBuildingListCount = village.GetBuildings().Count;
        _testOutputHelper.WriteLine($"Actual number of buildings in the village after the building is built: {actualBuildingListCount}");
        
        var actualBuildingTypeInList = village.GetBuildings()[village.GetBuildings().Count - 1].BuildingType; // The new building will end up as the last element of the list.
        _testOutputHelper.WriteLine($"Actual building type of the building we just built: {actualBuildingTypeInList}");
        
        var actualMetalPerDayAfterQuarry = village.GetMetalPerDay();
        _testOutputHelper.WriteLine($"Actual metal per day after the quarry is built: {actualMetalPerDayAfterQuarry}");
        
        var actualMetalAfterComplete = village.GetMetal();
        _testOutputHelper.WriteLine($"Actual metal in the village after the building is built: {actualMetalAfterComplete}");
        
        var actualWoodAfterComplete = village.GetWood();
        _testOutputHelper.WriteLine($"Actual wood in the village after the building is built: {actualWoodAfterComplete}");
        
        //Assert
        Assert.Equal(expectedMetalPerDayBeforeQuarry, actualMetalPerDayBeforeQuarry);
        Assert.Equal(expectedDaysPassed, actualDaysPassed);
        Assert.Equal(expectedComplete, actualComplete);
        Assert.Equal(expectedProjectListCount, actualProjectListCount);
        Assert.Equal(expectedProjectListCount, actualProjectListCount);
        Assert.Equal(expectedBuildingListCount, actualBuildingListCount);
        Assert.Equal(expectedBuildingTypeInList, actualBuildingTypeInList);
        Assert.Equal(expectedMetalPerDayAfterQuarry, actualMetalPerDayAfterQuarry);
        Assert.Equal(expectedMetalAfterComplete, actualMetalAfterComplete);
        Assert.Equal(expectedWoodAfterComplete, actualWoodAfterComplete);
    }
    [Fact]
    public void BuildAFarmCompleteAfter5DaysIncreasesFoodPerDayBy10DecreasesMetalBy2AndWoodBy5()
    {
        //Arrange
        Village village = _villageFixture.Village;
        village.AddWorker("Leif", Worker.Type.Builder, () => village.Build());
        village.AddWorker("Sven", Worker.Type.Farmer, () => village.AddFood());
        
        // The village starts with the resources expected to build the building being tested.
        var expectedMetalDeducted = 2;
        var expectedWoodDeducted = 5;
        
        village.AddMetal(expectedMetalDeducted);
        village.AddWood(expectedWoodDeducted);
        village.AddProject(Building.Type.Farm);
        var project = village.GetProjects()[0]; 
        
        var expectedFoodPerDayBeforeFarm = 5;
        _testOutputHelper.WriteLine($"Expected food per day before the farm is built: {expectedFoodPerDayBeforeFarm}");
        
        var expectedDaysPassed = 5;
        _testOutputHelper.WriteLine($"Expected days having passed (the required days needed to build the building): {expectedDaysPassed}");
        
        var expectedComplete = true;
        _testOutputHelper.WriteLine($"Expected completion status of the building after the days required to build it have passed: {expectedComplete}");
        
        var expectedProjectListCount = 0;
        _testOutputHelper.WriteLine($"Expected unfinished projects after the woodmill is built: {expectedProjectListCount}");
        
        var expectedBuildingListCount = 4; // Because the village starts with 3 houses.
        _testOutputHelper.WriteLine($"Expected number of buildings in the village after the building is built: {expectedBuildingListCount}");
        
        var expectedBuildingTypeInList = Building.Type.Farm;
        _testOutputHelper.WriteLine($"Expected building type of the building we just built: {expectedBuildingTypeInList}");
        
        var expectedFoodPerDayAfterFarm = expectedFoodPerDayBeforeFarm + 10;
        _testOutputHelper.WriteLine($"Expected food per day after the farm is built: {expectedFoodPerDayAfterFarm}");

        var expectedMetalAfterComplete = 0;
        _testOutputHelper.WriteLine($"Expected metal in the village after the building is built: {expectedMetalAfterComplete}");

        var expectedWoodAfterComplete = 0;
        _testOutputHelper.WriteLine($"Expected wood in the village after the building is built: {expectedWoodAfterComplete}");
        
        _testOutputHelper.WriteLine("");

        //Act
        var actualFoodPerDayBeforeFarm = village.GetFoodPerDay();
        _testOutputHelper.WriteLine($"Actual food per day before the farm is built: {actualFoodPerDayBeforeFarm}");
        
        for (int i = 0; i < expectedDaysPassed; i++)
        {
            village.Day();
        }
        
        var actualDaysPassed = village.GetDaysGone();
        _testOutputHelper.WriteLine($"Actual days having passed (the required days needed to build the building): {actualDaysPassed}");
        
        var actualComplete = project.IsComplete;
        _testOutputHelper.WriteLine($"Actual completion status of the building after the days required to build it have passed: {actualComplete}");
        
        var actualProjectListCount = village.GetProjects().Count;
        _testOutputHelper.WriteLine($"Actual unfinished projects after the woodmill is built: {actualProjectListCount}");
        
        var actualBuildingListCount = village.GetBuildings().Count;
        _testOutputHelper.WriteLine($"Actual number of buildings in the village after the building is built: {actualBuildingListCount}");
        
        var actualBuildingTypeInList = village.GetBuildings()[village.GetBuildings().Count - 1].BuildingType; // The new building will end up as the last element of the list.
        _testOutputHelper.WriteLine($"Actual building type of the building we just built: {actualBuildingTypeInList}");
        
        var actualFoodPerDayAfterFarm = village.GetFoodPerDay();
        _testOutputHelper.WriteLine($"Actual food per day after the farm is built: {actualFoodPerDayAfterFarm}");
        
        var actualMetalAfterComplete = village.GetMetal();
        _testOutputHelper.WriteLine($"Actual metal in the village after the building is built: {actualMetalAfterComplete}");
        
        var actualWoodAfterComplete = village.GetWood();
        _testOutputHelper.WriteLine($"Actual wood in the village after the building is built: {actualWoodAfterComplete}");
        
        //Assert
        Assert.Equal(expectedFoodPerDayBeforeFarm, actualFoodPerDayBeforeFarm);
        Assert.Equal(expectedDaysPassed, actualDaysPassed);
        Assert.Equal(expectedComplete, actualComplete);
        Assert.Equal(expectedProjectListCount, actualProjectListCount);
        Assert.Equal(expectedProjectListCount, actualProjectListCount);
        Assert.Equal(expectedBuildingListCount, actualBuildingListCount);
        Assert.Equal(expectedBuildingTypeInList, actualBuildingTypeInList);
        Assert.Equal(expectedFoodPerDayAfterFarm, actualFoodPerDayAfterFarm);
        Assert.Equal(expectedMetalAfterComplete, actualMetalAfterComplete);
        Assert.Equal(expectedWoodAfterComplete, actualWoodAfterComplete);
    }
    [Fact]
    public void BuildACastleCompleteAfter50DaysDecreasesMetalBy50AndWoodBy50()
    {
        //Arrange
        Village village = _villageFixture.Village;
        village.AddWorker("Leif", Worker.Type.Builder, () => village.Build());
        
        // The village starts with the resources expected to build the building being tested.
        var expectedMetalDeducted = 50;
        var expectedWoodDeducted = 50;
        var expectedFoodEaten = 50;
        
        village.AddMetal(expectedMetalDeducted);
        village.AddWood(expectedWoodDeducted);
        village.AddFood(expectedFoodEaten);
        village.AddProject(Building.Type.Castle);
        var project = village.GetProjects()[0];

        var expectedDaysPassed = 50;
        _testOutputHelper.WriteLine($"Expected days having passed (the required days needed to build the building): {expectedDaysPassed}");
        
        var expectedComplete = true;
        _testOutputHelper.WriteLine($"Expected completion status of the building after the days required to build it have passed: {expectedComplete}");
        
        var expectedProjectListCount = 0;
        _testOutputHelper.WriteLine($"Expected unfinished projects after the building is built: {expectedProjectListCount}");
        
        var expectedBuildingListCount = 4; // Because the village starts with 3 houses.
        _testOutputHelper.WriteLine($"Expected number of buildings in the village after the building is built: {expectedBuildingListCount}");
        
        var expectedBuildingTypeInList = Building.Type.Castle;
        _testOutputHelper.WriteLine($"Expected building type of the building we just built: {expectedBuildingTypeInList}");

        var expectedMetalAfterComplete = 0;
        _testOutputHelper.WriteLine($"Expected metal in the village after the building is built: {expectedMetalAfterComplete}");

        var expectedWoodAfterComplete = 0;
        _testOutputHelper.WriteLine($"Expected wood in the village after the building is built: {expectedWoodAfterComplete}");
        
        _testOutputHelper.WriteLine("");

        //Act

        for (int i = 0; i < expectedDaysPassed; i++)
        {
            village.Day();
        }
        
        var actualDaysPassed = village.GetDaysGone();
        _testOutputHelper.WriteLine($"Actual days having passed (the required days needed to build the building): {actualDaysPassed}");
        
        var actualComplete = project.IsComplete;
        _testOutputHelper.WriteLine($"Actual completion status of the building after the days required to build it have passed: {actualComplete}");
        
        var actualProjectListCount = village.GetProjects().Count;
        _testOutputHelper.WriteLine($"Actual unfinished projects after the building is built: {actualProjectListCount}");
        
        var actualBuildingListCount = village.GetBuildings().Count;
        _testOutputHelper.WriteLine($"Actual number of buildings in the village after the building is built: {actualBuildingListCount}");
        
        var actualBuildingTypeInList = village.GetBuildings()[village.GetBuildings().Count - 1].BuildingType; // The new building will end up as the last element of the list.
        _testOutputHelper.WriteLine($"Actual building type of the building we just built: {actualBuildingTypeInList}");

        var actualMetalAfterComplete = village.GetMetal();
        _testOutputHelper.WriteLine($"Actual metal in the village after the building is built: {actualMetalAfterComplete}");
        
        var actualWoodAfterComplete = village.GetWood();
        _testOutputHelper.WriteLine($"Actual wood in the village after the building is built: {actualWoodAfterComplete}");
        
        //Assert
        Assert.Equal(expectedDaysPassed, actualDaysPassed);
        Assert.Equal(expectedComplete, actualComplete);
        Assert.Equal(expectedProjectListCount, actualProjectListCount);
        Assert.Equal(expectedProjectListCount, actualProjectListCount);
        Assert.Equal(expectedBuildingListCount, actualBuildingListCount);
        Assert.Equal(expectedBuildingTypeInList, actualBuildingTypeInList);
        Assert.Equal(expectedMetalAfterComplete, actualMetalAfterComplete);
        Assert.Equal(expectedWoodAfterComplete, actualWoodAfterComplete);
    }
    [Fact]
    public void BuildACastleWithTwoBuildersCompleteAfter25Days()
    {
        //Arrange
        Village village = _villageFixture.Village;
        village.AddWorker("Leif", Worker.Type.Builder, () => village.Build());
        village.AddWorker("Bob", Worker.Type.Builder, () => village.Build());
        
        // The village starts with the resources expected to build the building being tested.
        var expectedMetalDeducted = 50;
        var expectedWoodDeducted = 50;
        var expectedFoodEaten = 50;
        
        village.AddMetal(expectedMetalDeducted);
        village.AddWood(expectedWoodDeducted);
        village.AddFood(expectedFoodEaten);
        village.AddProject(Building.Type.Castle);
        var project = village.GetProjects()[0];

        var expectedDaysPassed = 25;
        _testOutputHelper.WriteLine($"Expected days having passed (the required days needed to build the building): {expectedDaysPassed}");
        
        var expectedComplete = true;
        _testOutputHelper.WriteLine($"Expected completion status of the building after the days required to build it have passed: {expectedComplete}");
        
        var expectedProjectListCount = 0;
        _testOutputHelper.WriteLine($"Expected unfinished projects after the building is built: {expectedProjectListCount}");
        
        var expectedBuildingListCount = 4; // Because the village starts with 3 houses.
        _testOutputHelper.WriteLine($"Expected number of buildings in the village after the building is built: {expectedBuildingListCount}");
        
        var expectedBuildingTypeInList = Building.Type.Castle;
        _testOutputHelper.WriteLine($"Expected building type of the building we just built: {expectedBuildingTypeInList}");

        var expectedMetalAfterComplete = 0;
        _testOutputHelper.WriteLine($"Expected metal in the village after the building is built: {expectedMetalAfterComplete}");

        var expectedWoodAfterComplete = 0;
        _testOutputHelper.WriteLine($"Expected wood in the village after the building is built: {expectedWoodAfterComplete}");
        
        _testOutputHelper.WriteLine("");

        //Act

        for (int i = 0; i < expectedDaysPassed; i++)
        {
            village.Day();
        }
        
        var actualDaysPassed = village.GetDaysGone();
        _testOutputHelper.WriteLine($"Actual days having passed (the required days needed to build the building): {actualDaysPassed}");
        
        var actualComplete = project.IsComplete;
        _testOutputHelper.WriteLine($"Actual completion status of the building after the days required to build it have passed: {actualComplete}");
        
        var actualProjectListCount = village.GetProjects().Count;
        _testOutputHelper.WriteLine($"Actual unfinished projects after the building is built: {actualProjectListCount}");
        
        var actualBuildingListCount = village.GetBuildings().Count;
        _testOutputHelper.WriteLine($"Actual number of buildings in the village after the building is built: {actualBuildingListCount}");
        
        var actualBuildingTypeInList = village.GetBuildings()[village.GetBuildings().Count - 1].BuildingType; // The new building will end up as the last element of the list.
        _testOutputHelper.WriteLine($"Actual building type of the building we just built: {actualBuildingTypeInList}");

        var actualMetalAfterComplete = village.GetMetal();
        _testOutputHelper.WriteLine($"Actual metal in the village after the building is built: {actualMetalAfterComplete}");
        
        var actualWoodAfterComplete = village.GetWood();
        _testOutputHelper.WriteLine($"Actual wood in the village after the building is built: {actualWoodAfterComplete}");
        
        //Assert
        Assert.Equal(expectedDaysPassed, actualDaysPassed);
        Assert.Equal(expectedComplete, actualComplete);
        Assert.Equal(expectedProjectListCount, actualProjectListCount);
        Assert.Equal(expectedProjectListCount, actualProjectListCount);
        Assert.Equal(expectedBuildingListCount, actualBuildingListCount);
        Assert.Equal(expectedBuildingTypeInList, actualBuildingTypeInList);
        Assert.Equal(expectedMetalAfterComplete, actualMetalAfterComplete);
        Assert.Equal(expectedWoodAfterComplete, actualWoodAfterComplete);
    }
    [Fact]
    public void BuildACastleMessageIsDisplayedGameWonIsTrue()
    {
        //Arrange
        // The village starts with the resources expected to build the building being tested.
        Village village = new Village(new ConsoleUI(), startFood: 50, startWood: 50, startMetal: 50, databaseConnection: new DatabaseConnection());
        Strings strings = new Strings(_ui);
        var writer = new StringWriter();
        Console.SetOut(writer);

        village.AddWorker("Leif", Worker.Type.Builder, () => village.Build());

        village.AddProject(Building.Type.Castle);

        var expectedDaysPassed = 50;
        
        var expectedMessage = $"The castle is complete! You won! You took {expectedDaysPassed} days.";
        _testOutputHelper.WriteLine($"Expected message when the Castle is built: {expectedMessage}");

        var expectedGameWon = true;
        _testOutputHelper.WriteLine($"Expected GameWon status: {expectedGameWon}");
       
        _testOutputHelper.WriteLine("");

        //Act

        for (int i = 0; i < expectedDaysPassed; i++)
        {
            village.Day();
        }
        var actualMessage = writer.ToString().Trim();
        _testOutputHelper.WriteLine($"Actual message when the Castle is built: {actualMessage}");
        
        var actualGameWon = village.GameWon;
        _testOutputHelper.WriteLine($"Actual GameWon status: {actualGameWon}");

        //Assert
        Assert.Equal(expectedMessage, actualMessage);
        Assert.Equal(expectedGameWon, actualGameWon);

    }
    [Fact]
    public void TryingToBuildAnyBuildingWithInsufficientResourcesWillResultInErrorMessageAndFalseAndNoNewBuildingOrProject()
    {
        //Arrange
        Village village = _villageFixture.Village;
        village.AddWorker("Leif", Worker.Type.Builder, () => village.Build());

        var boolList = new List<bool>();
        
        Strings strings = new Strings(_ui);
        var writer = new StringWriter();
        Console.SetOut(writer);
        var expectedMessage = strings.Messages[BuildNotEnoughResources];
        _testOutputHelper.WriteLine($"Expected error message when we fail to add a project to the list of unfinished projects: {expectedMessage}");
        
        // The village starts without the resources expected to be needed to build any building.
        
        // Try to add the projects.

        boolList.Add(village.AddProject(Building.Type.House));
        var actualMessageHouse = writer.ToString().Trim();
        writer.GetStringBuilder().Clear();

        boolList.Add(village.AddProject(Building.Type.Farm));
        var actualMessageFarm = writer.ToString().Trim();
        writer.GetStringBuilder().Clear();

        boolList.Add(village.AddProject(Building.Type.Castle));
        var actualMessageCastle = writer.ToString().Trim();
        writer.GetStringBuilder().Clear();

        boolList.Add(village.AddProject(Building.Type.Woodmill));
        var actualMessageWoodmill = writer.ToString().Trim();
        writer.GetStringBuilder().Clear();

        boolList.Add(village.AddProject(Building.Type.Quarry));
        var actualMessageQuarry = writer.ToString().Trim();
        writer.GetStringBuilder().Clear();

        var expectedNumberOfFalseInBoolList = 5;
        _testOutputHelper.WriteLine($"Expected number of false should be one for each building type that failed to add to the project list, so: {expectedNumberOfFalseInBoolList}");
        
        var expectedProjectListCount = 0;
        _testOutputHelper.WriteLine($"Expected unfinished projects: {expectedProjectListCount}");
        
        var expectedBuildingListCount = 3; // Because the village starts with 3 houses.
        _testOutputHelper.WriteLine($"Expected number of buildings in the village after the house is built: {expectedBuildingListCount}");
        
        _testOutputHelper.WriteLine("");

        //Act
        
        _testOutputHelper.WriteLine($"Actual error message when we fail to add a house to the list of unfinished projects: {actualMessageHouse}");
        _testOutputHelper.WriteLine($"Actual error message when we fail to add a farm to the list of unfinished projects: {actualMessageFarm}");
        _testOutputHelper.WriteLine($"Actual error message when we fail to add a castle to the list of unfinished projects: {actualMessageCastle}");
        _testOutputHelper.WriteLine($"Actual error message when we fail to add a woodmill to the list of unfinished projects: {actualMessageWoodmill}");
        _testOutputHelper.WriteLine($"Actual error message when we fail to add a quarry to the list of unfinished projects: {actualMessageQuarry}");

        var actualNumberOfFalseInBoolList = boolList.Count(b => !b);
        _testOutputHelper.WriteLine($"Actual number of false after we tried to add one of each building type to the project list: {actualNumberOfFalseInBoolList}");
        
        var actualProjectListCount = village.GetProjects().Count;
        _testOutputHelper.WriteLine($"Actual unfinished projects: {actualProjectListCount}");
        
        var actualBuildingListCount = village.GetBuildings().Count;
        _testOutputHelper.WriteLine($"Actual number of buildings in the village: {actualBuildingListCount}");

        //Assert
        Assert.Equal(expectedProjectListCount, actualProjectListCount);
        Assert.Equal(expectedBuildingListCount, actualBuildingListCount);
        Assert.Equal(expectedNumberOfFalseInBoolList, actualNumberOfFalseInBoolList);
        Assert.Equal(expectedMessage, actualMessageHouse);
        Assert.Equal(expectedMessage, actualMessageFarm);
        Assert.Equal(expectedMessage, actualMessageCastle);
        Assert.Equal(expectedMessage, actualMessageQuarry);
        Assert.Equal(expectedMessage, actualMessageWoodmill);
    }
    [Fact]
    public void A_Village_With_18_Wood_3_Lumberjacks_And_1_Woodmill_Should_Have_9_Wood_Per_Day_And_The_Next_Day_23_Wood()
    {
        //Arrange
        Village village = _villageFixture.Village;
        
        // Setting up the starting conditions.
        village.AddWorker("Leif", Worker.Type.Lumberjack, () => village.AddWood());
        village.AddWorker("Leif II", Worker.Type.Lumberjack, () => village.AddWood());
        village.AddWorker("Leif III", Worker.Type.Lumberjack, () => village.AddWood());
        village.AddWorker("Leif IV", Worker.Type.Farmer, () => village.AddFood());
        village.AddWorker("Leif V", Worker.Type.Builder, () => village.Build());
        
        village.BuildingCompleted(new Building(Building.Type.Woodmill, village));
        // The woodmill comes for free in this test, so we must give the resources back that it cost.
        village.AddWood(5);
        village.AddMetal(1);
        
        // Now we add the condition we want to test.
        village.AddWood(18);

        var expectedWoodBeforeTheNextDay = 18; 
        var expectedWoodPerDayBeforeTheNextDay = 9; 
        var expectedWoodmills = 1;
        
        var expectedWoodAfterTheNextDay = expectedWoodBeforeTheNextDay + expectedWoodPerDayBeforeTheNextDay;

        //Act
        var actualWoodBeforeTheNextDay = village.GetWood(); 
        var actualWoodPerDayBeforeTheNextDay = village.GetWoodPerDay(); 
        var actualWoodmills = village.GetBuildings().Count(b => b.BuildingType == Building.Type.Woodmill);
        
        village.Day();
        
        var actualWoodAfterTheNextDay = village.GetWood();

        //Assert
        Assert.Equal(expectedWoodBeforeTheNextDay, actualWoodBeforeTheNextDay);
        Assert.Equal(expectedWoodPerDayBeforeTheNextDay, actualWoodPerDayBeforeTheNextDay);
        Assert.Equal(expectedWoodmills, actualWoodmills);
        Assert.Equal(expectedWoodAfterTheNextDay, actualWoodAfterTheNextDay);
    }
    [Fact]
    public void A_Village_With_3_Lumberjacks_And_1_Woodmill_That_Adds_a_New_Lumberjack_Should_Have_9_Wood_Per_Day_Before_And_12_Wood_Per_Day_After()
    {
        //Arrange
        Village village = _villageFixture.Village;
        
        // Setting up the starting conditions.
        village.AddWorker("Leif", Worker.Type.Lumberjack, () => village.AddWood());
        village.AddWorker("Leif II", Worker.Type.Lumberjack, () => village.AddWood());
        village.AddWorker("Leif III", Worker.Type.Lumberjack, () => village.AddWood());
        village.AddWorker("Leif IV", Worker.Type.Farmer, () => village.AddFood());
        village.AddWorker("Leif V", Worker.Type.Builder, () => village.Build());
        
        village.BuildingCompleted(new Building(Building.Type.Woodmill, village));
        // The woodmill comes for free in this test, so we must give the resources back that it cost.
        village.AddWood(5);
        village.AddMetal(1);

        var expectedLumberjacksBefore = 3;
        var expectedWoodPerDayBefore = 9; 
        
        var expectedWoodmills = 1;

        var expectedLumberjacksAfter = 4;
        var expectedWoodPerDayAfter = 12;

        //Act
        var actualLumberjacksBefore = village.GetWorkers().Count(w => w.Job == Worker.Type.Lumberjack);
        var actualWoodPerDayBefore = village.GetWoodPerDay(); 
        var actualWoodmills = village.GetBuildings().Count(b => b.BuildingType == Building.Type.Woodmill);

        village.AddWorker("Sven", Worker.Type.Lumberjack, () => village.AddWood());

        var actualLumberjacksAfter = village.GetWorkers().Count(w => w.Job == Worker.Type.Lumberjack);
        var actualWoodPerDayAfter = village.GetWoodPerDay();

        //Assert
        Assert.Equal(expectedLumberjacksBefore, actualLumberjacksBefore);
        Assert.Equal(expectedWoodPerDayBefore, actualWoodPerDayBefore);
        Assert.Equal(expectedWoodmills, actualWoodmills);
        Assert.Equal(expectedLumberjacksAfter, actualLumberjacksAfter);
        Assert.Equal(expectedWoodPerDayAfter, actualWoodPerDayAfter);
    }
}