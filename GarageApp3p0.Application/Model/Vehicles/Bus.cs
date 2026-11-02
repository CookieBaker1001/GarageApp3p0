using GarageApp3p0.Application.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp3p0.Model.Vehicles
{
    public class Bus : Vehicle
    {
        private EngineType _engineType;
        public EngineType EngineType { 
            get => _engineType;
            set {
                if (value > EngineType.Gasoline || value < EngineType.Electric) {
                    throw new ArgumentOutOfRangeException(nameof(value), "Ogiltig motortyp.");
                }
                _engineType = value;
            } 
        }
        public Bus(string make, string model, string regNumber, int year,
            int seatCount, int parkingSize, string color, EngineType engineType) :
            base(make, model, regNumber, year, seatCount, parkingSize, color)
        {
            EngineType = engineType;
        }

        public override string ToString()
        {
            return "Buss\n" + base.ToString() +
                $"\nMotortyp: '{EngineType}'";
        }
    }
}
