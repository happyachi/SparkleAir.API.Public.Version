using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Dto.Tickets
{
    public class TicketInfoDto
    {
        //tickets
        public int? MemberId { get; set; }
        public int? AirFlightId { get; set; }
        public string? OrderNum { get; set; }
        public decimal? TotalPayableAmount { get; set; }
        public decimal? ActualPaidAmount { get; set; } 
        public DateTime? OrderTime { get; set; }
        public int? TransferPaymentId { get; set; }
        public bool? IsEstablished { get; set; }
        public int? MileRedemption { get; set; }
        public int? TotalGeneratedMile { get; set; }
        public bool? IsInvoiced { get; set; }

        //ticketDetail
        //ticketId 
        public string? AirCabinRuleId { get; set; }
        public int? TypeofPassengerId { get; set; }
        public decimal? TicketAmount { get; set; }
        public int? AccruedMile { get; set; }
        public string? BookRef { get; set; }

        //ticketInvocing
        //public int TicketDetailId { get; set; }
        public string? AirFlightSeatId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public int? CountryId { get; set; }
        public DateTime? DateofBirth { get; set; }
        public bool? Gender { get; set; }
        public string? PassportNum { get; set; }
        public DateTime? PassportExpirationDate { get; set; }
        public string? Remark { get; set; }
        public string? TicketNum { get; set; }
        public int? AirMealId { get; set; }
        public bool? CheckInStatus { get; set; }
        public DateTime? CheckInTime { get; set; }

        //mileageDetail
        //memberId
        public int? TotalMile { get; set; }
        public int? OriginalMile { get; set; }
        public int? ChangeMile { get; set; }
        public int? FinalMile { get; set; }
        public DateTime? MileValidity { get; set; }
        public string? MileReason { get; set; }
        public string? MileDetailOrderNumber { get; set; }
        public DateTime? MileChangeTime { get; set; }
    }
}
