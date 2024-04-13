using SparkleAir.IDAL.IRepository.Members;
using SparkleAir.Infa.Criteria.Members;
using SparkleAir.Infa.Dto.Members;
using SparkleAir.Infa.Utility.Exts.Entities;
using SparkleAir.Infa.Utility.Helper.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SparkleAir.BLL.Service.Members.Login
{
    internal class MemberLoginByGoogle : IMemberLogin
    {
        public  async Task<MemberDto> Login(IMemberRepository repository, MemberLoginDto loginDto)
        {
            // 確認使否有GoogleAccessToken
            if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.GoogleAccessToken))
            {
                throw new ArgumentNullException("GoogleAccessToken", "GoogleAccessToken不得為空值");
            }

            // 透過Token去找 userInfo
            var google = new GoogleOAuthHelper();
            var userInfo = await google.GetUserInfo(loginDto.GoogleAccessToken);

            if(userInfo == null || userInfo.Id == null)
            {
                throw new Exception("Google認證失敗");
            }
            var criteria = new MemberGetCriteria
            {
                GoogleId = userInfo.Id
            };

            var memberEntity = repository.Get(criteria);

            var memberDto = memberEntity.EntityToDto();

            return memberDto;
        }
    }
}
