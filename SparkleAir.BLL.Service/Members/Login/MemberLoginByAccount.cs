using SparkleAir.IDAL.IRepository.Members;
using SparkleAir.Infa.Criteria.Members;
using SparkleAir.Infa.Dto.Members;
using SparkleAir.Infa.Utility.Exts.Entities;
using SparkleAir.Infa.Utility.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.BLL.Service.Members.Login
{
    internal class MemberLoginByAccount : IMemberLogin
    {
        public async Task<MemberDto> Login(IMemberRepository repository, MemberLoginDto loginDto)
        {
            // 檢驗是否為空值
            if (loginDto == null ||  string.IsNullOrWhiteSpace( loginDto.Account))
            {
                throw new ArgumentNullException("Account", "");
            }

            // 檢驗密碼
            if (string.IsNullOrWhiteSpace(loginDto.Password))
            {
                throw new Exception("密碼不得為空值");
            }
            var criteria = new MemberGetCriteria
            {
                Account = loginDto.Account
            };

            // 取出member資料
            var memberEntity = repository.Get(criteria);

            // 確認是否有找到member，沒有就報錯
            if (memberEntity == null )
            {
                throw new Exception("帳號或密碼錯誤");
            }

            // 檢查密碼是否正確
            var passwordSalt = memberEntity.PasswordSalt.Trim();
            var inputPassword = loginDto.Password;

            // 將輸入的密碼加密
            var inputPasswordHash = HashHelper.ToSHA256(inputPassword, passwordSalt);

            // 臨時密碼確認
            //if (loginDto.Password == memberEntity.Password)
            //{
            //    MemberDto memberDto = memberEntity.EntityToDto();
            //    return memberDto;
            //}

            // 正常有加密的密碼
            if (inputPasswordHash == memberEntity.Password)
            {
                MemberDto memberDto = memberEntity.EntityToDto();
                return memberDto;
            }
            else 
            {
                // 密碼錯誤就報錯
                throw new Exception("帳號或密碼錯誤");
            }
        }
    }
}
