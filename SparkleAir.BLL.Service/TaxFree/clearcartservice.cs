using SparkleAir.IDAL.IRepository.TaxFree;
using SparkleAir.Infa.EFModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.BLL.Service.TaxFree
{
    public class clearcartservice
    {
        private AppDbContext _db;

        

        public clearcartservice(AppDbContext db)
        {
            _db = db;
            
        }

        public void CheckAndClearCart()
        {
            var now = DateTime.Now;
            var ordersToDelete = _db.Tforderlists
                .Where(order => order.TicketDetails.Ticket.AirFlight.ScheduledDeparture <= now)
                .ToList();

            _db.Tforderlists.RemoveRange(ordersToDelete);
            _db.SaveChanges();
        }
    }
}
