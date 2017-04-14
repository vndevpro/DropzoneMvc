namespace GdNet.Integrations.DropzoneMvc.Models
{
    public class AttachmentViewModel : EditableEntityViewModelBase
    {
        public string Name { get; set; }

        public string Extension { get; set; }

        public string ContentType { get; set; }

        public long Size { get; set; }

        public string Icon
        {
            get
            {
                return Extension.Substring(1);
            }
        }
    }
}
