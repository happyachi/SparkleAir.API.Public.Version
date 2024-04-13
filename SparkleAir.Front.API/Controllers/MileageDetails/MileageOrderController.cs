using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparkleAir.BLL.Service.MileageDetails;

using SparkleAir.BLL.Service.MileOrders;
using SparkleAir.DAL.EFRepository.Members;
using SparkleAir.DAL.EFRepository.MileageDetails;

using SparkleAir.DAL.EFRepository.MileOrders;
using SparkleAir.IDAL.IRepository.Members;
using SparkleAir.IDAL.IRepository.MileageDetails;
using SparkleAir.IDAL.IRepository.MileOrder;
using SparkleAir.Infa.Dto.MileOrder;
using SparkleAir.Infa.EFModel.Models;
using System.Configuration;

namespace SparkleAir.Front.API.Controllers.MileageDetails
{
    [Route("api/[controller]")]
    [ApiController]
    public class MileageOrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly MileageOrderService _MileOrderService;
        private readonly IMileageOrderRepository _IMileageOrderRepository;

        private readonly IMemberRepository _mm;
        private readonly IMileageDetailRepository _mileageDetailRepository;
        private readonly IConfiguration _configuration;


        public MileageOrderController(AppDbContext context, IConfiguration configuration)
        {
            _mm = new MemberEFRepository(context);
            _mileageDetailRepository = new MileageDetailEFRepository(context);
            _IMileageOrderRepository = new MileageOrderEFRepository(context);
            _configuration = configuration;
            _MileOrderService = new MileageOrderService(_mileageDetailRepository, _mm, _IMileageOrderRepository , _configuration);
        }

        [HttpPost("Create")]
        public async Task<string> Create([FromBody] MileOrderDto mileageOrderDto)
        {
            try
            {
                _MileOrderService.Create(mileageOrderDto);


                return "成功";
            }
            catch (Exception ex)
            {
                return "失敗";
            }

        }

        //測試取值
        [HttpPost("dto")]
        public string cae(MileOrderDto dto)
        {
            return _MileOrderService.CreateOrder(dto);
        }




        //金流結帳後的 MileageCreate
        //[HttpPost("MileageCreate")]
        //public async Task<string> MileageCreate([FromBody] MileOrderDto mileageOrderDto)
        //{
        //    try
         //   {
         //       _MileOrderService.Create(mileageOrderDto);


          //      return "成功";
          //  }
           // catch (Exception ex)
          //  {
           //     return "失敗";
           // }

            //}


    }
}
