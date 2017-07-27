namespace GdNet.Integrations.DropzoneMvc.Services
{
    public interface IDropzoneAttachmentSecurityCheck
    {
        /// <summary>
        /// Validate if the current user can post files or not
        /// </summary>
        bool ValidatePermission();
    }
}