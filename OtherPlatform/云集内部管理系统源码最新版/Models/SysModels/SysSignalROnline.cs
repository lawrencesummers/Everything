using System;
using System.ComponentModel.DataAnnotations;

namespace Models.SysModels
{
    public class SysSignalROnline
    {
        public SysSignalROnline()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }

        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid UserId { get; set; }

        public Guid EnterpriseId { get; set; }

        [MaxLength(100)]
        public string ConnectionId { get; set; }

        [MaxLength(100)]
        public string GroupId { get; set; }
    }
}