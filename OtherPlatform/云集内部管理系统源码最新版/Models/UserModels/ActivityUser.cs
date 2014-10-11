using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.SysModels;

namespace Models.UserModels
{
    public class ActivityUser : DbSetBase
    {

        //关联项目
        [ForeignKey("Activity")]
        public Guid ActivityId { get; set; }

        public virtual Activity Activity { get; set; }

        //发布人
        [ScaffoldColumn(false)]
        [ForeignKey("UserId")]
        public virtual SysUser SysUser { get; set; }
    }
}