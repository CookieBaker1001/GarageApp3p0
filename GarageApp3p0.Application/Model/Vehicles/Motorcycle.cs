using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp3p0.Model.Vehicles
{
    public class Motorcycle : Vehicle
    {
        public override int DefaultParkingSize { get; } = 1;
        public bool HasSidecar { get; set; }
        public Motorcycle(string make, string model, string regNumber, int year,
            int seatCount, int parkingSize, string color, bool hasSidecar) :
            base(make, model, regNumber, year, seatCount, parkingSize, color)
        {
            HasSidecar = hasSidecar;
        }

        public override string ToString()
        {
            return $"Motorcykel (Storlek: {DefaultParkingSize})\n" + base.ToString() +
                $"\nSido-vagn: {(HasSidecar ? "Ja" : "Nej")}";
        }
    }
}
