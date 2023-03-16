using GameLib;
using Moq;
using Xunit.Abstractions;
using static GameLib.Strings.Message;

namespace GameLibTest;

public class WorkerTest : IClassFixture<VillageFixture>
{
    private VillageFixture _villageFixture;
    private ITestOutputHelper _testOutputHelper;
    private IUI _ui;

    public WorkerTest(VillageFixture villageFixture, ITestOutputHelper testOutputHelper)
    {
        _villageFixture = villageFixture;
        _testOutputHelper = testOutputHelper;
        _ui = new ConsoleUI();
    }

    [Fact]
    public void AddWorkerViaWorkerNamedSvenCreatesANewWorkerNamedSven()
    {
        //Arrange
        Village village = _villageFixture.Village;
        Worker worker = new Worker("Sven", Worker.Type.Farmer, () => village.AddFood());
        var expectedName = "Sven";
        
        //Act
        var actualName = worker.Name;
        
        //Assert
        Assert.Equal(expectedName, actualName);
    }
    [Fact]
    public void AddWorkerViaVillageNamedSvenCreatesANewWorkerNamedSven()
    {
        //Arrange
        Village village = _villageFixture.Village;
        village.AddWorker("Sven", Worker.Type.Farmer, () => village.AddFood());
        var expectedName = "Sven";
        
        //Act
        var actualName = village.GetWorkers()[0].Name;
        
        //Assert
        Assert.Equal(expectedName, actualName);
    }

    [Fact]
    public void AddRandomWorker_Mock_Random_1_Should_Add_QuarryWorker_And_Do_The_Right_Job()
    {
        //Arrange
        Mock<RandomGenerator> randMock = new Mock<RandomGenerator>();
        Village village = new Village(new ConsoleUI(), random: randMock.Object);

        randMock.Setup(rand => rand.Next(0, Enum.GetValues(typeof(Worker.Type)).Length)).Returns(1);
        
        var expectedNumber = 1;
        var expectedJobType = Worker.Type.QuarryWorker;
        var expectedMetalBeforeDay = 0;
        var expectedMetalPerDayBeforeNewWorker = 0;
        var expectedMetalAfterDay = 1;
        var expectedMetalPerDayAfterNewWorker = 1;
        
        // Act
        var actualMetalPerDayBeforeNewWorker = village.GetMetalPerDay();
        
        village.AddRandomWorker("Sven");
        
        var actualMetalBeforeDay = village.GetMetal();

        village.Day();
        
        var actualNumber = randMock.Invocations.Last().ReturnValue;
        var actualJobType = village.GetWorkers()[0].Job;
        var actualMetalAfterDay = village.GetMetal();
        var actualMetalPerDayAfterNewWorker = village.GetMetalPerDay();

        // Assert
        randMock.Verify(rand => rand.Next(0, Enum.GetValues(typeof(Worker.Type)).Length), Times.Once);
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedJobType, actualJobType);
        Assert.Equal(expectedMetalBeforeDay, actualMetalBeforeDay);
        Assert.Equal(expectedMetalAfterDay, actualMetalAfterDay);
        Assert.Equal(expectedMetalPerDayBeforeNewWorker, actualMetalPerDayBeforeNewWorker);
        Assert.Equal(expectedMetalPerDayAfterNewWorker, actualMetalPerDayAfterNewWorker);
    }
    [Fact]
    public void AddRandomWorker_Mock_Random_2_Should_Add_Builder_And_Do_The_Right_Job()
    {
        //Arrange
        Mock<RandomGenerator> randMock = new Mock<RandomGenerator>();
        Village village = new Village(new ConsoleUI(), random: randMock.Object, startFood:100, startMetal:100, startWood:100);

        randMock.Setup(rand => rand.Next(0, Enum.GetValues(typeof(Worker.Type)).Length)).Returns(2);
        
        var expectedNumber = 2;
        var expectedJobType = Worker.Type.Builder;
        var expectedDaysWorkedOnProjectBeforeDay = 0;
        var expectedDaysWorkedOnProjectAfterDay = 1;

        // Act
        village.AddProject(Building.Type.House);
        
        var actualDaysWorkedOnProjectBeforeDay = village.GetProjects()[0].DaysSpent;
        
        village.AddRandomWorker("Sven");

        village.Day();
        
        var actualNumber = randMock.Invocations.Last().ReturnValue;
        var actualJobType = village.GetWorkers()[0].Job;
        var actualDaysWorkedOnProjectAfterDay = village.GetProjects()[0].DaysSpent;

        // Assert
        randMock.Verify(rand => rand.Next(0, Enum.GetValues(typeof(Worker.Type)).Length), Times.Once);
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedJobType, actualJobType);
        Assert.Equal(expectedDaysWorkedOnProjectBeforeDay, actualDaysWorkedOnProjectBeforeDay);
        Assert.Equal(expectedDaysWorkedOnProjectAfterDay, actualDaysWorkedOnProjectAfterDay);
    }
    [Fact]
    public void AddRandomWorker_Mock_Random_3_Should_Add_Lumberjack_And_Do_The_Right_Job()
    {
        //Arrange
        Mock<RandomGenerator> randMock = new Mock<RandomGenerator>();
        Village village = new Village(new ConsoleUI(), random: randMock.Object);

        randMock.Setup(rand => rand.Next(0, Enum.GetValues(typeof(Worker.Type)).Length)).Returns(3);
        
        var expectedNumber = 3;
        var expectedJobType = Worker.Type.Lumberjack;
        var expectedWoodBeforeDay = 0;
        var expectedWoodPerDayBeforeNewWorker = 0;
        var expectedWoodAfterDay = 1;
        var expectedWoodPerDayAfterNewWorker = 1;
        
        // Act
        var actualWoodPerDayBeforeNewWorker = village.GetWoodPerDay();
        
        village.AddRandomWorker("Sven");
        
        var actualWoodBeforeDay = village.GetWood();

        village.Day();
        
        var actualNumber = randMock.Invocations.Last().ReturnValue;
        var actualJobType = village.GetWorkers()[0].Job;
        var actualWoodAfterDay = village.GetWood();
        var actualWoodPerDayAfterNewWorker = village.GetWoodPerDay();

        // Assert
        randMock.Verify(rand => rand.Next(0, Enum.GetValues(typeof(Worker.Type)).Length), Times.Once);
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedJobType, actualJobType);
        Assert.Equal(expectedWoodBeforeDay, actualWoodBeforeDay);
        Assert.Equal(expectedWoodAfterDay, actualWoodAfterDay);
        Assert.Equal(expectedWoodPerDayBeforeNewWorker, actualWoodPerDayBeforeNewWorker);
        Assert.Equal(expectedWoodPerDayAfterNewWorker, actualWoodPerDayAfterNewWorker);
    }
    [Fact]
    public void AddRandomWorker_Mock_Random_4_Should_Add_Farmer_And_Do_The_Right_Job()
    {
        //Arrange
        Mock<RandomGenerator> randMock = new Mock<RandomGenerator>();
        Village village = new Village(new ConsoleUI(), random: randMock.Object);

        randMock.Setup(rand => rand.Next(0, Enum.GetValues(typeof(Worker.Type)).Length)).Returns(4);
        
        var expectedNumber = 4;
        var expectedJobType = Worker.Type.Farmer;
        var expectedFoodBeforeDay = 10;
        var expectedFoodPerDayBeforeNewWorker = 0;
        var expectedFoodAfterDay = 14; // Because he eats one.
        var expectedFoodPerDayAfterNewWorker = 5;
        
        // Act
        var actualFoodPerDayBeforeNewWorker = village.GetFoodPerDay();
        
        village.AddRandomWorker("Sven");
        
        var actualFoodBeforeDay = village.GetFood();

        village.Day();
        
        var actualNumber = randMock.Invocations.Last().ReturnValue;
        var actualJobType = village.GetWorkers()[0].Job;
        var actualFoodAfterDay = village.GetFood();
        var actualFoodPerDayAfterNewWorker = village.GetFoodPerDay();

        // Assert
        randMock.Verify(rand => rand.Next(0, Enum.GetValues(typeof(Worker.Type)).Length), Times.Once);
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedJobType, actualJobType);
        Assert.Equal(expectedFoodBeforeDay, actualFoodBeforeDay);
        Assert.Equal(expectedFoodAfterDay, actualFoodAfterDay);
        Assert.Equal(expectedFoodPerDayBeforeNewWorker, actualFoodPerDayBeforeNewWorker);
        Assert.Equal(expectedFoodPerDayAfterNewWorker, actualFoodPerDayAfterNewWorker);
    }
    [Fact]
    public void Add1WorkerVillageShouldHave1Worker()
    {
        //Arrange
        Village village = _villageFixture.Village;
        village.AddWorker("Sven", Worker.Type.Farmer, () => village.AddFood());
        var expectedWorkers = 1;
        
        //Act
        var actualWorkers = village.GetWorkers().Count;
        
        //Assert
        Assert.Equal(expectedWorkers, actualWorkers);
    }
    [Fact]
    public void Add2WorkersVillageShouldHave2Workers()
    {
        //Arrange
        Village village = _villageFixture.Village;
        village.AddWorker("Sven", Worker.Type.Farmer, () => village.AddFood());
        village.AddWorker("Sven II", Worker.Type.Farmer, () => village.AddFood());
        var expectedWorkers = 2;
        
        //Act
        var actualWorkers = village.GetWorkers().Count;
        
        //Assert
        Assert.Equal(expectedWorkers, actualWorkers);
    }
    [Fact]
    public void Add3WorkersVillageShouldHave3Workers()
    {
        //Arrange
        Village village = _villageFixture.Village;
        village.AddWorker("Sven", Worker.Type.Farmer, () => village.AddFood());
        village.AddWorker("Sven II", Worker.Type.Farmer, () => village.AddFood());
        village.AddWorker("Sven III", Worker.Type.Farmer, () => village.AddFood());
        var expectedWorkers = 3;
        
        //Act
        var actualWorkers = village.GetWorkers().Count;
        
        //Assert
        Assert.Equal(expectedWorkers, actualWorkers);
    }
    [Fact]
    public void AddWorkerViaVillageWithJobFarmerCreatesANewWorkerWithJobFarmer()
    {
        //Arrange
        Village village = _villageFixture.Village;
        village.AddWorker("Sven", Worker.Type.Farmer, () => village.AddFood());
        var expectedJob = Worker.Type.Farmer;
        
        //Act
        var actualJob = village.GetWorkers()[0].Job;
        
        //Assert
        Assert.Equal(expectedJob, actualJob);
    }
    [Fact]
    public void AddWorkerNoNameSendsExpectedMessage()
    {
        //Arrange
        Village village = _villageFixture.Village;
        Strings strings = new Strings(_ui);
        var writer = new StringWriter();
        Console.SetOut(writer);
        var expectedMessage = strings.Messages[WorkerAddNoName];
        village.AddWorker("", Worker.Type.Farmer, () => village.AddFood());

        //Act
        var actualMessage = writer.ToString().Trim();
        
        //Assert
        Assert.Equal(expectedMessage, actualMessage);
        _testOutputHelper.WriteLine(actualMessage);
    }

    [Fact]
    public void AddWorkerFailsNotEnoughHouses()
    {
        //Arrange
        Village village = _villageFixture.Village;
        village.AddWorker("Sven the Farmer", Worker.Type.Farmer, () => village.AddFood());
        village.AddWorker("Bob the Quarry Man", Worker.Type.QuarryWorker, () => village.AddFood());
        village.AddWorker("Olof the Lumberjack", Worker.Type.Lumberjack, () => village.AddFood());
        village.AddWorker("Olof II the Lumberjack", Worker.Type.Lumberjack, () => village.AddFood());
        village.AddWorker("Olof III the Lumberjack", Worker.Type.Lumberjack, () => village.AddFood());
        village.AddWorker("Olof IV the Lumberjack", Worker.Type.Lumberjack, () => village.AddFood());

        var expectedSuccess = false;
        _testOutputHelper.WriteLine($"Expected success: {expectedSuccess}");
        var expectedMaxWorkers = 6;
        _testOutputHelper.WriteLine($"Expected max workers: {expectedMaxWorkers}");
        var expectedWorkers = 6;
        _testOutputHelper.WriteLine($"Expected workers: {expectedWorkers}");
        Strings strings = new Strings(_ui);
        var writer = new StringWriter();
        Console.SetOut(writer);
        var expectedMessage = strings.Messages[WorkerAddNoRoom];
        _testOutputHelper.WriteLine($"Expected message: {expectedMessage}");
        
        _testOutputHelper.WriteLine("");
        
        // Act
        var actualSuccess = village.AddWorker("Olof V the Lumberjack", Worker.Type.Lumberjack, () => village.AddFood());
        _testOutputHelper.WriteLine($"Actual success: {actualSuccess}");
        
        var actualMaxWorkers = village.GetMaxWorkers();
        _testOutputHelper.WriteLine($"Actual max workers: {actualMaxWorkers}");
        
        var actualWorkers = village.GetWorkers().Count;
        _testOutputHelper.WriteLine($"Actual workers: {actualWorkers}");
        
        var actualMessage = writer.ToString().Trim();
        _testOutputHelper.WriteLine($"Actual message: {actualMessage}");

        //Assert
        Assert.Equal(expectedSuccess, actualSuccess);
        Assert.Equal(expectedMaxWorkers, actualMaxWorkers);
        Assert.Equal(expectedWorkers, actualWorkers);
        Assert.Equal(expectedMessage, actualMessage);
    }
    [Fact]
    public void AFourthHouseAllows8WorkersButNot9()
    {
        //Arrange
        Village village = _villageFixture.Village;
        
        // Attempt to add 6 workers to live in the 3 start houses.
        village.AddWorker("Sven the Farmer", Worker.Type.Farmer, () => village.AddFood());
        village.AddWorker("Bob the Quarry Man", Worker.Type.QuarryWorker, () => village.AddFood());
        village.AddWorker("Olof the Lumberjack", Worker.Type.Lumberjack, () => village.AddFood());
        village.AddWorker("Olof II the Lumberjack", Worker.Type.Lumberjack, () => village.AddFood());
        village.AddWorker("Olof III the Lumberjack", Worker.Type.Lumberjack, () => village.AddFood());
        village.AddWorker("Olof IV the Builder", Worker.Type.Builder, () => village.Build());
        
        // We need wood to build the house.
        village.AddWood(5);
        
        // Add a new house
        village.AddProject(Building.Type.House);
        
        var expectedMaxWorkersBeforeNewHouseIsBuilt = 6;
        _testOutputHelper.WriteLine($"Expected max workers before the fourth house is built: {expectedMaxWorkersBeforeNewHouseIsBuilt}");
        
        var expectedWorkersBeforeNewHouseIsBuilt = 6;
        _testOutputHelper.WriteLine($"Expected workers before the fourth house is built: {expectedWorkersBeforeNewHouseIsBuilt}");
        
        // After the fourth house is built, the village should allow two more workers to live there.
        var expectedMaxWorkersAfterNewHouseIsBuilt = 8;
        _testOutputHelper.WriteLine($"Expected max workers after the fourth house is built: {expectedMaxWorkersAfterNewHouseIsBuilt}");
        
        // Two new workers should be added successfully after the fourth house is built.
        var expectedNumberOfTrueInBoolList = 2;
        _testOutputHelper.WriteLine($"Expected number of success to add worker = true: {expectedNumberOfTrueInBoolList}");
        
        // A ninth worker should not be possible to add, thus we should get a false value.
        var expectedNumberOfFalseInBoolList = 1;
        _testOutputHelper.WriteLine($"Expected number of success to add worker = false: {expectedNumberOfFalseInBoolList}");
        
        // After the fourth house is built, two more workers have been successfully added to the village and one worker failed to add to the village, we should have 8 workers living in the village.
        var expectedWorkersAfterNewHouseIsBuiltAndTotalOf9AttemptsToAddANewWorker = 8;
        _testOutputHelper.WriteLine($"Expected workers after the fourth house is built and a total of 9 attempts to add a worker: {expectedWorkersAfterNewHouseIsBuiltAndTotalOf9AttemptsToAddANewWorker}");
        
        _testOutputHelper.WriteLine("");
        
        // Act
        
        var actualMaxWorkersBeforeNewHouseIsBuilt = village.GetMaxWorkers();
        _testOutputHelper.WriteLine($"Actual max workers before the fourth house is built: {actualMaxWorkersBeforeNewHouseIsBuilt}");
        
        var actualWorkersBeforeNewHouseIsBuilt = village.GetWorkers().Count;
        _testOutputHelper.WriteLine($"Actual workers before the fourth house is built: {actualWorkersBeforeNewHouseIsBuilt}");
        
        // The house gets built.
        for (int i = 0; i < 3; i++)
        {
            village.Day();
        }
        
        var actualMaxWorkersAfterNewHouseIsBuilt = village.GetMaxWorkers();
        _testOutputHelper.WriteLine($"Actual max workers after the fourth house is built: {actualMaxWorkersAfterNewHouseIsBuilt}");
        
        // Try to add two more workers to the village.
        var boolList = new List<bool>();
        boolList.Add(village.AddWorker("Olof V the Builder", Worker.Type.Builder, () => village.Build()));
        boolList.Add(village.AddWorker("Olof VI the Builder", Worker.Type.Builder, () => village.Build()));
        var actualNumberOfTrueInBoolList = boolList.Count(b => b);
        _testOutputHelper.WriteLine($"Actual number of success to add worker = true: {actualNumberOfTrueInBoolList}");
        
        // Try to add one more worker.
        boolList.Add(village.AddWorker("Olof VI the Builder", Worker.Type.Builder, () => village.Build()));
        var actualNumberOfFalseInBoolList = boolList.Count(b => !b);
        _testOutputHelper.WriteLine($"Actual number of success to add worker = false: {actualNumberOfFalseInBoolList}");
        
        var actualWorkersAfterNewHouseIsBuiltAndTotalOf9AttemptsToAddANewWorker = village.GetWorkers().Count;
        _testOutputHelper.WriteLine($"Actual workers after the fourth house is built and a total of 9 attempts to add a worker: {actualWorkersAfterNewHouseIsBuiltAndTotalOf9AttemptsToAddANewWorker}");

        //Assert
        Assert.Equal(expectedMaxWorkersBeforeNewHouseIsBuilt, actualMaxWorkersBeforeNewHouseIsBuilt);
        Assert.Equal(expectedWorkersBeforeNewHouseIsBuilt, actualWorkersBeforeNewHouseIsBuilt);
        Assert.Equal(expectedMaxWorkersAfterNewHouseIsBuilt, actualMaxWorkersAfterNewHouseIsBuilt);
        Assert.Equal(expectedNumberOfTrueInBoolList, actualNumberOfTrueInBoolList);
        Assert.Equal(expectedNumberOfFalseInBoolList, actualNumberOfFalseInBoolList);
        Assert.Equal(expectedWorkersAfterNewHouseIsBuiltAndTotalOf9AttemptsToAddANewWorker, actualWorkersAfterNewHouseIsBuiltAndTotalOf9AttemptsToAddANewWorker);
    }
    
    [Fact]
    public void OneDayOneFarmerGives14FoodAtTheEndOfDay()
    {
        //Arrange
        
        // Add the village and the workers.
        Village village = _villageFixture.Village;
        village.AddWorker("Sven the Farmer", Worker.Type.Farmer, () => village.AddFood());

        // One farmer brings in 5 food and eats 1 food. The village starts with 10 food. Therefore, the village should have 14 food after one day.
        var expectedFoodAtTheEndOfDay = 14;
        _testOutputHelper.WriteLine($"Expected food at the end of day: {expectedFoodAtTheEndOfDay}");
        var expectedDaysGone = 1;
        _testOutputHelper.WriteLine($"Expected days passed by: {expectedDaysGone}");

        _testOutputHelper.WriteLine("");
        
        //Act
        // 1 day passing by.
        for (int i = 0; i < expectedDaysGone; i++)
        {
            village.Day();
        }
        
        var actualFoodAtTheEndOfDay = village.GetFood();
        _testOutputHelper.WriteLine($"Actual food at the end of day: {actualFoodAtTheEndOfDay}");
        
        var actualDaysGone = village.GetDaysGone();
        _testOutputHelper.WriteLine($"Actual days passed by: {actualDaysGone}");

        //Assert
        Assert.Equal(expectedFoodAtTheEndOfDay, actualFoodAtTheEndOfDay);
        Assert.Equal(expectedDaysGone, actualDaysGone);
    }
    
    [Fact]
    public void OneDayOneLumberjackGives1WoodAtTheEndOfDay()
    {
        //Arrange
        
        // Add the village and the workers.
        Village village = _villageFixture.Village;
        village.AddWorker("Lyle the Lumberjack", Worker.Type.Lumberjack, () => village.AddWood());

        // One lumberjack brings in 1 wood. The village starts with 0 wood. Therefore, the village should have 1 wood after one day.
        var expectedWoodAtTheEndOfDay = 1;
        _testOutputHelper.WriteLine($"Expected wood at the end of day: {expectedWoodAtTheEndOfDay}");
        var expectedDaysGone = 1;
        _testOutputHelper.WriteLine($"Expected days passed by: {expectedDaysGone}");

        _testOutputHelper.WriteLine("");
        
        //Act
        // 1 day passing by.
        for (int i = 0; i < expectedDaysGone; i++) village.Day();

        var actualWoodAtTheEndOfDay = village.GetWood();
        _testOutputHelper.WriteLine($"Actual wood at the end of day: {actualWoodAtTheEndOfDay}");
        
        var actualDaysGone = village.GetDaysGone();
        _testOutputHelper.WriteLine($"Actual days passed by: {actualDaysGone}");

        //Assert
        Assert.Equal(expectedWoodAtTheEndOfDay, actualWoodAtTheEndOfDay);
        Assert.Equal(expectedDaysGone, actualDaysGone);
    }
    [Fact]
    public void OneDayOneQuarrymanGives1MetalAtTheEndOfDay()
    {
        //Arrange
        
        // Add the village and the workers.
        Village village = _villageFixture.Village;
        village.AddWorker("Sten the Quarry Man", Worker.Type.QuarryWorker, () => village.AddMetal());

        // One quarry worker brings in 1 metal. The village starts with 0 metal. Therefore, the village should have 1 metal after one day.
        var expectedMetalAtTheEndOfDay = 1;
        _testOutputHelper.WriteLine($"Expected metal at the end of day: {expectedMetalAtTheEndOfDay}");
        var expectedDaysGone = 1;
        _testOutputHelper.WriteLine($"Expected days passed by: {expectedDaysGone}");

        _testOutputHelper.WriteLine("");
        
        //Act
        // 1 day passing by.
        for (int i = 0; i < expectedDaysGone; i++) village.Day();

        var actualMetalAtTheEndOfDay = village.GetMetal();
        _testOutputHelper.WriteLine($"Actual metal at the end of day: {actualMetalAtTheEndOfDay}");
        
        var actualDaysGone = village.GetDaysGone();
        _testOutputHelper.WriteLine($"Actual days passed by: {actualDaysGone}");

        //Assert
        Assert.Equal(expectedMetalAtTheEndOfDay, actualMetalAtTheEndOfDay);
        Assert.Equal(expectedDaysGone, actualDaysGone);
    }
}