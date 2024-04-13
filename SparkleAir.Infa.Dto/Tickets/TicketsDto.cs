using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Dto.Tickets
{
    public class TicketsDto
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public int AirFlightId { get; set; }

        public string OrderNum { get; set; }

        public decimal TotalPayableAmount { get; set; }

        public decimal ActualPaidAmount { get; set; }

        public DateTime OrderTime { get; set; }

        public int TransferPaymentId { get; set; }

        public bool IsEstablished { get; set; }

        public int? MileRedemption { get; set; }

        public int TotalGeneratedMile { get; set; }

        public bool IsInvoiced { get; set; }
    }
}
