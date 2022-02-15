using JwtGenerate;
using LogUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication1.Authentication;

namespace WebApplication1.Attributes
{
    /// <summary>
    /// A customized implementation of AuthorizeAttribute (System.Web.Mvc) from blog: https://dougrathbone.com/blog/2011/07/24/writing-your-own-custom-aspnet-mvc-authorize-attributes
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string token;
            try
            {
                token = httpContext.GetAuthToken();
            }
            catch (Exception ex)
            {
                Log4netLogger.Error(MethodBase.GetCurrentMethod().DeclaringType, $"AuthorizeCore error", ex);
                return false;
            }
            return AuthUtility.IsAuthorized(token, true);
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index" }));
        }
    }
}