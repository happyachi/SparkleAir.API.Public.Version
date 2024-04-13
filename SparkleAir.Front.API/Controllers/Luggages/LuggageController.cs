using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparkleAir.BLL.Service.Airports;
using SparkleAir.BLL.Service.Luggage;
using SparkleAir.BLL.Service.LuggageOrderService;
using SparkleAir.DAL.EFRepository.Luggages;
using SparkleAir.IDAL.IRepository.Luggage;
using SparkleAir.IDAL.IRepository.LuggageOrders;
using SparkleAir.Infa.Dto.Airport;
using SparkleAir.Infa.Dto.Luggage;
using SparkleAir.Infa.EFModel.Models;

namespace SparkleAir.Front.API.Controllers.Luggages
{
    [Route("api/[controller]")]
    [ApiController]
    public class LuggageController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly LuggageService _luggageService;
        private readonly ILuggageRepository _luggageEFRepository;

        public LuggageController(AppDbContext context)
        {
            _luggageEFRepository = new LuggageEFRepository(context);
            _luggageService = new LuggageService(_luggageEFRepository);
        
        }

        [HttpGet("GetAll")]
        public List<LuggageDto> GetAll()
        {
            return _luggageService.GetAll();
        }


      //  private readonly LuggageOrderService _luggageOrderService;
      //  private readonly ILuggageOrderRepository _luggageOrderRepository;

      //public LuggageOrderController(AppDbContext context)
      //  {
      //      _luggageRepository = new LuggageEFRepository(context);
      //      _luggageService = new LuggageService(_luggageRepository);
      //  }


    }
}
