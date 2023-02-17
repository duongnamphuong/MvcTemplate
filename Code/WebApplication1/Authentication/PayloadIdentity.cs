using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Modules;

namespace WebApplication1.Authentication
{
    /// <summary>
    /// Represents the payload in JSON Web Token.
    /// </summary>
    public class PayloadIdentity
    {
        /// <summary>
        /// Time when this token is issued, represented in UNIX timestamp UTC timezone
        /// </summary>
        public long IssuedAt { get; set; }

        /// <summary>
        /// Time when this token would expires, represented in UNIX timestamp UTC timezone
        /// </summary>
        public long ExpireAt { get; set; }

        public string Username { get; set; }

        /// <summary>
        /// Check whether the token expires yet. Timezone: UTC
        /// </summary>
        /// <returns></returns>
        public bool IsTokenExpired()
        {
            return ExpireAt <= DateGenerator.ToUnixTimeStamp(DateTime.UtcNow);
        }
    }
}