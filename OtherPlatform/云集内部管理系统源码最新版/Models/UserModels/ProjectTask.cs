using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Models.SysModels;

namespace Models.UserModels
{
    public class ProjectTask : DbSetBase
    {
        public ProjectTask()
        {
            Finish = false;
            FinishTime = DateTime.Now;
            BeginTime = DateTime.Now;
            EndTime = DateTime.Now.AddHours(1);
            Milestone = false;
            AcceptTime = DateTime.Now;
        }

        [MaxLength(100)]
        [Required]
        public string ProjectTaskName { get; set; } //项目任务名称

        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string ProjectTaskObjective { get; set; } //项目任务目标

        //时间
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime BeginTime { get; set; } //开始时间

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; } //结束时间

        //状态
        public bool Finish { get; set; } //是否完成

        public bool Milestone { get; set; } //里程碑

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime FinishTime { get; set; } //完成时间

        //项目 所在项目 可有可无
        [Display(Name = "ProjectInfo")]
        [ForeignKey("ProjectInfo")]
        
        public Guid? ProjectInfoId { get; set; }

        public virtual ProjectInfo ProjectInfo { get; set; }

        //执行该任务的人员
        [Display(Name = "User")]
        [ForeignKey("SysUser")]
        public Guid? SysUserId { get; set; }

        public virtual SysUser SysUser { get; set; }

        //任务回复
        public virtual ICollection<ProjectTaskReply> ProjectTaskReplys { get; set; }

        [DataType("File")]
        [MaxLength(200)]
        public string FileUrl { get; set; }

        public bool? Accept { get; set; } //是否接受任务

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime AcceptTime { get; set; } //处理时间

        [ScaffoldColumn(false)]
        [ForeignKey("UserId")]
        public virtual SysUser SendUser { get; set; } //发布人

        [MaxLength(50)]
        public string GoogleEventEntryId { get; set; }
    }
}