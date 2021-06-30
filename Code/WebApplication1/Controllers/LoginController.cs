using AuthorizeBll;
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
    public class LoginController : BaseController
    {
        [AllowAnonymous]
        // GET: Login
        public ActionResult Index()
        {
            string token = HttpContext.GetAuthToken();
            if (AuthUtility.IsAuthorized(token, true))
                return RedirectToAction("Index", "Home");
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            string username = form["username"];
            string password = form["password"];
            if (AuthorizeService.Login(username, password))
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
            ViewBagRegister();
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(FormCollection form)
        {
            if (AuthorizeService.RegisterUser(form["username"], form["password"], int.Parse(form["hashtype"])))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBagRegister();
                return View();
            }
        }

        private void ViewBagRegister()
        {
            ViewBag.HashTypeDict = Settings.InitSetting.Instance.HashTypes;
        }
    }
}