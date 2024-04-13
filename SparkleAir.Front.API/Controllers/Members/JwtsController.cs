using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SparkleAir.Infa.Dto.Members;
using SparkleAir.Infa.Dto.Methods;
using SparkleAir.Infa.Utility.Helper.Jwts;

namespace SparkleAir.Front.API.Controllers.Members
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly JwtHelper _jwtHelper;
        public JwtsController(JwtHelper jwtHelper, IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtHelper = jwtHelper;
        }
        // 這裡用來產生我們的Token
        [AllowAnonymous]
        [HttpPost("gentoken")]
        public IActionResult GenToken(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest();
            }
            var dict = new Dictionary<string, string>();
            var token = _jwtHelper.GenerateToken(username, dict);
            return Ok(token);
        }

               


        [HttpPost("addJwtTokenData")]
        public BasicApiReturnDto AddJwtTokenData(AddJwtTokenDataDto dto)
        {
            try
            {
                // 把obj變成 dict
                var json = dto.Obj.ToString();
                var obj = JsonConvert.DeserializeObject<dynamic>(json);
                JwtHelper jwtHelper = new JwtHelper(_configuration);
                var dict = jwtHelper.ConvertDynamicToDictionary(obj);

                // 把token資訊解出來
                var claimsPrincipal = jwtHelper.DecodeJwtToken(dto.Token);
                Dictionary<string, string> dictFromToken = new Dictionary<string, string>();
                string? sub = null;
                (dictFromToken, sub) = jwtHelper.ClaimsPrincipalToDictionary(claimsPrincipal);


                foreach (var kvp in dictFromToken)
                {
                    if (!dict.ContainsKey(kvp.Key))
                    {
                        dict.Add(kvp.Key, kvp.Value);
                    }
                }

                var token = jwtHelper.GenerateToken(sub, dict);

                // 先驗證token

                // 增加 參數，若原本就有的直接覆蓋

                // 回傳 token
                return new BasicApiReturnDto
                {
                    IsVerify = true,
                    Token = token,
                };

            }
            catch (Exception error)
            {
                return new BasicApiReturnDto() 
                { 
                    IsVerify = false,
                    Error = error.Message
                };
            }
           
        }

        [HttpPost("deleteJwtTokenData")]
        public void DeleteJwtTokenData(AddJwtTokenDataDto dto)
        {
            var o = dto.Obj.ToList();
            // 先驗證token

            // 把有的項目移除

            // 回傳 token
        }



        // 來看看我們在Claims裡有有哪些屬性和內容
        [HttpGet("claims")]
        public IActionResult GetClaims()
        {
            return Ok(User.Claims.Select(p => new { p.Type, p.Value }));
        }

        // 回傳我們剛剛在產Token時輸入的username
        [HttpPost("username")]
        public IActionResult GetUserName()
        {
            var providedApiKey = Request.Headers["Authorization"].FirstOrDefault();

            return Ok(User.Identity.Name);
        }

        // 傳回Jwt的id
        [HttpPost("jwtid")]
        public IActionResult GetUniqueId()
        {

            var providedApiKey = Request.Headers["Authorization"].FirstOrDefault();


            var jti = User.Claims.FirstOrDefault(p => p.Type == "jti");
            return Ok(jti.Value);
        }
    }
}
