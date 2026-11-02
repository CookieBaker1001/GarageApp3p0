using GarageApp3p0.Model.Vehicles;
using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp3p0.Application
{
    public static class FilterHelperClass
    {
        public static List<Vehicle> FilterMake(List<Vehicle> list)
        {
            string brand = GetMake();
            var newList = list.Where(v => v.Make == brand).ToList();
            return newList;
        }

        private static string GetMake()
        {
            Console.WriteLine("Ange märke att filtrera på.");
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
            Console.WriteLine("Ange model att filtrera på.");
            string? model;
            while (string.IsNullOrWhiteSpace(model = Console.ReadLine()))
            {
                Console.WriteLine("Ogiltig model. Försök igen.");
            }
            return model;
        }
    }
}
