using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Dto.MileageDetails
{
    public class MileageDetailDto
    {
        public int Id { get; set; }

        public int MermberIsd { get; set; }

        public int TotalMile { get; set; }

        public int OriginalMile { get; set; }

        public int ChangeMile { get; set; }

        public int FinalMile { get; set; }

        public DateTime MileValidity { get; set; }


        public string MileReason { get; set; }

 
        public string OrderNumber { get; set; }

        public DateTime ChangeTime { get; set; }
    }
}
