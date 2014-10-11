using System.ComponentModel.DataAnnotations;

namespace Models.SysModels
{
    public class SysUploadFile : DbSetBase
    {
        //上传的文件管理

        [MaxLength(100)]
        [Required]
        public string FileName { get; set; }

        //文件名 

        [Required]
        public int FileSize { get; set; }

        //大小 

        [MaxLength(50)]
        [Required]
        public string FileType { get; set; }

        //类型

        [MaxLength(200)]
        [Required]
        public string FileUrl { get; set; }

        //存储路径
    }
}