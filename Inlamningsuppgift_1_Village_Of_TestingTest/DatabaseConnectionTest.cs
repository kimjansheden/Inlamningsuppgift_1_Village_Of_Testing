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
    public void LoadProgress_Loads_Values_Into_Village_And_They_Will_Be_Shown_Using_Get_Methods()
    {
        // Arrange
        
        // Create the mock database connection and the test village.
        Mock<DatabaseConnection> dbcMock = new Mock<DatabaseConnection>();

        Village village = new Village(ui: new ConsoleUI(), databaseConnection: dbcMock.Object, startFood: 0);
        
        // Expected results.
        int expectedDaysGone = 10;
        int expectedFood = 10;
        int expectedFoodPerDay = 15;
        int expectedFoodPerDayBonus = 10;
        int expectedMetal = 5;
        int expectedMetalPerDay = 3;
        int expectedMetalPerDayBonus = 2;
        int expectedWood = 20;
        int expectedWoodPerDay = 3;
        int expectedWoodPerDayBonus = 2;
        List<Building> expectedBuildings = new List<Building>()
        {
            new Building(Building.Type.House, village),
            new Building(Building.Type.House, village),
            new Building(Building.Type.House, village),
            new Building(Building.Type.Farm, village),
            new Building(Building.Type.Woodmill, village),
            new Building(Building.Type.Quarry, village)
        };
        List<Building> expectedProjects = new List<Building>()
        {
            new Building(Building.Type.Castle, village)
        };
        List<Worker> expectedWorkers = new List<Worker>()
        {
            new Worker("Sven", Worker.Type.Builder, () => village.Build()),
            new Worker("Olof", Worker.Type.Farmer, () => village.AddFood()),
            new Worker("Stina", Worker.Type.QuarryWorker, () => village.AddMetal()),
            new Worker("Anna", Worker.Type.Lumberjack, () => village.AddWood()),
        };
        bool expectedGameOver = false;
        bool expectedGameWon = false;
        int expectedMaxWorkers = 6;
        int expectedFarmers = 1;
        int expectedLumberjacks = 1;
        int expectedQuarryworkers = 1;
        int expectedBuilders = 1;
        int expectedFarms = 1;
        int expectedWoodmills = 1;
        int expectedQuarries = 1;
        int expectedHouses = 3;
        bool expectedStarvation = false;
        List<DayEventArgs> expectedDayEventsList = new List<DayEventArgs>();
        List<Worker> expectedStarvingWorkers = new List<Worker>();
        
        // Mock the DatabaseConnection.Load() method.
        dbcMock.Setup(mock => mock.Load()).Callback(() =>
        {
            // Simulate loading values from database.
            int daysGone = expectedDaysGone;
            int food = expectedFood;
            int foodPerDay = expectedFoodPerDay;
            int foodPerDayBonus = expectedFoodPerDayBonus;
            int metal = expectedMetal;
            int metalPerDay = expectedMetalPerDay;
            int metalPerDayBonus = expectedMetalPerDayBonus;
            int wood = expectedWood;
            int woodPerDay = expectedWoodPerDay;
            int woodPerDayBonus = expectedWoodPerDayBonus;
            List<Building> buildings = expectedBuildings;
            List<Building> projects = expectedProjects;
            List<Worker> workers = expectedWorkers;
            bool gameOver = expectedGameOver;
            bool gameWon = expectedGameWon;
            int maxWorkers = expectedMaxWorkers;
            int farmers = expectedFarmers;
            int lumberjacks = expectedLumberjacks;
            int quarryworkers = expectedQuarryworkers;
            int builders = expectedBuilders;
            int farms = expectedFarms;
            int woodmills = expectedWoodmills;
            int quarries = expectedQuarries;
            int houses = expectedHouses;
            bool starvation = expectedStarvation;
            List<DayEventArgs> dayEventsList = expectedDayEventsList;
            List<Worker> starvingWorkers = expectedStarvingWorkers;
            
            // Save the simulated database values to the mocked DatabaseConnection object.
            typeof(DatabaseConnection).GetField("_wood", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, wood);
            
            typeof(DatabaseConnection).GetField("_daysGone", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, daysGone);
            
            typeof(DatabaseConnection).GetField("_food", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, food);
            
            typeof(DatabaseConnection).GetField("_foodPerDay", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, foodPerDay);
            
            typeof(DatabaseConnection).GetField("_foodPerDayBonus", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, foodPerDayBonus);
            
            typeof(DatabaseConnection).GetField("_metal", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, metal);
            
            typeof(DatabaseConnection).GetField("_metalPerDay", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, metalPerDay);
            
            typeof(DatabaseConnection).GetField("_metalPerDayBonus", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, metalPerDayBonus);
            
            typeof(DatabaseConnection).GetField("_woodPerDay", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, woodPerDay);
            
            typeof(DatabaseConnection).GetField("_woodPerDayBonus", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, woodPerDayBonus);
            
            typeof(DatabaseConnection).GetField("_buildings", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, buildings);
            
            typeof(DatabaseConnection).GetField("_projects", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, projects);
            
            typeof(DatabaseConnection).GetField("_workers", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, workers);
            
            typeof(DatabaseConnection).GetField("_gameOver", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, gameOver);
            
            typeof(DatabaseConnection).GetField("_gameWon", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, gameWon);
            
            typeof(DatabaseConnection).GetField("_maxWorkers", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, maxWorkers);
            
            typeof(DatabaseConnection).GetField("_farmers", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, farmers);
            
            typeof(DatabaseConnection).GetField("_lumberjacks", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, lumberjacks);
            
            typeof(DatabaseConnection).GetField("_quarryworkers", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, quarryworkers);
            
            typeof(DatabaseConnection).GetField("_builders", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, builders);
            
            typeof(DatabaseConnection).GetField("_farms", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, farms);
            
            typeof(DatabaseConnection).GetField("_woodmills", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, woodmills);
            
            typeof(DatabaseConnection).GetField("_quarries", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, quarries);
            
            typeof(DatabaseConnection).GetField("_houses", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, houses);
            
            typeof(DatabaseConnection).GetField("_starvation", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, starvation);
            
            typeof(DatabaseConnection).GetField("_dayEventsList", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, dayEventsList);
            
            typeof(DatabaseConnection).GetField("_starvingWorkers", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dbcMock.Object, starvingWorkers);
        });

        // Mock the methods that return the values from the database.
        dbcMock.Setup(mock => mock.GetWood())
            .Returns(() => (int)typeof(DatabaseConnection).GetField("_wood", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetDaysGone())
            .Returns(() => (int)typeof(DatabaseConnection).GetField("_daysGone", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetFood())
            .Returns(() => (int)typeof(DatabaseConnection).GetField("_food", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetFoodPerDay())
            .Returns(() => (int)typeof(DatabaseConnection).GetField("_foodPerDay", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetFoodPerDayBonus())
            .Returns(() => (int)typeof(DatabaseConnection).GetField("_foodPerDayBonus", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetMetal())
            .Returns(() => (int)typeof(DatabaseConnection).GetField("_metal", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetMetalPerDay())
            .Returns(() => (int)typeof(DatabaseConnection).GetField("_metalPerDay", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetMetalPerDayBonus())
            .Returns(() => (int)typeof(DatabaseConnection).GetField("_metalPerDayBonus", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetWoodPerDay())
            .Returns(() => (int)typeof(DatabaseConnection).GetField("_woodPerDay", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetWoodPerDayBonus())
            .Returns(() => (int)typeof(DatabaseConnection).GetField("_woodPerDayBonus", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetBuildings())
            .Returns(() => (List<Building>)typeof(DatabaseConnection).GetField("_buildings", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetProjects())
            .Returns(() => (List<Building>)typeof(DatabaseConnection).GetField("_projects", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetWorkers())
            .Returns(() => (List<Worker>)typeof(DatabaseConnection).GetField("_workers", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetGameOver())
            .Returns(() => (bool)typeof(DatabaseConnection).GetField("_gameOver", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetGameWon())
            .Returns(() => (bool)typeof(DatabaseConnection).GetField("_gameWon", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetMaxWorkers())
            .Returns(() => (int)typeof(DatabaseConnection).GetField("_maxWorkers", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetFarmers())
            .Returns(() => (int)typeof(DatabaseConnection).GetField("_farmers", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetLumberjacks())
            .Returns(() => (int)typeof(DatabaseConnection).GetField("_lumberjacks", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetQuarryWorkers())
            .Returns(() => (int)typeof(DatabaseConnection).GetField("_quarryworkers", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetBuilders())
            .Returns(() => (int)typeof(DatabaseConnection).GetField("_builders", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetFarms())
            .Returns(() => (int)typeof(DatabaseConnection).GetField("_farms", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetWoodmills())
            .Returns(() => (int)typeof(DatabaseConnection).GetField("_woodmills", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetQuarries())
            .Returns(() => (int)typeof(DatabaseConnection).GetField("_quarries", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetHouses())
            .Returns(() => (int)typeof(DatabaseConnection).GetField("_houses", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetStarvation())
            .Returns(() => (bool)typeof(DatabaseConnection).GetField("_starvation", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetDayEventsList())
            .Returns(() => (List<DayEventArgs>)typeof(DatabaseConnection).GetField("_dayEventsList", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));
        
        dbcMock.Setup(mock => mock.GetStarvingWorkers())
            .Returns(() => (List<Worker>)typeof(DatabaseConnection).GetField("_starvingWorkers", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dbcMock.Object));

        // Act
        
        // Now load the mocked database values into the test village, thus recreating a saved state.
        village.LoadProgress();

        // Assert
        
        // Check if we successfully recreated the Village state from our mocked database.
        
        Assert.Equal(expectedWood, village.GetWood());
        Assert.Equal(expectedDaysGone, village.GetDaysGone());
        Assert.Equal(expectedFood, village.GetFood());
        Assert.Equal(expectedFoodPerDay, village.GetFoodPerDay());
        Assert.Equal(expectedFoodPerDayBonus, village.GetFoodPerDayBonus());
        Assert.Equal(expectedMetal, village.GetMetal());
        Assert.Equal(expectedMetalPerDay, village.GetMetalPerDay());
        Assert.Equal(expectedMetalPerDayBonus, village.GetMetalPerDayBonus());
        Assert.Equal(expectedWoodPerDay, village.GetWoodPerDay());
        Assert.Equal(expectedWoodPerDayBonus, village.GetWoodPerDayBonus());
        Assert.Equal(expectedBuildings, village.GetBuildings());
        Assert.Equal(expectedProjects, village.GetProjects());
        Assert.Equal(expectedWorkers, village.GetWorkers());
        Assert.Equal(expectedGameOver, village.GameOver);
        Assert.Equal(expectedGameWon, village.GameWon);
        Assert.Equal(expectedMaxWorkers, village.GetMaxWorkers());
        Assert.Equal(expectedFarmers, village.GetFarmers());
        Assert.Equal(expectedLumberjacks, village.GetLumberjacks());
        Assert.Equal(expectedQuarryworkers, village.GetQuarryWorkers());
        Assert.Equal(expectedBuilders, village.GetBuilders());
        Assert.Equal(expectedFarms, village.GetFarms());
        Assert.Equal(expectedWoodmills, village.GetWoodmills());
        Assert.Equal(expectedQuarries, village.GetQuarries());
        Assert.Equal(expectedHouses, village.GetHouses());
        Assert.Equal(expectedStarvation, village.Starvation);
        Assert.Equal(expectedDayEventsList, village.GetDayEventsList());
        Assert.Equal(expectedStarvingWorkers, village.GetStarvingWorkers());
        dbcMock.Verify(mock => mock.Load(), Times.Once());
    }
}