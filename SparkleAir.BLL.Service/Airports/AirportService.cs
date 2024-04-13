using SparkleAir.IDAL.IRepository.Airport;
using SparkleAir.Infa.Dto.Airport;
using SparkleAir.Infa.Entity.Airports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;


namespace SparkleAir.BLL.Service.Airports
{
    public class AirportService
    {
        private readonly IAirportRepository _repo;

        public AirportService(IAirportRepository repo) //接收IAirportRepository,方便可以使用Dapper或是EF
        {
            _repo = repo;
        }


        //取全部
        public List<AirportDto> GetAll()
        {
            List<AirportEntity> entity = _repo.GetAll();

            List<AirportDto> dto = entity.Select(p => new AirportDto
            {
                Id = p.Id,
                Lata= p.Lata,
                Gps=p.Gps,
                Country=p.Country,  
                City= p.City,
                AirPortName = p.AirPortName,
                TimeArea = p.TimeArea,
                Zone= p.Zone,
                CityIntroduction = p.CityIntroduction,
                Img = p.Img,
                Continent = p.Continent,

            }).ToList();
            return dto;
        }


        public AirportDto Getid(int id)
        {
            AirportEntity entity = _repo.Getid(id);

            if (entity == null)
            {
                return default(AirportDto);
            }

            AirportDto  en = new AirportDto()
            {
                Id = entity.Id,
                Lata = entity.Lata,
                Gps = entity.Gps,
                Country = entity.Country,
                City = entity.City,
                AirPortName = entity.AirPortName,
                TimeArea = entity.TimeArea,
                Zone = entity.Zone,
                CityIntroduction = entity.CityIntroduction,
                Img = entity.Img,
                Continent = entity.Continent
            };

            return en;
        }


        //刪除
        public void Delete(int id)
        {
            _repo.Delete(id);
        }

        //新增
        public int Create(AirportDto dto)
        {
            AirportEntity entity = new AirportEntity
            {
                Id = dto.Id,
                Lata= dto.Lata,
                Gps = dto.Gps,
                Country = dto.Country,
                City = dto.City,
                AirPortName = dto.AirPortName,
                TimeArea = dto.TimeArea,
                Zone = dto.Zone,
                CityIntroduction = dto.CityIntroduction,
                Img = dto.Img,
                Continent = dto.Continent,

            };
            _repo.Create(entity);  //沒呼叫AirportEFRepository是因為AirportEFRepository有實作interface,所以呼叫_repo也就好
            return entity.Id;
        }


        //取得一筆
        public AirportDto Get(int id)
        {
            AirportEntity entity = _repo.Getid(id);

            AirportDto dto = new AirportDto
            {
                Id = entity.Id,
                Lata = entity.Lata,
                Gps = entity.Gps,
                Country = entity.Country,
                City = entity.City,
                AirPortName = entity.AirPortName,
                TimeArea = entity.TimeArea,
                Zone = entity.Zone,
                CityIntroduction = entity.CityIntroduction,
                Img = entity.Img,
                Continent = entity.Continent,
            };

            return dto;
        }


        //更新
        public void Update(AirportDto dto)
        {
            AirportEntity entity = new AirportEntity
            {
                Id = dto.Id,
                Lata = dto.Lata,
                Gps = dto.Gps,
                Country = dto.Country,
                City = dto.City,
                AirPortName = dto.AirPortName,
                TimeArea = dto.TimeArea,
                Zone = dto.Zone,
                CityIntroduction = dto.CityIntroduction,
                Img = dto.Img,
                Continent = dto.Continent,
            };


            _repo.Update(entity);
        }









    }
}
