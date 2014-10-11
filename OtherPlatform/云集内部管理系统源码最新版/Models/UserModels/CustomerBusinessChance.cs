using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.UserModels
{
    public class CustomerBusinessChance : DbSetBase
    {
        //业务


        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; } //业务机会 多选


        public Guid BusinessChanceId { get; set; }

        [ForeignKey("BusinessChanceId")]
        public virtual BusinessChance BusinessChance { get; set; } //业务机会 多选
    }
}