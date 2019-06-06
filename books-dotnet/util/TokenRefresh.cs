using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace zohobooks.util
{
    public static class TokenRefresh
    {
        public class RespoceError
        {
            public string error { get; set; }
        }

        public class tokenResponse : RespoceError
        {
            public string access_token { get; set; }
            public long expires_in_sec { get; set; }
            public string api_domain { get; set; }
            public string token_type { get; set; }
            public long expires_in { get; set; }
            public string refresh_token { get; set; }
        }

        public static tokenResponse GetAccessToken(string RefreshToken, string domen, string ClientID, string ClientSecret)
        {
            using (var webClient = new WebClient())
            {
                string endpoint = "https://accounts.zoho.{0}/oauth/v2/token";

                var pars = new NameValueCollection();
                pars.Add("refresh_token", RefreshToken);
                pars.Add("client_id", ClientID);
                pars.Add("client_secret", ClientSecret);
                pars.Add("grant_type", "refresh_token");
                var s = Encoding.UTF8.GetString(webClient.UploadValues(string.Format(endpoint, domen), pars));

                return JsonConvert.DeserializeObject<tokenResponse>(s);
            }
        }
    }
}
