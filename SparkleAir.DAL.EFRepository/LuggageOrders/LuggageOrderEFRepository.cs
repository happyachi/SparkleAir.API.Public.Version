using SparkleAir.IDAL.IRepository.LuggageOrders;
using SparkleAir.Infa.Entity.LuggageOrders;
using SparkleAir.Infa.EFModel.Models;
using Microsoft.EntityFrameworkCore;

namespace SparkleAir.DAL.EFRepository.LuggageOrders
{
    public class LuggageOrderEFRepository : ILuggageOrderRepository
    {
        private AppDbContext db;
        public LuggageOrderEFRepository(AppDbContext _db)
        {
          db = _db;
        }
      


        public int Create(LuggageOrderEntity model)
        {
            LuggageOrder luggage = new LuggageOrder
            {
                //Id = model.Id,
                TicketInvoicingDetailId = model.TicketInvoicingDetailId,
                LuggageId = model.LuggageId,
                Amount = model.Amount,
                Price = model.Price,
                OrderTime = model.OrderTime,
                TransferPaymentsId = model.TransferPaymentsId,
                OrderStatus = model.OrderStatus,
                LuggageNum = model.LuggageNum,
            };
            db.LuggageOrders.Add(luggage);
            db.SaveChanges();
            return luggage.Id;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public LuggageOrderEntity Get(int id)
        {
            var get = db.LuggageOrders.Find(id);
            if (get == null)
            {
                 throw new Exception();
            }
            else
            {
                return new LuggageOrderEntity
                {
                    Id = get.Id,
                    FlightCode = get.TicketInvoicingDetail.TicketDetail.Ticket.AirFlight.AirFlightManagement.FlightCode,
                    TicketInvoicingDetailId = get.TicketInvoicingDetailId,
                    TicketInvoicingDetailName = get.TicketInvoicingDetail.FirstName + get.TicketInvoicingDetail.LastName,
                    LuggageId = get.LuggageId,
                    LuggagePrice = get.Luggage.BookPrice,
                    Amount = get.Amount,
                    Price = get.Price,
                    OrderTime = get.OrderTime,
                    TransferPaymentsId = get.TransferPaymentsId,
                    OrderStatus = get.OrderStatus,
                    LuggageNum = get.LuggageNum,
                };
            }

        }

        //從TicketInvoicingDetailId 取得 AirflightmanageId,在取得LuggagePrice
        public LuggagePriceEntity GetLuggage(int ticketinvocingdetailId)
        {
            var AirflightmanageId = db.TicketInvoicingDetails.Include(p => p.TicketDetail.Ticket.AirFlight)
                                               .Where(p => p.Id ==(ticketinvocingdetailId))
                                               .Select(p=>p.TicketDetail.Ticket.AirFlight.AirFlightManagementId)
                                               .FirstOrDefault();

            var luggageprice = db.Luggages.Where(p => p.AirFlightManagementsId == AirflightmanageId)
                                          //.Select(p => p.BookPrice)
                                          //.FirstOrDefault()
                                          .Select(p => new LuggagePriceEntity
                                          {   Id=p.Id,
                                              AirFlightManagementsId = p.AirFlightManagementsId,
                                              BookPrice = p.BookPrice,
                                              OriginalPrice = p.OriginalPrice
                                          }).FirstOrDefault();
                                          
                                          ;

            

            return luggageprice;

        }


        //public LuggageOrderEntity GetLuggageId(int id)
        //{
        //    var get = db.LuggageOrders.Find(id);
        //    if (get == null)
        //    {
        //        throw new Exception();
        //    }
        //    else
        //    {
        //        return new LuggageOrderEntity
        //        {
        //            Id = get.Id,
        //            FlightCode = get.TicketInvoicingDetail.TicketDetail.Ticket.AirFlight.AirFlightManagement.FlightCode,
        //            TicketInvoicingDetailId = get.TicketInvoicingDetailId,
        //            TicketInvoicingDetailName = get.TicketInvoicingDetail.FirstName + get.TicketInvoicingDetail.LastName,
        //            LuggageId = get.LuggageId,
        //            LuggagePrice = get.Luggage.BookPrice,
        //            Amount = get.Amount,
        //            Price = get.Price,
        //            OrderTime = get.OrderTime,
        //            TransferPaymentsId = get.TransferPaymentsId,
        //            OrderStatus = get.OrderStatus,
        //            LuggageNum = get.LuggageNum,
        //        };
        //    }

        //}



        public List<LuggageOrderEntity> GetAll()
        {
           

            var luggageorder = db.LuggageOrders.AsNoTracking()
                               .Include(p => p.TicketInvoicingDetail)
                               .Include(p =>p.Luggage)
                               .Include(p=>p.TransferPayments)
                               .OrderBy(p => p.OrderTime)
                               .Select(p => new LuggageOrderEntity
                               {
                                   
                                   Id = p.Id,
                                   FlightCode = p.TicketInvoicingDetail.TicketDetail.Ticket.AirFlight.AirFlightManagement.FlightCode,
                                   TicketInvoicingDetailId = p.TicketInvoicingDetailId,
                                   TicketInvoicingDetailName=p.TicketInvoicingDetail.FirstName+p.TicketInvoicingDetail.LastName,
                                   LuggageId = p.LuggageId,
                                   LuggagePrice=p.Luggage.BookPrice,
                                   Amount = p.Amount,
                                   Price = p.Price,
                                   OrderTime = p.OrderTime,
                                   TransferPaymentsId = p.TransferPaymentsId,
                                   OrderStatus = p.OrderStatus,
                                   LuggageNum = p.LuggageNum,
                               })
                               .ToList();

            return luggageorder;
        }

        //取得託運資料
        public List <LuggageOrderEntity> GetAllLuggage()
        {

            var luggageorderNum = db.LuggageOrders.AsNoTracking()
                               .Select(p => new LuggageOrderEntity
                               {
                                   Id = p.Id,
                                   TicketInvoicingDetailId = p.TicketInvoicingDetailId,
                                   LuggageId = p.LuggageId,
                                   Amount = p.Amount,
                                   Price = p.Price,
                                   OrderTime = p.OrderTime,
                                   TransferPaymentsId = p.TransferPaymentsId,
                                   OrderStatus = p.OrderStatus,
                                   LuggageNum = p.LuggageNum,
                               })
                               .ToList();

            return luggageorderNum;
        }


        public void Update(LuggageOrderEntity model)
        {
            throw new NotImplementedException();
        }



       

    }
}
