using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;

namespace zohobooks.parser
{
    class ErrorParser
    {
        public static string GetErrorMessage(HttpResponseMessage responce, bool errorCodeInMessage = false)
        {
            var message = "";
            var jsonObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(responce.Content.ReadAsStringAsync().Result);
            if (jsonObj.ContainsKey("message"))
            {
                if (errorCodeInMessage && jsonObj["code"] != null)
                {
                    message = jsonObj["code"] + " / ";
                }
                message += jsonObj["message"].ToString();
            }
            return message;
        }

        public static long GetErrorCode(HttpResponseMessage responce)
        {
            var jsonObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(responce.Content.ReadAsStringAsync().Result);
            long res = 0;
            if (jsonObj["code"] != null)
            {
                res = Convert.ToInt64(jsonObj["code"]);
            }
            return res;
        }
    }
}
