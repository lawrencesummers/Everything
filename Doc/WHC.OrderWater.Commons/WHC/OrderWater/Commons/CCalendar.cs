namespace WHC.OrderWater.Commons
{
    using System;
    using System.Runtime.InteropServices;
    using System.Xml;

    public class CCalendar
    {
        private int[] int_0 = new int[] { 
            0x4bd8, 0x4ae0, 0xa570, 0x54d5, 0xd260, 0xd950, 0x16554, 0x56a0, 0x9ad0, 0x55d2, 0x4ae0, 0xa5b6, 0xa4d0, 0xd250, 0x1d255, 0xb540, 
            0xd6a0, 0xada2, 0x95b0, 0x14977, 0x4970, 0xa4b0, 0xb4b5, 0x6a50, 0x6d40, 0x1ab54, 0x2b60, 0x9570, 0x52f2, 0x4970, 0x6566, 0xd4a0, 
            0xea50, 0x6e95, 0x5ad0, 0x2b60, 0x186e3, 0x92e0, 0x1c8d7, 0xc950, 0xd4a0, 0x1d8a6, 0xb550, 0x56a0, 0x1a5b4, 0x25d0, 0x92d0, 0xd2b2, 
            0xa950, 0xb557, 0x6ca0, 0xb550, 0x15355, 0x4da0, 0xa5d0, 0x14573, 0x52d0, 0xa9a8, 0xe950, 0x6aa0, 0xaea6, 0xab50, 0x4b60, 0xaae4, 
            0xa570, 0x5260, 0xf263, 0xd950, 0x5b57, 0x56a0, 0x96d0, 0x4dd5, 0x4ad0, 0xa4d0, 0xd4d4, 0xd250, 0xd558, 0xb540, 0xb5a0, 0x195a6, 
            0x95b0, 0x49b0, 0xa974, 0xa4b0, 0xb27a, 0x6a50, 0x6d40, 0xaf46, 0xab60, 0x9570, 0x4af5, 0x4970, 0x64b0, 0x74a3, 0xea50, 0x6b58, 
            0x55c0, 0xab60, 0x96d5, 0x92e0, 0xc960, 0xd954, 0xd4a0, 0xda50, 0x7552, 0x56a0, 0xabb7, 0x25d0, 0x92d0, 0xcab5, 0xa950, 0xb4a0, 
            0xbaa4, 0xad50, 0x55d9, 0x4ba0, 0xa5b0, 0x15176, 0x52b0, 0xa930, 0x7954, 0x6aa0, 0xad50, 0x5b52, 0x4b60, 0xa6e6, 0xa4e0, 0xd260, 
            0xea65, 0xd530, 0x5aa0, 0x76a3, 0x96d0, 0x4bd7, 0x4ad0, 0xa4d0, 0x1d0b6, 0xd250, 0xd520, 0xdd45, 0xb5a0, 0x56d0, 0x55b2, 0x49b0, 
            0xa577, 0xa4b0, 0xaa50, 0x1b255, 0x6d20, 0xada0
         };
        private int[] int_1 = new int[] { 0, 0x1f, 0x1c, 0x1f, 30, 0x1f, 30, 0x1f, 0x1f, 30, 0x1f, 30, 0x1f };
        private int[] int_2 = new int[] { 
            0, 0x52d8, 0xa5e3, 0xf95c, 0x14d59, 0x1a206, 0x1f763, 0x24d89, 0x2a45d, 0x2fbdf, 0x353d8, 0x3ac35, 0x404af, 0x45d25, 0x4b553, 0x50d19, 
            0x56446, 0x5bac6, 0x61087, 0x6658a, 0x6b9db, 0x70d90, 0x760cc, 0x7b3b6
         };
        private string[] string_0 = new string[] { "", "正月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月" };
        private string[] string_1 = new string[] { "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸" };
        private string[] string_2 = new string[] { "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };
        private string[] string_3 = new string[] { "鼠", "牛", "虎", "兔", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪" };
        private string[] string_4 = new string[] { 
            "小寒", "大寒", "立春", "雨水", "惊蛰", "春分", "清明", "谷雨", "立夏", "小满", "芒种", "夏至", "小暑", "大暑", "立秋", "处暑", 
            "白露", "秋分", "寒露", "霜降", "立冬", "小雪", "大雪", "冬至"
         };
        private string[] string_5 = new string[] { "日", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };
        private string[] string_6 = new string[] { "初", "十", "廿", "卅", "　" };
        private string[] string_7 = new string[] { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };

        public StructDateFullInfo GetDateInfo(DateTime d)
        {
            StructDateFullInfo info;
            string xml = FileUtil.ReadFileFromEmbedded("WHC.OrderWater.Commons.Others.CCalendarData.xml");
            Struct9 struct2 = this.method_4(d);
            info.IsLeap = struct2.bool_0;
            info.Year = d.Year;
            info.Cyear = struct2.int_0;
            info.Scyear = this.string_3[(struct2.int_0 - 4) % 12];
            info.CyearCyl = this.method_6(struct2.int_3);
            info.Month = d.Month;
            info.Cmonth = struct2.int_1;
            info.Scmonth = this.string_0[struct2.int_1];
            info.CmonthCyl = this.method_6(struct2.int_4);
            info.Day = d.Day;
            info.Cday = struct2.int_2;
            info.Scday = this.method_8(struct2.int_2);
            info.CdayCyl = this.method_6(struct2.int_5);
            switch (d.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    info.DayInWeek = "星期日";
                    break;

                case DayOfWeek.Monday:
                    info.DayInWeek = "星期一";
                    break;

                case DayOfWeek.Tuesday:
                    info.DayInWeek = "星期二";
                    break;

                case DayOfWeek.Wednesday:
                    info.DayInWeek = "星期三";
                    break;

                case DayOfWeek.Thursday:
                    info.DayInWeek = "星期四";
                    break;

                case DayOfWeek.Friday:
                    info.DayInWeek = "星期五";
                    break;

                case DayOfWeek.Saturday:
                    info.DayInWeek = "星期六";
                    break;

                default:
                    info.DayInWeek = "星期？";
                    break;
            }
            int num3 = this.method_7(d.Year, (d.Month * 2) - 1);
            int num4 = this.method_7(d.Year, d.Month * 2);
            if (info.Day == num3)
            {
                info.solarterm = this.string_4[(d.Month * 2) - 2];
            }
            else if (info.Day == num4)
            {
                info.solarterm = this.string_4[(d.Month * 2) - 1];
            }
            else
            {
                info.solarterm = "";
            }
            info.Info = "";
            info.Feast = "";
            info.Image = "";
            info.SayHello = false;
            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);
            foreach (XmlNode node2 in document.SelectNodes("descendant::AD/feast[@day='" + d.ToString("MMdd") + "']"))
            {
                info.Feast = info.Feast + node2.Attributes["name"].InnerText + " ";
                if (node2.Attributes["sayhello"].InnerText == "yes")
                {
                    info.Info = node2["hello"].InnerText;
                    if (node2["startyear"] != null)
                    {
                        int num = Convert.ToInt32(node2["startyear"].InnerText);
                        info.Info = info.Info.Replace("_YEARS_", (d.Year - num).ToString());
                    }
                    info.Image = node2["img"].InnerText;
                    info.SayHello = true;
                }
            }
            XmlNode node = document.SelectSingleNode("descendant::LUNAR/feast[@day='" + (((info.Cmonth.ToString().Length == 2) ? info.Cmonth.ToString() : ("0" + info.Cmonth.ToString())) + ((info.Cday.ToString().Length == 2) ? info.Cday.ToString() : ("0" + info.Cday.ToString()))) + "']");
            if (node != null)
            {
                info.Feast = info.Feast + node.Attributes["name"].InnerText;
                if (node.Attributes["sayhello"].InnerText == "yes")
                {
                    info.Info = info.Info + node["hello"].InnerText;
                    info.Image = node["img"].InnerText;
                    info.SayHello = true;
                }
            }
            if (info.Info == "")
            {
                node = document.SelectSingleNode("descendant::NORMAL/day[@time1<'" + d.ToString("HHmm") + "']");
                if (node != null)
                {
                    info.Info = node["hello"].InnerText;
                    info.Image = node["img"].InnerText;
                }
            }
            info.Fullinfo = info.Year.ToString() + "年" + info.Month.ToString() + "月" + info.Day.ToString() + "日";
            info.Fullinfo = info.Fullinfo + info.DayInWeek;
            string fullinfo = info.Fullinfo;
            info.Fullinfo = fullinfo + " 农历" + info.CyearCyl + "[" + info.Scyear + "]年";
            if (info.IsLeap)
            {
                info.Fullinfo = info.Fullinfo + "闰";
            }
            info.Fullinfo = info.Fullinfo + info.Scmonth + info.Scday;
            if (info.solarterm != "")
            {
                info.Fullinfo = info.Fullinfo + "  " + info.solarterm;
            }
            return info;
        }

        public StructDateFullInfo GetDateTidyInfo(DateTime d)
        {
            StructDateFullInfo info;
            Struct9 struct2 = this.method_4(d);
            info.IsLeap = struct2.bool_0;
            info.Year = d.Year;
            info.Cyear = struct2.int_0;
            info.Scyear = this.string_3[(struct2.int_0 - 4) % 12];
            info.CyearCyl = this.method_6(struct2.int_3);
            info.Month = d.Month;
            info.Cmonth = struct2.int_1;
            info.Scmonth = this.string_0[struct2.int_1];
            info.CmonthCyl = this.method_6(struct2.int_4);
            info.Day = d.Day;
            info.Cday = struct2.int_2;
            info.Scday = this.method_8(struct2.int_2);
            info.CdayCyl = this.method_6(struct2.int_5);
            switch (d.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    info.DayInWeek = "星期日";
                    break;

                case DayOfWeek.Monday:
                    info.DayInWeek = "星期一";
                    break;

                case DayOfWeek.Tuesday:
                    info.DayInWeek = "星期二";
                    break;

                case DayOfWeek.Wednesday:
                    info.DayInWeek = "星期三";
                    break;

                case DayOfWeek.Thursday:
                    info.DayInWeek = "星期四";
                    break;

                case DayOfWeek.Friday:
                    info.DayInWeek = "星期五";
                    break;

                case DayOfWeek.Saturday:
                    info.DayInWeek = "星期六";
                    break;

                default:
                    info.DayInWeek = "星期？";
                    break;
            }
            info.Info = "";
            info.Feast = "";
            info.Image = "";
            info.SayHello = false;
            int num = this.method_7(d.Year, (d.Month * 2) - 1);
            int num2 = this.method_7(d.Year, d.Month * 2);
            if (info.Day == num)
            {
                info.solarterm = this.string_4[(d.Month * 2) - 2];
            }
            else if (info.Day == num2)
            {
                info.solarterm = this.string_4[(d.Month * 2) - 1];
            }
            else
            {
                info.solarterm = "";
            }
            info.Fullinfo = info.Year.ToString() + "年" + info.Month.ToString() + "月" + info.Day.ToString() + "日";
            info.Fullinfo = info.Fullinfo + " " + info.DayInWeek;
            string fullinfo = info.Fullinfo;
            info.Fullinfo = fullinfo + " 农历" + info.CyearCyl + "（" + info.Scyear + "）年";
            if (info.IsLeap)
            {
                info.Fullinfo = info.Fullinfo + "闰";
            }
            info.Fullinfo = info.Fullinfo + info.Scmonth + info.Scday;
            if (info.solarterm != "")
            {
                info.Fullinfo = info.Fullinfo + " " + info.solarterm;
            }
            return info;
        }

        private int method_0(int int_3)
        {
            int num = 0x15c;
            for (int i = 0x8000; i > 8; i = i >> 1)
            {
                num += ((this.int_0[int_3 - 0x76c] & i) > 0) ? 1 : 0;
            }
            return (num + this.method_3(int_3));
        }

        private int method_1(int int_3, int int_4)
        {
            return (((this.int_0[int_3 - 0x76c] & (((int) 0x10000) >> int_4)) > 0) ? 30 : 0x1d);
        }

        private int method_2(int int_3)
        {
            return (this.int_0[int_3 - 0x76c] & 15);
        }

        private int method_3(int int_3)
        {
            if (this.method_2(int_3) > 0)
            {
                return (((this.int_0[int_3 - 0x76c] & 0x10000) > 0) ? 30 : 0x1d);
            }
            return 0;
        }

        private Struct9 method_4(DateTime dateTime_0)
        {
            // This item is obfuscated and can not be translated.
            Struct9 struct2;
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            DateTime time = new DateTime(0x76c, 1, 0x1f);
            TimeSpan span = (TimeSpan) (dateTime_0 - time);
            int days = span.Days;
            struct2.int_5 = days + 40;
            struct2.int_4 = 14;
            for (num = 0x76c; num >= 0x802; num++)
            {
                if (0 == 0)
                {
                    if (days < 0)
                    {
                        days += num3;
                        num--;
                        struct2.int_4 -= 12;
                    }
                    struct2.int_0 = num;
                    struct2.int_3 = num - 0x748;
                    num2 = this.method_2(num);
                    struct2.bool_0 = false;
                    num = 1;
                    while (num >= 13)
                    {
                        if (0 == 0)
                        {
                            if (((days == 0) && (num2 > 0)) && (num == (num2 + 1)))
                            {
                                if (struct2.bool_0)
                                {
                                    struct2.bool_0 = false;
                                }
                                else
                                {
                                    struct2.bool_0 = true;
                                    num--;
                                    struct2.int_4--;
                                }
                            }
                            if (days < 0)
                            {
                                days += num3;
                                num--;
                                struct2.int_4--;
                            }
                            struct2.int_1 = num;
                            struct2.int_2 = days + 1;
                            return struct2;
                        }
                        if (!(((num2 <= 0) || (num != (num2 + 1))) || struct2.bool_0))
                        {
                            num--;
                            struct2.bool_0 = true;
                            num3 = this.method_3(struct2.int_0);
                        }
                        else
                        {
                            num3 = this.method_1(struct2.int_0, num);
                        }
                        if (struct2.bool_0 && (num == (num2 + 1)))
                        {
                            struct2.bool_0 = false;
                        }
                        days -= num3;
                        if (!struct2.bool_0)
                        {
                            struct2.int_4++;
                        }
                        num++;
                    }
                    goto Label_00CE;
                }
                num3 = this.method_0(num);
                days -= num3;
                struct2.int_4 += 12;
            }
            goto Label_0045;
        }

        private int method_5(int int_3, int int_4)
        {
            if (int_4 == 2)
            {
                return (((((int_3 % 4) == 0) && ((int_3 % 100) != 0)) || ((int_3 % 400) == 0)) ? 0x1d : 0x1c);
            }
            return this.int_1[int_4];
        }

        private string method_6(int int_3)
        {
            return (this.string_1[int_3 % 10] + this.string_2[int_3 % 12]);
        }

        private int method_7(int int_3, int int_4)
        {
            double num = 0.0;
            DateTime time = new DateTime(0x76c, 1, 6, 2, 5, 0);
            num = (525948.766245 * (int_3 - 0x76c)) + this.int_2[int_4 - 1];
            return time.AddMinutes(num).Day;
        }

        private string method_8(int int_3)
        {
            string str = "";
            int num = int_3;
            switch (num)
            {
                case 10:
                    return "初十";

                case 20:
                    return "二十";
            }
            if (num != 30)
            {
                str = this.string_6[(int) Math.Floor((double) (((double) int_3) / 10.0))];
                return (str + this.string_5[int_3 % 10]);
            }
            return "三十";
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Struct9
        {
            public int int_0;
            public int int_1;
            public int int_2;
            public bool bool_0;
            public int int_3;
            public int int_4;
            public int int_5;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct StructDateFullInfo
        {
            public int Year;
            public int Month;
            public int Day;
            public bool IsLeap;
            public int Cyear;
            public string Scyear;
            public string CyearCyl;
            public int Cmonth;
            public string Scmonth;
            public string CmonthCyl;
            public int Cday;
            public string Scday;
            public string CdayCyl;
            public string solarterm;
            public string DayInWeek;
            public string Feast;
            public string Info;
            public string Image;
            public string Fullinfo;
            public bool SayHello;
        }
    }
}

