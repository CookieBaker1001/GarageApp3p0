using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp3p0.Model.Vehicles
{
    public class Car : Vehicle
    {
        public Car(string make, string model, string regNumber, 
            int year, int seatCount, string color) : 
            base(make, model, regNumber, year, seatCount, 3, color)
        {
            
        }

        public override string ToString()
        {
            return $"Bil (Storlek: {ParkingSize})\n" + base.ToString();
        }
    }
}
