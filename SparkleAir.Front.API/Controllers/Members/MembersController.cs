using Azure.Core;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Common;
using SparkleAir.BLL.Service.Members;
using SparkleAir.DAL.DapperRepository.Members;
using SparkleAir.DAL.EFRepository.Members;
using SparkleAir.IDAL.IRepository.Members;
using SparkleAir.Infa.Criteria.Members;
using SparkleAir.Infa.Dto.Members;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Utility.Helper.GoogleDriveApi;
using SparkleAir.Infa.Utility.Helper.Jwts;
using SparkleAir.Infa.Utility.Helper.Members;
using SparkleAir.Infa.Utility.Helper.Notices;
using System.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SparkleAir.Front.API.Controllers.Members
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMemberRepository _repo;
        private readonly MemberService _service;
        
        public MembersController(AppDbContext context, IConfiguration configuration)
        {
            // EF
            _repo = new MemberEFRepository(context);

            // Dapper
            _configuration = configuration;
            //var connectionString = configuration.GetConnectionString("AppDbContext");
            //_repo = new MemberDapperRepository(connectionString);

            _service = new MemberService(_repo, _configuration);
        }

        [HttpGet("testLineLogin")]
        public async Task testLineLogin(string code)
        {
 
            //g.CreateFolder();
            //g.ListFiles();

            //try
            //{
            //    var line = new LineOAuthHelper();
            //    var r= line.GetToken(code);
            //    return r.ToString();
            //}catch (Exception ex) { return ex.Message; }

        }

        [HttpGet]
        public async Task<IEnumerable<MemberDto>> GetMemberDtos()
        {
            MemberSearchCriteria criteria = null;

            return _service.Search(criteria);
        }

        [HttpPost("googleLogin")]
        public async Task GoogleLogin(GoogleLoginDto data)
        {
            var google = new GoogleOAuthHelper();
            var userInfo = google.GetUserInfo(data.AccessToken);
        }


        [HttpPost("Login")]
        public async Task<BasicApiReturnDto> Login([FromBody]MemberLoginDto loginDto)
        {
            try
            {
                var token = await _service.Login(loginDto);
                var returnDto = new BasicApiReturnDto()
                {
                    IsVerify = true,
                    Token = token,
                };

                return returnDto;
                // 回傳認證，再請前端加到用戶端section
            }
            catch (Exception error)
            {
                var returnDto = new BasicApiReturnDto()
                {
                    IsVerify = false,
                    Error = error.Message,
                };
                return returnDto;
            }
        }

        [HttpPost("ForgetPassword")]
        public async Task<string> ForgetPassword([FromForm]MemberForgetPasswordDto dto)
        {
            // todo ForgetPassword

            // 回傳已發信至信箱
            throw new NotImplementedException();

        }


        [HttpGet("ResetPassword/{value}")]
        public async Task<string> CheckMember(string value)
        {
            // 透過雜湊值去找指定會員
            // 回傳帳號(非認證)，再請前端加到用戶端section
            throw new NotImplementedException();


        }

        [HttpPost("ResetPassword")]
        public async Task<string> ResetPassword(UpdateMemberPasswordDto dto)
        {
            // 透過section和密碼去重新設定密碼
            // 成功要請用戶端清除帳號section
            throw new NotImplementedException();


        }

        [HttpPost("Register")]
        public async Task<BasicApiReturnDto> Register( MemberRegisterDto dto)
        {
            //生成 http:// 或 https:// 生成 www.example.com 或 example.com
            //var baseUri = $"{Request.Scheme}://{Request.Host}:{Request.Host.Port ?? 80}";
            var baseUri = $"http://127.0.0.1:8888";

            try
            {
                // 要驗證內容是否正確，正確再進行註冊
                // 
                _service.Register(dto, baseUri);
                var returnDto = new BasicApiReturnDto()
                {
                    IsVerify = true,
                };
                return returnDto;
            }
            catch (Exception error)
            {
                var returnDto = new BasicApiReturnDto()
                {
                    IsVerify = false,
                    Error = error.Message,
                };
                return returnDto;
            }
        }


        [HttpGet("GetAllMembersIdByMemberClassId/{memberClassId}")]
        public async Task<IEnumerable<int>>  GetAllMembersIdByMemberClassId(int memberClassId)
        {
            var meberIdList = _service.GetAllMembersIdByMemberClassId(memberClassId);

            return meberIdList;
        }

        [HttpGet("checkAccountIsRegister")]
        public async Task<bool> CheckAccountIsRegister(string account)
        {
            var result = _service.CheckAccountIsRegister(account);
            return result;
        }

        [HttpGet("checkEmailIsRegister")]
        public async Task<bool> CheckEmailIsRegister(string email)
        {
            var result = _service.CheckEmailIsRegister(email);
            return result;
        }

        [HttpGet("ActiveRegister")]
        public async Task<bool> ActiveRegister(int memberId, string confirmCode)
        {
            var result = _service.ActiveRegister(memberId, confirmCode);
            return result;
        }

        [HttpGet("GetMemberInfo")]
        public async Task<MemberDto> GetMemberInfo(string token)
        {
            try
            {
                // 把token資訊解出來
                JwtHelper jwtHelper = new JwtHelper(_configuration);
                var claimsPrincipal = jwtHelper.DecodeJwtToken(token);

                Dictionary<string, string> dictFromToken = new Dictionary<string, string>();
                string? sub = null;
                (dictFromToken, sub) = jwtHelper.ClaimsPrincipalToDictionary(claimsPrincipal);
                var memberId = dictFromToken["MemberId"];
                MemberGetCriteria criteria = new MemberGetCriteria()
                {
                    Id = Int32.Parse( memberId)
                };

                var dto = _service.Get(criteria);
                return dto;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("UpdateMemberInfo")]
        public async Task<BasicApiReturnDto> UpdateMemberInfo(MemberUpdateDto updateDto)
        {
            try
            {
                // 把token資訊解出來
                JwtHelper jwtHelper = new JwtHelper(_configuration);
                var claimsPrincipal = jwtHelper.DecodeJwtToken(updateDto.Token);

                Dictionary<string, string> dictFromToken = new Dictionary<string, string>();
                string? sub = null;
                (dictFromToken, sub) = jwtHelper.ClaimsPrincipalToDictionary(claimsPrincipal);
                var memberId = dictFromToken["MemberId"];

                MemberDto member = new MemberDto
                {
                    ChineseLastName = updateDto.ChineseLastName,
                    ChineseFirstName = updateDto.ChineseFirstName,
                    EnglishLastName = updateDto.EnglishLastName,
                    EnglishFirstName = updateDto.EnglishFirstName,
                    Phone = updateDto.Phone,
                    Email=updateDto.Email,
                    PassportNumber = updateDto.PassportNumber,
                    PassportExpiryDate=updateDto.PassportExpiryDate,
                    Id = Int32.Parse(memberId)
                };
                _service.Update(member);

                BasicApiReturnDto basicApiReturnDto = new BasicApiReturnDto()
                {
                    IsVerify = true,
                };
                return basicApiReturnDto;

            }
            catch (Exception e)
            {
                BasicApiReturnDto basicApiReturnDto = new BasicApiReturnDto()
                {
                    IsVerify = false,
                    Error = e.Message
                };
                return basicApiReturnDto;
            }
           
        }


        [HttpPost("UpdateMemberPassword")]
        public async Task<BasicApiReturnDto> UpdateMemberPassword(UpdateMemberPasswordDto updateDto)
        {
            try
            {
                // 把token資訊解出來
                JwtHelper jwtHelper = new JwtHelper(_configuration);
                var claimsPrincipal = jwtHelper.DecodeJwtToken(updateDto.Token);

                Dictionary<string, string> dictFromToken = new Dictionary<string, string>();
                string? sub = null;
                (dictFromToken, sub) = jwtHelper.ClaimsPrincipalToDictionary(claimsPrincipal);
                var memberId = dictFromToken["MemberId"];

                
                _service.UpdateMemberPassword(updateDto,Int32.Parse(memberId));

                BasicApiReturnDto basicApiReturnDto = new BasicApiReturnDto()
                {
                    IsVerify = true,
                };
                return basicApiReturnDto;

            }
            catch (Exception e)
            {
                BasicApiReturnDto basicApiReturnDto = new BasicApiReturnDto()
                {
                    IsVerify = false,
                    Error = e.Message
                };
                return basicApiReturnDto;
            }
        }

        [HttpPost("BindGoogleLogin")]
        public async Task<BasicApiReturnDto> BindGoogleLogin(GoogleLoginDto loginDto)
        {
            try
            {
                // 把token資訊解出來
                JwtHelper jwtHelper = new JwtHelper(_configuration);
                var claimsPrincipal = jwtHelper.DecodeJwtToken(loginDto.MemberToken);

                Dictionary<string, string> dictFromToken = new Dictionary<string, string>();
                string? sub = null;
                (dictFromToken, sub) = jwtHelper.ClaimsPrincipalToDictionary(claimsPrincipal);
                var memberId = dictFromToken["MemberId"];


                // 確認使否有GoogleAccessToken
                if (memberId == null || string.IsNullOrWhiteSpace(loginDto.AccessToken))
                {
                    throw new ArgumentNullException("GoogleAccessToken", "GoogleAccessToken不得為空值");
                }

                // 透過Token去找 userInfo
                var google = new GoogleOAuthHelper();
                var userInfo = await google.GetUserInfo(loginDto.AccessToken);
                if (userInfo.Id == null) throw new Exception("google 驗證錯誤");

                MemberGetCriteria getCriteria = new MemberGetCriteria
                {
                    GoogleId = userInfo.Id,
                };
                var dto = _service.Get(getCriteria);
                if (dto != null) throw new Exception("已被註冊");

                MemberDto memberDto = new MemberDto()
                {
                    Id = Int32.Parse(memberId),
                    GoogleId = userInfo.Id,
                };

                _service.Update(memberDto);

                BasicApiReturnDto basicApiReturnDto = new BasicApiReturnDto()
                {
                    IsVerify = true,
                };
                return basicApiReturnDto;

            }
            catch (Exception e)
            {
                BasicApiReturnDto basicApiReturnDto = new BasicApiReturnDto()
                {
                    IsVerify = false,
                    Error = e.Message
                };
                return basicApiReturnDto;
            }
        }


        [HttpPost("BindLineLogin")]
        public async Task<BasicApiReturnDto> BindLineLogin(LineLoginDto loginDto)
        {
            try
            {
                // 把token資訊解出來
                JwtHelper jwtHelper = new JwtHelper(_configuration);
                var claimsPrincipal = jwtHelper.DecodeJwtToken(loginDto.MemberToken);

                Dictionary<string, string> dictFromToken = new Dictionary<string, string>();
                string? sub = null;
                (dictFromToken, sub) = jwtHelper.ClaimsPrincipalToDictionary(claimsPrincipal);
                var memberId = dictFromToken["MemberId"];

                MemberGetCriteria getCriteria = new MemberGetCriteria
                {
                    LineId = loginDto.LineId,
                };
                var dto = _service.Get(getCriteria);
                if (dto != null) throw new Exception("已被註冊");

                MemberDto memberDto = new MemberDto()
                {
                    Id = Int32.Parse(memberId),
                    LineId = loginDto.LineId,
                };

                _service.Update(memberDto);

                BasicApiReturnDto basicApiReturnDto = new BasicApiReturnDto()
                {
                    IsVerify = true,
                };
                return basicApiReturnDto;

            }
            catch (Exception e)
            {
                BasicApiReturnDto basicApiReturnDto = new BasicApiReturnDto()
                {
                    IsVerify = false,
                    Error = e.Message
                };
                return basicApiReturnDto;
            }
        }
    }
}

