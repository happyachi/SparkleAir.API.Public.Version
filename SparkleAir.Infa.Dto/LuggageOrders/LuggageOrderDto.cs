using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Dto.LuggageOrders
{
    public class LuggageOrderDto
    {
        public int Id { get; set; }
        public string FlightCode { get; set; }

        public int TicketInvoicingDetailId { get; set; }
        public string TicketInvoicingDetailName { get; set; }

        public int LuggageId { get; set; }
        public int LuggagePrice { get; set; }

        public int Amount { get; set; }

        public int Price { get; set; }

        public DateTime OrderTime { get; set; }

        public int TransferPaymentsId { get; set; }

      
        public string OrderStatus { get; set; }

       
        public string LuggageNum { get; set; }

    }
}
