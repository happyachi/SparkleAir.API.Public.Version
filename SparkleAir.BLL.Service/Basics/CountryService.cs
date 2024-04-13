using SparkleAir.IDAL.IRepository.Basics;
using SparkleAir.Infa.Dto.Basics;
using SparkleAir.Infa.Entity.Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.BLL.Service.Basics
{
    public class CountryService
    {
        private readonly ICountryRepository _repo;
        public CountryService(ICountryRepository repo)
        {
            _repo = repo;
        }

        public List<CountryDto> Search()
        {
            var entities = _repo.Search();
            var dtos = entities.Select(c => new CountryDto
            {
                Id = c.Id,
                ChineseName = c.ChineseName,
                EnglishName = c.EnglishName,
            }).ToList();

            return dtos;
        }

        public void Create(CountryDto dto)
        {
            var entity = new CountryEntity
            {
                ChineseName = dto.ChineseName,
                EnglishName = dto.EnglishName,
            };

            _repo.Create(entity);
        }

        public void Update(CountryDto dto)
        {
            var entity = new CountryEntity
            {
                Id = dto.Id,
                ChineseName = dto.ChineseName,
                EnglishName = dto.EnglishName
            };
            _repo.Update(entity);
        }

        public void Delete(CountryDto dto)
        {
            throw new NotImplementedException();
        }

        public CountryDto Get(int id)
        {
            var entity = _repo.Get(id);
            var dto = new CountryDto
            {
                Id = entity.Id,
                ChineseName = entity.ChineseName,
                EnglishName = entity.EnglishName
            };

            return dto;
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }
}
