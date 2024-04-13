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
    public class OwnRepository : IOwnRepository
    {
        private readonly AppDbContext db;
        public OwnRepository(AppDbContext context)
        {
            db = context;
        }
        public void Create(OwnEntity model)
        {
           
            AirOwn entity = new AirOwn
            {

                AirTypeId = model.AirTypeId,
                RegistrationNum = model.RegistrationNum,
                Status = model.Status,
                ManufactureYear = model.ManufactureYear,

            };
            db.AirOwns.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var plane = db.AirOwns.Find(id);

            db.AirOwns.Remove(plane);
            db.SaveChanges();

        }

        public bool Exists(string registrationnum)
        {
            var existingRegisNum = db.AirOwns.FirstOrDefault(p => p.RegistrationNum == registrationnum);
            if (existingRegisNum != null)
            {
                // 如果存在，可以選擇拋出異常或回傳錯誤訊息
                throw new InvalidOperationException("相同的註冊編號已存在");
            }

            return false;
        }

        public OwnEntity Get(int id)
        {
            var result = GetAll().SingleOrDefault(x => x.Id == id);
            return result;
        }

        public List<OwnEntity> GetAll()
        //asnotracking讀取時不要追蹤異動,效能較好,因getall不會異動
        //include是讀取時直接包含airtype,如果有很多筆資料不用每次讀取時都要重新join

        {

            var seats = db.AirOwns.AsNoTracking().Include(p => p.AirType)
                .Select(p => new OwnEntity
                {
                    Id = p.Id,
                    AirTypeId = p.AirTypeId,
                    FlightModel = p.AirType.FlightModel,
                    RegistrationNum = p.RegistrationNum,
                    Status = p.Status,
                    ManufactureYear = p.ManufactureYear,
                })
                .ToList();

            return seats;
        }

        public void Update(OwnEntity entity)
        {
            var x = db.AirOwns.Find(entity.Id);

            x.AirTypeId = entity.AirTypeId;
            x.RegistrationNum = entity.RegistrationNum;
            x.Status = entity.Status;
            x.ManufactureYear = entity.ManufactureYear;

            db.SaveChanges();
        }
    }
}

