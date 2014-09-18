namespace RDIFramework.Utilities
{
    using System;
    using System.Runtime.InteropServices;

    public class LunarCalendarHelper
    {
        private bool bool_0;
        private bool bool_1;
        private static DateTime CxgjyvvCi = new DateTime(0x801, 12, 0x1f);
        private DateTime dateTime_0;
        private DateTime dateTime_1;
        private static DateTime dateTime_2 = new DateTime(0x76c, 1, 30);
        private static DateTime dateTime_3 = new DateTime(0x76b, 12, 0x16);
        private static DateTime dateTime_4 = new DateTime(0x7d7, 9, 13);
        private const int int_0 = 0x76c;
        private const int int_1 = 0x802;
        private const int int_2 = 0x748;
        private const int int_3 = 0x76c;
        private int int_4;
        private int int_5;
        private int int_6;
        private static int[] int_7 = new int[] { 
            0x4bd8, 0x4ae0, 0xa570, 0x54d5, 0xd260, 0xd950, 0x16554, 0x56a0, 0x9ad0, 0x55d2, 0x4ae0, 0xa5b6, 0xa4d0, 0xd250, 0x1d255, 0xb540, 
            0xd6a0, 0xada2, 0x95b0, 0x14977, 0x4970, 0xa4b0, 0xb4b5, 0x6a50, 0x6d40, 0x1ab54, 0x2b60, 0x9570, 0x52f2, 0x4970, 0x6566, 0xd4a0, 
            0xea50, 0x6e95, 0x5ad0, 0x2b60, 0x186e3, 0x92e0, 0x1c8d7, 0xc950, 0xd4a0, 0x1d8a6, 0xb550, 0x56a0, 0x1a5b4, 0x25d0, 0x92d0, 0xd2b2, 
            0xa950, 0xb557, 0x6ca0, 0xb550, 0x15355, 0x4da0, 0xa5b0, 0x14573, 0x52b0, 0xa9a8, 0xe950, 0x6aa0, 0xaea6, 0xab50, 0x4b60, 0xaae4, 
            0xa570, 0x5260, 0xf263, 0xd950, 0x5b57, 0x56a0, 0x96d0, 0x4dd5, 0x4ad0, 0xa4d0, 0xd4d4, 0xd250, 0xd558, 0xb540, 0xb6a0, 0x195a6, 
            0x95b0, 0x49b0, 0xa974, 0xa4b0, 0xb27a, 0x6a50, 0x6d40, 0xaf46, 0xab60, 0x9570, 0x4af5, 0x4970, 0x64b0, 0x74a3, 0xea50, 0x6b58, 
            0x55c0, 0xab60, 0x96d5, 0x92e0, 0xc960, 0xd954, 0xd4a0, 0xda50, 0x7552, 0x56a0, 0xabb7, 0x25d0, 0x92d0, 0xcab5, 0xa950, 0xb4a0, 
            0xbaa4, 0xad50, 0x55d9, 0x4ba0, 0xa5b0, 0x15176, 0x52b0, 0xa930, 0x7954, 0x6aa0, 0xad50, 0x5b52, 0x4b60, 0xa6e6, 0xa4e0, 0xd260, 
            0xea65, 0xd530, 0x5aa0, 0x76a3, 0x96d0, 0x4bd7, 0x4ad0, 0xa4d0, 0x1d0b6, 0xd250, 0xd520, 0xdd45, 0xb5a0, 0x56d0, 0x55b2, 0x49b0, 
            0xa577, 0xa4b0, 0xaa50, 0x1b255, 0x6d20, 0xada0, 0x14b63
         };
        private static int[] int_8 = new int[] { 
            0, 0x52d8, 0xa5e3, 0xf95c, 0x14d59, 0x1a206, 0x1f763, 0x24d89, 0x2a45d, 0x2fbdf, 0x353d8, 0x3ac35, 0x404af, 0x45d25, 0x4b553, 0x50d19, 
            0x56446, 0x5bac6, 0x61087, 0x6658a, 0x6b9db, 0x70d90, 0x760cc, 0x7b3b6
         };
        private static string[] IuflidOpN = new string[] { "白羊座", "金牛座", "双子座", "巨蟹座", "狮子座", "处女座", "天秤座", "天蝎座", "射手座", "摩羯座", "水瓶座", "双鱼座" };
        private const string string_0 = "零一二三四五六七八九";
        private static string[] string_1 = new string[] { 
            "小寒", "大寒", "立春", "雨水", "惊蛰", "春分", "清明", "谷雨", "立夏", "小满", "芒种", "夏至", "小暑", "大暑", "立秋", "处暑", 
            "白露", "秋分", "寒露", "霜降", "立冬", "小雪", "大雪", "冬至"
         };
        private static string[] string_2 = new string[] { 
            "角木蛟", "亢金龙", "女土蝠", "房日兔", "心月狐", "尾火虎", "箕水豹", "斗木獬", "牛金牛", "氐土貉", "虚日鼠", "危月燕", "室火猪", "壁水獝", "奎木狼", "娄金狗", 
            "胃土彘", "昴日鸡", "毕月乌", "觜火猴", "参水猿", "井木犴", "鬼金羊", "柳土獐", "星日马", "张月鹿", "翼火蛇", "轸水蚓"
         };
        private static string[] string_3 = new string[] { 
            "小寒", "大寒", "立春", "雨水", "惊蛰", "春分", "清明", "谷雨", "立夏", "小满", "芒种", "夏至", "小暑", "大暑", "立秋", "处暑", 
            "白露", "秋分", "寒露", "霜降", "立冬", "小雪", "大雪", "冬至"
         };
        private static string string_4 = "甲乙丙丁戊己庚辛壬癸";
        private static string string_5 = "子丑寅卯辰巳午未申酉戌亥";
        private static string string_6 = "鼠牛虎兔龙蛇马羊猴鸡狗猪";
        private static string string_7 = "日一二三四五六七八九";
        private static string string_8 = "初十廿卅";
        private static string[] string_9 = new string[] { "出错", "正月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "腊月" };
        private static Struct0[] struct0_0 = new Struct0[] { 
            new Struct0(1, 1, 1, "元旦"), new Struct0(2, 2, 0, "世界湿地日"), new Struct0(2, 10, 0, "国际气象节"), new Struct0(2, 14, 0, "情人节"), new Struct0(3, 1, 0, "国际海豹日"), new Struct0(3, 5, 0, "学雷锋纪念日"), new Struct0(3, 8, 0, "妇女节"), new Struct0(3, 12, 0, "植树节 孙中山逝世纪念日"), new Struct0(3, 14, 0, "国际警察日"), new Struct0(3, 15, 0, "消费者权益日"), new Struct0(3, 0x11, 0, "中国国医节 国际航海日"), new Struct0(3, 0x15, 0, "世界森林日 消除种族歧视国际日 世界儿歌日"), new Struct0(3, 0x16, 0, "世界水日"), new Struct0(3, 0x18, 0, "世界防治结核病日"), new Struct0(4, 1, 0, "愚人节"), new Struct0(4, 7, 0, "世界卫生日"), 
            new Struct0(4, 0x16, 0, "世界地球日"), new Struct0(5, 1, 1, "劳动节"), new Struct0(5, 2, 1, "劳动节假日"), new Struct0(5, 3, 1, "劳动节假日"), new Struct0(5, 4, 0, "青年节"), new Struct0(5, 8, 0, "世界红十字日"), new Struct0(5, 12, 0, "国际护士节"), new Struct0(5, 0x1f, 0, "世界无烟日"), new Struct0(6, 1, 0, "国际儿童节"), new Struct0(6, 5, 0, "世界环境保护日"), new Struct0(6, 0x1a, 0, "国际禁毒日"), new Struct0(7, 1, 0, "建党节 香港回归纪念 世界建筑日"), new Struct0(7, 11, 0, "世界人口日"), new Struct0(8, 1, 0, "建军节"), new Struct0(8, 8, 0, "中国男子节 父亲节"), new Struct0(8, 15, 0, "抗日战争胜利纪念"), 
            new Struct0(9, 9, 0, "毛主席逝世纪念"), new Struct0(9, 10, 0, "教师节"), new Struct0(9, 0x12, 0, "九\x00b7一八事变纪念日"), new Struct0(9, 20, 0, "国际爱牙日"), new Struct0(9, 0x1b, 0, "世界旅游日"), new Struct0(9, 0x1c, 0, "孔子诞辰"), new Struct0(10, 1, 1, "国庆节 国际音乐日"), new Struct0(10, 2, 1, "国庆节假日"), new Struct0(10, 3, 1, "国庆节假日"), new Struct0(10, 6, 0, "老人节"), new Struct0(10, 0x18, 0, "联合国日"), new Struct0(11, 10, 0, "世界青年节"), new Struct0(11, 12, 0, "孙中山诞辰纪念"), new Struct0(12, 1, 0, "世界艾滋病日"), new Struct0(12, 3, 0, "世界残疾人日"), new Struct0(12, 20, 0, "澳门回归纪念"), 
            new Struct0(12, 0x18, 0, "平安夜"), new Struct0(12, 0x19, 0, "圣诞节"), new Struct0(12, 0x1a, 0, "毛主席诞辰纪念")
         };
        private static Struct1[] struct1_0 = new Struct1[] { new Struct1(1, 1, 1, "春节"), new Struct1(1, 15, 0, "元宵节"), new Struct1(5, 5, 0, "端午节"), new Struct1(7, 7, 0, "七夕情人节"), new Struct1(7, 15, 0, "中元节 盂兰盆节"), new Struct1(8, 15, 0, "中秋节"), new Struct1(9, 9, 0, "重阳节"), new Struct1(12, 8, 0, "腊八节"), new Struct1(12, 0x17, 0, "北方小年(扫房)"), new Struct1(12, 0x18, 0, "南方小年(掸尘)") };
        private static Struct2[] struct2_0 = new Struct2[] { new Struct2(5, 2, 1, "母亲节"), new Struct2(5, 3, 1, "全国助残日"), new Struct2(6, 3, 1, "父亲节"), new Struct2(9, 3, 3, "国际和平日"), new Struct2(9, 4, 1, "国际聋人节"), new Struct2(10, 1, 2, "国际住房日"), new Struct2(10, 1, 4, "国际减轻自然灾害日"), new Struct2(11, 4, 5, "感恩节") };

        public LunarCalendarHelper(DateTime dt)
        {
            this.method_5(dt);
            this.dateTime_0 = dt.Date;
            this.dateTime_1 = dt;
            int num = 0;
            int num2 = 0;
            TimeSpan span = (TimeSpan) (this.dateTime_0 - dateTime_2);
            int days = span.Days;
            int num4 = 0x76c;
            while (num4 <= 0x802)
            {
                num2 = this.method_3(num4);
                if ((days - num2) < 1)
                {
                    break;
                }
                days -= num2;
                num4++;
            }
            this.int_4 = num4;
            num = this.method_1(this.int_4);
            if (num > 0)
            {
                this.bool_1 = true;
            }
            else
            {
                this.bool_1 = false;
            }
            this.bool_0 = false;
            num4 = 1;
            while (num4 <= 12)
            {
                if (!(((num <= 0) || (num4 != (num + 1))) || this.bool_0))
                {
                    this.bool_0 = true;
                    num4--;
                    num2 = this.method_2(this.int_4);
                }
                else
                {
                    this.bool_0 = false;
                    num2 = this.method_0(this.int_4, num4);
                }
                days -= num2;
                if (days <= 0)
                {
                    break;
                }
                num4++;
            }
            days += num2;
            this.int_5 = num4;
            this.int_6 = days;
        }

        public LunarCalendarHelper(int cy, int cm, int cd, bool leapMonthFlag)
        {
            int num2;
            int num3;
            this.method_6(cy, cm, cd, leapMonthFlag);
            this.int_4 = cy;
            this.int_5 = cm;
            this.int_6 = cd;
            int num = 0;
            for (num2 = 0x76c; num2 < cy; num2++)
            {
                num3 = this.method_3(num2);
                num += num3;
            }
            int num4 = this.method_1(cy);
            if (num4 != 0)
            {
                this.bool_1 = true;
            }
            else
            {
                this.bool_1 = false;
            }
            if (cm != num4)
            {
                this.bool_0 = false;
            }
            else
            {
                this.bool_0 = leapMonthFlag;
            }
            if (!this.bool_1 || (cm < num4))
            {
                for (num2 = 1; num2 < cm; num2++)
                {
                    num3 = this.method_0(cy, num2);
                    num += num3;
                }
                if (cd > this.method_0(cy, cm))
                {
                    throw new newCalendarException("不合法的农历日期");
                }
                num += cd;
            }
            else
            {
                for (num2 = 1; num2 < cm; num2++)
                {
                    num3 = this.method_0(cy, num2);
                    num += num3;
                }
                if (cm > num4)
                {
                    num3 = this.method_2(cy);
                    num += num3;
                    if (cd > this.method_0(cy, cm))
                    {
                        throw new newCalendarException("不合法的农历日期");
                    }
                    num += cd;
                }
                else
                {
                    if (this.bool_0)
                    {
                        num3 = this.method_0(cy, cm);
                        num += num3;
                    }
                    if (cd > this.method_2(cy))
                    {
                        throw new newCalendarException("不合法的农历日期");
                    }
                    num += cd;
                }
            }
            this.dateTime_0 = dateTime_2.AddDays((double) num);
        }

        private int method_0(int int_9, int int_10)
        {
            if (this.method_8(int_7[int_9 - 0x76c] & 0xffff, 0x10 - int_10))
            {
                return 30;
            }
            return 0x1d;
        }

        private int method_1(int int_9)
        {
            return (int_7[int_9 - 0x76c] & 15);
        }

        private bool method_10(DateTime dateTime_5, int int_9, int int_10, int int_11)
        {
            bool flag = false;
            if ((dateTime_5.Month == int_9) && (this.method_9(dateTime_5.DayOfWeek) == int_11))
            {
                DateTime time = new DateTime(dateTime_5.Year, dateTime_5.Month, 1);
                int num = this.method_9(time.DayOfWeek);
                int num2 = (7 - this.method_9(time.DayOfWeek)) + 1;
                if (num > int_11)
                {
                    if (((((int_10 - 1) * 7) + int_11) + num2) == dateTime_5.Day)
                    {
                        flag = true;
                    }
                    return flag;
                }
                if (((int_11 + num2) + ((int_10 - 2) * 7)) == dateTime_5.Day)
                {
                    flag = true;
                }
            }
            return flag;
        }

        private int method_2(int int_9)
        {
            if (this.method_1(int_9) != 0)
            {
                if ((int_7[int_9 - 0x76c] & 0x10000) != 0)
                {
                    return 30;
                }
                return 0x1d;
            }
            return 0;
        }

        private int method_3(int int_9)
        {
            int num = 0x15c;
            int num2 = 0x8000;
            int num3 = int_7[int_9 - 0x76c] & 0xffff;
            for (int i = 0; i < 12; i++)
            {
                int num5 = num3 & num2;
                if (num5 != 0)
                {
                    num++;
                }
                num2 = num2 >> 1;
            }
            return (num + this.method_2(int_9));
        }

        private string method_4(DateTime dateTime_5)
        {
            int hour = dateTime_5.Hour;
            if (dateTime_5.Minute != 0)
            {
                hour++;
            }
            int num3 = hour / 2;
            if (num3 >= 12)
            {
                num3 = 0;
            }
            TimeSpan span = (TimeSpan) (this.dateTime_0 - dateTime_3);
            int num4 = span.Days % 60;
            int startIndex = (((((num4 % 10) + 1) * 2) - 1) % 10) - 1;
            char ch = (string_4.Substring(startIndex) + string_4.Substring(0, startIndex + 2))[num3];
            ch = string_5[num3];
            return (ch.ToString() + ch.ToString());
        }

        private void method_5(DateTime dateTime_5)
        {
            if ((dateTime_5 < dateTime_2) || (dateTime_5 > CxgjyvvCi))
            {
                throw new newCalendarException("超出可转换的日期");
            }
        }

        private void method_6(int int_9, int int_10, int int_11, bool bool_2)
        {
            if ((int_9 < 0x76c) || (int_9 > 0x802))
            {
                throw new newCalendarException("非法农历日期");
            }
            if ((int_10 < 1) || (int_10 > 12))
            {
                throw new newCalendarException("非法农历日期");
            }
            if ((int_11 < 1) || (int_11 > 30))
            {
                throw new newCalendarException("非法农历日期");
            }
            int num = this.method_1(int_9);
            if (bool_2 && (int_10 != num))
            {
                throw new newCalendarException("非法农历日期");
            }
        }

        private string method_7(char char_0)
        {
            if ((char_0 >= '0') && (char_0 <= '9'))
            {
                char ch2;
                switch (char_0)
                {
                    case '0':
                        ch2 = "零一二三四五六七八九"[0];
                        return ch2.ToString();

                    case '1':
                        ch2 = "零一二三四五六七八九"[1];
                        return ch2.ToString();

                    case '2':
                        ch2 = "零一二三四五六七八九"[2];
                        return ch2.ToString();

                    case '3':
                        ch2 = "零一二三四五六七八九"[3];
                        return ch2.ToString();

                    case '4':
                        ch2 = "零一二三四五六七八九"[4];
                        return ch2.ToString();

                    case '5':
                        ch2 = "零一二三四五六七八九"[5];
                        return ch2.ToString();

                    case '6':
                        ch2 = "零一二三四五六七八九"[6];
                        return ch2.ToString();

                    case '7':
                        ch2 = "零一二三四五六七八九"[7];
                        return ch2.ToString();

                    case '8':
                        ch2 = "零一二三四五六七八九"[8];
                        return ch2.ToString();

                    case '9':
                        ch2 = "零一二三四五六七八九"[9];
                        return ch2.ToString();
                }
            }
            return "";
        }

        private bool method_8(int int_9, int int_10)
        {
            if ((int_10 > 0x1f) || (int_10 < 0))
            {
                throw new Exception("Error Param: bitpostion[0-31]:" + int_10.ToString());
            }
            int num = ((int) 1) << int_10;
            if ((int_9 & num) == 0)
            {
                return false;
            }
            return true;
        }

        private int method_9(DayOfWeek dayOfWeek_0)
        {
            switch (dayOfWeek_0)
            {
                case DayOfWeek.Sunday:
                    return 1;

                case DayOfWeek.Monday:
                    return 2;

                case DayOfWeek.Tuesday:
                    return 3;

                case DayOfWeek.Wednesday:
                    return 4;

                case DayOfWeek.Thursday:
                    return 5;

                case DayOfWeek.Friday:
                    return 6;

                case DayOfWeek.Saturday:
                    return 7;
            }
            return 0;
        }

        public LunarCalendarHelper NextDay()
        {
            return new LunarCalendarHelper(this.dateTime_0.AddDays(1.0));
        }

        public LunarCalendarHelper PervDay()
        {
            return new LunarCalendarHelper(this.dateTime_0.AddDays(-1.0));
        }

        public int Animal
        {
            get
            {
                int num = this.dateTime_0.Year - 0x76c;
                return ((num % 12) + 1);
            }
        }

        public string AnimalString
        {
            get
            {
                int num = this.dateTime_0.Year - 0x76c;
                char ch = string_6[num % 12];
                return ch.ToString();
            }
        }

        public string ChineseConstellation
        {
            get
            {
                int index = 0;
                TimeSpan span = (TimeSpan) (this.dateTime_0 - dateTime_4);
                index = span.Days % 0x1c;
                return ((index >= 0) ? string_2[index] : string_2[0x1b + index]);
            }
        }

        public string ChineseDateString
        {
            get
            {
                if (this.bool_0)
                {
                    return ("农历" + this.ChineseYearString + "闰" + this.ChineseMonthString + this.ChineseDayString);
                }
                return ("农历" + this.ChineseYearString + this.ChineseMonthString + this.ChineseDayString);
            }
        }

        public int ChineseDay
        {
            get
            {
                return this.int_6;
            }
        }

        public string ChineseDayString
        {
            get
            {
                switch (this.int_6)
                {
                    case 0:
                        return "";

                    case 10:
                        return "初十";

                    case 20:
                        return "二十";

                    case 30:
                        return "三十";
                }
                char ch = string_8[this.int_6 / 10];
                ch = string_7[this.int_6 % 10];
                return (ch.ToString() + ch.ToString());
            }
        }

        public string ChineseHour
        {
            get
            {
                return this.method_4(this.dateTime_1);
            }
        }

        public int ChineseMonth
        {
            get
            {
                return this.int_5;
            }
        }

        public string ChineseMonthString
        {
            get
            {
                return string_9[this.int_5];
            }
        }

        public string ChineseTwentyFourDay
        {
            get
            {
                DateTime time = new DateTime(0x76c, 1, 6, 2, 5, 0);
                int year = this.dateTime_0.Year;
                for (int i = 1; i <= 0x18; i++)
                {
                    double num3 = (525948.76 * (year - 0x76c)) + int_8[i - 1];
                    if (time.AddMinutes(num3).DayOfYear == this.dateTime_0.DayOfYear)
                    {
                        return string_3[i - 1];
                    }
                }
                return "";
            }
        }

        public string ChineseTwentyFourNextDay
        {
            get
            {
                DateTime time = new DateTime(0x76c, 1, 6, 2, 5, 0);
                int year = this.dateTime_0.Year;
                for (int i = 1; i <= 0x18; i++)
                {
                    double num3 = (525948.76 * (year - 0x76c)) + int_8[i - 1];
                    DateTime time2 = time.AddMinutes(num3);
                    if (time2.DayOfYear > this.dateTime_0.DayOfYear)
                    {
                        return string.Format("{0}[{1}]", string_3[i - 1], time2.ToString("yyyy-MM-dd"));
                    }
                }
                return "";
            }
        }

        public string ChineseTwentyFourPrevDay
        {
            get
            {
                DateTime time = new DateTime(0x76c, 1, 6, 2, 5, 0);
                int year = this.dateTime_0.Year;
                for (int i = 0x18; i >= 1; i--)
                {
                    double num3 = (525948.76 * (year - 0x76c)) + int_8[i - 1];
                    DateTime time2 = time.AddMinutes(num3);
                    if (time2.DayOfYear < this.dateTime_0.DayOfYear)
                    {
                        return string.Format("{0}[{1}]", string_3[i - 1], time2.ToString("yyyy-MM-dd"));
                    }
                }
                return "";
            }
        }

        public int ChineseYear
        {
            get
            {
                return this.int_4;
            }
        }

        public string ChineseYearString
        {
            get
            {
                string str = "";
                string str2 = this.int_4.ToString();
                for (int i = 0; i < 4; i++)
                {
                    str = str + this.method_7(str2[i]);
                }
                return (str + "年");
            }
        }

        public string Constellation
        {
            get
            {
                int index = 0;
                int year = this.dateTime_0.Year;
                int month = this.dateTime_0.Month;
                int day = this.dateTime_0.Day;
                year = (month * 100) + day;
                if ((year >= 0x141) && (year <= 0x1a3))
                {
                    index = 0;
                }
                else if ((year >= 420) && (year <= 520))
                {
                    index = 1;
                }
                else if ((year >= 0x209) && (year <= 620))
                {
                    index = 2;
                }
                else if ((year >= 0x26d) && (year <= 0x2d2))
                {
                    index = 3;
                }
                else if ((year >= 0x2d3) && (year <= 0x336))
                {
                    index = 4;
                }
                else if ((year >= 0x337) && (year <= 0x39a))
                {
                    index = 5;
                }
                else if ((year >= 0x39b) && (year <= 0x3fe))
                {
                    index = 6;
                }
                else if ((year >= 0x3ff) && (year <= 0x461))
                {
                    index = 7;
                }
                else if ((year >= 0x462) && (year <= 0x4c5))
                {
                    index = 8;
                }
                else if ((year >= 0x4c6) || (year <= 0x77))
                {
                    index = 9;
                }
                else if ((year >= 120) && (year <= 0xda))
                {
                    index = 10;
                }
                else if ((year >= 0xdb) && (year <= 320))
                {
                    index = 11;
                }
                else
                {
                    index = 0;
                }
                return IuflidOpN[index];
            }
        }

        public DateTime Date
        {
            get
            {
                return this.dateTime_0;
            }
            set
            {
                this.dateTime_0 = value;
            }
        }

        public string DateHoliday
        {
            get
            {
                foreach (Struct0 struct2 in struct0_0)
                {
                    if ((struct2.int_0 == this.dateTime_0.Month) && (struct2.int_1 == this.dateTime_0.Day))
                    {
                        return struct2.string_0;
                    }
                }
                return "";
            }
        }

        public string DateString
        {
            get
            {
                return ("公元" + this.dateTime_0.ToLongDateString());
            }
        }

        public string GanZhiDateString
        {
            get
            {
                return (this.GanZhiYearString + this.GanZhiMonthString + this.GanZhiDayString);
            }
        }

        public string GanZhiDayString
        {
            get
            {
                TimeSpan span = (TimeSpan) (this.dateTime_0 - dateTime_3);
                int num = span.Days % 60;
                char ch = string_4[num % 10];
                ch = string_5[num % 12];
                return (ch.ToString() + ch.ToString() + "日");
            }
        }

        public string GanZhiMonthString
        {
            get
            {
                int num;
                if (this.int_5 > 10)
                {
                    num = this.int_5 - 10;
                }
                else
                {
                    num = this.int_5 + 2;
                }
                string str = string_5[num - 1].ToString();
                int num2 = 1;
                int num3 = (this.int_4 - 0x748) % 60;
                switch ((num3 % 10))
                {
                    case 0:
                        num2 = 3;
                        break;

                    case 1:
                        num2 = 5;
                        break;

                    case 2:
                        num2 = 7;
                        break;

                    case 3:
                        num2 = 9;
                        break;

                    case 4:
                        num2 = 1;
                        break;

                    case 5:
                        num2 = 3;
                        break;

                    case 6:
                        num2 = 5;
                        break;

                    case 7:
                        num2 = 7;
                        break;

                    case 8:
                        num2 = 9;
                        break;

                    case 9:
                        num2 = 1;
                        break;
                }
                char ch = string_4[((num2 + this.int_5) - 2) % 10];
                return (ch.ToString() + str + "月");
            }
        }

        public string GanZhiYearString
        {
            get
            {
                int num = (this.int_4 - 0x748) % 60;
                char ch = string_4[num % 10];
                ch = string_5[num % 12];
                return (ch.ToString() + ch.ToString() + "年");
            }
        }

        public bool IsChineseLeapMonth
        {
            get
            {
                return this.bool_0;
            }
        }

        public bool IsChineseLeapYear
        {
            get
            {
                return this.bool_1;
            }
        }

        public bool IsLeapYear
        {
            get
            {
                return DateTime.IsLeapYear(this.dateTime_0.Year);
            }
        }

        public string newCalendarHoliday
        {
            get
            {
                string str = "";
                if (!this.bool_0)
                {
                    foreach (Struct1 struct2 in struct1_0)
                    {
                        if ((struct2.int_0 == this.int_5) && (struct2.int_1 == this.int_6))
                        {
                            str = struct2.string_0;
                            break;
                        }
                    }
                    if (this.int_5 == 12)
                    {
                        int num2 = this.method_0(this.int_4, 12);
                        if (this.int_6 == num2)
                        {
                            str = "除夕";
                        }
                    }
                }
                return str;
            }
        }

        public DayOfWeek WeekDay
        {
            get
            {
                return this.dateTime_0.DayOfWeek;
            }
        }

        public string WeekDayHoliday
        {
            get
            {
                foreach (Struct2 struct2 in struct2_0)
                {
                    if (this.method_10(this.dateTime_0, struct2.int_0, struct2.int_1, struct2.int_2))
                    {
                        return struct2.string_0;
                    }
                }
                return "";
            }
        }

        public string WeekDayStr
        {
            get
            {
                switch (this.dateTime_0.DayOfWeek)
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
                }
                return "星期六";
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Struct0
        {
            public int int_0;
            public int int_1;
            public int int_2;
            public string string_0;
            public Struct0(int int_3, int int_4, int int_5, string string_1)
            {
                this.int_0 = int_3;
                this.int_1 = int_4;
                this.int_2 = int_5;
                this.string_0 = string_1;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Struct1
        {
            public int int_0;
            public int int_1;
            public int int_2;
            public string string_0;
            public Struct1(int int_3, int int_4, int int_5, string string_1)
            {
                this.int_0 = int_3;
                this.int_1 = int_4;
                this.int_2 = int_5;
                this.string_0 = string_1;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Struct2
        {
            public int int_0;
            public int int_1;
            public int int_2;
            public string string_0;
            public Struct2(int int_3, int int_4, int int_5, string string_1)
            {
                this.int_0 = int_3;
                this.int_1 = int_4;
                this.int_2 = int_5;
                this.string_0 = string_1;
            }
        }
    }
}

