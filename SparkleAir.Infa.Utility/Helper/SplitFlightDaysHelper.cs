using SparkleAir.Infa.Criteria.AirFlights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Utility.Helper
{
    public static class SplitFlightDaysHelper
    {
        public static FlightDate[] FlightDays(this string flightDays)
        {
            var dayNumbers = flightDays.Split(',').Select(int.Parse);

            // 數字轉換 FlightDate 陣列
            var daysArray = dayNumbers.Select(number => (FlightDate)number).ToArray();

            return daysArray;
        }

        public static string dayofweek(this string days)
        {
            var dayArr = days.Split(',').Select(d => int.Parse(d).ToString());
            var day = dayArr.Select(d => d == "0" ? "7" : d);
            return string.Join(",", day);
        }
    }
}
