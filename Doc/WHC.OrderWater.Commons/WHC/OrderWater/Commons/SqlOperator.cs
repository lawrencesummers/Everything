namespace WHC.OrderWater.Commons
{
    using System;
    using System.ComponentModel;

    public enum SqlOperator
    {
        [Description("＝ 等于号")]
        Equal = 3,
        [Description("在某个字符串值中")]
        In = 9,
        [Description("＜小于号")]
        LessThan = 6,
        [Description("≤ 小于或等于号")]
        LessThanOrEqual = 8,
        [Description("Like 模糊查询")]
        Like = 0,
        [Description("Like 开始匹配模糊查询，如Like 'ABC%'")]
        LikeStartAt = 2,
        [Description("＞ 大于号")]
        MoreThan = 5,
        [Description("≥大于或等于号 ")]
        MoreThanOrEqual = 7,
        [Description("<> (≠) 不等于号")]
        NotEqual = 4,
        [Description("Not LiKE 模糊查询")]
        NotLike = 1
    }
}

