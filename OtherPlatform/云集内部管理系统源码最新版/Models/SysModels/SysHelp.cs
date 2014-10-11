using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Models.SysModels
{
    public class SysHelp : DbSetBase
    {
        public SysHelp()
        {
            Sort = 0;
        }

        [MaxLength(100)]
        [AdditionalMetadata("Size", 50)]
        [Required]
        public string Title { get; set; }

        [MaxLength]
        [DataType(DataType.Html)]
        [Required]
        [AllowHtml]
        public string Content { get; set; }

        public int Sort { get; set; }

        [DataType("File")]
        [MaxLength(200)]
        public string FileUrl { get; set; }
    }
}