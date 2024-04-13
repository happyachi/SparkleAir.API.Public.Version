using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Utility.Helper
{
    public static class TimeZoneHelper
    {
        /// <summary>
        /// 把當地時間轉換成 GMT
        /// </summary>
        /// <param name="localTime"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>

        public static TimeSpan ConvertToGMT(TimeSpan localTime, int timeZone)
        {
            TimeSpan timeOffset = TimeSpan.FromHours(timeZone);
            TimeSpan gmtTime = localTime - timeOffset;
            return AdjustTimeToValidRange(gmtTime);
        }

        public static TimeSpan ConvertToLocal(TimeSpan gmtTime, int timeZone)
        {
            TimeSpan timeOffset = TimeSpan.FromHours(timeZone);
            TimeSpan localTime = gmtTime + timeOffset;
            return AdjustTimeToValidRange(localTime);
        }

        private static TimeSpan AdjustTimeToValidRange(TimeSpan time)
        {
            string formattedTime = time.ToString(@"hh\:mm\:ss");

            // 如果格式化後的時間字串為空，表示原始時間為零，則返回 TimeSpan.Zero
            if (string.IsNullOrEmpty(formattedTime))
            {
                return TimeSpan.Zero;
            }

            // 將格式化後的字串轉換為 TimeSpan
            TimeSpan adjustedTime;
            if (TimeSpan.TryParse(formattedTime, out adjustedTime))
            {
                // 將時間調整為有效範圍
                if (adjustedTime < TimeSpan.Zero)
                {
                    return TimeSpan.FromHours(24) + adjustedTime; // 將負值時間轉換為對應的正值時間
                }
                else if (adjustedTime > TimeSpan.FromHours(23) + TimeSpan.FromMinutes(59) + TimeSpan.FromSeconds(59))
                {
                    return TimeSpan.FromHours(0) + adjustedTime;
                }
                else
                {
                    return adjustedTime;
                }
            }
            else
            {
                // 轉換失敗時返回 TimeSpan.Zero 或其他預設值
                return TimeSpan.Zero;
            }
        }
    }
}
