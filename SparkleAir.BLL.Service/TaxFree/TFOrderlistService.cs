using SparkleAir.DAL.EFRepository.TaxFree;
using SparkleAir.IDAL.IRepository.TaxFree;
using SparkleAir.Infa.Dto.TaxFree;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.TaxFree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace SparkleAir.BLL.Service.TaxFree
{
    public class TFOrderlistService
    {
        private AppDbContext _db;

        private readonly ITFOrderRepository _repository;

        public TFOrderlistService(AppDbContext db, ITFOrderRepository repository)
        {
            _db = db;
            _repository = repository;
        }



        public List<TFOrderlistsDto> Get()
        {
            List<TFOrderlistsEntity> result = _repository.Get();
            List<TFOrderlistsDto> list = result.Select(x => new TFOrderlistsDto
            {
                Id = x.Id,
                //TicketId = x.TicketId,
                TicketDetailsId = x.TicketDetailsId,
                //AirCabinRuleId = x.AirCabinRuleId,
                //TypeofPassengerId = x.TypeofPassengerId,
                //TicketAmount = x.TicketAmount,
                //AccuruedMile = x.AccuruedMile,
                TFItemsSerialNumber = x.TFItemsSerialNumber,
                TFItemsImage = x.TFItemsImage,
                TFItemsName = x.TFItemsName,
                TFItemsQuantity = x.TFItemsQuantity,
                TFItemsUnitPrice = x.TFItemsUnitPrice,
                TFItemsId = x.TFItemsId,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                TFOrderStatusID = x.TFOrderStatusID,
                Paid = x.Paid


            }).ToList();
            return list;
        }

        public void Delete(int Id)
        {
            _repository.Delete(Id);
        }

        public int Create(TFOrderlistsDto dto)
        {
            TFOrderlistsEntity entity = new TFOrderlistsEntity
            {
                Id = dto.Id,
                //TicketId = dto.TicketId,
                TicketDetailsId = dto.TicketDetailsId,
                //AirCabinRuleId = dto.AirCabinRuleId,
                //TypeofPassengerId = dto.TypeofPassengerId,
                //TicketAmount = dto.TicketAmount,
                //AccuruedMile = dto.AccuruedMile,
                TFItemsSerialNumber = dto.TFItemsSerialNumber,
                TFItemsImage = dto.TFItemsImage,
                TFItemsName = dto.TFItemsName,
                TFItemsQuantity = dto.TFItemsQuantity,
                TFItemsUnitPrice = dto.TFItemsUnitPrice,
                TFItemsId = dto.TFItemsId,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice,
                TFOrderStatusID = dto.TFOrderStatusID,

            };
            _repository.Create(entity);
            return entity.Id;
        }

        public void Update(TFOrderlistsDto dto)
        {
            TFOrderlistsEntity entity = _repository.Getid(dto.Id);

            if (entity != null)
            {
                entity.Id = dto.Id;
                //entity.TicketId = dto.TicketId;
                entity.TicketDetailsId = dto.TicketDetailsId;
                //entity.AirCabinRuleId = dto.AirCabinRuleId;
                //entity.TypeofPassengerId = dto.TypeofPassengerId;
                //entity.TicketAmount = dto.TicketAmount;
                //entity.AccuruedMile = dto.AccuruedMile;
                entity.TFItemsSerialNumber = dto.TFItemsSerialNumber;
                entity.TFItemsImage = dto.TFItemsImage;
                entity.TFItemsName = dto.TFItemsName;
                entity.TFItemsQuantity = dto.TFItemsQuantity;
                entity.TFItemsQuantity = dto.TFItemsUnitPrice;
                entity.TFItemsId = dto.TFItemsId;
                entity.Quantity = dto.Quantity;
                entity.UnitPrice = dto.UnitPrice;
                entity.TFOrderStatusID = dto.TFOrderStatusID;
                entity.Paid = dto.Paid;
            };

            _repository.Update(entity);
        }

        public void UpdateQuantity(int id, int newQuantity)
        {
            TFOrderlistsEntity entity = _repository.UpdateQuantity(id, newQuantity);

            if (entity != null)
            {

                entity.Quantity = newQuantity;
                _repository.Update(entity);
            };

        }
        public void UpdatePaid(int id, int newstatusid)
        {
            TFOrderlistsEntity entity = _repository.Getorderid(id);
            

            if (entity != null)
            {
                entity.TFOrderStatusID=newstatusid;
                _repository.UpdatePaid(entity);
            }
        }

        public TFOrderlistsDto Getid(int Id)
        {
            var entity = _repository.Getid(Id);

            if (entity == null)
            {
                throw new Exception("找不到對應的項目");
            }

            var dto = new TFOrderlistsDto
            {
                Id = entity.Id,
                //TicketId = entity.TicketId,
                TicketDetailsId = entity.TicketDetailsId,
                //AirCabinRuleId = entity.AirCabinRuleId,
                //TypeofPassengerId = entity.TypeofPassengerId,
                //TicketAmount = entity.TicketAmount,
                //AccuruedMile = entity.AccuruedMile,
                TFItemsSerialNumber = entity.TFItemsSerialNumber,
                TFItemsImage = entity.TFItemsImage,
                TFItemsName = entity.TFItemsName,
                TFItemsQuantity = entity.TFItemsQuantity,
                TFItemsUnitPrice = entity.TFItemsUnitPrice,
                TFItemsId = entity.TFItemsId,
                Quantity = entity.Quantity,
                UnitPrice = entity.UnitPrice,
                TFOrderStatusID = entity.TFOrderStatusID,
                Paid = entity.Paid
            };
            return dto;
        }

        public List<TFOrderlistsDto> GetItemsByTicketDetailsId(int ticketDetailsId)
        {
            var entities = _repository.GetItemsByTicketDetailsId(ticketDetailsId);

            if (entities.Count == 0 || !entities.Any())
            {
                return new List<TFOrderlistsDto>();
            }

            var dtoList = entities.Select(entity => new TFOrderlistsDto
            {
                Id = entity.Id,
                //TicketId = entity.TicketId,
                TicketDetailsId = entity.TicketDetailsId,
                //AirCabinRuleId = entity.AirCabinRuleId,
                //TypeofPassengerId = entity.TypeofPassengerId,
                //TicketAmount = entity.TicketAmount,
                //AccuruedMile = entity.AccuruedMile,
                TFItemsSerialNumber = entity.TFItemsSerialNumber,
                TFItemsImage = entity.TFItemsImage,
                TFItemsName = entity.TFItemsName,
                TFItemsQuantity = entity.TFItemsQuantity,
                TFItemsUnitPrice = entity.TFItemsUnitPrice,
                TFItemsId = entity.TFItemsId,
                Quantity = entity.Quantity,
                UnitPrice = entity.UnitPrice,
                TFOrderStatusID = entity.TFOrderStatusID,
                Paid = entity.Paid
            }).ToList();

            return dtoList;
        }

        public List<TFOrderlistsDto> GetSelectedItems(int ticketDetailsId, List<int> selectedOrderListIds)
        {
            var entities = _repository.GetSelectedItems(ticketDetailsId,selectedOrderListIds);

            if (entities == null || !entities.Any())
            {
                throw new Exception("找不到對應的項目");
            }

            var dtoList = entities.Select(entity => new TFOrderlistsDto
            {
                Id = entity.Id,
                //TicketId = entity.TicketId,
                TicketDetailsId = entity.TicketDetailsId,
                //AirCabinRuleId = entity.AirCabinRuleId,
                //TypeofPassengerId = entity.TypeofPassengerId,
                //TicketAmount = entity.TicketAmount,
                //AccuruedMile = entity.AccuruedMile,
                TFItemsSerialNumber = entity.TFItemsSerialNumber,
                TFItemsImage = entity.TFItemsImage,
                TFItemsName = entity.TFItemsName,
                TFItemsQuantity = entity.TFItemsQuantity,
                TFItemsUnitPrice = entity.TFItemsUnitPrice,
                TFItemsId = entity.TFItemsId,
                Quantity = entity.Quantity,
                UnitPrice = entity.UnitPrice,
                TFOrderStatusID = entity.TFOrderStatusID,
                Paid = entity.Paid
            }).ToList();

            return dtoList;
        }




        public int AddToCart(TFOrderlistsDto dto)
        {
            TFOrderlistsEntity entity = new TFOrderlistsEntity
            {
                Id = dto.Id,
                //TicketId = dto.TicketId,
                TicketDetailsId = dto.TicketDetailsId,
                //AirCabinRuleId = dto.AirCabinRuleId,
                //TypeofPassengerId = dto.TypeofPassengerId,
                //TicketAmount = dto.TicketAmount,
                //AccuruedMile = dto.AccuruedMile,
                TFItemsSerialNumber = dto.TFItemsSerialNumber,
                TFItemsImage = dto.TFItemsImage,
                TFItemsName = dto.TFItemsName,
                TFItemsQuantity = dto.TFItemsQuantity,
                TFItemsUnitPrice = dto.TFItemsUnitPrice,
                TFItemsId = dto.TFItemsId,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice,
                TFOrderStatusID = dto.TFOrderStatusID,

            };
            _repository.AddToCart(entity);
            return entity.Id;
        }

        
    }
}
