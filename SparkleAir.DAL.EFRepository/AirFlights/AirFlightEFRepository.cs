using Microsoft.EntityFrameworkCore;
using SparkleAir.IDAL.IRepository.AirFlights;
using SparkleAir.Infa.Criteria.AirFlights;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.AirFlightsEntity;
using SparkleAir.Infa.Utility.Exts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.DAL.EFRepository.AirFlights
{
    public class AirFlightEFRepository : IAirFlightRepository
    {
        private AppDbContext db;

        public AirFlightEFRepository(AppDbContext appDbContext)
        {
            db = appDbContext;
        }

        //private Func<AirFlight, AirFlightEntity> ToEntityFunc = (f) => f.ToAirFlightEntity();

        private AirFlightEntity ToEntityFunc(AirFlight air)
        {
            return air.ToAirFlightEntity(db);
        }

        public async Task<(int, string)> Create(AirFlightEntity entity)
        {
            AirFlight airFlight = new AirFlight
            {
                Id = entity.Id,
                AirOwnId = (int)entity.RegistrationNum.GetAirOwnIdByFlightModel(db),
                AirFlightManagementId = entity.AirFlightManagementId,
                ScheduledDeparture = entity.ScheduledDeparture,
                ScheduledArrival = entity.ScheduledArrival,
                AirFlightSaleStatusId = entity.AirFlightSaleStatusId
            };
            db.AirFlights.Add(airFlight);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
            var id = airFlight.Id;
            var ati = airFlight.AirOwn.AirType.FlightModel;
            return (id, ati);
        }

        public List<AirFlightEntity> GetAll()
        {
            var flights = db.AirFlights
                .AsNoTracking()
                .Include(x => x.AirOwn)
                .Include(x => x.AirFlightManagement)
                .Include(x => x.AirOwn.AirType)
                .Include(x => x.AirFlightSaleStatus)
                .ToList()
                .Select(ToEntityFunc)
                .ToList();
            return flights;
        }

        public Task<List<AirFlightEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public AirFlightEntity GetById(int id)
        {
            var flight = db.AirFlights.AsNoTracking()
                .Include(x => x.AirOwn)
                .Include(x => x.AirFlightManagement)
                .Include(x => x.AirOwn.AirType)
                .Include(x => x.AirFlightSaleStatus)
                .Where(p => p.Id == id).FirstOrDefault();


            return flight.ToAirFlightEntity(db);
        }

        public async Task UpdateSaleStatusAsync(AirFlightEntity entity)
        {
            var flight = db.AirFlights
         .Include(f => f.AirOwn)
         .FirstOrDefault(f => f.Id == entity.Id);

            if (flight != null)
            {
                flight.Id = entity.Id;
                flight.AirOwnId = (int)entity.RegistrationNum.GetAirOwnIdByFlightModel(db);
                flight.AirFlightManagementId = entity.AirFlightManagementId;
                flight.ScheduledDeparture = entity.ScheduledDeparture;
                flight.ScheduledArrival = entity.ScheduledArrival;
                flight.AirFlightSaleStatusId = entity.AirFlightSaleStatusId;
            }
            await db.SaveChangesAsync();

        }

        //public List<AirFlightEntity> Search(AirFlightCriteria criteria)
        //{
        //    var query = db.AirFlights
        //        .AsNoTracking()
        //        .Include(x => x.AirOwn)
        //        .Include(x => x.AirFlightManagement)
        //        .Include(x => x.AirOwn.AirType)
        //        .Include(x => x.AirFlightSaleStatus)
        //        .ToList()
        //        .Select(ToEntityFunc);

        //    if (!string.IsNullOrEmpty(criteria.FlightCode))
        //    {
        //        query = query.Where(e => e.FlightCode.Contains(criteria.FlightCode));
        //    }

        //    if (!string.IsNullOrEmpty(criteria.DepartureAirport))
        //    {
        //        query = query.Where(e => e.DepartureAirport == criteria.DepartureAirport);
        //    }

        //    if (!string.IsNullOrEmpty(criteria.ArrivalAirport))
        //    {
        //        query = query.Where(e => e.ArrivalAirport == criteria.ArrivalAirport);
        //    }

        //    if (criteria.DepartureStartTime != default(DateTime))
        //    {
        //        query = query.Where(e => e.ScheduledDeparture >= criteria.DepartureStartTime);
        //    }
        //    if (criteria.DepartureEndTime != default(DateTime))
        //    {
        //        query = query.Where(e => e.ScheduledDeparture <= criteria.DepartureEndTime);
        //    }
        //    if (criteria.ArrivalStartTime != default(DateTime))
        //    {
        //        query = query.Where(e => e.ScheduledArrival >= criteria.ArrivalStartTime);
        //    }
        //    if (criteria.ArrivalEndTime != default(DateTime))
        //    {
        //        query = query.Where(e => e.ScheduledArrival <= criteria.ArrivalEndTime);
        //    }
        //    return query.ToList();
        //}

        public List<AirFlightEntity> Search(AirFlightCriteria criteria)
        {
            var query = db.AirFlights
                .AsNoTracking()
                .Include(x => x.AirOwn)
                .Include(x => x.AirFlightManagement)
                .Include(x => x.AirOwn.AirType)
                .Include(x => x.AirFlightSaleStatus)
                .ToList()
                .Select(ToEntityFunc);

            if (!string.IsNullOrEmpty(criteria.FlightCode))
            {
                query = query.Where(e => e.FlightCode == criteria.FlightCode);
            }

            if (!string.IsNullOrEmpty(criteria.DepartureAirport))
            {
                query = query.Where(e => e.DepartureAirport == criteria.DepartureAirport);
            }

            if (!string.IsNullOrEmpty(criteria.ArrivalAirport))
            {
                query = query.Where(e => e.ArrivalAirport == criteria.ArrivalAirport);
            }

            if (criteria.DepartureStartTime != default(DateTime))
            {
                query = query.Where(e => e.ScheduledDeparture.Date == criteria.DepartureStartTime.Date);
            }
            if (criteria.DepartureEndTime != default(DateTime))
            {
                query = query.Where(e => e.ScheduledDeparture.Date == criteria.DepartureEndTime.Date);
            }
            if (criteria.ArrivalStartTime != default(DateTime))
            {
                query = query.Where(e => e.ScheduledArrival.Date == criteria.ArrivalStartTime.Date);
            }
            if (criteria.ArrivalEndTime != default(DateTime))
            {
                query = query.Where(e => e.ScheduledArrival.Date == criteria.ArrivalEndTime.Date);
            }
            return query.ToList();
        }
    }
}
