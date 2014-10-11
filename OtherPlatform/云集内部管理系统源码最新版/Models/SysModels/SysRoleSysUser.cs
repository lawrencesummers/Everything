using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.SysModels
{
    public class SysRoleSysUser : DbSetBase
    {
        [ForeignKey("SysUser")]
        public Guid SysUserId { get; set; }

        public virtual SysUser SysUser { get; set; }

        [ForeignKey("SysRole")]
        public Guid SysRoleId { get; set; }

        public virtual SysRole SysRole { get; set; }
    }
}