using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Models.SysModels;

namespace Models.UserModels
{
    public class Activity : DbSetBase
    {
        //活动管理
        //

        public Activity()
        {
            ActivityStartDateTime = DateTime.Now.AddDays(1);
            ActivityDeadline = DateTime.Now.AddDays(7);
        }

        [MaxLength(100)]
        [AdditionalMetadata("Size", 100)]
        [Required]
        public string ActivityTitle { get; set; } //活动标题

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ActivityStartDateTime { get; set; }//活动开始时间

        [MaxLength(100)]
        [AdditionalMetadata("Size", 100)]
        [Required]
        public string ActivitySite { get; set; }//活动地点

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ActivityDeadline { get; set; }//活动截至日期

        [MaxLength]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string ActivityContent { get; set; } //活动内容

        //[ForeignKey("SysDepartment")]
        //public Guid SysDepartmentId { get; set; }

        //public virtual SysDepartment SysDepartment { get; set; }//活动可以报名的部门

        [ScaffoldColumn(false)]
        [ForeignKey("UserId")]
        public virtual SysUser SysUser { get; set; }

        [ScaffoldColumn(false)]
        public virtual ICollection<ActivityUser> ActivityUsers { get; set; }
    }
}