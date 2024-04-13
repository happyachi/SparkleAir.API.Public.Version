using SparkleAir.Infa.Entity.Luggage;
using SparkleAir.Infa.Entity.LuggageOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.IDAL.IRepository.LuggageOrders
{
    public interface ILuggageOrderRepository
    {
        //取全部
        List<LuggageOrderEntity> GetAll();

        //新增
        int Create(LuggageOrderEntity model);

        //取得一筆
        LuggageOrderEntity Get(int id);

        //更新
        void Update(LuggageOrderEntity model);

        //刪除
        void Delete(int id);

        //由ticketinvocingdetailId 取得 Luggageprice
        LuggagePriceEntity GetLuggage(int ticketinvocingdetailId);

        //取得Luggage全部
        List<LuggageOrderEntity> GetAllLuggage();
    }
}
