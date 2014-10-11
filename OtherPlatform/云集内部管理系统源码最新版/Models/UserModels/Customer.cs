using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.SysModels;

namespace Models.UserModels
{
    public class Customer : DbSetBase
    {
        //客户
        public Customer()
        {
            CustomerBusinessChances = new List<CustomerBusinessChance>();
        }

        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; } //客户名称

        [Display(Name = "CustomerType")]
        
        public Guid CustomerTypeId { get; set; } //客户类别

        [ForeignKey("CustomerTypeId")]
        [ScaffoldColumn(false)]
        public virtual CustomerType CustomerType { get; set; } //客户类别

        [Display(Name = "CustomerLevel")]
        
        public Guid CustomerLevelId { get; set; } //客户等级

        [ForeignKey("CustomerLevelId")]
        [ScaffoldColumn(false)]
        public virtual CustomerLevel CustomerLevel { get; set; } //客户等级

        [MaxLength(100)]
        public string Address { get; set; } //地址

        [MaxLength(10)]
        public string Postcode { get; set; } //邮编

        [MaxLength(50)]
        public string Telephony { get; set; } //电话

        [MaxLength(50)]
        public string Fax { get; set; } //传真

        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } //E-mail

        [MaxLength(50)]
        [DataType(DataType.Url)]
        public string Url { get; set; } //网址

        
        [Display(Name = "Leader")]
        public Guid LeaderId { get; set; } //项目负责人

        [ScaffoldColumn(false)]
        [ForeignKey("LeaderId")]
        public virtual SysUser Leader { get; set; }

        [DataType("MultiSelectList")]
        [Display(Name = "BusinessChance")]
        public List<Guid> BusinessChancesId { get; set; }

        public virtual ICollection<CustomerBusinessChance> CustomerBusinessChances { get; set; } //业务机会 多选

        [ForeignKey("BusinessState")]
        
        [Display(Name = "BusinessState")]
        public Guid BusinessStateId { get; set; } //业务状态

        [ScaffoldColumn(false)]
        public virtual BusinessState BusinessState { get; set; } //业务状态

    
        [MaxLength]
        [DataType("Extension")]
        public string Extension { get; set; } //扩展内容

        [ScaffoldColumn(false)]
        [ForeignKey("UserId")]
        public virtual SysUser SysUser { get; set; }

        //联系人
        public virtual ICollection<Contact> Contacts { get; set; }

        //项目
        public virtual ICollection<ProjectInfo> ProjectInfos { get; set; }
    }
}