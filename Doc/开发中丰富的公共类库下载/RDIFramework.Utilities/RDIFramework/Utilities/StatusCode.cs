namespace RDIFramework.Utilities
{
    using System;

    public enum StatusCode
    {
        CanNotLock = 12,
        ChangePasswordOK = 0x24,
        [EnumDescription("数据库连接错误")]
        DbError = 0,
        Error = 9,
        ErrorChanged = 0x18,
        ErrorCodeExist = 0x11,
        ErrorDataRelated = 0x16,
        ErrorDeleted = 0x17,
        ErrorIPAddress = 0x1f,
        ErrorLogOn = 0x29,
        ErrorMacAddress = 30,
        ErrorNameExist = 0x12,
        ErrorOnLine = 0x1d,
        ErrorOnLineLimit = 0x20,
        ErrorUserExist = 20,
        ErrorValueExist = 0x13,
        Exist = 0x10,
        LockOK = 13,
        LogOnDeny = 0x1c,
        NotFound = 0x19,
        OK = 10,
        OKAdd = 11,
        OKDelete = 15,
        OKUpdate = 14,
        OldPasswordError = 0x23,
        PasswordCanNotBeNull = 0x21,
        PasswordError = 0x1b,
        SetPasswordOK = 0x22,
        UserDuplicate = 0x2b,
        UserIsActivate = 40,
        UserLocked = 0x26,
        UserNotActive = 0x27,
        UserNotEmail = 0x25,
        UserNotFound = 0x1a,
        WaitForAudit = 0x2a
    }
}

