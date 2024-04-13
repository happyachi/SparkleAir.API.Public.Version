using SparkleAir.Infa.Criteria.AirFlights;
using SparkleAir.Infa.Entity.AirFlightsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.IDAL.IRepository.AirFlights
{
    public interface IAirFlightManagementRepository
    {
        List<AirFlightManagementEntity> GetAll();
        int Create(AirFlightManagementEntity entity);
        void Update(AirFlightManagementEntity entity);
        void Delete(int id);
        AirFlightManagementEntity GetById(int id);
        List<AirFlightManagementEntity> Search(AirFlightManagementSearchCriteria entity);
    }
}
