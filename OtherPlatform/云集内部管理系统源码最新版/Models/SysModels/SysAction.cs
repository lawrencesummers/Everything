using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.SysModels
{
    public class SysAction : DbSetBase
    {
        public SysAction()
        {
            SystemId = "000";
        }

        [MaxLength(40)]
        [Required]
        public string ActionDisplayName { get; set; }

        [MaxLength(40)]
        [Required]
        public string ActionName { get; set; }

        [MaxLength(50)]
        [Required]
        public string SystemId { get; set; }

        public virtual ICollection<SysControllerSysAction> SysControllerSysActions { get; set; }
    }
}