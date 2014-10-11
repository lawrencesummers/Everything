using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.SysModels
{
    public class SysArea : DbSetBase
    {
        public SysArea()
        {
            SystemId = "000";
        }

        [MaxLength(40)]
        [Required]
        public string AreaDisplayName { get; set; }

        [MaxLength(40)]
        [Required]
        public string AreaName { get; set; }

        [MaxLength(30)]
        [Required]
        public string SystemId { get; set; }

        public virtual ICollection<SysController> SysControllers { get; set; }
    }
}