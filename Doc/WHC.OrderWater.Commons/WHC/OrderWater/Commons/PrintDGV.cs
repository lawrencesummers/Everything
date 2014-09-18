namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class PrintDGV
    {
        private static ArrayList arrayList_0 = new ArrayList();
        private static ArrayList arrayList_1 = new ArrayList();
        private static ArrayList arrayList_2 = new ArrayList();
        private static bool bool_0;
        private static bool bool_1 = true;
        private static bool bool_2 = true;
        private static Button button_0;
        private static CheckBox checkBox_0;
        private static ComboBox comboBox_0;
        private static DataGridView dataGridView_0;
        private static int int_0;
        private static int int_1;
        private static int int_2;
        private static int int_3;
        private static int int_4;
        private static int int_5 = 0;
        private static List<string> list_0 = new List<string>();
        private static List<string> list_1 = new List<string>();
        private static PrintDocument printDocument_0 = new PrintDocument();
        private static string string_0 = "";
        private static StringFormat stringFormat_0;
        private static StringFormat stringFormat_1;

        public static void Print_DataGridView(DataGridView dgv1)
        {
            Print_DataGridView(dgv1, "");
        }

        public static void Print_DataGridView(DataGridView dgv1, string title)
        {
            try
            {
                try
                {
                    dataGridView_0 = dgv1;
                    list_1.Clear();
                    foreach (DataGridViewColumn column in dataGridView_0.Columns)
                    {
                        if (column.Visible)
                        {
                            list_1.Add(column.HeaderText);
                        }
                    }
                    PrintOptions options = new PrintOptions(list_1) {
                        PrintTitle = string_0
                    };
                    if (options.ShowDialog() == DialogResult.OK)
                    {
                        string_0 = options.PrintTitle;
                        bool_1 = options.PrintAllRows;
                        bool_2 = options.FitToPageWidth;
                        list_0 = options.GetSelectedColumns();
                        int_4 = 0;
                        PrintPreviewDialog dialog = new PrintPreviewDialog {
                            Document = printDocument_0
                        };
                        printDocument_0.BeginPrint += new PrintEventHandler(PrintDGV.smethod_0);
                        printDocument_0.PrintPage += new PrintPageEventHandler(PrintDGV.smethod_1);
                        if (dialog.ShowDialog() != DialogResult.OK)
                        {
                            printDocument_0.BeginPrint -= new PrintEventHandler(PrintDGV.smethod_0);
                            printDocument_0.PrintPage -= new PrintPageEventHandler(PrintDGV.smethod_1);
                        }
                        else
                        {
                            printDocument_0.Print();
                            printDocument_0.BeginPrint -= new PrintEventHandler(PrintDGV.smethod_0);
                            printDocument_0.PrintPage -= new PrintPageEventHandler(PrintDGV.smethod_1);
                        }
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            finally
            {
            }
        }

        private static void smethod_0(object object_0, object object_1)
        {
            try
            {
                stringFormat_0 = new StringFormat();
                stringFormat_0.Alignment = StringAlignment.Near;
                stringFormat_0.LineAlignment = StringAlignment.Center;
                stringFormat_0.Trimming = StringTrimming.EllipsisCharacter;
                stringFormat_1 = new StringFormat();
                stringFormat_1.LineAlignment = StringAlignment.Center;
                stringFormat_1.FormatFlags = StringFormatFlags.NoWrap;
                stringFormat_1.Trimming = StringTrimming.EllipsisCharacter;
                arrayList_0.Clear();
                arrayList_1.Clear();
                arrayList_2.Clear();
                int_3 = 0;
                int_4 = 0;
                button_0 = new Button();
                checkBox_0 = new CheckBox();
                comboBox_0 = new ComboBox();
                int_0 = 0;
                foreach (DataGridViewColumn column in dataGridView_0.Columns)
                {
                    if (column.Visible && list_0.Contains(column.HeaderText))
                    {
                        int_0 += column.Width;
                    }
                }
                int_2 = 1;
                bool_0 = true;
                int_1 = 0;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private static void smethod_1(object sender, PrintPageEventArgs e)
        {
            int top = e.MarginBounds.Top;
            int left = e.MarginBounds.Left;
            try
            {
                if (int_2 == 1)
                {
                    foreach (DataGridViewColumn column in dataGridView_0.Columns)
                    {
                        if (column.Visible && list_0.Contains(column.HeaderText))
                        {
                            int width;
                            if (bool_2)
                            {
                                width = (int) Math.Floor((double) (((((double) column.Width) / ((double) int_0)) * int_0) * (((double) e.MarginBounds.Width) / ((double) int_0))));
                            }
                            else
                            {
                                width = column.Width;
                            }
                            int_5 = ((int) e.Graphics.MeasureString(column.HeaderText, column.InheritedStyle.Font, width).Height) + 11;
                            arrayList_0.Add(left);
                            arrayList_1.Add(width);
                            arrayList_2.Add(column.GetType());
                            left += width;
                        }
                    }
                }
                while (int_1 <= (dataGridView_0.Rows.Count - 1))
                {
                    int num4;
                    DataGridViewRow row = dataGridView_0.Rows[int_1];
                    if (row.IsNewRow || (!bool_1 && !row.Selected))
                    {
                        int_1++;
                        continue;
                    }
                    int_3 = row.Height;
                    if ((top + int_3) >= (e.MarginBounds.Height + e.MarginBounds.Top))
                    {
                        goto Label_0A74;
                    }
                    if (bool_0)
                    {
                        e.Graphics.DrawString(string_0, new Font(dataGridView_0.Font, FontStyle.Bold), Brushes.Black, (float) e.MarginBounds.Left, (e.MarginBounds.Top - e.Graphics.MeasureString(string_0, new Font(dataGridView_0.Font, FontStyle.Bold), e.MarginBounds.Width).Height) - 13f);
                        string s = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
                        e.Graphics.DrawString(s, new Font(dataGridView_0.Font, FontStyle.Bold), Brushes.Black, (float) (e.MarginBounds.Left + (e.MarginBounds.Width - e.Graphics.MeasureString(s, new Font(dataGridView_0.Font, FontStyle.Bold), e.MarginBounds.Width).Width)), (float) ((e.MarginBounds.Top - e.Graphics.MeasureString(string_0, new Font(new Font(dataGridView_0.Font, FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height) - 13f));
                        top = e.MarginBounds.Top;
                        num4 = 0;
                        foreach (DataGridViewColumn column in dataGridView_0.Columns)
                        {
                            if (column.Visible && list_0.Contains(column.HeaderText))
                            {
                                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle((int) arrayList_0[num4], top, (int) arrayList_1[num4], int_5));
                                e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int) arrayList_0[num4], top, (int) arrayList_1[num4], int_5));
                                e.Graphics.DrawString(column.HeaderText, column.InheritedStyle.Font, new SolidBrush(column.InheritedStyle.ForeColor), new RectangleF((float) ((int) arrayList_0[num4]), (float) top, (float) ((int) arrayList_1[num4]), (float) int_5), stringFormat_0);
                                num4++;
                            }
                        }
                        bool_0 = false;
                        top += int_5;
                    }
                    num4 = 0;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (!cell.OwningColumn.Visible || !list_0.Contains(cell.OwningColumn.HeaderText))
                        {
                            continue;
                        }
                        if ((((Type) arrayList_2[num4]).Name == "DataGridViewTextBoxColumn") || (((Type) arrayList_2[num4]).Name == "DataGridViewLinkColumn"))
                        {
                            e.Graphics.DrawString(cell.Value.ToString(), cell.InheritedStyle.Font, new SolidBrush(cell.InheritedStyle.ForeColor), new RectangleF((float) ((int) arrayList_0[num4]), (float) top, (float) ((int) arrayList_1[num4]), (float) int_3), stringFormat_0);
                        }
                        else
                        {
                            Bitmap bitmap;
                            if (((Type) arrayList_2[num4]).Name == "DataGridViewButtonColumn")
                            {
                                button_0.Text = cell.Value.ToString();
                                button_0.Size = new Size((int) arrayList_1[num4], int_3);
                                bitmap = new Bitmap(button_0.Width, button_0.Height);
                                button_0.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                                e.Graphics.DrawImage(bitmap, new Point((int) arrayList_0[num4], top));
                            }
                            else if (((Type) arrayList_2[num4]).Name == "DataGridViewCheckBoxColumn")
                            {
                                checkBox_0.Size = new Size(14, 14);
                                checkBox_0.Checked = (bool) cell.Value;
                                bitmap = new Bitmap((int) arrayList_1[num4], int_3);
                                Graphics.FromImage(bitmap).FillRectangle(Brushes.White, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                                checkBox_0.DrawToBitmap(bitmap, new Rectangle((bitmap.Width - checkBox_0.Width) / 2, (bitmap.Height - checkBox_0.Height) / 2, checkBox_0.Width, checkBox_0.Height));
                                e.Graphics.DrawImage(bitmap, new Point((int) arrayList_0[num4], top));
                            }
                            else if (((Type) arrayList_2[num4]).Name == "DataGridViewComboBoxColumn")
                            {
                                comboBox_0.Size = new Size((int) arrayList_1[num4], int_3);
                                bitmap = new Bitmap(comboBox_0.Width, comboBox_0.Height);
                                comboBox_0.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                                e.Graphics.DrawImage(bitmap, new Point((int) arrayList_0[num4], top));
                                e.Graphics.DrawString(cell.Value.ToString(), cell.InheritedStyle.Font, new SolidBrush(cell.InheritedStyle.ForeColor), new RectangleF((float) (((int) arrayList_0[num4]) + 1), (float) top, (float) (((int) arrayList_1[num4]) - 0x10), (float) int_3), stringFormat_1);
                            }
                            else if (((Type) arrayList_2[num4]).Name == "DataGridViewImageColumn")
                            {
                                Rectangle rectangle2 = new Rectangle((int) arrayList_0[num4], top, (int) arrayList_1[num4], int_3);
                                Size size = ((Image) cell.FormattedValue).Size;
                                e.Graphics.DrawImage((Image) cell.FormattedValue, new Rectangle(((int) arrayList_0[num4]) + ((rectangle2.Width - size.Width) / 2), top + ((rectangle2.Height - size.Height) / 2), ((Image) cell.FormattedValue).Width, ((Image) cell.FormattedValue).Height));
                            }
                        }
                        e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int) arrayList_0[num4], top, (int) arrayList_1[num4], int_3));
                        num4++;
                    }
                    top += int_3;
                    int_1++;
                    if (int_2 == 1)
                    {
                        int_4++;
                    }
                }
                if (int_4 != 0)
                {
                    smethod_2(e, int_4);
                    e.HasMorePages = false;
                }
                return;
            Label_0A74:
                smethod_2(e, int_4);
                bool_0 = true;
                int_2++;
                e.HasMorePages = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private static void smethod_2(PrintPageEventArgs printPageEventArgs_0, int int_6)
        {
            double count = 0.0;
            if (bool_1)
            {
                if (dataGridView_0.Rows[dataGridView_0.Rows.Count - 1].IsNewRow)
                {
                    count = dataGridView_0.Rows.Count - 2;
                }
                else
                {
                    count = dataGridView_0.Rows.Count - 1;
                }
            }
            else
            {
                count = dataGridView_0.SelectedRows.Count;
            }
            string s = int_2.ToString() + " ֮ " + Math.Ceiling((double) (count / ((double) int_6))).ToString();
            printPageEventArgs_0.Graphics.DrawString(s, dataGridView_0.Font, Brushes.Black, printPageEventArgs_0.MarginBounds.Left + ((printPageEventArgs_0.MarginBounds.Width - printPageEventArgs_0.Graphics.MeasureString(s, dataGridView_0.Font, printPageEventArgs_0.MarginBounds.Width).Width) / 2f), (float) ((printPageEventArgs_0.MarginBounds.Top + printPageEventArgs_0.MarginBounds.Height) + 0x1f));
        }
    }
}

