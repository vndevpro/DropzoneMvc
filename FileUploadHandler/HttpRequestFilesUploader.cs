using System.Collections.Generic;
using System.IO;
using System.Web;

namespace FileUploadHandler
{
    public class HttpRequestFilesUploader
    {
        private readonly HttpRequestBase _request;
        private readonly IAttachmentSecurityService _securityService;

        public IDictionary<string, string> Status { get; private set; }

        /// <summary>
        /// Constructor with a request object
        /// </summary>
        /// <param name="request"></param>
        public HttpRequestFilesUploader(HttpRequestBase request) :
            this(request, new AllowAllAttachmentSecurityService())
        {
        }

        /// <summary>
        /// Constructor with a request object and custom security service
        /// </summary>
        /// <param name="request"></param>
        /// <param name="securityService"></param>
        public HttpRequestFilesUploader(HttpRequestBase request, IAttachmentSecurityService securityService)
        {
            _request = request;
            _securityService = securityService;
            Status = new Dictionary<string, string>();
        }

        /// <summary>
        /// Upload all files attached in the request
        /// </summary>
        /// <param name="rootFolder">The root folder to contain files</param>
        /// <param name="subFolder">The path to folder to hold files</param>
        /// <returns></returns>
        public IEnumerable<string> UploadRequestFiles(string rootFolder, string subFolder)
        {
            var targetFolder = Path.Combine(rootFolder, subFolder);

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

                if (!_securityService.Validate(fileName))
                {
                    Status.Add(fileName, $"Unable to upload because security validation failed");
                    continue;
                }

                var targetFilePath = CalculateUniqueFileName(targetFolder, file.FileName);

                using (var fw = new FileStream(targetFilePath, FileMode.Create))
                {
                    var fileAsBytes = new byte[file.ContentLength];
                    file.InputStream.Read(fileAsBytes, 0, file.ContentLength);
                    fw.Write(fileAsBytes, 0, fileAsBytes.Length);
                    fw.Flush();
                }

                Status.Add(fileName, $"Uploaded to {targetFolder}");

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

                var fileExt = Path.GetExtension(fileName);
                var newFile = fileName.Substring(0, fileName.Length - fileExt.Length) + "_" + count + fileExt;

                targetFilePath = Path.Combine(folder, newFile);
            }

            return targetFilePath;
        }
    }
}