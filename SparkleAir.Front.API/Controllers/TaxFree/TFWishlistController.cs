using ECPay.Payment.Integration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SparkleAir.BLL.Service.TaxFree;
using SparkleAir.DAL.EFRepository.TaxFree;
using SparkleAir.Front.API.Models;
using SparkleAir.IDAL.IRepository.TaxFree;
using SparkleAir.Infa.Dto.TaxFree;
using SparkleAir.Infa.EFModel.Models;

namespace SparkleAir.Front.API.Controllers.TaxFree
{
    [Route("api/[controller]")]
    [ApiController]
    public class TFWishlistController : ControllerBase

    {
        private AppDbContext _db;
        private readonly ITFWishlist _repository;
        private readonly TFWishlistService _service;

        public TFWishlistController(AppDbContext context)

        {
            _db = context;
            _repository = new TFWishlistEFRepository(context);
            _service = new TFWishlistService(_repository);
        }

        [HttpGet]
        public async Task<IEnumerable<TFWishlistDto>> Get()
        {
            return _service.Get();
        }
       
        [HttpGet("member{id}")]
        public async Task<IEnumerable<TFWishlistDto>> Getbymemberid(int id)
        {
            return _service.Getid(id);
        }

        [HttpPost("ToggleWishlistItem")]
        public async Task<IActionResult> ToggleWishlistItem(int memberId, int itemId)
        {
            try
            {
                var existingItem = await _db.Tfwishlists
                    .FirstOrDefaultAsync(x => x.MemberId == memberId && x.TfitemsId == itemId);

                if (existingItem != null)
                {
                    // 如果項目已存在，則刪除它
                    _db.Tfwishlists.Remove(existingItem);
                    await _db.SaveChangesAsync();

                    return Ok("Item removed from wishlist successfully.");
                }
                else
                {
                    // 如果項目不存在，則添加它
                    var newItem = new Infa.EFModel.Models.Tfwishlist
                    {
                        MemberId = memberId,
                        TfitemsId = itemId,
                    };

                    _db.Tfwishlists.Add(newItem);
                    await _db.SaveChangesAsync();

                    return Ok("Item added to wishlist successfully.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




    }
}
