using SparkleAir.Infa.Entity.TaxFree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.IDAL.IRepository.TaxFree
{
    public interface ITFReservelist
    {
        List<TFReservelistsEntity> Get();

        List<TFReservelistsEntity> Search(TFReservelistsEntity entity);

        void Update(TFReservelistsEntity entity);

        int Create(TFReservelistsEntity entity);

        void Delete(int id);

        List<TFReservelistsEntity> Getid(int id);
    }
}
