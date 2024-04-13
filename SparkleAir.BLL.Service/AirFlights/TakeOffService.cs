using Microsoft.EntityFrameworkCore;
using SparkleAir.Infa.Criteria.AirFlights;
using SparkleAir.Infa.Dto.AirFlights;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.AirFlightsEntity;
using SparkleAir.Infa.Utility.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.BLL.Service.AirFlights
{
    public class TakeOffService
    {
        private AppDbContext _appDbContext;
        public TakeOffService(AppDbContext db)
        {
            _appDbContext = db;
        }
        public List<AirTakeOffDto> GetAirTakeOff(TakeOffCriteria criteria)
        {
            DateTime currentDateTime = DateTime.UtcNow;
            var result = GetAirTakeOffList(criteria).Where(x => x.OriginalDepartureTime.Date == currentDateTime.Date || x.OriginalArrivalTime.Date == currentDateTime.Date).ToList();
            TakeOffUpdate(result);
            return result.Select(x => new AirTakeOffDto
            {
                Id = x.Id,
                AirFlightId = x.AirFlightId,
                ActualArrivalTime = x.ActualArrivalTime.Date + TimeZoneHelper.ConvertToLocal(x.ActualArrivalTime.TimeOfDay, x.ArrivalTimeZone),
                ActualDepartureTime = x.ActualDepartureTime.Date + TimeZoneHelper.ConvertToLocal(x.ActualDepartureTime.TimeOfDay, x.DepartureTimeZone),
                AirFlightStatusId = x.AirFlightStatusId,
                FlightCode = x.FlightCode,
                ArrivalAirport = x.ArrivalAirport,
                DepartureAirport = x.DepartureAirport,
                ArrivalAirportCity = x.ArrivalAirportCity,
                DepartureAirportCity = x.DepartureAirportCity,
                ArrivalTimeZone = x.ArrivalTimeZone,
                DepartureTimeZone = x.DepartureTimeZone,
                FlightModel = x.FlightModel,
                AirFlightStatus = x.AirFlightStatus,
                DepartureTerminal = x.DepartureTerminal,
                ArrivalTerminal = x.ArrivalTerminal,
                OriginalArrivalTime = x.OriginalArrivalTime.Date + TimeZoneHelper.ConvertToLocal(x.OriginalArrivalTime.TimeOfDay, x.ArrivalTimeZone),
                OriginalDepartureTime = x.OriginalDepartureTime.Date + TimeZoneHelper.ConvertToLocal(x.OriginalDepartureTime.TimeOfDay, x.DepartureTimeZone),
            }).ToList();
        }

        public void TakeOffUpdate(List<AirTakeOffEntity> entity)
        {
            DateTime currentDateTime = DateTime.UtcNow;
            foreach (var item in entity)
            {
                if (currentDateTime >= item.OriginalArrivalTime && item.AirFlightStatusId == 2)
                {
                    ArrivalUpdate(item);
                }
                if (currentDateTime >= item.OriginalDepartureTime && item.AirFlightStatusId == 1)
                {
                    TakeOffUpdate(item);
                }
            }
        }
        public void TakeOffUpdate(AirTakeOffEntity entity)
        {
            var flight = _appDbContext.AirTakeOffStatuses.FirstOrDefault(f => f.Id == entity.Id);

            if (flight != null)
            {
                flight.ActualArrivalTime = entity.ActualArrivalTime;
                flight.AirFlightStatusId = 2;
            }
            _appDbContext.SaveChanges();

        }

        public void ArrivalUpdate(AirTakeOffEntity entity)
        {

            var flight = _appDbContext.AirTakeOffStatuses.FirstOrDefault(f => f.Id == entity.Id);

            if (flight != null)
            {
                flight.ActualDepartureTime = entity.ActualDepartureTime;
                flight.AirFlightStatusId = 3;
            }
            _appDbContext.SaveChanges();
        }

        public List<AirTakeOffEntity> GetAirTakeOffList(TakeOffCriteria criteria)
        {
            DateTime currentDateTime = DateTime.UtcNow;
            var query = _appDbContext.AirTakeOffStatuses
                .AsNoTracking()
                .Include(x => x.AirFlight)
                .Include(x => x.AirFlight.AirFlightManagement)
                .Include(x => x.AirFlightStatus)
                .Include(x => x.AirFlight.AirFlightManagement.ArrivalAirport)
                .Include(x => x.AirFlight.AirFlightManagement.DepartureAirport)
                .Include(x => x.AirFlight.AirOwn.AirType)
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
                    FlightModel = x.AirFlight.AirOwn.AirType.FlightModel,
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
