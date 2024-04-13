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
    public class TFReseveEFRepository : ITFReserve
    {
        private AppDbContext _db;

        public TFReseveEFRepository(AppDbContext db)
        {
            _db = db;
        }
        int ITFReserve.Create(TFReservesEntity entity)
        {
            var tfModel = new Tfreserf();

            tfModel.TicketDetailsId = entity.TicketDetailsId;
            tfModel.MemberId = entity.MemberId;
            tfModel.TransferPaymentId = entity.TransferPaymentId;
            tfModel.Discount = entity.Discount;
            tfModel.TotalPrice = entity.TotalPrice;

            _db.Tfreserves.Add(tfModel);
            _db.SaveChanges();
            return entity.Id;
        }

        void ITFReserve.Delete(int id)
        {
            throw new NotImplementedException();
        }

        List<TFReservesEntity> ITFReserve.Get()
        {
           
            var getlist = _db.Tfreserves.AsNoTracking()
                                         .Include(x => x.Member)
                                         .Include(x => x.TicketDetails)
                                         .Include(x => x.TransferPayment)

                                         .Select(x => new TFReservesEntity
                                         {
                                             Id = x.Id,
                                             MemberId = x.MemberId,
                                             MemberChineseFirstName = x.Member.ChineseFirstName,
                                             MemberChineseLastName = x.Member.ChineseLastName,
                                             MemberEnglishFirstName = x.Member.EnglishFirstName,
                                             MemberEnglishLastName = x.Member.EnglishLastName,
                                             MemberPhone = x.Member.Phone,
                                             MemberEmail = x.Member.Email,
                                             MemberPassportNumber = x.Member.PassportNumber,
                                             Discount = x.Discount,
                                             TotalPrice = x.TotalPrice,
                                             TransferPaymentId = x.TransferPaymentId,
                                             PaymentDate=x.TransferPayment.PaymentDate,
                                             PaymentAmount=x.TransferPayment.PaymentAmount,
                                             Info=x.TransferPayment.Info,
                                             TicketId = x.TicketDetails.TicketId,
                                             TicketDetailsId = x.TicketDetailsId,
                                             AirCabinRuleId = x.TicketDetails.AirCabinRuleId,
                                             TypeofPassengerId = x.TicketDetails.TypeofPassengerId,
                                             AccruedMile = x.TicketDetails.AccruedMile,
                                             
                                         })
                                         .ToList();
            return getlist;
        }

        List<TFReservesEntity> ITFReserve.Getid(int ticketDetailsId)
        {

            var getlist = _db.Tfreserves.AsNoTracking()
                                         .Include(x => x.Member)
                                         .Include(x => x.TicketDetails)
                                         .Include(x => x.TransferPayment)
                                         .Where(x => x.TicketDetailsId == ticketDetailsId)
                                         .Select(x => new TFReservesEntity
                                         {
                                             Id = x.Id,
                                             MemberId = x.MemberId,
                                             MemberChineseFirstName = x.Member.ChineseFirstName,
                                             MemberChineseLastName = x.Member.ChineseLastName,
                                             MemberEnglishFirstName = x.Member.EnglishFirstName,
                                             MemberEnglishLastName = x.Member.EnglishLastName,
                                             MemberPhone = x.Member.Phone,
                                             MemberEmail = x.Member.Email,
                                             MemberPassportNumber = x.Member.PassportNumber,
                                             Discount = x.Discount,
                                             TotalPrice = x.TotalPrice,
                                             TransferPaymentId = x.TransferPaymentId,
                                             PaymentDate = x.TransferPayment.PaymentDate,
                                             PaymentAmount = x.TransferPayment.PaymentAmount,
                                             Info = x.TransferPayment.Info,
                                             TicketId = x.TicketDetails.TicketId,
                                             TicketDetailsId = x.TicketDetailsId,
                                             AirCabinRuleId = x.TicketDetails.AirCabinRuleId,
                                             TypeofPassengerId = x.TicketDetails.TypeofPassengerId,
                                             AccruedMile = x.TicketDetails.AccruedMile,

                                         })
                                         .ToList();
            return getlist;
        }

        List<TFReservesEntity> ITFReserve.Search(TFReservesEntity entity)
        {
            throw new NotImplementedException();
        }

        void ITFReserve.Update(TFReservesEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
