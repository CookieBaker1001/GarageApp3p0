using GarageApp3p0.Application.HandlerClasses;
using GarageApp3p0.Model;
using GarageApp3p0.Model.Vehicles;
using System.Diagnostics.Metrics;

namespace GarageApp3p0.Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainMenuHandler.RunMainMenu();

            Console.WriteLine("Tack för att du använde garaget!");
            Console.WriteLine("Stänger ner programmet...");
        }
    }
}
