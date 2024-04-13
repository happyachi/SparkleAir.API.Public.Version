using Microsoft.EntityFrameworkCore;
using SparkleAir.IDAL.IRepository.Members;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.DAL.EFRepository.Members
{
    public class MemberClassEFRepository : IMemberClassRepository
    {
        private AppDbContext _db;
        public MemberClassEFRepository(AppDbContext db)
        {
            _db = db;
        }

        public void Create(MemberClassEntity entity)
        {
            var memberClass = new MemberClass
            {
                Name = entity.Name,
                ClassOrder = entity.ClassOrder,
                MileageStart = entity.MileageStart,
                MileageEnd = entity.MileageEnd
            };

            _db.MemberClasses.Add(memberClass);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var memberClass = _db.MemberClasses.Find(id);
            _db.MemberClasses.Remove(memberClass);
            _db.SaveChanges();
        }

        public MemberClassEntity Get(int id)
        {
            var memberClass = _db.MemberClasses.Find(id);

            var entity = new MemberClassEntity
            {
                Id = memberClass.Id,
                Name = memberClass.Name,
                ClassOrder = memberClass.ClassOrder,
                MileageEnd = memberClass.MileageEnd,
                MileageStart = memberClass.MileageStart,
            };
            return entity;
        }

        public List<MemberClassEntity> Search()
        {
            var eneities = _db.MemberClasses
                .AsNoTracking()
                .Select(m => new MemberClassEntity
                {
                    Id = m.Id,
                    Name = m.Name,
                    ClassOrder = m.ClassOrder,
                    MileageStart = m.MileageStart,
                    MileageEnd = m.MileageEnd
                })
                .ToList();
            return eneities;
        }

        public void Update(MemberClassEntity entity)
        {
            var memberClass = _db.MemberClasses.Find(entity.Id);

            if (entity.Name != null) memberClass.Name = entity.Name;
            if (entity.ClassOrder != 0) memberClass.ClassOrder = entity.ClassOrder;
            if (entity.MileageStart != 0) memberClass.MileageStart = entity.MileageStart;
            if (entity.MileageEnd != 0) memberClass.MileageEnd = entity.MileageEnd;

            _db.SaveChanges();
        }
    }
}
