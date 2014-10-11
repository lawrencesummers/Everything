using System;
using System.ComponentModel.DataAnnotations;

namespace Models.SysModels
{
    public class SysLog
    {
        public SysLog()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }

        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        [MaxLength(10)]
        public string Level { get; set; }

        [MaxLength(1000)]
        public string Message { get; set; }
    }
}