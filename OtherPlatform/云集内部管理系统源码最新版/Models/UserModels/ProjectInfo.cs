using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Models.UserModels
{
    public class ProjectInfo : DbSetBase
    {
        public ProjectInfo()
        {
            Public = true;
            Finish = false;
            StarTime = DateTime.Today;
            ProjectUsers = new List<ProjectUser>();
            LeaderUserId = new List<Guid>();
            Raty = 1;
         
        }


        [Display(Name = "Customer")]
        [ForeignKey("Customer")]

        public Guid? CustomerId { get; set; }

        [ScaffoldColumn(false)]
        public virtual Customer Customer { get; set; }

        [Display(Name = "ProjectInfoState")]

        [ForeignKey("ProjectInfoState")]
        public Guid? ProjectInfoStateId { get; set; }

        [ScaffoldColumn(false)]
        public virtual ProjectInfoState ProjectInfoState { get; set; }

        [ForeignKey("LastProjectInfo")]
        public Guid? LastProjectInfoId { get; set; }

        [ScaffoldColumn(false)]
        public virtual ProjectInfo LastProjectInfo { get; set; }

        [ScaffoldColumn(false)]
        public virtual ICollection<ProjectInfo> ProjectInfos { get; set; }

        [MaxLength(100)]
        [Required]
        public string ProjectName { get; set; } //项目名称

        [DataType("Raty")]
        public int Raty { get; set; } //星级

        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string ProjectObjective { get; set; } //项目目标

        [MaxLength(200)]
        public string Tag { get; set; } //项目标签

        //状态
        [Required]
        public bool Public { get; set; } //项目状态是否公开

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StarTime { get; set; } //项目开始日期

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? EndTime { get; set; } //项目结束日期

        [Required]
        public bool Finish { get; set; } //结束


        //项目负责人
        [Display(Name = "Leader")]
        [DataType("MultiSelectList")]
        public List<Guid> LeaderUserId { get; set; }

        //人员
        [Display(Name = "ProjectUsers")]
        [DataType("MultiSelectList")]
        public List<Guid> ProjectUsersId { get; set; }


        public virtual ICollection<ProjectUser> ProjectUsers { get; set; }

        //项目回复

        public virtual ICollection<ProjectInfoReply> ProjectInfoReplys { get; set; }

        //项目任务

        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }

        //计划

        public virtual ICollection<Plan> Plans { get; set; }

        //财务信息

        public virtual ICollection<ProjectFinancial> ProjectFinancials { get; set; }

        public virtual ICollection<ProjectFile> ProjectFiles { get; set; }
    }
}