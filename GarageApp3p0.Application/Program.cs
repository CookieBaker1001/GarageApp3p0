using GarageApp3p0.Application.Data;
using GarageApp3p0.Model;
using GarageApp3p0.Model.Vehicles;
using System.Diagnostics.Metrics;

namespace GarageApp3p0.Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Garage<Vehicle> garage = Utilities.StartupSequence();

            bool exit = false;
            string? input;
            int choice;

            while (!exit) {
                Utilities.PrintMainMenu();
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input) 
                    || !int.TryParse(input, out choice)) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Inmatningsfel. Försök igen.");
                    Console.ResetColor();
                    continue;
                }

                switch (choice) { 
                    case 1:
                        TryToAddVehicle(garage);
                        break;
                    case 2:
                        TryToRemoveVehicle(garage);
                        break;
                    case 3:
                        TryToShowVehicles(garage);
                        break;
                    default:
                        exit = true;
                        break;
                }
            }

            if (!garage.IsEmpty()) Utilities.SaveGarage(garage);

            Console.WriteLine("Tack för att du använde garaget!");
            Console.WriteLine("Stänger ner programmet...");
        }

        private static void TryToRemoveVehicle(Garage<Vehicle> garage) 
        {
            try
            {
                Utilities.RemoveVehicle(garage);
            }
            catch (Exception ex) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Generellt fel: {ex.Message}");
                Console.ResetColor();
            }
        }

        private static void TryToAddVehicle(Garage<Vehicle> garage)
        {
            try
            {
                Vehicle vehicle = Utilities.CreateVehicle(garage);
                garage.AddVehicle(vehicle);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Fordonet {vehicle.RegNumber} har lagts till i garaget.");
                Console.ResetColor();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Argumentintervalfel: {ex.Message}");
                Console.ResetColor();
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Argumentfel: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Generellt fel: {ex.Message}");
                Console.ResetColor();
            }
        }

        private static void TryToShowVehicles(Garage<Vehicle> garage)
        {
            try
            {
                Utilities.PrintGarage(garage, false, true);
            }
            catch (InvalidOperationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Ogiltig operation: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Generellt fel: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
