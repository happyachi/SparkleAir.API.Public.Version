using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Entity.Campaigns
{
    public class CampaignsCouponMembersEntity
    {
        public int Id { get; set; }

        public int CampaignsCouponId { get; set; }

        public int MemberId { get; set; }

    }
}
