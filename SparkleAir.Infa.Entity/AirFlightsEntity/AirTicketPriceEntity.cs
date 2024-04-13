using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Entity.AirFlightsEntity
{
    public class AirTicketPriceEntity
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int AirCabinRuleId { get; set; }

        public string CabinName { get; set; }
        public string CabinCode { get; set; }
    }
}
