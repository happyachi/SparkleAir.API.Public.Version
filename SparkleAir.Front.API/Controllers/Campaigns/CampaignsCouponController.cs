using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparkleAir.BLL.Service.Campaigns;
using SparkleAir.DAL.EFRepository.Campaigns;
using SparkleAir.DAL.EFRepository.Members;
using SparkleAir.IDAL.IRepository.Campaigns;
using SparkleAir.Infa.Criteria.Campaigns;
using SparkleAir.Infa.Dto.Campaigns;
using SparkleAir.Infa.EFModel.Models;


namespace SparkleAir.Front.API.Controllers.Campaigns
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignsCouponController : ControllerBase
    {
        private readonly ICampaignsCouponsRepository _repo;
        private readonly CampaignsCouponsService _service;
        private readonly IConfiguration _configuration;

        public CampaignsCouponController(AppDbContext context, IConfiguration configuration)
        {
            var _memberrepo = new MemberEFRepository(context);
            _repo = new CampaignsCouponsEFRepository(context);
            _configuration = configuration;
            _service = new CampaignsCouponsService(_configuration, context);
           
        }

        [HttpGet]
        public async Task<IEnumerable<CampaignsCouponDto>> GetAll()
        {
            List<CampaignsCouponDto> dto = _service.GetAll();
            return dto;
        }

        [HttpGet("Coupon/{id}")]
        public async Task<CampaignsCouponDto> Get(int id)
        {
            CampaignsCouponDto dto = _service.Get(id);
            return dto;
        }

        [HttpGet("search")]
        public List<CampaignsCouponDto> SearchCoupons()
        {
            CampaignsCouponSearchCriteria vm = new CampaignsCouponSearchCriteria
            {
                Name = "秋高氣爽旅行趣"
            };
            var list = _service.Search(vm);
           return list;
        }

        [HttpPost]
        //自動生成CampaignsCouponAirFlights Table
        public void CreateAirFlightsTable(int id)
        {
            _service.CreateAirFlightsTable(id);
        }

        //自動生成CampaignsCouponMembers Table
        [HttpPost("CreateCouponMembersTable")]
        public void CreateCouponMembersTable(int couponid)
        {
            _service.CreateCouponMembersTable(couponid);
        }

        //生成CampaignsCouponMembers Table 
        //票券歸戶
        [HttpGet("AddCoupontoMemberId")]
        public void AddCoupontoMemberId (int campaignsCouponId, int memberid)
        {
            _service.AddCoupontoMemberId(campaignsCouponId, memberid);
        }

        [HttpGet("CouponCart")]
        //用戶票券顯示
        public ActionResult<IEnumerable<CampaignsCouponDto>> CouponCart(int memberid, decimal originalPrice)
        {
            try
            {
                var dto = _service.CouponCart(memberid, originalPrice); // 直接调用同步方法
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        //生日券發放
        [HttpPost("GenerateBirthdayCoupon")]
        public async Task<IActionResult> GenerateBirthdayCouponsForThisMonth()
        {
            try
            {
                //await _service.GenerateBirthdayCouponsForThisMonth();
                RecurringJob.AddOrUpdate(() => _service.GenerateBirthdayCouponsForThisMonth(), Cron.Monthly);
                return Ok("當月生日優惠券已成功生成並發放。");
                

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"生成生日優惠券時出錯: {ex.Message}");
            }
        }
    }
}
