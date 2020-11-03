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
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ViewBag.Username = HttpContext.GetUsername();
            return View();
        }
    }
}
