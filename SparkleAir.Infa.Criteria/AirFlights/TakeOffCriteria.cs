using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Criteria.AirFlights
{
    public class TakeOffCriteria
    {
        public string? FlightCode { get; set; }
        public string? DepartureAirport { get; set; }
        public string? ArrivalAirport { get; set; }
    }
}
