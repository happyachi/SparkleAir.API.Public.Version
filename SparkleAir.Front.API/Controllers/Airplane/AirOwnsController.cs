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
    public class AirOwnsController : ControllerBase
    {
    
            private readonly IOwnRepository _repo;
            private readonly OwnService _service;
            public AirOwnsController(AppDbContext context)
            {
                _repo = new OwnRepository(context);
                _service = new OwnService(_repo);
            }

            [HttpGet("GetAll")]
            public List<OwnDto> GetAll()
            {
                var datas = _service.GetAll();
                return datas;
            }
    }
}
