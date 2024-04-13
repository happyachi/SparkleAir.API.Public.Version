using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Entity.TaxFree
{
    public class TFOrderStatusEntity
    {
        public int Id { get; set; }

        public DateTime? PaidTime { get; set; }

        public DateTime? FulfilledTime { get; set; }

        public DateTime? CancelledTime { get; set; }

        public DateTime? RefundTime { get; set; }

        public DateTime? StockedTime { get; set; }

        public bool? Paid { get; set; }

        public bool? Fulfilled { get; set; }

        public bool? Cancelled { get; set; }

        public bool? Refund { get; set; }

        public bool? Stocked { get; set; }

        public int? TFReserveId { get; set; }
    }
}
