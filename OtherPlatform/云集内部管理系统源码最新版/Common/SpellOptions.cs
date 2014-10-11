

namespace Common
{
    /// <summary>
    /// 提供用于设置转换选项的枚举值。
    /// </summary>
    /// <value>
    /// <code>FirstLetterOnly</code>
    /// 只转换拼音首字母，默认转换全部
    /// </value>
    /// <value>
    /// <code>TranslateUnknowWordToInterrogation</code>
    /// 转换未知汉字为问号，默认不转换
    /// </value>
    /// <value>
    /// <code>EnableUnicodeLetter</code>
    /// 保留非字母、非数字字符，默认不保留
    /// </value>
    [System.FlagsAttribute]
    public enum SpellOptions
    {
        FirstLetterOnly = 1,													//只转换拼音首字母，默认转换全部
        TranslateUnknowWordToInterrogation = 1 << 1,		//转换未知汉字为问号，默认不转换
        EnableUnicodeLetter = 1 << 2,								//保留非字母、非数字字符，默认不保留
    }

}
