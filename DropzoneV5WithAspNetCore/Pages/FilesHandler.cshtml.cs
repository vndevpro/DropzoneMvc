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
            var files = Request.Form.Files;

            return new JsonResult(new
            {
                FilesCount = files.Count
            });
        }
    }
}
