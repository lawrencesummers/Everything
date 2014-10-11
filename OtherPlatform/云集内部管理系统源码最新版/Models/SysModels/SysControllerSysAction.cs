using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.SysModels
{
    public class SysControllerSysAction : DbSetBase
    {
        [ForeignKey("SysController")]
        public Guid SysControllerId { get; set; }

        public virtual SysController SysController { get; set; }


        [ForeignKey("SysAction")]
        public Guid SysActionId { get; set; }

        public virtual SysAction SysAction { get; set; }

        public virtual ICollection<SysRoleSysControllerSysAction> SysRoleSysControllerSysActions { get; set; }
    }
}