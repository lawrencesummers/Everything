using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.SysModels;

namespace Models.UserModels
{
    public class ProjectInfoReply : DbSetBase
    {
        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        public string ProjectInfoReplyObjective { get; set; } //回复内容


        //关联项目
        [ForeignKey("ProjectInfo")]
        public Guid ProjectInfoId { get; set; }

        public virtual ProjectInfo ProjectInfo { get; set; }

        //发布人
        [ScaffoldColumn(false)]
        [ForeignKey("UserId")]
        public virtual SysUser SysUser { get; set; }
    }
}