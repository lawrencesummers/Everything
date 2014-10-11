using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.SysModels
{
    public class SysDepartmentSysUser : DbSetBase
    {
        [ForeignKey("SysUser")]
        public Guid SysUserId { get; set; }

        public virtual SysUser SysUser { get; set; }

        [ForeignKey("SysDepartment")]
        public Guid SysDepartmentId { get; set; }

        public virtual SysDepartment SysDepartment { get; set; }
    }
}