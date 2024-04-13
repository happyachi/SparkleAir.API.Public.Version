using SparkleAir.IDAL.IRepository.Members;
using SparkleAir.Infa.Dto.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.BLL.Service.Members.Login
{
    public interface IMemberLogin
    {
        Task<MemberDto> Login(IMemberRepository repository, MemberLoginDto loginDto);
    }
}
