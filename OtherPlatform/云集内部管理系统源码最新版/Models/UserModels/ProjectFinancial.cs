using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.SysModels;

namespace Models.UserModels
{
    public class ProjectFinancial : DbSetBase
    {
        //财务信息
        public ProjectFinancial()
        {
            Raty = 1;
            AccountReceivableDate = DateTime.Now;
            Invoice = false;
            Finish = false;
        }

        [DataType("Raty")]
        public int Raty { get; set; } //星级

        //关联项目
        [Display(Name = "ProjectInfo")]
        [ForeignKey("ProjectInfo")]
        
        public Guid ProjectInfoId { get; set; }

        [ScaffoldColumn(false)]
        public virtual ProjectInfo ProjectInfo { get; set; }

        public decimal AccountReceivable { get; set; } //应收账款

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AccountReceivableDate { get; set; } //应收日期

        public decimal PaidIn { get; set; } //实收

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PaidInDate { get; set; } //实收日期

        public bool Invoice { get; set; } //已开发票

        public bool Finish { get; set; } //是否完成收款 应对付款不全的情况

        [MaxLength(20)]
        public string InvoiceType { get; set; } //发票类型

        //发布人
        [ScaffoldColumn(false)]
        [ForeignKey("UserId")]
        public virtual SysUser SysUser { get; set; }
    }
}