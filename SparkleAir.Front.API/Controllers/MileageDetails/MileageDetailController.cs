using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparkleAir.BLL.Service.Airports;
using SparkleAir.BLL.Service.Luggage;
using SparkleAir.BLL.Service.MileageDetails;
using SparkleAir.DAL.EFRepository.Members;
using SparkleAir.DAL.EFRepository.MileageDetails;
using SparkleAir.IDAL.IRepository.Members;
using SparkleAir.IDAL.IRepository.MileageDetails;
using SparkleAir.Infa.Dto.Luggage;
using SparkleAir.Infa.Dto.MileageDetails;
using SparkleAir.Infa.EFModel.Models;

namespace SparkleAir.Front.API.Controllers.MileageDetails
{
    [Route("api/[controller]")]
    [ApiController]
    public class MileageDetailController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly MileageDetailService _mileageDetailService;
        private readonly IMileageDetailRepository _mileageDetailRepository;
        private readonly IMemberRepository _mm;
 

        public MileageDetailController(AppDbContext context)
        {
            _mm = new MemberEFRepository(context);
            _mileageDetailRepository = new MileageDetailEFRepository(context);
            _mileageDetailService = new MileageDetailService( context);
                   
        }

        //測試里程扣抵的create 
        [HttpPost("DiscountMileCreate")]
        public int DiscountMileCreate(MileageDetailDto dto)
        {
            return _mileageDetailService.Create(dto);
        }

        //測試里程過期的create 
        [HttpPost("ExpiredCreate")]
        public void ExpiredCreate()
        {
            //重複執行，預設為每天00:00啟動
            //RecurringJob.AddOrUpdate(() => _mileageDetailService.ExpiredCreate(), Cron.Daily);
            _mileageDetailService.ExpiredCreate();
        }



        [HttpGet("GetAll")]
        public List<MileageDetailDto> GetAll()
        {
            return _mileageDetailService.GetAll();
        }


        [HttpGet("Get/{id}")]
        public List<MileageDetailDto> Get(int id)
        {
            return _mileageDetailService.Get(id);
        }

        //取得已折抵里程明細表紀錄
        [HttpGet("Getapplydetail/{id}")]
        public List<MileApplyDto> Getapplydetail(int id)
        {
            return _mileageDetailService.Getapplydetail(id);
        }

        //取的某會員的最新final miledetail
        [HttpGet("Getfinalmile/{id}")]
        public int Getfinalmile(int id)
        {
            return _mileageDetailService.Getfinalmile(id);
        }

    }
}
