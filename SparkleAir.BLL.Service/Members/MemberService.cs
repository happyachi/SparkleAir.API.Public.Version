using Hangfire;
using Microsoft.Extensions.Configuration;
using SparkleAir.BLL.Service.Members.Login;
using SparkleAir.IDAL.IRepository.Members;
using SparkleAir.Infa.Criteria.Members;
using SparkleAir.Infa.Dto.Members;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.Members;
using SparkleAir.Infa.Utility.Exts.Entities;
using SparkleAir.Infa.Utility.Helper;
using SparkleAir.Infa.Utility.Helper.GoogleDriveApi;
using SparkleAir.Infa.Utility.Helper.Jwts;
using SparkleAir.Infa.Utility.Helper.Members;
using SparkleAir.Infa.Utility.Helper.Notices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HashHelper = SparkleAir.Infa.Utility.Helper.HashHelper;

namespace SparkleAir.BLL.Service.Members
{
    public class MemberService
    {
        private readonly IMemberRepository _repo;
        private readonly IConfiguration _configuration;


        public MemberService(IMemberRepository repo, IConfiguration configuration)
        {
            _repo = repo;
            _configuration = configuration;

        }

        public List<MemberDto> Search(MemberSearchCriteria criteria)
        {
            var list = _repo.Search(criteria)
                .Select(m => m.EntityToDto())
                .ToList();

            return list;
        }


        public MemberDto Get(MemberGetCriteria criteria)
        {
            var member = _repo.Get(criteria);
            if(member == null)
            {
                return null;
            }

            var dto = member.EntityToDto();

            return dto;

        }

        public IEnumerable<int> GetAllMembersIdByMemberClassId(int memberClassId)
        {
            var meberIdList = _repo.GetAllMembersIdByMemberClassId(memberClassId);

            return meberIdList;
        }

        public void Update(MemberDto member)
        {

            var entity = new MemberEntity
            {
                Id = member.Id,
                MemberClassId = member.MemberClassId,
                CountryId = member.CountryId,
                Account = member.Account,
                Password = member.Password,
                ChineseLastName = member.ChineseLastName,
                ChineseFirstName = member.ChineseFirstName,
                EnglishLastName = member.EnglishLastName,
                EnglishFirstName = member.EnglishFirstName,
                Gender = member.Gender,
                DateOfBirth = member.DateOfBirth,
                Phone = member.Phone,
                Email = member.Email,
                TotalMileage = member.TotalMileage,
                PassportNumber = member.PassportNumber,
                PassportExpiryDate = member.PassportExpiryDate,
                RegistrationTime = member.RegistrationTime,
                LastPasswordChangeTime = member.LastPasswordChangeTime,
                IsAllow = member.IsAllow,
                ConfirmCode = member.ConfirmCode,
                GoogleId = member.GoogleId,
                LineId = member.LineId,
                
            };

            _repo.Update(entity);
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        public async Task<string>Login(MemberLoginDto loginDto)
        {
            // 先確認是哪個登入方法
            IMemberLogin memberLogin = ChooseLoginMethod(loginDto);
            
            // 給一個方法去執行，到時回傳一個用戶資料
            var memberDto = await memberLogin.Login(_repo, loginDto);
            if (memberDto != null && memberDto.IsAllow == true)
            {
                Dictionary<string, string> claimsValue = new Dictionary<string, string>();

                claimsValue.Add("EnglishFirstName", memberDto.EnglishFirstName);
                claimsValue.Add("MemberId", memberDto.Id.ToString());

                JwtHelper jwtHelper = new JwtHelper(_configuration);
                string token = jwtHelper.GenerateToken(memberDto.EnglishFirstName, claimsValue);

                return token;
            }
            else
            {
                throw new Exception("登入失敗");
            }
        }

        private IMemberLogin ChooseLoginMethod(MemberLoginDto loginDto)
        {
            switch (loginDto.LoginMethod)
            {
                case ("Account"):
                    return new MemberLoginByAccount();
                case ("Google"):
                    return new MemberLoginByGoogle();
                case ("Line"):
                    return new MemberLoginByLine();
            }
            throw new Exception("錯誤的登入方法");
        }

        public bool CheckAccountIsRegister(string account)
        {
            MemberGetCriteria criteria = new MemberGetCriteria()
            {
                Account = account
            };
            var member = _repo.Get(criteria);
            bool result = member!=null? true: false;
            return result;
        }

        public bool CheckEmailIsRegister(string email)
        {
            MemberGetCriteria criteria = new MemberGetCriteria()
            {
                Email = email
            };
            var member = _repo.Get(criteria);
            bool result = member != null ? true : false;
            return result;
        }

        public int Register(MemberRegisterDto dto, string baseUri)
        {
            MemberGetCriteria criteria = new MemberGetCriteria()
            {
                Account = dto.Account
            };
            var member = _repo.Get(criteria);
            if (member != null) { throw new Exception("帳號已存在"); }

            

            // 填入confirmCode
            var confirmCode = Guid.NewGuid().ToString("N");
            var salt = Guid.NewGuid().ToString("N");

            var inputPasswordHash = Infa.Utility.Helper.HashHelper.ToSHA256(dto.Password, salt);


            // todo 發出確認信
            var urlTemplate = baseUri + // 生成 www.example.com 或 example.com
                              "/ActiveRegister?memberId={0}&confirmCode={1}"; // 生成網頁url

            MemberEntity memberEntity = new MemberEntity()
            {
                MemberClassId = 1,
                CountryId = 1,
                Account = dto.Account,
                Password = inputPasswordHash.Trim(),
                ChineseLastName = dto.ChineseLastName,
                ChineseFirstName = dto.ChineseFirstName,
                EnglishLastName = dto.EnglishLastName,
                EnglishFirstName = dto.EnglishFirstName,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                Phone = dto.Phone,
                Email = dto.Email,
                TotalMileage = 0,
                PassportNumber = dto.PassportNumber,
                PassportExpiryDate = dto.PassportExpiryDate,
                RegistrationTime = DateTime.Now,
                LastPasswordChangeTime = DateTime.Now,
                IsAllow = false,
                ConfirmCode = confirmCode.Trim(),
                PasswordSalt = salt.Trim(),
                GoogleId = null,
                LineId = null,
            };

            var memberId = _repo.Register(memberEntity);

            // 發送設置新密碼的信
            var url = string.Format(urlTemplate, memberId, confirmCode);

            var subject = "Sparkle Airline會員註冊成功";
            var body = $@"歡迎您成為Sparkle Airline會員，請點擊以下連結開通會員功能
                        {url}";

            var emailSetting = _configuration.GetSection("SendEmailSetting").Get<Dictionary<string, string>>();
            SendEmailHelper.SendEmail(
                emailSetting["FromEmail"],
                emailSetting["FromPassword"],
                dto.Email,
                subject,
                body);

            return memberId;
        }

        public bool ActiveRegister(int memberId, string confirmCode)
        {
            //驗證傳入值是否合理
            if (memberId <= 0 || string.IsNullOrEmpty(confirmCode))
            {
                return false;  // 在view中，我們會顯示"已開通"
            }

            MemberGetCriteria criteria = new MemberGetCriteria()
            {
                Id = memberId,
            };
            var member = _repo.Get(criteria);

            if (member == null) return false;

            if(member.ConfirmCode != confirmCode) return false;

            var memberEntity = new MemberEntity()
            {
                Id=memberId,
                IsAllow = true,
            };

            _repo.Update(memberEntity);

            return true;

            // 根據memberId, confirmCode 取得未確認的Member
            //bool  r = _repo(memberId, confirmCode);
            //if (member == null) return View();

            //// 如果有找到，將他更新為已確認
            //ConfirmMember(memberId);
            //return View();
        }

        public void UpdateMemberPassword(UpdateMemberPasswordDto updateDto, int memberId)
        {
            if( updateDto.PasswordNew != updateDto.PasswordConfirm)
            {
                throw new Exception("密碼錯誤");
            }

            MemberGetCriteria getCriteria = new MemberGetCriteria()
            {
                Id = memberId
            };
            var memberEntity = _repo.Get(getCriteria);

            // 確認是否有找到member，沒有就報錯
            if (memberEntity == null)
            {
                throw new Exception("密碼錯誤");
            }

            // 檢查密碼是否正確
            var passwordSalt = memberEntity.PasswordSalt.Trim();
            var inputPassword = updateDto.PasswordOld;

            // 將輸入的密碼加密
            var inputPasswordHash = HashHelper.ToSHA256(inputPassword, passwordSalt);

            if (inputPasswordHash == memberEntity.Password)
            {
               var newPasswordHash = HashHelper.ToSHA256(updateDto.PasswordNew, passwordSalt);

                MemberEntity entity = new MemberEntity()
                {
                    Id = memberId,
                    Password = newPasswordHash
                };
                _repo.Update(entity);
            }
        }


        public async void BindgoogleLogin(string accessToken, int memberId)
        {
            // 確認使否有GoogleAccessToken
            if (memberId == null || string.IsNullOrWhiteSpace(accessToken))
            {
                throw new ArgumentNullException("GoogleAccessToken", "GoogleAccessToken不得為空值");
            }

            // 透過Token去找 userInfo
            var google = new GoogleOAuthHelper();
            var userInfo = await google.GetUserInfo(accessToken);
            if (userInfo.Id == null) throw new Exception("google 驗證錯誤");

            MemberEntity memberEntity = new MemberEntity()
            {
                Id = memberId,
                //GoogleId = userInfo.Id,
            };
            _repo.Update(memberEntity);
        }
    }
}
