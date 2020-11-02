using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Attributes;
using WebApplication1.Authentication;

namespace WebApplication1.Controllers
{
    [CustomAuthorize]
    public class LoginController : Controller
    {
        [AllowAnonymous]
        // GET: Login
        public ActionResult Index()
        {
            string token = HttpContext.GetAuthToken();
            if (AuthUtility.IsAuthorized(token))
                return RedirectToAction("Index", "Home");
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            string username = form["username"];
            string password = form["password"];
            if ((username == "sampleusername") && password == "password1")
            {
                HttpContext.SetAuthorizationCookie(username);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            HttpContext.ClearAuthorizationCookie();
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(FormCollection form)
        {
            throw new NotImplementedException();
        }
    }
}