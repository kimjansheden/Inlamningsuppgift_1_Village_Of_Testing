using Inlamningsuppgift_1_Village_Of_Testing;

namespace Inlamningsuppgift_1_Village_Of_TestingTest;

public class VillageTest : IClassFixture<VillageFixture>
{
    private VillageFixture _villageFixture;

    public VillageTest(VillageFixture villageFixture)
    {
        _villageFixture = villageFixture;
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
    public void VillageConstructorCreates3Houses()
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
    public void AddWorker(string name, Worker.WorkDelegate work)
    {

    }
    public void Build()
    {

    }
    public void BuryDead()
    {

    }
    public void Day()
    {

    }
    public void FeedWorkers()
    {

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