using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Entity.Airtype_Owns
{
    public class AirSeatEntity
    {

        public int Id { get; set; }

        public int AirTypeId { get; set; }

        public int AirCabinId { get; set; }

        public int? SeatNum { get; set; }
    }
}
