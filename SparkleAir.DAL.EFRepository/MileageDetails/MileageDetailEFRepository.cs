using SparkleAir.IDAL.IRepository.MileageDetails;
using SparkleAir.Infa.Entity.MileageDetails;
using SparkleAir.Infa.EFModel.Models;
using Microsoft.EntityFrameworkCore;
using Google.Apis.Drive.v3.Data;

namespace SparkleAir.DAL.EFRepository.MileageDetails
{
    public class MileageDetailEFRepository : IMileageDetailRepository
    {
        private AppDbContext db;

        public MileageDetailEFRepository(AppDbContext _db)
        {
            db = _db;
        }
       

        public int Create(MileageDetailEntity model)
        {
            MileageDetail mileagedetail = new MileageDetail
            {
                //Id = model.Id,
                MermberIsd = model.MermberIsd,
                TotalMile = model.TotalMile,
                OriginalMile = model.OriginalMile,
                ChangeMile = model.ChangeMile,
                FinalMile = model.FinalMile,
                MileValidity = model.MileValidity,
                MileReason = model.MileReason,
                OrderNumber = model.OrderNumber,
                ChangeTime = model.ChangeTime,
            };
            db.MileageDetails.Add(mileagedetail);
            db.SaveChanges();
            return mileagedetail.Id;
        }

        //新增里程折抵表
        public void MileApplyCreate(MileApplyEntity model)
        {
           MileApply mileapply = new MileApply
            {
              
               Change = model.Change,
               EventId = model.EventId,
               ChoseId = model.ChoseId,
               Final = model.Final,           
           };
            db.MileApplies.Add(mileapply);
            db.SaveChanges();
        }


        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List< MileageDetailEntity> Get(int memerbId)
        {
            var memberfinalmaile = db.MileageDetails.Where(p => p.MermberIsd == memerbId)
                                                    .OrderByDescending(p=>p.ChangeTime)  //修改時間近到遠
                                                    .Select(p => new MileageDetailEntity
                                                    {
                                                        Id = p.Id,
                                                        MermberIsd = p.MermberIsd,
                                                        TotalMile = p.TotalMile,
                                                        OriginalMile = p.OriginalMile,
                                                        ChangeMile = p.ChangeMile,
                                                        FinalMile = p.FinalMile,
                                                        MileValidity = p.MileValidity,
                                                        MileReason = p.MileReason,
                                                        OrderNumber = p.OrderNumber,
                                                        ChangeTime = p.ChangeTime,

                                                    }).ToList();
            return memberfinalmaile;
        }

        //取某會員的里程
        public List<MileageDetailEntity> Getdetail(int id)
        {
            var memberfinalmaile = db.MileageDetails.Where(p => p.MermberIsd == id )                                                
                                                    .Select(p => new MileageDetailEntity
                                                    {
                                                        Id = p.Id,
                                                        MermberIsd = p.MermberIsd,
                                                        TotalMile = p.TotalMile,
                                                        OriginalMile = p.OriginalMile,
                                                        ChangeMile = p.ChangeMile,
                                                        FinalMile = p.FinalMile,
                                                        MileValidity = p.MileValidity,
                                                        MileReason = p.MileReason,
                                                        OrderNumber = p.OrderNumber,
                                                        ChangeTime = p.ChangeTime,

                                                    }).ToList();
            return memberfinalmaile;
        }

        //取mileapply表中找某會員的折抵
        public List<MileApplyEntity> Getapplydetail(int memberId)
        {
            var getapplydetail = db.MileApplies .Include(p=>p.Chose)
                                                .Where(p => p.Chose.MermberIsd == memberId)
                                                .OrderByDescending(p => p.Id)
                                              // .Include(p => p.Event)
                                               .Select(p => new MileApplyEntity
                                                 {
                                                        Id = p.Id,
                                                        Change= p.Change,
                                                        EventId = p.EventId,
                                                        ChoseId = p.ChoseId,
                                                        MileReason=p.Event.MileReason, //關聯 里程Detail表中的 折抵原因
                                                        Final = null,

                                                 }).ToList();
            return getapplydetail;
        }


        public List<MileageDetailEntity> GetAll()
        {
            var Mileage = db.MileageDetails.AsNoTracking()
                          //.Include(p => p.Members)
                          .OrderByDescending(p => p.MermberIsd) //大到小
                          .Select(p => new MileageDetailEntity
                          {
                              Id = p.Id,
                              MermberIsd = p.MermberIsd,
                              TotalMile = p.TotalMile,
                              OriginalMile = p.OriginalMile,
                              ChangeMile = p.ChangeMile,
                              FinalMile = p.FinalMile,
                              MileValidity = p.MileValidity,
                              MileReason = p.MileReason,
                              OrderNumber = p.OrderNumber,
                              ChangeTime = p.ChangeTime,
                          })
                          .ToList();

            return Mileage;
        }

        public void Update(MileageDetailEntity model)
        {
            var get = db.MileageDetails.Find(model.Id);
            if (get != null)
            {
                get.Id = model.Id;
                get.MermberIsd = model.MermberIsd;
                get.TotalMile = model.TotalMile;
                get.OriginalMile = model.OriginalMile;
                get.ChangeMile = model.ChangeMile;
                get.FinalMile = model.FinalMile;
                get.MileValidity = model.MileValidity;
                get.MileReason = model.MileReason;
                get.OrderNumber = model.OrderNumber;
                get.ChangeTime = model.ChangeTime;

            }
            else
            {
                throw new Exception("找不到要修改的資料");
            }

            db.SaveChanges();
        }


        //取某會員的最後一筆最終里程
        public int Getfinalmile(int memberId)
        {
            var memberfinalmaile = db.MileageDetails.Where(p => p.MermberIsd == memberId)
                                      .OrderByDescending(p=>p.ChangeTime)
                                      .FirstOrDefault();

            if(memberfinalmaile == null)
            {
                return 0;
            }

            return memberfinalmaile.FinalMile;     

        }

       
    }
}
