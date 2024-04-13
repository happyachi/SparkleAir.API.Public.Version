using SparkleAir.IDAL.IRepository.Payment;
using SparkleAir.Infa.Dto.Payment;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace SparkleAir.BLL.Service.Payment.PaymentMethod
{
    public class ByTest : IPaymentMethod
    {
        public int ProcessPayment(TransferDto dto, ITransferEFRepository _repo)
        {
            var data = new TransferEntity
            {
                TransferMethodsId = 3,
                PaymentDate = DateTime.Now,
                PaymentAmount = dto.PaymentAmount,
                Info = dto.Info !=null? dto.Info: "test",
            };

            var paymentId=_repo.SavePaymentInfo(data);
            return paymentId;
        }

       
    }
}
