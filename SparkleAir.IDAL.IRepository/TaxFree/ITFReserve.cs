using SparkleAir.Infa.Entity.TaxFree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.IDAL.IRepository.TaxFree
{
    public interface ITFReserve
    {
        List<TFReservesEntity> Get();

        List<TFReservesEntity> Getid(int id);
        List<TFReservesEntity> Search(TFReservesEntity entity);

        void Update(TFReservesEntity entity);

        int Create(TFReservesEntity entity);

        void Delete(int id);

       
    }
}
