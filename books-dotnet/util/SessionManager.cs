using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zohobooks.util
{
    public class SessionData
    {
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string OrganizationID { get; set; }
        public string RefreshToken { get; set; }
        public string APIdomen { get; set; }
        public DateTime TokenExpireTime { get; set; }
        public string AuthToken { get; set; }
    }

    public static class SessionManager
    {
        public static readonly ConcurrentDictionary<string, SessionData> Sessions = new ConcurrentDictionary<string, SessionData>();
    }
}
