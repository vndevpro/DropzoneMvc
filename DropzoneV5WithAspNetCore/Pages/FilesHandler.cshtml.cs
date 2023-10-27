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

        public IActionResult OnGetCleanup()
        {
            var deleteCount = 0;
            var tempFilesRoot = Path.Combine(_contentRoot, "App_Data");

            foreach (var subDir in Directory.GetDirectories(tempFilesRoot))
            {
                if (DateTime.Now - Directory.GetLastWriteTime(subDir) > TimeSpan.FromHours(1))
                {
                    Directory.Delete(subDir, true);
                    deleteCount += 1;
                }
            }

            return new ContentResult()
            {
                Content = $"OK - Deleted {deleteCount}",
            };
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

            var tempFilesRoot = Path.Combine(_contentRoot, "App_Data", temporaryFolder);

            var uploader = new HttpRequestFilesUploader(Request);
            return uploader.UploadRequestFiles(tempFilesRoot);
        }
    }
}
