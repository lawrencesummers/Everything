using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.UserModels
{
    public class BusinessState : DbSetBase
    {
        public BusinessState()
        {
            SystemId = "000";
            Statistics = false;
        }

        [MaxLength(40)]
        [Required]
        public string BusinessStateName { get; set; }

        [MaxLength(30)]
        [Required]
        public string SystemId { get; set; }

        public bool Statistics { get; set; } //是否参与统计


        public virtual ICollection<Customer> Customers { get; set; }
    }
}