using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp3p0.Application.HelperClasses
{
    public static class ColorHelper
    {
        private static HashSet<string> validColors = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "röd", "blå", "grön", "gul", "orange",
                "svart", "vit", "grå", "ljusgrå", "mörkgrå",
                "lila", "rosa", "brun", "turkos", "beige",
                "silver", "guld",
            };
        public static bool IsValidColor(string color)
        {
            return validColors.Contains(color.ToLower());
        }

        public static string[] GetValidColors()
        {
            return new List<string>(validColors).ToArray();
        }
    }
}
