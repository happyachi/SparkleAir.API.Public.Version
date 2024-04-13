using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Dto.AirFlights
{
    public class AirFlightManagementDto
    {
        public int Id { get; set; }

        public string FlightCode { get; set; }

        public int DepartureAirportId { get; set; }

        public int ArrivalAirportId { get; set; }

        public string DepartureTerminal { get; set; }

        public string ArrivalTerminal { get; set; }

        public TimeSpan DepartureTime { get; set; }

        public TimeSpan ArrivalTime { get; set; }

        public string DayofWeek { get; set; }

        public int Mile { get; set; }

        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public int DepartureTimeZone { get; set; }
        public int ArrivalTimeZone { get; set; }

        public int? AirOwnId { get; set; }
        public string FlightModel { get; set; }
        public int CrossDay { get; set; }

        public string RegistrationNum { get; set; }
    }
}
