using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.SysModels
{
    public class SysEnterprise : DbSetBase
    {
        public SysEnterprise()
        {
            Enabled = true;
            MaxUser = 5;
            Validity = DateTime.Now.AddYears(1);
        }

        [MaxLength(50)]
        [Required]
        public string EnterpriseName { get; set; }


        [MaxLength(50)]
        public string EnterpriseShortName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MaxUser { get; set; }

        [MaxLength(50)]
        public string Host { get; set; }

        [Required]
        public DateTime Validity { get; set; }

        [Required]
        public bool Enabled { get; set; }

        public virtual ICollection<SysUser> SysUser { get; set; }
    }
}