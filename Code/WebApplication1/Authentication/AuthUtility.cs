using AuthorizeBll;
using JwtGenerate;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebApplication1.Modules;

namespace WebApplication1.Authentication
{
    public static class AuthUtility
    {
        public static void SetAuthorizationCookie(this HttpContextBase httpContext, string username)
        {
            string key = ConfigurationManager.AppSettings["AuthenticationCookieName"];
            DateTime utcNow = DateTime.UtcNow;
            PayloadIdentity identity = new PayloadIdentity()
            {
                IssuedAt = DateGenerator.ToUnixTimeStamp(utcNow),
                ExpireAt = DateGenerator.ToUnixTimeStamp(utcNow) + int.Parse(ConfigurationManager.AppSettings["TokenExpiresAfter"]),
                Username = username
            };
            JwtUtility<JwtHeader, PayloadIdentity> jwtUtil = new JwtUtility<JwtHeader, PayloadIdentity>(new JwtHeader(), identity);
            string token = jwtUtil.ToString();
            HttpCookie cookie = new HttpCookie(key);
            cookie.HttpOnly = false;
            cookie.Value = token;
            cookie.Expires = DateTime.Now.AddSeconds(int.Parse(ConfigurationManager.AppSettings["TokenExpiresAfter"]));
            httpContext.Response.Cookies.Add(cookie);
            AddTokenIssued(username, token, DateGenerator.ZeroUnixTimestamp.AddSeconds(identity.IssuedAt), DateGenerator.ZeroUnixTimestamp.AddSeconds(identity.ExpireAt));
        }
        public static void ClearAuthorizationCookie(this HttpContextBase httpContext)
        {
            string key = ConfigurationManager.AppSettings["AuthenticationCookieName"];
            if (httpContext.Request.Cookies[key] != null)
                httpContext.Response.Cookies[key].Expires = DateTime.Now.AddDays(-1);
        }
        public static string GetAuthToken(this HttpContextBase httpContext)
        {
            var cookie = httpContext.Request.Cookies[ConfigurationManager.AppSettings["AuthenticationCookieName"]];
            return cookie != null ? cookie.Value : null;
        }
        public static string GetUsername(this HttpContextBase httpContext)
        {
            string username = null;
            JwtUtility<JwtHeader, PayloadIdentity> jwtUtil = new JwtUtility<JwtHeader, PayloadIdentity>(new JwtHeader(), new PayloadIdentity());
            string token = httpContext.GetAuthToken();
            if (token != null)
            {
                PayloadIdentity payloadIdentity = jwtUtil.GetPayload(token);
                username = payloadIdentity.Username;
            }
            return username;
        }
        public static bool IsAuthorized(string token, bool extendToken)
        {
            JwtUtility<JwtHeader, PayloadIdentity> jwtUtil = new JwtUtility<JwtHeader, PayloadIdentity>(new JwtHeader(), new PayloadIdentity());
            PayloadIdentity payloadIdentity = null;
            try
            {
                if (token != null)
                    payloadIdentity = jwtUtil.GetPayload(token);
            }
            catch (Exception) { }
            bool isAuthorized = (token != null && jwtUtil.IsValid(token) && payloadIdentity != null && !payloadIdentity.isTokenExpired());
            if (isAuthorized && extendToken)
            {
                ExtendTokenIssued(payloadIdentity.Username, token);
            }
            return isAuthorized;
        }
        private static void AddTokenIssued(string username, string token, DateTime IssuedAtUtc, DateTime ExpireAtUtc)
        {
            AuthorizeService.AddTokenIssued(username, token, IssuedAtUtc, ExpireAtUtc);
        }
        private static void ExtendTokenIssued(string username, string token)
        {
            AuthorizeService.ExtendTokenIssued(username, token);
        }
    }
}