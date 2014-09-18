namespace WHC.OrderWater.Commons
{
    using System;
    using System.Data;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class IDCardHelper
    {
        private static DataTable dataTable_0 = new DataTable();
        private static int[] int_0 = new int[] { 
            7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 
            2, 1
         };
        private static string[] string_0 = new string[] { "1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2" };

        static IDCardHelper()
        {
            dataTable_0.Columns.Add("text");
            dataTable_0.Columns.Add("value");
            dataTable_0.Rows.Add(new object[] { "居民身份证", "A" });
            dataTable_0.Rows.Add(new object[] { "军官证", "C" });
            dataTable_0.Rows.Add(new object[] { "士兵证", "D" });
            dataTable_0.Rows.Add(new object[] { "军官离退休证", "E" });
            dataTable_0.Rows.Add(new object[] { "境外人员身份证明", "F" });
            dataTable_0.Rows.Add(new object[] { "外交人员身份证明", "G" });
        }

        public static DataTable CreateIDType()
        {
            return dataTable_0;
        }

        public static string GetArea(string id)
        {
            return id.Substring(0, 6);
        }

        public static DateTime GetBirthday(string id)
        {
            string str = string.Empty;
            if (id.Length == 15)
            {
                str = "19" + id.Substring(6, 2) + "-" + id.Substring(8, 2) + "-" + id.Substring(10, 2);
            }
            else
            {
                if (id.Length != 0x12)
                {
                    throw new ArgumentException("身份证号码不是15或者18位！");
                }
                str = id.Substring(6, 4) + "-" + id.Substring(10, 2) + "-" + id.Substring(12, 2);
            }
            return Convert.ToDateTime(str);
        }

        public static string GetCity(string id)
        {
            return (id.Substring(0, 4) + "00");
        }

        public static string GetProvince(string id)
        {
            return (id.Substring(0, 2) + "0000");
        }

        public static string GetSexName(string id)
        {
            int num = 0;
            if (id.Length == 15)
            {
                num = Convert.ToInt32(id.Substring(14, 1));
            }
            else
            {
                if (id.Length != 0x12)
                {
                    throw new ArgumentException("身份证号码不是15或者18位！");
                }
                num = Convert.ToInt32(id.Substring(0x10, 1));
            }
            return (((num % 2) == 0) ? "女" : "男");
        }

        public static string IdCard15To18(string idcard)
        {
            if ((idcard == null) || (idcard.Length != 15))
            {
                return idcard;
            }
            string str = string.Empty;
            int index = 0;
            int num3 = 0;
            str = idcard.Substring(0, 6) + "19" + idcard.Substring(6, 9);
            for (int i = 0; i < 0x11; i++)
            {
                num3 = int.Parse(str.Substring(i, 1));
                index += num3 * int_0[i];
            }
            index = index % 11;
            return (str + string_0[index]);
        }

        public static void InitIdType(ComboBox cb)
        {
            cb.DataSource = dataTable_0;
            cb.DisplayMember = "text";
            cb.ValueMember = "value";
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public static string Validate(string idcard)
        {
            if ((idcard.Length != 0x12) && (idcard.Length != 15))
            {
                return "身份证明号码必须是15或者18位！";
            }
            Regex regex = new Regex(@"^\d{17}(\d|X)$");
            if (!regex.Match(idcard).Success)
            {
                return "身份证号码必须为数字或者X！";
            }
            if (idcard.Length == 15)
            {
                idcard = IdCard15To18(idcard);
            }
            else if (idcard.Length == 0x12)
            {
                int index = 0;
                int num3 = 0;
                for (int i = 0; i < 0x11; i++)
                {
                    num3 = int.Parse(idcard.Substring(i, 1));
                    index += num3 * int_0[i];
                }
                index = index % 11;
                if (idcard.Substring(0x11, 1) != string_0[index])
                {
                    return ("身份证号码最后一位应该是" + string_0[index] + "！");
                }
            }
            try
            {
                DateTime.Parse(idcard.Substring(6, 4) + "-" + idcard.Substring(10, 2) + "-" + idcard.Substring(12, 2));
            }
            catch
            {
                return "非法生日";
            }
            return string.Empty;
        }
    }
}

