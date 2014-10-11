using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.SysModels
{
    public class ChangePassword
    {
        [DisplayName("原密码")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "请填写原密码")]
        public string OldPassword { get; set; }

        [DisplayName("新密码")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "请填写新密码")]
        public string NewPassword { get; set; }

        [DisplayName("重复密码")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "请填写重复密码")]
        [Compare("NewPassword", ErrorMessage = "两次输入的密码不同")]
        public string ReNewPassword { get; set; }

        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}