using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.SysModels
{
    public class SysUserLog : DbSetBase
    {
        [ForeignKey("SysControllerSysAction")]
        public Guid SysControllerSysActionId { get; set; }

        public virtual SysControllerSysAction SysControllerSysAction { get; set; }

        public string RecordId { get; set; }

        [ForeignKey("SysUser")]
        public Guid SysUserId { get; set; }

        public virtual SysUser SysUser { get; set; }

        [MaxLength(100)]
        [Required]
        public string Ip { get; set; }
    }
}