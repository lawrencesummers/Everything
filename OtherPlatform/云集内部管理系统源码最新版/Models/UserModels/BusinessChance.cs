using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.UserModels
{
    public class BusinessChance : DbSetBase
    {
        //业务机会
        public BusinessChance()
        {
            SystemId = "000";
            Disable = false;
        }

        [MaxLength(40)]
        [Required]
        public string BusinessChanceName { get; set; }

        [MaxLength(30)]
        [Required]
        public string SystemId { get; set; }

        public bool Disable { get; set; }

        public virtual ICollection<CustomerBusinessChance> CustomerBusinessChances { get; set; }
    }
}