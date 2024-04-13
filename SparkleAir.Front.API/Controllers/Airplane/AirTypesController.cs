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
    public class AirTypesController : ControllerBase
    {
        private readonly IPlaneRepository _repo;
        private readonly PlaneService _service;
        public AirTypesController(AppDbContext context)
        {
            _repo = new PlaneRepository(context);
            _service = new PlaneService(_repo);
        }

        [HttpGet("GetAll")]
        public List<PlaneDto> GetAll()
        {
            var datas = _service.GetAll();
            return datas;
        }
    }

}
