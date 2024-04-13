using SparkleAir.IDAL.IRepository.TaxFree;
using SparkleAir.Infa.Dto.TaxFree;
using SparkleAir.Infa.Entity.TaxFree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.BLL.Service.TaxFree
{
    public class TFReserveService
    {
        private readonly ITFReserve _repo;

        public TFReserveService(ITFReserve repo)
        {
            _repo = repo;
        }

        public List<TFReservesDto> Get()
        {
            List<TFReservesEntity> result = _repo.Get();
            List<TFReservesDto> list = result.Select(x => new TFReservesDto
            {
                Id = x.Id,
                MemberId = x.MemberId,
                Discount = x.Discount.HasValue ? x.Discount.Value : 0,
                TotalPrice = x.TotalPrice,
                TransferPaymentId = x.TransferPaymentId,
                MemberChineseFirstName = x.MemberChineseFirstName,
                MemberChineseLastName = x.MemberChineseLastName,
                MemberEnglishFirstName = x.MemberEnglishFirstName,
                MemberEnglishLastName = x.MemberEnglishLastName,
                MemberPhone = x.MemberPhone,
                MemberEmail = x.MemberEmail,
                MemberPassportNumber = x.MemberPassportNumber,
                TicketId = x.TicketId,
                TicketDetailsId = x.TicketDetailsId,
                AccruedMile = x.AccruedMile,
                AirCabinRuleId = x.AirCabinRuleId,
                TypeofPassengerId = x.TypeofPassengerId,
                PaymentDate = x.PaymentDate,
                PaymentAmount = x.PaymentAmount,
                TicketAmount = x.TicketAmount
                
            }).ToList();
            return list;
        }

        public void Delete(int Id)
        {
            _repo.Delete(Id);
        }
        public int Create(TFReservesDto x)
        {
            TFReservesEntity entity = new TFReservesEntity
            {
                Id = x.Id,
                MemberId = x.MemberId,
                Discount = x.Discount.HasValue ? x.Discount.Value : 0,
                TotalPrice = x.TotalPrice,
                TransferPaymentId = x.TransferPaymentId,
                MemberChineseFirstName = x.MemberChineseFirstName,
                MemberChineseLastName = x.MemberChineseLastName,
                MemberEnglishFirstName = x.MemberEnglishFirstName,
                MemberEnglishLastName = x.MemberEnglishLastName,
                MemberPhone = x.MemberPhone,
                MemberEmail = x.MemberEmail,
                MemberPassportNumber = x.MemberPassportNumber,
                TicketId = x.TicketId,
                TicketDetailsId = x.TicketDetailsId,
                AccruedMile = x.AccruedMile,
                AirCabinRuleId = x.AirCabinRuleId,
                TypeofPassengerId = x.TypeofPassengerId,
                PaymentDate = x.PaymentDate,
                PaymentAmount = x.PaymentAmount,
                TicketAmount = x.TicketAmount

            };
            _repo.Create(entity);
            return entity.Id;
        }

        public void Update(TFReservesDto dto)
        {
            TFReservesEntity entity = new TFReservesEntity
            {

                Id = dto.Id,
                MemberId = dto.MemberId,
                MemberChineseFirstName = dto.MemberChineseFirstName,
                MemberChineseLastName = dto.MemberChineseLastName,
                MemberEnglishFirstName = dto.MemberEnglishFirstName,
                MemberEnglishLastName = dto.MemberEnglishLastName,
                MemberPhone = dto.MemberPhone,
                MemberEmail = dto.MemberEmail,
                MemberPassportNumber = dto.MemberPassportNumber,
                Discount = dto.Discount,
                TotalPrice = dto.TotalPrice,
                TransferPaymentId = dto.TransferPaymentId
            };
            _repo.Update(entity);
        }

        public List<TFReservesDto> Getid(int id)
        {
            List<TFReservesEntity> result = _repo.Getid(id);
            List<TFReservesDto> list = result.Select(x => new TFReservesDto
            {
                Id = x.Id,
                MemberId = x.MemberId,
                Discount = x.Discount.HasValue ? x.Discount.Value : 0,
                TotalPrice = x.TotalPrice,
                TransferPaymentId = x.TransferPaymentId,
                MemberChineseFirstName = x.MemberChineseFirstName,
                MemberChineseLastName = x.MemberChineseLastName,
                MemberEnglishFirstName = x.MemberEnglishFirstName,
                MemberEnglishLastName = x.MemberEnglishLastName,
                MemberPhone = x.MemberPhone,
                MemberEmail = x.MemberEmail,
                MemberPassportNumber = x.MemberPassportNumber,
                TicketId = x.TicketId,
                TicketDetailsId = x.TicketDetailsId,
                AccruedMile = x.AccruedMile,
                AirCabinRuleId = x.AirCabinRuleId,
                TypeofPassengerId = x.TypeofPassengerId,
                PaymentDate = x.PaymentDate,
                PaymentAmount = x.PaymentAmount,
                TicketAmount = x.TicketAmount

            }).ToList();
            return list;
        }

    }
}

