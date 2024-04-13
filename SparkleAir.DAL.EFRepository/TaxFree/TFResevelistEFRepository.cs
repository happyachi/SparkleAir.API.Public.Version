using Microsoft.EntityFrameworkCore;
using SparkleAir.IDAL.IRepository.TaxFree;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.TaxFree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.DAL.EFRepository.TaxFree
{
    public class TFResevelistEFRepository : ITFReservelist
    {
        private AppDbContext _db;

        public TFResevelistEFRepository(AppDbContext db)
        {
            _db = db;
        }
        int ITFReservelist.Create(TFReservelistsEntity entity)
        {
            var tfModel = new Tfreservelist();

            tfModel.TfitemsId = entity.TFItemsId;
            tfModel.Discount = entity.Discount;
            tfModel.TotalPrice = entity.TotalPrice;
            tfModel.UnitPrice = entity.UnitPrice;
            tfModel.Quantity = entity.Quantity;
            tfModel.TfreserveId = entity.TFReserveId;


            _db.Tfreservelists.Add(tfModel);
            _db.SaveChanges();
            return entity.Id;
        }

        void ITFReservelist.Delete(int id)
        {
            throw new NotImplementedException();
        }

        List<TFReservelistsEntity> ITFReservelist.Get()
        {

            var getlist = _db.Tfreservelists.AsNoTracking()
                                         .Include(x => x.Tfitems)
                                         .Include(x => x.Tfreserve)
                                         .Include(x => x.Tfreserve.Member)
                                         .Include(x => x.Tfreserve.TransferPayment)
                                         .Include(x => x.Tfreserve.TicketDetails)
                                         .Select(x => new TFReservelistsEntity
                                         {
                                             Id = x.Id,
                                             TFItemsId = x.TfitemsId,
                                             TFItemsName=x.Tfitems.Name,
                                             TFItemsImage = x.Tfitems.Image,
                                             TFItemsUnitPrice=x.Tfitems.UnitPrice,
                                             TFItemsQuantity=x.Tfitems.Quantity,
                                             UnitPrice = x.UnitPrice,
                                             Quantity = x.Quantity,
                                             Discount = x.Discount,
                                             TotalPrice = x.TotalPrice,
                                             MemberId = x.Tfreserve.MemberId,
                                             MemberChineseFirstName = x.Tfreserve.Member.ChineseFirstName,
                                             MemberChineseLastName = x.Tfreserve.Member.ChineseLastName,
                                             MemberEnglishFirstName = x.Tfreserve.Member.EnglishFirstName,
                                             MemberEnglishLastName = x.Tfreserve.Member.EnglishLastName,
                                             MemberPhone = x.Tfreserve.Member.Phone,
                                             MemberEmail = x.Tfreserve.Member.Email,
                                             MemberPassportNumber = x.Tfreserve.Member.PassportNumber,
                                             TransferPaymentId = x.Tfreserve.TransferPaymentId,
                                             PaymentDate = x.Tfreserve.TransferPayment.PaymentDate,
                                             PaymentAmount = x.Tfreserve.TransferPayment.PaymentAmount,
                                             Info = x.Tfreserve.TransferPayment.Info,
                                             TicketId = x.Tfreserve.TicketDetails.TicketId,
                                             TicketDetailsId = x.Tfreserve.TicketDetailsId,
                                             AirCabinRuleId = x.Tfreserve.TicketDetails.AirCabinRuleId,
                                             TypeofPassengerId = x.Tfreserve.TicketDetails.TypeofPassengerId,
                                             AccruedMile = x.Tfreserve.TicketDetails.AccruedMile,
                                             TFReserveId = x.TfreserveId
                                             

                                         })
                                         .ToList();
            return getlist;
        }

        List<TFReservelistsEntity> ITFReservelist.Getid(int ticketDetailsId)
        {

            var getlist = _db.Tfreservelists.AsNoTracking()
                                         .Include(x => x.Tfitems)
                                         .Include(x => x.Tfreserve)
                                         .Include(x => x.Tfreserve.Member)
                                         .Include(x => x.Tfreserve.TransferPayment)
                                         .Include(x => x.Tfreserve.TicketDetails)
                                         .Include(x => x.Tfreserve.TicketDetails.Ticket)
                                         .Include(x => x.Tfreserve.TicketDetails.Ticket.AirFlight)
                                         .Include(x => x.Tfreserve.TicketDetails.Ticket.AirFlight.AirFlightManagement)
                                         .Include(x => x.Tfreserve.TicketDetails.Ticket.AirFlight.AirFlightManagement.DepartureAirport)
                                         .Include(x => x.Tfreserve.TicketDetails.TicketInvoicingDetails)
                                         .Where(x => x.Tfreserve.TicketDetailsId == ticketDetailsId)
                                         .Select(x => new TFReservelistsEntity
                                         {
                                             Id = x.Id,
                                             TFItemsId = x.TfitemsId,
                                             TFItemsName = x.Tfitems.Name,
                                             TFItemsImage = x.Tfitems.Image,
                                             TFItemsUnitPrice = x.Tfitems.UnitPrice,
                                             TFItemsQuantity = x.Tfitems.Quantity,
                                             UnitPrice = x.UnitPrice,
                                             Quantity = x.Quantity,
                                             Discount = x.Discount,
                                             TotalPrice = x.TotalPrice,
                                             MemberId = x.Tfreserve.MemberId,
                                             MemberChineseFirstName = x.Tfreserve.Member.ChineseFirstName,
                                             MemberChineseLastName = x.Tfreserve.Member.ChineseLastName,
                                             MemberEnglishFirstName = x.Tfreserve.Member.EnglishFirstName,
                                             MemberEnglishLastName = x.Tfreserve.Member.EnglishLastName,
                                             MemberPhone = x.Tfreserve.Member.Phone,
                                             MemberEmail = x.Tfreserve.Member.Email,
                                             MemberPassportNumber = x.Tfreserve.Member.PassportNumber,
                                             TransferPaymentId = x.Tfreserve.TransferPaymentId,
                                             PaymentDate = x.Tfreserve.TransferPayment.PaymentDate,
                                             PaymentAmount = x.Tfreserve.TransferPayment.PaymentAmount,
                                             Info = x.Tfreserve.TransferPayment.Info,
                                             TicketId = x.Tfreserve.TicketDetails.TicketId,
                                             TicketDetailsId = x.Tfreserve.TicketDetailsId,
                                             AirCabinRuleId = x.Tfreserve.TicketDetails.AirCabinRuleId,
                                             TypeofPassengerId = x.Tfreserve.TicketDetails.TypeofPassengerId,
                                             AccruedMile = x.Tfreserve.TicketDetails.AccruedMile,
                                             TFReserveId = x.TfreserveId,
                                             TicketDetailFirstName = _db.TicketInvoicingDetails.Where(t=>t.TicketDetailId == x.Tfreserve.TicketDetails.Id).Select(t=>t.FirstName).FirstOrDefault(),
                                             TicketDetailLastName = _db.TicketInvoicingDetails.Where(t => t.TicketDetailId == x.Tfreserve.TicketDetails.Id).Select(t => t.LastName).FirstOrDefault(),
                                             TicketDetailBookRef = _db.TicketInvoicingDetails.Where(t => t.TicketDetailId == x.Tfreserve.TicketDetails.Id).Select(t => t.TicketDetail.BookRef).FirstOrDefault(),
                                             TicketDetailFlightCode = _db.TicketInvoicingDetails.Where(t => t.TicketDetailId == x.Tfreserve.TicketDetails.Id).Select(t => t.TicketDetail.Ticket.AirFlight.AirFlightManagement.FlightCode).FirstOrDefault(),
                                             TicketDetailScheduledDepartureTime = _db.TicketInvoicingDetails.Where(t => t.TicketDetailId == x.Tfreserve.TicketDetails.Id).Select(t => t.TicketDetail.Ticket.AirFlight.ScheduledDeparture).FirstOrDefault(),
                                         })
                                         .ToList();
            return getlist;
        }

       
        List <TFReservelistsEntity> ITFReservelist.Search(TFReservelistsEntity entity)
        {
            throw new NotImplementedException();
        }

        void ITFReservelist.Update(TFReservelistsEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
