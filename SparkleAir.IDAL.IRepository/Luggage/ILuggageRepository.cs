using SparkleAir.Infa.Entity.Airports;
using SparkleAir.Infa.Entity.Luggage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.IDAL.IRepository.Luggage
{
    public interface ILuggageRepository
    {
        //取全部
        List<LuggageEntity> GetAll();

        //新增
        int Create(LuggageEntity model);

        //取得一筆
        LuggageEntity Get(int id);

        //更新
        void Update(LuggageEntity model);

        //刪除
        void Delete(int id);
    }
}
