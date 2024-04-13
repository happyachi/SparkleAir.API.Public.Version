using Microsoft.EntityFrameworkCore;
using SparkleAir.IDAL.IRepository.Airtype_Owns;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.Airtype_Owns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.DAL.EFRepository.Airtype_Owns
{
    public class AirSeatRepository: IAirSeatRepository
    {
        private readonly AppDbContext db;
        public AirSeatRepository(AppDbContext context)
        {
            db = context;
        }
        public List<AirSeatEntity> GetAll()
        {

            var seats = db.AirCabinSeats
                .AsNoTracking()
                .Select(p => new AirSeatEntity
                {
                    Id = p.Id,
                    AirTypeId = p.AirTypeId,
                    AirCabinId = p.AirCabinId,
                    SeatNum = p.SeatNum

                })
                .ToList();

            return seats;
        }
        public List<AirSeatEntity> GetByFlightModel(string flightModel)
        {

            var seats = db.AirCabinSeats
                .AsNoTracking()
                .Where(x=>x.AirType.FlightModel == flightModel)
                .Select(p => new AirSeatEntity
                {
                    Id = p.Id,
                    AirTypeId = p.AirTypeId,
                    AirCabinId = p.AirCabinId,
                    SeatNum = p.SeatNum

                })
                .ToList();

            return seats;
        }
    }
}
