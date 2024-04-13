using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparkleAir.BLL.Service.TaxFree;
using SparkleAir.DAL.EFRepository.TaxFree;
using SparkleAir.IDAL.IRepository.TaxFree;
using SparkleAir.Infa.Dto.TaxFree;
using SparkleAir.Infa.EFModel.Models;

namespace SparkleAir.Front.API.Controllers.TaxFree
{
    [Route("api/[controller]")]
    [ApiController]
    public class TFresevelistController : ControllerBase
    {
        private readonly ITFReservelist _repository;
        private readonly TFReservelistService _service;

        public TFresevelistController(AppDbContext context)
        {
            _repository = new TFResevelistEFRepository(context);
            _service = new TFReservelistService(_repository);
        }

        [HttpGet]
        public async Task<IEnumerable<TFReservelistsDto>> Get()
        {
            return _service.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TFReservelistsDto>> Getid(int id)
        {
            var item = _service.Getid(id);
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] TFReservelistsDto dto)
        {
            try
            {
                _service.Create(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"無此品項 {ex.Message}");
            }
        }





        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"無此品項 {ex.Message}");
            }
        }
    }
}
