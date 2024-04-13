using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using Microsoft.IdentityModel.Tokens;

namespace SparkleAir.Infa.Utility.Helper.Jwts
{
    public class TokenManager
    {
        //金鑰，從設定檔或資料庫取得
        public string key = "bJs3iqzDSP1qiTzWeMJa2cMsQjlsijioJIUIOH498u0No21NczRvpfE5m7oUE1VCp4F";
    
    //產生 Token
        public Token Create(User user)
        {
            var exp = 3600;   //過期時間(秒)

            //稍微修改 Payload 將使用者資訊和過期時間分開
            var payload = new Payload
            {
                info = user,
                //Unix 時間戳
                exp = Convert.ToInt32(
                    (DateTime.Now.AddSeconds(exp) -
                     new DateTime(1970, 1, 1)).TotalSeconds)
            };

            var json = JsonConvert.SerializeObject(payload);
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
            var iv = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);

            //使用 AES 加密 Payload
            var encrypt = TokenCrypto
                .AESEncrypt(base64, key.Substring(0, 16), iv);

            //取得簽章
            var signature = TokenCrypto
                .ComputeHMACSHA256(iv + "." + encrypt, key.Substring(0, 64));

            return new Token
            {
                //Token 為 iv + encrypt + signature，並用 . 串聯
                access_token = iv + "." + encrypt + "." + signature,
                //Refresh Token 使用 Guid 產生
                refresh_token = Guid.NewGuid().ToString().Replace("-", ""),
                expires_in = exp,
            };
        }

        //取得使用者資訊
        public User GetUser(string token)
        {
            // 
            string secret = "bJs3iqzDSP1qiTzWeMJa2cMsQlsijioJIUIOH498u0No21NczRvpfE5m7oUE1VCp4F";
            var key1 = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key1),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);

            foreach (var item in claims.Claims)
            {
                
            }
            



            // 1
            //var handler = new JwtSecurityTokenHandler();
            //var jsonToken = handler.ReadToken(token);
            //var tokenS = jsonToken as JwtSecurityToken;

            var split = token.Split('.');
            var iv = split[0];
            var encrypt = split[1];
            var signature = split[2];

            //檢查簽章是否正確
            //if (signature != TokenCrypto
            //    .ComputeHMACSHA256(iv + "." + encrypt, key.Substring(0, 64)))
            //{
            //    return null;
            //}

            //使用 AES 解密 Payload
            var base64 = TokenCrypto
                .AESDecrypt(encrypt, key.Substring(0, 16), iv);
            var json = Encoding.UTF8.GetString(Convert.FromBase64String(base64));
            var payload = JsonConvert.DeserializeObject<Payload>(json);

            //檢查是否過期
            if (payload.exp < Convert.ToInt32(
                (DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds))
            {
                return null;
            }

           

            return payload.info;

        }

        
    }
    public class Token
    {
        //Token
        public string access_token { get; set; }
        //Refresh Token
        public string refresh_token { get; set; }
        //幾秒過期
        public int expires_in { get; set; }
    }
    public class Payload
    {
        //使用者資訊
        public User info { get; set; }
        //過期時間
        public int exp { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string username { get; set; }
    }
}
