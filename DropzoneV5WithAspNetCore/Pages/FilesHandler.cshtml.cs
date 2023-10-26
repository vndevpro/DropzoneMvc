using FileUploadHandler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DropzoneWithAspNetCore.Pages
{
    public class FilesHandlerModel : PageModel
    {
        private readonly string _contentRoot;

        public FilesHandlerModel(IHostEnvironment environment)
        {
            _contentRoot = environment.ContentRootPath;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var files = UploadFiles();
            return new JsonResult(new
            {
                FilesUploaded = files
            });
        }

        private IEnumerable<string> UploadFiles()
        {
            var temporaryFolder = Request.Headers["X-DZ-ComponentId"];
            if (string.IsNullOrWhiteSpace(temporaryFolder))
            {
                temporaryFolder = Guid.NewGuid().ToString();
            }

            var tempFilesRoot = Path.Combine(_contentRoot, "App_Data");

            var uploader = new HttpRequestFilesUploader(Request, new AllowAllAttachmentSecurityService());
            return uploader.UploadRequestFiles(tempFilesRoot, temporaryFolder);
        }
    }
}
