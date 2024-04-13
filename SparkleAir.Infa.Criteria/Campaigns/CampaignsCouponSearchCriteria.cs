using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Criteria.Campaigns
{
    public class CampaignsCouponSearchCriteria
    {
        public int Id { get; set; }

        public int CampaignId { get; set; }

        public string Name { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public DateTime DateCreated { get; set; }

        public string Status { get; set; }

        public decimal DiscountValue { get; set; }

        public int DiscountQuantity { get; set; }

        public int AvailableQuantity { get; set; }

        public int? MinimumOrderValue { get; set; }

        public int MaximumDiscountAmount { get; set; }

        public string Code { get; set; }

        public bool DisplayDescription { get; set; }

        public string MemberCriteria { get; set; }

        public string AirFlightsCriteria { get; set; }

        public string Campaign { get; set; }

        public string Type { get; set; }

    }
}
