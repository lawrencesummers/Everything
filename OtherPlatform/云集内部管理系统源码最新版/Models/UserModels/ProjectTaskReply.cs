using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.SysModels;

namespace Models.UserModels
{
    public class ProjectTaskReply : DbSetBase
    {
        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        public string ProjectTaskReplyObjective { get; set; } //项目任务目标


        //关联任务
        [ForeignKey("ProjectTask")]
        public Guid ProjectTaskId { get; set; }

        public virtual ProjectTask ProjectTask { get; set; }

        //发布人
        [ScaffoldColumn(false)]
        [ForeignKey("UserId")]
        public virtual SysUser SysUser { get; set; }

        [DataType("File")]
        [MaxLength(200)]
        public string FileUrl { get; set; } //附件
    }
}