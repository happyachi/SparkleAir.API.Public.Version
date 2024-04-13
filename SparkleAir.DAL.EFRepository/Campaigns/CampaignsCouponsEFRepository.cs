using SparkleAir.IDAL.IRepository.Campaigns;
using SparkleAir.Infa.Entity.Campaigns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparkleAir.Infa.Utility.Exts.Models;
using System.Xml.Linq;
using SparkleAir.Infa.Criteria.Campaigns;
using SparkleAir.Infa.EFModel.Models;
using Microsoft.EntityFrameworkCore;

namespace SparkleAir.DAL.EFRepository.Campaigns
{
    public class CampaignsCouponsEFRepository : ICampaignsCouponsRepository
    {
        private AppDbContext db;
        private Func<CampaignsCoupon, CampaignsCouponEntity> func = c => c.ToEntity();
        public CampaignsCouponsEFRepository(AppDbContext context)
        {
            db = context;
        }

        public int Create(CampaignsCouponEntity entity)
        {
            CampaignsCoupon coupon = new CampaignsCoupon()
            {
                Name = entity.Name,
                CampaignId = entity.CampaignId,
                DateStart= entity.DateStart,
                DateEnd=entity.DateEnd,
                DateCreated =entity.DateCreated,
                Status = entity.Status,
                DiscountQuantity = entity.DiscountQuantity,
                DiscountValue = entity.DiscountValue,
                AvailableQuantity = entity.AvailableQuantity,
                MinimumOrderValue = entity.MinimumOrderValue,
                MaximumDiscountAmount = entity.MaximumDiscountAmount,
                Code = entity.Code,
                DisplayDescription = entity.DisplayDescription,
                MemberCriteria = entity.MemberCriteria,
                AirFlightsCriteria = entity.AirFlightsCriteria
            };

            db.CampaignsCoupons.Add(coupon);
            db.SaveChanges();

            return coupon.Id;
        }

        public void Delete(int id)
        {
            var coupon = db.CampaignsCoupons.Find(id);
            db.CampaignsCoupons.Remove(coupon);
            db.SaveChanges();
        }

        public CampaignsCouponEntity Get(int id)
        {
            var query = db.CampaignsCoupons.AsNoTracking()
                             .Include(c => c.Campaign)
                             .Where(c =>c.Id==id)
                             .FirstOrDefault();
            return query.ToEntity();
        }

        public List<CampaignsCouponEntity> GetAll()
        {
            var coupons = db.CampaignsCoupons.AsNoTracking()
                             .Include(c => c.Campaign)
                             .OrderBy(c => c.DateCreated)
                             .Select(func)
                             .ToList();

            return coupons;
        }

        public void Update(CampaignsCouponEntity entity)
        {
            var coupon = db.CampaignsCoupons.Find(entity.Id);
            if (coupon != null)
            {
                coupon.Id = entity.Id;
                coupon.CampaignId = entity.CampaignId;
                coupon.Name = entity.Name;
                coupon.DateStart = entity.DateStart;
                coupon.DateEnd = entity.DateEnd;
                coupon.DateCreated = entity.DateCreated;
                coupon.Status = entity.Status;
                coupon.DiscountQuantity = entity.DiscountQuantity;
                coupon.DiscountValue = entity.DiscountValue;
                coupon.AvailableQuantity = entity.AvailableQuantity;
                coupon.MinimumOrderValue = entity.MinimumOrderValue;
                coupon.MaximumDiscountAmount = entity.MaximumDiscountAmount;
                coupon.Code = entity.Code;
                coupon.DisplayDescription = entity.DisplayDescription;
                coupon.MemberCriteria = entity.MemberCriteria;
                coupon.AirFlightsCriteria = entity.AirFlightsCriteria;
               
            };
            db.SaveChanges();
        }
        public List<CampaignsCouponEntity> Search(CampaignsCouponSearchCriteria entity)
        {
            var query = db.CampaignsCoupons.AsNoTracking()
                             .Include(c => c.Campaign)
                             .OrderBy(c => c.DateCreated)
                             .ToList()
                             .Select(func);
                            
            if (!string.IsNullOrEmpty(entity.Name))
            {
                query = query.Where(e => e.Name.Contains(entity.Name));
            }
            return query.ToList();
        }

        public void CreateFlightsIds(int campaignsCouponId, IEnumerable<int> airflightId)
        {
            foreach (var item in airflightId)
            {
                var flightid = new CampaignsCouponAirFlight() { 
                    AirFlightId = item, 
                    CampaignsCouponId=campaignsCouponId 
                };
                db.CampaignsCouponAirFlights.Add(flightid);   
            }
            db.SaveChanges();
        }

        public void AddCoupontoMemberId(int campaignsCouponId, int memberid)
        {
            CampaignsCouponMember coupon = new CampaignsCouponMember()
            {
                CampaignsCouponId = campaignsCouponId,
                MemberId = memberid
            };

            db.CampaignsCouponMembers.Add(coupon);
            db.SaveChanges();
        }

        public List<CampaignsCouponEntity> CouponCart(int memberid)
        {
            var coupons = db.CampaignsCouponMembers.AsNoTracking()
                             .Include(m => m.CampaignsCoupon)
                             .Include(m => m.CampaignsCoupon.Campaign)
                             .Where(m => m.MemberId == memberid)
                             .ToList()
                             .Select(m => new CampaignsCouponEntity(m.CampaignsCoupon.CampaignId, m.CampaignsCoupon.Name, m.CampaignsCoupon.DateStart, m.CampaignsCoupon.DateEnd, m.CampaignsCoupon.DateCreated, m.CampaignsCoupon.Status, m.CampaignsCoupon.DiscountQuantity, m.CampaignsCoupon.DiscountValue, m.CampaignsCoupon.AvailableQuantity, m.CampaignsCoupon.MinimumOrderValue, m.CampaignsCoupon.MaximumDiscountAmount, m.CampaignsCoupon.Code, m.CampaignsCoupon.DisplayDescription, m.CampaignsCoupon.MemberCriteria, m.CampaignsCoupon.AirFlightsCriteria, m.CampaignsCoupon.Campaign.Type,m.CampaignsCoupon.Id))                          
                             .ToList();
            return coupons;
        }

        /// <summary>
        /// 先比對買家是否已經有此券
        /// </summary>
        /// <param name="campaignsCouponId"></param>
        /// <param name="membersId"></param>
        public void CreateCouponMembersTable(int campaignsCouponId, IEnumerable<int> membersId)
        {
            foreach (var item in membersId)
            {
                var couponid = new CampaignsCouponMember()
                {
                    MemberId = item,
                    CampaignsCouponId = campaignsCouponId

                };
                var check = db.CampaignsCouponMembers.Where(x => x.MemberId == item && x.CampaignsCouponId == campaignsCouponId).FirstOrDefault();
                if (check == null)
                {
                    db.CampaignsCouponMembers.Add(couponid);
                }
            }
            db.SaveChanges();
        }
    }
}
