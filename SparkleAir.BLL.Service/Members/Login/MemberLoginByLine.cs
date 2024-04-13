using SparkleAir.IDAL.IRepository.Members;
using SparkleAir.Infa.Criteria.Members;
using SparkleAir.Infa.Dto.Members;
using SparkleAir.Infa.Utility.Exts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.BLL.Service.Members.Login
{
    internal class MemberLoginByLine : IMemberLogin
    {
        public async Task<MemberDto> Login(IMemberRepository repository, MemberLoginDto loginDto)
        {
            MemberGetCriteria getCriteria = new MemberGetCriteria()
            {
                LineId = loginDto.LineCode,
            };
            var memberEntity = repository.Get(getCriteria);

            return memberEntity.EntityToDto();
        }
    }
}
