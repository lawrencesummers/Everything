using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Models.SysModels
{
    public class SysMail : DbSetBase
    {
        public SysMail()
        {
            RecordId = Guid.NewGuid();
            Sent = false;
        }

        [ScaffoldColumn(false)]
        [Required]
        public Guid RecordId { get; set; }

        [AdditionalMetadata("Size", 50)]
        [MaxLength(100)]
        [Required]
        public string To { get; set; }

        [AdditionalMetadata("Size", 50)]
        [MaxLength(100)]
        [Required]
        public string Subject { get; set; }

        [MaxLength]
        [DataType(DataType.Html)]
        [Required]
        [AllowHtml]
        public string Body { get; set; }

        public bool Sent { get; set; }
    }
}