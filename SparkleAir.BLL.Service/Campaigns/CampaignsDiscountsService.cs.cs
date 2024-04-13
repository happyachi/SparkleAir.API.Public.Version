using Microsoft.EntityFrameworkCore;
using SparkleAir.BLL.Service.TaxFree;
using SparkleAir.IDAL.IRepository.Campaigns;
using SparkleAir.IDAL.IRepository.Members;
using SparkleAir.IDAL.IRepository.TaxFree;
using SparkleAir.Infa.Criteria.Campaigns;
using SparkleAir.Infa.Dto.Campaigns;
using SparkleAir.Infa.Dto.TaxFree;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.Campaigns;
using SparkleAir.Infa.Utility.Exts.Models;
using SparkleAir.Infa.Utility.Helper.Campaigns;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SparkleAir.BLL.Service.Campaigns
{
    public class CampaignsDiscountsService
    {
        private AppDbContext db;
        private readonly ICampaignsDiscountsRepository _repo;
        private readonly IMemberRepository _memberrepo;
        private readonly ITFRepository _ITFrepo;
        private readonly ITFOrderRepository _ITForderrepo;

        public CampaignsDiscountsService(AppDbContext context,ICampaignsDiscountsRepository repo, IMemberRepository memberrepo, ITFRepository itfrepo, ITFOrderRepository itforderrepo)
        {
            db = context;
            _repo = repo;
            _memberrepo = memberrepo;
            _ITFrepo = itfrepo;
            _ITForderrepo = itforderrepo;
        }

        public int Create(CampaignsDiscountDto dto)
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
            if (!PriceHelper.IsMaximumDiscountValid(dto.DiscountValue, (int?)dto.Value))
            {
                throw new ArgumentException("最高折扣金額不得大於最低消費金額。");
            }

            if (dto.DateEnd < dto.DateStart)
            {
                throw new ArgumentException("結束時間不能早於開始時間。");
            }

            if (dto.CampaignId == 6)
            {
                if (dto.DiscountValue >= 99 || dto.DiscountValue < 10)
                {
                    throw new ArgumentException("折數必須介於10-99之間。");
                }
            }

            string status = CamapignsTimeHelper.DetermineStatus(dto.DateStart, dto.DateEnd);

            CampaignsDiscountEntity entity = new CampaignsDiscountEntity(
              
              dto.CampaignId,
              dto.Name,
              DateTime.Now,
              dto.DateStart,
              dto.DateEnd,
              status,
              dto.DiscountValue,
              dto.Value,
              dto.BundleSKUs,
              dto.MemberCriteria,
              dto.TFItemsCriteria,
              dto.Type,
              dto.Description,
              dto.Image

                //dto.Id
                );

            _repo.Create(entity);
            return entity.Id;

            
        }
        public void Delete(int id)
        {
            _repo.Delete(id);
        }

        public CampaignsDiscountDto Get(int id)
        {

            CampaignsDiscountEntity entity = _repo.Get(id);
            CampaignsDiscountDto dto = new CampaignsDiscountDto
            {
                Id = id,
                CampaignId = entity.CampaignId,
                Name = entity.Name,
                DateCreated = entity.DateCreated,
                DateStart = entity.DateStart,
                DateEnd = entity.DateEnd,
                Status = entity.Status,
                DiscountValue = entity.DiscountValue,
                Value = entity.Value,
                BundleSKUs = entity.BundleSKUs,
                MemberCriteria = entity.MemberCriteria,
                TFItemsCriteria = entity.TFItemsCriteria,
                Type = entity.Type,
                Campaign = entity.Campaign,
                Description = entity.Description,
                Image = entity.Image,
            };
            return dto;
        }

        public List<CampaignsDiscountDto> GetAll()
        {
            List<CampaignsDiscountEntity> entity = _repo.GetAll();

            List<CampaignsDiscountDto> dto = entity.Select(d => new CampaignsDiscountDto
            {
                Id = d.Id,
                CampaignId = d.CampaignId,
                Name = d.Name,
                DateCreated = d.DateCreated,
                DateStart = d.DateStart,
                DateEnd = d.DateEnd,
                Status = CamapignsTimeHelper.DetermineStatus(d.DateStart, d.DateEnd),
                DiscountValue = d.DiscountValue,
                Value = d.Value,
                BundleSKUs = d.BundleSKUs,
                MemberCriteria = d.MemberCriteria,
                TFItemsCriteria = d.TFItemsCriteria,
                Type = d.Type,
                Description = d.Description,
                Image= d.Image
                //Campaign = d.Campaign
            }).ToList();

            return dto;

        }
        public void Update(CampaignsDiscountDto dto)
        {
            string status = CamapignsTimeHelper.DetermineStatus(dto.DateStart, dto.DateEnd);

            CampaignsDiscountEntity entity = new CampaignsDiscountEntity
            (
              dto.CampaignId,
              dto.Name,
              dto.DateCreated,
              dto.DateStart,
              dto.DateEnd,
              status,
              dto.DiscountValue,
              dto.Value,
              dto.BundleSKUs,
              dto.MemberCriteria,
              dto.TFItemsCriteria,
              dto.Type,
              dto.Description,
              dto.Image,
              dto.Id              
                );

            _repo.Update(entity);
        }

        public List<CampaignsDiscountDto> Search(CampaignsDiscountSearchCriteria dto)
        {

            var list = _repo.Search(dto);

            return list.Select(d => new CampaignsDiscountDto
            {
                Id = d.Id,
                CampaignId = d.CampaignId,
                Name = d.Name,
                DateCreated = d.DateCreated,
                DateStart = d.DateStart,
                DateEnd = d.DateEnd,
                Status = d.Status,
                DiscountValue = d.DiscountValue,
                Value = d.Value,
                BundleSKUs = d.BundleSKUs,
                MemberCriteria = d.MemberCriteria,
                TFItemsCriteria = d.TFItemsCriteria,
                Type = d.Type
            }).ToList();

        }

        public List<CampaignsDiscountDto> GetAllFiltered()
        {
            List<CampaignsDiscountEntity> entity = _repo.GetAll();

            List<CampaignsDiscountDto> dto = entity
                .Where(d => d.Status == "進行中" || d.Status == "接下來")
                .Select(d => new CampaignsDiscountDto
                {
                    Id = d.Id,
                    CampaignId = d.CampaignId,
                    Name = d.Name,
                    DateCreated = d.DateCreated,
                    DateStart = d.DateStart,
                    DateEnd = d.DateEnd,
                    Status = CamapignsTimeHelper.DetermineStatus(d.DateStart, d.DateEnd),
                    DiscountValue = d.DiscountValue,
                    Value = d.Value,
                    BundleSKUs = d.BundleSKUs,
                    MemberCriteria = d.MemberCriteria,
                    TFItemsCriteria = d.TFItemsCriteria,
                    Type = d.Type,
                    Description = d.Description,
                    Image= d.Image
                    //Campaign = d.Campaign
                }).ToList();

            return dto;
        }
        public IEnumerable<int> splitTFItemsCriteria(string tFItemsCriteria)
        {
            var tFItemsId = tFItemsCriteria.Split(",");
            return tFItemsId.Select(x => Int32.Parse(x));
        }
        public void CreateDiscountsTable(int discountId)
        {
            var entity = _repo.Get(discountId);
            var tFItemsId = splitTFItemsCriteria(entity.TFItemsCriteria);
            _repo.CreateTFItemsIds(discountId, tFItemsId);
        }

        //輸入Productid 找到對應活動
        public List<CampaignsDiscountDto> GetDiscountsByProductId(int productId)
        {
            var allDiscounts = GetAll(); // 获取所有活动
            var relatedDiscounts = allDiscounts.Where(d => d.TFItemsCriteria.Split(',')
                                 .Any(id => int.Parse(id) == productId))
                                 .ToList();
            return relatedDiscounts;
        }

        public List<DiscountDto> Pos (int ticketid)
        {
            // 根據ticketid查詢訂單列表
            var ordersList = db.Tforderlists.Where(x => x.TicketDetailsId == ticketid);
            var productsId = new List<int>();
            // 遍歷訂單，將商品ID根據數量加入列表
            foreach (var order in ordersList)
            {
                var pId = order.TfitemsId;
                var amount = order.Quantity;
                for (int i = 0; i < amount; i++)
                {
                    productsId.Add(pId);
                }
            }

            // 將商品ID去重，獲得唯一商品ID列表
            var productsListId = productsId.Distinct();

            // 創建商品折扣DTO列表
            List<DiscountProductDto> products = new List<DiscountProductDto>();

            // 根據商品ID，查詢商品詳情，並加入到DTO列表
            foreach (var productId in productsId) { 
                 var product = db.Tfitems.Find(productId);
                var productDto = new DiscountProductDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Quantity= product.Quantity,
                    UnitPrice = product.UnitPrice,
                };
                products.Add(productDto);
            }

            // 查詢符合條件的優惠商品
            List<CampaignsDiscountTfitem> discountTfitem = new List<CampaignsDiscountTfitem>();
            foreach (var productId in productsListId)
            {
                var productdicount = db.CampaignsDiscountTfitems.Where(c=>c.TfitemId==productId).ToList();
                discountTfitem.AddRange(productdicount);
            }

            // 根據商品ID，查詢對應的優惠ID，去重
            var discountsList = new List<int>();

            foreach (var product in productsListId)
            {
                var discountsId = db.CampaignsDiscountTfitems.AsNoTracking()
                                    .Where(x => x.TfitemId==product)
                                    .Select(x =>x.CampaignsDiscountId)
                                    .ToList();
               discountsList.AddRange(discountsId);
            }

            discountsList = discountsList.Distinct().ToList();

            // 根據優惠ID計算優惠詳情
            //取出每一筆優惠id,去找他是哪一個活動類別及折扣方式
            var totalDiscount = 0;

            var totalDiscountList = new List<DiscountDto>();

            //優惠券id找活動類別&折扣方式
            foreach (var discountId in discountsList)
            {
                var discount = db.CampaignsDiscounts.Find(discountId);
                //透過優惠券找出符合的商品id List
                var productIdList = discountTfitem.Where(x => x.CampaignsDiscountId == discountId).ToList()
                    .Select(x=>x.TfitemId).ToList();

                //找出符合的商品
                var productList= new List<DiscountProductDto>();
                foreach(var productid in productIdList)
                {
                    var product = products.Where(x => x.Id == productid).ToList();
                    productList.AddRange(product);
                }

                var discountList = new List<DiscountDto>();
                 
                //滿x元折y元
                if (discount.CampaignId == 5)
                {
                    discountList = ApplyDiscountForTotalAmount(Convert.ToInt32(discount.Value), Convert.ToInt32(discount.DiscountValue), productList, discountId, discount.Name).ToList();

                }
                //滿x件y折
                else if (discount.CampaignId == 6)
                {
                    discountList=ApplyPercentageDiscountForTotalQuantity(Convert.ToInt32(discount.BundleSkus), Convert.ToInt32(discount.DiscountValue), productList, discountId, discount.Name).ToList();
                }
                //滿x元y折
                else if (discount.CampaignId == 8)
                {
                    discountList = ApplyPercentageDiscountForTotalAmount(Convert.ToInt32(discount.Value), Convert.ToInt32(discount.DiscountValue), productList, discountId, discount.Name).ToList();
                }
                //滿x件折y元
                else if (discount.CampaignId == 10)
                {
                    discountList = ApplyDiscountForTotalQuantity(Convert.ToInt32(discount.BundleSkus), Convert.ToInt32(discount.DiscountValue), productList, discountId, discount.Name).ToList();
                }
                totalDiscountList.AddRange(discountList);
            }
            return totalDiscountList;
        }

        //滿x件折y元
        private IEnumerable<DiscountDto> ApplyDiscountForTotalQuantity(int? bundleSkus, int discountValue, List<DiscountProductDto> productList, int discountId, string discountName)
        {
            if (!bundleSkus.HasValue || bundleSkus.Value <= 0)
            {
                yield break; // 若bundleSkus無效，則直接終止方法
            }

            // 由高價到低價排序
            var orderedProductList = productList.OrderByDescending(x => x.UnitPrice).ToList();
            var qty = orderedProductList.Count;

            // 按照bundleSkus分組產生折扣
            for (int i = 0; i < qty; i += bundleSkus.Value)
            {
                if (i + bundleSkus.Value > qty) break; // 如果剩餘商品不足以形成一個完整的bundleSkus，則終止迴圈

                var matchedProducts = orderedProductList.Skip(i).Take(bundleSkus.Value).ToList();
                yield return new DiscountDto()
                {
                    DiscountId = discountId,
                    DiscountName = discountName,
                    Amount = discountValue,
                    Products = matchedProducts,
                    RuleName = $"滿{bundleSkus}件折{discountValue}元"
                };
            }
        }

        //滿x元打y折
        private IEnumerable<DiscountDto> ApplyPercentageDiscountForTotalAmount(int value, int discountValue, List<DiscountProductDto> productList, int discountId, string discountName)
        {
            var totalPrice = productList.Select(x => x.UnitPrice).Sum();

            if (totalPrice > value)
            {
                var discountAmount = totalPrice - (totalPrice * discountValue / 100.0M);
                yield return new DiscountDto()
                {
                    DiscountId = discountId,
                    DiscountName = discountName,
                    Amount = discountAmount,
                    Products = productList,
                    RuleName = $"滿{value}元再享{discountValue}折"
                };
            }
        }

        //滿幾件打幾折
        private IEnumerable<DiscountDto> ApplyPercentageDiscountForTotalQuantity(int? bundleSkus, int discountValue, List<DiscountProductDto> productList, int discountId, string discountName)
        {
            if (!bundleSkus.HasValue || bundleSkus <= 0) yield break; // 檢查bundleSkus是否有效

            // 由高價到低價排序
            productList = productList.OrderByDescending(x => x.UnitPrice).ToList();

            // 分組處理，每組bundleSkus數量
            for (int i = 0; i < productList.Count; i += bundleSkus.Value)
            {
                if (i + bundleSkus.Value > productList.Count) break; // 如果剩餘商品不足以形成一個完整的bundleSkus，則終止迴圈

                var matchedProducts = productList.Skip(i).Take(bundleSkus.Value).ToList();
                var totalUnitPrice = matchedProducts.Select(p => p.UnitPrice).Sum();
                var discountAmount = totalUnitPrice * (100 - discountValue) / 100;

                yield return new DiscountDto()
                {
                    DiscountId = discountId,
                    DiscountName = discountName,
                    Amount = discountAmount,
                    Products = matchedProducts,
                    RuleName = $"{bundleSkus}件再{discountValue}折"
                };
            }
        }

        //滿x元折y元，單筆不累計
        private IEnumerable<DiscountDto> ApplyDiscountForTotalAmount(int value, int discountValue, List<DiscountProductDto> productList, int discountId, string discountName)
        {
            var totalPrice = productList.Select(x => x.UnitPrice).Sum();

            if (totalPrice > value)
            {
                yield return new DiscountDto()
                {
                    DiscountId = discountId,
                    DiscountName = discountName,
                    Amount = discountValue, // 折扣金額應該明確表明是減少了多少，或者是折扣後的價格
                    Products = productList,
                    RuleName = $"滿{value}元折{discountValue}元"
                };
            }
        }

        public List<Tfitem> SearchForProducts(int discountId)
        {
            var products = db.CampaignsDiscountTfitems.Where(x => x.CampaignsDiscountId == discountId)
                                                      .Select(x=>x.Tfitem)
                                                      .OrderByDescending(x=>x.Quantity)
                                                      .ToList();
            return products;
        }
    }
}
