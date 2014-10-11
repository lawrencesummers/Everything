using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.UserModels
{
    public class ProjectInfoState : DbSetBase
    {
        public ProjectInfoState()
        {
            SystemId = "000";
        }

        [MaxLength(40)]
        [Required]
        public string ProjectInfoStateName { get; set; }

        [MaxLength(30)]
        [Required]
        public string SystemId { get; set; }

        public virtual ICollection<ProjectInfo> ProjectInfos { get; set; }
    }
}