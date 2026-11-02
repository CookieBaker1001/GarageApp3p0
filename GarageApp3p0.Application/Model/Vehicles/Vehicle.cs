using GarageApp3p0.Application.Model;
using System.Collections;

namespace GarageApp3p0.Model.Vehicles
{
    public abstract class Vehicle
    {
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string RegNumber { get; set; } = string.Empty;
        private int _year;
        public int Year { 
            get { return _year; }
            set {
                if (value < 0 || value > DateTime.Now.Year)
                    throw new ArgumentOutOfRangeException("Ogiltigt årtal.");
                _year = value; 
            }
        }
        public int SeatCount { get; set; }
        // Bil = 3, Motorcykel = 1, Flygplan = 5, Båt = 4, Buss = 8
        public int ParkingSize { get; set; }
        public string Color { get; set; } = string.Empty;

        public Vehicle(string make, string model, string regNumber, int year, 
            int seatCount, int parkingSize, string color) { 
            Make = make;
            Model = model;
            RegNumber = regNumber;
            Year = year;
            SeatCount = seatCount;
            ParkingSize = parkingSize;
            Color = color;
        }

        public override string ToString() { 
            return $"Märke: {Make}\nModell: {Model} ({Year})\n" +
                $"Reg-nummer: {RegNumber}, Sittplatser: {SeatCount}, Färg: {Color}";
        }

        public string GetLowDetails()
        {
            return $"{Make} {Model} - {RegNumber}";
        }
    }
}
