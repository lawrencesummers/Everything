namespace RDIFramework.Utilities
{
    using System;

    public class RegexPatternHelper
    {
        public const string Chinese = @"^[\u4e00-\u9fa5]+$";
        public const string Date = @"^[\d]{4}(-|/)[\d]{1,2}(-|/)[\d]{1,2}$";
        public const string Email = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        public const string Float = @"^(-?\d+)(\.\d+)?$";
        public const string HTMLTag = "<.+?>";
        public const string IDCardNumber15 = @"^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$";
        public const string IDCardNumber18 = @"^\d{15}|\d{17}[X|x]$";
        public const string Integer = @"^-?\d{1,9}$";
        public const string string_0 = @"\b((25[0-5]|2[0-4]\d|[01]\d\d|\d?\d)\.){3}(25[0-5]|2[0-4]\d|[01]\d\d|\d?\d)\b$";
        public const string URL = @"^http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
        public const string UserName = @"^[A-Za-z\d\-_]+$";
    }
}

