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
        Assert.Equal(capacity, garage.MaxCapacity);
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

    [Fact]
    public void Garage_ShouldOrShouldNotFitVehicle()
    {
        // Arrange
        Garage<Vehicle> garage = new Garage<Vehicle>(3);
        Motorcycle motorcycle = new Motorcycle() { };
        Car car = new Car() { };
        Boat boat = new Boat() { };

        // Act
        bool m = garage.DoesThisVehicleFit(motorcycle);
        bool c = garage.DoesThisVehicleFit(car);
        bool b = garage.DoesThisVehicleFit(boat);

        // Assert
        Assert.True(m);
        Assert.True(c);
        Assert.False(b);
    }

    [Fact]
    public void Garage_ShouldAddVehicleCorrectly()
    {
        // Arrange
        Garage<Vehicle> garage1 = new Garage<Vehicle>(1);
        Garage<Vehicle> garage2 = new Garage<Vehicle>(5);
        Motorcycle motorcycle = new Motorcycle() { };
        Car car = new Car() { };
        Airplane airplane = new Airplane() { };
        Bus bus = new Bus() { };

        // Act
        garage1.AddVehicle(motorcycle);
        garage2.AddVehicle(car);

        // Assert
        Assert.Throws<InvalidOperationException>(() => garage1.AddVehicle(motorcycle));
        Assert.Throws<InvalidOperationException>(() => garage1.AddVehicle(car));
        Assert.Throws<InvalidOperationException>(() => garage2.AddVehicle(airplane));
        Assert.Throws<InvalidOperationException>(() => garage2.AddVehicle(bus));
    }

    [Fact]
    public void Garage_RemovesVehiclesCorrectly()
    {
        // Arrange
        Garage<Vehicle> garage = new Garage<Vehicle>(5);
        Motorcycle motorcycle = new Motorcycle() { };
        Car car = new Car() { };

        // Act
        garage.AddVehicle(motorcycle);
        garage.AddVehicle(car);
        garage.RemoveVehicle(0);

        // Assert & Act
        Assert.Throws<ArgumentOutOfRangeException>(() => garage.RemoveVehicle(5));

        // Assert
        Assert.Equal(3, garage.CurrentCapacity);

        // Act
        garage.RemoveVehicle(0);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => garage.RemoveVehicle(0));
    }

    [Fact]
    public void Garage_RegNumberExistsOrNot() {
        // Arrange
        Garage<Vehicle> garage = new Garage<Vehicle>(6);
        Car car1 = new Car() { RegNumber = "ABC123" };
        Car car2 = new Car() { RegNumber = "DEF456" };

        // Act
        garage.AddVehicle(car1);
        garage.AddVehicle(car2);

        // Assert
        Assert.True(garage.DoesThisRegNumberAlreadyExist("ABC123"));
        Assert.True(garage.DoesThisRegNumberAlreadyExist("DEF456"));
        Assert.False(garage.DoesThisRegNumberAlreadyExist("GHI789"));
    }

    [Fact]
    public void Garage_IsFullOrEmpty()
    {
        // Arrange
        Garage<Vehicle> garage = new Garage<Vehicle>(6);
        Car car1 = new Car() { RegNumber = "ABC123" };
        Car car2 = new Car() { RegNumber = "DEF456" };

        // Act
        garage.AddVehicle(car1);
        garage.AddVehicle(car2);

        // Assert
        Assert.True(garage.IsFull());
        Assert.False(garage.IsEmpty());

        // Act
        garage.RemoveVehicle(1);

        // Assert
        Assert.False(garage.IsFull());
        Assert.False(garage.IsEmpty());

        // Act
        garage.RemoveVehicle(0);

        // Assert
        Assert.False(garage.IsFull());
        Assert.True(garage.IsEmpty());
    }

    [Fact]
    public void Garage_ShouldImportVehiclesCorrectly()
    {
        // Arrange
        Garage<Vehicle> garage = new Garage<Vehicle>(10);
        garage.AddVehicle(new Car() { });
        List<Vehicle> vehicles = new List<Vehicle>();
        Car car2 = new Car() { };
        Boat boat = new Boat() { };
        vehicles.Add(car2);
        vehicles.Add(boat);

        // Act
        garage.ImportVehicles(vehicles);

        // Assert
        Assert.Equal(10, garage.CurrentCapacity);
    }

    [Fact]
    public void Garage_ShouldCopyVehiclesCorrectly()
    {
        // Arrange
        Garage<Vehicle> garage = new Garage<Vehicle>(15);
        garage.AddVehicle(new Airplane() { });
        garage.AddVehicle(new Bus() { });
        List<Vehicle> vehicles;

        // Act
        vehicles = garage.CopyContent();

        // Assert
        Assert.Equal(2, vehicles.Count);
    }
}
