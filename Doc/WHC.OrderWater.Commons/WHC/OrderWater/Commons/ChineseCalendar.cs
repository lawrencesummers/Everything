namespace WHC.OrderWater.Commons
{
    using System;
    using System.Globalization;

    public class ChineseCalendar
    {
        private static ChineseLunisolarCalendar chineseLunisolarCalendar_0 = new ChineseLunisolarCalendar();
        private static string[] string_0 = new string[] { "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸" };
        private static string[] string_1 = new string[] { "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };
        private static string[] string_2 = new string[] { "鼠", "牛", "虎", "免", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪" };
        private static string[] string_3 = new string[] { "正", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二(腊)" };
        private static string[] string_4 = new string[] { "初", "十", "廿", "三" };
        private static string[] string_5 = new string[] { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };

        public static string GetChineseDateTime(DateTime datetime)
        {
            int year = chineseLunisolarCalendar_0.GetYear(datetime);
            int month = chineseLunisolarCalendar_0.GetMonth(datetime);
            int dayOfMonth = chineseLunisolarCalendar_0.GetDayOfMonth(datetime);
            int leapMonth = chineseLunisolarCalendar_0.GetLeapMonth(year);
            bool flag = false;
            if (leapMonth > 0)
            {
                if (leapMonth == month)
                {
                    flag = true;
                    month--;
                }
                else if (month > leapMonth)
                {
                    month--;
                }
            }
            return (GetLunisolarYear(year) + "年" + (flag ? "闰" : string.Empty) + GetLunisolarMonth(month) + "月" + GetLunisolarDay(dayOfMonth));
        }

        public static string GetChineseDateTimeNow()
        {
            return GetChineseDateTime(DateTime.Now);
        }

        public static string GetLunisolarDay(int day)
        {
            if ((day <= 0) || (day >= 0x20))
            {
                throw new ArgumentOutOfRangeException("无效的日!");
            }
            if ((day != 20) && (day != 30))
            {
                return (string_4[(day - 1) / 10] + string_5[(day - 1) % 10]);
            }
            return (string_5[(day - 1) / 10] + string_4[1]);
        }

        public static string GetLunisolarMonth(int month)
        {
            if ((month >= 13) || (month <= 0))
            {
                throw new ArgumentOutOfRangeException("无效的月份!");
            }
            return string_3[month - 1];
        }

        public static string GetLunisolarYear(int year)
        {
            if (year <= 3)
            {
                throw new ArgumentOutOfRangeException("无效的年份!");
            }
            int index = (year - 4) % 10;
            int num2 = (year - 4) % 12;
            return (string_0[index] + string_1[num2] + "[" + string_2[num2] + "]");
        }

        public static string GetShengXiao(DateTime datetime)
        {
            return string_2[chineseLunisolarCalendar_0.GetTerrestrialBranch(chineseLunisolarCalendar_0.GetSexagenaryYear(datetime)) - 1];
        }

        public static DateTime MaxSupportedDateTime
        {
            get
            {
                return chineseLunisolarCalendar_0.MaxSupportedDateTime;
            }
        }

        public static DateTime MinSupportedDateTime
        {
            get
            {
                return chineseLunisolarCalendar_0.MinSupportedDateTime;
            }
        }

        public static string Now
        {
            get
            {
                return GetChineseDateTimeNow();
            }
        }
    }
}

