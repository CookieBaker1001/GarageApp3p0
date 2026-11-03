using GarageApp3p0.Application.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp3p0.Model.Vehicles
{
    public class Boat : Vehicle
    {
        private PropulsionType _propulsionType;
        public PropulsionType PropulsionType { 
            get => _propulsionType; 
            set { 
                if (value < PropulsionType.Rowing || value > PropulsionType.Engine)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Felaktig drivmedelstyp.");
                }
                _propulsionType = value;
            } 
        }
        private int _parkingSize = 4;
        public override int ParkingSize => _parkingSize;
        public Boat() { }
        public Boat(string make, string model, string regNumber, int year,
            int seatCount, string color, PropulsionType propulsionType) :
            base(make, model, regNumber, year, seatCount, 4, color)
        {
            PropulsionType = propulsionType;
        }

        public override string ToString()
        {
            return $"Båt (Storlek: {ParkingSize})\n" + base.ToString() +
                $"\nDrivmedel: '{PropulsionType}'";
        }
    }
}
