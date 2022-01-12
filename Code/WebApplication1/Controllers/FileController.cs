using LogUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Attributes;

namespace WebApplication1.Controllers
{
    [CustomAuthorize]
    public class FileController : BaseController
    {
        // GET: File
        public ActionResult Index()
        {
            try
            {
                string RootDirectory = Server.MapPath("~");
                DirectoryInfo d = new DirectoryInfo($"{RootDirectory}files\\");
                FileInfo[] Files = d.GetFiles("*.txt");
                ViewBag.Files = Files;
            }
            catch (Exception ex)
            {
                Log4netLogger.Error(MethodBase.GetCurrentMethod().DeclaringType, $"Error in {nameof(FileController)}", ex);
                return View("Error");
            }
            ViewBag.Title = Resources.Resource.File;
            return View();
        }
        public ActionResult Download()
        {
            return Json(new {
                success = true,
                note = "mock result"
            }, JsonRequestBehavior.AllowGet);
        }
    }
}