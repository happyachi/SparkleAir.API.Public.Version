using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Utility.Helper.AirFlights
{
    public class StatusHelper
    {
        public static int SaleStatusUpdate(DateTime daptTime)
        {
            DateTime today = DateTime.Now;
            if (today > daptTime)
            {
                return 5;
            }
            return 1;
        }
    }
}
