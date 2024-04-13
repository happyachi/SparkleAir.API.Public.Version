using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Entity.AirFlightsEntity
{
    public class SeatGroupEntity
    {
        public int Id { get; set; }
        public int AirTypeId { get; set; }
        public int SeatAreaId { get; set; }
        public string SeatNum { get; set; }
    }
}
