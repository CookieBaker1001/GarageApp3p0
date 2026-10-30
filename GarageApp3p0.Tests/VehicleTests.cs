namespace GarageApp3p0.Tests;

using GarageApp3p0.Model.Vehicles;

public class VehicleTests
{
    [Theory]
    [InlineData(1, "Toyota", "Corolla", "ABC123", 2020, "Blue")]
    [InlineData(2, "Harley-Davidson", "Street 750", "XYZ789", 2021, "Black")]
    [InlineData(3, "Yamaha", "WaveRunner", "DEF456", 2020, "Blue")]
    [InlineData(4, "Boeing", "737", "GHI789", 2021, "White")]
    [InlineData(5, "Mercedes", "Sprinter", "JKL012", 2020, "White")]
    public void Vehicle_ToString_ReturnsCorrectFormat(int type, string make, 
        string model, string regNumber, int year, string color)
    {
        // Arrange
        Vehicle v;
        if (type == 1) v = new Car(make, model, regNumber, year, color);
        else if (type == 2) v = new Motorcycle(make, model, regNumber, year, color);
        else if (type == 3) v = new Boat(make, model, regNumber, year, color);
        else if (type == 4) v = new Airplane(make, model, regNumber, year, color);
        else v = new Buss(make, model, regNumber, year, color);

        // Act
        string result = v.ToString();

        // Assert
        Assert.Equal($"Make: {make}, Model: {model}, RegNumber: {regNumber}, Year: {year}, Color: {color}", result);
    }
}
