using SparkleAir.Infa.Entity.TaxFree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.IDAL.IRepository.TaxFree
{
    public interface ITFRepository
    {
        List<TFItemEntity> Get();
        List<TFItemEntity> Search(TFItemEntity entity);
        void Update(TFItemEntity entity);

        int Create(TFItemEntity entity);

        void Delete(int id);

        TFItemEntity Getid(int id);

        


    }
}
