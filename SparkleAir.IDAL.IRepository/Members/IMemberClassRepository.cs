using SparkleAir.Infa.Entity.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.IDAL.IRepository.Members
{
    public interface IMemberClassRepository
    {
        List<MemberClassEntity> Search();

        void Create(MemberClassEntity entity);

        MemberClassEntity Get(int id);

        void Update(MemberClassEntity entity);

        void Delete(int id);
    }
}
