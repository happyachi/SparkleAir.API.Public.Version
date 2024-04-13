using SparkleAir.IDAL.IRepository.Members;
using SparkleAir.Infa.Criteria.Members;
using SparkleAir.Infa.Dto.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.BLL.Service.Members
{
    public class MemberLoginLogService
    {
        private IMemberLoginLogRepository _repo;

        public MemberLoginLogService(IMemberLoginLogRepository repo)
        {
            _repo = repo;
        }

        public List<MemberLoginLogDto> Search(MemberLoginLogSearchCriteria criteria)
        {
            var entity = _repo.Search(criteria);

            var dto = entity.Select(m => new MemberLoginLogDto
            {
                Id = m.Id,
                MemberId = m.MemberId,
                MemberName = m.MemberName,
                Logintime = m.Logintime,
                LogoutTime = m.LogoutTime,
                IPAddress = m.IPAddress,
                LoginStatus = m.LoginStatus
            }).ToList();

            return dto;
        }
    }
}
