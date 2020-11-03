using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class BaseController : Controller
    {
        protected override bool DisableAsyncSupport
        {
            get { return true; }
        }
        protected override void ExecuteCore()
        {
            SetCulture();
            base.ExecuteCore();
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Remove parameter in case of changing culture request.
            if (Request.QueryString[Settings.InitSetting.Instance.CultureCookieName] != null)
            {
                // By redirecting to the same controller, same action (but lacking the query parameter).
                filterContext.Result = RedirectToAction(filterContext.RouteData.Values["action"].ToString(), filterContext.RouteData.Values["controller"].ToString());
            }
            base.OnActionExecuting(filterContext);
        }
        private string GetCultureCookie()
        {
            var cookie = Request.Cookies[Settings.InitSetting.Instance.CultureCookieName];
            return cookie != null ? cookie.Value : null;
        }
        private void SetCultureCookie(string cultureName)
        {
            HttpCookie cookie = Request.Cookies[Settings.InitSetting.Instance.CultureCookieName];
            if (cookie != null)
            {
                cookie.Value = cultureName;
            }
            else
            {
                cookie = new HttpCookie(Settings.InitSetting.Instance.CultureCookieName);
                cookie.Value = cultureName;
                cookie.Expires = DateTime.Now.Add(Settings.InitSetting.Instance.CookieLifeSpan);
            }
            Response.Cookies.Add(cookie);
        }
        private void SetCulture()
        {
            var cultureName = Request.QueryString[Settings.InitSetting.Instance.CultureCookieName] ?? GetCultureCookie();
            if (cultureName == null || (cultureName != "en-US" && cultureName != "ja-JP"))
            {
                cultureName = "en-US";
            }
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            SetCultureCookie(cultureName);
        }
    }
}