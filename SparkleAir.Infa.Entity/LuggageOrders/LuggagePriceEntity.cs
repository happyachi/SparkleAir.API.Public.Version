using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Entity.LuggageOrders
{
    public class LuggagePriceEntity
    {
        public int Id { get; set; }

        public int AirFlightManagementsId { get; set; }

        public int OriginalPrice { get; set; }

        public int BookPrice { get; set; }

    }
}
