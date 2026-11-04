using GarageApp3p0.Application.HelperClasses;
using GarageApp3p0.Model;
using GarageApp3p0.Model.Vehicles;
using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp3p0.Application.HandlerClasses
{
    public static class MainMenuHandler
    {
        public static void RunMainMenu()
        {
            Garage<Vehicle> garage = Utilities.StartupSequence();

            bool exit = false;
            string? input;
            int choice;

            while (!exit)
            {
                PromptHandler.PromptUserMainMenu();
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)
                    || !int.TryParse(input, out choice))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Inmatningsfel. Försök igen.");
                    Console.ResetColor();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        TryToAddVehicle(garage);
                        break;
                    case 2:
                        TryToRemoveVehicle(garage);
                        break;
                    case 3:
                        TryToShowVehicles(garage, false);
                        break;
                    case 4:
                        TryToShowVehicles(garage, true);
                        break;
                    case 5:
                        TryToShowVehicleTypes(garage);
                        break;
                    case 6:
                        TryToPerformAdvancedSearch(garage);
                        break;
                    default:
                        exit = true;
                        break;
                }
            }

            if (!garage.IsEmpty()) Utilities.SaveGarage(garage);
        }

        private static void TryToShowVehicleTypes(Garage<Vehicle> garage)
        {
            try
            {
                Utilities.PrintVehicleTypes(garage);
            }
            catch (InvalidOperationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Ogiltig operation: {ex.Message}");
                Console.ResetColor();
            }
            catch (KeyNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Nyckelfel: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Generellt fel: {ex.Message}");
                Console.ResetColor();
            }
        }

        private static void TryToPerformAdvancedSearch(Garage<Vehicle> garage)
        {
            try
            {
                AdvacedSearchHandler.PerformAdvancedSearch(garage);
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

        private static void TryToShowVehicles(Garage<Vehicle> garage, bool showLowDetail)
        {
            try
            {
                Utilities.PrintGarage(garage, showLowDetail, true);
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
