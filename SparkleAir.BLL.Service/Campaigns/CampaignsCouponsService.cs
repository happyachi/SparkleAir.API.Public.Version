using Microsoft.Extensions.Configuration;
using SparkleAir.DAL.EFRepository.Campaigns;
using SparkleAir.DAL.EFRepository.Members;
using SparkleAir.IDAL.IRepository.Campaigns;
using SparkleAir.IDAL.IRepository.Members;
using SparkleAir.Infa.Criteria.Campaigns;
using SparkleAir.Infa.Criteria.Members;
using SparkleAir.Infa.Dto.Campaigns;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.Campaigns;
using SparkleAir.Infa.Utility.Helper;
using SparkleAir.Infa.Utility.Helper.Campaigns;
using SparkleAir.Infa.Utility.Helper.Notices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SparkleAir.BLL.Service.Campaigns
{
    public class CampaignsCouponsService
    {
        //private readonly ICampaignsCouponsRepository _repo;
        readonly AppDbContext _appDbContext;
        readonly CampaignsCouponsEFRepository _repo;
        //private readonly IMemberRepository _memberrepo;
        readonly MemberEFRepository _memberrepo;
        private readonly IConfiguration _configuration;
        public CampaignsCouponsService(IConfiguration configuration, AppDbContext context)
        {
            //_repo = repo;
            //_memberrepo = memberrepo;
            _configuration = configuration;
            _appDbContext = context;
            _repo = new CampaignsCouponsEFRepository(_appDbContext);
            _memberrepo = new MemberEFRepository(_appDbContext);
        }

        public int Create(CampaignsCouponDto dto)
        {
            // 檢查 datastart 和 dataend 期間不能超過三個月
            if (!CamapignsTimeHelper.IsDateRangeValid(dto.DateStart, dto.DateEnd))
            {
                throw new ArgumentException("活動期間不能超過三個月。");
            }

            // 檢查 datastart 必須是現在的時間半小時以後
            if (!CamapignsTimeHelper.IsStartDateValid(dto.DateStart))
            {
                throw new ArgumentException("開始時間必須在目前時間的半小時後");
            }

            // 檢查 MaximumDiscountAmount 必須小於等於 MinimumOrderValue
            if (!PriceHelper.IsMaximumDiscountValid(dto.MaximumDiscountAmount, dto.MinimumOrderValue))
            {
                throw new ArgumentException("最高折扣金額不得大於最低消費金額。");
            }

            if (dto.DateEnd < dto.DateStart)
            {
                throw new ArgumentException("結束時間不能早於開始時間。");
            }

            if (dto.CampaignId == 4)
            {
                if (dto.DiscountValue >= 99 || dto.DiscountValue < 10)
                {
                    throw new ArgumentException("折數必須介於10-99之間。");
                }
            }

            string status = CamapignsTimeHelper.DetermineStatus(dto.DateStart, dto.DateEnd);

            CampaignsCouponEntity entity = new CampaignsCouponEntity(

              dto.CampaignId,
              dto.Name,
              dto.DateStart,
              dto.DateEnd,
              DateTime.Now,
              status,
              dto.DiscountQuantity,
              dto.DiscountValue,
              dto.DiscountQuantity,
              dto.MinimumOrderValue,
              dto.MaximumDiscountAmount,
              dto.Code,
              dto.DisplayDescription,
              dto.MemberCriteria,
              dto.AirFlightsCriteria,
              dto.Type
                //dto.Id
                );

            _repo.Create(entity);
            return entity.Id;
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
        }

        public CampaignsCouponDto Get(int id)
        {
            CampaignsCouponEntity entity = _repo.Get(id);
            CampaignsCouponDto dto = new CampaignsCouponDto
            {
                Id = id,
                CampaignId = entity.CampaignId,
                Name = entity.Name,
                DateCreated = entity.DateCreated,
                DateStart = entity.DateStart,
                DateEnd = entity.DateEnd,
                Status = entity.Status,
                DiscountQuantity = entity.DiscountQuantity,
                DiscountValue = entity.DiscountValue,
                AvailableQuantity = entity.AvailableQuantity,
                MinimumOrderValue = entity.MinimumOrderValue,
                MaximumDiscountAmount = entity.MaximumDiscountAmount,
                Code = entity.Code,
                DisplayDescription = entity.DisplayDescription,
                MemberCriteria = entity.MemberCriteria,
                AirFlightsCriteria = entity.AirFlightsCriteria,
                Campaign = entity.Campaign,
                Type = entity.Type
            };
            return dto;
        }

        public List<CampaignsCouponDto> GetAll()
        {
            List<CampaignsCouponEntity> entity = _repo.GetAll();


            List<CampaignsCouponDto> dto = entity.Select(c => new CampaignsCouponDto
            {
                Id = c.Id,
                Name = c.Name,
                CampaignId = c.CampaignId,
                DateStart = c.DateStart,
                DateEnd = c.DateEnd,
                DateCreated = c.DateCreated,
                Status = CamapignsTimeHelper.DetermineStatus(c.DateStart,c.DateEnd),
                DiscountQuantity = c.DiscountQuantity,
                DiscountValue = c.DiscountValue,
                AvailableQuantity = c.AvailableQuantity,
                MinimumOrderValue = c.MinimumOrderValue,
                MaximumDiscountAmount = c.MaximumDiscountAmount,
                Code = c.Code,
                DisplayDescription = c.DisplayDescription,
                MemberCriteria = c.MemberCriteria,
                AirFlightsCriteria = c.AirFlightsCriteria,
                Type = c.Type
            }).ToList();

            return dto;
        }

        public void Update(CampaignsCouponDto dto)
        {
            // 檢查 datastart 和 dataend 期間不能超過三個月
            if (!CamapignsTimeHelper.IsDateRangeValid(dto.DateStart, dto.DateEnd))
            {
                throw new ArgumentException("活動期間不能超過三個月。");
            }
            string status = CamapignsTimeHelper.DetermineStatus(dto.DateStart, dto.DateEnd);
            CampaignsCouponEntity entity = new CampaignsCouponEntity
            (
              dto.CampaignId,
              dto.Name,
              dto.DateStart,
              dto.DateEnd,
              dto.DateCreated,
              status,
              dto.DiscountQuantity,
              dto.DiscountValue,
              dto.AvailableQuantity,
              dto.MinimumOrderValue,
              dto.MaximumDiscountAmount,
              dto.Code,
              dto.DisplayDescription,
              dto.MemberCriteria,
              dto.AirFlightsCriteria,
              dto.Type,
              dto.Id
            );

            _repo.Update(entity);
        }

        public List<CampaignsCouponDto> Search(CampaignsCouponSearchCriteria dto)
        {

            var list = _repo.Search(dto);

            return list.Select(c => new CampaignsCouponDto
            {
                Id = c.Id,
                Name = c.Name,
                CampaignId = c.CampaignId,
                DateStart = c.DateStart,
                DateEnd = c.DateEnd,
                DateCreated = c.DateCreated,
                Status = c.Status,
                DiscountQuantity = c.DiscountQuantity,
                DiscountValue = c.DiscountValue,
                AvailableQuantity = c.AvailableQuantity,
                MinimumOrderValue = c.MinimumOrderValue,
                MaximumDiscountAmount = c.MaximumDiscountAmount,
                Code = c.Code,
                DisplayDescription = c.DisplayDescription,
                MemberCriteria = c.MemberCriteria,
                AirFlightsCriteria = c.AirFlightsCriteria,
                Type = c.Type
            }).ToList();

        }
        public IEnumerable<int> splitFlightCriteria(string flightCriteria)
        {
            var flightsIds = flightCriteria.Split(",");
            return flightsIds.Select( x =>Int32.Parse(x));
        }

        public void CreateAirFlightsTable(int couponId)
        {
            var entity = _repo.Get(couponId);
            var flightids = splitFlightCriteria(entity.AirFlightsCriteria);
            _repo.CreateFlightsIds(couponId, flightids);
        }

        public void AddCoupontoMemberId(int campaignsCouponId, int memberid)
        {
            _repo.AddCoupontoMemberId(campaignsCouponId, memberid);
        }


        public List<CampaignsCouponDto> CouponCart(int memberid, decimal originalPrice)
        {
            var coupons = _repo.CouponCart(memberid);

            // 修改为存储额外的信息，包括最终价格和折扣金额
            var couponDetails = new List<(CampaignsCouponEntity coupon, decimal finalPrice, decimal discountAmount)>();

            foreach (var coupon in coupons)
            {
                decimal discountAmount = 0;
                switch (coupon.CampaignId)
                {
                    case 1:
                        discountAmount = coupon.DiscountValue;
                        if (discountAmount > coupon.MaximumDiscountAmount)
                        {
                            discountAmount = coupon.MaximumDiscountAmount;
                        }
                        break;
                    case 4:
                        discountAmount = originalPrice * (coupon.DiscountValue / 100);
                        if (discountAmount > coupon.MaximumDiscountAmount)
                        {
                            discountAmount = coupon.MaximumDiscountAmount;
                        }
                        break;
                }
                decimal finalPrice = originalPrice - discountAmount;
                couponDetails.Add((coupon, finalPrice, discountAmount));
            }

            // 使用finalPrice排序，并包括discountAmount
            var sortedCoupons = couponDetails.OrderBy(details => details.finalPrice)
                     .Select(details => new CampaignsCouponDto
                     {
                         Id = details.coupon.Id,
                         Name = details.coupon.Name,
                         CampaignId = details.coupon.CampaignId,
                         DateStart = details.coupon.DateStart,
                         DateEnd = details.coupon.DateEnd,
                         DateCreated = details.coupon.DateCreated,
                         Status = CamapignsTimeHelper.DetermineStatus(details.coupon.DateStart, details.coupon.DateEnd),
                         DiscountQuantity = details.coupon.DiscountQuantity,
                         DiscountValue = details.coupon.DiscountValue,
                         AvailableQuantity = details.coupon.AvailableQuantity,
                         MinimumOrderValue = details.coupon.MinimumOrderValue,
                         MaximumDiscountAmount = details.coupon.MaximumDiscountAmount,
                         Code = details.coupon.Code,
                         DisplayDescription = details.coupon.DisplayDescription,
                         MemberCriteria = details.coupon.MemberCriteria,
                         AirFlightsCriteria = details.coupon.AirFlightsCriteria,
                         Type = details.coupon.Type,
                         IsBestChoice = false, // Initially set all options to not the best choice
                         FinalPrice = details.finalPrice,
                         discountAmount = details.discountAmount // Set discountAmount here
                     }).ToList();

            // Mark the best choice
            if (sortedCoupons.Any())
            {
                sortedCoupons.First().IsBestChoice = true;
            }

            return sortedCoupons;
        }





        public IEnumerable<int> splitClassCriteria(string memberCriteria)
        {
            var memberIds = memberCriteria.Split(",");
            return memberIds.Select(x => Int32.Parse(x));
        }
        public void CreateCouponMembersTable(int couponid)
        {
            var entity = _repo.Get(couponid);
            var memberids = splitClassCriteria(entity.MemberCriteria);
            foreach ( var memberid in memberids) 
            { 
                 var member = _memberrepo.GetAllMembersIdByMemberClassId(memberid);
                _repo.CreateCouponMembersTable(couponid, member);
                
            }            
        }


        //生日券發放
        public async Task GenerateBirthdayCouponsForThisMonth()
        {
            // 创建一个空的搜索条件对象
            MemberSearchCriteria emptyCriteria = new MemberSearchCriteria();

            // 使用 Search 方法获取所有当月生日的会员
            var thisMonthBirthdayMembers = _memberrepo.Search(emptyCriteria)
                .Where(m => m.DateOfBirth.Month == DateTime.Today.Month)
                .Select(m=>m.Id)
                .ToList();

              // todo检查是否已经为该会员在当月生成过生日优惠券
               
                // 创建一个新的优惠券实体
                CampaignsCouponEntity coupon = new CampaignsCouponEntity(
                    campaignId: 4, 
                    name: "當月壽星禮",
                    dateStart: new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), 
                    dateEnd: new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)), 
                    dateCreated: DateTime.Now,
                    status: "進行中",
                    discountQuantity: 1000,
                    discountValue: 90,
                    availableQuantity: 1000, 
                    minimumOrderValue: null,
                    maximumDiscountAmount: 2500,
                    code: "HBD4U", 
                    displayDescription: true,
                    memberCriteria: "",
                    airFlightsCriteria: "",
                    type: "BirthdayCoupon"
                );

            // 假设 _repo.Create(coupon) 返回新创建的优惠券ID
                int newCouponId = _repo.Create(coupon);
            var emailSetting = _configuration.GetSection("SendEmailSetting").Get<Dictionary<string, string>>();
            // 使用HTML格式的邮件正文
            string emailBody = @"
        <html>
            <body>
                <p>親愛的用戶，您好：</p>
                <p>發送資訊如下：</p>
                <p>您的專屬折扣碼：HBD4U</p>
                <img src='https://lh3.googleusercontent.com/d/1y7ajqh1cdHi8vmfJ4RABkTCDlwrNxu6H' alt='Happy Birthday' width='500px' />
 <div style='background-color: #f2f2f2; padding: 20px; margin-top: 20px;'>
                     <h4>使用注意事項：</h4>
                     <ul>
                         <li>生日禮金會員當月1日起即可登入信箱查看（當日即可使用）</li>
                         <li>生日禮金無法與其他優惠專案活動、折扣碼同時使用。 </li>
                         <li>生日禮金於當月1日系統自動發放生日禮金至會員登記信箱（當日即可使用） 例如：12月壽星，只於當年11/30 前註冊者，禮金將於12/01 發放 至會員登記信箱。 若於11/30之後完成註冊者，將於隔年度12/1收到生日禮金，以此類推。 如超過會員生日月份恕無法申請補發，敬請見諒。 </li>
                         <li>生日禮金限當月使用，不得跨月使用，逾期將視同放棄。 </li>
                         <li>如遇週末例假日則順延至客服上班日發送。 </li>
                     </ul>
                 </div>
            </body>
        </html>";

            // 在SendEmail方法中添加参数，指明使用HTML格式
            SendEmailHelper.SendEmail(
                emailSetting["FromEmail"],
                emailSetting["FromPassword"],
                "sparkle.airline@gmail.com",
                "【點我領生日禮】Sparkle Airline送您會員專屬生日禮！",
                emailBody);
            _repo.CreateCouponMembersTable(newCouponId, thisMonthBirthdayMembers);

        }

    }

    
}
