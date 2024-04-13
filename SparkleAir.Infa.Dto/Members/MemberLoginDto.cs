using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Dto.Members
{
    public class MemberLoginDto
    {
        [JsonProperty("loginMethod")]
        public required string LoginMethod {  get; set; }

        [JsonProperty("account")]
        public string? Account { get; set; }

        [JsonProperty("password")]
        public string? Password { get; set; }

        [JsonProperty("passwordConfirm")]
        public string? PasswordConfirm { get; set; }

        [JsonProperty("googleAccessToken")]
        public string? GoogleAccessToken { get; set; }

        [JsonProperty("lineCode")]
        public string? LineCode { get; set; }
    }
}
