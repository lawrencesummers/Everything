using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.UserModels
{
    public class CustomerType : DbSetBase
    {
        public CustomerType()
        {
            SystemId = "000";
        }

        [MaxLength(40)]
        [Required]
        public string CustomerTypeName { get; set; }

        [MaxLength(30)]
        [Required]
        public string SystemId { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}