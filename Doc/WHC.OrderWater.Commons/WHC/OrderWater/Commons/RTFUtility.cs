namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    public static class RTFUtility
    {
        public static string AlignCenter(string s)
        {
            return ("\r\n\\qc\r\n" + s);
        }

        public static string AlignFull(string s)
        {
            return ("\r\n\\qf\r\n" + s);
        }

        public static string AlignLeft(string s)
        {
            return ("\r\n\\ql\r\n" + s);
        }

        public static string AlignRight(string s)
        {
            return ("\r\n\\qr\r\n" + s);
        }

        public static string Bold(string s)
        {
            return ("\\b\r\n" + s + "\\b0\r\n");
        }

        public static string BuildTable(int NumRows, int NumCells, ArrayList values)
        {
            StringBuilder builder = new StringBuilder();
            int num = NumRows * NumCells;
            int count = values.Count;
            while (count <= num)
            {
                values.Add("");
                count++;
            }
            IEnumerator enumerator = values.GetEnumerator();
            enumerator.MoveNext();
            int num3 = 1;
            for (int i = 1; i <= NumRows; i++)
            {
                builder.Append(@"\trowd\trautofit1\intbl");
                num3 = 1;
                count = 1;
                while (count <= NumCells)
                {
                    builder.Append(@"\cellx" + num3);
                    num3++;
                    count++;
                }
                builder.Append("{");
                count = 1;
                while (count <= NumCells)
                {
                    builder.Append(@"\pard " + enumerator.Current.ToString() + @"\cell \pard");
                    enumerator.MoveNext();
                    count++;
                }
                builder.Append("}");
                builder.Append("{");
                builder.Append(@"\trowd\trautofit1\intbl\trqc");
                num3 = 1;
                for (count = 1; count <= NumCells; count++)
                {
                    builder.Append(@"\clpadt100\clpadft3\clpadr100\clpadfr3\clwWidth1\clftsWidth\clwWidth1\clftsWidth1\clbrdrt\brdrs\brdrw10\clbrdrl\brdrs\brdrw10\clbrdrb\brdrs\brdrw10\clbrdrr\brdrs\brdrw10\clNoWrap\cellx" + num3);
                    num3++;
                }
                builder.Append(@"\row }");
            }
            return ("\n\\pard\\par\n{" + builder.ToString() + "}\n\\pard\n");
        }

        public static string FontSize(string s, int n)
        {
            return ("\r\n\\fs" + Convert.ToString((int) (n * 2)) + "\r\n" + s);
        }

        public static string Italic(string s)
        {
            return ("\\i\r\n" + s + "\\i0\r\n");
        }

        public static string LineBreak()
        {
            return LineBreak(1);
        }

        public static string LineBreak(int n)
        {
            string str = "";
            for (int i = 0; i < n; i++)
            {
                str = str + "\\par\r\n";
            }
            return str;
        }

        public static string ParagraphBorder(string s)
        {
            s = s.Replace(@"\par", @"\line");
            return (@"\par {\pard \brdrt \brdrs \brdrw10 \brsp125 \brdrl \brdrs \brdrw10 \brsp125 \brdrb \brdrs \brdrw10 \brsp125 \brdrr \brdrs \brdrw10 \brsp125 " + s + @" \par}");
        }

        public static string RtfToHtml(RichTextBox m_pText)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<html>\r\n");
            m_pText.SelectionStart = 0;
            m_pText.SelectionLength = 1;
            Font selectionFont = m_pText.SelectionFont;
            Color selectionColor = m_pText.SelectionColor;
            Color selectionBackColor = m_pText.SelectionBackColor;
            int num = 0;
            int startIndex = 0;
            while (m_pText.Text.Length > m_pText.SelectionStart)
            {
                m_pText.SelectionStart++;
                m_pText.SelectionLength = 1;
                if ((m_pText.Text.Length == m_pText.SelectionStart) || ((((selectionFont.Name != m_pText.SelectionFont.Name) || (selectionFont.Size != m_pText.SelectionFont.Size)) || ((selectionFont.Style != m_pText.SelectionFont.Style) || (selectionColor != m_pText.SelectionColor))) || (selectionBackColor != m_pText.SelectionBackColor)))
                {
                    string str3 = m_pText.Text.Substring(startIndex, m_pText.SelectionStart - startIndex);
                    string str4 = "#" + selectionColor.R.ToString("X2") + selectionColor.G.ToString("X2") + selectionColor.B.ToString("X2");
                    string str5 = "#" + selectionBackColor.R.ToString("X2") + selectionBackColor.G.ToString("X2") + selectionBackColor.B.ToString("X2");
                    string str = "";
                    string str2 = "";
                    if (selectionFont.Bold)
                    {
                        str = str + "<b>";
                        str2 = str2 + "</b>";
                    }
                    if (selectionFont.Italic)
                    {
                        str = str + "<i>";
                        str2 = str2 + "</i>";
                    }
                    if (selectionFont.Underline)
                    {
                        str = str + "<u>";
                        str2 = str2 + "</u>";
                    }
                    builder.Append(string.Concat(new object[] { "<span\n style=\"color:", str4, "; font-size:", selectionFont.Size, "pt; font-family:", selectionFont.FontFamily.Name, "; background-color:", str5, ";\">", str, str3.Replace("\n", "</br>"), str2 }));
                    startIndex = m_pText.SelectionStart;
                    selectionFont = m_pText.SelectionFont;
                    selectionColor = m_pText.SelectionColor;
                    selectionBackColor = m_pText.SelectionBackColor;
                    num++;
                }
            }
            for (int i = 0; i < num; i++)
            {
                builder.Append("</span>");
            }
            builder.Append("\r\n</html>\r\n");
            return builder.ToString();
        }

        public static string Underline(string s)
        {
            return ("\\ul\r\n" + s + "\\ul0\r\n");
        }
    }
}

