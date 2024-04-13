using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Dto.Airtype_Owns
{
    public class CabinRuleDto
    {
        public int Id { get; set; }

        public int AirCabinId { get; set; }

        public string CabinCode { get; set; }

        public int Priority { get; set; }

        public int ShipmentWeight { get; set; }

        public int ShipmentCount { get; set; }

        public int CarryBagCount { get; set; }

        public int CarryBagWeight { get; set; }

        public bool PreSelectedSeat { get; set; }

        public double AccumulatedMile { get; set; }

        public bool MileUpgrade { get; set; }

        public int TicketVaildity { get; set; }

        public int RefundFee { get; set; }

        public int NoShowFee { get; set; }

        public bool FreeWifi { get; set; }
    }
}
