using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp3p0.Application.HelperClasses
{
    // Hjälpklass för att hantera färger.
    public static class ColorHandler
    {
        private static HashSet<string> validColors = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "röd", "blå", "grön", "gul", "orange",
                "svart", "vit", "grå", "ljusgrå", "mörkgrå",
                "lila", "rosa", "brun", "turkos", "beige",
                "silver", "guld",
            };

        // Validerar om en färg sträng är giltig.
        public static bool IsValidColor(string color)
        {
            return validColors.Contains(color.ToLower());
        }

        // Returnerar en sträng med alla giltiga färger, uppdelade i rader
        // med ett angivet antal färger per rad.
        public static string GetValidColorsString(int rowCount)
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            for (int i = 0; i < validColors.Count; i++) { 
                sb.Append(validColors.ElementAt(i)).Append(", ");
                count++;
                if (count == rowCount) {
                    sb.AppendLine();
                    count = 0;
                }
            }
            return sb.ToString();
        }
    }
}
