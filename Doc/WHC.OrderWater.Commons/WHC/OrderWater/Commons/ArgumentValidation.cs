namespace WHC.OrderWater.Commons
{
    using System;

    public sealed class ArgumentValidation
    {
        private const string string_0 = "参数 '{0}'的值不能为空字符串。";
        private const string string_1 = "参数'{0}'的名称不能为空引用或空字符串。";
        private const string string_2 = "数值必须大于0字节.";
        private const string string_3 = "无效的类型，期待的类型必须为'{0}'。";
        private const string string_4 = "{0}不是{1}的一个有效值";

        private ArgumentValidation()
        {
        }

        public static void CheckEnumeration(Type enumType, object variable, string variableName)
        {
            CheckForNullReference(variable, "variable");
            CheckForNullReference(enumType, "enumType");
            CheckForNullReference(variableName, "variableName");
            if (!Enum.IsDefined(enumType, variable))
            {
                throw new ArgumentException(string.Format("{0}不是{1}的一个有效值", variable.ToString(), enumType.FullName, variableName));
            }
        }

        public static void CheckExpectedType(object variable, Type type)
        {
            CheckForNullReference(variable, "variable");
            CheckForNullReference(type, "type");
            if (!type.IsAssignableFrom(variable.GetType()))
            {
                throw new ArgumentException(string.Format("无效的类型，期待的类型必须为'{0}'。", type.FullName));
            }
        }

        public static void CheckForEmptyString(string variable, string variableName)
        {
            CheckForNullReference(variable, variableName);
            CheckForNullReference(variableName, "variableName");
            if (variable.Length == 0)
            {
                throw new ArgumentException(string.Format("参数 '{0}'的值不能为空字符串。", variableName));
            }
        }

        public static void CheckForInvalidNullNameReference(string name, string messageName)
        {
            if ((name == null) || (name.Length == 0))
            {
                throw new InvalidOperationException(string.Format("参数'{0}'的名称不能为空引用或空字符串。", messageName));
            }
        }

        public static void CheckForNullReference(object variable, string variableName)
        {
            if (variableName == null)
            {
                throw new ArgumentNullException("variableName");
            }
            if (null == variable)
            {
                throw new ArgumentNullException(variableName);
            }
        }

        public static void CheckForZeroBytes(byte[] bytes, string variableName)
        {
            CheckForNullReference(bytes, "bytes");
            CheckForNullReference(variableName, "variableName");
            if (bytes.Length == 0)
            {
                throw new ArgumentException(string.Format("数值必须大于0字节.", variableName));
            }
        }
    }
}

