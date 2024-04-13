using SparkleAir.Infa.Entity.MileageDetails;
using SparkleAir.Infa.Entity.MileOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.IDAL.IRepository.MileOrder
{
    public interface IMileageOrderRepository
    {
        
            //取全部
            List<MileOrderEntity> GetAll();

            //新增
            void Create(MileOrderEntity model);

            //取得一筆
            List<MileOrderEntity> Get(int id);

            //更新
            void Update(MileOrderEntity model);

            //刪除
            void Delete(int id);

            int Getfinalmile(int memberid);
        
    }
}
