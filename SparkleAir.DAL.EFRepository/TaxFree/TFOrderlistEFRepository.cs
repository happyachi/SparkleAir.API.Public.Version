using Microsoft.EntityFrameworkCore;
using SparkleAir.IDAL.IRepository.TaxFree;
using SparkleAir.Infa.Dto.TaxFree;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.TaxFree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.DAL.EFRepository.TaxFree
{
    public class TFOrderlistEFRepository : ITFOrderRepository
    {
        private AppDbContext _db;

        public TFOrderlistEFRepository(AppDbContext db)
        {
            _db = db;
        }

        int ITFOrderRepository.Create(TFOrderlistsEntity entity)
        {

            var tfModel = new Tforderlist();
            var orderstatus = new Infa.EFModel.Models.TforderStatus
            {
                Paid = false,
                PaidTime = DateTime.Now,
            };
            _db.TforderStatuses.Add(orderstatus);
            _db.SaveChangesAsync();

            tfModel.Id = entity.Id;
            tfModel.TicketDetailsId = entity.TicketDetailsId;
            tfModel.TfitemsId = entity.TFItemsId;
            tfModel.Quantity = entity.Quantity;
            tfModel.UnitPrice = entity.UnitPrice;
            tfModel.TforderStatusId = orderstatus.Id;

            _db.Tforderlists.Add(tfModel);
            _db.SaveChanges();
            return entity.Id;
        }


        void ITFOrderRepository.Delete(int id)
        {
            var tfModel = _db.Tforderlists.Find(id);
            _db.Tforderlists.Remove(tfModel);
            _db.SaveChanges();
        }

        List<TFOrderlistsEntity> ITFOrderRepository.Get()
        {

            var getlist = _db.Tforderlists.AsNoTracking()
                                         .Include(x => x.Tfitems)
                                         .Include(x => x.TforderStatus)
                                         .Include(x => x.TicketDetails)
                                         .Select(x => new TFOrderlistsEntity
                                         {
                                             Id = x.Id,
                                             TFItemsId = x.TfitemsId,
                                             TFItemsSerialNumber = x.Tfitems.SerialNumber,
                                             TFItemsImage = x.Tfitems.Image,
                                             TFItemsName = x.Tfitems.Name,
                                             TFItemsQuantity = x.Tfitems.Quantity,
                                             TFItemsUnitPrice = x.Tfitems.UnitPrice,
                                             TicketDetailsId = x.TicketDetailsId,
                                             //AirCabinRuleId = x.TicketDetails.AirCabinRuleId,
                                             //TypeofPassengerId = x.TicketDetails.TypeofPassengerId,
                                             //AccuruedMile = x.TicketDetails.AccruedMile,
                                             Quantity = x.Quantity,
                                             UnitPrice = x.UnitPrice,
                                             TFOrderStatusID = x.TforderStatus.Id,
                                             Paid = x.TforderStatus.Paid

                                         })
                                         .ToList();
            return getlist;


        }

        TFOrderlistsEntity ITFOrderRepository.Getid(int ticketDetailsId)
        {
            var get = _db.Tforderlists.Include(x => x.Tfitems)
                                .Include(x => x.TforderStatus)
                               .Include(x => x.TicketDetails)
                               .FirstOrDefault(x => x.TicketDetailsId == ticketDetailsId);

            if (get != null)
            {
                TFOrderlistsEntity getlist = new TFOrderlistsEntity()
                {
                    Id = get.Id,
                    TFItemsId = get.TfitemsId,
                    TFItemsSerialNumber = get.Tfitems.SerialNumber,
                    TFItemsImage = get.Tfitems.Image,
                    TFItemsName = get.Tfitems.Name,
                    TFItemsQuantity = get.Tfitems.Quantity,
                    TFItemsUnitPrice = get.Tfitems.UnitPrice,
                    //TicketId = get.TicketDetails.TicketId,
                    //TicketAmount = (int)get.TicketDetails.TicketAmount,
                    TicketDetailsId = get.TicketDetailsId,
                    //AirCabinRuleId = get.TicketDetails.AirCabinRuleId,
                    //TypeofPassengerId = get.TicketDetails.TypeofPassengerId,
                    //AccuruedMile = get.TicketDetails.AccruedMile,
                    Quantity = get.Quantity,
                    UnitPrice = get.UnitPrice,
                   

                };
                return getlist;
            }
            else
            {

                return null;
            }
        }

        TFOrderlistsEntity ITFOrderRepository.Getorderid(int orderId)
        {
            var getOrder = _db.Tforderlists
                      .Include(x => x.Tfitems)
                      .Include(x => x.TforderStatus)
                      .Include(x => x.TicketDetails)
                      .FirstOrDefault(x => x.Id == orderId);

            if (getOrder != null)
            {
                TFOrderlistsEntity orderEntity = new TFOrderlistsEntity()
                {
                    Id = getOrder.Id,
                    TFItemsId = getOrder.TfitemsId,
                    TFItemsSerialNumber = getOrder.Tfitems.SerialNumber,
                    TFItemsImage = getOrder.Tfitems.Image,
                    TFItemsName = getOrder.Tfitems.Name,
                    TFItemsQuantity = getOrder.Tfitems.Quantity,
                    TFItemsUnitPrice = getOrder.Tfitems.UnitPrice,
                    //TicketId = getOrder.TicketDetails.TicketId,
                    //TicketAmount = (int)getOrder.TicketDetails.TicketAmount,
                    TicketDetailsId = getOrder.TicketDetailsId,
                    //AirCabinRuleId = getOrder.TicketDetails.AirCabinRuleId,
                    //TypeofPassengerId = getOrder.TicketDetails.TypeofPassengerId,
                    //AccuruedMile = getOrder.TicketDetails.AccruedMile,
                    //Quantity = getOrder.Quantity,
                    UnitPrice = getOrder.UnitPrice,
                    TFOrderStatusID = getOrder.TforderStatus.Id,
                    Paid = getOrder.TforderStatus.Paid
                };
                return orderEntity;
            }
            else
            {
                return null;
            }
        }

        public List<TFOrderlistsEntity> GetItemsByTicketDetailsId(int ticketDetailsId)
        {
            var getlist = _db.Tforderlists
                    .Include(x => x.Tfitems)
                    .Include(x => x.TicketDetails)
                    .Include(x => x.TforderStatus)
                    .Where(x => x.TicketDetailsId == ticketDetailsId)
                    .Select(x => new TFOrderlistsEntity
                    {
                        Id = x.Id,
                        TFItemsId = x.TfitemsId,
                        TFItemsSerialNumber = x.Tfitems.SerialNumber,
                        TFItemsImage = x.Tfitems.Image,
                        TFItemsName = x.Tfitems.Name,
                        TFItemsQuantity = x.Tfitems.Quantity,
                        TFItemsUnitPrice = x.Tfitems.UnitPrice,
                        TicketDetailsId = x.TicketDetailsId,
                        //AirCabinRuleId = x.TicketDetails.AirCabinRuleId,
                        //TypeofPassengerId = x.TicketDetails.TypeofPassengerId,
                        //AccuruedMile = x.TicketDetails.AccruedMile,
                        Quantity = x.Quantity,
                        UnitPrice = x.UnitPrice,
                        TFOrderStatusID = x.TforderStatus.Id,
                        Paid = x.TforderStatus.Paid
                    })
                    .ToList();

            return getlist;
        }

        public List<TFOrderlistsEntity> GetSelectedItems(int ticketDetailsId, List<int> selectedOrderListIds)
        {
            var getlist = _db.Tforderlists
                .Include(x => x.Tfitems)
                .Include(x => x.TicketDetails)
                .Include(x => x.TforderStatus)
                .Where(x => x.TicketDetailsId == ticketDetailsId && selectedOrderListIds.Contains(x.Id))
                .Select(x => new TFOrderlistsEntity
                {
                    Id = x.Id,
                    TFItemsId = x.TfitemsId,
                    TFItemsSerialNumber = x.Tfitems.SerialNumber,
                    TFItemsImage = x.Tfitems.Image,
                    TFItemsName = x.Tfitems.Name,
                    TFItemsQuantity = x.Tfitems.Quantity,
                    TFItemsUnitPrice = x.Tfitems.UnitPrice,
                    TicketDetailsId = x.TicketDetailsId,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    TFOrderStatusID = x.TforderStatus.Id,
                    Paid = x.TforderStatus.Paid
                })
                .ToList();

            return getlist;
        }



        List<TFOrderlistsEntity> ITFOrderRepository.Search(TFOrderlistsEntity entity)
        {
            List<TFOrderlistsEntity> getlist = _db.Tforderlists.AsNoTracking()
                                                 .Include(x => x.Tfitems)
                                                 .Include(x => x.TicketDetails)
                                                 //.Where(x => x.MemberId == entity.MemberId)
                                                 .Select(x => new TFOrderlistsEntity
                                                 {
                                                     Id = x.Id,
                                                     TFItemsId = x.TfitemsId,
                                                     TFItemsSerialNumber = x.Tfitems.SerialNumber,
                                                     TFItemsImage = x.Tfitems.Image,
                                                     TFItemsName = x.Tfitems.Name,
                                                     TFItemsQuantity = x.Tfitems.Quantity,
                                                     TFItemsUnitPrice = x.Tfitems.UnitPrice,
                                                     TicketDetailsId = x.TicketDetailsId,
                                                     //AirCabinRuleId = x.TicketDetails.AirCabinRuleId,
                                                     //TypeofPassengerId = x.TicketDetails.TypeofPassengerId,
                                                     //AccuruedMile = x.TicketDetails.AccruedMile,
                                                     Quantity = x.Quantity,
                                                     UnitPrice = x.UnitPrice
                                                 })
                                                 .ToList();
            return getlist;
        }

        void ITFOrderRepository.Update(TFOrderlistsEntity entity)
        {

            var tfModel = _db.Tforderlists.Find(entity.Id);


            if (tfModel != null)
            {
                tfModel.Quantity = entity.Quantity;

            }
            _db.SaveChanges();
        }

        void ITFOrderRepository.UpdatePaid(TFOrderlistsEntity entity)
        {
            var tfModel = _db.Tforderlists.Find(entity.Id);

            if (tfModel != null)
            {
                tfModel.TforderStatusId = entity.TFOrderStatusID;
            }

            _db.SaveChanges();
        }

        int ITFOrderRepository.AddToCart(TFOrderlistsEntity entity)
        {
            var tfModel = new Tforderlist();
            var orderstatus = new Infa.EFModel.Models.TforderStatus
            {
                Paid = false,
                                
            };
            _db.TforderStatuses.Add(orderstatus);
            _db.SaveChanges();

            tfModel.TfitemsId = entity.TFItemsId;
            tfModel.TicketDetailsId = entity.TicketDetailsId;
            tfModel.Quantity = entity.Quantity;
            tfModel.UnitPrice = entity.UnitPrice;
            tfModel.TforderStatusId = orderstatus.Id;

            _db.Tforderlists.Add(tfModel);

            _db.SaveChanges();

            return tfModel.Id;
        }
        



        TFOrderlistsEntity ITFOrderRepository.UpdateQuantity(int id, int newQuantity)
        {
            var order = _db.Tforderlists.Find(id);

            if (order != null)
            {
                order.Quantity = newQuantity;
                _db.SaveChanges(); // 保存更改

                // 轉換為 TFOrderlistsEntity 並返回
                return new TFOrderlistsEntity
                {
                    Id = order.Id,
                    Quantity = order.Quantity,
                    // 其他屬性的轉換
                };
            }
            return null;

        }
    }
 
}
