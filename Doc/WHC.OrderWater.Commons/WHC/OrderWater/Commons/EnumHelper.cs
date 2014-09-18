namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class EnumHelper
    {
        public static string GetDescription(Type t, object v)
        {
            try
            {
                DescriptionAttribute[] customAttributes = (DescriptionAttribute[]) t.GetField(smethod_0(t, v)).GetCustomAttributes(typeof(DescriptionAttribute), false);
                return ((customAttributes.Length > 0) ? customAttributes[0].Description : smethod_0(t, v));
            }
            catch
            {
                return "UNKNOWN";
            }
        }

        public static T GetInstance<T>(string member)
        {
            return ConvertHelper.ConvertTo<T>(Enum.Parse(typeof(T), member, true));
        }

        public static Dictionary<string, object> GetMemberKeyValue<T>()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (string str in GetMemberNames<T>())
            {
                dictionary.Add(str, GetMemberValue<T>(str));
            }
            return dictionary;
        }

        public static string GetMemberName<T>(object member)
        {
            Type underlyingType = GetUnderlyingType(typeof(T));
            object obj2 = ConvertHelper.ConvertTo(member, underlyingType);
            return Enum.GetName(typeof(T), obj2);
        }

        public static string[] GetMemberNames<T>()
        {
            return Enum.GetNames(typeof(T));
        }

        public static object GetMemberValue<T>(string memberName)
        {
            Type underlyingType = GetUnderlyingType(typeof(T));
            return ConvertHelper.ConvertTo(GetInstance<T>(memberName), underlyingType);
        }

        public static Array GetMemberValues<T>()
        {
            return Enum.GetValues(typeof(T));
        }

        public static SortedList GetStatus(Type t)
        {
            SortedList list = new SortedList();
            Array values = Enum.GetValues(t);
            for (int i = 0; i < values.Length; i++)
            {
                string str = values.GetValue(i).ToString();
                int v = (int) Enum.Parse(t, str);
                string description = GetDescription(t, v);
                list.Add(v, description);
            }
            return list;
        }

        public static Type GetUnderlyingType(Type enumType)
        {
            return Enum.GetUnderlyingType(enumType);
        }

        public static bool IsDefined<T>(string member)
        {
            return Enum.IsDefined(typeof(T), member);
        }

        private static string smethod_0(Type type_0, object object_0)
        {
            try
            {
                return Enum.GetName(type_0, object_0);
            }
            catch
            {
                return "UNKNOWN";
            }
        }
    }
}

