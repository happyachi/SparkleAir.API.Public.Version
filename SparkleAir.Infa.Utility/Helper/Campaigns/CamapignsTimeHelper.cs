using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Utility.Helper.Campaigns
{
    public class CamapignsTimeHelper 
    {
        public static string DetermineStatus(DateTime dateStart, DateTime dateEnd)
        {
            DateTime now = DateTime.Now;

            if (dateEnd > now && dateStart < now)
            {
                return "進行中";
            }
            else if (dateStart > now && dateEnd > now)
            {
                return "接下來";
            }
            else
            {
                return "已結束";
            }
        }

        // 檢查 datastart 必須是現在的時間半小時以後
        public static bool IsStartDateValid(DateTime dateStart)
        {
            return dateStart > DateTime.Now.AddMinutes(30);
        }

        // 檢查 datastart 和 dataend 期間不能超過三個月
        public static bool IsDateRangeValid(DateTime dateStart, DateTime dateEnd)
        {
            return (dateEnd - dateStart).TotalDays <= 90;
        }

    }
}
