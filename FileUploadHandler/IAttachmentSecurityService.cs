namespace FileUploadHandler
{
    public interface IAttachmentSecurityService
    {
        /// <summary>
        /// Validate if the current user can post files or not
        /// </summary>
        bool Validate(string fileName);
    }
}