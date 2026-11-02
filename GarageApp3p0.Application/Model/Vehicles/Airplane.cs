using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp3p0.Model.Vehicles
{
    public class Airplane : Vehicle
    {
        public override int DefaultParkingSize { get; } = 5;
        private int _maxHeight;
        public int MaxHeight { 
            get => _maxHeight;
            set { 
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Maxhöjd måste vara större än 0.");
                }
                _maxHeight = value;
            }
        }
        private int _engineCount;
        public int EngineCount { 
            get => _engineCount;
            set { 
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Antal motorer måste vara större än 0.");
                }
                _engineCount = value;
            } 
        }
        public Airplane(string make, string model, string regNumber, int year,
            int seatCount, int parkingSize, string color, int maxHeight, int engineCount) :
            base(make, model, regNumber, year, seatCount, parkingSize, color)
        {
            MaxHeight = maxHeight;
            EngineCount = engineCount;
        }

        public override string ToString()
        {
            return $"Flygplan (Storlek: {DefaultParkingSize})\n" + base.ToString() + 
                $"\nMaxhöjd: {MaxHeight}\nAntal motorer: {EngineCount}";
        }
    }
}
