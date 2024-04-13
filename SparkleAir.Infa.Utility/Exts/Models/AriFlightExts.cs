using SparkleAir.Infa.Criteria.AirFlights;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.AirFlightsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Utility.Exts.Models
{
    public static class AriFlightExts
    {
        //用lata取得Id
        public static int GetAirportId(this string lata, AppDbContext db)
        {
            var data = db.AirPorts.FirstOrDefault(airPort => airPort.Lata == lata);
            return data.Id;
        }

        //用Id取得Lata
        public static (string, int) GetAirport(this int airportId, AppDbContext db)
        {
            var data = db.AirPorts.Find(airportId);

            return (data.Lata, data.TimeArea);
        }

        //取得AirOwnId 給 ToAirFlightManagementEntity 用的
        public static int? GetAirOwnId(this IEnumerable<AirFlight> airFlights)
        {
            return airFlights?.Select(f => f.AirOwnId).FirstOrDefault();
        }

        //取得AirOwnId 給 EF 用的
        public static int? GetAirOwnIdByFlightModel(this string registrationNum, AppDbContext db)
        {
            var airOwnId = db.AirOwns.Where(t => t.RegistrationNum == registrationNum).FirstOrDefault();
            return airOwnId.Id;
        }

        //取得FlightModel
        public static (string, string) GetFlightModelByAirOwnId(this AppDbContext db, int? airOwnId)
        {
            var flightModel = db.AirOwns
                            .Where(ao => ao.Id == airOwnId)
                            .Join(db.AirTypes, ao => ao.AirTypeId, ty => ty.Id, (ao, ty) => ty.FlightModel)
                            .FirstOrDefault();
            var num = db.AirOwns.Where(ao => ao.Id == airOwnId).FirstOrDefault();
            if (num == null)
            {
                return (flightModel ?? "", "");
            }
            else
            {
                return (flightModel ?? "", num.RegistrationNum);
            }
        }

        //轉換AirFlightManagement To AirFlightManagementEntity
        public static AirFlightManagementEntity ToAirFlightManagementEntity(this AirFlightManagement afm, AppDbContext appDbContext)
        {
            AppDbContext db = appDbContext;
            AirFlightManagementEntity entity = new AirFlightManagementEntity
            {
                Id = afm.Id,
                FlightCode = afm.FlightCode,
                DepartureAirportId = afm.DepartureAirportId,
                ArrivalAirportId = afm.ArrivalAirportId,
                DepartureTerminal = afm.DepartureTerminal,
                ArrivalTerminal = afm.ArrivalTerminal,
                DepartureTime =  afm.DepartureTime.ToTimeSpan(),
                ArrivalTime = afm.ArrivalTime.ToTimeSpan(),
                DayofWeek = afm.DayofWeek,
                Mile = afm.Mile,
                DepartureAirport = db.AirPorts.Find(afm.DepartureAirportId).Lata,
                ArrivalAirport = db.AirPorts.Find(afm.ArrivalAirportId).Lata,
                DepartureTimeZone = db.AirPorts.Find(afm.DepartureAirportId).TimeArea,
                ArrivalTimeZone = db.AirPorts.Find(afm.ArrivalAirportId).TimeArea,
                AirOwnId = afm.AirFlights.GetAirOwnId(),
                FlightModel = db.GetFlightModelByAirOwnId(afm.AirFlights.GetAirOwnId()).Item1,
                CrossDay = afm.CrossDay,
                RegistrationNum = afm.AirFlights.GetAirOwnId() == null ? "" : db.GetFlightModelByAirOwnId(afm.AirFlights.GetAirOwnId()).Item2
            };
            return entity;
        }
        public static EachSeatInfoEntity ToEachSeatInfoEntity(this AirBookSeat airBookSeat, int seatId)
        {
            EachSeatInfoEntity entity = new EachSeatInfoEntity
            {

                AirFlightSeatId = seatId,
                TicketInvoicingDetailId = airBookSeat.TicketInvoicingDetailId,
                ReservationTime = airBookSeat.ReservationTime,
                TransferPaymentId = airBookSeat.TransferPaymentId,
                HandlingFee = airBookSeat.HandlingFee,
                SeatAssignmentNum = airBookSeat.SeatAssignmentNum,
                SeatNum = airBookSeat.AirFlightSeat.SeatNum,
                LastName = airBookSeat.TicketInvoicingDetail.LastName,
                FirstName = airBookSeat.TicketInvoicingDetail.FirstName,
                Country = airBookSeat.TicketInvoicingDetail.Country.ChineseName,
                Gender = airBookSeat.TicketInvoicingDetail.Gender.ToString(),
                TypeofPassenger = airBookSeat.TicketInvoicingDetail.TicketDetail.TypeofPassenger.PassengerType,
                CheckInstatus = airBookSeat.TicketInvoicingDetail.CheckInStatus.ToString()
            };
            return entity;
        }

        //用迴圈跑值班表(一個月內的)
        //自動更新班表
        public static List<DateTime> GetScheduledFlights(this DateTime currentDate, FlightDate[] days, DateTime today)
        {
            var scheduledFlights = new List<DateTime>();
            var currentMonth = currentDate.Month;
            var nextMonth = currentMonth + 1;

            if (nextMonth > 12)
            {
                nextMonth = 1;
            }

            int targetMonth = nextMonth;

            for (int month = currentMonth; month <= targetMonth || (month == 12 && targetMonth == 1); month = (month % 12) + 1)
            {
                int year = (month == 1) ? currentDate.Year + 1 : currentDate.Year;
                int daysInMonth = DateTime.DaysInMonth(year, month);

                for (int day = 1; day <= daysInMonth; day++)
                {
                    var currentDay = new DateTime(year, month, day);

                    foreach (var flightDay in days)
                    {
                        // 將使用者輸入的數字 7 轉換為 DayOfWeek.Sunday
                        DayOfWeek userDayOfWeek = (DayOfWeek)((int)flightDay % 7);

                        if ((userDayOfWeek == currentDay.DayOfWeek) && currentDay >= today)
                        {
                            scheduledFlights.Add(currentDay);
                            break; // 如果符合條件，跳出內層迴圈，繼續處理下一天
                        }
                    }
                }
            }
            return scheduledFlights;
        }

        //轉換AirFlight To AirFlightEntity
        public static AirFlightEntity ToAirFlightEntity(this AirFlight af, AppDbContext appDbContext)
        {
            AppDbContext db = appDbContext;
            AirFlightEntity entity = new AirFlightEntity
            {
                Id = af.Id,
                AirOwnId = af.AirOwnId,
                AirFlightManagementId = af.AirFlightManagementId,
                ScheduledDeparture = af.ScheduledDeparture,
                ScheduledArrival = af.ScheduledArrival,
                AirFlightSaleStatusId = af.AirFlightSaleStatusId,
                FlightModel = af.AirOwn.AirType.FlightModel,
                FlightCode = af.AirFlightManagement.FlightCode,
                DepartureAirport = db.AirPorts.Find(af.AirFlightManagement.DepartureAirportId).Lata,
                ArrivalAirport = db.AirPorts.Find(af.AirFlightManagement.ArrivalAirportId).Lata,
                DepartureAirportId = db.GetFlightCode(af.AirFlightManagementId).Item1,
                ArrivalAirportId = db.GetFlightCode(af.AirFlightManagementId).Item2,
                DepartureTimeZone = db.AirPorts.Find(af.AirFlightManagement.DepartureAirportId).TimeArea,
                ArrivalTimeZone = db.AirPorts.Find(af.AirFlightManagement.ArrivalAirportId).TimeArea,
                AirFlightSaleStatus = af.AirFlightSaleStatus.Status,
                DayofWeek = af.AirFlightManagement.DayofWeek,
                RegistrationNum = af.AirOwn.RegistrationNum,
                CrossDay = af.AirFlightManagement.CrossDay,
                Mile = af.AirFlightManagement.Mile,
                ArrivalTerminal = af.AirFlightManagement.ArrivalTerminal,
                DepartureTerminal = af.AirFlightManagement.DepartureTerminal,
            };
            return entity;
        }

        public static string GetSaleStatus(this int statusId, AppDbContext db)
        {
            var status = db.AirFlightSaleStatuses.Find(statusId);
            return status.Status;
        }

        //取得ToAirFlightEntity所需相關訊息
        //出發地ID 目的地ID
        public static (int, int) GetFlightCode(this AppDbContext db, int id)
        {
            var data = db.AirFlightManagements.Find(id);
            return (data.DepartureAirportId, data.ArrivalAirportId);
        }

        public static AirFlightSeatsEntity ToFlightSeatsEntity(this AirFlightSeat seats, int airFlightId)
        {
            AirFlightSeatsEntity entity = new AirFlightSeatsEntity
            {
                Id = seats.Id,
                AirFlightId = airFlightId,
                AirCabinId = seats.AirCabinId,
                IsSeated = seats.IsSeated,
                SeatNum = seats.SeatNum,
                CabinName = seats.AirCabin.CabinClass,
                FlightModel = seats.AirFlight.AirOwn.AirType.FlightModel,
                RegisterNum = seats.AirFlight.AirOwn.RegistrationNum
            };
            return entity;
        }

        public static bool IsExist(this DateTime deptTime, int afmId, AppDbContext appDbContext)
        {
            AppDbContext db = appDbContext;
            var flights = db.AirFlights
      .Where(f => f.ScheduledDeparture == deptTime && f.AirFlightManagementId == afmId)
      .ToList();
            if (flights.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
