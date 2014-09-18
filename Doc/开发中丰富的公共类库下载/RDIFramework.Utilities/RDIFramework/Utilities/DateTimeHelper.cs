namespace RDIFramework.Utilities
{
    using System;
    using System.Globalization;

    public class DateTimeHelper
    {
        private static ChineseLunisolarCalendar chineseLunisolarCalendar_0 = new ChineseLunisolarCalendar();
        internal static CultureInfo cultureInfo_0 = new CultureInfo("zh-cn");

        public static int DiffDays(DateTime dtfrm, DateTime dtto)
        {
            TimeSpan span = (TimeSpan) (dtto.Date - dtfrm.Date);
            return span.Days;
        }

        public static string FormatDate(string strValue)
        {
            if (IsDate(strValue.Trim()))
            {
                return DateTime.Parse(strValue.Trim()).ToString("yyyy-MM-dd");
            }
            return "";
        }

        public static AnimalSign GetAnimalSign(DateTime dt)
        {
            if (!IsLunisolarSupported(dt.Year))
            {
                throw new Exception("不支持给定的时间");
            }
            return (AnimalSign) chineseLunisolarCalendar_0.GetTerrestrialBranch(chineseLunisolarCalendar_0.GetSexagenaryYear(dt));
        }

        public Constellation GetConstellation(DateTime dt)
        {
            int num = (dt.Month * 100) + dt.Day;
            if ((num >= 0x141) && (num <= 0x1a3))
            {
                return Constellation.const_2;
            }
            if ((num >= 420) && (num <= 520))
            {
                return Constellation.const_3;
            }
            if ((num >= 0x209) && (num <= 620))
            {
                return Constellation.const_4;
            }
            if ((num >= 0x26d) && (num <= 0x2d2))
            {
                return Constellation.const_5;
            }
            if ((num >= 0x2d3) && (num <= 0x336))
            {
                return Constellation.const_6;
            }
            if ((num >= 0x337) && (num <= 0x39a))
            {
                return Constellation.const_7;
            }
            if ((num >= 0x39b) && (num <= 0x3ff))
            {
                return Constellation.const_8;
            }
            if ((num >= 0x400) && (num <= 0x462))
            {
                return Constellation.const_9;
            }
            if ((num >= 0x463) && (num <= 0x4c5))
            {
                return Constellation.const_10;
            }
            if ((num >= 0x4c6) || (num <= 0x77))
            {
                return Constellation.const_11;
            }
            if ((num >= 120) && (num <= 0xda))
            {
                return Constellation.const_0;
            }
            return Constellation.const_1;
        }

        public static string GetDate(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }

        public static string GetDateTime(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static int GetDaysOfMonth(DateTime dt)
        {
            int year = dt.Year;
            switch (dt.Month)
            {
                case 1:
                    return 0x1f;

                case 2:
                    return (smethod_0(year) ? 0x1d : 0x1c);

                case 3:
                    return 0x1f;

                case 4:
                    return 30;

                case 5:
                    return 0x1f;

                case 6:
                    return 30;

                case 7:
                    return 0x1f;

                case 8:
                    return 0x1f;

                case 9:
                    return 30;

                case 10:
                    return 0x1f;

                case 11:
                    return 30;

                case 12:
                    return 0x1f;
            }
            return 0;
        }

        public static int GetDaysOfMonth(int iYear, int Month)
        {
            switch (Month)
            {
                case 1:
                    return 0x1f;

                case 2:
                    return (smethod_0(iYear) ? 0x1d : 0x1c);

                case 3:
                    return 0x1f;

                case 4:
                    return 30;

                case 5:
                    return 0x1f;

                case 6:
                    return 30;

                case 7:
                    return 0x1f;

                case 8:
                    return 0x1f;

                case 9:
                    return 30;

                case 10:
                    return 0x1f;

                case 11:
                    return 30;

                case 12:
                    return 0x1f;
            }
            return 0;
        }

        public static int GetDaysOfYear(DateTime dt)
        {
            return (smethod_0(dt.Year) ? 0x16e : 0x16d);
        }

        public static int GetDaysOfYear(int iYear)
        {
            return (smethod_0(iYear) ? 0x16e : 0x16d);
        }

        public static string GetFormatTime(DateTime dt, string format)
        {
            return dt.ToString(format);
        }

        public static string GetNumberOfDays(string strYear, string strMonth)
        {
            string str = string.Empty;
            int num = Convert.ToInt32(strMonth);
            int num2 = Convert.ToInt32(strYear);
            strMonth = Convert.ToString(num);
            switch (strMonth)
            {
                case "1":
                    return "31";

                case "2":
                    if (((num2 % 400) != 0) && (((num2 % 100) == 0) || ((num2 % 4) != 0)))
                    {
                        return "28";
                    }
                    return "29";

                case "3":
                    return "31";

                case "4":
                    return "30";

                case "5":
                    return "31";

                case "6":
                    return "30";

                case "7":
                    return "31";

                case "8":
                    return "31";

                case "9":
                    return "30";

                case "10":
                    return "31";

                case "11":
                    return "30";

                case "12":
                    return "31";
            }
            return str;
        }

        public static string GetTime(DateTime dt)
        {
            return dt.ToString("HH:mm:ss");
        }

        public static int GetWeekAmount(int year)
        {
            DateTime time = new DateTime(year, 12, 0x1f);
            GregorianCalendar calendar = new GregorianCalendar();
            return calendar.GetWeekOfYear(time, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        public static string GetWeekNameOfDay(DateTime dt)
        {
            string str = string.Empty;
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "星期日";

                case DayOfWeek.Monday:
                    return "星期一";

                case DayOfWeek.Tuesday:
                    return "星期二";

                case DayOfWeek.Wednesday:
                    return "星期三";

                case DayOfWeek.Thursday:
                    return "星期四";

                case DayOfWeek.Friday:
                    return "星期五";

                case DayOfWeek.Saturday:
                    return "星期六";
            }
            return str;
        }

        public static int GetWeekNumberOfDay(DateTime dt)
        {
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return 7;

                case DayOfWeek.Monday:
                    return 1;

                case DayOfWeek.Tuesday:
                    return 2;

                case DayOfWeek.Wednesday:
                    return 3;

                case DayOfWeek.Thursday:
                    return 4;

                case DayOfWeek.Friday:
                    return 5;

                case DayOfWeek.Saturday:
                    return 6;
            }
            return 0;
        }

        public static int GetWeekOfYear(DateTime dt)
        {
            GregorianCalendar calendar = new GregorianCalendar();
            return calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        public static bool IsDate(string strValue)
        {
            try
            {
                DateTime.Parse(strValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDateTime(string dateTime)
        {
            return ValidateUtil.IsDateTime(dateTime);
        }

        public static bool IsLunisolarSupported(int year)
        {
            return ((year > 0x76d) && (year < 0x835));
        }

        private static bool smethod_0(int int_0)
        {
            int num = int_0;
            return (((num % 400) == 0) || (((num % 4) == 0) && ((num % 100) != 0)));
        }

        public static DateTime? ToDataTime(string strValue)
        {
            if (IsDate(strValue.Trim()))
            {
                return new DateTime?(DateTime.Parse(strValue.Trim()));
            }
            return null;
        }

        public static DateTime? ToDate(string strValue)
        {
            if (IsDate(strValue.Trim()))
            {
                return new DateTime?(DateTime.Parse(DateTime.Parse(strValue.Trim()).ToString("yyyy-MM-dd")));
            }
            return null;
        }

        public static string ToString(DateTime oDateTime, string strFormat)
        {
            try
            {
                string str3 = strFormat.ToUpper();
                if (str3 != null)
                {
                    if (!(str3 == "SHORTDATE"))
                    {
                        if (str3 == "LONGDATE")
                        {
                            return oDateTime.ToLongDateString();
                        }
                    }
                    else
                    {
                        return oDateTime.ToShortDateString();
                    }
                }
                return oDateTime.ToString(strFormat);
            }
            catch (Exception)
            {
                return oDateTime.ToShortDateString();
            }
        }

        public static void WeekRange(int year, int weekOrder, ref DateTime firstDate, ref DateTime lastDate)
        {
            DateTime time = new DateTime(year, 1, 1);
            int num = Convert.ToInt32(time.DayOfWeek);
            int num2 = (-1 * num) + 1;
            int num3 = 7 - num;
            firstDate = time.AddDays((double) num2).Date;
            lastDate = time.AddDays((double) num3).Date;
            if (weekOrder != 1)
            {
                int num4 = (weekOrder - 1) * 7;
                firstDate = firstDate.AddDays((double) num4);
                lastDate = lastDate.AddDays((double) num4);
            }
        }
    }
}

