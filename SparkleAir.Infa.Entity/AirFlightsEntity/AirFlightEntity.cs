using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Entity.AirFlightsEntity
{
    public class AirFlightEntity
    {
        public int Id { get; set; }
        public int AirOwnId { get; set; }
        public int AirFlightManagementId { get; set; }
        public DateTime ScheduledDeparture { get; set; }
        public DateTime ScheduledArrival { get; set; }
        public int AirFlightSaleStatusId { get; set; }

        public string FlightModel { get; set; }
        public string FlightCode { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public string AirFlightSaleStatus { get; set; }
        public string DayofWeek { get; set; }

        public int DepartureTimeZone { get; set; }
        public int ArrivalTimeZone { get; set; }
        public int DepartureAirportId { get; set; }
        public int ArrivalAirportId { get; set; }

        public string RegistrationNum { get; set; }
        public int CrossDay { get; set; }
        public int Mile { get; set; }

        public string DepartureTerminal { get; set; }
        public string ArrivalTerminal { get; set; }
    }
}
