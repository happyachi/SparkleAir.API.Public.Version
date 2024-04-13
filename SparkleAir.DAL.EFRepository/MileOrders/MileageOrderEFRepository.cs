using Google.Apis.Drive.v3.Data;
using Microsoft.EntityFrameworkCore;
using SparkleAir.IDAL.IRepository.MileOrder;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.MileageDetails;
using SparkleAir.Infa.Entity.MileOrder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.DAL.EFRepository.MileOrders
{
    public class MileageOrderEFRepository: IMileageOrderRepository
    {
        private AppDbContext db;
        public MileageOrderEFRepository(AppDbContext _db)
        {
            db = _db;
        }

        public void Create(MileOrderEntity model)
        {
            MileOrder mileOrder = new MileOrder
            {
                //Id = model.Id,
                Amount = model.Amount,
                Price = model.Price,
                OrderTime= DateTime.Now,
                TransferPaymentId = model.TransferPaymentId,
                OrderStatus = "完成付款",
                MileNum = model.MileNum,
                MemberId = model.MemberId

            };
            
            db.MileOrders.Add(mileOrder);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<MileOrderEntity> Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<MileOrderEntity> GetAll()
        {
            var Mileage = db.MileOrders.AsNoTracking()
                         //.Include(p => p.Members) 
                         .Select(p => new MileOrderEntity
                         {
                             Id = p.Id,
                             Amount = p.Amount,
                             Price = p.Price,
                             OrderTime = p.OrderTime,
                             TransferPaymentId = p.TransferPaymentId,
                             OrderStatus = p.OrderStatus,
                             MileNum = p.MileNum,
                             MemberId = p.MemberId,
                            
                         })
                         .ToList();

            return Mileage;
        }

        public int Getfinalmile(int memberid)
        {
            throw new NotImplementedException();
        }

        public void Update(MileOrderEntity model)
        {
            throw new NotImplementedException();
        }
    }
}
