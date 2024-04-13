using Microsoft.EntityFrameworkCore;
using SparkleAir.IDAL.IRepository.AirFlights;
using SparkleAir.Infa.Criteria.AirFlights;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.AirFlightsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.DAL.EFRepository.AirFlights
{
    public class AirTakeOffEFRepository : IAirTakeOffRepository
    {
        private AppDbContext db;
        public AirTakeOffEFRepository(AppDbContext appDbContext)
        {
            db = appDbContext;
        }

        public void CreateAirTakeOff(AirTakeOffEntity entity)
        {
            AirTakeOffStatus status = new AirTakeOffStatus
            {
                Id = entity.Id,
                AirFlightId = entity.AirFlightId,
                ActualArrivalTime = entity.ActualArrivalTime,
                ActualDepartureTime = entity.ActualDepartureTime,
                AirFlightStatusId = entity.AirFlightStatusId
            };
            db.AirTakeOffStatuses.Add(status);
            db.SaveChanges();
        }

        public void TakeOffUpdate(AirTakeOffEntity entity)
        {
            var flight = db.AirTakeOffStatuses.FirstOrDefault(f => f.Id == entity.Id);

            if (flight != null)
            {
                flight.ActualArrivalTime = entity.ActualArrivalTime;
                flight.AirFlightStatusId = 2;
            }
            db.SaveChanges();

        }

        public void ArrivalUpdate(AirTakeOffEntity entity)
        {

            var flight = db.AirTakeOffStatuses.FirstOrDefault(f => f.Id == entity.Id);

            if (flight != null)
            {
                flight.ActualDepartureTime = entity.ActualDepartureTime;
                flight.AirFlightStatusId = 3;
            }
             db.SaveChanges();
        }

        public List<AirTakeOffEntity> GetAirTakeOffList(TakeOffCriteria criteria)
        {
            DateTime currentDateTime = DateTime.UtcNow;
            var query = db.AirTakeOffStatuses
                .AsNoTracking()
                .Include(x => x.AirFlight)
                .Include(x => x.AirFlight.AirFlightManagement)
                .Include(x => x.AirFlightStatus)
                .Include(x=>x.AirFlight.AirFlightManagement.ArrivalAirport)
                .Include(x=>x.AirFlight.AirFlightManagement.DepartureAirport)
                .Include(x=>x.AirFlight.AirOwn.AirType)
                .ToList()
                .Select(x => new AirTakeOffEntity
                {
                    Id = x.Id,
                    AirFlightId = x.AirFlightId,
                    ActualArrivalTime = x.ActualArrivalTime,
                    ActualDepartureTime = x.ActualDepartureTime,
                    AirFlightStatusId = x.AirFlightStatusId,
                    FlightCode = x.AirFlight.AirFlightManagement.FlightCode,
                    DepartureAirport = x.AirFlight.AirFlightManagement.DepartureAirport.Lata,
                    ArrivalAirport = x.AirFlight.AirFlightManagement.ArrivalAirport.Lata,
                    ArrivalTimeZone = x.AirFlight.AirFlightManagement.ArrivalAirport.TimeArea,
                    DepartureTimeZone = x.AirFlight.AirFlightManagement.DepartureAirport.TimeArea,
                    FlightModel= x.AirFlight.AirOwn.AirType.FlightModel,
                    DepartureTerminal = x.AirFlight.AirFlightManagement.DepartureTerminal,
                    ArrivalTerminal = x.AirFlight.AirFlightManagement.ArrivalTerminal,
                    DepartureAirportCity = x.AirFlight.AirFlightManagement.DepartureAirport.AirPortName,
                    ArrivalAirportCity = x.AirFlight.AirFlightManagement.ArrivalAirport.AirPortName,
                    OriginalDepartureTime = x.AirFlight.ScheduledDeparture,
                    OriginalArrivalTime = x.AirFlight.ScheduledArrival,                  
                    AirFlightStatus = x.AirFlightStatus.Status
                });

            if (!string.IsNullOrEmpty(criteria.ArrivalAirport) && !string.IsNullOrEmpty(criteria.DepartureAirport))
            {
                query = query.Where(e => e.DepartureAirport == criteria.DepartureAirport && e.ArrivalAirport == criteria.ArrivalAirport);
            }

            if (!string.IsNullOrEmpty(criteria.FlightCode))
            {
                query = query.Where(e => e.FlightCode == criteria.FlightCode);
            }

            return query.ToList();
        }
    }
}
