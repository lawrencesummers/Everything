using System.ComponentModel.DataAnnotations;

namespace Models.UserModels
{
    public class Tag : DbSetBase
    {
        //标签

        [Required]
        [MaxLength(100)]
        public string TagType { get; set; } //标签分类

        [Required]
        [MaxLength(100)]
        public string TagName { get; set; } //标签名称
    }
}