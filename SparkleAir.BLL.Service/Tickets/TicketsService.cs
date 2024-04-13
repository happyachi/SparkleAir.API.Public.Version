using Google.Apis.Drive.v3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SparkleAir.IDAL.IRepository.Tickets;
using SparkleAir.Infa.Dto;
using SparkleAir.Infa.Dto.AirFlights;
using SparkleAir.Infa.Dto.LuggageOrders;
using SparkleAir.Infa.Dto.Members;
using SparkleAir.Infa.Dto.Tickets;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.Tickets;
using SparkleAir.Infa.Utility.Helper;
using SparkleAir.Infa.Utility.Helper.Jwts;
using SparkleAir.Infa.Utility.Helper.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.BLL.Service.Tickets
{
    public class TicketsService
    {
        private ITicketsRepository _ticketsRepo;
        private AppDbContext _context;
        private readonly IConfiguration _configuration;
        private TicketsHelper helper;
        public TicketsService(ITicketsRepository ticketsRepo, AppDbContext context, IConfiguration configuration)
        {
            _ticketsRepo = ticketsRepo;
            _context = context;
            _configuration = configuration;
            helper = new TicketsHelper(context);
        }

        //orderNum  Guid.NewGuid().toString("N")自動生成(?
        public async Task<int> CreateTicket(TicketsDto dto, int paymentId)
        {
            DateTime now = DateTime.Now;
            TicketsEntity entity = new TicketsEntity
            {
                Id = dto.Id,
                MemberId = dto.MemberId,
                AirFlightId = dto.AirFlightId,
                OrderNum = helper.CreateOrderNum(),
                TotalPayableAmount = dto.TotalPayableAmount,
                ActualPaidAmount = dto.ActualPaidAmount,
                OrderTime = now,
                TransferPaymentId = paymentId,
                IsEstablished = true,
                MileRedemption = dto.MileRedemption,
                TotalGeneratedMile = dto.TotalGeneratedMile,
                IsInvoiced = true
            };
            var ticketId = await _ticketsRepo.CreateTicket(entity);
            return ticketId;
        }

        //BookingRef 自動生成 6碼
        public async Task<(int, string)> CreateTicketDetail(int ticketId, TicketsDetailDto dto)
        {
            TicketsDetailEntity entity = new TicketsDetailEntity
            {
                Id = dto.Id,
                TicketId = ticketId,
                AirCabinRuleId = dto.AirCabinRuleId,
                TypeofPassengerId = dto.TypeofPassengerId,
                TicketAmount = dto.TicketAmount,
                AccruedMile = dto.AccruedMile,
                BookRef = TicketsHelper.CreateBookingRef()
            };
            var ticketDetail = await _ticketsRepo.CreateTicketDetail(entity);
            return (ticketDetail.Item1, ticketDetail.Item2);
        }

        //TicketNum 自動生成 13碼
        public async Task<int> CreateTicketInvoce(int ticketDetailId, TicketInvoicingDetailDto dto, int airFlightId)
        {
            TicketInvoicingDetailEntity entity = new TicketInvoicingDetailEntity
            {
                Id = dto.Id,
                TicketDetailId = ticketDetailId,
                AirFlightSeatId = dto.AirFlightSeatId,
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                CountryId = dto.CountryId,
                DateofBirth = dto.DateofBirth,
                Gender = dto.Gender,
                PassportNum = dto.PassportNum,
                PassportExpirationDate = dto.PassportExpirationDate,
                Remark = dto.Remark,
                TicketNum = TicketsHelper.CreateTicketNum(),
                AirMealId = dto.AirMealId,
                CheckInStatus = false,
                CheckInTime = dto.CheckInTime
            };
            var id = await _ticketsRepo.CreateTicketInvoce(entity, airFlightId);
            return id;
        }

        public TicketInvoicingDetailDto GetTicketInvoicingDetail(string ticketNum, string firstName, string lastName)
        {
            throw new NotImplementedException();
        }


        public string LoginByTicketAndName(TicketsLoginDto loginDto)
        {
            var ticketDetailEntity = _ticketsRepo.GetTicketInvoicingDetail(loginDto.BookRef, loginDto.FirstName, loginDto.LastName);


            if (ticketDetailEntity != null)
            {

                Dictionary<string, string> claimsValue = new Dictionary<string, string>();

                claimsValue.Add("TicketDetailId", ticketDetailEntity.TicketDetailId.ToString());
                claimsValue.Add("AirFlightSeatId", ticketDetailEntity.AirFlightSeatId.ToString());
                claimsValue.Add("LastName", ticketDetailEntity.LastName);
                claimsValue.Add("FirstName", ticketDetailEntity.FirstName);
                claimsValue.Add("CountryId", ticketDetailEntity.CountryId.ToString());
                claimsValue.Add("DateofBirth", ticketDetailEntity.DateofBirth.ToString());
                claimsValue.Add("Gender", ticketDetailEntity.Gender.ToString());
                claimsValue.Add("PassportNum", ticketDetailEntity.PassportNum.ToString());
                claimsValue.Add("PassportExpirationDate", ticketDetailEntity.PassportExpirationDate.ToString());
                claimsValue.Add("Remark", ticketDetailEntity.Remark.ToString());
                claimsValue.Add("TicketNum", ticketDetailEntity.TicketNum.ToString());
                claimsValue.Add("AirMealId", ticketDetailEntity.AirMealId.ToString());
                claimsValue.Add("CheckInStatus", ticketDetailEntity.CheckInStatus.ToString());
                claimsValue.Add("CheckInTime", ticketDetailEntity.CheckInTime.ToString());
                claimsValue.Add("TicketInvoicingDetailsId", ticketDetailEntity.Id.ToString());



                JwtHelper jwtHelper = new JwtHelper(_configuration);
                string token = jwtHelper.GenerateToken(ticketDetailEntity.FirstName, claimsValue);

                return token;
            }
            else
            {
                throw new Exception("登入失敗");
            }
        }


        public List<AirFlightDto> GetAirFlightListByMemberId(int memberId)
        {
            var ticketsList = _context.Tickets
                .Include(t => t.AirFlight)
                .Include(t => t.AirFlight.AirFlightManagement)
                .Include(t => t.AirFlight.AirFlightManagement.ArrivalAirport)
                .Where(t => t.MemberId == memberId);
            //.Where(t => t.MemberId == memberId && t.AirFlight.ScheduledDeparture > DateTime.Now.AddDays(1));


            var airFlightList = ticketsList.Select(t => new AirFlightDto
            {
                Id = t.AirFlight.Id,
                ScheduledDeparture = t.AirFlight.ScheduledDeparture.AddHours(_context.AirPorts.Where(a => a.Id == t.AirFlight.AirFlightManagement.DepartureAirportId).Select(a => a.TimeArea).FirstOrDefault()),
                ScheduledArrival = t.AirFlight.ScheduledArrival.AddHours(_context.AirPorts.Where(a => a.Id == t.AirFlight.AirFlightManagement.ArrivalAirportId).Select(a => a.TimeArea).FirstOrDefault()),
                DepartureAirport = _context.AirPorts.Where(a => a.Id == t.AirFlight.AirFlightManagement.DepartureAirportId).Select(a => $"{a.City}({a.Lata})").FirstOrDefault(),
                ArrivalAirport = _context.AirPorts.Where(a => a.Id == t.AirFlight.AirFlightManagement.ArrivalAirportId).Select(a => $"{a.City}({a.Lata})").FirstOrDefault(),
                FlightCode = t.AirFlight.AirFlightManagement.FlightCode,
                //DepartureTimeZone = ,
                //ArrivalTimeZone = _context.AirPorts.Where(a => a.Id == t.AirFlight.AirFlightManagement.ArrivalAirportId).Select(a => a.Zone).FirstOrDefault(),
            })
                .ToList();

            return airFlightList;
        }


        public List<TicketPassengerInfoDto> GetPassengerByMemberIdAndFlightId(int memberId, int flightId)
        {
            var ticketList = _context.Tickets.Where(t => t.MemberId == memberId && t.AirFlightId == flightId).AsNoTracking().ToList();

            var ticketPassengerInfoList = new List<TicketPassengerInfoDto>();

            if (ticketList.Count > 0)
            {
                var infoDtoList = _context.TicketInvoicingDetails
                    .Include(t => t.TicketDetail)
                    .Include(t => t.TicketDetail.Ticket)
                    .Include(t => t.TicketDetail.Ticket.AirFlight.AirFlightManagement)
                    .Where(t => t.TicketDetail.Ticket.MemberId == memberId && t.TicketDetail.Ticket.AirFlightId == flightId)
                    .Select(t => new TicketPassengerInfoDto
                    {
                        TicketInvoicingDetailId = t.Id,
                        TicketsDetailId = t.TicketDetailId,
                        LastName = t.LastName,
                        FirstName = t.FirstName,
                        FlightCode = t.TicketDetail.Ticket.AirFlight.AirFlightManagement.FlightCode,
                        DepartureTime = t.TicketDetail.Ticket.AirFlight.ScheduledDeparture.AddHours(t.TicketDetail.Ticket.AirFlight.AirFlightManagement.DepartureAirport.Zone),
                        BookRef = t.TicketDetail.BookRef,
                        TicketNum = t.TicketNum
                    })
                    .AsNoTracking()
                    .ToList();

                ticketPassengerInfoList.AddRange(infoDtoList);
            }

            return ticketPassengerInfoList;
        }

        public BookingManagementDto GetTicketInfoByTicketInvoicingDetailsId(int ticketInvoicingDetailsId)
        {
            //dto.ScheduledDeparture.Date + TimeZoneHelper.ConvertToGMT(dto.ScheduledDeparture.TimeOfDay, dto.DepartureTimeZone)
            var bookingDto =  _context.TicketInvoicingDetails
                .Include(t => t.TicketDetail)
                .Include(t=>t.TicketDetail.Ticket.AirFlight.AirFlightManagement.ArrivalAirport)
                .Include(t=>t.TicketDetail.Ticket.AirFlight.AirFlightManagement.DepartureAirport)                
                .Where(t => t.Id == ticketInvoicingDetailsId)
                .Select(t => new BookingManagementDto
                {
                    LastName = t.LastName,
                    FirstName = t.FirstName,
                    BookRef = t.TicketDetail.BookRef,
                    FlightCode = t.TicketDetail.Ticket.AirFlight.AirFlightManagement.FlightCode,
                    ScheduledDeparture = t.TicketDetail.Ticket.AirFlight.ScheduledDeparture.Date + TimeZoneHelper.ConvertToLocal(t.TicketDetail.Ticket.AirFlight.ScheduledDeparture.TimeOfDay, t.TicketDetail.Ticket.AirFlight.AirFlightManagement.DepartureAirport.TimeArea),
                    ScheduledArrival = t.TicketDetail.Ticket.AirFlight.ScheduledArrival.Date + TimeZoneHelper.ConvertToLocal(t.TicketDetail.Ticket.AirFlight.ScheduledArrival.TimeOfDay, t.TicketDetail.Ticket.AirFlight.AirFlightManagement.ArrivalAirport.TimeArea),

                    DepartureAirportCity = _context.AirPorts.Where(a => a.Id == t.TicketDetail.Ticket.AirFlight.AirFlightManagement.DepartureAirportId).Select(a => $"{a.Country} - {a.City}").FirstOrDefault(),

                    ArrivalAirportCity = _context.AirPorts.Where(a => a.Id == t.TicketDetail.Ticket.AirFlight.AirFlightManagement.ArrivalAirportId).Select(a => $"{a.Country} - {a.City}").FirstOrDefault(),

                    DepartureAirportName = _context.AirPorts.Where(a => a.Id == t.TicketDetail.Ticket.AirFlight.AirFlightManagement.DepartureAirportId).Select(a => $"{a.AirPortName} - {a.Lata}").FirstOrDefault(),

                    ArrivalAirportName = _context.AirPorts.Where(a => a.Id == t.TicketDetail.Ticket.AirFlight.AirFlightManagement.ArrivalAirportId).Select(a => $"{a.AirPortName} - {a.Lata}").FirstOrDefault(),

                    AirFlightStatuses = _context.AirFlightStatuses
                                        .Where(a=>a.Id == _context.AirTakeOffStatuses
                                                        .Where(air=>air.AirFlightId == t.TicketDetail.Ticket.AirFlightId)
                                                        .Select(air=>air.AirFlightStatusId)
                                                        .FirstOrDefault())
                                        .Select(a=>a.Status)
                                        .FirstOrDefault()
                })
                .FirstOrDefault();

            return bookingDto;  
        }

        public IEnumerable<LuggageOrderDto> GetLuggageListByTicketInvoicingDetailsId(int ticketInvoicingDetailsId)
        {
            var luggageOrderDtoList = _context.LuggageOrders
                .Where(l => l.TicketInvoicingDetailId == ticketInvoicingDetailsId)
                .Select(l => new LuggageOrderDto
                {
                    Amount = l.Amount,
                    Price = l.Price,
                    OrderTime = l.OrderTime,
                    LuggagePrice = l.Luggage.BookPrice,
                })
                .OrderByDescending(l=>l.OrderTime);
            return luggageOrderDtoList;
        }

        public TicketsDto GetTicketByMemberIdAndFlightId(int memberId, int flightId)
        {
            var dto = _context.Tickets
                .Where(t => t.MemberId == memberId && t.AirFlightId == flightId)
                .Select(t=> new TicketsDto
                {
                    OrderNum = t.OrderNum,
                    TotalPayableAmount = t.TotalPayableAmount,
                    ActualPaidAmount = t.ActualPaidAmount,
                    OrderTime = t.OrderTime,
                    MileRedemption = t.MileRedemption,
                    TotalGeneratedMile = t.TotalGeneratedMile,
                })
                .AsNoTracking()
                .FirstOrDefault();

            return dto;
        }
    }
}
