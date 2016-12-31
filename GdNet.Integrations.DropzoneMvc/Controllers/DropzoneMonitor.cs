using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using log4net;

namespace GdNet.Integrations.DropzoneMvc.Controllers
{
    public sealed class DropzoneMonitor
    {
        private readonly HttpRequestBase _request;
        private readonly ILog _logger = LogManager.GetLogger(typeof(DropzoneMonitor).FullName);

        public DropzoneMonitor(HttpRequestBase request)
        {
            _request = request;
        }

        /// <summary>
        /// Calculate the absolute paths of uploaded files
        /// </summary>
        /// <param name="rootFolder"></param>
        /// <returns></returns>
        public IEnumerable<string> GetActiveUploadedFiles(string rootFolder)
        {
            var attachments = _request.Form["dropzone_files"];

            _logger.InfoFormat("dropzone_files is {0}", attachments);

            return
                attachments.Split(';')
                           .Where(x => !string.IsNullOrWhiteSpace(x))
                           .Select(relativePath => Path.Combine(rootFolder, relativePath));
        }
    }
}