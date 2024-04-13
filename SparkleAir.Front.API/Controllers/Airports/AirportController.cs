using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparkleAir.BLL.Service.Airports;
using SparkleAir.BLL.Service.MileageDetails;
using SparkleAir.DAL.EFRepository.Airports;
using SparkleAir.IDAL.IRepository.Airport;
using SparkleAir.Infa.Dto.Airport;
using SparkleAir.Infa.Dto.MileageDetails;
using SparkleAir.Infa.EFModel.Models;

namespace SparkleAir.Front.API.Controllers.Airports
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly AirportService _airportService;
        private readonly IAirportRepository _airportRepository;
        public AirportController(AppDbContext context)
        {
            _airportRepository = new AirportEFRepository(context);
            _airportService = new AirportService(_airportRepository);
        }

        [HttpGet("GetAll")]
        public List<AirportDto> GetAll()
        {
            return _airportService.GetAll();
        }

        [HttpGet("Get/{id}")]
        public AirportDto Getid(int id)
        {
            return _airportService.Getid(id);
        }

    }
}
