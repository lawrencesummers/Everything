using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.SysModels;

namespace Models.UserModels
{
    public class KnowledgeReply : DbSetBase
    {
        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        public string KnowledgeReplyObjective { get; set; } //回复内容


        //关联项目
        [ForeignKey("Knowledge")]
        public Guid KnowledgeId { get; set; }

        public virtual Knowledge Knowledge { get; set; }

        //发布人
        [ScaffoldColumn(false)]
        [ForeignKey("UserId")]
        public virtual SysUser SysUser { get; set; }
    }
}