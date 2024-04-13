using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using SparkleAir.BLL.Service.Members;
using SparkleAir.BLL.Service.Payment;
using SparkleAir.DAL.EFRepository.Payment;
using SparkleAir.IDAL.IRepository.Members;
using SparkleAir.IDAL.IRepository.Payment;
using SparkleAir.Infa.Dto.Members;
using SparkleAir.Infa.Dto.Payment;
using SparkleAir.Infa.EFModel.Models;

namespace SparkleAir.Front.API.Controllers.Payment
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITransferEFRepository _repo;
        private readonly TransferService _service;

        public TransferController(AppDbContext db)
        {
            _repo = new TransferEFRepository(db);
            _service = new TransferService(_repo); 
            
        }

        /// <summary>
        /// 付款方法"test"  
        ///   
        /// Amount欄位必填
        /// </summary>
        /// <param name="transferdto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public async Task<int> ProcessPayment([FromBody] TransferDto transferdto)
        {
            try
            {
                // 调用 TransferService 中的方法将支付信息保存到数据库中
                var paymentId = await _service.SavePaymentInfo(transferdto);

                return paymentId;
            }
            catch (Exception ex)
            {
                // 处理异常
                throw new Exception("未知付款方式");
            }
        }

        [HttpPost("GetMileOrder")]
        public async Task<BasicApiReturnDto> GetMileOrder([FromBody] TransferDto transferdto)
        {
            try
            {
                // 调用 TransferService 中的方法将支付信息保存到数据库中
                var paymentId = await _service.SavePaymentInfo(transferdto);

                BasicApiReturnDto returnDto = new BasicApiReturnDto()
                {
                    IsVerify = true,
                    Url = $"http://127.0.0.1:8888/MileOrderPayoutSuccess/{paymentId}"
                };
                return returnDto;
            }
            catch (Exception ex)
            {
                BasicApiReturnDto returnDto = new BasicApiReturnDto()
                {
                    IsVerify = false,
                    Error = "Internal server error"
                };
                // 如果發生異常，記錄錯誤並返回錯誤狀態碼
                return returnDto;
            }
        }


        [HttpPost("GetLuggageOrder")]
        public async Task<BasicApiReturnDto> GetLuggageOrder([FromBody] TransferDto transferdto)
        {
            try
            {
                // 调用 TransferService 中的方法将支付信息保存到数据库中
                var paymentId = await _service.SavePaymentInfo(transferdto);

                BasicApiReturnDto returnDto = new BasicApiReturnDto()
                {
                    IsVerify = true,
                    Url = $"http://127.0.0.1:8888/LuggageOrderPayoutSuccess/{paymentId}"
                };
                return returnDto;
            }
            catch (Exception ex)
            {
                BasicApiReturnDto returnDto = new BasicApiReturnDto()
                {
                    IsVerify = false,
                    Error = "Internal server error"
                };
                // 如果發生異常，記錄錯誤並返回錯誤狀態碼
                return returnDto;
            }

        }

        [HttpPost("GetTicketOrder")]
        public async Task<BasicApiReturnDto> GetTicketOrder([FromBody] TransferDto transferdto)
        {
            try
            {
                // 调用 TransferService 中的方法将支付信息保存到数据库中
                var paymentId = await _service.SavePaymentInfo(transferdto);

                BasicApiReturnDto returnDto = new BasicApiReturnDto()
                {
                    IsVerify = true,
                    Url = $"http://127.0.0.1:8888/TicketOrderPayoutSuccess/{paymentId}"
                };
                return returnDto;
            }
            catch (Exception ex)
            {
                BasicApiReturnDto returnDto = new BasicApiReturnDto()
                {
                    IsVerify = false,
                    Error = "Internal server error"
                };
                // 如果發生異常，記錄錯誤並返回錯誤狀態碼
                return returnDto;
            }
        }

        [HttpPost("GetTaxfreeOrder")]
        public async Task<BasicApiReturnDto> GetTaxfreeOrder([FromBody] TransferDto transferdto)
        {
            try
            {
                // 调用 TransferService 中的方法将支付信息保存到数据库中
                var paymentId = await _service.SavePaymentInfo(transferdto);

                BasicApiReturnDto returnDto = new BasicApiReturnDto()
                {
                    IsVerify = true,
                    Url = $"http://127.0.0.1:8888/TaxfreeOrderPayoutSuccess/{paymentId}"
                };
                return returnDto;
            }
            catch (Exception ex)
            {
                BasicApiReturnDto returnDto = new BasicApiReturnDto()
                {
                    IsVerify = false,
                    Error = "Internal server error"
                };
                // 如果發生異常，記錄錯誤並返回錯誤狀態碼
                return returnDto;
            }
        }
    }
}
