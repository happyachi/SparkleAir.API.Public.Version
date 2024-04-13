using Microsoft.Extensions.Configuration;
using SparkleAir.DAL.DapperRepository.AirFlights;
using SparkleAir.DAL.EFRepository.AirFlights;
using SparkleAir.IDAL.IRepository.AirFlights;
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
using static Dapper.SqlMapper;

namespace SparkleAir.BLL.Service.AirFlights
{
    public class AirTakeOffService
    {
        private readonly IAirTakeOffRepository _repo;
        private readonly IAirFlightRepository _airFlightRepo;
        private readonly IConfiguration _configuration;
        public AirTakeOffService(IAirTakeOffRepository repo, IConfiguration configuration)
        {
            _repo = repo;

            // Dapper
            _configuration = configuration;
            var connectionString = configuration.GetConnectionString("AppDbContext");
            _airFlightRepo = new AirFlightDapperRepository(connectionString);
        }


        public async Task UpdateStatus()
        {
            var allFlights = await _airFlightRepo.GetAllAsync();
            var todaysFlight = allFlights.Where(x => x.ScheduledDeparture.Date == DateTime.Now.Date).ToList();

            foreach (var flight in todaysFlight)
            {
                var departureTime = flight.ScheduledDeparture.Date + TimeZoneHelper.ConvertToLocal(flight.ScheduledDeparture.TimeOfDay, 8);
                var arrivalTime = flight.ScheduledArrival.Date + TimeZoneHelper.ConvertToLocal(flight.ScheduledArrival.TimeOfDay, 8);

                //當 datetime now 等於 departuretime 更新takeoff
                if (departureTime <= DateTime.Now)
                {
                    AirTakeOffEntity takeoff = new AirTakeOffEntity
                    {
                        ActualArrivalTime = flight.ScheduledArrival,
                        ActualDepartureTime = flight.ScheduledDeparture,
                        AirFlightStatusId = 2
                    };
                     _repo.TakeOffUpdate(takeoff);
                }

                //當 datetime now 等於 arrivaltime 更新arrival
                if (arrivalTime <= DateTime.Now)
                {
                    AirTakeOffEntity takeoff = new AirTakeOffEntity
                    {
                        ActualArrivalTime = flight.ScheduledArrival,
                        ActualDepartureTime = flight.ScheduledDeparture,
                        AirFlightStatusId = 3
                    };
                     _repo.ArrivalUpdate(takeoff);
                }
            }
        }

        //依照航線(出發地 目的地) OR FlightCode 來查詢 當日飛機
        public List<AirTakeOffDto> GetAirTakeOffList(TakeOffCriteria criteria)
        {
            DateTime currentDateTime = DateTime.UtcNow;
            var result = _repo.GetAirTakeOffList(criteria).Where(x => x.OriginalDepartureTime.Date == currentDateTime.Date || x.OriginalArrivalTime.Date == currentDateTime.Date).ToList();
             //TakeOffUpdate(result);
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

        private void TakeOffUpdate(List<AirTakeOffEntity> entity)
        {
            DateTime currentDateTime = DateTime.UtcNow;
            foreach (var item in entity)
            {
                if (currentDateTime >= item.OriginalArrivalTime  &&  item.AirFlightStatusId == 2)
                {
                     _repo.ArrivalUpdate(item);
                }
                if (currentDateTime >= item.OriginalDepartureTime&&  item.AirFlightStatusId == 1)
                {
                     _repo.TakeOffUpdate(item);
                }
            }
        }

        public async Task Create()
        {
            var allFlights = await _airFlightRepo.GetAllAsync();
            foreach (var item in allFlights)
            {
                AirTakeOffEntity entity = new AirTakeOffEntity
                {
                    AirFlightId = item.Id,
                    ActualArrivalTime = item.ScheduledArrival,
                    ActualDepartureTime = item.ScheduledDeparture,
                    AirFlightStatusId = 1
                };
                _repo.CreateAirTakeOff(entity);
            }
        }


    }
}
