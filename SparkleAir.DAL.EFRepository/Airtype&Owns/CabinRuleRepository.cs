using Microsoft.EntityFrameworkCore;
using SparkleAir.IDAL.IRepository.Airtype_Owns;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.Airtype_Owns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.DAL.EFRepository.Airtype_Owns
{
   public class CabinRuleRepository:ICabinRuleRepository
    {
        private readonly AppDbContext db;
        public CabinRuleRepository(AppDbContext context)
        {
            db = context;
        }
        public List<CabinRuleEntity> GetAll()
        {

            var rules = db.AirCabinRules
                .AsNoTracking()
                .Select(p => new CabinRuleEntity
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

            return rules;
        }
    }
}
