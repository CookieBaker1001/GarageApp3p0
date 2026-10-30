namespace GarageApp3p0.Tests;

using GarageApp3p0.Model;
using GarageApp3p0.Model.Vehicles;

public class GarageTests
{
    [Fact]
    public void Garage_ShouldBeCreatedCorrectly() 
    {
        // Arrange
        int capacity = 5;

        // Act
        Garage<Vehicle> garage = new Garage<Vehicle>(capacity);

        // Assert
        Assert.Equal(capacity, garage.Capacity);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-3)]
    public void Garage_ShouldHaveValidCapacity(int capacity) {
        // Arrange
        Garage<Vehicle> garage;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => garage = new Garage<Vehicle>(capacity));
    }

    [Fact]
    public void Garage_ShouldBeEnumerable()
    {
        // Arrange
        Garage<Vehicle> garage = new Garage<Vehicle>(5);

        // Act
        IEnumerator<Vehicle> enumerator = garage.GetEnumerator();

        // Assert
        Assert.NotNull(enumerator);
    }
}
