using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.SysModels
{
    public class SysUserResetPassword : DbSetBase
    {
        public SysUserResetPassword()
        {
            Used = false;
        }

        [MaxLength(100)]
        [Required]
        public string Key { get; set; }

        [ForeignKey("SysUser")]
        public Guid SysUserId { get; set; }

        public virtual SysUser SysUser { get; set; }

        public bool Used { get; set; }
    }
}