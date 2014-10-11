using System;
using System.ComponentModel.DataAnnotations.Schema;
using Models.SysModels;

namespace Models.UserModels
{
    public class ProjectUser : DbSetBase
    {
        public ProjectUser()
        {
            Leader = false;
            Follow = false;
        }

        [ForeignKey("ProjectInfo")]
        public Guid ProjectInfoId { get; set; }

        public virtual ProjectInfo ProjectInfo { get; set; }


        [ForeignKey("SysUser")]
        public Guid SysUserId { get; set; }

        public virtual SysUser SysUser { get; set; }

        public bool Leader { get; set; } //项目负责人

        public bool Follow { get; set; } //关注
    }
}