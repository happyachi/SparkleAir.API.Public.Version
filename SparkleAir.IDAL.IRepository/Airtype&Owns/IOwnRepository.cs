using SparkleAir.Infa.Entity.Airtype_Owns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.IDAL.IRepository.Airtype_Owns
{
    public interface IOwnRepository
    {
        void Create(OwnEntity entity);
        void Update(OwnEntity entity);
        void Delete(int id);

        List<OwnEntity> GetAll();

        OwnEntity Get(int id);
        bool Exists(string registrationnum);
    }
}
