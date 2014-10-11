using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.UserModels
{
    public class CustomerLevel : DbSetBase
    {
        public CustomerLevel()
        {
            SystemId = "000";
        }

        [MaxLength(40)]
        [Required]
        public string CustomerLevelName { get; set; }

        [MaxLength(30)]
        [Required]
        public string SystemId { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}