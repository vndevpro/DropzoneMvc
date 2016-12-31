using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using log4net;

namespace GdNet.Integrations.DropzoneMvc.Controllers
{
    internal class DropzoneUploader
    {
        private readonly HttpRequestBase _request;
        protected readonly ILog Logger = LogManager.GetLogger(typeof(DropzoneUploader).FullName);

        public DropzoneUploader(HttpRequestBase request)
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
            var attachments = _request.Params["dropzone_files"];

            Logger.InfoFormat("dropzone_files is {0}", attachments);

            return
                attachments.Split(';')
                           .Where(x => !string.IsNullOrWhiteSpace(x))
                           .Select(relativePath => Path.Combine(rootFolder, relativePath));
        }

        public IEnumerable<string> UploadRequestFiles(string rootFolder, string temporaryFolder)
        {
            var targetFolder = Path.Combine(rootFolder, temporaryFolder);

            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }

            foreach (string fileName in _request.Files.AllKeys)
            {
                var file = _request.Files[fileName];
                if (file == null)
                {
                    continue;
                }

                var targetFilePath = CalculateUniqueFileName(targetFolder, file.FileName);

                using (var fw = new FileStream(targetFilePath, FileMode.Create))
                {
                    var fileAsBytes = new byte[file.ContentLength];
                    file.InputStream.Read(fileAsBytes, 0, file.ContentLength);
                    fw.Write(fileAsBytes, 0, fileAsBytes.Length);
                }

                Logger.InfoFormat("Uploaded file {0} to {1}", file.FileName, targetFolder);

                yield return targetFilePath.Substring(rootFolder.Length + 1);
            }
        }

        private string CalculateUniqueFileName(string folder, string fileName)
        {
            var targetFilePath = Path.Combine(folder, fileName);

            var count = 0;
            while (File.Exists(targetFilePath))
            {
                count += 1;
                targetFilePath = Path.Combine(folder, count + "_" + fileName);
            }

            return targetFilePath;
        }
    }
}