using System;

namespace GdNet.Integrations.DropzoneMvc.Models
{
    public class DropzoneComponentViewModel
    {
        public DropzoneComponentViewModel()
        {
            ComponentId = Guid.NewGuid();
        }

        public Guid ComponentId { get; set; }

        public string ElementId { get; set; }

        public string TargetAction { get; set; }
    }
}