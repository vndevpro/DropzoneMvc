using FileUploadHandler;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.Mvc;

namespace DropzoneV5DemoAspNetMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Use this form to test the upload wit DropzoneJS.";

            return View();
        }


        [HttpPost]
        public ActionResult HandleFiles()
        {
            var attachmentInfos = UploadFiles();
            return Json(attachmentInfos, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<string> UploadFiles()
        {
            var temporaryFolder = Request.Headers["X-DZ-ComponentId"];
            if (string.IsNullOrWhiteSpace(temporaryFolder))
            {
                temporaryFolder = Guid.NewGuid().ToString();
            }

            var tempFilesRoot = ConfigurationManager.AppSettings["TempFilesRoot"] ?? "~/App_Data";
            if (!Path.IsPathRooted(tempFilesRoot))
            {
                tempFilesRoot = Server.MapPath(tempFilesRoot);
            }

            var uploader = new HttpRequestFilesUploader(Request, new AllowAllAttachmentSecurityService());
            return uploader.UploadRequestFiles(tempFilesRoot, temporaryFolder);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}