using GarageApp3p0.Application.HandlerClasses;
using GarageApp3p0.Model;
using GarageApp3p0.Model.Vehicles;
using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp3p0.Application.HelperClasses
{
    public static class AdvacedSearchHandler
    {
        public static void PerformAdvancedSearch(Garage<Vehicle> garage)
        {
            bool exit = false;
            string? input;
            List<Vehicle> filteredVehicles = garage.CopyContent();
            int previousCount;
            while (!exit)
            {
                previousCount = filteredVehicles.Count;
                PromptHandler.PromptUserAdvancedSearchMenu();
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Inmatningsfel. Försök igen.");
                    continue;
                }

                switch (input.ToLower())
                {
                    case "1":
                        // Sort by make
                        filteredVehicles = FilterHandler.FilterMake(filteredVehicles);
                        break;
                    case "2":
                        // Sort by model
                        filteredVehicles = FilterHandler.FilterModel(filteredVehicles);
                        break;
                    case "3":
                        // Search by regnumber
                        filteredVehicles = FilterHandler.FilterRegNumber(filteredVehicles);
                        break;
                    case "4":
                        // Sort by year
                        filteredVehicles = FilterHandler.FilterYear(filteredVehicles);
                        break;
                    case "5":
                        // Sort by color
                        filteredVehicles = FilterHandler.FilterColor(filteredVehicles);
                        break;
                    case "6":
                        // View the list
                        Utilities.PrintGarageContent(filteredVehicles.GetEnumerator(), true, true);
                        break;
                    case "7":
                        // Done with advanced search, return to main menu
                        exit = true;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        Console.ResetColor();
                        break;
                }
                Console.WriteLine($"{previousCount - filteredVehicles.Count} fordon filtrerades bort.");
                if (filteredVehicles.Count == 0) exit = true;
            }

            if (filteredVehicles.Count == 0)
            {
                Console.WriteLine("Inga fordon matchade dina filterkriterier. " +
                    "Återgår till huvudmenyn.");
            }
            else
            {
                Console.WriteLine("Dessa fordon matchade dina filterkriterier:");
                Utilities.PrintGarageContent(filteredVehicles.GetEnumerator(), true, true);
            }
        }
    }
}
