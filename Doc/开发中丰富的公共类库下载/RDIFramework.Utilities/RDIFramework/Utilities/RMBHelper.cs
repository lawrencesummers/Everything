namespace RDIFramework.Utilities
{
    using System;

    public class RMBHelper
    {
        public static string CmycurD(decimal num)
        {
            string str = "零壹贰叁肆伍陆柒捌玖";
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            int num2 = 0;
            num = Math.Round(Math.Abs(num), 2);
            str4 = ((long) (num * 100M)).ToString();
            int length = str4.Length;
            if (length > 15)
            {
                return "溢出";
            }
            str2 = str2.Substring(15 - length);
            for (int i = 0; i < length; i++)
            {
                str3 = str4.Substring(i, 1);
                int startIndex = Convert.ToInt32(str3);
                if ((((i != (length - 3)) && (i != (length - 7))) && (i != (length - 11))) && (i != (length - 15)))
                {
                    if (str3 == "0")
                    {
                        str6 = "";
                        str7 = "";
                        num2++;
                    }
                    else if ((str3 != "0") && (num2 != 0))
                    {
                        str6 = "零" + str.Substring(startIndex, 1);
                        str7 = str2.Substring(i, 1);
                        num2 = 0;
                    }
                    else
                    {
                        str6 = str.Substring(startIndex, 1);
                        str7 = str2.Substring(i, 1);
                        num2 = 0;
                    }
                }
                else if ((str3 != "0") && (num2 != 0))
                {
                    str6 = "零" + str.Substring(startIndex, 1);
                    str7 = str2.Substring(i, 1);
                    num2 = 0;
                }
                else if ((str3 != "0") && (num2 == 0))
                {
                    str6 = str.Substring(startIndex, 1);
                    str7 = str2.Substring(i, 1);
                    num2 = 0;
                }
                else if ((str3 == "0") && (num2 >= 3))
                {
                    str6 = "";
                    str7 = "";
                    num2++;
                }
                else if (length >= 11)
                {
                    str6 = "";
                    num2++;
                }
                else
                {
                    str6 = "";
                    str7 = str2.Substring(i, 1);
                    num2++;
                }
                if ((i == (length - 11)) || (i == (length - 3)))
                {
                    str7 = str2.Substring(i, 1);
                }
                str5 = str5 + str6 + str7;
                if ((i == (length - 1)) && (str3 == "0"))
                {
                    str5 = str5 + "整";
                }
            }
            if (num == 0M)
            {
                str5 = "零元整";
            }
            return str5;
        }

        public static string CmycurD(string numstr)
        {
            try
            {
                return CmycurD(Convert.ToDecimal(numstr));
            }
            catch
            {
                return "非数字形式！";
            }
        }
    }
}

