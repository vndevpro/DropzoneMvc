using GdNet.Integrations.DropzoneMvc.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace GdNet.Integrations.DropzoneMvc.Models
{
    public abstract class EditableEntityViewModelBase
    {
        public Guid Id { get; set; }

        [Display(Name = "CreatedAt", ResourceType = typeof(AttachmentResx))]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "CreatedBy", ResourceType = typeof(AttachmentResx))]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedBy", ResourceType = typeof(AttachmentResx))]
        public string LastModifiedBy { get; set; }

        [Display(Name = "LastModifiedAt", ResourceType = typeof(AttachmentResx))]
        public DateTime? LastModifiedAt { get; set; }

        [Display(Name = "State", ResourceType = typeof(AttachmentResx))]
        public string State { get; set; }

        [Display(Name = "Note", ResourceType = typeof(AttachmentResx))]
        public string Note { get; set; }
    }
}
