using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Dto.TaxFree
{
    public class TFOrderlistsDto
    {
        public int Id { get; set; }

        public int TicketDetailsId { get; set; }

        //public int TicketId { get; set; }

        //public int  AirCabinRuleId { get; set; }

        //public int TypeofPassengerId { get; set; }

        //public int TicketAmount { get; set; }

        //public int AccuruedMile { get; set; }

        public int TFItemsId { get; set; }

        public string TFItemsName { get; set; }

        public string TFItemsSerialNumber { get; set; }

        public string TFItemsImage { get; set; }

        public int TFItemsQuantity { get; set; }

        public int TFItemsUnitPrice { get; set; }

        public int Quantity { get; set; }

        public int UnitPrice { get; set; }

        public int? TFOrderStatusID { get; set; }

        public bool? Paid { get; set; }
    }
}
