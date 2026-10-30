using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp3p0.Model.Vehicles
{
    public class Airplane : Vehicle
    {
        public Airplane(string make, string model, string regNumber, int year, string color)
            : base(make, model, regNumber, year, color)
        {

        }
    }
}
