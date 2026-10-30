using GarageApp3p0.Model.Vehicles;

namespace GarageApp3p0.Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Car car = new Car("Toyota", "Corolla", "ABC123", 2020, "Blue");
            Console.WriteLine(car);
        }
    }
}
