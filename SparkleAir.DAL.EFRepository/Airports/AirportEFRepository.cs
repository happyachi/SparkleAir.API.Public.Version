using Microsoft.EntityFrameworkCore;
using SparkleAir.IDAL.IRepository.Airport;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.Airports;

namespace SparkleAir.DAL.EFRepository.Airports
{
    public class AirportEFRepository:IAirportRepository
    {
        private AppDbContext db;
        public AirportEFRepository(AppDbContext _db)
        {
            db = _db;
        }

        public List<AirportEntity> GetAll()
        {
            var products = db.AirPorts.AsNoTracking()
                            .OrderBy(p => p.Continent)
                            .Select(p => new AirportEntity
                            {
                                Id = p.Id,
                                Lata = p.Lata,
                                Gps = p.Gps,
                                Country = p.Country,
                                City = p.City,
                                AirPortName = p.AirPortName,
                                TimeArea = p.TimeArea,
                                Zone = p.Zone,
                                CityIntroduction = p.CityIntroduction,
                                Img = p.Img,
                                Continent = p.Continent,
                            })
                            .ToList();

            return products;
        }


        public int Create(AirportEntity model)
        {
            AirPort airport = new AirPort
            {
                Lata = model.Lata,
                Gps = model.Gps,
                Country = model.Country,
                City = model.City,
                AirPortName = model.AirPortName,
                TimeArea = model.TimeArea,
                Zone = model.Zone,
                CityIntroduction = model.CityIntroduction,
                Img = model.Img,
                Continent = model.Continent
            };
            db.AirPorts.Add(airport);
            db.SaveChanges();
            return airport.Id;
        }



        public void Delete(int id)
        {
            var Airport = db.AirPorts.Find(id);
            db.AirPorts.Remove(Airport);
            db.SaveChanges();
        }

        public AirportEntity Getid(int id)
        {
            var get = db.AirPorts.Find(id);

            if (get == null)
            {
                return default(AirportEntity);
            }

           var en = new AirportEntity
            {
                Id = get.Id,
                Lata = get.Lata,
                Gps = get.Gps,
                Country = get.Country,
                City = get.City,
                AirPortName = get.AirPortName,
                TimeArea = get.TimeArea,
                Zone = get.Zone,
                CityIntroduction = get.CityIntroduction,
                Img = get.Img,
                Continent = get.Continent
            };
            return en;
        }
           
        

        public void Update(AirportEntity model)
        {
            var get = db.AirPorts.Find(model.Id);
            if (get != null)
            {
                get.Id = model.Id;
                get.Lata = model.Lata;
                get.Gps = model.Gps;
                get.Country = model.Country;
                get.City = model.City;
                get.AirPortName = model.AirPortName;
                get.TimeArea = model.TimeArea;
                get.Zone = model.Zone;
                get.CityIntroduction = model.CityIntroduction;
                get.Img = model.Img;
                get.Continent = model.Continent;

            }

            db.SaveChanges();
        }
    }
}
