namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections.Specialized;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    public class ExRichTextBox : RichTextBox
    {
        private float float_0;
        private float float_1;
        private HybridDictionary hybridDictionary_0;
        private HybridDictionary hybridDictionary_1;
        private const int int_0 = 1;
        private const int int_1 = 2;
        private const int int_2 = 3;
        private const int int_3 = 4;
        private const int int_4 = 5;
        private const int int_5 = 6;
        private const int int_6 = 7;
        private const int int_7 = 8;
        private const int int_8 = 0x9ec;
        private const int int_9 = 0x5a0;
        private RtfColor rtfColor_0;
        private RtfColor rtfColor_1;
        private const string string_0 = "UNKNOWN";
        private const string string_1 = @"{\rtf1\ansi\ansicpg936\deff0\deflang1033\deflangfe2052";
        private const string string_2 = @"\viewkind4\uc1\pard\lang2052\cf1\f0\fs20";
        private const string string_3 = @"\cf0\fs17}";
        private string string_4;

        public ExRichTextBox()
        {
            this.string_4 = "}";
            this.rtfColor_0 = RtfColor.Black;
            this.rtfColor_1 = RtfColor.White;
            this.hybridDictionary_0 = new HybridDictionary();
            this.hybridDictionary_0.Add(RtfColor.Aqua, @"\red0\green255\blue255");
            this.hybridDictionary_0.Add(RtfColor.Black, @"\red0\green0\blue0");
            this.hybridDictionary_0.Add(RtfColor.Blue, @"\red0\green0\blue255");
            this.hybridDictionary_0.Add(RtfColor.Fuchsia, @"\red255\green0\blue255");
            this.hybridDictionary_0.Add(RtfColor.Gray, @"\red128\green128\blue128");
            this.hybridDictionary_0.Add(RtfColor.Green, @"\red0\green128\blue0");
            this.hybridDictionary_0.Add(RtfColor.Lime, @"\red0\green255\blue0");
            this.hybridDictionary_0.Add(RtfColor.Maroon, @"\red128\green0\blue0");
            this.hybridDictionary_0.Add(RtfColor.Navy, @"\red0\green0\blue128");
            this.hybridDictionary_0.Add(RtfColor.Olive, @"\red128\green128\blue0");
            this.hybridDictionary_0.Add(RtfColor.Purple, @"\red128\green0\blue128");
            this.hybridDictionary_0.Add(RtfColor.Red, @"\red255\green0\blue0");
            this.hybridDictionary_0.Add(RtfColor.Silver, @"\red192\green192\blue192");
            this.hybridDictionary_0.Add(RtfColor.Teal, @"\red0\green128\blue128");
            this.hybridDictionary_0.Add(RtfColor.White, @"\red255\green255\blue255");
            this.hybridDictionary_0.Add(RtfColor.Yellow, @"\red255\green255\blue0");
            this.hybridDictionary_1 = new HybridDictionary();
            this.hybridDictionary_1.Add(FontFamily.GenericMonospace.Name, @"\fmodern");
            this.hybridDictionary_1.Add(FontFamily.GenericSansSerif, @"\fswiss");
            this.hybridDictionary_1.Add(FontFamily.GenericSerif, @"\froman");
            this.hybridDictionary_1.Add("UNKNOWN", @"\fnil");
            using (Graphics graphics = base.CreateGraphics())
            {
                this.float_0 = graphics.DpiX;
                this.float_1 = graphics.DpiY;
            }
        }

        public ExRichTextBox(RtfColor _textColor) : this()
        {
            this.rtfColor_0 = _textColor;
        }

        public ExRichTextBox(RtfColor _textColor, RtfColor _highlightColor) : this()
        {
            this.rtfColor_0 = _textColor;
            this.rtfColor_1 = _highlightColor;
        }

        public void AppendRtf(string _rtf)
        {
            base.Select(this.TextLength, 0);
            base.SelectedRtf = _rtf;
        }

        public void AppendTextAsRtf(string _text)
        {
            this.AppendTextAsRtf(_text, this.Font);
        }

        public void AppendTextAsRtf(string _text, Font _font)
        {
            this.AppendTextAsRtf(_text, _font, this.rtfColor_0);
        }

        public void AppendTextAsRtf(string _text, Font _font, RtfColor _textColor)
        {
            this.AppendTextAsRtf(_text, _font, _textColor, this.rtfColor_1);
        }

        public void AppendTextAsRtf(string _text, Font _font, RtfColor _textColor, RtfColor _backColor)
        {
            base.Select(this.TextLength, 0);
            this.InsertTextAsRtf(_text, _font, _textColor, _backColor);
        }

        [DllImport("gdiplus.dll")]
        private static extern uint GdipEmfToWmfBits(IntPtr intptr_0, uint uint_0, byte[] byte_0, int int_10, Enum5 enum5_0);
        public void InsertImage(Image _image)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"{\rtf1\ansi\ansicpg936\deff0\deflang1033\deflangfe2052");
            builder.Append(this.method_3(this.Font));
            builder.Append(this.method_1(_image));
            builder.Append(this.method_2(_image));
            builder.Append(this.string_4);
            base.SelectedRtf = builder.ToString();
        }

        public void InsertRtf(string _rtf)
        {
            base.SelectedRtf = _rtf;
        }

        public void InsertTextAsRtf(string _text)
        {
            this.InsertTextAsRtf(_text, this.Font);
        }

        public void InsertTextAsRtf(string _text, Font _font)
        {
            this.InsertTextAsRtf(_text, _font, this.rtfColor_0);
        }

        public void InsertTextAsRtf(string _text, Font _font, RtfColor _textColor)
        {
            this.InsertTextAsRtf(_text, _font, _textColor, this.rtfColor_1);
        }

        public void InsertTextAsRtf(string _text, Font _font, RtfColor _textColor, RtfColor _backColor)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"{\rtf1\ansi\ansicpg936\deff0\deflang1033\deflangfe2052");
            builder.Append(this.method_3(_font));
            builder.Append(this.method_4(_textColor, _backColor));
            builder.Append(this.method_0(_text, _font));
            RichTextBox box = new RichTextBox {
                Font = _font,
                Text = _text
            };
            this.AppendRtf(box.Rtf);
        }

        private string method_0(string string_5, Font font_0)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"\viewkind4\uc1\pard\lang2052\cf1\f0\fs20");
            builder.Append(@"\highlight2");
            if (font_0.Bold)
            {
                builder.Append(@"\b");
            }
            if (font_0.Italic)
            {
                builder.Append(@"\i");
            }
            if (font_0.Strikeout)
            {
                builder.Append(@"\strike");
            }
            if (font_0.Underline)
            {
                builder.Append(@"\ul");
            }
            builder.Append(@"\f0");
            builder.Append(@"\fs");
            builder.Append((int) Math.Round((double) (2f * font_0.SizeInPoints)));
            builder.Append(" ");
            builder.Append(string_5.Replace("\n", @"\par "));
            builder.Append(@"\highlight0");
            if (font_0.Bold)
            {
                builder.Append(@"\b0");
            }
            if (font_0.Italic)
            {
                builder.Append(@"\i0");
            }
            if (font_0.Strikeout)
            {
                builder.Append(@"\strike0");
            }
            if (font_0.Underline)
            {
                builder.Append(@"\ulnone");
            }
            builder.Append(@"\f0");
            builder.Append(@"\fs20");
            builder.Append(@"\cf0\fs17}");
            return builder.ToString();
        }

        private string method_1(Image image_0)
        {
            StringBuilder builder = new StringBuilder();
            int num = (int) Math.Round((double) ((((float) image_0.Width) / this.float_0) * 2540f));
            int num2 = (int) Math.Round((double) ((((float) image_0.Height) / this.float_1) * 2540f));
            int num3 = (int) Math.Round((double) ((((float) image_0.Width) / this.float_0) * 1440f));
            int num4 = (int) Math.Round((double) ((((float) image_0.Height) / this.float_1) * 1440f));
            builder.Append(@"{\pict\wmetafile8");
            builder.Append(@"\picw");
            builder.Append(num);
            builder.Append(@"\pich");
            builder.Append(num2);
            builder.Append(@"\picwgoal");
            builder.Append(num3);
            builder.Append(@"\pichgoal");
            builder.Append(num4);
            builder.Append(" ");
            return builder.ToString();
        }

        private string method_2(Image image_0)
        {
            StringBuilder builder = null;
            MemoryStream stream = null;
            Graphics graphics = null;
            Metafile image = null;
            string str;
            try
            {
                builder = new StringBuilder();
                stream = new MemoryStream();
                using (graphics = base.CreateGraphics())
                {
                    IntPtr hdc = graphics.GetHdc();
                    image = new Metafile(stream, hdc);
                    graphics.ReleaseHdc(hdc);
                }
                using (graphics = Graphics.FromImage(image))
                {
                    graphics.DrawImage(image_0, new Rectangle(0, 0, image_0.Width, image_0.Height));
                }
                IntPtr henhmetafile = image.GetHenhmetafile();
                uint num = GdipEmfToWmfBits(henhmetafile, 0, null, 8, (Enum5) 0);
                byte[] buffer = new byte[num];
                GdipEmfToWmfBits(henhmetafile, num, buffer, 8, (Enum5) 0);
                for (int i = 0; i < buffer.Length; i++)
                {
                    builder.Append(string.Format("{0:X2}", buffer[i]));
                }
                str = builder.ToString();
            }
            finally
            {
                if (graphics != null)
                {
                    graphics.Dispose();
                }
                if (image != null)
                {
                    image.Dispose();
                }
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return str;
        }

        private string method_3(Font font_0)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"{\fonttbl{\f0");
            builder.Append(@"\");
            if (this.hybridDictionary_1.Contains(font_0.FontFamily.Name))
            {
                builder.Append(this.hybridDictionary_1[font_0.FontFamily.Name]);
            }
            else
            {
                builder.Append(this.hybridDictionary_1["UNKNOWN"]);
            }
            builder.Append(@"\fcharset134 ");
            builder.Append(font_0.Name);
            builder.Append(";}}");
            return builder.ToString();
        }

        private string method_4(RtfColor rtfColor_2, RtfColor rtfColor_3)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"{\colortbl ;");
            builder.Append(this.hybridDictionary_0[rtfColor_2]);
            builder.Append(";");
            builder.Append(this.hybridDictionary_0[rtfColor_3]);
            builder.Append(@";}\n");
            return builder.ToString();
        }

        private string method_5(string string_5)
        {
            return string_5.Replace("\0", "");
        }

        public void Print(bool preview)
        {
            new ExRichTextBoxPrintHelper(this).PrintRTF(preview);
        }

        public RtfColor HiglightColor
        {
            get
            {
                return this.rtfColor_1;
            }
            set
            {
                this.rtfColor_1 = value;
            }
        }

        public string Rtf
        {
            get
            {
                return this.method_5(base.Rtf);
            }
            set
            {
                base.Rtf = value;
            }
        }

        public RtfColor TextColor
        {
            get
            {
                return this.rtfColor_0;
            }
            set
            {
                this.rtfColor_0 = value;
            }
        }

        private enum Enum5
        {
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        private struct Struct17
        {
            public const string string_0 = @"\red0\green0\blue0";
            public const string string_1 = @"\red128\green0\blue0";
            public const string string_2 = @"\red0\green128\blue0";
            public const string string_3 = @"\red128\green128\blue0";
            public const string string_4 = @"\red0\green0\blue128";
            public const string string_5 = @"\red128\green0\blue128";
            public const string string_6 = @"\red0\green128\blue128";
            public const string string_7 = @"\red128\green128\blue128";
            public const string string_8 = @"\red192\green192\blue192";
            public const string string_9 = @"\red255\green0\blue0";
            public const string string_10 = @"\red0\green255\blue0";
            public const string string_11 = @"\red255\green255\blue0";
            public const string string_12 = @"\red0\green0\blue255";
            public const string string_13 = @"\red255\green0\blue255";
            public const string string_14 = @"\red0\green255\blue255";
            public const string string_15 = @"\red255\green255\blue255";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        private struct Struct18
        {
            public const string string_0 = @"\fnil";
            public const string string_1 = @"\froman";
            public const string string_2 = @"\fswiss";
            public const string string_3 = @"\fmodern";
            public const string string_4 = @"\fscript";
            public const string string_5 = @"\fdecor";
            public const string string_6 = @"\ftech";
            public const string string_7 = @"\fbidi";
        }
    }
}

