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
    public class PlaneRepository : IPlaneRepository
    {
        private readonly AppDbContext db;

        public PlaneRepository(AppDbContext context) 
        {db=context; }

        public void Create(PlaneEntity model)
        {
            AirType entity = new AirType
            {

                FlightModel = model.FlightModel,
                TotalSeat = model.TotalSeat,
                MaxMile = model.MaxMile,
                MaxWeight = model.MaxWeight,
                ManufactureCompany = model.ManufactureCompany,
                Introduction = model.Introduction,
                Img = model.Img,
            };
            db.AirTypes.Add(entity);
            db.SaveChanges();

        }



        public PlaneEntity Get(int id)
        {
            var result = GetAll().SingleOrDefault(x => x.ID == id);
            return result;
        }

        public List<PlaneEntity> GetAll()
        {

            var seats = db.AirTypes
                .AsNoTracking()
                .Select(p => new PlaneEntity
                {
                    ID = p.Id,
                    FlightModel = p.FlightModel,
                    TotalSeat = p.TotalSeat,
                    MaxMile = p.MaxMile,
                    MaxWeight = p.MaxWeight,
                    ManufactureCompany = p.ManufactureCompany,
                    Introduction = p.Introduction,
                    Img = p.Img,
                })
                .ToList();

            return seats;

        }

        public void Update(PlaneEntity model)
        {
            var x = db.AirTypes.Find(model.ID);


            x.FlightModel = model.FlightModel;
            x.TotalSeat = model.TotalSeat;
            x.MaxMile = model.MaxMile;
            x.MaxWeight = model.MaxWeight;
            x.ManufactureCompany = model.ManufactureCompany;
            x.Introduction = model.Introduction;
            x.Img = model.Img;



            db.SaveChanges();
        }

        public void Delete(int id)
        {

            var plane = db.AirTypes.Find(id);

            db.AirTypes.Remove(plane);
            db.SaveChanges();

        }

        public bool Exists(string flightModel)
        {
            var existingPlane = db.AirTypes.FirstOrDefault(p => p.FlightModel == flightModel);
            if (existingPlane != null)
            {
                // 如果存在，可以選擇拋出異常或回傳錯誤訊息
                throw new InvalidOperationException("相同的飛機款式已存在");
            }

            return false; // 如果不存在相同的 FlightModel
        }
    }

}

