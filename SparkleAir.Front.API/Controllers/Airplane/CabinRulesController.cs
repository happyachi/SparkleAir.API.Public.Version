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
    public class CabinRulesController : ControllerBase
    {

        private readonly ICabinRuleRepository _repo;
        private readonly CabinRuleService _service;
        public CabinRulesController(AppDbContext context)
        {
            _repo = new CabinRuleRepository(context);
            _service = new CabinRuleService(_repo);
        }

        [HttpGet("GetAll")]
        public List<CabinRuleDto> GetAll()
        {
            var datas = _service.GetAll();
            return datas;
        }
    }
}
