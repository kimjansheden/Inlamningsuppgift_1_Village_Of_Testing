using GameLib;

namespace GameLibTest;

public class BuildingTest : IClassFixture<VillageFixture>
{
    private VillageFixture _villageFixture;

    public BuildingTest(VillageFixture villageFixture)
    {
        _villageFixture = villageFixture;
    }

    [Fact]
    public void BuildingTypeHouseCosts5Wood()
    {
        //Arrange
        Village village = _villageFixture.Village;
        Building building = new Building(Building.Type.House, village);
        var expectedWoodCost = 5;
        
        //Act
        var actualWoodCost = building.GetCostWood();
        
        //Assert
        Assert.Equal(expectedWoodCost, actualWoodCost);
    }
    [Fact]
    public void BuildingTypeHouseCosts0Metal()
    {
        //Arrange
        Village village = _villageFixture.Village;
        Building building = new Building(Building.Type.House, village);
        var expectedMetalCost = 0;
        
        //Act
        var actualMetalCost = building.GetCostMetal();
        
        //Assert
        Assert.Equal(expectedMetalCost, actualMetalCost);
    }
    [Fact]
    public void BuildingTypeHouseTakes3Days()
    {
        //Arrange
        Village village = _villageFixture.Village;
        Building building = new Building(Building.Type.House, village);
        var expectedDays = 3;
        
        //Act
        var actualDays = building.GetDaysToComplete();
        
        //Assert
        Assert.Equal(expectedDays, actualDays);
    }
    [Fact]
    public void BuildingTypeHouseCosts5Wood0MetalTakes3Days()
    {
        //Arrange
        Village village = _villageFixture.Village;
        Building building = new Building(Building.Type.House, village);
        var expectedWoodCost = 5;
        var expectedMetalCost = 0;
        var expectedDays = 3;
        
        //Act
        var actualWoodCost = building.GetCostWood();
        var actualMetalCost = building.GetCostMetal();
        var actualDays = building.GetDaysToComplete();
        
        //Assert
        Assert.Equal(expectedWoodCost, actualWoodCost);
        Assert.Equal(expectedMetalCost, actualMetalCost);
        Assert.Equal(expectedDays, actualDays);
    }
    [Fact]
    public void BuildingTypeWoodmillCosts5Wood1MetalTakes5Days()
    {
        //Arrange
        Village village = _villageFixture.Village;
        Building building = new Building(Building.Type.Woodmill, village);
        var expectedWoodCost = 5;
        var expectedMetalCost = 1;
        var expectedDays = 5;
        
        //Act
        var actualWoodCost = building.GetCostWood();
        var actualMetalCost = building.GetCostMetal();
        var actualDays = building.GetDaysToComplete();
        
        //Assert
        Assert.Equal(expectedWoodCost, actualWoodCost);
        Assert.Equal(expectedMetalCost, actualMetalCost);
        Assert.Equal(expectedDays, actualDays);
    }
    [Fact]
    public void BuildingTypeQuarryCosts3Wood5MetalTakes7Days()
    {
        //Arrange
        Village village = _villageFixture.Village;
        Building building = new Building(Building.Type.Quarry, village);
        var expectedWoodCost = 3;
        var expectedMetalCost = 5;
        var expectedDays = 7;
        
        //Act
        var actualWoodCost = building.GetCostWood();
        var actualMetalCost = building.GetCostMetal();
        var actualDays = building.GetDaysToComplete();
        
        //Assert
        Assert.Equal(expectedWoodCost, actualWoodCost);
        Assert.Equal(expectedMetalCost, actualMetalCost);
        Assert.Equal(expectedDays, actualDays);
    }
    [Fact]
    public void BuildingTypeFarmCosts5Wood2MetalTakes5Days()
    {
        //Arrange
        Village village = _villageFixture.Village;
        Building building = new Building(Building.Type.Farm, village);
        var expectedWoodCost = 5;
        var expectedMetalCost = 2;
        var expectedDays = 5;
        
        //Act
        var actualWoodCost = building.GetCostWood();
        var actualMetalCost = building.GetCostMetal();
        var actualDays = building.GetDaysToComplete();
        
        //Assert
        Assert.Equal(expectedWoodCost, actualWoodCost);
        Assert.Equal(expectedMetalCost, actualMetalCost);
        Assert.Equal(expectedDays, actualDays);
    }
    [Fact]
    public void BuildingTypeCastleCosts50Wood50MetalTakes50Days()
    {
        //Arrange
        Village village = _villageFixture.Village;
        Building building = new Building(Building.Type.Castle, village);
        var expectedWoodCost = 50;
        var expectedMetalCost = 50;
        var expectedDays = 50;
        
        //Act
        var actualWoodCost = building.GetCostWood();
        var actualMetalCost = building.GetCostMetal();
        var actualDays = building.GetDaysToComplete();
        
        //Assert
        Assert.Equal(expectedWoodCost, actualWoodCost);
        Assert.Equal(expectedMetalCost, actualMetalCost);
        Assert.Equal(expectedDays, actualDays);
    }
}