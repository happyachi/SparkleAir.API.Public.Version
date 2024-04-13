using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Dto.Airtype_Owns
{
    public class PlaneDto
    {
        public int ID { get; set; }

        public string FlightModel { get; set; }

        public int TotalSeat { get; set; }

        public int MaxMile { get; set; }

        public int MaxWeight { get; set; }

        public string ManufactureCompany { get; set; }

        public string Introduction { get; set; }
        public string Img { get; set; }
    }
}
