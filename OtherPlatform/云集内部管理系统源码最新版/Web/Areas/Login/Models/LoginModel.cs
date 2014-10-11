using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Login.Models
{
    public class LoginModel
    {
        [Display(Name = "UserName")]   
        [Required(ErrorMessage="请输入用户名")]
        public string UserName { get; set; }

        [Display(Name = "Password")]   
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }

        [Display(Name = "Enterprise")]
        public Guid EnterpriseId { get; set; }

        [Display(Name = "Remember")]
        public bool Remember { get; set; }
    }
}