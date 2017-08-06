using log4net;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace GdNet.Integrations.DropzoneMvc.Controllers
{
    public sealed class DropzoneMonitor
    {
        private readonly HttpRequestBase _request;
        private readonly ILog _logger = LogManager.GetLogger(typeof(DropzoneMonitor));

        public DropzoneMonitor(HttpRequestBase request)
        {
            _request = request;
        }

        /// <summary>
        /// Calculate the absolute paths of uploaded files
        /// </summary>
        public IEnumerable<string> GetActiveUploadedFiles()
        {
            var tempFilesRoot = ConfigurationManager.AppSettings["TempFilesRoot"];
            tempFilesRoot = Path.IsPathRooted(tempFilesRoot) ? tempFilesRoot : HttpContext.Current.Server.MapPath(tempFilesRoot);

            var attachments = _request.Form["dropzone_files"];
            _logger.InfoFormat("dropzone_files is {0}", attachments);

            return
                attachments.Split(';')
                           .Where(x => !string.IsNullOrWhiteSpace(x))
                           .Select(relativePath => Path.Combine(tempFilesRoot, relativePath));
        }
    }
}