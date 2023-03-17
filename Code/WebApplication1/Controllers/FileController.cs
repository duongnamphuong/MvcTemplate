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
                string RootDirectory = Server.MapPath("~/App_Data/");
                DirectoryInfo d = new DirectoryInfo($"{RootDirectory}files\\");
                FileInfo[] Files = d.GetFiles("*.*");
                ViewBag.Files = Files.ToArray();
            }
            catch (Exception ex)
            {
                Log4netLogger.Error(MethodBase.GetCurrentMethod().DeclaringType, $"Error in {nameof(FileController)}", ex);
                return View("Error");
            }
            ViewBag.Title = Resources.Resource.File;
            return View();
        }
        public ActionResult Download(string fileName)
        {
            try
            {
                if (fileName == null)
                {
                    throw new Exception($"Error attempting to download: {nameof(fileName)} is null.");
                }
                string virtualPathToFile = "~/App_Data/files/" + fileName;
                if (System.IO.File.Exists(Server.MapPath(virtualPathToFile)))
                {
                    return File(virtualPathToFile, "application/octet-stream", fileName);
                }
                else
                {
                    return new HttpStatusCodeResult(404, "File not found.");
                }
            }
            catch (Exception ex)
            {
                Log4netLogger.Error(MethodBase.GetCurrentMethod().DeclaringType, $"Error attempting to download", ex);
                return View("Error");
            }
        }
    }
}