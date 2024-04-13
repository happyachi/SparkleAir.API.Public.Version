using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SparkleAir.Infa.Dto.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Utility.Helper.Members
{
    public class GoogleOAuthHelper
    {
        private readonly string _accessTokenUrl = "https://accounts.google.com/o/oauth2/token";
        private readonly string _userInfoUrl = "https://www.googleapis.com/oauth2/v1/userinfo";

        public async Task Main(string authCode)
        {
            // var token = await GetAccessToken(authCode);
            //var userInfo = await GetUserInfo(token.AccessToken);
            //var userInfo = await GetUserInfo(authCode);


        }

        /// <summary>
        /// 取得用戶資訊
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<GoogleUserInfoDto> GetUserInfo(string accessToken)
        {
            var client = new HttpClient();
            var resp = await client.GetAsync($"{_userInfoUrl}?access_token={accessToken}");
            if (resp.IsSuccessStatusCode)
            {
                var result = await resp.Content.ReadAsStringAsync();
                var userInfo = JsonConvert.DeserializeObject<GoogleUserInfoDto>(result);
                return userInfo;
            }
            return null;
        }

        /// <summary>
        /// 獲取訪問token
        /// </summary>
        /// <param name="authCode"></param>
        /// <returns></returns>
        //private async Task<AccessTokenResponse> GetAccessToken(string authCode)
        private async Task <string> GetAccessToken(string authCode)

        {
            // 客戶端ID
            var clientId = "";
            // 客戶端密鑰
            var clientSecret = "";
            //重新導向網址
            var redirectUrl = "http://localhost:8889/googleCallback";

            using (var client = new HttpClient())
            {
                var param = @$"code={authCode}&client_id={clientId}&client_secret={clientSecret}&redirect_url={redirectUrl}&scope=&grant_type=authoriztion_code";
                var request = new HttpRequestMessage(HttpMethod.Post, $"{_accessTokenUrl}?{param}");
                var resp = await client.SendAsync( request );
                if(resp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await resp.Content.ReadAsStringAsync();
                    return result;
                    //var data = JsonConvert.DeserializeObject<AccessTokenResponse>( result );
                    //return data;
                }
            }
            return null;
        }
    }


    internal class AccessTokenResponse
    {
        /// <summary>
        /// 可發送到google API的token
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// 以秒 令牌接入剩餘壽命
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// 一個JWT包含有關數字是由google簽署的用戶身分資訊
        /// </summary>
        [JsonProperty("id_token")]
        public string IdToken { get; set; }

        /// <summary>
        /// 訪問的範圍授出的access_token表示為空格分隔，區分大小寫字符串列表??
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { get; set; }

        /// <summary>
        /// 訪問的範圍授出的access_token表示為空格分隔，區分大小寫字符串列表??
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        /// <summary>
        /// 如果此字段僅本access_type參數設置為offline在認證請求
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
