using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp3p0.Model.Vehicles
{
    internal abstract class Vehicle
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string RegNumber { get; set; }
        public override string ToString() { 
            return $"Make: {Make}, Model: {Model}, RegNumber: {RegNumber}";
        }
    }
}
