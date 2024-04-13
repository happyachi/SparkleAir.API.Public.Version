using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Criteria.AirFlights
{
    public class AirFlightManagementSearchCriteria
    {
        public int Id { get; set; }
        public string FlightCode { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public TimeSpan DepartureStartTime { get; set; }
        public TimeSpan DepartureEndTime { get; set; }
        public TimeSpan ArrivalStartTime { get; set; }
        public TimeSpan ArrivalEndTime { get; set; }
        public FlightDate FlightDays { get; set; }
    }
    public enum FlightDate
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 0
    }
}
