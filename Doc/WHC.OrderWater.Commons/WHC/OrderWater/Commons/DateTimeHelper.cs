namespace WHC.OrderWater.Commons
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;

    public class DateTimeHelper
    {
        private DateTime dateTime_0;

        public DateTimeHelper()
        {
            this.dateTime_0 = DateTime.Now;
        }

        public DateTimeHelper(DateTime dateTime)
        {
            this.dateTime_0 = DateTime.Now;
            this.dateTime_0 = dateTime;
        }

        public DateTimeHelper(string dateTime)
        {
            this.dateTime_0 = DateTime.Now;
            this.dateTime_0 = DateTime.Parse(dateTime);
        }

        public static DateTime ConvertPHPToTime(long time)
        {
            DateTime time2 = new DateTime(0x7b2, 1, 1);
            return new DateTime(((time + 0x7080) * 0x989680) + time2.Ticks);
        }

        public static long ConvertTimeToJS(DateTime TheDate)
        {
            DateTime time = new DateTime(0x7b2, 1, 1);
            DateTime time2 = TheDate.ToUniversalTime();
            TimeSpan span = new TimeSpan(time2.Ticks - time.Ticks);
            return (long) span.TotalMilliseconds;
        }

        public static long ConvertTimeToPHP(DateTime time)
        {
            DateTime time2 = new DateTime(0x7b2, 1, 1);
            return ((DateTime.UtcNow.Ticks - time2.Ticks) / 0x989680);
        }

        public static DateTime GetChineseDateTime()
        {
            DateTime minValue = DateTime.MinValue;
            try
            {
                string url = "http://www.time.ac.cn/stime.asp";
                string html = new HttpHelper { Encoding = Encoding.Default }.GetHtml(url);
                string pattern = @"\d{4}年\d{1,2}月\d{1,2}日";
                string str4 = @"hrs\s+=\s+\d{1,2}";
                string str5 = @"min\s+=\s+\d{1,2}";
                string str6 = @"sec\s+=\s+\d{1,2}";
                Regex regex = new Regex(pattern);
                Regex regex2 = new Regex(str4);
                Regex regex3 = new Regex(str5);
                Regex regex4 = new Regex(str6);
                minValue = DateTime.Parse(regex.Match(html).Value);
                int num = smethod_0(regex2.Match(html).Value, false);
                int num2 = smethod_0(regex3.Match(html).Value, false);
                int num3 = smethod_0(regex4.Match(html).Value, false);
                minValue = minValue.AddHours((double) num).AddMinutes((double) num2).AddSeconds((double) num3);
            }
            catch
            {
            }
            return minValue;
        }

        public string GetDayOfWeekCN()
        {
            string[] strArray2 = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            return strArray2[Convert.ToInt16(this.dateTime_0.DayOfWeek)];
        }

        public int GetDayOfWeekNum()
        {
            return ((Convert.ToInt16(this.dateTime_0.DayOfWeek) == 0) ? 7 : Convert.ToInt16(this.dateTime_0.DayOfWeek));
        }

        public static string GetDiffTime(DateTime beginTime, DateTime endTime)
        {
            int mindTime = 0;
            return GetDiffTime(beginTime, endTime, ref mindTime);
        }

        public static string GetDiffTime(DateTime beginTime, DateTime endTime, ref int mindTime)
        {
            string str = string.Empty;
            int num = Convert.ToInt32(endTime.Subtract(beginTime).TotalSeconds);
            int num2 = 60;
            int num3 = 60 * 60;
            int num4 = 0xe10 * 0x18;
            int num5 = 0x15180 * 30;
            int num6 = 0x278d00 * 12;
            if ((mindTime > num) && (num > 0))
            {
                mindTime = 1;
            }
            else
            {
                mindTime = 0;
            }
            if (num > num6)
            {
                str = str + (num / num6) + "年";
                num = num % num6;
            }
            if (num > num5)
            {
                str = str + (num / num5) + "月";
                num = num % num5;
            }
            if (num > num4)
            {
                str = str + (num / num4) + "天";
                num = num % num4;
            }
            if (num > num3)
            {
                str = str + (num / num3) + "小时";
                num = num % num3;
            }
            if (num > num2)
            {
                str = str + (num / num2) + "分";
                num = num % num2;
            }
            return (str + num + "秒");
        }

        public static TimeSpan GetDiffTime2(DateTime DateTime1, DateTime DateTime2)
        {
            TimeSpan span = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts = new TimeSpan(DateTime2.Ticks);
            return span.Subtract(ts).Duration();
        }

        public string GetFirstDayOfMonth(int? months)
        {
            int? nullable = months;
            int num = nullable.HasValue ? nullable.GetValueOrDefault() : 0;
            return DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(num).ToShortDateString();
        }

        public string GetFirstDayOfQuarter(int? quarters)
        {
            int? nullable = quarters;
            int num = nullable.HasValue ? nullable.GetValueOrDefault() : 0;
            return this.dateTime_0.AddMonths((num * 3) - ((this.dateTime_0.Month - 1) % 3)).ToString("yyyy-MM-01");
        }

        public string GetFirstDayOfYear(int? years)
        {
            int? nullable = years;
            int num = nullable.HasValue ? nullable.GetValueOrDefault() : 0;
            return DateTime.Parse(this.dateTime_0.ToString("yyyy-01-01")).AddYears(num).ToShortDateString();
        }

        public string GetLastDayOfMonth(int? months)
        {
            int? nullable = months;
            int num = nullable.HasValue ? nullable.GetValueOrDefault() : 0;
            return DateTime.Parse(this.dateTime_0.ToString("yyyy-MM-01")).AddMonths(num).AddDays(-1.0).ToShortDateString();
        }

        public string GetLastDayOfQuarter(int? quarters)
        {
            int? nullable = quarters;
            int num = nullable.HasValue ? nullable.GetValueOrDefault() : 0;
            return DateTime.Parse(this.dateTime_0.AddMonths((num * 3) - ((this.dateTime_0.Month - 1) % 3)).ToString("yyyy-MM-01")).AddDays(-1.0).ToShortDateString();
        }

        public string GetLastDayOfYear(int? years)
        {
            int? nullable = years;
            int num = nullable.HasValue ? nullable.GetValueOrDefault() : 0;
            return DateTime.Parse(this.dateTime_0.ToString("yyyy-01-01")).AddYears(num).AddDays(-1.0).ToShortDateString();
        }

        public static DateTime GetRandomTime(DateTime time1, DateTime time2)
        {
            Random random = new Random();
            DateTime time = new DateTime();
            DateTime time3 = new DateTime();
            TimeSpan span = new TimeSpan(time1.Ticks - time2.Ticks);
            double totalSeconds = span.TotalSeconds;
            int num2 = 0;
            if (totalSeconds > 2147483647.0)
            {
                num2 = 0x7fffffff;
            }
            else if (totalSeconds < -2147483648.0)
            {
                num2 = -2147483648;
            }
            else
            {
                num2 = (int) totalSeconds;
            }
            if (num2 > 0)
            {
                time = time2;
                time3 = time1;
            }
            else
            {
                if (num2 >= 0)
                {
                    return time1;
                }
                time = time1;
                time3 = time2;
            }
            int num3 = num2;
            if (num2 <= -2147483648)
            {
                num3 = -2147483647;
            }
            int num4 = random.Next(Math.Abs(num3));
            return time.AddSeconds((double) num4);
        }

        public string GetSaturday(int? weeks)
        {
            int? nullable = weeks;
            int num = nullable.HasValue ? nullable.GetValueOrDefault() : 0;
            return this.dateTime_0.AddDays(Convert.ToDouble((int) (6 - Convert.ToInt16(this.dateTime_0.DayOfWeek))) + (7 * num)).ToShortDateString();
        }

        public string GetSunday(int? weeks)
        {
            int? nullable = weeks;
            int num = nullable.HasValue ? nullable.GetValueOrDefault() : 0;
            return this.dateTime_0.AddDays(Convert.ToDouble((int) -Convert.ToInt16(this.dateTime_0.DayOfWeek)) + (7 * num)).ToShortDateString();
        }

        public string GetTheDay(int? days)
        {
            int? nullable = days;
            int num = nullable.HasValue ? nullable.GetValueOrDefault() : 0;
            return this.dateTime_0.AddDays((double) num).ToShortDateString();
        }

        public static int GetWeekAmount(int year)
        {
            DateTime time = new DateTime(year, 12, 0x1f);
            GregorianCalendar calendar = new GregorianCalendar();
            return calendar.GetWeekOfYear(time, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        public static void GetWeekTime(int nYear, int nNumWeek, out DateTime dtWeekStart, out DateTime dtWeekeEnd)
        {
            DateTime time = new DateTime(nYear, 1, 1);
            time += new TimeSpan((nNumWeek - 1) * 7, 0, 0, 0);
            dtWeekStart = time.AddDays((double) (-time.DayOfWeek + 1));
            dtWeekeEnd = time.AddDays((double) ((6 - time.DayOfWeek) + 1));
        }

        public static void GetWeekWorkTime(int nYear, int nNumWeek, out DateTime dtWeekStart, out DateTime dtWeekeEnd)
        {
            DateTime time = new DateTime(nYear, 1, 1);
            time += new TimeSpan((nNumWeek - 1) * 7, 0, 0, 0);
            dtWeekStart = time.AddDays((double) (-time.DayOfWeek + 1));
            dtWeekeEnd = time.AddDays((double) ((6 - time.DayOfWeek) + 1)).AddDays(-2.0);
        }

        public static void SetLocalTime(DateTime dt)
        {
            Struct8 struct2;
            struct2.short_0 = (short) dt.Year;
            struct2.short_1 = (short) dt.Month;
            struct2.short_2 = (short) dt.DayOfWeek;
            struct2.short_3 = (short) dt.Day;
            struct2.short_4 = (short) dt.Hour;
            struct2.short_5 = (short) dt.Minute;
            struct2.short_6 = (short) dt.Second;
            struct2.short_7 = (short) dt.Millisecond;
            SetLocalTime_1(ref struct2);
        }

        [DllImport("kernel32.dll", EntryPoint="SetLocalTime")]
        private static extern bool SetLocalTime_1(ref Struct8 struct8_0);
        private static int smethod_0(string string_0, bool bool_0)
        {
            if (string.IsNullOrEmpty(string_0))
            {
                return 0;
            }
            string_0 = string_0.Trim();
            if (!bool_0)
            {
                string pattern = @"-?\d+";
                Regex regex = new Regex(pattern);
                string_0 = regex.Match(string_0.Trim()).Value;
            }
            int result = 0;
            int.TryParse(string_0, out result);
            return result;
        }

        public static int WeekOfYear(DateTime date)
        {
            GregorianCalendar calendar = new GregorianCalendar();
            return calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }

        public static int WeekOfYear(DateTime date, DayOfWeek week)
        {
            GregorianCalendar calendar = new GregorianCalendar();
            return calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, week);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Struct8
        {
            public short short_0;
            public short short_1;
            public short short_2;
            public short short_3;
            public short short_4;
            public short short_5;
            public short short_6;
            public short short_7;
        }
    }
}

