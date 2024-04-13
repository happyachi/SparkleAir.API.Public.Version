using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using SparkleAir.IDAL.IRepository.Members;
using SparkleAir.Infa.Criteria.Members;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.Members;
using SparkleAir.Infa.Utility.Exts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SparkleAir.Infa.Utility.Helper.Members.MemberFunc;


namespace SparkleAir.DAL.EFRepository.Members
{
    public class MemberEFRepository : IMemberRepository
    {
        private AppDbContext _db;

        public MemberEFRepository(AppDbContext db)
        { 
            //DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            //.UseSqlServer(configuration.GetConnectionString("AppDbContext"))
            //.Options;
            //_db = new AppDbContext(dbContextOptions);
            _db = db;
        }

        public MemberEntity Get(MemberGetCriteria criteria)
        {
            var dbMember = _db.Members
                    .Include(m => m.Country)
                    .Include(m => m.MemberClass);

            var member = new Member();

            if (criteria.Id != null && criteria.Id != 0)
            {
                member = dbMember
                    .Where(m => m.Id == (int)criteria.Id)
                    .FirstOrDefault();
            }
            else if (criteria.Account != null)
            {
                member = dbMember
                    .Where(m => m.Account == criteria.Account)
                    .FirstOrDefault();
            }
            else if (criteria.GoogleId != null)
            {
                member = dbMember
                    .Where(m => m.GoogleId == criteria.GoogleId)
                    .FirstOrDefault();
            }
            else if (criteria.LineId != null)
            {
                member = dbMember
                    .Where(m => m.LineId == criteria.LineId)
                    .FirstOrDefault();
            }
            else if (criteria.Email != null)
            {
                member = dbMember
                    .Where(m => m.Email == criteria.Email)
                    .FirstOrDefault();
            }

            if (member != null)
            {
                MemberEntity memberEntity = member.MemberToEntity();
                return memberEntity;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<int> GetAllMembersIdByMemberClassId(int memberClassId)
        {
            var meberIdList = _db.Members
                .Where(m => m.MemberClassId == memberClassId)
                .Select(m => m.Id);

            return meberIdList;
        }

        public List<MemberEntity> Search(MemberSearchCriteria criteria)
        {
            var query = _db.Members
                .Include(m => m.MemberClass)
                .Include(m => m.Country)
                .AsNoTracking()
                .MemberSearchExt(criteria);


            var entity = query.Select(member => new MemberEntity
            {
                Id = member.Id,
                MemberClassId = member.MemberClassId,
                MemberClassName = member.MemberClass.Name,
                CountryId = member.CountryId,
                CountryName = member.Country.ChineseName,
                Account = member.Account,
                Password = member.Password,
                ChineseLastName = member.ChineseLastName,
                ChineseFirstName = member.ChineseFirstName,
                EnglishLastName = member.EnglishLastName,
                EnglishFirstName = member.EnglishFirstName,
                Gender = member.Gender,
                DateOfBirth = new DateTime(member.DateOfBirth.Year, member.DateOfBirth.Month, member.DateOfBirth.Day),
                Phone = member.Phone,
                Email = member.Email,
                TotalMileage = member.TotalMileage,
                PassportNumber = member.PassportNumber,
                PassportExpiryDate = new DateTime(member.PassportExpiryDate.Year, member.PassportExpiryDate.Month, member.PassportExpiryDate.Day),
                RegistrationTime = member.RegistrationTime,
                LastPasswordChangeTime = member.LastPasswordChangeTime,
                IsAllow = member.IsAllow,
                ConfirmCode = member.ConfirmCode
            });


            return entity.ToList();
        }

        public async void Update(MemberEntity entity)
        {
            if (entity == null) throw new Exception("沒有實例");

            var member = _db.Members
                .Where(m=>m.Id ==  entity.Id)
                .FirstOrDefault();

            if (entity.MemberClassId != 0) member.MemberClassId = entity.MemberClassId;
            if (entity.CountryId != 0) member.CountryId = entity.CountryId;
            if (entity.TotalMileage != 0) member.TotalMileage = entity.TotalMileage;
            if (entity.Password != null) member.Password = entity.Password;
            if (entity.ChineseLastName != null) member.ChineseLastName = entity.ChineseLastName;
            if (entity.ChineseFirstName != null) member.ChineseFirstName = entity.ChineseFirstName;
            if (entity.EnglishLastName != null) member.EnglishLastName = entity.EnglishLastName;
            if (entity.EnglishFirstName != null) member.EnglishFirstName = entity.EnglishFirstName;
            if (entity.Phone != null) member.Phone = entity.Phone;
            if (entity.Email != null) member.Email = entity.Email;
            if (entity.PassportNumber != null) member.PassportNumber = entity.PassportNumber;
            if (entity.ConfirmCode != null) member.ConfirmCode = entity.ConfirmCode;
            if (entity.DateOfBirth != new DateTime(1, 1, 1)) member.DateOfBirth = DateOnly.FromDateTime( entity.DateOfBirth);
            if (entity.PassportExpiryDate != new DateTime(1, 1, 1)) member.PassportExpiryDate = DateOnly.FromDateTime(entity.PassportExpiryDate);
            if (entity.LastPasswordChangeTime != new DateTime(1, 1, 1)) member.LastPasswordChangeTime = entity.LastPasswordChangeTime;
            if (entity.IsAllow != null) member.IsAllow = (bool)entity.IsAllow;
            if (entity.GoogleId != null) member.GoogleId = entity.GoogleId;
            if (entity.LineId != null) member.LineId = entity.LineId;


            _db.SaveChanges();
        }

        public int Register(MemberEntity entity)
        {
            Member member = new Member()
            {
                MemberClassId = entity.MemberClassId,
                CountryId = entity.CountryId,
                Account = entity.Account,
                Password = entity.Password,
                ChineseLastName = entity.ChineseLastName,
                ChineseFirstName = entity.ChineseFirstName,
                EnglishLastName = entity.EnglishLastName,
                EnglishFirstName = entity.EnglishFirstName,
                Gender = entity.Gender,
                DateOfBirth = DateOnly.FromDateTime(entity.DateOfBirth),
                Phone = entity.Phone,
                Email = entity.Email,
                TotalMileage = entity.TotalMileage,
                PassportNumber = entity.PassportNumber,
                PassportExpiryDate = DateOnly.FromDateTime(entity.PassportExpiryDate),
                RegistrationTime = entity.RegistrationTime,
                LastPasswordChangeTime = entity.LastPasswordChangeTime,
                IsAllow = (bool)entity.IsAllow,
                ConfirmCode = entity.ConfirmCode,
                PasswordSalt = entity.PasswordSalt,
            };

            var memberId =  
                _db.Members.Add(member);
                _db.SaveChanges();
            return member.Id;
        }

        public bool ActiveRegister(int memberId, string confirmCode)
        {
            throw new NotImplementedException();
        }
    }
}
