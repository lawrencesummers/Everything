using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Models.SysModels;

namespace Models.UserModels
{
    public class Message : DbSetBase
    {
        //系统消息 内部
        public Message()
        {
            Read = false;
        }

        [ForeignKey("SysUser")]
        public Guid? SysUserId { get; set; } //收件人

        public virtual SysUser SysUser { get; set; }

        [MaxLength(200)]
        [Required]
        public string MessageTitle { get; set; } //消息标题

        [MaxLength]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string MessageContent { get; set; } //消息内容

        public bool Read { get; set; } // 是否读取
    }
}