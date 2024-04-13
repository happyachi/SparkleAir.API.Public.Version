using SparkleAir.Infa.Entity.Airports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.IDAL.IRepository.Airport
{
    public interface IAirportRepository
    {
        //取全部
        List<AirportEntity> GetAll();

        //新增
        int Create(AirportEntity model);

        //取得一筆
        AirportEntity Getid(int id);

        //更新
        void Update(AirportEntity model);

        //刪除
        void Delete(int id);
    }
}
