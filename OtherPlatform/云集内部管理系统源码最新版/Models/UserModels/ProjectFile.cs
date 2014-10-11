using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.SysModels;

namespace Models.UserModels
{
    public class ProjectFile : DbSetBase
    {
        public ProjectFile()
        {

        }

        //项目 所在项目 可有可无
        [Display(Name = "ProjectInfo")]
        [ForeignKey("ProjectInfo")]
        public Guid ProjectInfoId { get; set; }

        public virtual ProjectInfo ProjectInfo { get; set; }

        [MaxLength(100)]
        [Required]
        public string ProjectFileName { get; set; }

        [DataType("File")]
        [MaxLength(200)]
        public string FileUrl { get; set; }

        //发布人
        [ScaffoldColumn(false)]
        [ForeignKey("UserId")]
        public virtual SysUser SysUser { get; set; }

    }
}