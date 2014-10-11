using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.SysModels
{
    public class SysRoleSysControllerSysAction : DbSetBase
    {
        [ForeignKey("SysRole")]
        public Guid SysRoleId { get; set; }

        public virtual SysRole SysRole { get; set; }

        [ForeignKey("SysControllerSysAction")]
        public Guid SysControllerSysActionId { get; set; }

        public virtual SysControllerSysAction SysControllerSysAction { get; set; }
    }
}