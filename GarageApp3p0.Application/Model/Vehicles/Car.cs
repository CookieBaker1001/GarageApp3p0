using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp3p0.Model.Vehicles
{
    public class Car : Vehicle
    {
        public override int DefaultParkingSize { get; } = 3;
        public Car(string make, string model, string regNumber, int year, 
            int seatCount, int parkingSize, string color) : 
            base(make, model, regNumber, year, seatCount, parkingSize, color)
        {
            
        }

        public override string ToString()
        {
            return $"Bil (Storlek: {DefaultParkingSize})\n" + base.ToString();
        }
    }
}
