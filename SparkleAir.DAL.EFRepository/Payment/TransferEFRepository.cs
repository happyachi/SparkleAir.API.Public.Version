using Microsoft.EntityFrameworkCore;
using SparkleAir.IDAL.IRepository.Payment;
using SparkleAir.Infa.Dto.Airtype_Owns;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.Airtype_Owns;
using SparkleAir.Infa.Entity.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.DAL.EFRepository.Payment
{
    public class TransferEFRepository: ITransferEFRepository
    {
        private readonly AppDbContext db;
        public TransferEFRepository(AppDbContext context)
        {
            db = context;
        }

        public List<TransferEntity> GetAll()
        { 
            var transfers = db.TransferPayments.AsNoTracking().Include(p => p.TransferMethods).Select(p => new TransferEntity
        {
            Id = p.Id,
            TransferMethodsId = p.TransferMethodsId,
            PaymentDate = p.PaymentDate,
            PaymentAmount = p.PaymentAmount,
            Info = p.Info
        }).ToList();
        return transfers;

        }
        public TransferEntity Get(int id)
        {
            var result = GetAll().SingleOrDefault(x => x.Id == id);
            return result;
        }

        public int SavePaymentInfo(TransferEntity entity)
        {

            var transferPayment = new TransferPayment
            {
                TransferMethodsId = entity.TransferMethodsId,
                PaymentDate = entity.PaymentDate,
                PaymentAmount = entity.PaymentAmount,
                Info = entity.Info
            };

            db.TransferPayments.Add(transferPayment);
            db.SaveChanges();

            return transferPayment.Id;
        }
    }
}
