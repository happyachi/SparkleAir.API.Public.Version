using SparkleAir.IDAL.IRepository.Airport;
using SparkleAir.IDAL.IRepository.Luggage;
using SparkleAir.Infa.Dto.Airport;
using SparkleAir.Infa.Dto.Luggage;
using SparkleAir.Infa.Entity.Airports;
using SparkleAir.Infa.Entity.Luggage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.BLL.Service.Luggage
{
    public class LuggageService
    {
        private readonly ILuggageRepository _repo;

        public LuggageService(ILuggageRepository repo) //接收IAirportRepository,方便可以使用Dapper或是EF
        {
            _repo = repo;
        }



        //取全部
        public List<LuggageDto> GetAll()
        {
            List<LuggageEntity> entity = _repo.GetAll();

            List<LuggageDto> dto = entity.Select(p => new LuggageDto
            {
                Id = p.Id,
                AirFlightManagementsId = p.AirFlightManagementsId,
                OriginalPrice = p.OriginalPrice,
                BookPrice = p.BookPrice,

            }).ToList();
            return dto;
        }

        public int Create(LuggageDto dto)
        {
            bool common = (dto.OriginalPrice== dto.BookPrice  );
            if (common)
            {
                throw new Exception("價格和預定價不可重複");
            }
            if(dto.OriginalPrice < dto.BookPrice)
            {
                throw new Exception("預定價不可比價格高");
            }


            LuggageEntity entity = new LuggageEntity
            {
                Id = dto.Id,
                AirFlightManagementsId = dto.AirFlightManagementsId,
                OriginalPrice = dto.OriginalPrice,
                BookPrice = dto.BookPrice,

            };
            _repo.Create(entity);  //沒呼叫AirportEFRepository是因為AirportEFRepository有實作interface,所以呼叫_repo也就好
            return entity.Id;
        }



        //刪除
        public void Delete(int id)
        {
            _repo.Delete(id);
        }



        //取得一筆
        public LuggageDto Get(int id)
        {
            LuggageEntity entity = _repo.Get(id);

            LuggageDto dto = new LuggageDto
            {
                Id = entity.Id,
                AirFlightManagementsId = entity.AirFlightManagementsId,
                OriginalPrice = entity.OriginalPrice,
                BookPrice = entity.BookPrice,
            };

            return dto;
        }


        //更新
        public void Update(LuggageDto dto)
        {
            bool common = (dto.OriginalPrice == dto.BookPrice);
            if (common)
            {
                throw new Exception("價格和預定價不可重複");
            }
            if (dto.OriginalPrice < dto.BookPrice)
            {
                throw new Exception("預定價不可比價格高");
            }

            LuggageEntity entity = new LuggageEntity
            {
                Id = dto.Id,
                AirFlightManagementsId = dto.AirFlightManagementsId,
                OriginalPrice = dto.OriginalPrice,
                BookPrice = dto.BookPrice,
            };


            _repo.Update(entity);
        }




    }
}
