using Microsoft.Extensions.Configuration;
using SparkleAir.IDAL.IRepository.Members;
using SparkleAir.IDAL.IRepository.MileageDetails;
using SparkleAir.IDAL.IRepository.MileOrder;
using SparkleAir.Infa.Criteria.Members;
using SparkleAir.Infa.Dto.MileageDetails;
using SparkleAir.Infa.Dto.MileOrder;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.Members;
using SparkleAir.Infa.Entity.MileageDetails;
using SparkleAir.Infa.Entity.MileOrder;
using SparkleAir.Infa.Utility.Helper.Notices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.BLL.Service.MileOrders
{
    public class MileageOrderService
    {
        private readonly IMileageDetailRepository _repo;
        private readonly IMemberRepository _mrepo;
        private readonly IMileageOrderRepository _IMileageOrderRepository;

        private readonly IConfiguration _configuration;

        public MileageOrderService(IMileageDetailRepository repo, IMemberRepository mr, IMileageOrderRepository md , IConfiguration configuration)
        {
            _repo = repo;
            _mrepo = mr;
            _IMileageOrderRepository = md;
            _configuration = configuration;
        }


        //新增MileageOrder 
        public void Create(MileOrderDto dto)
        {
            var mileNum = CreateOrder(dto); //接回來的流水號


            CreateDetail(dto, mileNum);

            //MileOrderEntity mileOrder = new MileOrderEntity
            //{
            //    Id = dto.Id,
            //    Amount = dto.Amount,
            //    Price = dto.Price,
            //    OrderTime = dto.OrderTime,
            //    TransferPaymentId = dto.TransferPaymentId,
            //    OrderStatus = dto.OrderStatus,
            //    MileNum = dto.MileNum,
            //    MemberId = dto.MemberId
            //};
            //_IMileageOrderRepository.Create(mileOrder);
        }

        private void CreateDetail(MileOrderDto orderDto, string mileNum)
        {
            // new MileageDetailDto dto
            MileageDetailDto dto = new MileageDetailDto
            {
                MermberIsd = orderDto.MemberId,
                ChangeMile = orderDto.Amount,
                MileReason = "購買里程",
                OrderNumber = mileNum,
            };

            //先取得會員
            var criteria = new MemberGetCriteria
            {
                Id = dto.MermberIsd //這裡是會員編號
            };
            var memb = _mrepo.Get(criteria); //取得某會員的所有資料

            var changemile = dto.ChangeMile >= 0 ? memb.TotalMileage + dto.ChangeMile : memb.TotalMileage;//總里程

            var sb = _repo.Getfinalmile(dto.MermberIsd); //取得某會員的最後一筆里程紀錄

            //todo orderNumber 要寫自動帶入參數

            MileageDetailEntity entity = new MileageDetailEntity
            {
                //Id = dto.Id,
                MermberIsd = dto.MermberIsd,
                TotalMile = changemile,
                OriginalMile = sb,
                ChangeMile = dto.ChangeMile,
                FinalMile = sb + dto.ChangeMile,
                MileValidity = DateTime.Now.AddYears(1),
                MileReason = dto.MileReason,
                OrderNumber = dto.OrderNumber,
                ChangeTime = DateTime.Now,

            };
            _repo.Create(entity);  //沒呼叫AirportEFRepository是因為AirportEFRepository有實作interface,所以呼叫_repo也就好


            MemberEntity member = new MemberEntity
            {
                Id = dto.MermberIsd,
                TotalMileage = changemile,
            };
            _mrepo.Update(member);



            //------發送Email
            string emailDetail = $@"
             <html>
                <body>
                    <h1>里程購買數:　{dto.ChangeMile}　公哩</h1>
                    <img src=""https://lh3.googleusercontent.com/d/1SBCPDBd9NTXniKcyYb8mWFHWtHzSDBI_"" alt=""图片"" width=""300""
                       height=""200""    />
                </body>
             </html>";


            var emailSetting = _configuration.GetSection("SendEmailSetting").Get<Dictionary<string, string>>();
            SendEmailHelper.SendEmail(
               emailSetting["FromEmail"],
               emailSetting["FromPassword"],
               "sparkle.airline@gmail.com", //收件人信箱
               "【里程購買明細】", // 主旨標題
              emailDetail); //信件內容
            //////////--------發送Email結束


        }

        public string CreateOrder(MileOrderDto dto)
        {
            // 先去看資料庫已經到幾號了
            var Mileorder = _IMileageOrderRepository.GetAll().Select(x => x.MileNum)  
                                         .OrderByDescending(x => x)
                                         .FirstOrDefault();

            if (Mileorder == null)
            {
                // 產生流水號
                Mileorder = "MO00001";
            }
            else
            {
                // 生出最新的流水號
                Mileorder = Mileorder.Substring(2,5);
                Mileorder = (Convert.ToInt32(Mileorder) + 1).ToString().PadLeft(5, '0');
                Mileorder = "MO" + Mileorder;
            }
               
            
                
            // num + dto => new entity
            MileOrderEntity entity = new MileOrderEntity
            {
                //Id = dto.Id,
                Amount = dto.Amount,
                Price = dto.Price,
                //OrderTime = dto.OrderTime,
                TransferPaymentId = dto.TransferPaymentId,
                //OrderStatus = dto.OrderStatus,
                MileNum = Mileorder,
                MemberId = dto.MemberId
            };

            //order_repo.Create(entity)
            _IMileageOrderRepository.Create(entity);
            //return 流水號
            return Mileorder;
        }
    }
}
