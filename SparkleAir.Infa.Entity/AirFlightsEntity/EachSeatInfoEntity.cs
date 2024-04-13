using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Entity.AirFlightsEntity
{
    public class EachSeatInfoEntity
    {
        public int Id { get; set; }
        public int AirFlightSeatId { get; set; }
        public int TicketInvoicingDetailId { get; set; }
        public DateTime ReservationTime { get; set; }
        public int? TransferPaymentId { get; set; }
        public decimal? HandlingFee { get; set; }
        public string SeatAssignmentNum { get; set; }

        public string SeatNum { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public string CheckInstatus { get; set; }
        public string TypeofPassenger { get; set; }
    }
}
