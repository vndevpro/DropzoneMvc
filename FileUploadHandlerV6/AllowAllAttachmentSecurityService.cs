namespace FileUploadHandler
{
    public class AllowAllAttachmentSecurityService : IAttachmentSecurityService
    {
        public bool Validate(string fileName)
        {
            return true;
        }
    }
}