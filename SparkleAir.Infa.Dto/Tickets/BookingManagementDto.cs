using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Dto.Tickets
{
    public class BookingManagementDto
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string BookRef { get; set; }

        public string FlightCode { get; set; }

        public DateTime ScheduledDeparture { get; set; }

        public DateTime ScheduledArrival { get; set; }

        public string DepartureAirportCity { get; set; }
        public string ArrivalAirportCity { get; set; }
        public string DepartureAirportName { get; set; }
        public string ArrivalAirportName { get; set; }

        public string AirFlightStatuses { get; set; }

    }
}
