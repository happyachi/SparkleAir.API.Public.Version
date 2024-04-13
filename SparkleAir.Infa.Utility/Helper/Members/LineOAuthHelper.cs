using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SparkleAir.Infa.Utility.Helper.Members
{
    public class LineOAuthHelper
    {
        private readonly string _getTokenUrl = "https://api.line.me/oauth2/v2.1/token";

        private readonly string _redirectUri = "http://localhost:8888/login";

        private readonly string _clientId = "2004013922";

        private readonly string _client_secret = "7bf0ccd16f6fcc579a5c63055bb66dd2";

        public async Task<string> GetToken(string code)
        {
            var grantType = "authorization_code";

            var content = $"grant_type={HttpUtility.UrlEncode(grantType)}" +
                $"&code={HttpUtility.UrlEncode(code)}" +
                $"&redirect_uri={HttpUtility.UrlEncode(_redirectUri)}" +
                $"&client_id={HttpUtility.UrlEncode(_clientId)}" +
                $"&client_secret{HttpUtility.UrlEncode(_client_secret)}";

            var postContent = new StringContent(content);
            var client = new HttpClient();
            //client.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
            var response = client.PostAsync(_getTokenUrl, postContent).Result;
            return await response.Content.ReadAsStringAsync();
        }
    }
}
