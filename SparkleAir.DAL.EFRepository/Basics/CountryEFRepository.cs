using Microsoft.EntityFrameworkCore;
using SparkleAir.IDAL.IRepository.Basics;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.DAL.EFRepository.Basics
{
    public class CountryEFRepository : ICountryRepository
    {
        private AppDbContext _db;

        public CountryEFRepository(AppDbContext db)
        {
            _db = db;
        }
        public void Create(CountryEntity entity)
        {
            var country = new Country
            {
                ChineseName = entity.ChineseName,
                EnglishName = entity.EnglishName
            };

            _db.Countries.Add(country);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var country = _db.Countries.Find(id);
            _db.Countries.Remove(country);
            _db.SaveChanges();
        }

        public CountryEntity Get(int id)
        {
            var country = _db.Countries.Find(id);
            var entity = new CountryEntity
            {
                Id = country.Id,
                ChineseName = country.ChineseName,
                EnglishName = country.EnglishName
            };
            return entity;
        }

        public List<CountryEntity> Search()
        {
            var entities = _db.Countries
                .AsNoTracking()
                .Select(c => new CountryEntity
                {
                    Id = c.Id,
                    ChineseName = c.ChineseName,
                    EnglishName = c.EnglishName,
                })
                .ToList();
            return entities;
        }

        public void Update(CountryEntity entity)
        {
            var country = _db.Countries.Find(entity.Id);
            country.ChineseName = entity.ChineseName;
            country.EnglishName = entity.EnglishName;
            _db.SaveChanges();
        }
    }
}
