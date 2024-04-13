using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Entity.AirFlightsEntity
{
    public class AirTakeOffEntity
    {
        public int Id { get; set; }

        public int AirFlightId { get; set; }

        public DateTime ActualDepartureTime { get; set; }

        public DateTime ActualArrivalTime { get; set; }

        public int AirFlightStatusId { get; set; }

        public string FlightCode { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public int DepartureTimeZone { get; set; }
        public int ArrivalTimeZone { get; set; }
        public string FlightModel { get; set; }
        public string DepartureTerminal { get; set; }
        public string ArrivalTerminal { get; set; }
        public string DepartureAirportCity { get; set; }
        public string ArrivalAirportCity { get; set; }
        public DateTime OriginalDepartureTime { get; set; }
        public DateTime OriginalArrivalTime { get; set; }
        public string AirFlightStatus { get; set; }
    }
}
