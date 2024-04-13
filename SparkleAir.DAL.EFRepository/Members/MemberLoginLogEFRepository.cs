using Microsoft.EntityFrameworkCore;
using SparkleAir.IDAL.IRepository.Members;
using SparkleAir.Infa.Criteria.Members;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.DAL.EFRepository.Members
{
    public class MemberLoginLogEFRepository : IMemberLoginLogRepository
    {
        private AppDbContext _db;
        public MemberLoginLogEFRepository(AppDbContext db)
        {
            _db = db;
        }
        public List<MemberLoginLogEntity> Search(MemberLoginLogSearchCriteria criteria)
        {
            var query = _db.MemberLoginLogs.AsNoTracking();

            if (criteria != null)
            {
                // todo 篩選
            }
            var entity = query.Select(member => new MemberLoginLogEntity
            {
                Id = member.Id,
                MemberId = member.MemberId,
                MemberName = member.Member.EnglishFirstName + "  " + member.Member.EnglishLastName,
                Logintime = member.Logintime,
                LogoutTime = member.LogoutTime,
                IPAddress = member.Ipaddress,
                LoginStatus = member.LoginStatus

            }).ToList();

            return entity;
        }
    }
}
