using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace GarageApp3p0.Application.HelperClasses
{
    public static class RegexHelper
    {
        private static Regex regNumberRegex = new Regex(@"[A-Z]{3}\d{3}\Z");
        public static bool IsValidRegistrationNumber(string regNumber)
        {
            regNumber = regNumber.ToUpper();
            return regNumberRegex.IsMatch(regNumber);
        }
    }
}
