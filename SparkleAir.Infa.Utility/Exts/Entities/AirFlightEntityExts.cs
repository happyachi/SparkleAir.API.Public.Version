using SparkleAir.Infa.Dto.AirFlights;
using SparkleAir.Infa.Entity.AirFlightsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Utility.Exts.Entities
{
    public static class AirFlightEntityExts
    {
        public static AirFlightManagementDto ToDto(this AirFlightManagementEntity entity)
        {
            AirFlightManagementDto dto = new AirFlightManagementDto
            {
                Id = entity.Id,
                FlightCode = entity.FlightCode,
                DepartureAirportId = entity.DepartureAirportId,
                ArrivalAirportId = entity.ArrivalAirportId,
                DepartureTerminal = entity.DepartureTerminal,
                ArrivalTerminal = entity.ArrivalTerminal,
                DepartureTime = entity.DepartureTime,
                ArrivalTime = entity.ArrivalTime,
                DayofWeek = entity.DayofWeek,
                Mile = entity.Mile,
            };
            return dto;
        }

        public static AirFlightManagementEntity ToEntity(this AirFlightManagementDto dto)
        {
            AirFlightManagementEntity entity = new AirFlightManagementEntity
            {
                Id = dto.Id,
                FlightCode = dto.FlightCode,
                DepartureAirportId = dto.DepartureAirportId,
                ArrivalAirportId = dto.ArrivalAirportId,
                DepartureTerminal = dto.DepartureTerminal,
                ArrivalTerminal = dto.ArrivalTerminal,
                DepartureTime = dto.DepartureTime,
                ArrivalTime = dto.ArrivalTime,
                DayofWeek = dto.DayofWeek,
                Mile = dto.Mile,
            };
            return entity;
        }

    }
}
