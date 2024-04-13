using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Entity.Campaigns
{
    public class CampaignsDiscountEntity
    {
        public CampaignsDiscountEntity(int campaignid, string name, DateTime datecreated, DateTime datestart, DateTime dateend, string status, decimal discountvalue, decimal value, decimal? bundleskus, string membercriteria, string tfitemscriteria,string type,string description, string image, int id = -1)
        {
            Id = id;
            CampaignId = campaignid;
            Name = name;
            DateCreated = datecreated;
            DateStart = datestart;
            DateEnd = dateend;
            Status = status;
            DiscountValue = discountvalue;
            Value = value;
            BundleSKUs = bundleskus;
            MemberCriteria = membercriteria;
            TFItemsCriteria = tfitemscriteria;
            Type = type;
            Description = description;
            Image = image;
            //Campaign = campaign;
        }

        public int Id { get; set; }

        public int CampaignId { get; set; }

        public string Name { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public DateTime DateCreated { get; set; }

        public string Status { get; set; }

        public decimal DiscountValue { get; set; }

        public decimal Value { get; set; }

        public decimal? BundleSKUs { get; set; }

        public string MemberCriteria { get; set; }

        public string TFItemsCriteria { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public string Campaign { get; set; }

        public string Image { get; set; }
    }
}
