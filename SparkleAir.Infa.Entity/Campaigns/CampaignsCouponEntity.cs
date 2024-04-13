using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Entity.Campaigns
{
    public class CampaignsCouponEntity
    {
        public CampaignsCouponEntity(int campaignId, string name, DateTime dateStart, DateTime dateEnd, DateTime dateCreated, string status, int discountQuantity, decimal discountValue, int availableQuantity, int? minimumOrderValue, int maximumDiscountAmount, string code, bool displayDescription, string memberCriteria, string airFlightsCriteria,string type,int id=-1)
        {
            Id = id;
            CampaignId = campaignId;
            Name = name;
            DateCreated = dateCreated;
            DateStart = dateStart;
            DateEnd = dateEnd;
            Status = status;
            DiscountQuantity = discountQuantity;
            DiscountValue = discountValue;
            AvailableQuantity = availableQuantity;
            MinimumOrderValue = minimumOrderValue;
            MaximumDiscountAmount = maximumDiscountAmount;
            Code = code;
            DisplayDescription = displayDescription;
            MemberCriteria = memberCriteria;
            AirFlightsCriteria = airFlightsCriteria;
            Type = type;
        }

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
