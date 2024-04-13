using SparkleAir.Infa.Criteria.AirFlights;
using SparkleAir.Infa.Entity.AirFlightsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.IDAL.IRepository.AirFlights
{
    public interface IAirTakeOffRepository
    {
        void CreateAirTakeOff(AirTakeOffEntity entity);

        void TakeOffUpdate(AirTakeOffEntity entity);

        void ArrivalUpdate(AirTakeOffEntity entity);

        List<AirTakeOffEntity> GetAirTakeOffList(TakeOffCriteria criteria);
    }
}
