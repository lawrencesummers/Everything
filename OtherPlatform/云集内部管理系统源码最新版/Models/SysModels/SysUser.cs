using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Models.SysModels
{
    [Table("Sys_Users")]
    public class SysUser : DbSetBase
    {
        public SysUser()
        {
            Enabled = true;
            SysDepartmentSysUsers = new List<SysDepartmentSysUser>();
            SysRoleSysUsers = new List<SysRoleSysUser>();
        }

        [ForeignKey("EnterpriseId")]
        [ScaffoldColumn(false)]
        public virtual SysEnterprise SysEnterprise { get; set; }

        [MaxLength(50)]
        [MinLength(3)]
        [Required]
        public string UserName { get; set; }

        [MaxLength(50)]
        [Required]
        public string DisplayName { get; set; }

        [MaxLength(50)]
        [MinLength(5)]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [MaxLength(50)]
        [HiddenInput]
        public string OldPassword { get; set; }

        [DataType("File")]
        [Display(Name = "Head")]
        [MaxLength(200)]
        public string Picture { get; set; }

        [MaxLength(50)]
        public string MobilePhone { get; set; }

        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool Enabled { get; set; }

        [Display(Name = "Department")]
        [DataType("MultiSelectList")]
        public List<Guid> SysDepartmentsId { get; set; }

        public virtual ICollection<SysDepartmentSysUser> SysDepartmentSysUsers { get; set; }

        //角色
        [Display(Name = "Role")]
        [DataType("MultiSelectList")]
        public List<Guid> SysRolesId { get; set; }

        public virtual ICollection<SysRoleSysUser> SysRoleSysUsers { get; set; }

        //用户操作记录
        public virtual ICollection<SysUserLog> SysUserLogs { get; set; }

        //绑定Google账户
        public string GoogleUserName { get; set; }

        [DataType(DataType.Password)]
        public string GooglePassword { get; set; }
    }
}