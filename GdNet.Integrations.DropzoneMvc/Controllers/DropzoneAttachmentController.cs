using FileUploadHandler;
using GdNet.Integrations.DropzoneMvc.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.Mvc;
using System.Web.Routing;

namespace GdNet.Integrations.DropzoneMvc.Controllers
{
    public class DropzoneAttachmentController : Controller
    {
        private readonly IAttachmentSecurityService _attachmentSecurityCheck;

        private string _tempFilesRoot;

        public DropzoneAttachmentController(IAttachmentSecurityService attachmentSecurityCheck)
        {
            _attachmentSecurityCheck = attachmentSecurityCheck;
            _tempFilesRoot = ConfigurationManager.AppSettings["TempFilesRoot"] ?? "~/App_Data";
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            _tempFilesRoot = Path.IsPathRooted(_tempFilesRoot) ? _tempFilesRoot : Server.MapPath(_tempFilesRoot);
        }

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
            var temporaryFolder = Request.Headers["X-ComponentId"];
            return new HttpRequestFilesUploader(Request, _attachmentSecurityCheck)
                .UploadRequestFiles(_tempFilesRoot, temporaryFolder);
        }
    }
}
