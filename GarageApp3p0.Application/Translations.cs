using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp3p0.Application
{
    public static class Translations
    {
        public static Dictionary<string, string> VehicleTypes = new Dictionary<string, string>() 
            {
                {"Car", "Bil"},
                {"Motorcycle", "Motorcykel"},
                {"Airplane", "Flygplan"},
                {"Boat", "Båt"},
                {"Bus", "Buss"}
            };
    }
}
