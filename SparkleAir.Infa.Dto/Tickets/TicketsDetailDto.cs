using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Dto.Tickets
{
    public class TicketsDetailDto
    {
        public int Id { get; set; }

        public int TicketId { get; set; }

        public string AirCabinRuleId { get; set; }

        public int TypeofPassengerId { get; set; }

        public decimal TicketAmount { get; set; }

        public int AccruedMile { get; set; }

        public string BookRef { get; set; }
    }
}
