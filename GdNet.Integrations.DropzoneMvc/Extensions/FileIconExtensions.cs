using System;
using System.IO;
using System.Web;

namespace GdNet.Integrations.DropzoneMvc.Extensions
{
    public static class FileIconExtensions
    {
        private const string IconFileExtension = ".png";

        /// <summary>
        /// Get icon of the file
        /// </summary>
        public static string GetFileIcon(string virtualPath, string fileExtension)
        {
            var folder = HttpContext.Current.Request.MapPath(virtualPath);
            var iconFileName = fileExtension + IconFileExtension;

            var filePath = Path.Combine(folder, iconFileName);
            if (File.Exists(filePath))
            {
                return Path.Combine(virtualPath, iconFileName);
            }

            if (fileExtension.EndsWith("x", StringComparison.InvariantCultureIgnoreCase))
            {
                return Path.Combine(virtualPath, fileExtension.Substring(0, fileExtension.Length - 1) + IconFileExtension).Replace('\\', '/');
            }

            return string.Empty;
        }
    }
}
