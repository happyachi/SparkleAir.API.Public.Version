using Microsoft.EntityFrameworkCore;
using SparkleAir.IDAL.IRepository.Tickets;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.DAL.EFRepository.Tickets
{
    public class TicketsEFRepository : ITicketsRepository
    {
        private AppDbContext db;
        public TicketsEFRepository(AppDbContext appDbContext)
        {
            db = appDbContext;
        }

        public async Task<int> CreateTicket(TicketsEntity entity)
        {
            Ticket ticket = new Ticket
            {
                MemberId = entity.MemberId,
                AirFlightId = entity.AirFlightId,
                OrderNum = entity.OrderNum,
                TotalPayableAmount = entity.TotalPayableAmount,
                ActualPaidAmount = entity.ActualPaidAmount,
                OrderTime = entity.OrderTime,
                TransferPaymentId = entity.TransferPaymentId,
                IsEstablished = entity.IsEstablished,
                MileRedemption = entity.MileRedemption,
                TotalGeneratedMile = entity.TotalGeneratedMile,
                IsInvoiced = entity.IsInvoiced
            };
            db.Tickets.Add(ticket);
            await db.SaveChangesAsync();
            return ticket.Id;
        }

        public async Task<(int, string)> CreateTicketDetail(TicketsDetailEntity entity)
        {
            var cabinId = db.AirCabinRules.Where(x => x.CabinCode == entity.AirCabinRuleId).FirstOrDefault().Id;
            TicketDetail ticketDetail = new TicketDetail
            {
                TicketId = entity.TicketId,
                AirCabinRuleId = cabinId,
                TypeofPassengerId = entity.TypeofPassengerId,
                TicketAmount = entity.TicketAmount,
                AccruedMile = entity.AccruedMile,
                BookRef = entity.BookRef
            };
            db.TicketDetails.Add(ticketDetail);
            await db.SaveChangesAsync();
            return (ticketDetail.Id, ticketDetail.BookRef);
        }

        public async Task<int> CreateTicketInvoce(TicketInvoicingDetailEntity entity, int airFlightId)
        {
            List<AirFlightSeat> flight = db.AirFlightSeats.Where(x => x.AirFlightId == airFlightId).ToList();
            var seatId = 0;
            foreach (var item in flight)
            {
                if (item.SeatNum == entity.AirFlightSeatId)
                {
                    seatId = item.Id;
                }
            }
            //update AirFlightSeat isSeated ==1
            var seat = db.AirFlightSeats.Find(seatId);
            if (seat != null)
            {
                seat.IsSeated = true;
                db.SaveChanges();
            }

            TicketInvoicingDetail ticketInvoicingDetail = new TicketInvoicingDetail
            {
                TicketDetailId = entity.TicketDetailId,
                AirFlightSeatId = seatId,
                LastName = entity.LastName,
                FirstName = entity.FirstName,
                CountryId = entity.CountryId,
                DateofBirth = entity.DateofBirth,
                Gender = entity.Gender,
                PassportNum = entity.PassportNum,
                PassportExpirationDate = entity.PassportExpirationDate,
                Remark = entity.Remark,
                TicketNum = entity.TicketNum,
                AirMealId = entity.AirMealId,
                CheckInStatus = entity.CheckInStatus,
                CheckInTime = entity.CheckInTime
            };
            db.TicketInvoicingDetails.Add(ticketInvoicingDetail);
            await db.SaveChangesAsync();
            return ticketInvoicingDetail.Id;
        }

        public TicketInvoicingDetailEntity GetTicketByMemberId(int memberId)
        {
            throw new NotImplementedException();
        }


        //Get TicketDetail Include InvocingDeatil By TicketNum && Member's FirstName && LastName
        public TicketInvoicingDetailEntity GetTicketInvoicingDetail(string ticketNum, string firstName, string lastName)
        {
            var ticketInvoicingDetailEntity = db.TicketInvoicingDetails
                .Include(t => t.TicketDetail)
                .Where(t => t.FirstName.ToLower() == firstName.ToLower() && t.LastName.ToLower() == lastName.ToLower() && t.TicketDetail.BookRef == ticketNum)
                .Select(t => new TicketInvoicingDetailEntity
                {
                    Id = t.Id,
                    TicketDetailId = t.TicketDetailId,
                    AirFlightSeatId = t.AirFlightSeatId.ToString(),
                    LastName = t.LastName,
                    FirstName = t.FirstName,
                    CountryId = t.CountryId,
                    DateofBirth = t.DateofBirth,
                    Gender = t.Gender,
                    PassportNum = t.PassportNum,
                    PassportExpirationDate = t.PassportExpirationDate,
                    Remark = t.Remark,
                    TicketNum = t.TicketNum,
                    AirMealId = t.AirMealId,
                    CheckInStatus = t.CheckInStatus,
                    CheckInTime = t.CheckInTime,
                })
                .FirstOrDefault();

            return ticketInvoicingDetailEntity;
        }

        //Cancle Tickets / change status

    }
}
