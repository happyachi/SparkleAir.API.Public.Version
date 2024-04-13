using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparkleAir.BLL.Service.Luggage;
using SparkleAir.BLL.Service.LuggageOrderService;
using SparkleAir.BLL.Service.MileageDetails;
using SparkleAir.DAL.EFRepository.LuggageOrders;
using SparkleAir.DAL.EFRepository.Luggages;
using SparkleAir.Infa.Dto.Luggage;
using SparkleAir.Infa.Dto.LuggageOrders;
using SparkleAir.Infa.EFModel.Models;

namespace SparkleAir.Front.API.Controllers.Luggages
{
    [Route("api/[controller]")]
    [ApiController]
    public class LuggageOrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly LuggageOrderEFRepository _luggageEFRepository;
        private readonly LuggageOrderService _luggageOrderService;
        private readonly IConfiguration _configuration;


        public LuggageOrderController(AppDbContext context, IConfiguration configuration)
        {
            _luggageEFRepository = new LuggageOrderEFRepository(context);
            _configuration = configuration;
            _luggageOrderService = new LuggageOrderService(_luggageEFRepository , _configuration);

        }

        [HttpGet("GetAll")]
        public List<LuggageOrderDto> GetAll()
        {
            return _luggageOrderService.GetAll();
        }


        //測試取得行李價格
        [HttpGet("GetLuggageprice/{TicketInvoicingDetailId}")]
        public int GetLuggageprice(int TicketInvoicingDetailId)
        {
            return _luggageOrderService.GetLuggageprice(TicketInvoicingDetailId);
        }


        //里程Order 的create 
        [HttpPost("Create")]
        public string Create([FromBody]LuggageOrderDto dto)
        {
            try
            {
                var id = _luggageOrderService.Create(dto);


                return "成功";
            }
            catch (Exception ex)
            {
                return "失敗";
            }
            
          
        }
    }
}
