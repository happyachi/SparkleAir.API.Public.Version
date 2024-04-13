using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Dto.MileOrder
{
    public class MileOrderDto
    {
        public int? Id { get; set; }

        public int Amount { get; set; }

        public int Price { get; set; }

        public DateTime? OrderTime { get; set; }

        public int TransferPaymentId { get; set; }

        public string? OrderStatus { get; set; }

        public string? MileNum { get; set; }

        public int MemberId { get; set; }
    }
}
