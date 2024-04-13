using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Dto.Airtype_Owns
{
    public class OwnDto
    {
        public int Id { get; set; }

        public int AirTypeId { get; set; }

        public string FlightModel { get; set; }
        public string RegistrationNum { get; set; }


        public string Status { get; set; }

        public int ManufactureYear { get; set; }

    }
}

