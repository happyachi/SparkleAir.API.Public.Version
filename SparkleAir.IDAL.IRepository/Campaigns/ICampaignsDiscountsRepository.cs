using SparkleAir.Infa.Criteria.Campaigns;
using SparkleAir.Infa.Entity.Campaigns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.IDAL.IRepository.Campaigns
{
    public interface ICampaignsDiscountsRepository
    {
        //取全部
        List<CampaignsDiscountEntity> GetAll();

        //取得一筆
        CampaignsDiscountEntity Get(int id);

        //新增
        int Create(CampaignsDiscountEntity model);

        //刪除
        void Delete(int id);

        //更新
        void Update(CampaignsDiscountEntity model);

        //查詢
        List<CampaignsDiscountEntity> Search(CampaignsDiscountSearchCriteria entity);

        void CreateTFItemsIds(int discountId, IEnumerable<int> tFItemsId);

    }
}
