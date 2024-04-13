using SparkleAir.Infa.Entity.Airtype_Owns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.IDAL.IRepository.Airtype_Owns
{
    public interface ICabinRuleRepository
    {
        List<CabinRuleEntity> GetAll();
    }
}
