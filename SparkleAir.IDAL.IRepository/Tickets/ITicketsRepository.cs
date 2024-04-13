using SparkleAir.Infa.Entity.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.IDAL.IRepository.Tickets
{
    public interface ITicketsRepository
    {
        Task<int> CreateTicket(TicketsEntity ticketEntity);
        Task<(int,string)> CreateTicketDetail(TicketsDetailEntity ticketDetailEntity);
        Task <int> CreateTicketInvoce(TicketInvoicingDetailEntity ticketInvoicingDetailEntity,int airFlightId);
        TicketInvoicingDetailEntity GetTicketInvoicingDetail(string ticketNum, string firstName, string lastName);
        TicketInvoicingDetailEntity GetTicketByMemberId(int memberId);

    }
}
