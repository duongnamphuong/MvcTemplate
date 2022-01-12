using LogUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Attributes;
using WebApplication1.ClassExtend;

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
                string RootDirectory = Server.MapPath("~/App_Data/");
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
        public ActionResult Download(string name)
        {
            if (name == null)
                return View("Error");
            //var appData = Server.MapPath("~/App_Data/files");
            //var fullPath = Path.Combine(appData, name);
            return new DownloadResult
            {
                VirtualPath = "~/App_Data/files/" + name,
                FileDownloadName = name
            };
        }
    }
}