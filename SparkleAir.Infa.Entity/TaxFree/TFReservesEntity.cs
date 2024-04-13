using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Entity.TaxFree
{
    public class TFReservesEntity
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public string MemberChineseFirstName { get; set; }

        public string MemberChineseLastName { get; set; }

        public string MemberEnglishLastName { get; set; }

        public string MemberEnglishFirstName { get; set; }

        public string MemberPhone { get; set; }

        public string MemberEmail { get; set; }

        public string MemberPassportNumber { get; set; }

        public int? Discount { get; set; }

        public int TotalPrice { get; set; }

        public int TransferPaymentId { get; set; }
        public DateTime PaymentDate { get; set; }

        public decimal PaymentAmount { get; set; }

        public string? Info { get; set; }

        public int? TforderStatusId { get; set; }

        public int TicketDetailsId { get; set; }

        public int TicketId { get; set; }

        public int AirCabinRuleId { get; set; }

        public int TypeofPassengerId { get; set; }

        public decimal TicketAmount { get; set; }

        public decimal AccruedMile { get; set; }

        public string? BookRef { get; set; }

        
    }
}
