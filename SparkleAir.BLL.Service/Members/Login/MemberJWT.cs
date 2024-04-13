using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.BLL.Service.Members.Login
{
    public class MemberJWT
    {
        public static string JWT_Encoder(Guid fileID)
        {
            oJWT_Header header = new oJWT_Header();
            header.alg = "HS256";
            header.typ = "JWT";

            oJWT_Payload payload = new oJWT_Payload();
            DateTimeOffset now = DateTimeOffset.UtcNow;
            long unixTimestamp = now.ToUnixTimeSeconds();
            long unixTimestamp_exp = now.AddSeconds(20).ToUnixTimeSeconds();
            payload.iat = unixTimestamp;
            payload.exp = unixTimestamp_exp;
            payload.id = fileID;

            string json_header = JsonConvert.SerializeObject(header);
            byte[] b_header = System.Text.UTF8Encoding.UTF8.GetBytes(json_header);
            string b64_header = Convert.ToBase64String(b_header);

            string json_payload = JsonConvert.SerializeObject(payload);
            byte[] b_pl = System.Text.UTF8Encoding.UTF8.GetBytes(json_payload);
            string b64_payload = Convert.ToBase64String(b_pl);

            string encryptSignature = ComputeHMACSHA256(b64_header + b64_payload, b64_payload + payload.id);
            string token = b64_header + "." + b64_payload + "." + encryptSignature;

            return token;
        }
        
        public static string ComputeHMACSHA256(string data, string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            using (var hmacSHA = new HMACSHA256(keyBytes))
            {
                var dataBytes = Encoding.UTF8.GetBytes(data);
                var hash = hmacSHA.ComputeHash(dataBytes, 0, dataBytes.Length);
                return BitConverter.ToString(hash).Replace("-", "").ToUpper();
            }
        }
        public static bool JWT_Decoder(string JWT)
        {
            string[] ary = JWT.Split('.');
            if (ary.Length != 3) return false;
            else
            {
                string b64_header = ary[0];
                string b64_payload = ary[1];
                string Signature = ary[2];

                Byte[] ary_header = Convert.FromBase64String(b64_header);
                string json_header = System.Text.UTF8Encoding.UTF8.GetString(ary_header);
                oJWT_Header header = JsonConvert.DeserializeObject<oJWT_Header>(json_header);

                Byte[] ary_payload = Convert.FromBase64String(b64_payload);
                string json_pl = System.Text.UTF8Encoding.UTF8.GetString(ary_payload);
                oJWT_Payload payload = JsonConvert.DeserializeObject<oJWT_Payload>(json_pl);

                string encryptSignature = ComputeHMACSHA256(b64_header + b64_payload, b64_payload + payload.id);

                if (!Signature.Equals(encryptSignature)) return false;

                long exp = payload.exp;
                long now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                if (now > exp) return false;
            }
            return true;
        }
    }
    public class oJWT_Header
    {
        public string alg { get; set; }
        public string typ { get; set; }
    }
    public class oJWT_Payload
    {
        public long exp { get; set; }
        public long iat { get; set; }
        public Guid id { get; set; }
    }
}
