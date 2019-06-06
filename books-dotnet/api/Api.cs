using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using zohobooks.util;
using zohobooks.exceptions;

namespace zohobooks.api
{
    /// <summary>
    /// Class Api is the super class to all API classes.
    /// This class will maintains the service URL and credentials.
    /// </summary>
    public class Api
    {
        private readonly string Location = "com";
        private const string baseURL = "https://books.zoho.{0}/api/v3";

        /// <summary>
        /// It is the API base URL for the Zoho Books service. 
        /// </summary>
        protected string baseurl { get => string.Format(baseURL, Location); }

        /// <summary>
        /// Initializes static members of the <see cref="Api"/> class.
        /// </summary>
        static Api()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
            };
        }

        /// <summary>
        /// The authtoken is the authentication parameter which authenticates the API call.
        /// </summary>
        string authtoken { get; set; }
        /// <summary>
        /// The organization identifier is used to choose on which organisation,the API call has been applied.
        /// </summary>

        string organizationId { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Api"/> class.
        /// </summary>
        /// <param name="RefreshToken">Refresh token</param>
        /// <param name="OrganizationID">The organization_id is the identifier of the organisation on which user is working currently.</param>

        public Api(string RefreshToken, string OrganizationID)
        {
            this.organizationId = OrganizationID;

            if (SessionManager.Sessions.TryGetValue(RefreshToken, out SessionData value))
            {
                Location = value.APIdomen;
                if (value.TokenExpireTime == DateTime.MinValue || value.TokenExpireTime < DateTime.Now.AddSeconds(5))
                {
                    var r = TokenRefresh.GetAccessToken(value.RefreshToken, value.APIdomen, value.ClientID, value.ClientSecret);
                    if (r != null && r.error == null & !string.IsNullOrEmpty(r.access_token))
                    {
                        authtoken = r.access_token;
                        value.TokenExpireTime = DateTime.Now.AddSeconds(r.expires_in_sec);
                        value.AuthToken = r.access_token;
                    }
                    else
                    {
                        if (r.error != null)
                        {
                            throw new BooksException(r.error);
                        }
                    }
                }
                else
                {
                    authtoken = value.AuthToken;
                }
            }
            else
            {
                throw new BooksException("Сессия не найдена, используйте new ZohoBooks(new ZohoCredentials { ... })");
            }
        }

        /// <summary>
        /// Constructs the QueryString using users authToken and organisation id.
        /// </summary>
        /// <returns>A queryString as a Dictionary object.</returns>
        public Dictionary<object, object> getQueryParameters()
        {
            var queryParameters = new Dictionary<object, object>();
            queryParameters.Add("authtoken", authtoken);
            queryParameters.Add("organization_id", organizationId);
            return queryParameters;
        }

        /// <summary>
        /// Constructs the Dictionary object using user's auth token,organisation id and using the query parameters.
        /// </summary>
        /// <param name="queryParameters">The query parameters.</param>
        /// <returns>Dictionary{System.ObjectSystem.Object}.</returns>
        public Dictionary<object, object> getQueryParameters(Dictionary<object, object> queryParameters)
        {
            if (queryParameters == null)
                queryParameters = new Dictionary<object, object>();
            if (!queryParameters.ContainsKey("authtoken"))
                queryParameters.Add("authtoken", authtoken);
            else
                queryParameters["authtoken"] = authtoken;

            if (!queryParameters.ContainsKey("organization_id"))
                queryParameters.Add("organization_id", organizationId);
            else
                queryParameters["organization_id"] = organizationId;

            return queryParameters;
        }
    }
}
