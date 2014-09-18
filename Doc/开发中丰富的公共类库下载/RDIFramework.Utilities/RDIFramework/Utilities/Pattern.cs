namespace RDIFramework.Utilities
{
    using System;

    public enum Pattern : byte
    {
        ACCOUNT = 0,
        AREANAME = 0x24,
        BIGCHAR = 7,
        CHAR = 5,
        CHINESE = 0x12,
        DATETIME = 0x1c,
        DOUBLE_BYTE = 0x13,
        EMAIL = 2,
        EMAILS = 0x20,
        FILENAME = 0x1b,
        FLOAT = 13,
        GUDINGPHONE = 0x18,
        HTMLLABLE = 0x21,
        IDCARDNUMBER15 = 0x2a,
        IDCARDNUMBER18 = 0x29,
        INTEGER = 8,
        MOBILEPHONE = 0x17,
        NEGATIVE_FLOAT = 15,
        NEGATIVE_INTEGER = 10,
        NICKNAME = 0x1d,
        NONNEGATIVE_FLOAT = 0x11,
        NONNEGATIVE_INTEGER = 12,
        NONPOSITIVE_FLOAT = 0x10,
        NONPOSITIVE_INTEGER = 11,
        NUM_CHAR_UNDERLINE = 4,
        NUMBERWITHTOWPOINTS = 0x16,
        OUTDOOR = 40,
        PASSWORD = 1,
        POSITIVE_FLOAT = 14,
        POSITIVE_INTEGER = 9,
        PROJECTTAG = 0x22,
        QQ = 0x19,
        RESENDEMAIL = 20,
        SHAREID = 0x23,
        SIGHT = 0x26,
        SINGLEAREANAME = 0x27,
        SMALLCHAR = 6,
        SPACENAME = 0x1f,
        TRACKURL = 0x25,
        URL = 3,
        USERLOVE = 30,
        VALIDATECODE = 0x15,
        ZIP = 0x1a
    }
}

