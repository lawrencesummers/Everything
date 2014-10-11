using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Models.SysModels;

namespace Models.UserModels
{
    public class Knowledge : DbSetBase
    {
        public Knowledge()
        {
            Public = true;
        }

        [MaxLength(100)]
        [AdditionalMetadata("Size", 50)]
        [Required]
        public string KnowledgeTitle { get; set; } //知识库标题

        [MaxLength]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string KnowledgeContent { get; set; } //知识库内容

        //状态
        [Required]
        public bool Public { get; set; } //项目状态是否公开

        [ScaffoldColumn(false)]
        [ForeignKey("UserId")]
        public virtual SysUser SysUser { get; set; }


        [ScaffoldColumn(false)]
        public virtual ICollection<KnowledgeReply> KnowledgeReplys { get; set; }
    }
}