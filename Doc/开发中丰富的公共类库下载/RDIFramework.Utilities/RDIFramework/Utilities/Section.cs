namespace RDIFramework.Utilities
{
    using System;
    using System.Drawing;

    public class Section
    {
        internal bool bool_0;
        public string ChartChangeOnField;
        public string ChartLabelHeader;
        public string ChartPercentageHeader;
        public bool ChartShowAtBottom;
        public bool ChartShowBorder;
        public string ChartTitle;
        public string ChartValueField;
        public string ChartValueHeader;
        internal Color color_0;
        public bool GradientBackground;
        public string GroupBy;
        public bool IncludeChart;
        public bool IncludeFooter;
        public bool IncludeTotal;
        internal int int_0;
        internal string string_0;
        public Section SubSection;
        public string TitlePrefix;

        public Section()
        {
            this.ChartValueField = "Count";
            this.ChartLabelHeader = "Label";
            this.ChartPercentageHeader = "Percentage";
            this.ChartValueHeader = "Value";
            this.SubSection = null;
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.ChartValueField = "Count";
            this.GradientBackground = false;
            this.ChartTitle = "&nbsp;";
        }

        public Section(string groupBy, string titlePrefix)
        {
            this.ChartValueField = "Count";
            this.ChartLabelHeader = "Label";
            this.ChartPercentageHeader = "Percentage";
            this.ChartValueHeader = "Value";
            this.GroupBy = groupBy;
            this.TitlePrefix = titlePrefix;
            this.SubSection = null;
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.ChartValueField = "Count";
            this.GradientBackground = false;
            this.ChartTitle = "&nbsp;";
        }

        public Section(string groupBy, string titlePrefix, Color bgcolor)
        {
            this.ChartValueField = "Count";
            this.ChartLabelHeader = "Label";
            this.ChartPercentageHeader = "Percentage";
            this.ChartValueHeader = "Value";
            this.GroupBy = groupBy;
            this.TitlePrefix = titlePrefix;
            this.SubSection = null;
            this.BackColor = bgcolor;
            this.ChartValueField = "Count";
            this.GradientBackground = false;
            this.ChartTitle = "&nbsp;";
        }

        public Color BackColor
        {
            get
            {
                return this.color_0;
            }
            set
            {
                this.string_0 = Class4.smethod_0(value);
                this.color_0 = value;
            }
        }
    }
}

