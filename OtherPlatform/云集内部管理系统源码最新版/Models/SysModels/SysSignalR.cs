﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Models.SysModels
{
    public class SysSignalR
    {
        public SysSignalR()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }

        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        [MaxLength(100)]
        public string GroupId { get; set; }

        
        public Guid EnterpriseId { get; set; }



        [MaxLength(100)]
        public string GroupName { get; set; }

        public Guid UserId { get; set; } //发件人

        public Guid? UserId1 { get; set; } //收件人

        [MaxLength(100)]
        public string UserName { get; set; } //发件人

        [MaxLength(100)]
        public string UserName1 { get; set; } //收件人

        [MaxLength]
        public string Message { get; set; }
    }
}