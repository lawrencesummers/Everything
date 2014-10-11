using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Models.SysModels
{
    public class SysDepartment : DbSetBase
    {
        public SysDepartment()
        {
            DepartmentCode = "000";
            SystemId = "000";
            Enabled = true;
        }

        [MaxLength(40)]
        [AdditionalMetadata("Size", 40)]
        [Required]
        public string DepartmentName { get; set; }

        [MaxLength(50)]
        [Required]
        public string DepartmentCode { get; set; }

        [MaxLength(50)]
        [Required]
        public string SystemId { get; set; }

        public bool Enabled { get; set; }

        [DataType("Ico")]
        public string Ico { get; set; }

        public virtual ICollection<SysDepartmentSysUser> SysDepartmentSysUsers { get; set; }
    }
}