using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Dto.MileageDetails
{
    public class MileApplyDto
    {
        public int Id { get; set; }

        public int Change { get; set; }

        public int EventId { get; set; }

        public int ChoseId { get; set; }

        public int? Final { get; set; }
        public string MileReason { get; set; }

    }
}
