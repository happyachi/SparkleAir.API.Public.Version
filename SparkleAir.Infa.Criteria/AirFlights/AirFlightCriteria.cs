using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Criteria.AirFlights
{
    public class AirFlightCriteria
    {
        public string? FlightCode { get; set; }
        public string? DepartureAirport { get; set; }
        public string? ArrivalAirport { get; set; }
        public DateTime DepartureStartTime { get; set; }
        public DateTime DepartureEndTime { get; set; }
        public DateTime ArrivalStartTime { get; set; }
        public DateTime ArrivalEndTime { get; set; }
    }
}
