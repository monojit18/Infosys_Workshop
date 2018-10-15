using System;

namespace SampleNuget.Subsystems
{
    public class DateCalculator
    {

        public static string GetDateTime()
        {

            var currentDateTime = DateTime.UtcNow;
            var dateString = currentDateTime.ToString("dddd, dd MMMM yyyy");
            return dateString;

        }
    }
}
