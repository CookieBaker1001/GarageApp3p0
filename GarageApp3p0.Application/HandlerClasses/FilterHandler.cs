using GarageApp3p0.Application.HandlerClasses;
using GarageApp3p0.Model;
using GarageApp3p0.Model.Vehicles;
using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp3p0.Application.HelperClasses
{
    // Hjälpklass för att filtrera listor av fordon baserat på fordonens
    // egenskaper som märke, modell, registreringsnummer, årtal och färg.
    public static class FilterHandler
    {
        public static List<Vehicle> FilterMake(List<Vehicle> list)
        {
            string brand = GetMake();
            var newList = list.Where(v => v.Make == brand).ToList();
            return newList;
        }

        private static string GetMake()
        {
            PromptHandler.AskUser("Ange märke att filtrera på.");
            string? brand;
            while (string.IsNullOrWhiteSpace(brand = Console.ReadLine()))
            {
                Console.WriteLine("Ogiltigt märke. Försök igen.");
            }
            return brand;
        }

        public static List<Vehicle> FilterModel(List<Vehicle> list)
        {
            string model = GetModel();
            var newList = list.Where(v => v.Model == model).ToList();
            return newList;
        }

        private static string GetModel()
        {
            PromptHandler.AskUser("Ange modell att filtrera på.");
            string? model;
            while (string.IsNullOrWhiteSpace(model = Console.ReadLine()))
            {
                Console.WriteLine("Ogiltig model. Försök igen.");
            }
            return model;
        }

        private static string GetRegNumber()
        {
            PromptHandler.AskUser("Ange registreringsnummer att filtrera på.");
            string? regNumber;
            while (string.IsNullOrWhiteSpace(regNumber = Console.ReadLine())
                || !RegexHandler.IsValidRegistrationNumber(regNumber))
            {
                Console.WriteLine("Ogiltigt registreringsnummer. Försök igen.");
            }
            return regNumber.ToUpper();
        }

        public static List<Vehicle> FilterYear(List<Vehicle> list)
        {
            int year = GetYear();
            var newList = list.Where(v => v.Year == year).ToList();
            return newList;
        }

        private static int GetYear()
        {
            PromptHandler.AskUser("Ange årtal att filtrera på.");
            int year;
            while (!int.TryParse(Console.ReadLine(), out year))
            {
                Console.WriteLine("Ogiltigt årtal. Försök igen.");
            }
            return year;
        }

        public static List<Vehicle> FilterColor(List<Vehicle> list)
        {
            string color = GetColor();
            var newList = list.Where(v => v.Color == color).ToList();
            return newList;
        }

        private static string GetColor()
        {
            PromptHandler.AskUser("Ange färg att filtrera på.");
            string? color;
            while (string.IsNullOrWhiteSpace(color = Console.ReadLine()))
            {
                Console.WriteLine("Ogiltig färg. Försök igen.");
            }
            return color;
        }

        public static List<Vehicle> FilterRegNumber(List<Vehicle> list)
        {
            string regNumber = GetRegNumber();
            var newList = list.Where(v => v.RegNumber == regNumber).ToList();
            return newList;
        }

        
    }
}
