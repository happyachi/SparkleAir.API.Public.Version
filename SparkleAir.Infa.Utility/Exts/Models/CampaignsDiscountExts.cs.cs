using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.Campaigns;
using SparkleAir.Infa.Entity.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Utility.Exts.Models
{
    public static class CampaignsDiscountExts
    {
        public static CampaignsDiscountEntity ToEntity(this CampaignsDiscount discount) 
        {
            CampaignsDiscountEntity campaignsDiscountEntity = new CampaignsDiscountEntity
            (
             
             discount.CampaignId,
             discount.Name,
             discount.DateCreated,
             discount.DateStart,
             discount.DateEnd,
             discount.Status,
             discount.DiscountValue,
             discount.Value,
             discount.BundleSkus,
             discount.MemberCriteria,
             discount.TfitemsCriteria,
             discount.Campaign.Type,
             discount.Description,
             discount.Image,
             discount.Id
            );

            return campaignsDiscountEntity;
        }
    }
}
