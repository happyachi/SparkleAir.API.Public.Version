using SparkleAir.IDAL.IRepository.AirFlights;
using SparkleAir.Infa.Dto.AirFlights;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.AirFlightsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.BLL.Service.AirFlights
{
    public class AirFlightSeatsService
    {
        private readonly IAirFlightSeatsRepository _repo;
        private AppDbContext _appDbContext;
        public AirFlightSeatsService(IAirFlightSeatsRepository repo)
        {
            _repo = repo;
        }
        public async Task CreateSeats(int flightId, string flightModel)
        {
            if (flightModel == "777-300ER")
            {
                await _repo.Create777300ER(flightId);
            }
            if (flightModel == "787-10")
            {
                await _repo.Create78710(flightId);
            }
            if (flightModel == "A320neo")
            {
                await _repo.CreateA320neo(flightId);
            }
            if (flightModel == "A350-900")
            {
                await _repo.CreateA350900(flightId);
            }
        }

        public List<AirFlightSeatsDto> GetById(int flightId)
        {
            List<AirFlightSeatsEntity> entity = _repo.GetByFlightId(flightId);
            List<AirFlightSeatsDto> dto = entity.Select(s => new AirFlightSeatsDto
            {
                Id = s.Id,
                AirFlightId = flightId,
                AirCabinId = s.AirCabinId,
                SeatNum = s.SeatNum,
                IsSeated = s.IsSeated,
                FlightModel = s.FlightModel,
                RegisterNum = s.RegisterNum,
                CabinName = s.CabinName,
            }).ToList();
            return dto;
        }

        public EachSeatInfoDto GetEachSeatInfo(int id)
        {
            var entity = _repo.GetEachSeatInfo(id);
            var dto = new EachSeatInfoDto
            {
                AirFlightSeatId = id,
                TicketInvoicingDetailId = entity.TicketInvoicingDetailId,
                ReservationTime = entity.ReservationTime,
                TransferPaymentId = entity.TransferPaymentId,
                HandlingFee = entity.HandlingFee,
                SeatAssignmentNum = entity.SeatAssignmentNum,
                SeatNum = entity.SeatNum,
                LastName = entity.LastName,
                FirstName = entity.FirstName,
                Country = entity.Country,
                Gender = entity.Gender,
                CheckInstatus = entity.CheckInstatus,
                TypeofPassenger = entity.TypeofPassenger,
            };
            return dto;
        }
    }
}
