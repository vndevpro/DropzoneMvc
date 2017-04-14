using GdNet.Integrations.DropzoneMvc.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace GdNet.Integrations.DropzoneMvc.Controllers
{
    public class DropzoneAttachmentController : Controller
    {
        public ActionResult GetComponent(Guid? referenceId)
        {
            var model = new DropzoneComponentViewModel()
            {
                ElementId = "filesZone",
                TargetAction = Url.Action("Handle")
            };

            if (referenceId.HasValue && referenceId.Value != Guid.Empty)
            {
                model.ComponentId = referenceId.Value;
            }

            return PartialView("_DropzoneComponent", model);
        }

        [HttpPost]
        public ActionResult Handle()
        {
            var attachmentInfos = UploadFiles();
            return Json(attachmentInfos, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<string> UploadFiles()
        {
            var rootFolder = Server.MapPath(ConfigurationManager.AppSettings["TempFilesRoot"]);
            var temporaryFolder = Request.Headers["X-ComponentId"];
            return new DropzoneUploader(Request).UploadRequestFiles(rootFolder, temporaryFolder);
        }
    }
}