namespace GdNet.Integrations.DropzoneMvc.Services
{
    public class AllowAllDropzoneAttachmentSecurityCheck : IDropzoneAttachmentSecurityCheck
    {
        public bool ValidatePermission()
        {
            return true;
        }
    }
}