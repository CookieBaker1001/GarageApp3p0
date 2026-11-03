namespace GarageApp3p0.Tests;

using GarageApp3p0.Application.Model;
using GarageApp3p0.Model.Vehicles;
using System.Drawing;
using System.Reflection.Emit;
using System.Security.Cryptography;

public class VehicleTests
{
    [Theory]
    [InlineData("Toyota", "Corolla", "ABC123", 2020, 5, "blå", false)]
    public void Car_ToString_ShouldBeCorrect(string make, string model,
        string regNumber, int year, int seatCount, string color, bool hasSunroof)
    {
        // Arrange
        Car motorcycle = new Car(
            make, model, regNumber, year, seatCount, color, hasSunroof);
        string toString = $"Bil (Storlek: 3)\n" + $"Märke: {make}\nModell: {model} ({year})\n" +
                $"Reg-nummer: {regNumber}, Sittplatser: {seatCount}, Färg: {color}" +
                $"\nFönstertak?: {(hasSunroof ? "Ja" : "Nej")}";

        // Act
        string result = motorcycle.ToString();
        
        // Assert
        Assert.Equal(toString, result);
    }

    [Theory]
    [InlineData("Harley", "Davidson", "HEY750", 2021, 1, "svart", false)]
    public void Motorcycle_ToString_ShouldBeCorrect(string make, string model, 
        string regNumber, int year, int seatCount, string color, bool hasSidecar)
    {
        // Arrange
        Motorcycle motorcycle = new Motorcycle(
            make, model, regNumber, year, seatCount, color, hasSidecar);
        string toString = $"Motorcykel (Storlek: 1)\n" + $"Märke: {make}\nModell: {model} ({year})\n" +
                $"Reg-nummer: {regNumber}, Sittplatser: {seatCount}, Färg: {color}" +
                $"\nSidovagn?: {(hasSidecar ? "Ja" : "Nej")}";

        // Act
        string result = motorcycle.ToString();

        // Assert
        Assert.Equal(toString, result);
    }

    [Theory]
    [InlineData("Air", "Plane", "HOI394", 2021, 1, "röd", 10000, 2)]
    public void Airplane_ToString_ShouldBeCorrect(string make, string model,
        string regNumber, int year, int seatCount, string color, int maxAltitude, int engineCount)
    {
        // Arrange
        Airplane airplane = new Airplane(
            make, model, regNumber, year, seatCount, color, maxAltitude, engineCount);
        string toString = $"Flygplan (Storlek: 5)\n" + $"Märke: {make}\nModell: {model} ({year})\n" +
                $"Reg-nummer: {regNumber}, Sittplatser: {seatCount}, Färg: {color}" +
                $"\nMaxhöjd: {maxAltitude}\nAntal motorer: {engineCount}";

        // Act
        string result = airplane.ToString();

        // Assert
        Assert.Equal(toString, result);
    }

    [Theory]
    [InlineData("Båt", "Speeder", "JFI462", 2021, 1, "grön", PropulsionType.Rowing)]
    public void Boat_ToString_ShouldBeCorrect(string make, string model,
        string regNumber, int year, int seatCount, string color, PropulsionType type)
    {
        // Arrange
        Boat boat = new Boat(
            make, model, regNumber, year, seatCount, color, type);
        string toString = $"Båt (Storlek: 4)\n" + $"Märke: {make}\nModell: {model} ({year})\n" +
                $"Reg-nummer: {regNumber}, Sittplatser: {seatCount}, Färg: {color}" +
                $"\nDrivmedel: '{type}'";

        // Act
        string result = boat.ToString();

        // Assert
        Assert.Equal(toString, result);
    }

    [Theory]
    [InlineData("Buss", "Volvo", "BUS123", 2021, 50, "gul", EngineType.Diesel)]
    public void Buss_ToString_ShouldBeCorrect(string make, string model,
        string regNumber, int year, int seatCount, string color, EngineType type)
    {
        // Arrange
        Bus buss = new Bus(
            make, model, regNumber, year, seatCount, color, type);
        string toString = $"Buss (Storlek: 8)\n" + $"Märke: {make}\nModell: {model} ({year})\n" +
                $"Reg-nummer: {regNumber}, Sittplatser: {seatCount}, Färg: {color}" +
                $"\nMotortyp: '{type}'";

        // Act
        string result = buss.ToString();

        // Assert
        Assert.Equal(toString, result);
    }
}
