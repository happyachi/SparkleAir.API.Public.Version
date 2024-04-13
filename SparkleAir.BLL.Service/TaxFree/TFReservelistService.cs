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
    public class TFReservelistService
    {
        private readonly ITFReservelist _repo;
        public TFReservelistService(ITFReservelist repo)
        {
            _repo = repo;
        }

        public List<TFReservelistsDto> Get()
        {
            List<TFReservelistsEntity> result = _repo.Get();
            List<TFReservelistsDto> list = result.Select(x => new TFReservelistsDto
            {
                Id = x.Id,
                TFItemsId = x.TFItemsId,
                TFItemsName = x.TFItemsName,
                TFItemsImage = x.TFItemsImage,
                TFItemsQuantity = x.TFItemsQuantity,
                TFItemsUnitPrice = x.TFItemsUnitPrice,
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity,
                Discount = x.Discount,
                TotalPrice = x.TotalPrice,
                MemberId = x.MemberId,
                MemberChineseFirstName = x.MemberChineseFirstName,
                MemberChineseLastName = x.MemberChineseLastName,
                MemberEnglishFirstName = x.MemberEnglishFirstName,
                MemberEnglishLastName = x.MemberEnglishLastName,
                MemberPhone = x.MemberPhone,
                MemberEmail = x.MemberEmail,
                MemberPassportNumber = x.MemberPassportNumber,
                TransferPaymentId = x.TransferPaymentId,
                PaymentDate = x.PaymentDate,
                PaymentAmount = x.PaymentAmount,
                Info = x.Info,
                TicketId = x.TicketId,
                TicketDetailsId = x.TicketDetailsId,
                AirCabinRuleId = x.AirCabinRuleId,
                TypeofPassengerId = x.TypeofPassengerId,
                AccruedMile = x.AccruedMile,
                TFReserveId = x.TFReserveId

            }).ToList();
            return list;
        }

        public void Delete(int Id)
        {
            _repo.Delete(Id);
        }

        public int Create(TFReservelistsDto dto)
        {
            TFReservelistsEntity entity = new TFReservelistsEntity
            {
                Id = dto.Id,
                TFItemsId = dto.TFItemsId,
                TFItemsName = dto.TFItemsName,
                TFItemsImage = dto.TFItemsImage,
                UnitPrice = dto.UnitPrice,
                Quantity = dto.Quantity,
                Discount = dto.Discount,
                TotalPrice = dto.TotalPrice,
                MemberId = dto.MemberId,
                MemberChineseFirstName = dto.MemberChineseFirstName,
                MemberChineseLastName = dto.MemberChineseLastName,
                MemberEnglishFirstName = dto.MemberEnglishFirstName,
                MemberEnglishLastName = dto.MemberEnglishLastName,
                MemberPhone = dto.MemberPhone,
                MemberEmail = dto.MemberEmail,
                MemberPassportNumber = dto.MemberPassportNumber,
                TransferPaymentId = dto.TransferPaymentId,
                PaymentDate = dto.PaymentDate,
                PaymentAmount = dto.PaymentAmount,
                Info = dto.Info,
                TicketId = dto.TicketId,
                TicketDetailsId = dto.TicketDetailsId,
                AirCabinRuleId = dto.AirCabinRuleId,
                TypeofPassengerId = dto.TypeofPassengerId,
                AccruedMile = dto.AccruedMile,
                TFReserveId = dto.TFReserveId

            };
            _repo.Create(entity);
            return entity.Id;
        }

        public void Update(TFReservelistsDto dto)
        {
            TFReservelistsEntity entity = new TFReservelistsEntity
            {
                Id = dto.Id,
                TFItemsId = dto.TFItemsId,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice,
                Discount = dto.Discount,
                TotalPrice = dto.TotalPrice,
            };
            _repo.Update(entity);
        }
        
        public List<TFReservelistsDto> Getid(int id)
        {
            List<TFReservelistsEntity> result = _repo.Getid(id);
            List<TFReservelistsDto> list = result.Select(x => new TFReservelistsDto
            {
                Id = x.Id,
                TFItemsId = x.TFItemsId,
                TFItemsName = x.TFItemsName,
                TFItemsImage = x.TFItemsImage,
                TFItemsQuantity = x.TFItemsQuantity,
                TFItemsUnitPrice = x.TFItemsUnitPrice,
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity,
                Discount = x.Discount,
                TotalPrice = x.TotalPrice,
                MemberId = x.MemberId,
                MemberChineseFirstName = x.MemberChineseFirstName,
                MemberChineseLastName = x.MemberChineseLastName,
                MemberEnglishFirstName = x.MemberEnglishFirstName,
                MemberEnglishLastName = x.MemberEnglishLastName,
                MemberPhone = x.MemberPhone,
                MemberEmail = x.MemberEmail,
                MemberPassportNumber = x.MemberPassportNumber,
                TransferPaymentId = x.TransferPaymentId,
                PaymentDate = x.PaymentDate,
                PaymentAmount = x.PaymentAmount,
                Info = x.Info,
                TicketId = x.TicketId,
                TicketDetailsId = x.TicketDetailsId,
                AirCabinRuleId = x.AirCabinRuleId,
                TypeofPassengerId = x.TypeofPassengerId,
                AccruedMile = x.AccruedMile,
                TFReserveId = x.TFReserveId,
                TicketDetailFirstName = x.TicketDetailFirstName,
                TicketDetailLastName = x.TicketDetailLastName,
                TicketDetailBookRef = x.TicketDetailBookRef,
                TicketDetailFlightCode = x.TicketDetailFlightCode,
                TicketDetailScheduledDepartureTime = x.TicketDetailScheduledDepartureTime
            }).ToList();
            return list;
        }
    }
}

