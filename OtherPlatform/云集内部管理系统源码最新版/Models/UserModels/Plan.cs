using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.SysModels;

namespace Models.UserModels
{
    public static class PlanTypes
    {
        public const string Day = "Day";
        public const string Week = "Week";
        public const string Month = "Month";
        public const string Year = "Year";
    }

    public class Plan : DbSetBase
    {
        public Plan()
        {
            Finish = false;
            FinishTime = DateTime.Now;
            Raty = 1;
            Milestone = false;
            PlanType = PlanTypes.Day;
        }


        [MaxLength(10)]
        public string PlanType { get; set; } //计划类型 日计划 Day 周计划 Week 月计划 Month 年计划 Year

        [MaxLength(200)]
        [Required]
        public string PlanTitle { get; set; } //计划标题

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public bool Finish { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime FinishTime { get; set; }

        //项目 所在项目 可有可无
        [Display(Name = "ProjectInfo")]
        [ForeignKey("ProjectInfo")]
        
        public Guid? ProjectInfoId { get; set; }

        [ScaffoldColumn(false)]
        public virtual ProjectInfo ProjectInfo { get; set; }

        [DataType("Raty")]
        public int Raty { get; set; }

        public bool Milestone { get; set; } //里程碑

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? RemindTime { get; set; } //提醒时间

        [ScaffoldColumn(false)]
        [ForeignKey("UserId")]
        public virtual SysUser SysUser { get; set; }

        [MaxLength(50)]
        public string GoogleEventEntryId { get; set; }
    }
}