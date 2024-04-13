using SparkleAir.IDAL.IRepository.Airtype_Owns;
using SparkleAir.Infa.Dto.Airtype_Owns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.BLL.Service.Airtype_Owns
{
    public class CabinRuleService
    {
        private readonly ICabinRuleRepository _repo;
        public CabinRuleService(ICabinRuleRepository repo)
        {
            _repo = repo;
        }

        public List<CabinRuleDto> GetAll()
        {
            var list = _repo.GetAll()//entity格式
                .Select(p => new CabinRuleDto//轉成dto格式
                {

                    Id = p.Id,
                    AirCabinId = p.AirCabinId,
                    CabinCode = p.CabinCode,
                    Priority = p.Priority,
                    ShipmentWeight = p.ShipmentWeight,
                    ShipmentCount = p.ShipmentCount,
                    CarryBagCount = p.CarryBagCount,
                    CarryBagWeight = p.CarryBagWeight,
                    PreSelectedSeat = p.PreSelectedSeat,
                    AccumulatedMile = p.AccumulatedMile,
                    MileUpgrade = p.MileUpgrade,
                    TicketVaildity = p.TicketVaildity,
                    RefundFee = p.RefundFee,
                    NoShowFee = p.NoShowFee,
                    FreeWifi = p.FreeWifi
                })
                .ToList();
            return list;

        }

    }
}
