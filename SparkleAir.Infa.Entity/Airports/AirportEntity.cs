using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Entity.Airports
{
    public class AirportEntity
    {
        //public AirportEntity(int id,string lata,string gps,string country,string city,string airportname,int timeArea,int zone )
        //{
        //    Id=id; Lata= lata; Gps = gps; Country = country; City = city; AirPortName = airportname; TimeArea = timeArea; Zone = zone;
        //}
        public int Id { get; set; }


        public string Lata { get; set; }


        public string Gps { get; set; }

        public string Country { get; set; }


        public string City { get; set; }


        public string AirPortName { get; set; }

        public int TimeArea { get; set; }

        public int Zone { get; set; }


        public string CityIntroduction { get; set; }


        public string Img { get; set; }


        public string Continent { get; set; }
    }
}
