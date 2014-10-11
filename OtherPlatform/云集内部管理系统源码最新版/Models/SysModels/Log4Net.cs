using System;
using System.ComponentModel.DataAnnotations;

namespace Models.SysModels
{
    public class Log4Net
    {
        public Log4Net()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        [MaxLength(255)]
        public string Thread { get; set; }

        [MaxLength(10)]
        public string Level { get; set; }

        [MaxLength(1000)]
        public string Logger { get; set; }

        [MaxLength(4000)]
        public string Message { get; set; }

        [MaxLength(4000)]
        public string Exception { get; set; }
    }
}