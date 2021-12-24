using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Attributes;

namespace WebApplication1.Controllers
{
    [CustomAuthorize]
    public class PaypalController : BaseController
    {
        // GET: Paypal
        public ActionResult Index()
        {
            ViewBag.Title = "Paypal demo";
            return View();
        }
    }
}