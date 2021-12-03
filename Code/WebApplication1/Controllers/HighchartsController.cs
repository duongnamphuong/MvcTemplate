using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Attributes;

namespace WebApplication1.Controllers
{
    [CustomAuthorize]
    public class HighchartsController : BaseController
    {
        // GET: Highcharts
        public ActionResult Index()
        {
            ViewBag.Title = Resources.Resource.Highcharts;
            return View();
        }
    }
}