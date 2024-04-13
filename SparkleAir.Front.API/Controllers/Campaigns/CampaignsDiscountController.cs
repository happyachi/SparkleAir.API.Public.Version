using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparkleAir.BLL.Service.Campaigns;
using SparkleAir.DAL.EFRepository.Campaigns;
using SparkleAir.DAL.EFRepository.Members;
using SparkleAir.DAL.EFRepository.TaxFree;
using SparkleAir.IDAL.IRepository.Campaigns;
using SparkleAir.IDAL.IRepository.TaxFree;
using SparkleAir.Infa.Dto.Campaigns;
using SparkleAir.Infa.EFModel.Models;

namespace SparkleAir.Front.API.Controllers.Campaigns
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignsDiscountController : ControllerBase
    {
        private readonly ICampaignsDiscountsRepository _repo;
        private readonly CampaignsDiscountsService _service; 

        public CampaignsDiscountController(AppDbContext context) {

            var _memberrepo = new MemberEFRepository(context);
            var _tfrepo = new TFItemEFRepository (context);
            var _tforderrepo = new TFOrderlistEFRepository(context);
            _repo = new CampaignsDiscountsEFRepository(context);
            _service = new CampaignsDiscountsService(context,_repo, _memberrepo, _tfrepo, _tforderrepo);
        }

        [HttpGet]
        public async Task<IEnumerable<CampaignsDiscountDto>> GetAll()
        {
            List<CampaignsDiscountDto> dto = _service.GetAllFiltered();
            //List<CampaignsDiscountDto> dto = _service.GetAll();
            return dto;
        }

        [HttpGet("Discount/{id}")]
        public async Task<CampaignsDiscountDto> Get(int id)
        {
            CampaignsDiscountDto dto = _service.Get(id);
            return dto;
        }

        //自動生成DiscountTFPr  oducts Table
        [HttpPost]
        public void CreateDiscountsTable (int discountId)
        {
            _service.CreateDiscountsTable(discountId);
        }

        //輸入Productid 找到對應活動
        [HttpGet("DiscountProduct/{productid}")]
        public async Task<ActionResult<List<CampaignsDiscountDto>>> GetDiscountsByProductId(int productid)
        {
            try
            {
                var discounts = _service.GetDiscountsByProductId(productid);
                if (discounts == null || discounts.Count == 0)
                {
                    return NotFound("No discounts found for the given product ID.");
                }
                return Ok(discounts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data from the database: {ex.Message}");
            }
        }

        //輸入ticketId 計算折扣
        [HttpGet("CartDiscount/{ticketid}")]
        public IEnumerable<DiscountDto> CartDiscount(int ticketid)
        //public DiscountDto CartDiscount(int ticketid)
        {
            //var request = _service.Pos(ticketid);
            var request = _service.Pos(ticketid).Where(x => x.Products.Count != 0);
            return request;
            //return "OK";

        }

        //輸入discountId 找到正在參加的產品們
        [HttpGet("SearchForProducts/{discountId}")]
        public ActionResult<List<Tfitem>> SearchForProducts(int discountId)
        {
            return _service.SearchForProducts(discountId);
        }


    }
}
