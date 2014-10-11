using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.SysModels
{
    public class SysRole : DbSetBase
    {
        public SysRole()
        {
            SystemId = "000";
        }

        [MaxLength(50)]
        [Required]
        public string RoleName { get; set; }

        [MaxLength(50)]
        [Required]
        public string SystemId { get; set; }

        //用户多对多关系表
        public virtual ICollection<SysRoleSysUser> SysRoleSysUsers { get; set; }

        public virtual ICollection<SysRoleSysControllerSysAction> SysRoleSysControllerSysActions { get; set; }
    }
}