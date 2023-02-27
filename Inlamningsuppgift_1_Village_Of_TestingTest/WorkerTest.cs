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
        Worker worker = new Worker("Sven", () => village.AddFood(5));
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
        village.AddWorker("Sven", () => village.AddFood(5));
        var expectedName = "Sven";
        
        //Act
        var actualName = village.GetWorkers()[0].Name;
        
        //Assert
        Assert.Equal(expectedName, actualName);
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
        village.AddWorker("", () => village.AddFood(5));

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
        village.AddWorker("Sven the Farmer", () => village.AddFood(5));
        village.AddWorker("Bob the Quarry Man", () => village.AddMetal(1));
        village.AddWorker("Olof the Lumberjack", () => village.AddWood(1));
        village.AddWorker("Olof II the Lumberjack", () => village.AddWood(1));
        village.AddWorker("Olof III the Lumberjack", () => village.AddWood(1));
        village.AddWorker("Olof IV the Lumberjack", () => village.AddWood(1));
        village.AddWorker("Olof V the Lumberjack", () => village.AddWood(1));
        var expectedMaxWorkers = 6;
    }
}