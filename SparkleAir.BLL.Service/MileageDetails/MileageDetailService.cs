using Google.Apis.Drive.v3.Data;
using Microsoft.Extensions.Logging;
using SparkleAir.DAL.EFRepository.Members;
using SparkleAir.DAL.EFRepository.MileageDetails;
using SparkleAir.IDAL.IRepository.Members;
using SparkleAir.IDAL.IRepository.MileageDetails;
using SparkleAir.IDAL.IRepository.MileOrder;
using SparkleAir.Infa.Criteria.Members;
using SparkleAir.Infa.Dto.Airport;
using SparkleAir.Infa.Dto.MileageDetails;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.Airports;
using SparkleAir.Infa.Entity.Members;
using SparkleAir.Infa.Entity.MileageDetails;

namespace SparkleAir.BLL.Service.MileageDetails
{
    public class MileageDetailService
    {
        //private readonly IMileageDetailRepository _repo;

        //private readonly IMemberRepository _mm;


        MemberEFRepository _mm;
        MileageDetailEFRepository _repo;



        public MileageDetailService(AppDbContext db) //接收IAirportRepository,方便可以使用Dapper或是EF
        {
            //_repo = repo;
            _mm = new MemberEFRepository(db);
            _repo = new MileageDetailEFRepository(db);
        }




        //取全部
        public List<MileageDetailDto> GetAll()
        {
            List<MileageDetailEntity> entity = _repo.GetAll();

            List<MileageDetailDto> dto = entity.Select(p => new MileageDetailDto
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
            return dto;
        }





        public int Create(MileageDetailDto dto)
        {
            //取得某會員的最終final里程
            var getsomeonefinal = Getdetail(dto.MermberIsd).OrderByDescending(p => p.ChangeTime).FirstOrDefault();


            //// 先去看資料庫已經到幾號了
            var Mileorder = GetAll().Select(x => x.OrderNumber)
                                    .Where(x => x.StartsWith("DM"))
                                    .OrderByDescending(x => x)
                                    .FirstOrDefault();

            if (Mileorder == null)
            {
                // 產生流水號
                Mileorder = "DM00001";
            }
            else
            {
                // 生出最新的流水號
                Mileorder = Mileorder.Substring(2, 5);
                Mileorder = (Convert.ToInt32(Mileorder) + 1).ToString().PadLeft(5, '0');
                Mileorder = "DM" + Mileorder;
            }




            //判斷某位會員的值 ,是否夠減
            if (getsomeonefinal.FinalMile + dto.ChangeMile < 0)
            {
                throw new Exception("會員里程不足");
            }
            //先取得會員
            var criteria = new MemberGetCriteria
            {
                Id = dto.MermberIsd //這裡是會員編號
            };
            var memb = _mm.Get(criteria);

            var totalmile = dto.ChangeMile >= 0 ? memb.TotalMileage + dto.ChangeMile : memb.TotalMileage;//判斷是否把 更改的里程 加上 總里程

            var sb = _repo.Getfinalmile(dto.MermberIsd); //取得某會員的最後一筆里程紀錄

            //todo orderNumber 要寫自動帶入參數



            MileageDetailEntity entity = new MileageDetailEntity
            {
                //Id = dto.Id,
                MermberIsd = dto.MermberIsd,
                TotalMile = totalmile,
                OriginalMile = sb,
                ChangeMile = dto.ChangeMile,
                FinalMile = sb + dto.ChangeMile,
                MileValidity = DateTime.Now.AddYears(1),
                MileReason = dto.MileReason,
                OrderNumber = Mileorder,
                ChangeTime = DateTime.Now,

            };

            var detailid = _repo.Create(entity);   //取得新增Detail後的id



            //如果添加的里程是負數，則要產生一筆APPLY表的資料
            if (entity.ChangeMile < 0)
            {

                MileApplyCreate(detailid, entity);
            }

            MemberEntity member = new MemberEntity
            {
                Id = dto.MermberIsd,
                TotalMileage = totalmile,
            };
            _mm.Update(member);
            return entity.Id;


        }

        public void MileApplyCreate(int eventid, MileageDetailEntity mdentity)
        {

            //取得某會員的所有正向值,時間由遠到近,且時間是一年內
            var getsomeonetime = Getdetail(mdentity.MermberIsd).Where(p => p.ChangeMile > 0 && p.ChangeTime > DateTime.Now.AddYears(-1))
                                                    .OrderBy(p => p.ChangeTime); //時間由遠到近

            //有多少里程要減,先換成正數
            var reducedmile = mdentity.ChangeMile * (-1);

            //儲存該筆要減的里程,已折多少
            var reducedmile2 = mdentity.ChangeMile * (-1);

            //如果夠減
            //用迴圈  取出每一筆的id

            foreach (var item in getsomeonetime)
            {
                if (reducedmile != 0)
                {
                    //某會員的每筆里程Id紀錄
                    var everymiledetailId = item.Id;
                    //去APPLY表找，ChoseId 有沒有等於 id
                    var someoneapplydetail = _repo.Getapplydetail(everymiledetailId);

                    if (someoneapplydetail.Count != 0 && reducedmile != 0) //判斷某會員的某筆里程有沒有被折抵過
                    {
                        //有的話，去看目前已折抵多少
                        //宣告變數以存儲已折抵的里程數
                        var totalAppliedMiles = 0;

                        // 獲取每個申請細節的折抵里程數並加總起來
                        foreach (var applyDetail in someoneapplydetail)
                        {
                            totalAppliedMiles += applyDetail.Change;
                        }

                        if (item.ChangeMile - totalAppliedMiles > 0) //判斷某會員的逐項某筆里程 扣掉 某會員已折抵里程後 是否夠折抵, 夠的話
                        {
                            var remainmile = item.ChangeMile - totalAppliedMiles; //判斷某會員的 某筆里程 扣掉 已折抵的里程後, 剩多少里程可折抵

                            if (reducedmile - remainmile > 0) //要折抵的里程數 大於 剩餘可折抵的里程
                            {

                                MileApplyEntity applyentity = new MileApplyEntity
                                {
                                    Change = remainmile, //?????????????
                                    EventId = eventid,
                                    ChoseId = everymiledetailId,
                                };
                                _repo.MileApplyCreate(applyentity);

                                reducedmile -= remainmile; //扣掉剩餘可折抵的里程數後,還有多少要里程要折抵
                            }
                            else
                            {

                                MileApplyEntity applyentity = new MileApplyEntity
                                {
                                    Change = reducedmile,
                                    EventId = eventid,
                                    ChoseId = everymiledetailId,

                                };
                                _repo.MileApplyCreate(applyentity);
                                reducedmile = 0;

                                //儲存MileDetail表

                            }
                        }
                    }
                    else  //沒有折抵過的紀錄
                    {
                        if (item.ChangeMile > 0) //判斷某會員的逐項某筆里程 扣掉 某會員已折抵里程後 是否夠折抵, 夠的話
                        {
                            var remainmile = item.ChangeMile; //判斷某會員的 某筆里程 扣掉 已折抵的里程後, 剩多少里程可折抵

                            if (reducedmile - remainmile > 0) //要折抵的里程數 大於 剩餘可折抵的里程
                            {

                                //reducedmile2 -= reducedmile; //已折抵的里程數
                                MileApplyEntity applyentity = new MileApplyEntity
                                {
                                    Change = remainmile, //把以折抵掉的里程數存起來
                                    EventId = eventid, //里程是負數折抵 的那筆id
                                    ChoseId = everymiledetailId, //逐項里程中 被折抵的那筆 id
                                };
                                _repo.MileApplyCreate(applyentity);
                                reducedmile -= remainmile; //扣掉剩餘可折抵的里程數後,還有多少要里程要折抵
                                                           //儲存MileDetail表                          
                            }
                            else
                            {
                                MileApplyEntity applyentity = new MileApplyEntity
                                {
                                    Change = reducedmile,
                                    EventId = eventid,
                                    ChoseId = everymiledetailId,

                                };
                                _repo.MileApplyCreate(applyentity);
                                reducedmile = 0;
                            }
                        }
                    }
                }
            }
        }


        //里程過期的新增
        public void ExpiredCreate()
        {
            //取得某會員的所有正向值且已過期的,時間由遠到近
            var getsomeonetime = GetAll().Where(p => p.ChangeMile > 0 && p.MileValidity < DateTime.Now)
                                                               .OrderBy(p => p.ChangeTime); //時間由遠到近


            foreach (var item in getsomeonetime) //每筆里程數是正值且過期的
            {
                //取得某會員的最終final里程
                var getsomeonefinal = Getdetail(item.MermberIsd).OrderByDescending(p => p.ChangeTime).FirstOrDefault();

                var everymiledetailId = item.Id;
                //去APPLY表找，ChoseId 有沒有等於 id
                var someoneapplydetail = _repo.Getapplydetail(everymiledetailId);

                if (someoneapplydetail.Count != 0) //判斷某會員的某筆里程有沒有被折抵過
                {
                    //有的話，去看目前已折抵多少
                    //宣告變數以存儲已折抵的里程數
                    var totalAppliedMiles = 0;

                    // 獲取每個申請細節的折抵里程數並加總起來
                    foreach (var applyDetail in someoneapplydetail)
                    {
                        totalAppliedMiles += applyDetail.Change;
                    }

                    if (item.ChangeMile - totalAppliedMiles > 0) //判斷某會員的逐項某筆里程 扣掉 某會員已折抵里程後 是否夠折抵, 夠的話
                    {
                        var remainmile = item.ChangeMile - totalAppliedMiles;

                        MileageDetailEntity entity = new MileageDetailEntity
                        {
                            MermberIsd = item.MermberIsd,
                            TotalMile = getsomeonefinal.TotalMile,
                            OriginalMile = getsomeonefinal.FinalMile,
                            ChangeMile = remainmile * (-1),
                            FinalMile = getsomeonefinal.FinalMile + remainmile * (-1),
                            MileValidity = item.MileValidity,
                            MileReason = "里程過期",
                            OrderNumber = "過期編號",
                            ChangeTime = DateTime.Now,
                        };

                        var miledetailcreateId = _repo.Create(entity);

                        MileApplyEntity applyentity = new MileApplyEntity
                        {
                            Change = remainmile,
                            EventId = miledetailcreateId,
                            ChoseId = everymiledetailId,
                        };
                        _repo.MileApplyCreate(applyentity);
                    }
                    else  //判斷某會員某筆里程數已被折抵完
                    {


                    }
                }
                else
                {
                    MileageDetailEntity entity = new MileageDetailEntity
                    {
                        MermberIsd = item.MermberIsd,
                        TotalMile = getsomeonefinal.TotalMile,
                        OriginalMile = getsomeonefinal.FinalMile,
                        ChangeMile = item.ChangeMile * (-1),
                        FinalMile = getsomeonefinal.FinalMile + item.ChangeMile * (-1),
                        MileValidity = item.MileValidity,
                        MileReason = "里程過期",
                        OrderNumber = "過期編號",
                        ChangeTime = DateTime.Now,
                    };

                    var miledetailcreateId = _repo.Create(entity); //每筆事件的id

                    MileApplyEntity applyentity = new MileApplyEntity
                    {
                        Change = item.ChangeMile,
                        EventId = miledetailcreateId,
                        ChoseId = everymiledetailId,
                    };
                    _repo.MileApplyCreate(applyentity);
                }
            }
        }





        public void Update(MileageDetailDto dto)
        {
            MileageDetailEntity entity = new MileageDetailEntity
            {
                Id = dto.Id,
                MermberIsd = dto.MermberIsd,
                TotalMile = dto.TotalMile,
                OriginalMile = dto.OriginalMile,
                ChangeMile = dto.ChangeMile,
                FinalMile = dto.FinalMile,
                MileValidity = dto.MileValidity,
                MileReason = dto.MileReason,
                OrderNumber = dto.OrderNumber,
                ChangeTime = dto.ChangeTime,
            };


            _repo.Update(entity);
        }



        //取得一筆
        public List<MileageDetailDto> Get(int id)
        {
            List<MileageDetailEntity> entity = _repo.Get(id);

            List<MileageDetailDto> dto = entity.Select(p => new MileageDetailDto
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

            return dto;
        }

        //取得一筆
        public List<MileageDetailDto> Getdetail(int memberId)
        {
            List<MileageDetailEntity> entity = _repo.Get(memberId);  //取得某會員的所有里程紀錄,用時間近到遠排序

            List<MileageDetailDto> dto = entity.Select(p => new MileageDetailDto
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

            return dto;
        }


        //取得applyMile 的某一筆
        public List<MileApplyDto> Getapplydetail(int memberId)
        {
            List<MileApplyEntity> entity = _repo.Getapplydetail(memberId);

            List<MileApplyDto> dto = entity.Select(p => new MileApplyDto
            {
                Id = p.Id,
                Change = p.Change,
                EventId = p.EventId,
                ChoseId = p.ChoseId,
                MileReason = p.MileReason,

            }).ToList();

            return dto;
        }

        //取得某位會員的最新final miledetail
        public int Getfinalmile(int memberId)
        {
            return _repo.Getfinalmile(memberId);
        }

    }
}
