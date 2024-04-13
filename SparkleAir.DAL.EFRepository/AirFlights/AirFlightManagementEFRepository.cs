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
    public class AirFlightManagementEFRepository : IAirFlightManagementRepository
    {
        private AppDbContext db;

        public AirFlightManagementEFRepository(AppDbContext appDbContext)
        {
            db = appDbContext;
        }

        //private Func<AirFlightManagement, AirFlightManagementEntity> ToEntityFunc = (e) => e.ToAirFlightManagementEntity();

        private AirFlightManagementEntity ToEntityFunc(AirFlightManagement air)
        {
            return air.ToAirFlightManagementEntity(db);
        }
        public int Create(AirFlightManagementEntity entity)
        {
            AirFlightManagement airFlight = new AirFlightManagement
            {
                FlightCode = entity.FlightCode,
                DepartureAirportId = entity.DepartureAirport.GetAirportId(db),
                ArrivalAirportId = entity.ArrivalAirport.GetAirportId(db),
                DepartureTerminal = entity.DepartureTerminal,
                ArrivalTerminal = entity.ArrivalTerminal,
                DepartureTime =  TimeOnly.FromTimeSpan( entity.DepartureTime),
                ArrivalTime = TimeOnly.FromTimeSpan(entity.ArrivalTime),
                DayofWeek = entity.DayofWeek,
                Mile = entity.Mile,
                CrossDay = entity.CrossDay
            };

            db.AirFlightManagements.Add(airFlight);
            db.SaveChanges();

            return airFlight.Id;
        }

        public void Delete(int id)
        {
            var flight = db.AirFlightManagements.Find(id);

            db.AirFlightManagements.Remove(flight);

            db.SaveChanges();
        }

        public List<AirFlightManagementEntity> GetAll()
        {
            var flights = db.AirFlightManagements
                .AsNoTracking()
                .Include(a => a.AirFlights)
                .ToList()
                .Select(ToEntityFunc)
                .ToList();

            return flights;
        }

        public AirFlightManagementEntity GetById(int id)
        {
            var flight = db.AirFlightManagements
                .Find(id);
            var airplain = ToEntityFunc(flight);

            return airplain;
        }

        public void Update(AirFlightManagementEntity entity)
        {
            var flight = db.AirFlightManagements
                .FirstOrDefault(f => f.Id == entity.Id);

            if (flight != null)
            {
                flight.FlightCode = entity.FlightCode;
                flight.DepartureAirportId = entity.DepartureAirport.GetAirportId(db);
                flight.ArrivalAirportId = entity.ArrivalAirport.GetAirportId(db);
                flight.DepartureTerminal = entity.DepartureTerminal;
                flight.ArrivalTerminal = entity.ArrivalTerminal;
                flight.DepartureTime = TimeOnly.FromTimeSpan( entity.DepartureTime);
                flight.ArrivalTime = TimeOnly.FromTimeSpan(entity.ArrivalTime);
                flight.DayofWeek = entity.DayofWeek;
                flight.Mile = entity.Mile;
                flight.CrossDay = entity.CrossDay;
            }
            db.SaveChanges();
        }

        public List<AirFlightManagementEntity> Search(AirFlightManagementSearchCriteria entity)
        {
            var query = db.AirFlightManagements.AsNoTracking().ToList().Select(ToEntityFunc);

            if (!string.IsNullOrEmpty(entity.FlightCode))
            {
                query = query.Where(e => e.FlightCode.Contains(entity.FlightCode));
            }

            if (!string.IsNullOrEmpty(entity.DepartureAirport))
            {
                query = query.Where(e => e.DepartureAirport == entity.DepartureAirport);
            }

            if (!string.IsNullOrEmpty(entity.ArrivalAirport))
            {
                query = query.Where(e => e.ArrivalAirport == entity.ArrivalAirport);
            }

            if (entity.DepartureStartTime != default(TimeSpan))
            {
                query = query.Where(e => e.DepartureTime >= entity.DepartureStartTime);
            }
            if (entity.DepartureEndTime != default(TimeSpan))
            {
                query = query.Where(e => e.DepartureTime <= entity.DepartureEndTime);
            }
            if (entity.ArrivalStartTime != default(TimeSpan))
            {
                query = query.Where(e => e.ArrivalTime >= entity.ArrivalStartTime);
            }
            if (entity.ArrivalEndTime != default(TimeSpan))
            {
                query = query.Where(e => e.DepartureTime <= entity.ArrivalEndTime);
            }
            return query.ToList();
        }
    }
}
