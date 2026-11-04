using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace GarageApp3p0.Application.HelperClasses
{
    // Hjälpklass för validering av registreringsnummer med hjälp av Regex
    public static class RegexHandler
    {
        private static Regex regNumberRegex = new Regex(@"[A-Z]{3}\d{3}\Z");
        // Hjälpmetod för validering av registreringsnummer
        public static bool IsValidRegistrationNumber(string regNumber)
        {
            regNumber = regNumber.ToUpper();
            return regNumberRegex.IsMatch(regNumber);
        }
    }
}
