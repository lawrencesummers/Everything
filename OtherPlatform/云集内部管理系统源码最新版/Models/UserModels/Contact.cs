using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.SysModels;

namespace Models.UserModels
{
    public class Contact : DbSetBase
    {
        //联系人

        [Display(Name = "Customer")]
        [ForeignKey("Customer")]
        
        public Guid? CustomerId { get; set; }

        [ScaffoldColumn(false)]
        public virtual Customer Customer { get; set; }

        [Required]
        [MaxLength(50)]
        public string ContactName { get; set; } //联系人姓名

        [ScaffoldColumn(false)]
        [MaxLength(100)]
        public string Pinyin { get; set; } //拼音

        [MaxLength(100)]
        public string Tag { get; set; } //联系人标签

        [MaxLength(50)]
        public string Position { get; set; } //职务

        [MaxLength(50)]
        public string Telephony { get; set; } //电话

        [MaxLength(50)]
        public string MobilePhone { get; set; } //手机

        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } //E-mail

        [MaxLength]
        [DataType("Extension")]
        public string Extension { get; set; } //扩展内容

        [ScaffoldColumn(false)]
        [ForeignKey("UserId")]
        public virtual SysUser SysUser { get; set; }
    }
}