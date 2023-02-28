using Inlamningsuppgift_1_Village_Of_Testing;
using Xunit.Abstractions;
using static Inlamningsuppgift_1_Village_Of_Testing.Strings.Message;

namespace Inlamningsuppgift_1_Village_Of_TestingTest;

public class WorkerTest : IClassFixture<VillageFixture>
{
    private VillageFixture _villageFixture;
    private ITestOutputHelper _testOutputHelper;

    public WorkerTest(VillageFixture villageFixture, ITestOutputHelper testOutputHelper)
    {
        _villageFixture = villageFixture;
        _testOutputHelper = testOutputHelper;
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
        Strings strings = new Strings();
        var writer = new StringWriter();
        Console.SetOut(writer);
        var expectedMessage = strings.Messages[AddWorkerNoName];
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
        village.AddWorker("Olof V the Lumberjack", Worker.Type.Lumberjack, () => village.AddFood());
        var expectedMaxWorkers = 6;
    }
}