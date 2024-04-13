using SparkleAir.Infa.Criteria.AirFlights;
using SparkleAir.Infa.Entity.AirFlightsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.IDAL.IRepository.AirFlights
{
    public interface IAirFlightRepository
    {
        Task<(int, string)> Create(AirFlightEntity entity);
        AirFlightEntity GetById(int id);
        List<AirFlightEntity> GetAll();
        Task UpdateSaleStatusAsync(AirFlightEntity entity);
        Task<List<AirFlightEntity>> GetAllAsync();
        List<AirFlightEntity> Search(AirFlightCriteria criteria);
    }
}
