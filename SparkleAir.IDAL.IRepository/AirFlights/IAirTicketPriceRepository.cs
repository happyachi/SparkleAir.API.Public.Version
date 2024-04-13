using SparkleAir.Infa.Entity.AirFlightsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.IDAL.IRepository.AirFlights
{
    public interface IAirTicketPriceRepository
    {
        Task CreateTicketPirce1500(int id);
        Task CreateTicketPirce2500(int id);
        Task CreateTicketPirce5000(int id);
        Task CreateTicketPirce10000(int id);

        List<AirTicketPriceEntity> GetFlightPrice(int airFlightManagementId, string flightModel);
    }
}
