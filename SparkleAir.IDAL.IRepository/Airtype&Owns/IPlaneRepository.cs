using SparkleAir.Infa.Entity.Airtype_Owns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.IDAL.IRepository.Airtype_Owns
{
  public interface IPlaneRepository
    {

        void Create(PlaneEntity entity);
        void Update(PlaneEntity entity);
        void Delete(int id);

        List<PlaneEntity> GetAll();

        PlaneEntity Get(int id);

        bool Exists(string flightModel);
    }
}
