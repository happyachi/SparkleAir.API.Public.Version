using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Dto.TaxFree
{
    public class TFReservelistsDto
    {
        public int Id { get; set; }

        public int TFItemsId { get; set; }

        public string TFItemsName { get; set; }

        public string TFItemsSerialNumber { get; set; }

        public string TFItemsImage { get; set; }

        public int TFItemsQuantity { get; set; }

        public int TFItemsUnitPrice { get; set; }

        public int Quantity { get; set; }

        public int UnitPrice { get; set; }

        public int? Discount { get; set; }

        public int TotalPrice { get; set; }

        public int TFReserveId { get; set; }

        public int MemberId { get; set; }

        public string MemberChineseFirstName { get; set; }

        public string MemberChineseLastName { get; set; }

        public string MemberEnglishLastName { get; set; }

        public string MemberEnglishFirstName { get; set; }

        public string MemberPhone { get; set; }

        public string MemberEmail { get; set; }

        public string MemberPassportNumber { get; set; }

        public int TicketId { get; set; }

        public int TicketDetailsId { get; set; }

        public int AirCabinRuleId { get; set; }

        public int TypeofPassengerId { get; set; }

        public decimal TicketAmount { get; set; }

        public int AccruedMile { get; set; }

        public int TransferPaymentId { get; set; }

        public DateTime PaymentDate { get; set; }

        public decimal PaymentAmount { get; set; }

        public string? Info { get; set; }

        public string? TicketDetailFirstName { get; set; }

        public string? TicketDetailLastName { get; set; }

        public string? TicketDetailBookRef { get; set; }

        public string? TicketDetailFlightCode { get; set; }

        public DateTime? TicketDetailScheduledDepartureTime { get; set; }
    }
}
