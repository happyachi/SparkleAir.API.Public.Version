using Google.Apis.Drive.v3.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop.Infrastructure;
using SparkleAir.IDAL.IRepository.Luggage;
using SparkleAir.IDAL.IRepository.LuggageOrders;
using SparkleAir.IDAL.IRepository.MileOrder;
using SparkleAir.Infa.Dto.Airport;
using SparkleAir.Infa.Dto.Luggage;
using SparkleAir.Infa.Dto.LuggageOrders;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.Airports;
using SparkleAir.Infa.Entity.Luggage;
using SparkleAir.Infa.Entity.LuggageOrders;
using SparkleAir.Infa.Utility.Helper.Notices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.BLL.Service.LuggageOrderService
{
    public class LuggageOrderService
    {
        private readonly ILuggageOrderRepository _repo;
        private readonly IConfiguration _configuration;

        public LuggageOrderService(ILuggageOrderRepository repo, IConfiguration configuration) //接收IAirportRepository,方便可以使用Dapper或是EF
        {
            _repo = repo;
            _configuration = configuration;
        }


        //取全部
        public List<LuggageOrderDto> GetAll()
        {
            List<LuggageOrderEntity> entity = _repo.GetAll();

            List<LuggageOrderDto> dto = entity.Select(p => new LuggageOrderDto
            {
                Id = p.Id,
                FlightCode = p.FlightCode,
                TicketInvoicingDetailId = p.TicketInvoicingDetailId,
                TicketInvoicingDetailName = p.TicketInvoicingDetailName,
                LuggageId = p.LuggageId,
                LuggagePrice = p.LuggagePrice,
                Amount = p.Amount,
                Price = p.Price,
                OrderTime = p.OrderTime,
                TransferPaymentsId = p.TransferPaymentsId,
                OrderStatus = p.OrderStatus,
                LuggageNum = p.LuggageNum,


            }).ToList();
            return dto;
        }



        public int Create(LuggageOrderDto dto)
        {
            // 先去看資料庫已經到幾號了
            var LuggageNum = _repo.GetAllLuggage().Select(x => x.LuggageNum)
                                           .OrderByDescending(x => x)
                                           .FirstOrDefault();

            if (LuggageNum == null)
            {
                // 產生流水號
                LuggageNum = "LONO0001";
            }
            else
            {
                // 生出最新的流水號
                LuggageNum = LuggageNum.Substring(3, 5);
                LuggageNum = (Convert.ToInt32(LuggageNum) + 1).ToString().PadLeft(5, '0');
                LuggageNum = "LON" + LuggageNum;
            }
           
            
            var GetLuggageprice = _repo.GetLuggage(dto.TicketInvoicingDetailId); //取得某筆TicketInvoicingDetailId的整個Luggage

           
            LuggageOrderEntity entity = new LuggageOrderEntity
            {
                //Id = dto.Id,
                TicketInvoicingDetailId = dto.TicketInvoicingDetailId,
                LuggageId = GetLuggageprice.Id,
                Amount = dto.Amount,
                Price = dto.Price,
                OrderTime = DateTime.Now,
                TransferPaymentsId = dto.TransferPaymentsId,
                OrderStatus = "已付款",
                LuggageNum = LuggageNum,

            };
            _repo.Create(entity);  //沒呼叫AirportEFRepository是因為AirportEFRepository有實作interface,所以呼叫_repo也就好


            /////------發送Email
            string emailDetail = $@"
             <html>
                <body>
                    <h1>託運加購數量:　{dto.Amount}件</h1>
                    <img src=""https://lh3.googleusercontent.com/d/1p2S-Ea-F1rbnRwz0-xPmilmLudpDo4-U"" alt=""图片"" width=""400""
                       height=""300""    />
                </body>
             </html>
            ";


            var emailSetting = _configuration.GetSection("SendEmailSetting").Get<Dictionary<string, string>>();
            SendEmailHelper.SendEmail(
               emailSetting["FromEmail"],
               emailSetting["FromPassword"],
               "sparkle.airline@gmail.com", //收件人信箱
               "【託運購買明細】", // 主旨標題
              emailDetail); //信件內容
            //////////--------發送Email結束


            return entity.Id;

           

        }

        //取得 LuggagePrice
        public int GetLuggageprice(int TicketInvoicingDetailId)
        {
            return _repo.GetLuggage(TicketInvoicingDetailId).BookPrice;
        }


        //取得一筆
        public LuggageOrderDto Get(int id)
        {
            LuggageOrderEntity entity = _repo.Get(id);

            LuggageOrderDto dto = new LuggageOrderDto
            {
                Id = entity.Id,
                FlightCode = entity.FlightCode,
                TicketInvoicingDetailId = entity.TicketInvoicingDetailId,
                TicketInvoicingDetailName = entity.TicketInvoicingDetailName,
                LuggageId = entity.LuggageId,
                LuggagePrice = entity.LuggagePrice,
                Amount = entity.Amount,
                Price = entity.Price,
                OrderTime = entity.OrderTime,
                TransferPaymentsId = entity.TransferPaymentsId,
                OrderStatus = entity.OrderStatus,
                LuggageNum = entity.LuggageNum,
            };

            return dto;
        }


    }
}
