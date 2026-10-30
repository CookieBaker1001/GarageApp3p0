using GarageApp3p0.Model;
using GarageApp3p0.Model.Vehicles;

namespace GarageApp3p0.Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Garage<Vehicle> garage = Utilities.WelcomeSequennce();

            Utilities.PrintMainMenu();
        }
    }
}
