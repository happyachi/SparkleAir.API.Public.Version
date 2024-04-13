using SparkleAir.IDAL.IRepository.Airtype_Owns;
using SparkleAir.Infa.Dto.Airtype_Owns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.BLL.Service.Airtype_Owns
{
    public class AirSeatService
    {

        private readonly IAirSeatRepository _repo;
        public AirSeatService(IAirSeatRepository repo)
        {
            _repo = repo;
        }

        public List<AirSeatDto> GetAll()
        {
            var list = _repo.GetAll()//entity格式
                .Select(p => new AirSeatDto//轉成dto格式
                {

                    Id = p.Id,
                    AirTypeId = p.AirTypeId,
                    AirCabinId = p.AirCabinId,
                    SeatNum = p.SeatNum
                })
                .ToList();
            return list;

        }
        public List<AirSeatDto> GetByFlightModel(string flightModel)
        {
            var list = _repo.GetByFlightModel(flightModel)//entity格式
                .Select(p => new AirSeatDto//轉成dto格式
                {

                    Id = p.Id,
                    AirTypeId = p.AirTypeId,
                    AirCabinId = p.AirCabinId,
                    SeatNum = p.SeatNum
                })
                .ToList();
            return list;

        }


    }
}

