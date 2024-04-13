using SparkleAir.Infa.Entity.Luggage;
using SparkleAir.Infa.Entity.MileageDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.IDAL.IRepository.MileageDetails
{
    public interface IMileageDetailRepository
    {
        //取全部
        List<MileageDetailEntity> GetAll();

        //新增
        int Create(MileageDetailEntity model);

        //取得一筆
        List< MileageDetailEntity> Get(int memberId);

        //更新
        void Update(MileageDetailEntity model);

        //刪除
        void Delete(int id);

        int Getfinalmile(int memberid);

        //新增里程折抵表
        void MileApplyCreate(MileApplyEntity model);

        //找MileApply中某會員資料
        List< MileApplyEntity>Getapplydetail(int id);
    }
}
