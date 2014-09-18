namespace WHC.OrderWater.Commons
{
    using System;

    public class CommandLine
    {
        public static CommandArgs Parse(string[] args)
        {
            char[] trimChars = new char[] { '=' };
            char[] chArray3 = new char[] { '-', '\\' };
            CommandArgs args2 = new CommandArgs();
            int num = -1;
            for (string str = smethod_1(args, ref num); str != null; str = smethod_1(args, ref num))
            {
                if (smethod_0(str))
                {
                    string key = str.TrimStart(chArray3).TrimEnd(trimChars);
                    string str3 = null;
                    if (key.Contains("="))
                    {
                        string[] strArray = key.Split(trimChars, 2);
                        if ((strArray.Length == 2) && (strArray[1] != string.Empty))
                        {
                            key = strArray[0];
                            str3 = strArray[1];
                        }
                    }
                    while (str3 == null)
                    {
                        string str2 = smethod_1(args, ref num);
                        if (str2 != null)
                        {
                            if (smethod_0(str2))
                            {
                                num--;
                                str3 = "true";
                            }
                            else if (str2 != "=")
                            {
                                str3 = str2.TrimStart(trimChars);
                            }
                        }
                    }
                    args2.ArgPairs.Add(key, str3);
                }
                else if (str != string.Empty)
                {
                    args2.Params.Add(str);
                }
            }
            return args2;
        }

        private static bool smethod_0(string string_0)
        {
            return (string_0.StartsWith("-") || string_0.StartsWith(@"\"));
        }

        private static string smethod_1(object object_0, ref int int_0)
        {
            int_0++;
            while (int_0 < object_0.Length)
            {
                string str2 = object_0[int_0].Trim();
                if (str2 != string.Empty)
                {
                    return str2;
                }
                int_0++;
            }
            return null;
        }
    }
}

