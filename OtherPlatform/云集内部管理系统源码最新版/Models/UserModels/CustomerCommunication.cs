using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.SysModels;

namespace Models.UserModels
{
    public enum CommunicateResults
    {
        跟进,
        成功,
        失败
    }

    public class CustomerCommunication : DbSetBase
    {
        //企业
        //沟通内容
        public CustomerCommunication()
        {
            Amount = 0;
        }

        [Display(Name = "Customer")]
        [ForeignKey("Customer")]

        public Guid? CustomerId { get; set; }

        [ScaffoldColumn(false)]
        public virtual Customer Customer { get; set; }

        [MaxLength(100)]
        public string Contact { get; set; }

        [MaxLength]
        [DataType(DataType.MultilineText)]
        public string CommunicationContent { get; set; } //沟通内容

        public CommunicateResults CommunicateResult { get; set; }//沟通结果

        public int Amount { get; set; }//金额 

        [ScaffoldColumn(false)]
        [ForeignKey("UserId")]
        public virtual SysUser SysUser { get; set; }
    }
}