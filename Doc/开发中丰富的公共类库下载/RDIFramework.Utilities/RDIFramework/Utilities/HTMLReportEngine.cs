namespace RDIFramework.Utilities
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    public class HTMLReportEngine
    {
        private ArrayList arrayList_0 = new ArrayList();
        private ArrayList arrayList_1 = new ArrayList();
        public string ChartChangeOnField;
        public string ChartLabelHeader = "Label";
        public string ChartPercentageHeader = "Percentage";
        public bool ChartShowAtBottom;
        public bool ChartShowBorder;
        public string ChartTitle;
        public string ChartValueField = "Count";
        public string ChartValueHeader = "Value";
        private DataSet dataSet_0;
        private Hashtable hashtable_0 = new Hashtable();
        public bool IncludeChart;
        public bool IncludeTotal;
        private int int_0 = 0;
        public string ReportFont = "Arial";
        private string string_0;
        private string string_1 = "\n";
        private string string_2 = "FILTER: progid:DXImageTransform.Microsoft.Gradient(gradientType=1,startColorStr=BackColor,endColorStr=#ffffff)";
        private StringBuilder stringBuilder_0 = new StringBuilder();
        public ArrayList TotalFields = new ArrayList();

        public string GenerateReport()
        {
            foreach (Field field in this.ReportFields)
            {
                if (!(this.TotalFields.Contains(field.FieldName) || !field.isTotalField))
                {
                    this.TotalFields.Add(field.FieldName);
                }
            }
            this.method_0();
            this.method_1();
            this.method_5();
            return this.stringBuilder_0.ToString();
        }

        private void method_0()
        {
            this.stringBuilder_0.Append("<HTML><HEAD><TITLE>Report - " + this.string_0 + "</TITLE></HEAD>" + this.string_1);
            this.stringBuilder_0.Append("<STYLE>" + this.string_1);
            this.stringBuilder_0.Append(" .TableStyle { border-collapse: collapse } " + this.string_1);
            this.stringBuilder_0.Append(" .TitleStyle { font-family: " + this.ReportFont + "; font-size:15pt } " + this.string_1);
            this.stringBuilder_0.Append(" .SectionHeader {font-family: " + this.ReportFont + "; font-size:10pt } " + this.string_1);
            this.stringBuilder_0.Append(" .DetailHeader {font-family: " + this.ReportFont + "; font-size:9pt } " + this.string_1);
            this.stringBuilder_0.Append(" .DetailData  {font-family: " + this.ReportFont + "; font-size:9pt } " + this.string_1);
            this.stringBuilder_0.Append(" .ColumnHeaderStyle  {font-family: " + this.ReportFont + "; font-size:9pt; border-style:outset; border-width:1} " + this.string_1);
            this.stringBuilder_0.Append("</STYLE>" + this.string_1);
            this.stringBuilder_0.Append("<BODY TOPMARGIN=0 LEFTMARGIN=0 RIGHTMARGIN=0 BOTTOMMARGIN=0>" + this.string_1);
            this.stringBuilder_0.Append("<TABLE Width='100%' style='FILTER: progid:DXImageTransform.Microsoft.Gradient(gradientType=1,startColorStr=#a9d4ff,endColorStr=#ffffff)' Cellpadding=5><TR><TD>");
            this.stringBuilder_0.Append("<font face='" + this.ReportFont + "' size=6>" + this.ReportTitle + "</font>");
            this.stringBuilder_0.Append("</TD></TR></TABLE>" + this.string_1);
        }

        private void method_1()
        {
            if (this.arrayList_0.Count == 0)
            {
                Section section = new Section {
                    int_0 = 5,
                    ChartChangeOnField = this.ChartChangeOnField,
                    ChartLabelHeader = this.ChartLabelHeader,
                    ChartPercentageHeader = this.ChartPercentageHeader,
                    ChartShowAtBottom = this.ChartShowAtBottom,
                    ChartShowBorder = this.ChartShowAtBottom,
                    ChartTitle = this.ChartTitle,
                    ChartValueField = this.ChartValueField,
                    ChartValueHeader = this.ChartValueHeader,
                    IncludeChart = this.IncludeChart
                };
                this.stringBuilder_0.Append("<TABLE Width='100%' class='TableStyle'  cellspacing=0 cellpadding=5 border=0>" + this.string_1);
                if (!(!this.IncludeChart || this.ChartShowAtBottom))
                {
                    this.method_7("", section);
                }
                Hashtable hashtable = this.method_3(null, "");
                if (this.IncludeTotal)
                {
                    section.IncludeTotal = true;
                    this.method_4(section, hashtable);
                }
                if (this.IncludeChart && this.ChartShowAtBottom)
                {
                    this.method_7("", section);
                }
                this.stringBuilder_0.Append("</TABLE></BODY></HTML>");
            }
            foreach (Section section2 in this.arrayList_0)
            {
                this.int_0 = 0;
                this.stringBuilder_0.Append("<TABLE Width='100%' class='TableStyle'  cellspacing=0 cellpadding=5 border=0>" + this.string_1);
                this.method_6(section2, "");
                this.stringBuilder_0.Append("</TABLE></BODY></HTML>");
            }
        }

        private Hashtable method_10(Hashtable hashtable_1)
        {
            foreach (object obj2 in this.TotalFields)
            {
                if (!hashtable_1.Contains(obj2.ToString()))
                {
                    hashtable_1.Add(obj2.ToString(), 0f);
                }
            }
            return hashtable_1;
        }

        private Hashtable method_11(Hashtable hashtable_1, Hashtable hashtable_2)
        {
            foreach (object obj2 in this.TotalFields)
            {
                hashtable_1[obj2.ToString()] = float.Parse(hashtable_1[obj2.ToString()].ToString()) + float.Parse(hashtable_2[obj2.ToString()].ToString());
            }
            return hashtable_1;
        }

        private string method_12(int int_1)
        {
            switch (int_1)
            {
                case 1:
                    return "14pt";

                case 2:
                    return "12pt";

                case 3:
                    return "10pt";
            }
            return "9pt";
        }

        private void method_2(Section section_0, string string_3)
        {
            string newValue = section_0.string_0;
            string str2 = (" style=\"font-family: " + this.ReportFont + "; font-weight:bold; font-size:") + this.method_12(section_0.int_0);
            if (section_0.GradientBackground)
            {
                str2 = str2 + "; " + this.string_2.Replace("BackColor", newValue) + "\"";
            }
            else
            {
                str2 = str2 + "\" bgcolor='" + newValue + "' ";
            }
            this.stringBuilder_0.Append(string.Concat(new object[] { "<TR><TD colspan='", this.ReportFields.Count, "' ", str2, " >" }));
            this.stringBuilder_0.Append(section_0.TitlePrefix + string_3);
            this.stringBuilder_0.Append("</TD></TR>" + this.string_1);
        }

        private Hashtable method_3(Section section_0, string string_3)
        {
            Hashtable hashtable = new Hashtable();
            hashtable = this.method_10(hashtable);
            if (section_0 == null)
            {
                section_0 = new Section();
            }
            try
            {
                object obj2;
                this.stringBuilder_0.Append("<TR>" + this.string_1);
                string str = "";
                foreach (Field field in this.arrayList_1)
                {
                    str = " bgcolor='" + field.string_1 + "' ";
                    if (field.Width != 0)
                    {
                        obj2 = str;
                        str = string.Concat(new object[] { obj2, " WIDTH=", field.Width, " " });
                    }
                    str = str + " ALIGN='" + field.string_2 + "' ";
                    this.stringBuilder_0.Append("  <TD " + str + " class='ColumnHeaderStyle'><b>" + field.HeaderName + "</b></TD>" + this.string_1);
                }
                this.stringBuilder_0.Append("</TR>" + this.string_1);
                if ((string_3 == null) || (string_3.Trim() == ""))
                {
                    string_3 = "";
                }
                else
                {
                    string_3 = string_3.Substring(3);
                }
                foreach (DataRow row in this.dataSet_0.Tables[0].Select(string_3))
                {
                    this.stringBuilder_0.Append("<TR>" + this.string_1);
                    foreach (Field field in this.arrayList_1)
                    {
                        str = " bgcolor='" + field.string_0 + "' ";
                        if (field.Width != 0)
                        {
                            obj2 = str;
                            str = string.Concat(new object[] { obj2, " WIDTH=", field.Width, " " });
                        }
                        if (this.TotalFields.Contains(field.FieldName))
                        {
                            str = str + " align='right' ";
                        }
                        str = str + " ALIGN='" + field.string_2 + "' ";
                        this.stringBuilder_0.Append(string.Concat(new object[] { "  <TD ", str, " VALIGN='top' class='DetailData'>", row[field.FieldName], "</TD>", this.string_1 }));
                    }
                    this.stringBuilder_0.Append("</TR>" + this.string_1);
                    try
                    {
                        foreach (object obj3 in this.TotalFields)
                        {
                            hashtable[obj3.ToString()] = float.Parse(hashtable[obj3.ToString()].ToString()) + float.Parse(row[obj3.ToString()].ToString());
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                return hashtable;
            }
            catch (Exception exception)
            {
                this.stringBuilder_0.Append("<p align=CENTER><b>Unable to generate report.<br>" + exception.Message + "</b></p>");
            }
            return hashtable;
        }

        private void method_4(Section section_0, Hashtable hashtable_1)
        {
            string str = "";
            if (section_0.IncludeTotal)
            {
                this.stringBuilder_0.Append("<TR>" + this.string_1);
                foreach (Field field in this.arrayList_1)
                {
                    str = "";
                    if (field.Width != 0)
                    {
                        object obj2 = str;
                        str = string.Concat(new object[] { obj2, " WIDTH=", field.Width, " " });
                    }
                    str = (str + " style=\"font-family: " + this.ReportFont + "; font-size:") + this.method_12(section_0.int_0 + 1) + "; border-style:outline; border-width:1 \" ";
                    if (hashtable_1.Contains(field.FieldName))
                    {
                        this.stringBuilder_0.Append("  <TD " + str + " align='right'><u>Total: " + hashtable_1[field.FieldName].ToString() + "</u></TD> " + this.string_1);
                    }
                    else
                    {
                        this.stringBuilder_0.Append("  <TD " + str + ">&nbsp;</TD>" + this.string_1);
                    }
                }
                this.stringBuilder_0.Append("</TR>");
            }
        }

        private void method_5()
        {
            this.stringBuilder_0.Append("<BR>");
        }

        private Hashtable method_6(Section section_0, string string_3)
        {
            this.int_0++;
            section_0.int_0 = this.int_0;
            ArrayList list = this.method_9(this.dataSet_0, section_0.GroupBy, string_3);
            Hashtable hashtable = new Hashtable();
            this.method_10(hashtable);
            foreach (object obj2 in list)
            {
                Hashtable hashtable2 = new Hashtable();
                this.method_10(hashtable2);
                string str = string_3 + "and " + section_0.GroupBy + "='" + obj2.ToString().Replace("'", "''") + "' ";
                this.method_2(section_0, obj2.ToString());
                if (!((!section_0.IncludeChart || section_0.ChartShowAtBottom) || section_0.bool_0))
                {
                    this.method_7(str, section_0);
                }
                if (section_0.SubSection != null)
                {
                    hashtable2 = this.method_6(section_0.SubSection, str);
                    this.int_0--;
                }
                else
                {
                    hashtable2 = this.method_3(section_0, str);
                    hashtable = this.method_11(hashtable, hashtable2);
                }
                this.method_4(section_0, hashtable2);
                if (!((!section_0.IncludeChart || !section_0.ChartShowAtBottom) || section_0.bool_0))
                {
                    this.method_7(str, section_0);
                }
                section_0.bool_0 = false;
            }
            if (section_0.int_0 < 2)
            {
                this.stringBuilder_0.Append("<TR><TD colspan='" + this.ReportFields.Count + "'>&nbsp;</TD></TR>");
            }
            return hashtable;
        }

        private void method_7(string string_3, Section section_0)
        {
            string chartChangeOnField = section_0.ChartChangeOnField;
            string chartValueField = section_0.ChartValueField;
            bool chartShowBorder = section_0.ChartShowBorder;
            section_0.bool_0 = true;
            string[] strArray2 = new string[] { "#ff0000", "#ffff00", "#ff00ff", "#00ff00", "#00ffff", "#0000ff", "#ff0f0f", "#f0f000", "#ff00f0", "#0f00f0" };
            this.stringBuilder_0.Append(string.Concat(new object[] { "<TR><TD colspan='", this.ReportFields.Count, "' align=CENTER>", this.string_1 }));
            this.stringBuilder_0.Append("<!--- Chart Table starts here -->" + this.string_1);
            if (chartShowBorder)
            {
                this.stringBuilder_0.Append("<TABLE cellpadding=4 cellspacing=1 border=0 bgcolor='#f5f5f5' width=550> ");
            }
            else
            {
                this.stringBuilder_0.Append("<TABLE border=0 cellspacing=5 width=550>");
            }
            if (string_3.ToUpper().StartsWith(" AND "))
            {
                string_3 = string_3.Substring(3);
            }
            try
            {
                ArrayList list = this.method_8(this.dataSet_0, string_3, chartChangeOnField, chartValueField);
                ArrayList list2 = (ArrayList) list[0];
                ArrayList list3 = (ArrayList) list[1];
                float num = 0f;
                foreach (object obj2 in list3)
                {
                    num += float.Parse(obj2.ToString());
                }
                int num2 = 300;
                string str3 = "<TR><TD class='DetailHeader' colspan=3 align='CENTER' width=550><B>ChartTitle</B></TD></TR>";
                this.stringBuilder_0.Append(str3.Replace("ChartTitle", section_0.ChartTitle) + this.string_1);
                object obj3 = "<TR><TD Width=150 class='DetailData' align='right'>Label</TD>" + this.string_1;
                obj3 = string.Concat(new object[] { obj3, " <TD  class='DetailData' Width=", 300 + 0x19, ">", this.string_1 });
                string str4 = ((((((((((string.Concat(new object[] { obj3, "    <TABLE cellpadding=0 cellspacing=0 HEIGHT='20' WIDTH=", 300, " class='TableStyle'>", this.string_1 }) + "       <TR>" + this.string_1) + "          <TD Width=ChartWidth>" + this.string_1) + "             <TABLE class='TableStyle' HEIGHT='20' Width=ChartTWidth border=NOTZERO>" + this.string_1) + "                <TR>" + this.string_1) + "                   <TD Width=ChartWidth bgcolor='BackColor' Width=ChartWidth style=\"FILTER: progid:DXImageTransform.Microsoft.Gradient(gradientType=0,startColorStr=BackColor,endColorStr=#ffffff); \"></TD>" + this.string_1) + "                </TR>" + this.string_1) + "             </TABLE>" + this.string_1) + "          </TD>" + this.string_1) + "          <TD class='DetailData'>&nbsp;Percentage</TD>" + this.string_1) + "       </TR>" + this.string_1) + "    </TABLE>" + "</TD><TD Width=70 class='DetailData'>Value</TD></TR>";
                obj3 = ("<TR>" + this.string_1) + " <TD Width=150  class='DetailData' align='right' bgColor='#e5e5e5'>Label</TD>" + this.string_1;
                this.stringBuilder_0.Append(((string.Concat(new object[] { obj3, " <TD  bgColor='#e5e5e5' class='DetailData' Width=", 300 + 0x19, ">" }) + "Percentage</TD>" + this.string_1) + " <TD Width=25  class='DetailData' bgColor='#e5e5e5'>Value</TD></TR>").Replace("Label", section_0.ChartLabelHeader).Replace("Percentage", section_0.ChartPercentageHeader).Replace("Value", section_0.ChartValueHeader) + this.string_1);
                float num3 = 0f;
                float num4 = 0f;
                float num5 = 0f;
                int index = 0;
                for (int i = 0; i < list2.Count; i++)
                {
                    string str6 = str4;
                    num4 = float.Parse(list3[i].ToString());
                    num3 = (float.Parse(list3[i].ToString()) * num2) / num;
                    num5 = (float.Parse(list3[i].ToString()) * 100f) / num;
                    str6 = str6.Replace("Label", list2[i].ToString());
                    if (num5 == 0.0)
                    {
                        str6 = str6.Replace("BackColor", "#f5f5f5").Replace("NOTZERO", "0");
                    }
                    else
                    {
                        str6 = str6.Replace("BackColor", strArray2[index]).Replace("NOTZERO", "1");
                    }
                    this.stringBuilder_0.Append(str6.Replace("ChartTWidth", decimal.Round((decimal) (num3 + 2f), 0).ToString()).Replace("ChartWidth", decimal.Round((decimal) num3, 0).ToString()).Replace("Value", num4.ToString()).Replace("Percentage", decimal.Round((decimal) num5, 2).ToString() + "%") + this.string_1);
                    index++;
                    if (index == 10)
                    {
                        index = 0;
                    }
                }
            }
            catch (Exception exception)
            {
                this.stringBuilder_0.Append("<TR><TD valign=MIDDLE align=CENTER><b>Unable to Generate Chart.<br>" + exception.Message + "</b></TD></TR>");
            }
            this.stringBuilder_0.Append("</TABLE>" + this.string_1);
            this.stringBuilder_0.Append("<!--- Chart Table ends here -->");
            this.stringBuilder_0.Append("</TD></TR>");
        }

        private ArrayList method_8(DataSet dataSet_1, string string_3, string string_4, string string_5)
        {
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            if ((string_3 == null) || (string_3.Trim() == ""))
            {
                string_3 = "";
            }
            else
            {
                string_3 = string_3.Substring(3);
            }
            foreach (DataRow row in dataSet_1.Tables[0].Select(string_3))
            {
                if (!list2.Contains(row[string_4].ToString()))
                {
                    list2.Add(row[string_4].ToString());
                }
            }
            ArrayList list3 = new ArrayList();
            if (string_3.Trim() != "")
            {
                string_3 = string_3 + " and ";
            }
            foreach (object obj2 in list2)
            {
                DataRow[] rowArray2 = this.dataSet_0.Tables[0].Select(string_3 + string_4 + "='" + obj2.ToString().Replace("'", "''") + "' ");
                if (string_5.Trim().ToUpper() == "COUNT")
                {
                    list3.Add(rowArray2.Length.ToString());
                }
                else
                {
                    float num3 = 0f;
                    foreach (DataRow row2 in rowArray2)
                    {
                        num3 += float.Parse(row2[string_5].ToString());
                    }
                    list3.Add(num3.ToString());
                }
            }
            list.Add(list2);
            list.Add(list3);
            return list;
        }

        private ArrayList method_9(DataSet dataSet_1, string string_3, string string_4)
        {
            ArrayList list = new ArrayList();
            if ((string_4 == null) || (string_4.Trim() == ""))
            {
                string_4 = "";
            }
            else
            {
                string_4 = string_4.Substring(3);
            }
            foreach (DataRow row in dataSet_1.Tables[0].Select(string_4))
            {
                if (!list.Contains(row[string_3].ToString()))
                {
                    list.Add(row[string_3].ToString());
                }
            }
            return list;
        }

        public bool SaveReport(string fileName)
        {
            try
            {
                this.GenerateReport();
                StreamWriter writer = new StreamWriter(fileName);
                writer.Write(this.stringBuilder_0.ToString());
                writer.Flush();
                writer.Close();
                return true;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                return false;
            }
        }

        public ArrayList ReportFields
        {
            get
            {
                return this.arrayList_1;
            }
            set
            {
                this.arrayList_1 = value;
            }
        }

        public DataSet ReportSource
        {
            get
            {
                return this.dataSet_0;
            }
            set
            {
                this.dataSet_0 = value;
            }
        }

        public string ReportTitle
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public ArrayList Sections
        {
            get
            {
                return this.arrayList_0;
            }
            set
            {
                this.arrayList_0 = value;
            }
        }
    }
}

