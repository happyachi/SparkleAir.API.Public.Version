using SparkleAir.IDAL.IRepository.AirFlights;
using SparkleAir.Infa.Criteria.AirFlights;
using SparkleAir.Infa.Dto.AirFlights;
using SparkleAir.Infa.Entity.AirFlightsEntity;
using SparkleAir.Infa.Utility.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.BLL.Service.AirFlights
{
    public class AirFlightManagementService
    {
        private readonly IAirFlightManagementRepository _repo;

        // 公司代碼
        private const string AirlineCode = "SK";

        // 判斷航班代碼是否重複
        private Func<string, List<AirFlightManagementDto>, bool> isFlightCodeExist = (s, l) =>
        {
            bool isFlight = false;
            foreach (var item in l)
            {
                isFlight = item.FlightCode == s;
                if (isFlight) return true;
            }
            return false;
        };

        public AirFlightManagementService(IAirFlightManagementRepository repo)
        {
            _repo = repo;
        }

        public int Create(AirFlightManagementDto dto)
        {
            string fullAirlineCode = AirlineCode + dto.FlightCode;

            if (isFlightCodeExist(fullAirlineCode, GetAll()))
            {
                throw new Exception("航班代碼已存在");
            }
            else if (dto.DepartureAirport == dto.ArrivalAirport)
            {
                throw new Exception("出發地以及目的地有誤");
            }
            else if (!(dto.Mile > 0))
            {
                throw new Exception("里程輸入錯誤");
            }

            TimeSpan departTime = TimeZoneHelper.ConvertToGMT(dto.DepartureTime, dto.DepartureTimeZone);
            TimeSpan arrivaTime = TimeZoneHelper.ConvertToGMT(dto.ArrivalTime, dto.ArrivalTimeZone);

            AirFlightManagementEntity entity = new AirFlightManagementEntity
            {
                Id = dto.Id,
                FlightCode = fullAirlineCode,
                DepartureAirportId = dto.DepartureAirportId,
                ArrivalAirportId = dto.ArrivalAirportId,
                DepartureTerminal = dto.DepartureTerminal,
                ArrivalTerminal = dto.ArrivalTerminal,
                DepartureTime = departTime,
                ArrivalTime = arrivaTime,
                DayofWeek = dto.DayofWeek,
                Mile = dto.Mile,
                DepartureAirport = dto.DepartureAirport,
                ArrivalAirport = dto.ArrivalAirport,
                DepartureTimeZone = dto.DepartureTimeZone,
                ArrivalTimeZone = dto.ArrivalTimeZone,
                CrossDay = dto.CrossDay,
            };

            var flightId = _repo.Create(entity);

            return flightId;
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
        }

        public List<AirFlightManagementDto> GetAll()
        {
            List<AirFlightManagementEntity> entity = _repo.GetAll();
            List<AirFlightManagementDto> dto = entity.Select(x => new AirFlightManagementDto
            {
                Id = x.Id,
                FlightCode = x.FlightCode,
                DepartureAirportId = x.DepartureAirportId,
                ArrivalAirportId = x.ArrivalAirportId,
                DepartureTerminal = x.DepartureTerminal,
                ArrivalTerminal = x.ArrivalTerminal,
                DepartureTime = TimeZoneHelper.ConvertToLocal(x.DepartureTime, x.DepartureTimeZone),
                ArrivalTime = TimeZoneHelper.ConvertToLocal(x.ArrivalTime, x.ArrivalTimeZone),
                DayofWeek = SplitFlightDaysHelper.dayofweek(x.DayofWeek),
                Mile = x.Mile,
                DepartureAirport = x.DepartureAirport,
                ArrivalAirport = x.ArrivalAirport,
                DepartureTimeZone = x.DepartureTimeZone,
                ArrivalTimeZone = x.ArrivalTimeZone,
                AirOwnId = x.AirOwnId,
                FlightModel = x.FlightModel,
                CrossDay = x.CrossDay
            }).ToList();

            return dto;
        }

        public AirFlightManagementDto GetById(int id)
        {
            AirFlightManagementEntity entity = _repo.GetById(id) ?? throw new Exception("沒有這個航班");
            AirFlightManagementDto dto = new AirFlightManagementDto
            {
                Id = entity.Id,
                FlightCode = entity.FlightCode,
                DepartureAirportId = entity.DepartureAirportId,
                ArrivalAirportId = entity.ArrivalAirportId,
                DepartureTerminal = entity.DepartureTerminal,
                ArrivalTerminal = entity.ArrivalTerminal,
                DepartureTime = TimeZoneHelper.ConvertToLocal(entity.DepartureTime, entity.DepartureTimeZone),
                ArrivalTime = TimeZoneHelper.ConvertToLocal(entity.ArrivalTime, entity.ArrivalTimeZone),
                DayofWeek = SplitFlightDaysHelper.dayofweek(entity.DayofWeek),
                Mile = entity.Mile,
                DepartureAirport = entity.DepartureAirport,
                ArrivalAirport = entity.ArrivalAirport,
                DepartureTimeZone = entity.DepartureTimeZone,
                ArrivalTimeZone = entity.ArrivalTimeZone,
                AirOwnId = entity.AirOwnId,
                FlightModel = entity.FlightModel,
                CrossDay = entity.CrossDay,
                RegistrationNum = entity.RegistrationNum
            };
            return dto;
        }

        public void Update(AirFlightManagementDto dto)
        {
            TimeSpan departTime = TimeZoneHelper.ConvertToGMT(dto.DepartureTime, dto.DepartureTimeZone);
            TimeSpan arrivaTime = TimeZoneHelper.ConvertToGMT(dto.ArrivalTime, dto.ArrivalTimeZone);
            AirFlightManagementEntity entity = new AirFlightManagementEntity
            {
                Id = dto.Id,
                FlightCode = dto.FlightCode,
                DepartureAirportId = dto.DepartureAirportId,
                ArrivalAirportId = dto.ArrivalAirportId,
                DepartureTerminal = dto.DepartureTerminal,
                ArrivalTerminal = dto.ArrivalTerminal,
                DepartureTime = departTime,
                ArrivalTime = arrivaTime,
                DayofWeek = dto.DayofWeek,
                Mile = dto.Mile,
                DepartureAirport = dto.DepartureAirport,
                ArrivalAirport = dto.ArrivalAirport,
                DepartureTimeZone = dto.DepartureTimeZone,
                ArrivalTimeZone = dto.ArrivalTimeZone,
                CrossDay = dto.CrossDay
            };

            _repo.Update(entity);
        }

        public List<AirFlightManagementDto> Search(AirFlightManagementSearchCriteria dto)
        {

            var list = _repo.Search(dto);

            return list.Select(x => new AirFlightManagementDto
            {
                Id = x.Id,
                FlightCode = x.FlightCode,
                DepartureAirportId = x.DepartureAirportId,
                ArrivalAirportId = x.ArrivalAirportId,
                DepartureTerminal = x.DepartureTerminal,
                ArrivalTerminal = x.ArrivalTerminal,
                DepartureTime = TimeZoneHelper.ConvertToLocal(x.DepartureTime, x.DepartureTimeZone),
                ArrivalTime = TimeZoneHelper.ConvertToLocal(x.ArrivalTime, x.ArrivalTimeZone),
                DayofWeek = x.DayofWeek,
                Mile = x.Mile,
                DepartureAirport = x.DepartureAirport,
                ArrivalAirport = x.ArrivalAirport,
                DepartureTimeZone = x.DepartureTimeZone,
                ArrivalTimeZone = x.ArrivalTimeZone,
                AirOwnId = x.AirOwnId,
                CrossDay = x.CrossDay
            }).ToList();
        }
    }
}
