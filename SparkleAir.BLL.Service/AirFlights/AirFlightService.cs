using SparkleAir.IDAL.IRepository.AirFlights;
using SparkleAir.Infa.Criteria.AirFlights;
using SparkleAir.Infa.Dto.AirFlights;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.AirFlightsEntity;
using SparkleAir.Infa.Utility.Exts.Models;
using SparkleAir.Infa.Utility.Helper;
using SparkleAir.Infa.Utility.Helper.AirFlights;

namespace SparkleAir.BLL.Service.AirFlights
{
    public class AirFlightService
    {
        private readonly IAirFlightRepository _repo;
        private readonly IAirFlightSeatsRepository _repoSeats;
        private AppDbContext _appDbContext;
        public AirFlightService(IAirFlightRepository repo, AppDbContext appDbContext)
        {
            _repo = repo;
            _appDbContext = appDbContext;
        }

        public async Task<List<(int, string)>> Create(AirFlightDto dto)
        {
            FlightDate[] days = dto.DayofWeek.FlightDays();
            var today = DateTime.Today;
            int thisMonth = today.Month;
            int thisYear = today.Year;
            DateTime currentDate = new DateTime(thisYear, thisMonth, 1);
            List<DateTime> scheduledFlights = currentDate.GetScheduledFlights(days, today);
            List<(int, string)> ids = new List<(int, string)>();
            foreach (var scheduledFlight in scheduledFlights)
            {
                // 將起飛時間和抵達時間與日期結合
                DateTime scheduledDeparture = scheduledFlight.Date + TimeZoneHelper.ConvertToGMT(dto.ScheduledDeparture.TimeOfDay, dto.DepartureTimeZone);
                DateTime scheduledArrival = scheduledFlight.Date + TimeZoneHelper.ConvertToGMT(dto.ScheduledArrival.TimeOfDay, dto.ArrivalTimeZone);

                AirFlightEntity entity = new AirFlightEntity
                {
                    AirOwnId = dto.AirOwnId,
                    FlightCode = dto.FlightCode,
                    FlightModel = dto.FlightModel,
                    DepartureAirport = dto.DepartureAirport,
                    ArrivalAirport = dto.ArrivalAirport,
                    AirFlightManagementId = dto.AirFlightManagementId,
                    AirFlightSaleStatusId = dto.AirFlightSaleStatusId,
                    AirFlightSaleStatus = dto.AirFlightSaleStatus,
                    DayofWeek = dto.DayofWeek,
                    ScheduledDeparture = scheduledDeparture,
                    ScheduledArrival = scheduledArrival.AddDays(dto.CrossDay),
                    RegistrationNum = dto.RegistrationNum,
                    CrossDay = dto.CrossDay
                };
                var flight = await _repo.Create(entity);
                ids.Add((flight.Item1, flight.Item2));

            }
            return ids;
        }

        //還未使用到
        public AirFlightDto GetById(int id)
        {
            var entity = _repo.GetById(id);
            AirFlightDto dto = new AirFlightDto
            {
                Id = entity.Id,
                AirOwnId = entity.AirOwnId,
                AirFlightManagementId = id,
                ScheduledDeparture = entity.ScheduledDeparture.Date + TimeZoneHelper.ConvertToLocal(entity.ScheduledDeparture.TimeOfDay, entity.DepartureTimeZone),
                ScheduledArrival = entity.ScheduledArrival.Date + TimeZoneHelper.ConvertToLocal(entity.ScheduledArrival.TimeOfDay, entity.ArrivalTimeZone),
                AirFlightSaleStatusId = entity.AirFlightSaleStatusId,
                FlightModel = entity.FlightModel,
                FlightCode = entity.FlightCode,
                DepartureAirport = entity.DepartureAirport,
                ArrivalAirport = entity.ArrivalAirport,
                AirFlightSaleStatus = entity.AirFlightSaleStatus,
                RegistrationNum = entity.RegistrationNum,
                ArrivalTimeZone = entity.ArrivalTimeZone,
                DepartureTimeZone = entity.DepartureTimeZone,
                ArrivalAirportId = entity.ArrivalAirportId,
                DepartureAirportId = entity.DepartureAirportId,
            };

            return dto;
        }

        public List<AirFlightDto> GetAll()
        {
            List<AirFlightEntity> entity = _repo.GetAll();

            //會過濾掉起飛的飛機
            List<AirFlightDto> dto = entity.Where(x => x.AirFlightSaleStatusId != 5).Select(x => new AirFlightDto
            {
                Id = x.Id,
                AirOwnId = x.AirOwnId,
                AirFlightManagementId = x.AirFlightManagementId,
                ScheduledDeparture = x.ScheduledDeparture.Date + TimeZoneHelper.ConvertToLocal(x.ScheduledDeparture.TimeOfDay, x.DepartureTimeZone),
                ScheduledArrival = x.ScheduledArrival.Date + TimeZoneHelper.ConvertToLocal(x.ScheduledArrival.TimeOfDay, x.ArrivalTimeZone),
                AirFlightSaleStatusId = StatusHelper.SaleStatusUpdate(x.ScheduledDeparture),
                FlightModel = x.FlightModel,
                FlightCode = x.FlightCode,
                DepartureAirport = x.DepartureAirport,
                ArrivalAirport = x.ArrivalAirport,
                AirFlightSaleStatus = x.AirFlightSaleStatus,
                DayofWeek = x.DayofWeek,
                DepartureTimeZone = x.DepartureTimeZone,
                ArrivalTimeZone = x.ArrivalTimeZone,
                DepartureAirportId = x.DepartureAirportId,
                ArrivalAirportId = x.ArrivalAirportId,
                RegistrationNum = x.RegistrationNum,
                CrossDay = x.CrossDay
            }).ToList();

            return dto;
        }

        public async Task UpdateSaleStatusAsync(AirFlightDto dto)
        {
            int stausId = StatusHelper.SaleStatusUpdate(dto.ScheduledDeparture);

            AirFlightEntity entity = new AirFlightEntity
            {
                Id = dto.Id,
                AirOwnId = dto.AirOwnId,
                AirFlightManagementId = dto.AirFlightManagementId,
                ScheduledDeparture = dto.ScheduledDeparture.Date + TimeZoneHelper.ConvertToGMT(dto.ScheduledDeparture.TimeOfDay, dto.DepartureTimeZone),
                ScheduledArrival = dto.ScheduledArrival.Date + TimeZoneHelper.ConvertToGMT(dto.ScheduledArrival.TimeOfDay, dto.ArrivalTimeZone),
                AirFlightSaleStatusId = stausId,
                FlightModel = dto.FlightModel,
                FlightCode = dto.FlightCode,
                DepartureAirport = dto.DepartureAirport,
                ArrivalAirport = dto.ArrivalAirport,
                AirFlightSaleStatus = dto.AirFlightSaleStatus,
                DayofWeek = dto.DayofWeek,
                DepartureTimeZone = dto.DepartureTimeZone,
                ArrivalTimeZone = dto.ArrivalTimeZone,
                DepartureAirportId = dto.DepartureAirportId,
                ArrivalAirportId = dto.ArrivalAirportId,
                RegistrationNum = dto.RegistrationNum
            };
            await _repo.UpdateSaleStatusAsync(entity);
        }

        public async Task<List<(int, string)>> UpdateScheduleIfNeeded(AirFlightDto dto)
        {

            FlightDate[] days = dto.DayofWeek.FlightDays();
            var today = DateTime.Today;
            //var today = new DateTime(2024, 12, 1);
            List<DateTime> scheduledFlights = today.GetScheduledFlights(days, today);
            List<(int, string)> ids = new List<(int, string)>();
            try
            {
                foreach (var scheduledFlight in scheduledFlights)
                {
                    DateTime scheduledDeparture = scheduledFlight.Date.Date + TimeZoneHelper.ConvertToGMT(dto.ScheduledDeparture.TimeOfDay, dto.DepartureTimeZone);
                    DateTime scheduledArrival = scheduledFlight.Date.Date + TimeZoneHelper.ConvertToGMT(dto.ScheduledArrival.TimeOfDay, dto.ArrivalTimeZone);

                    if (!scheduledDeparture.IsExist(dto.AirFlightManagementId, _appDbContext))
                    {
                        AirFlightEntity entity = new AirFlightEntity
                        {
                            AirOwnId = dto.AirOwnId,
                            FlightCode = dto.FlightCode,
                            FlightModel = dto.FlightModel,
                            DepartureAirport = dto.DepartureAirport,
                            ArrivalAirport = dto.ArrivalAirport,
                            AirFlightManagementId = dto.AirFlightManagementId,
                            AirFlightSaleStatusId = dto.AirFlightSaleStatusId,
                            AirFlightSaleStatus = dto.AirFlightSaleStatus,
                            DayofWeek = dto.DayofWeek,
                            ScheduledDeparture = scheduledDeparture,
                            ScheduledArrival = scheduledArrival.AddDays(dto.CrossDay),
                            RegistrationNum = dto.RegistrationNum,
                            CrossDay = dto.CrossDay
                        };

                        var flight = await _repo.Create(entity);
                        ids.Add((flight.Item1, flight.Item2));
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return ids;
        }

        public async Task<List<AirFlightDto>> GetAllAsync()
        {
            List<AirFlightEntity> entity = await _repo.GetAllAsync();

            //會過濾掉起飛的飛機
            List<AirFlightDto> dto = entity.Where(x => x.AirFlightSaleStatusId != 5).Select(x => new AirFlightDto
            {
                Id = x.Id,
                AirOwnId = x.AirOwnId,
                AirFlightManagementId = x.AirFlightManagementId,
                ScheduledDeparture = x.ScheduledDeparture.Date + TimeZoneHelper.ConvertToLocal(x.ScheduledDeparture.TimeOfDay, x.DepartureTimeZone),
                ScheduledArrival = x.ScheduledArrival.Date + TimeZoneHelper.ConvertToLocal(x.ScheduledArrival.TimeOfDay, x.ArrivalTimeZone),
                AirFlightSaleStatusId = StatusHelper.SaleStatusUpdate(x.ScheduledDeparture),
                FlightModel = x.FlightModel,
                FlightCode = x.FlightCode,
                DepartureAirport = x.DepartureAirport,
                ArrivalAirport = x.ArrivalAirport,
                AirFlightSaleStatus = x.AirFlightSaleStatus,
                DayofWeek = x.DayofWeek,
                DepartureTimeZone = x.DepartureTimeZone,
                ArrivalTimeZone = x.ArrivalTimeZone,
                DepartureAirportId = x.DepartureAirportId,
                ArrivalAirportId = x.ArrivalAirportId,
                RegistrationNum = x.RegistrationNum
            }).ToList();

            return dto;
        }

        public List<AirFlightDto> Search(AirFlightCriteria criteria)
        {

            var list = _repo.Search(criteria);

            return list.Select(x => new AirFlightDto
            {
                AirOwnId = x.AirOwnId,
                AirFlightManagementId = x.AirFlightManagementId,
                ScheduledDeparture = x.ScheduledDeparture.Date + TimeZoneHelper.ConvertToLocal(x.ScheduledDeparture.TimeOfDay, x.DepartureTimeZone),
                ScheduledArrival = x.ScheduledArrival.Date + TimeZoneHelper.ConvertToLocal(x.ScheduledArrival.TimeOfDay, x.ArrivalTimeZone),
                AirFlightSaleStatusId = StatusHelper.SaleStatusUpdate(x.ScheduledDeparture),
                FlightModel = x.FlightModel,
                FlightCode = x.FlightCode,
                DepartureAirport = x.DepartureAirport,
                ArrivalAirport = x.ArrivalAirport,
                AirFlightSaleStatus = x.AirFlightSaleStatus,
                DayofWeek = x.DayofWeek,
                DepartureTimeZone = x.DepartureTimeZone,
                ArrivalTimeZone = x.ArrivalTimeZone,
                DepartureAirportId = x.DepartureAirportId,
                ArrivalAirportId = x.ArrivalAirportId,
                RegistrationNum = x.RegistrationNum,
                Mile = x.Mile,
                CrossDay = x.CrossDay,
                Id = x.Id,
                ArrivalTerminal = x.ArrivalTerminal,
                DepartureTerminal = x.DepartureTerminal,
            }).ToList();
        }
    }
}
