using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Dto.Airtype_Owns
{
    public class AirSeatDto
    {
        public int Id { get; set; }

        public int AirTypeId { get; set; }

        public int AirCabinId { get; set; }

        public int? SeatNum { get; set; }
    }
}
