using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SparkleAir.BLL.Service.TaxFree;
using SparkleAir.DAL.EFRepository.Members;
using SparkleAir.DAL.EFRepository.TaxFree;
using SparkleAir.IDAL.IRepository.TaxFree;
using SparkleAir.Infa.Criteria.Members;
using SparkleAir.Infa.Dto.Members;
using SparkleAir.Infa.Dto.TaxFree;
using SparkleAir.Infa.EFModel.Models;

namespace SparkleAir.Front.API.Controllers.TaxFree
{
    [Route("api/[controller]")]
    [ApiController]
    public class TFitemController : ControllerBase
    {
        private readonly ITFRepository _repository;
        private readonly TaxFreeService _service;
        private readonly TFItemEFRepository _categoryrepo;

        public TFitemController(AppDbContext context)
        {
            _repository = new TFItemEFRepository(context);
            _service = new TaxFreeService(_repository);
            _categoryrepo = new TFItemEFRepository(context);
        }

        [HttpGet]
        public async Task<IEnumerable<TFItemDto>> Get()
        {
            return _service.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TFItemDto>>> Getid(int id)
        {
            var item = _service.Getid(id);
            return Ok(item);
        }
      

        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] TFItemDto dto)
        //{
        //    var tfitem = new TFItemDto
        //    {
        //        Id = dto.Id,
        //        Name = dto.Name,
        //        TFCategoriesId = dto.TFCategoriesId,
        //        SerialNumber = dto.SerialNumber,
        //        Image = dto.Image,
        //        Quantity = dto.Quantity,
        //        UnitPrice = dto.UnitPrice,
        //        Description = dto.Description,
        //        IsPublished = dto.IsPublished

        //    };

        //    //上傳圖片

        //    _service.Create(tfitem);

        //    return Ok(new { Id = tfitem.Id });
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TFItemDto dto)
        {


            var tfitem = new TFItemDto
            {
                Id = dto.Id,
                Name = dto.Name,
                TFCategoriesId = dto.TFCategoriesId,
                SerialNumber = dto.SerialNumber,
                Image = dto.Image,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice,
                Description = dto.Description,
                IsPublished = dto.IsPublished
            };

            _service.Getid(id);


            return Ok(new { Id = tfitem.Id });
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

        // GET: api/TFitem/categories
        [HttpGet("categories")]
        public IEnumerable<TFCategoryDto> GetCategories()
        {
            var categories = _categoryrepo.GetCategories();

            var categoryDtos = new List<TFCategoryDto>();
            foreach (var category in categories)
            {
                categoryDtos.Add(new TFCategoryDto
                {
                    Id = category.Id,
                    Category = category.Category
                });
            }

            return categoryDtos;
        }

        // GET: api/TFitem/categories/{id}
        [HttpGet("categories/{id}")]
        public ActionResult<TFCategoryDto> GetCategoryById(int id)
        {
            var category = _categoryrepo.GetCategoryById(id);

            if (category == null)
            {
                return NotFound(); // 如果找不到對應的分類，返回 NotFound
            }

            var categoryDto = new TFCategoryDto
            {
                Id = category.Id,
                Category = category.Category
            };

            return categoryDto;



        }
    }
}
