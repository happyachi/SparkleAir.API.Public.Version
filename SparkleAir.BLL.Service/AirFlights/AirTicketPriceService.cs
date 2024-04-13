using SparkleAir.DAL.EFRepository.Airtype_Owns;
using SparkleAir.IDAL.IRepository.AirFlights;
using SparkleAir.IDAL.IRepository.Airtype_Owns;
using SparkleAir.Infa.Dto.AirFlights;
using SparkleAir.Infa.EFModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.BLL.Service.AirFlights
{
    public class AirTicketPriceService
    {
        private readonly IAirTicketPriceRepository _repo;
 
        public AirTicketPriceService(IAirTicketPriceRepository repo)
        {
            _repo = repo;
        }

        public List<AirTicketPriceDto> GetTicketPrice(int airFlightManagementId, string flightModel)
        {
            var entity = _repo.GetFlightPrice(airFlightManagementId,flightModel);
            
            List<AirTicketPriceDto> dto = entity               
                .Select(x => new AirTicketPriceDto
                {
                    Id = x.Id,
                    Price = SetPrice(x.Price,x.CabinCode),
                    AirCabinRuleId = x.AirCabinRuleId,
                    CabinName = x.CabinName,
                    CabinCode = x.CabinCode
                }).ToList();

            return dto;
        }

        private decimal SetPrice(decimal price, string cabinCode)
        {
            Random random = new Random();
            switch(cabinCode)
            {
                //全額 1-1.05
                case "C":
                case "K":
                case "B":
                case "W":
                    return price * (decimal)(1 + random.NextDouble() * (1.05 - 1));
                //基本 0.95-1.05
                case "J":
                case "T":
                case "Y":
                case "S":
                    return price * (decimal)(0.95 + random.NextDouble() * (1.05 - 0.95));
                //超值0.85-1
                case "D":
                case "L":
                case "V":
                case "A":
                    return price * (decimal)(0.85 + random.NextDouble() * (1 - 0.85));                
                default:
                    return price;
            }
        }
    }
}
