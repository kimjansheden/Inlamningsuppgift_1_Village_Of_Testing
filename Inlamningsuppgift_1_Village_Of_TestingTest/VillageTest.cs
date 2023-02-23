using Inlamningsuppgift_1_Village_Of_Testing;

namespace Inlamningsuppgift_1_Village_Of_TestingTest;

public class VillageTest
{
    [Fact]
    public void VillageConstructorCreates10Food()
    {
        //Arrange
        Village village = new Village();
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
        Village village = new Village();
        var expectedHouses = 3;
        
        //Act
        var actualHouses = village.GetBuildings().Count;
        
        //Assert
        Assert.Equal(expectedHouses, actualHouses);
    }
}