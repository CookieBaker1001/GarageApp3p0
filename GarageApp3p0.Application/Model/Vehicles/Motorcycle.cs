using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp3p0.Model.Vehicles
{
    public class Motorcycle : Vehicle
    {
        public bool HasSidecar { get; set; }
        private int _parkingSize = 1;
        public override int ParkingSize => _parkingSize;
        public Motorcycle() { }

        public Motorcycle(string make, string model, string regNumber, int year,
            int seatCount, string color, bool hasSidecar) :
            base(make, model, regNumber, year, seatCount, 1, color)
        {
            HasSidecar = hasSidecar;
        }

        public override string ToString()
        {
            return $"Motorcykel (Storlek: {ParkingSize})\n" + base.ToString() +
                $"\nSidovagn?: {(HasSidecar ? "Ja" : "Nej")}";
        }
    }
}
