using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SparkleAir.BLL.Service.AirFlights;
using SparkleAir.DAL.DapperRepository.AirFlights;
using SparkleAir.DAL.EFRepository.AirFlights;
using SparkleAir.IDAL.IRepository.AirFlights;
using SparkleAir.Infa.Criteria.AirFlights;
using SparkleAir.Infa.Dto.AirFlights;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.AirFlightsEntity;
using SparkleAir.Infa.Utility.Helper;

namespace SparkleAir.Front.API.Controllers.AirFlights
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirFlightsController : ControllerBase
    {
        #region ctor
        private AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        private readonly IAirFlightRepository _airFlightDPRepo;
        private readonly AirFlightService _airFlightDPService;

        private readonly IAirFlightRepository _airFlightRepo;
        private readonly AirFlightService _airFlightService;

        private readonly IAirTakeOffRepository _airTakeOffRepo;
        private readonly AirTakeOffService _airTakeOffService;

        private readonly IAirTicketPriceRepository _airTicketPriceRepo;
        private readonly AirTicketPriceService _airTicketPriceService;

        private readonly IAirFlightSeatsRepository _airFlightSeatsRepo;
        private readonly AirFlightSeatsService _airFlightSeatsService;
        public AirFlightsController(AppDbContext context, IConfiguration configuration)
        {
            _airFlightRepo = new AirFlightEFRepository(context);
            _airFlightService = new AirFlightService(_airFlightRepo, _appDbContext);
            _airFlightSeatsRepo = new AirFlightSeatsEFRepository(context);
            _airFlightSeatsService = new AirFlightSeatsService(_airFlightSeatsRepo);
            // Dapper
            _configuration = configuration;
            var connectionString = configuration.GetConnectionString("AppDbContext");
            _airFlightDPRepo = new AirFlightDapperRepository(connectionString);
            _airFlightDPService = new AirFlightService(_airFlightDPRepo, _appDbContext);

            _airTakeOffRepo = new AirTakeOffEFRepository(context);
            _airTakeOffService = new AirTakeOffService(_airTakeOffRepo, _configuration);

            _airTicketPriceRepo = new AirTicketPriceEFRepository(context);
            _airTicketPriceService = new AirTicketPriceService(_airTicketPriceRepo);
        }
        #endregion

        //搜尋班機(出發地目的地)
        [HttpGet("SearchFlight")]
        public List<AirFlightDto> SearchScheduleFlight([FromQuery] AirFlightCriteria criteria)
        {
            var search = new AirFlightCriteria
            {
                FlightCode = criteria.FlightCode,
                DepartureAirport = criteria.DepartureAirport,
                ArrivalAirport = criteria.ArrivalAirport,
                DepartureStartTime = criteria.DepartureStartTime,
                DepartureEndTime = criteria.DepartureEndTime,
                ArrivalStartTime = criteria.ArrivalStartTime,
                ArrivalEndTime = criteria.ArrivalEndTime,
            };
            var result = _airFlightService.Search(search);
            return result;
        }

        //獲取目前班表飛機
        [HttpGet("GetScheduleFlights")]
        public async Task<List<AirFlightDto>> GetAirFlights()
        {
            var datas = await _airFlightDPService.GetAllAsync();
            return datas;
        }

        //獲取某一台飛機
        [HttpGet("GetFlight")]
        public AirFlightDto GetFlight(int id)
        {
            var flight = _airFlightService.GetById(id);
            return flight;
        }

        //獲取班機位置
        [HttpGet("GetFlightSeat")]
        public List<AirFlightSeatsDto> GetSeat(int flightId)
        {
            var seats = _airFlightSeatsService.GetById(flightId);
            return seats;
        }

        //依照出發地&&目的地 OR FlightCode 搜尋班機動態
        [HttpGet("GetTakeOffStatue")]
        public List<AirTakeOffDto> GetStatus([FromQuery] TakeOffCriteria criteria)
        {
            return _airTakeOffService.GetAirTakeOffList(criteria);
        }

        //獲取當前班機浮動價格
        [HttpGet("GetTicketPrice")]
        public List<AirTicketPriceDto> GetTicketPrices(int id,string flightModel)
        {
            return _airTicketPriceService.GetTicketPrice(id,flightModel);
        }
        [HttpGet("HangFire")]
        public void HangFire()
        {

            var service = new TakeOffService(_appDbContext);
          var criteria = new TakeOffCriteria
            {
                FlightCode ="",
                DepartureAirport="",
                ArrivalAirport=""
            };
            RecurringJob.AddOrUpdate(() =>service.GetAirTakeOff(criteria), "*/1 * * * *");
        }
        
    }
}
