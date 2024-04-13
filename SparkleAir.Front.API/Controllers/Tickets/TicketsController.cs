using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparkleAir.BLL.Service.LuggageOrderService;
using SparkleAir.BLL.Service.MileageDetails;
using SparkleAir.BLL.Service.MileOrders;
using SparkleAir.BLL.Service.Tickets;
using SparkleAir.DAL.EFRepository.LuggageOrders;
using SparkleAir.DAL.EFRepository.Luggages;
using SparkleAir.DAL.EFRepository.Members;
using SparkleAir.DAL.EFRepository.MileageDetails;
using SparkleAir.DAL.EFRepository.MileOrders;
using SparkleAir.DAL.EFRepository.Tickets;
using SparkleAir.IDAL.IRepository.LuggageOrders;
using SparkleAir.IDAL.IRepository.Members;
using SparkleAir.IDAL.IRepository.MileageDetails;
using SparkleAir.IDAL.IRepository.MileOrder;
using SparkleAir.IDAL.IRepository.Tickets;
using SparkleAir.Infa.Dto;
using SparkleAir.Infa.Dto.AirFlights;
using SparkleAir.Infa.Dto.LuggageOrders;
using SparkleAir.Infa.Dto.Members;
using SparkleAir.Infa.Dto.MileageDetails;
using SparkleAir.Infa.Dto.MileOrder;
using SparkleAir.Infa.Dto.Tickets;
using SparkleAir.Infa.EFModel.Models;

namespace SparkleAir.Front.API.Controllers.Tickets
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly TicketsService _ticketsService;
        private readonly ITicketsRepository _ticketsRepository;
        private readonly IConfiguration _configuration;

        private readonly LuggageOrderService _luggageOrderService;
        private readonly ILuggageOrderRepository _luggageOrderRepository;

        private readonly MileageOrderService _MileOrderService;
        private readonly IMileageOrderRepository _IMileageOrderRepository;

        private readonly IMemberRepository _mm;
        private readonly IMileageDetailRepository _mileageDetailRepository;
        private readonly MileageDetailService _mileageDetailService;

        public TicketsController(AppDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _ticketsRepository = new TicketsEFRepository(context);
            _ticketsService = new TicketsService(_ticketsRepository, context, configuration);

            _luggageOrderRepository = new LuggageOrderEFRepository(context);
            _luggageOrderService = new LuggageOrderService(_luggageOrderRepository, configuration);

            _mm = new MemberEFRepository(context);
            _mileageDetailRepository = new MileageDetailEFRepository(context);
            _IMileageOrderRepository = new MileageOrderEFRepository(context);
            _MileOrderService = new MileageOrderService(_mileageDetailRepository, _mm, _IMileageOrderRepository, configuration);
            _mileageDetailService = new MileageDetailService(context);
        }



        /// <summary>
        /// 藉由 訂位代號、英文名登入
        /// </summary>
        /// <param name="loginDto"></param>
        [HttpPost("loginByTicketAndName")]
        public async Task<BasicApiReturnDto> LoginByTicketAndName(TicketsLoginDto loginDto)
        {
            try
            {
                var token = _ticketsService.LoginByTicketAndName(loginDto);
                var returnDto = new BasicApiReturnDto()
                {
                    IsVerify = true,
                    Token = token,
                };

                return returnDto;
            }
            catch (Exception error)
            {
                var returnDto = new BasicApiReturnDto()
                {
                    IsVerify = false,
                    Error = error.Message,
                };
                return returnDto;
            }
        }


        [HttpGet("GetAirFlightListByMemberId")]
        public async Task<List<AirFlightDto>> GetAirFlightListByMemberId(int memberId)
        {
            var airFlightDtos = _ticketsService.GetAirFlightListByMemberId(memberId);
            return airFlightDtos;
        }


        [HttpGet("GetPassengerByMemberIdAndFlightId")]
        public async Task<List<TicketPassengerInfoDto>> GetPassengerByMemberIdAndFlightId(int memberId, int flightId)
        {
            var list = _ticketsService.GetPassengerByMemberIdAndFlightId(memberId, flightId);

            return list;
        }

        [HttpGet("GetTicketInfoByTicketInvoicingDetailsId")]
        public async Task<BookingManagementDto> GetTicketInfoByTicketInvoicingDetailsId(int ticketInvoicingDetailsId)
        {
            var bookingDto = _ticketsService.GetTicketInfoByTicketInvoicingDetailsId(ticketInvoicingDetailsId);

            return bookingDto;
        }


        [HttpGet("GetLuggageListByTicketInvoicingDetailsId")]
        public async Task<IEnumerable<LuggageOrderDto>> GetLuggageListByTicketInvoicingDetailsId(int ticketInvoicingDetailsId)
        {
            var luggageOrderDtoList = _ticketsService.GetLuggageListByTicketInvoicingDetailsId(ticketInvoicingDetailsId);

            return luggageOrderDtoList;
        }

        [HttpGet("GetTicketByMemberIdAndFlightId")]
        public async Task<TicketsDto> GetTicketByMemberIdAndFlightId(int memberId, int flightId)
        {
            var dto = _ticketsService.GetTicketByMemberIdAndFlightId(memberId, flightId);

            return dto;
        }

        //創建訂單/明細/開票
        //創建之間要先呼叫金流create
        [HttpPost("CreateTicket")]
        public async Task<string> CreateTicket(TicketInfoDto ticketInfo)
        {
            TicketsDto ticket = new TicketsDto
            {
                MemberId = (int)ticketInfo.MemberId,
                AirFlightId = (int)ticketInfo.AirFlightId,
                OrderNum = ticketInfo.OrderNum,
                TotalPayableAmount = (decimal)ticketInfo.TotalPayableAmount,
                ActualPaidAmount = (decimal)ticketInfo.ActualPaidAmount,
                OrderTime = (DateTime)ticketInfo.OrderTime,
                TransferPaymentId = (int)ticketInfo.TransferPaymentId,
                IsEstablished = true,
                MileRedemption = ticketInfo.MileRedemption,
                TotalGeneratedMile = (int)ticketInfo.TotalGeneratedMile,
                IsInvoiced = true,
            };
            var ticketId = await _ticketsService.CreateTicket(ticket, (int)ticketInfo.TransferPaymentId);

            TicketsDetailDto ticketDetail = new TicketsDetailDto
            {
                TicketId = ticketId,
                AirCabinRuleId = ticketInfo.AirCabinRuleId,
                TypeofPassengerId = (int)ticketInfo.TypeofPassengerId,
                TicketAmount = (decimal)ticketInfo.TicketAmount,
                AccruedMile = (int)ticketInfo.AccruedMile,
                BookRef = ticketInfo.BookRef,
            };
            var ticketDetailId = await _ticketsService.CreateTicketDetail(ticketId, ticketDetail);

            TicketInvoicingDetailDto ticketInvoicing = new TicketInvoicingDetailDto
            {
                TicketDetailId = ticketDetailId.Item1,
                AirFlightSeatId = ticketInfo.AirFlightSeatId,
                LastName = ticketInfo.LastName,
                FirstName = ticketInfo.FirstName,
                CountryId = 1,
                DateofBirth = (DateTime)ticketInfo.DateofBirth,
                Gender = (bool)ticketInfo.Gender,
                PassportNum = ticketInfo.PassportNum,
                PassportExpirationDate = (DateTime)ticketInfo.PassportExpirationDate,
                Remark = ticketInfo.Remark,
                TicketNum = ticketInfo.TicketNum,
                AirMealId = 4,
                CheckInStatus = (bool)ticketInfo.CheckInStatus,
                CheckInTime = ticketInfo.CheckInTime,
            };
            var ticketInvoceId = await _ticketsService.CreateTicketInvoce(ticketDetailId.Item1, ticketInvoicing, (int)ticketInfo.AirFlightId);

            MileageDetailDto mileageDetail = new MileageDetailDto
            {
                MermberIsd = (int)ticketInfo.MemberId,
                TotalMile = (int)ticketInfo.TotalMile,
                OriginalMile = (int)ticketInfo.OriginalMile,
                ChangeMile = (int)ticketInfo.ChangeMile,
                FinalMile = (int)ticketInfo.FinalMile,
                MileValidity = (DateTime)ticketInfo.MileValidity,
                MileReason = ticketInfo.MileReason,
                OrderNumber = ticketInfo.MileDetailOrderNumber,
                ChangeTime = (DateTime)ticketInfo.MileChangeTime
            };
            _mileageDetailService.Create(mileageDetail);

            return ticketDetailId.Item2;
        }
        //取消訂單
        //訂單/明細/開票狀態
    }
}
