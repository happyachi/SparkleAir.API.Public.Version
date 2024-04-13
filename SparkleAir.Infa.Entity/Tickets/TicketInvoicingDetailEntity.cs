using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Entity.Tickets
{
    public class TicketInvoicingDetailEntity
    {
        public int Id { get; set; }

        public int TicketDetailId { get; set; }

        public string AirFlightSeatId { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public int CountryId { get; set; }

        public DateTime DateofBirth { get; set; }

        public bool Gender { get; set; }

        public string PassportNum { get; set; }

        public DateTime PassportExpirationDate { get; set; }

        public string Remark { get; set; }

        public string TicketNum { get; set; }

        public int AirMealId { get; set; }

        public bool CheckInStatus { get; set; }

        public DateTime? CheckInTime { get; set; }
    }
}
