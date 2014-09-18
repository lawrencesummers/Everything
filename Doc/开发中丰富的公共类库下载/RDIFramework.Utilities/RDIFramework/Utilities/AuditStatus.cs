namespace RDIFramework.Utilities
{
    using System;

    public enum AuditStatus
    {
        [EnumDescription("完成")]
        AuditComplete = 6,
        [EnumDescription("通过")]
        AuditPass = 2,
        [EnumDescription("废弃")]
        AuditQuash = 7,
        [EnumDescription("退回")]
        AuditReject = 5,
        [EnumDescription("草稿")]
        Draft = 0,
        [EnumDescription("送审")]
        StartAudit = 1,
        [EnumDescription("转发")]
        Transmit = 4,
        [EnumDescription("待审")]
        WaitForAudit = 3
    }
}

