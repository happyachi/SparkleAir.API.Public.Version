using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Criteria.Campaigns
{
    public class CampaignsDiscountSearchCriteria
    {
        public int Id { get; set; }

        public int CampaignId { get; set; }

        public string Name { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public DateTime DateCreated { get; set; }

        public string Status { get; set; }

        public decimal DiscountValue { get; set; }

        public decimal Value { get; set; }

        public decimal BundleSKUs { get; set; }

        public string MemberCriteria { get; set; }

        public string TFItemsCriteria { get; set; }

        public string Type { get; set; }
    }
}
