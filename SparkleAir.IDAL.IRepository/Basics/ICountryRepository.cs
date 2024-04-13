using SparkleAir.Infa.Entity.Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.IDAL.IRepository.Basics
{
    public interface ICountryRepository
    {
        List<CountryEntity> Search();

        void Create(CountryEntity entity);

        void Update(CountryEntity entity);

        void Delete(int id);

        CountryEntity Get(int id);
    }
}
