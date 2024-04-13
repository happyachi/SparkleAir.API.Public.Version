using Google.Apis.Drive.v3.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SparkleAir.Infa.Dto.Methods;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Utility.Helper.Jwts
{
    public class JwtHelper
    {
        private readonly IConfiguration _configuration;
        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 建立 Token
        /// </summary>
        /// <param name="memberName"></param>
        /// <param name="expireMinutes"></param>
        /// <returns></returns>
        public string GenerateToken(string memberName, Dictionary<string, string> keyValuePairs, int expireMinutes = 300 )
        {
            var issuer = _configuration.GetValue<string>("JwtSettings:Issuer");
            var signKey = _configuration.GetValue<string>("JwtSettings:SignKey");

            var claims = new List<Claim>();

            //sub(Subject) - jwt所面向的用戶
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, memberName));
            //jti(JWT ID) - jwt的唯一身份標識，主要用來作為一次性token,從而迴避重放攻擊
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            //iss(Issuer) - jwt簽發者
            //claims.Add(new Claim(JwtRegisteredClaimNames.Iss, issuer)); 
            //aud(Audience) - 接收jwt的一方
            //claims.Add(new Claim(JwtRegisteredClaimNames.Aud, "The Audience")); 
            //exp(Expiration Time) - jwt的過期時間，這個過期時間必須要大於簽發時間
            //claims.Add(new Claim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes(30).ToUnixTimeSeconds().ToString())); 
            // nbf(Not Before) - 定義在什麼時間之前，該jwt都是不可用的
            //claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()));
            // iat(Issued At) - jwt的簽發時間
            //claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()));



            foreach( var keyValue in keyValuePairs )
            {
                claims.Add(new Claim(keyValue.Key, keyValue.Value));
            }


            // 這裡可以加權限參數(角色、群組名)
            // claims.Add(new Claim("roles", "Admin"));
            // claims.Add(new Claim("roles", "Users"));

            var memberClaimsIdentity = new ClaimsIdentity(claims);

            // 建立一組對稱式加密的金鑰，主要用於 JWT 簽章之用
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signKey));

            // HmacSha256 必須大於 128 bits, 所以最少要超過16個字.
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // Create SecurityTokenDescriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                //Audience = issuer, 
                // 由於你的 API 受眾通常沒有區分特別對象，因此通常不太需要設定，也不太需要驗證
                //NotBefore = DateTime.Now, 預設值就是 DateTime.Now
                //IssuedAt = DateTime.Now, 預設值就是 DateTime.Now
                Subject = memberClaimsIdentity,
                Expires = DateTime.Now.AddMinutes(expireMinutes),
                SigningCredentials = signingCredentials
            };

            // 產出所需要的 JWT securityToken 物件，並取得序列化後的 Token 結果(字串格式)
            //處理 JWT 的物件，我們使用它的 WriteToken 方法將 JWT 物件轉換為字符串形式，以便於傳輸。
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var serializeToken = tokenHandler.WriteToken(securityToken);

            //返回 JWT 字符串，您可以使用它來向客戶端返回身份驗證令牌。
            return serializeToken;
        }

        public ClaimsPrincipal DecodeJwtToken(string token)
        {
            var secret = _configuration.GetValue<string>("JwtSettings:SignKey");
          
            var key = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);

            return claims;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns>Dictionary是Claim的Key,Value，string是sub(memberName)</returns>
        public (Dictionary<string, string>, string?) ClaimsPrincipalToDictionary(ClaimsPrincipal claimsPrincipal)
        {
            var claimsDictionary = new Dictionary<string, string>();

            foreach (var claim in claimsPrincipal.Claims)
            {
                if (claim.Type == "exp" || claim.Type == "iat" || claim.Type == "iss" || claim.Type == "jti" || claim.Type == "nbf" || claim.Type == "sub"|| claim.Type== "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier") continue;

                // 将每个声明添加到字典中
                claimsDictionary[claim.Type] = claim.Value;
            }
            var sub = claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;
            sub = (sub == null) ? claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value : sub;
            return (claimsDictionary,sub);
        }

        public Dictionary<string, string> ConvertDynamicToDictionary(dynamic obj)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            foreach (var kvp in obj)
            {
                dictionary.Add(kvp.Name, kvp.Value.ToString());
            }

            return dictionary;
        }

        
    }
}
