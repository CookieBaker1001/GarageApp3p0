using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp3p0.Model.Vehicles
{
    public abstract class Vehicle
    {
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string RegNumber { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Color { get; set; } = string.Empty;

        public Vehicle(string make, string model, string regNumber, int year, string color) { 
            Make = make;
            Model = model;
            RegNumber = regNumber;
            Year = year;
            Color = color;
        }

        public override string ToString() { 
            return $"Make: {Make}, Model: {Model}, RegNumber: " +
                $"{RegNumber}, Year: {Year}, Color: {Color}";
        }
    }
}
