using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparkleAir.BLL.Service.Airtype_Owns;
using SparkleAir.DAL.EFRepository.Airtype_Owns;
using SparkleAir.IDAL.IRepository.Airtype_Owns;
using SparkleAir.Infa.Dto.Airtype_Owns;
using SparkleAir.Infa.EFModel.Models;

namespace SparkleAir.Front.API.Controllers.Airplane
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirSeatsController : ControllerBase
    {
        private readonly IAirSeatRepository _repo;
        private readonly AirSeatService _service;
        public AirSeatsController(AppDbContext context)
        {
            _repo = new AirSeatRepository(context);
            _service = new AirSeatService(_repo);
        }

        [HttpGet("GetAll")]
        public List<AirSeatDto> GetAByFlightModel(string flightModel)
        {
            var datas = _service.GetByFlightModel(flightModel);
            return datas;
        }
    }
}
