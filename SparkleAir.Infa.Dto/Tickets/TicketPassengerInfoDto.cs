using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Dto.Tickets
{
    public class TicketPassengerInfoDto
    {
        public int TicketInvoicingDetailId { get; set; }

        public int TicketsDetailId { get; set; }
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string BookRef { get; set; }

        public string FlightCode { get; set; }

        public DateTime DepartureTime { get; set; }
        public string TicketNum { get; set; }
    }
}
