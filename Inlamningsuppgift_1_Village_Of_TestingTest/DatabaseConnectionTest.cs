using System.Reflection;
using Inlamningsuppgift_1_Village_Of_Testing;
using Moq;
using Xunit.Abstractions;

namespace Inlamningsuppgift_1_Village_Of_TestingTest;

public class DatabaseConnectionTest
{
    private ITestOutputHelper _output;

    public DatabaseConnectionTest(ITestOutputHelper output)
    {
        this._output = output;
    }

    [Fact]
    public void LoadProgress_Loads_Values_Into_Village_And_They_Will_Be_Shown_Using_GetStats_Method()
    {
        // Arrange
        Mock<DatabaseConnection> dbcMock = new Mock<DatabaseConnection>();

        Village village = new Village(ui: new ConsoleUI(), databaseConnection: dbcMock.Object, startFood: 0);
        
        dbcMock.Setup(mock => mock.Load()).Callback(() =>
        {
            // Simulate loading values from database.
            int daysGone = 10;
            int food = 10;
            int foodPerDay = 15;
            int foodPerDayBonus = 10;
            int metal = 5;
            int metalPerDay = 3;
            int metalPerDayBonus = 2;
            int wood = 20;
            int woodPerDay = 3;
            int woodPerDayBonus = 2;
            List<Building> buildings = new List<Building>()
            {
                new Building(Building.Type.House, village),
                new Building(Building.Type.House, village),
                new Building(Building.Type.House, village),
                new Building(Building.Type.Farm, village),
                new Building(Building.Type.Woodmill, village),
                new Building(Building.Type.Quarry, village)
            };
            List<Building> projects = new List<Building>()
            {
                new Building(Building.Type.Castle, village)
            };
            List<Worker> workers = new List<Worker>()
            {
                new Worker("Sven", Worker.Type.Builder, () => village.Build()),
                new Worker("Olof", Worker.Type.Farmer, () => village.AddFood()),
                new Worker("Stina", Worker.Type.QuarryWorker, () => village.AddMetal()),
                new Worker("Anna", Worker.Type.Lumberjack, () => village.AddWood()),
            };
            bool gameOver = false;
            bool gameWon = false;
            int maxWorkers = 6;
            int farmers = 1;
            int lumberjacks = 1;
            int quarryworkers = 1;
            int builders = 1;
            int farms = 1;
            int woodmills = 1;
            int quarries = 1;
            int houses = 3;
            bool starvation = false;
            List<DayEventArgs> dayEventsList = new List<DayEventArgs>();
            List<Worker> starvingWorkers = new List<Worker>();
            
            // Save the simulated database values to the mocked DatabaseConnection object.
            //dbcMock.Object.Wood = wood;
            typeof(DatabaseConnection).GetField("_wood", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, wood);
        });
        
        // Save the simulated database values to the mocked DatabaseConnection object.
        //dbcMock.Object.GetType().GetField("_wood", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, wood);
        
        // Mock the methods that return the values from the database.
        dbcMock.Setup(mock => mock.GetWood())
            .Returns(() => dbcMock.Object.Wood);

        // Act
        village.LoadProgress();

        // Assert
        Assert.Equal(20, village.GetWood());
    }
}