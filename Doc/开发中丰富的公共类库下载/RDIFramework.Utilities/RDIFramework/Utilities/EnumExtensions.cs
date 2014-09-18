namespace RDIFramework.Utilities
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public static class EnumExtensions
    {
        public static string ToDescription(this Enum enumeration)
        {
            MemberInfo[] member = enumeration.GetType().GetMember(enumeration.ToString());
            if ((member != null) && (member.Length > 0))
            {
                object[] customAttributes = member[0].GetCustomAttributes(typeof(EnumDescription), false);
                if ((customAttributes != null) && (customAttributes.Length > 0))
                {
                    return ((EnumDescription) customAttributes[0]).Text;
                }
            }
            return enumeration.ToString();
        }
    }
}

