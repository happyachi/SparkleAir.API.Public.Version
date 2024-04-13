using SparkleAir.DAL.EFRepository.Airtype_Owns;
using SparkleAir.IDAL.IRepository.Airtype_Owns;
using SparkleAir.Infa.Dto.Airtype_Owns;
using SparkleAir.Infa.Entity.Airtype_Owns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.BLL.Service.Airtype_Owns
{
    public class OwnService
    {
        private readonly IOwnRepository _repo;
        public OwnService(IOwnRepository repo)
        {
            _repo = repo;
        }
        public List<OwnDto> GetAll()
        {
            var list = _repo.GetAll()//entity格式
                .Select(p => new OwnDto//轉成dto格式
                {
                    Id = p.Id,
                    AirTypeId = p.AirTypeId,
                    FlightModel = p.FlightModel,
                    RegistrationNum = p.RegistrationNum,
                    Status = p.Status,
                    ManufactureYear = p.ManufactureYear,

                })
                .ToList();
            return list;

        }

        public OwnDto Get(int? id)
        {
            var result = GetAll().SingleOrDefault(x => x.Id == id);
            return result;
        }

        public void Create(OwnDto dto)

        {
            if (_repo.Exists(dto.RegistrationNum))
            {
                // 如果存在，可以選擇拋出異常或回傳錯誤訊息
                throw new InvalidOperationException("相同的註冊編號已存在");

            }
            OwnEntity entity = new OwnEntity
            {
                Id = dto.Id,
                AirTypeId = dto.AirTypeId,
                RegistrationNum = dto.RegistrationNum,
                Status = dto.Status,
                ManufactureYear = dto.ManufactureYear,
            };

            _repo.Create(entity);
        }



        public void Update(OwnDto dto)
        {


            OwnEntity entity = new OwnEntity
            {
                Id = dto.Id,
                AirTypeId = dto.AirTypeId,
                RegistrationNum = dto.RegistrationNum,
                Status = dto.Status,
                ManufactureYear = dto.ManufactureYear,
            };

            _repo.Update(entity);
        }

        public void Delete(int id)
        {

            this._repo.Delete(id);
        }

      
    }
}

