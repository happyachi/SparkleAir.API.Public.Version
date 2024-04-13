using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Utility.Helper.Campaigns
{
    public class PriceHelper
    {
        public static bool IsMaximumDiscountValid(decimal maximumDiscount, int? minimumOrderValue)
        {
            return maximumDiscount <= minimumOrderValue;
        }
    }
}
