namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class DGVPrinter
    {
        protected Form _Owner = null;
        protected double _PrintPreviewZoom = 1.0;
        private Alignment alignment_0 = Alignment.NotSet;
        private bool bool_0 = true;
        private bool bool_1 = true;
        private bool bool_10 = false;
        private bool bool_2 = true;
        private bool bool_3 = false;
        private bool bool_4 = false;
        private bool bool_5 = false;
        private bool bool_6 = true;
        private bool bool_7 = false;
        private bool bool_8 = false;
        private bool bool_9 = false;
        private CellOwnerDrawEventHandler cellOwnerDrawEventHandler_0;
        private Color color_0;
        private Color color_1;
        private Color color_2;
        private Color color_3;
        private DataGridView dataGridView_0 = null;
        private DataGridViewCellStyle dataGridViewCellStyle_0;
        private Dictionary<string, DataGridViewCellStyle> dictionary_0 = new Dictionary<string, DataGridViewCellStyle>();
        private Dictionary<string, float> dictionary_1 = new Dictionary<string, float>();
        private Dictionary<string, DataGridViewCellStyle> dictionary_2 = new Dictionary<string, DataGridViewCellStyle>();
        private float float_0 = 0f;
        private float float_1 = 0f;
        private float float_2 = 0f;
        private float float_3 = 0f;
        private float float_4 = 0f;
        private float float_5 = 0f;
        private float float_6 = 0f;
        private float float_7;
        private float float_8;
        private float float_9;
        private Font font_0;
        private Font font_1;
        private Font font_2;
        private Font font_3;
        private Icon icon_0 = null;
        private IList<Class3> ilist_0;
        private IList ilist_1;
        private IList ilist_2;
        public IList<ImbeddedImage> ImbeddedImageList = new List<ImbeddedImage>();
        private const int int_0 = 0x7fffffff;
        private int int_1 = 0;
        private int int_10;
        private int int_2 = -1;
        private int int_3 = -1;
        private int int_4 = 0;
        private int int_5 = -1;
        private int int_6 = 0;
        private int int_7 = 0;
        private int int_8 = 0;
        private int int_9 = 0;
        private List<float> list_0;
        private List<float> list_1;
        private List<float> list_2 = new List<float>();
        private bool? nullable_0;
        private bool? nullable_1 = false;
        private PrintDialogSettingsClass printDialogSettingsClass_0 = new PrintDialogSettingsClass();
        private PrintDocument printDocument_0 = null;
        private PrintRange printRange_0;
        private RowHeightSetting rowHeightSetting_0 = RowHeightSetting.StringHeight;
        private string string_0;
        private string string_1;
        private string string_2;
        private string string_3;
        private string string_4;
        private string string_5 = " of ";
        private string string_6 = "Page ";
        private string string_7 = " - Part ";
        private string string_8 = "\t";
        private StringAlignment stringAlignment_0;
        private StringAlignment stringAlignment_1;
        private StringFormat stringFormat_0;
        private StringFormat stringFormat_1;
        private StringFormat stringFormat_2;
        private StringFormat stringFormat_3;
        private StringFormat stringFormat_4 = null;
        private StringFormat stringFormat_5 = null;
        private StringFormat stringFormat_6 = null;
        private StringFormatFlags stringFormatFlags_0;
        private StringFormatFlags stringFormatFlags_1;
        private bool xWsOuVkaTs = false;

        public event CellOwnerDrawEventHandler OwnerDraw
        {
            add
            {
                CellOwnerDrawEventHandler handler2;
                CellOwnerDrawEventHandler handler = this.cellOwnerDrawEventHandler_0;
                do
                {
                    handler2 = handler;
                    CellOwnerDrawEventHandler handler3 = (CellOwnerDrawEventHandler) Delegate.Combine(handler2, value);
                    handler = Interlocked.CompareExchange<CellOwnerDrawEventHandler>(ref this.cellOwnerDrawEventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
            remove
            {
                CellOwnerDrawEventHandler handler2;
                CellOwnerDrawEventHandler handler = this.cellOwnerDrawEventHandler_0;
                do
                {
                    handler2 = handler;
                    CellOwnerDrawEventHandler handler3 = (CellOwnerDrawEventHandler) Delegate.Remove(handler2, value);
                    handler = Interlocked.CompareExchange<CellOwnerDrawEventHandler>(ref this.cellOwnerDrawEventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
        }

        public DGVPrinter()
        {
            this.printDocument_0 = new PrintDocument();
            this.printDocument_0.PrintPage += new PrintPageEventHandler(this.printDocument_0_PrintPage);
            this.printDocument_0.BeginPrint += new PrintEventHandler(this.printDocument_0_BeginPrint);
            this.PrintMargins = new Margins(60, 60, 40, 40);
            this.font_3 = new Font("Tahoma", 8f, FontStyle.Regular, GraphicsUnit.Point);
            this.color_3 = Color.Black;
            this.font_0 = new Font("Tahoma", 18f, FontStyle.Bold, GraphicsUnit.Point);
            this.color_0 = Color.Black;
            this.font_1 = new Font("Tahoma", 12f, FontStyle.Bold, GraphicsUnit.Point);
            this.color_1 = Color.Black;
            this.font_2 = new Font("Tahoma", 10f, FontStyle.Bold, GraphicsUnit.Point);
            this.color_2 = Color.Black;
            this.float_7 = 0f;
            this.float_8 = 0f;
            this.float_9 = 0f;
            this.method_3(ref this.stringFormat_0, null, StringAlignment.Center, StringAlignment.Center, StringFormatFlags.NoClip | StringFormatFlags.LineLimit | StringFormatFlags.NoWrap, StringTrimming.Word);
            this.method_3(ref this.stringFormat_1, null, StringAlignment.Center, StringAlignment.Center, StringFormatFlags.NoClip | StringFormatFlags.LineLimit | StringFormatFlags.NoWrap, StringTrimming.Word);
            this.method_3(ref this.stringFormat_2, null, StringAlignment.Center, StringAlignment.Center, StringFormatFlags.NoClip | StringFormatFlags.LineLimit | StringFormatFlags.NoWrap, StringTrimming.Word);
            this.method_3(ref this.stringFormat_3, null, StringAlignment.Far, StringAlignment.Center, StringFormatFlags.NoClip | StringFormatFlags.LineLimit | StringFormatFlags.NoWrap, StringTrimming.Word);
            this.stringFormat_5 = null;
            this.stringFormat_4 = null;
            this.stringFormat_6 = null;
            this.Owner = null;
            this.PrintPreviewZoom = 1.0;
            this.stringAlignment_0 = StringAlignment.Near;
            this.stringFormatFlags_0 = StringFormatFlags.NoClip | StringFormatFlags.LineLimit;
            this.stringAlignment_1 = StringAlignment.Near;
            this.stringFormatFlags_1 = StringFormatFlags.NoClip | StringFormatFlags.LineLimit;
        }

        public DialogResult DisplayPrintDialog()
        {
            PrintDialog dialog = new PrintDialog {
                UseEXDialog = this.printDialogSettingsClass_0.bool_0,
                AllowSelection = this.printDialogSettingsClass_0.AllowSelection,
                AllowSomePages = this.printDialogSettingsClass_0.AllowSomePages,
                AllowCurrentPage = this.printDialogSettingsClass_0.AllowCurrentPage,
                AllowPrintToFile = this.printDialogSettingsClass_0.AllowPrintToFile,
                ShowHelp = this.printDialogSettingsClass_0.ShowHelp,
                ShowNetwork = this.printDialogSettingsClass_0.ShowNetwork,
                Document = this.printDocument_0
            };
            if (!string.IsNullOrEmpty(this.string_0))
            {
                this.printDocument_0.PrinterSettings.PrinterName = this.string_0;
            }
            this.printDocument_0.DefaultPageSettings.Landscape = dialog.PrinterSettings.DefaultPageSettings.Landscape;
            this.printDocument_0.DefaultPageSettings.PaperSize = new PaperSize(dialog.PrinterSettings.DefaultPageSettings.PaperSize.PaperName, dialog.PrinterSettings.DefaultPageSettings.PaperSize.Width, dialog.PrinterSettings.DefaultPageSettings.PaperSize.Height);
            return dialog.ShowDialog();
        }

        public bool EmbeddedPrint(DataGridView dgv, Graphics g, Rectangle area)
        {
            if ((dgv == null) || (null == g))
            {
                throw new Exception("Null Parameter passed to DGVPrinter.");
            }
            this.dataGridView_0 = dgv;
            Margins printMargins = this.PrintMargins;
            this.PrintMargins.Top = area.Top;
            this.PrintMargins.Bottom = 0;
            this.PrintMargins.Left = area.Left;
            this.PrintMargins.Right = 0;
            this.int_6 = area.Height + area.Top;
            this.int_8 = area.Width;
            this.int_7 = area.Width + area.Left;
            this.int_4 = 0;
            this.int_5 = 0x7fffffff;
            this.PrintHeader = false;
            this.PrintFooter = false;
            if (null == this.stringFormat_6)
            {
                this.method_3(ref this.stringFormat_6, dgv.DefaultCellStyle, this.stringAlignment_1, StringAlignment.Near, this.stringFormatFlags_1, StringTrimming.Word);
            }
            this.ilist_1 = new List<object>(dgv.Rows.Count);
            foreach (DataGridViewRow row in (IEnumerable) dgv.Rows)
            {
                if (row.Visible)
                {
                    this.ilist_1.Add(row);
                }
            }
            this.ilist_2 = new List<object>(dgv.Columns.Count);
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column.Visible)
                {
                    this.ilist_2.Add(column);
                }
            }
            SortedList list = new SortedList(this.ilist_2.Count);
            foreach (DataGridViewColumn column in this.ilist_2)
            {
                list.Add(column.DisplayIndex, column);
            }
            this.ilist_2.Clear();
            foreach (object obj2 in list.Values)
            {
                this.ilist_2.Add(obj2);
            }
            foreach (DataGridViewColumn column in this.ilist_2)
            {
                if (this.dictionary_1.ContainsKey(column.Name))
                {
                    this.list_2.Add(this.dictionary_1[column.Name]);
                }
                else
                {
                    this.list_2.Add(-1f);
                }
            }
            this.method_4(g);
            this.int_10 = this.method_6();
            this.int_1 = 0;
            this.int_2 = -1;
            this.int_9 = 0;
            return this.method_8(g);
        }

        public StringFormat GetCellFormat(DataGridView grid)
        {
            if ((grid != null) && (null == this.stringFormat_6))
            {
                this.method_3(ref this.stringFormat_6, grid.Rows[0].Cells[0].InheritedStyle, this.stringAlignment_1, StringAlignment.Near, this.stringFormatFlags_1, StringTrimming.Word);
            }
            if (null == this.stringFormat_6)
            {
                this.stringFormat_6 = new StringFormat(this.stringFormatFlags_1);
            }
            return this.stringFormat_6;
        }

        public StringFormat GetColumnHeaderCellFormat(DataGridView grid)
        {
            if ((grid != null) && (null == this.stringFormat_5))
            {
                this.method_3(ref this.stringFormat_5, grid.Columns[0].HeaderCell.InheritedStyle, this.stringAlignment_0, StringAlignment.Near, this.stringFormatFlags_0, StringTrimming.Word);
            }
            if (null == this.stringFormat_5)
            {
                this.stringFormat_5 = new StringFormat(this.stringFormatFlags_0);
            }
            return this.stringFormat_5;
        }

        public StringFormat GetRowHeaderCellFormat(DataGridView grid)
        {
            if ((grid != null) && (null == this.stringFormat_4))
            {
                this.method_3(ref this.stringFormat_4, grid.Rows[0].HeaderCell.InheritedStyle, this.stringAlignment_0, StringAlignment.Near, this.stringFormatFlags_0, StringTrimming.Word);
            }
            if (null == this.stringFormat_4)
            {
                this.stringFormat_4 = new StringFormat(this.stringFormatFlags_0);
            }
            return this.stringFormat_4;
        }

        private int method_0()
        {
            double num = this.printDocument_0.DefaultPageSettings.Bounds.Width + (3f * this.printDocument_0.DefaultPageSettings.HardMarginY);
            return (int) (num * this.PrintPreviewZoom);
        }

        private int method_1()
        {
            double num = this.printDocument_0.DefaultPageSettings.Bounds.Height + (3f * this.printDocument_0.DefaultPageSettings.HardMarginX);
            return (int) (num * this.PrintPreviewZoom);
        }

        private void method_10(Graphics graphics_0, ref float float_10, Margins margins_0)
        {
            float_10 = (this.int_6 - this.float_4) - margins_0.Bottom;
            float_10 += this.float_9;
            this.method_9(graphics_0, ref float_10, this.string_4, this.font_2, this.color_2, this.stringFormat_2, this.xWsOuVkaTs, margins_0);
            if (!this.bool_7 && this.bool_6)
            {
                string str = this.string_6 + this.int_9.ToString(CultureInfo.CurrentCulture);
                if (this.bool_9)
                {
                    str = str + this.string_5 + this.int_10.ToString();
                }
                if (1 < this.ilist_0.Count)
                {
                    str = str + this.string_7 + ((this.int_1 + 1)).ToString(CultureInfo.CurrentCulture);
                }
                if (!this.bool_8)
                {
                    float_10 -= this.float_5;
                }
                this.method_9(graphics_0, ref float_10, str, this.font_3, this.color_3, this.stringFormat_3, this.bool_5, margins_0);
            }
        }

        private void method_11(Graphics graphics_0, ref float float_10, Class3 class3_0)
        {
            float x = class3_0.margins_0.Left + this.float_2;
            Pen pen = new Pen(this.dataGridView_0.GridColor, 1f);
            for (int i = 0; i < class3_0.object_0.Count; i++)
            {
                DataGridViewColumn column = (DataGridViewColumn) class3_0.object_0[i];
                float width = (class3_0.list_0[i] > (this.int_8 - this.float_2)) ? (this.int_8 - this.float_2) : class3_0.list_0[i];
                DataGridViewCellStyle inheritedStyle = column.HeaderCell.InheritedStyle;
                if (this.ColumnHeaderStyles.ContainsKey(column.Name))
                {
                    inheritedStyle = this.ColumnHeaderStyles[column.Name];
                }
                RectangleF rect = new RectangleF(x, float_10, width, this.float_6);
                graphics_0.FillRectangle(new SolidBrush(inheritedStyle.BackColor), rect);
                graphics_0.DrawString(column.HeaderText, inheritedStyle.Font, new SolidBrush(inheritedStyle.ForeColor), rect, this.stringFormat_5);
                if (this.dataGridView_0.ColumnHeadersBorderStyle != DataGridViewHeaderBorderStyle.None)
                {
                    graphics_0.DrawRectangle(pen, x, float_10, width, this.float_6);
                }
                x += class3_0.list_0[i];
            }
            float_10 += this.float_6 + ((this.dataGridView_0.ColumnHeadersBorderStyle != DataGridViewHeaderBorderStyle.None) ? pen.Width : 0f);
        }

        private float method_12(Graphics graphics_0, float float_10, DataGridViewRow dataGridViewRow_0, Class3 class3_0, float float_11)
        {
            float left = class3_0.margins_0.Left;
            float y = float_10;
            Pen pen = new Pen(this.dataGridView_0.GridColor, 1f);
            float width = (class3_0.float_0 > this.int_8) ? ((float) this.int_8) : class3_0.float_0;
            float height = ((this.list_0[this.int_3] - float_11) > (this.float_0 - y)) ? (this.float_0 - y) : (this.list_0[this.int_3] - float_11);
            DataGridViewCellStyle inheritedStyle = dataGridViewRow_0.InheritedStyle;
            RectangleF rect = new RectangleF(left, y, width, height);
            graphics_0.FillRectangle(new SolidBrush(inheritedStyle.BackColor), rect);
            if (this.PrintRowHeaders.Value)
            {
                RectangleF ef5 = new RectangleF(left, y, this.float_2, height);
                graphics_0.FillRectangle(new SolidBrush(this.RowHeaderCellStyle.BackColor), ef5);
                string s = string.IsNullOrEmpty(dataGridViewRow_0.HeaderCell.EditedFormattedValue.ToString()) ? this.string_8 : dataGridViewRow_0.HeaderCell.EditedFormattedValue.ToString();
                graphics_0.DrawString(s, this.RowHeaderCellStyle.Font, new SolidBrush(this.RowHeaderCellStyle.ForeColor), ef5, this.stringFormat_4);
                if (this.dataGridView_0.RowHeadersBorderStyle != DataGridViewHeaderBorderStyle.None)
                {
                    graphics_0.DrawRectangle(pen, left, y, this.float_2, height);
                }
                left += this.float_2;
            }
            for (int i = 0; i < class3_0.object_0.Count; i++)
            {
                DataGridViewColumn column = (DataGridViewColumn) class3_0.object_0[i];
                string str2 = dataGridViewRow_0.Cells[column.Index].EditedFormattedValue.ToString();
                float num3 = (class3_0.list_0[i] > (this.int_8 - this.float_2)) ? (this.int_8 - this.float_2) : class3_0.list_0[i];
                if (num3 > 0f)
                {
                    StringFormat format = null;
                    DataGridViewCellStyle style = null;
                    if (this.ColumnStyles.ContainsKey(column.Name))
                    {
                        style = this.dictionary_2[column.Name];
                        this.method_3(ref format, style, this.stringFormat_6.Alignment, this.stringFormat_6.LineAlignment, this.stringFormat_6.FormatFlags, this.stringFormat_6.Trimming);
                        Font font = style.Font;
                    }
                    else if (column.HasDefaultCellStyle || dataGridViewRow_0.Cells[column.Index].HasStyle)
                    {
                        style = dataGridViewRow_0.Cells[column.Index].InheritedStyle;
                        this.method_3(ref format, style, this.stringFormat_6.Alignment, this.stringFormat_6.LineAlignment, this.stringFormat_6.FormatFlags, this.stringFormat_6.Trimming);
                        Font font2 = style.Font;
                    }
                    else
                    {
                        format = this.stringFormat_6;
                        style = dataGridViewRow_0.Cells[column.Index].InheritedStyle;
                    }
                    RectangleF ef = new RectangleF(left, y, num3, height);
                    if (!this.method_13(graphics_0, dataGridViewRow_0.Cells[column.Index], ef, style))
                    {
                        RectangleF clipBounds = graphics_0.ClipBounds;
                        graphics_0.FillRectangle(new SolidBrush(style.BackColor), ef);
                        ef = new RectangleF(left + style.Padding.Left, y + style.Padding.Top, (num3 - style.Padding.Right) - style.Padding.Left, (height - style.Padding.Bottom) - style.Padding.Top);
                        graphics_0.SetClip(ef);
                        RectangleF ef3 = new RectangleF(ef.X, ef.Y - float_11, num3, this.list_0[this.int_3]);
                        if ("DataGridViewImageCell" == column.CellType.Name)
                        {
                            this.method_14(graphics_0, (DataGridViewImageCell) dataGridViewRow_0.Cells[column.Index], ef3);
                        }
                        else
                        {
                            graphics_0.DrawString(str2, style.Font, new SolidBrush(style.ForeColor), ef3, format);
                        }
                        graphics_0.SetClip(clipBounds);
                        if (this.dataGridView_0.CellBorderStyle != DataGridViewCellBorderStyle.None)
                        {
                            graphics_0.DrawRectangle(pen, left, y, num3, height);
                        }
                    }
                }
                left += class3_0.list_0[i];
            }
            return height;
        }

        private bool method_13(Graphics graphics_0, DataGridViewCell dataGridViewCell_0, RectangleF rectangleF_0, DataGridViewCellStyle dataGridViewCellStyle_1)
        {
            DGVCellDrawingEventArgs e = new DGVCellDrawingEventArgs(graphics_0, rectangleF_0, dataGridViewCellStyle_1, dataGridViewCell_0.RowIndex, dataGridViewCell_0.ColumnIndex);
            this.OnCellOwnerDraw(e);
            return e.Handled;
        }

        private void method_14(Graphics graphics_0, DataGridViewImageCell dataGridViewImageCell_0, RectangleF rectangleF_0)
        {
            Image image = (Image) dataGridViewImageCell_0.Value;
            Rectangle srcRect = new Rectangle();
            int num = 0;
            int num2 = 0;
            if ((DataGridViewImageCellLayout.Normal == dataGridViewImageCell_0.ImageLayout) || (DataGridViewImageCellLayout.NotSet == dataGridViewImageCell_0.ImageLayout))
            {
                num = image.Width - ((int) rectangleF_0.Width);
                num2 = image.Height - ((int) rectangleF_0.Height);
                if (0 > num)
                {
                    rectangleF_0.Width = srcRect.Width = image.Width;
                }
                else
                {
                    srcRect.Width = (int) rectangleF_0.Width;
                }
                if (0 > num2)
                {
                    rectangleF_0.Height = srcRect.Height = image.Height;
                }
                else
                {
                    srcRect.Height = (int) rectangleF_0.Height;
                }
            }
            else if (DataGridViewImageCellLayout.Stretch == dataGridViewImageCell_0.ImageLayout)
            {
                srcRect.Width = image.Width;
                srcRect.Height = image.Height;
                num = 0;
                num2 = 0;
            }
            else
            {
                float num4;
                srcRect.Width = image.Width;
                srcRect.Height = image.Height;
                float num6 = rectangleF_0.Height / ((float) srcRect.Height);
                float num3 = rectangleF_0.Width / ((float) srcRect.Width);
                if (num6 > num3)
                {
                    num4 = num3;
                    num = 0;
                    num2 = (int) ((srcRect.Height * num4) - rectangleF_0.Height);
                }
                else
                {
                    num4 = num6;
                    num2 = 0;
                    num = (int) ((srcRect.Width * num4) - rectangleF_0.Width);
                }
                rectangleF_0.Width = srcRect.Width * num4;
                rectangleF_0.Height = srcRect.Height * num4;
            }
            DataGridViewContentAlignment alignment = dataGridViewImageCell_0.InheritedStyle.Alignment;
            if (alignment > DataGridViewContentAlignment.MiddleCenter)
            {
                switch (alignment)
                {
                    case DataGridViewContentAlignment.MiddleRight:
                        if (0 > num2)
                        {
                            rectangleF_0.Y -= num2 / 2;
                        }
                        else
                        {
                            srcRect.Y = num2 / 2;
                        }
                        if (0 > num)
                        {
                            rectangleF_0.X -= num;
                        }
                        else
                        {
                            srcRect.X = num;
                        }
                        break;

                    case DataGridViewContentAlignment.BottomLeft:
                        if (0 > num2)
                        {
                            rectangleF_0.Y -= num2;
                        }
                        else
                        {
                            srcRect.Y = num2;
                        }
                        srcRect.X = 0;
                        break;

                    case DataGridViewContentAlignment.BottomCenter:
                        if (0 > num2)
                        {
                            rectangleF_0.Y -= num2;
                        }
                        else
                        {
                            srcRect.Y = num2;
                        }
                        if (0 > num)
                        {
                            rectangleF_0.X -= num / 2;
                        }
                        else
                        {
                            srcRect.X = num / 2;
                        }
                        break;

                    case DataGridViewContentAlignment.BottomRight:
                        if (0 > num2)
                        {
                            rectangleF_0.Y -= num2;
                        }
                        else
                        {
                            srcRect.Y = num2;
                        }
                        if (0 > num)
                        {
                            rectangleF_0.X -= num;
                        }
                        else
                        {
                            srcRect.X = num;
                        }
                        break;
                }
            }
            else
            {
                switch (alignment)
                {
                    case DataGridViewContentAlignment.NotSet:
                        if (0 <= num2)
                        {
                            srcRect.Y = num2 / 2;
                            break;
                        }
                        rectangleF_0.Y -= num2 / 2;
                        break;

                    case DataGridViewContentAlignment.TopLeft:
                        srcRect.Y = 0;
                        srcRect.X = 0;
                        goto Label_0487;

                    case DataGridViewContentAlignment.TopCenter:
                        srcRect.Y = 0;
                        if (0 <= num)
                        {
                            srcRect.X = num / 2;
                        }
                        else
                        {
                            rectangleF_0.X -= num / 2;
                        }
                        goto Label_0487;

                    case (DataGridViewContentAlignment.TopCenter | DataGridViewContentAlignment.TopLeft):
                        goto Label_0487;

                    case DataGridViewContentAlignment.TopRight:
                        srcRect.Y = 0;
                        if (0 <= num)
                        {
                            srcRect.X = num;
                        }
                        else
                        {
                            rectangleF_0.X -= num;
                        }
                        goto Label_0487;

                    case DataGridViewContentAlignment.MiddleLeft:
                        if (0 > num2)
                        {
                            rectangleF_0.Y -= num2 / 2;
                        }
                        else
                        {
                            srcRect.Y = num2 / 2;
                        }
                        srcRect.X = 0;
                        goto Label_0487;

                    case DataGridViewContentAlignment.MiddleCenter:
                        if (0 > num2)
                        {
                            rectangleF_0.Y -= num2 / 2;
                        }
                        else
                        {
                            srcRect.Y = num2 / 2;
                        }
                        if (0 > num)
                        {
                            rectangleF_0.X -= num / 2;
                        }
                        else
                        {
                            srcRect.X = num / 2;
                        }
                        goto Label_0487;

                    default:
                        goto Label_0487;
                }
                if (0 > num)
                {
                    rectangleF_0.X -= num / 2;
                }
                else
                {
                    srcRect.X = num / 2;
                }
            }
        Label_0487:
            graphics_0.DrawImage(image, rectangleF_0, srcRect, GraphicsUnit.Pixel);
        }

        private void method_2()
        {
            DataGridViewRow current;
            int num3;
            if (!this.PrintColumnHeaders.HasValue)
            {
                this.PrintColumnHeaders = new bool?(this.dataGridView_0.Columns[0].HeaderCell.Visible);
            }
            if (!this.PrintRowHeaders.HasValue)
            {
                this.PrintRowHeaders = new bool?(this.dataGridView_0.RowHeadersVisible);
            }
            if (null == this.RowHeaderCellStyle)
            {
                this.RowHeaderCellStyle = this.dataGridView_0.Rows[0].HeaderCell.InheritedStyle;
            }
            if (null == this.stringFormat_5)
            {
                this.method_3(ref this.stringFormat_5, this.dataGridView_0.Columns[0].HeaderCell.InheritedStyle, this.stringAlignment_0, StringAlignment.Near, this.stringFormatFlags_0, StringTrimming.Word);
            }
            if (null == this.stringFormat_4)
            {
                this.method_3(ref this.stringFormat_4, this.RowHeaderCellStyle, this.stringAlignment_0, StringAlignment.Near, this.stringFormatFlags_0, StringTrimming.Word);
            }
            if (null == this.stringFormat_6)
            {
                this.method_3(ref this.stringFormat_6, this.dataGridView_0.DefaultCellStyle, this.stringAlignment_1, StringAlignment.Near, this.stringFormatFlags_1, StringTrimming.Word);
            }
            int num = (int) Math.Round((double) this.printDocument_0.DefaultPageSettings.HardMarginX);
            int num2 = (int) Math.Round((double) this.printDocument_0.DefaultPageSettings.HardMarginY);
            if (this.printDocument_0.DefaultPageSettings.Landscape)
            {
                num3 = (int) Math.Round((double) this.printDocument_0.DefaultPageSettings.PrintableArea.Height);
            }
            else
            {
                num3 = (int) Math.Round((double) this.printDocument_0.DefaultPageSettings.PrintableArea.Width);
            }
            this.int_6 = this.printDocument_0.DefaultPageSettings.Bounds.Height;
            this.int_7 = this.printDocument_0.DefaultPageSettings.Bounds.Width;
            this.PrintMargins = this.printDocument_0.DefaultPageSettings.Margins;
            this.PrintMargins.Right = (num > this.PrintMargins.Right) ? num : this.PrintMargins.Right;
            this.PrintMargins.Left = (num > this.PrintMargins.Left) ? num : this.PrintMargins.Left;
            this.PrintMargins.Top = (num2 > this.PrintMargins.Top) ? num2 : this.PrintMargins.Top;
            this.PrintMargins.Bottom = (num2 > this.PrintMargins.Bottom) ? num2 : this.PrintMargins.Bottom;
            this.int_8 = (this.int_7 - this.PrintMargins.Left) - this.PrintMargins.Right;
            this.int_8 = (this.int_8 > num3) ? num3 : this.int_8;
            this.printRange_0 = this.printDocument_0.PrinterSettings.PrintRange;
            if (PrintRange.SomePages == this.printRange_0)
            {
                this.int_4 = this.printDocument_0.PrinterSettings.FromPage;
                this.int_5 = this.printDocument_0.PrinterSettings.ToPage;
            }
            else
            {
                this.int_4 = 0;
                this.int_5 = 0x7fffffff;
            }
            if (PrintRange.Selection == this.printRange_0)
            {
                IEnumerator enumerator;
                SortedList list2;
                if (0 != this.dataGridView_0.SelectedRows.Count)
                {
                    list2 = new SortedList(this.dataGridView_0.SelectedRows.Count);
                    using (enumerator = this.dataGridView_0.SelectedRows.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            current = (DataGridViewRow) enumerator.Current;
                            list2.Add(current.Index, current);
                        }
                    }
                    list2.Values.GetEnumerator();
                    this.ilist_1 = new List<object>(list2.Count);
                    foreach (object obj2 in list2.Values)
                    {
                        this.ilist_1.Add(obj2);
                    }
                    this.ilist_2 = new List<object>(this.dataGridView_0.Columns.Count);
                    foreach (DataGridViewColumn column in this.dataGridView_0.Columns)
                    {
                        if (column.Visible)
                        {
                            this.ilist_2.Add(column);
                        }
                    }
                }
                else
                {
                    SortedList list;
                    if (0 != this.dataGridView_0.SelectedColumns.Count)
                    {
                        this.ilist_1 = this.dataGridView_0.Rows;
                        list = new SortedList(this.dataGridView_0.SelectedColumns.Count);
                        using (enumerator = this.dataGridView_0.SelectedColumns.GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                current = (DataGridViewRow) enumerator.Current;
                                list.Add(current.Index, current);
                            }
                        }
                        this.ilist_2 = new List<object>(list.Count);
                        foreach (object obj2 in list.Values)
                        {
                            this.ilist_2.Add(obj2);
                        }
                    }
                    else
                    {
                        list2 = new SortedList(this.dataGridView_0.SelectedCells.Count);
                        list = new SortedList(this.dataGridView_0.SelectedCells.Count);
                        foreach (DataGridViewCell cell in this.dataGridView_0.SelectedCells)
                        {
                            int columnIndex = cell.ColumnIndex;
                            int rowIndex = cell.RowIndex;
                            if (!list2.Contains(rowIndex))
                            {
                                list2.Add(rowIndex, this.dataGridView_0.Rows[rowIndex]);
                            }
                            if (!list.Contains(columnIndex))
                            {
                                list.Add(columnIndex, this.dataGridView_0.Columns[columnIndex]);
                            }
                        }
                        this.ilist_1 = new List<object>(list2.Count);
                        foreach (object obj2 in list2.Values)
                        {
                            this.ilist_1.Add(obj2);
                        }
                        this.ilist_2 = new List<object>(list.Count);
                        foreach (object obj2 in list.Values)
                        {
                            this.ilist_2.Add(obj2);
                        }
                    }
                }
            }
            else if (PrintRange.CurrentPage == this.printRange_0)
            {
                this.ilist_1 = new List<object>(this.dataGridView_0.DisplayedRowCount(true));
                this.ilist_2 = new List<object>(this.dataGridView_0.Columns.Count);
                for (int i = this.dataGridView_0.FirstDisplayedScrollingRowIndex; i < (this.dataGridView_0.FirstDisplayedScrollingRowIndex + this.dataGridView_0.DisplayedRowCount(true)); i++)
                {
                    current = this.dataGridView_0.Rows[i];
                    if (current.Visible)
                    {
                        this.ilist_1.Add(current);
                    }
                }
                this.ilist_2 = new List<object>(this.dataGridView_0.Columns.Count);
                foreach (DataGridViewColumn column in this.dataGridView_0.Columns)
                {
                    if (column.Visible)
                    {
                        this.ilist_2.Add(column);
                    }
                }
            }
            else
            {
                this.ilist_1 = new List<object>(this.dataGridView_0.Rows.Count);
                foreach (DataGridViewRow row in (IEnumerable) this.dataGridView_0.Rows)
                {
                    if (!(!row.Visible || row.IsNewRow))
                    {
                        this.ilist_1.Add(row);
                    }
                }
                this.ilist_2 = new List<object>(this.dataGridView_0.Columns.Count);
                foreach (DataGridViewColumn column in this.dataGridView_0.Columns)
                {
                    if (column.Visible)
                    {
                        this.ilist_2.Add(column);
                    }
                }
            }
            int num5 = 1;
            if (RightToLeft.Yes == this.dataGridView_0.RightToLeft)
            {
                num5 = -1;
            }
            SortedList list3 = new SortedList(this.ilist_2.Count);
            foreach (DataGridViewColumn column in this.ilist_2)
            {
                list3.Add(num5 * column.DisplayIndex, column);
            }
            this.ilist_2.Clear();
            foreach (object obj2 in list3.Values)
            {
                this.ilist_2.Add(obj2);
            }
            foreach (DataGridViewColumn column in this.ilist_2)
            {
                if (this.dictionary_1.ContainsKey(column.Name))
                {
                    this.list_2.Add(this.dictionary_1[column.Name]);
                }
                else
                {
                    this.list_2.Add(-1f);
                }
            }
            this.method_4(this.printDocument_0.PrinterSettings.CreateMeasurementGraphics());
            this.int_10 = this.method_6();
        }

        private void method_3(ref StringFormat stringFormat_7, DataGridViewCellStyle dataGridViewCellStyle_1, StringAlignment stringAlignment_2, StringAlignment stringAlignment_3, StringFormatFlags stringFormatFlags_2, StringTrimming stringTrimming_0)
        {
            if (null == stringFormat_7)
            {
                stringFormat_7 = new StringFormat();
            }
            stringFormat_7.Alignment = stringAlignment_2;
            stringFormat_7.LineAlignment = stringAlignment_3;
            stringFormat_7.FormatFlags = stringFormatFlags_2;
            stringFormat_7.Trimming = stringTrimming_0;
            if ((this.dataGridView_0 != null) && (RightToLeft.Yes == this.dataGridView_0.RightToLeft))
            {
                stringFormat_7.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
            }
            if (null != dataGridViewCellStyle_1)
            {
                DataGridViewContentAlignment alignment = dataGridViewCellStyle_1.Alignment;
                if (alignment.ToString().Contains("Center"))
                {
                    stringFormat_7.Alignment = StringAlignment.Center;
                }
                else if (alignment.ToString().Contains("Left"))
                {
                    stringFormat_7.Alignment = StringAlignment.Near;
                }
                else if (alignment.ToString().Contains("Right"))
                {
                    stringFormat_7.Alignment = StringAlignment.Far;
                }
                if (alignment.ToString().Contains("Top"))
                {
                    stringFormat_7.LineAlignment = StringAlignment.Near;
                }
                else if (alignment.ToString().Contains("Middle"))
                {
                    stringFormat_7.LineAlignment = StringAlignment.Center;
                }
                else if (alignment.ToString().Contains("Bottom"))
                {
                    stringFormat_7.LineAlignment = StringAlignment.Far;
                }
            }
        }

        private void method_4(Graphics graphics_0)
        {
            int num2;
            StringFormat format;
            DataGridViewColumn column;
            SizeF size;
            this.list_0 = new List<float>(this.ilist_1.Count);
            this.list_1 = new List<float>(this.ilist_2.Count);
            this.float_3 = 0f;
            this.float_4 = 0f;
            Font font = this.dataGridView_0.ColumnHeadersDefaultCellStyle.Font;
            if (null == font)
            {
                font = this.dataGridView_0.DefaultCellStyle.Font;
            }
            for (num2 = 0; num2 < this.ilist_2.Count; num2++)
            {
                column = (DataGridViewColumn) this.ilist_2[num2];
                format = null;
                DataGridViewCellStyle inheritedStyle = null;
                if (this.ColumnHeaderStyles.ContainsKey(column.Name))
                {
                    inheritedStyle = this.dictionary_0[column.Name];
                    this.method_3(ref format, inheritedStyle, this.stringFormat_6.Alignment, this.stringFormat_6.LineAlignment, this.stringFormat_6.FormatFlags, this.stringFormat_6.Trimming);
                }
                else if (column.HasDefaultCellStyle)
                {
                    inheritedStyle = column.HeaderCell.InheritedStyle;
                    this.method_3(ref format, inheritedStyle, this.stringFormat_6.Alignment, this.stringFormat_6.LineAlignment, this.stringFormat_6.FormatFlags, this.stringFormat_6.Trimming);
                }
                else
                {
                    format = this.stringFormat_5;
                    inheritedStyle = this.dataGridView_0.DefaultCellStyle;
                }
                float width = 0f;
                if (0f <= this.list_2[num2])
                {
                    width = this.list_2[num2];
                }
                else
                {
                    width = this.int_8;
                }
                size = graphics_0.MeasureString(column.HeaderText, inheritedStyle.Font, new SizeF(width, 2.147484E+09f), this.stringFormat_5);
                this.list_1.Add(size.Width);
                this.float_6 = (this.float_6 < size.Height) ? size.Height : this.float_6;
            }
            if (this.bool_6)
            {
                this.float_5 = graphics_0.MeasureString("Page", this.font_3, this.int_8, this.stringFormat_3).Height;
            }
            if (this.PrintHeader)
            {
                if (!(!this.bool_7 || this.bool_8))
                {
                    this.float_3 += this.float_5;
                }
                if (!string.IsNullOrEmpty(this.string_1))
                {
                    this.float_3 += graphics_0.MeasureString(this.string_1, this.font_0, this.int_8, this.stringFormat_0).Height;
                }
                if (!string.IsNullOrEmpty(this.string_3))
                {
                    this.float_3 += graphics_0.MeasureString(this.string_3, this.font_1, this.int_8, this.stringFormat_1).Height;
                }
                if (this.PrintColumnHeaders.Value)
                {
                    this.float_3 += this.float_6;
                }
                this.float_3 += this.float_7 + this.float_8;
            }
            if (this.PrintFooter)
            {
                if (!string.IsNullOrEmpty(this.string_4))
                {
                    this.float_4 += graphics_0.MeasureString(this.string_4, this.font_2, this.int_8, this.stringFormat_2).Height;
                }
                if (!(this.bool_7 || !this.bool_8))
                {
                    this.float_4 += this.float_5;
                }
                this.float_4 += this.float_9;
            }
            num2 = 0;
            while (num2 < this.ilist_1.Count)
            {
                DataGridViewRow row = (DataGridViewRow) this.ilist_1[num2];
                this.list_0.Add(0f);
                if (this.PrintRowHeaders.Value)
                {
                    string text = string.IsNullOrEmpty(row.HeaderCell.EditedFormattedValue.ToString()) ? this.string_8 : row.HeaderCell.EditedFormattedValue.ToString();
                    SizeF ef3 = graphics_0.MeasureString(text, font);
                    this.float_2 = (this.float_2 < ef3.Width) ? ef3.Width : this.float_2;
                }
                for (int i = 0; i < this.ilist_2.Count; i++)
                {
                    float num4;
                    column = (DataGridViewColumn) this.ilist_2[i];
                    string str2 = row.Cells[column.Index].EditedFormattedValue.ToString();
                    format = null;
                    DataGridViewCellStyle defaultCellStyle = null;
                    if (this.ColumnStyles.ContainsKey(column.Name))
                    {
                        defaultCellStyle = this.dictionary_2[column.Name];
                        this.method_3(ref format, defaultCellStyle, this.stringFormat_6.Alignment, this.stringFormat_6.LineAlignment, this.stringFormat_6.FormatFlags, this.stringFormat_6.Trimming);
                    }
                    else if (column.HasDefaultCellStyle || row.Cells[column.Index].HasStyle)
                    {
                        defaultCellStyle = row.Cells[column.Index].InheritedStyle;
                        this.method_3(ref format, defaultCellStyle, this.stringFormat_6.Alignment, this.stringFormat_6.LineAlignment, this.stringFormat_6.FormatFlags, this.stringFormat_6.Trimming);
                    }
                    else
                    {
                        format = this.stringFormat_6;
                        defaultCellStyle = this.dataGridView_0.DefaultCellStyle;
                    }
                    if (RowHeightSetting.CellHeight == this.RowHeight)
                    {
                        size = (SizeF) row.Cells[column.Index].Size;
                    }
                    else
                    {
                        size = graphics_0.MeasureString(str2, defaultCellStyle.Font);
                    }
                    if ((0f <= this.list_2[i]) || (size.Width > this.int_8))
                    {
                        int num8;
                        int num9;
                        if (0f <= this.list_2[i])
                        {
                            this.list_1[i] = this.list_2[i];
                        }
                        else if (size.Width > this.int_8)
                        {
                            this.list_1[i] = this.int_8;
                        }
                        num4 = (graphics_0.MeasureString(str2, defaultCellStyle.Font, new SizeF((this.list_1[i] - defaultCellStyle.Padding.Left) - defaultCellStyle.Padding.Right, 2.147484E+09f), format, out num8, out num9).Height + defaultCellStyle.Padding.Top) + defaultCellStyle.Padding.Bottom;
                        this.list_0[num2] = (this.list_0[num2] < num4) ? num4 : this.list_0[num2];
                    }
                    else
                    {
                        float num5 = (size.Width + defaultCellStyle.Padding.Left) + defaultCellStyle.Padding.Right;
                        num4 = (size.Height + defaultCellStyle.Padding.Top) + defaultCellStyle.Padding.Bottom;
                        this.list_1[i] = (this.list_1[i] < num5) ? num5 : this.list_1[i];
                        this.list_0[num2] = (this.list_0[num2] < num4) ? num4 : this.list_0[num2];
                    }
                }
                num2++;
            }
            this.ilist_0 = new List<Class3>();
            this.ilist_0.Add(new Class3(this.PrintMargins, this.ilist_2.Count));
            int num = 0;
            this.ilist_0[0].float_0 = this.float_2;
            for (num2 = 0; num2 < this.ilist_2.Count; num2++)
            {
                float num7 = (this.list_2[num2] >= 0f) ? this.list_2[num2] : this.list_1[num2];
                if ((this.int_8 < (this.ilist_0[num].float_0 + num7)) && (num2 != 0))
                {
                    this.ilist_0.Add(new Class3(this.PrintMargins, this.ilist_2.Count));
                    num++;
                    this.ilist_0[num].float_0 = this.float_2;
                }
                this.ilist_0[num].object_0.Add(this.ilist_2[num2]);
                this.ilist_0[num].list_0.Add(this.list_1[num2]);
                this.ilist_0[num].list_1.Add(this.list_2[num2]);
                Class3 local1 = this.ilist_0[num];
                local1.float_0 += num7;
            }
            for (num2 = 0; num2 < this.ilist_0.Count; num2++)
            {
                this.method_5(graphics_0, this.ilist_0[num2]);
            }
        }

        private void method_5(Graphics graphics_0, Class3 class3_0)
        {
            int num3;
            float num4;
            float num = this.float_2;
            float num2 = 0f;
            for (num3 = 0; num3 < class3_0.list_1.Count; num3++)
            {
                if (class3_0.list_1[num3] >= 0f)
                {
                    num += class3_0.list_1[num3];
                }
            }
            for (num3 = 0; num3 < class3_0.list_0.Count; num3++)
            {
                if (class3_0.list_1[num3] < 0f)
                {
                    num2 += class3_0.list_0[num3];
                }
            }
            if (this.bool_10 && (0f < num2))
            {
                num4 = (this.int_8 - num) / num2;
            }
            else
            {
                num4 = 1f;
            }
            class3_0.float_0 = this.float_2;
            for (num3 = 0; num3 < class3_0.list_0.Count; num3++)
            {
                if (class3_0.list_1[num3] >= 0f)
                {
                    class3_0.list_0[num3] = class3_0.list_1[num3];
                }
                else
                {
                    class3_0.list_0[num3] *= num4;
                }
                class3_0.float_0 += class3_0.list_0[num3];
            }
            if (Alignment.Left == this.alignment_0)
            {
                class3_0.margins_0.Right = (this.int_7 - class3_0.margins_0.Left) - ((int) class3_0.float_0);
                if (0 > class3_0.margins_0.Right)
                {
                    class3_0.margins_0.Right = 0;
                }
            }
            else if (Alignment.Right == this.alignment_0)
            {
                class3_0.margins_0.Left = (this.int_7 - class3_0.margins_0.Right) - ((int) class3_0.float_0);
                if (0 > class3_0.margins_0.Left)
                {
                    class3_0.margins_0.Left = 0;
                }
            }
            else if (Alignment.Center == this.alignment_0)
            {
                class3_0.margins_0.Left = (this.int_7 - ((int) class3_0.float_0)) / 2;
                if (0 > class3_0.margins_0.Left)
                {
                    class3_0.margins_0.Left = 0;
                }
                class3_0.margins_0.Right = class3_0.margins_0.Left;
            }
        }

        private int method_6()
        {
            int num = 1;
            float num2 = 0f;
            float num3 = (((this.int_6 - this.float_3) - this.float_4) - this.PrintMargins.Top) - this.PrintMargins.Bottom;
            if (this.int_5 < 0x7fffffff)
            {
                return this.int_5;
            }
            for (int i = 0; i < this.list_0.Count; i++)
            {
                if ((num2 + this.list_0[i]) > num3)
                {
                    num++;
                    num2 = 0f;
                }
                num2 += this.list_0[i];
            }
            return num;
        }

        private bool method_7()
        {
            this.int_1++;
            return (this.int_1 < this.ilist_0.Count);
        }

        private bool method_8(Graphics graphics_0)
        {
            // This item is obfuscated and can not be translated.
            float num2;
            float num3;
            bool flag = false;
            bool flag2 = false;
            float top = this.ilist_0[this.int_1].margins_0.Top;
            this.int_9++;
            if ((this.int_9 >= this.int_4) && (this.int_9 <= this.int_5))
            {
                flag2 = true;
            }
            this.float_0 = (this.int_6 - this.float_4) - this.ilist_0[this.int_1].margins_0.Bottom;
            while (!flag2)
            {
                top = this.ilist_0[this.int_1].margins_0.Top + this.float_3;
                bool flag5 = false;
                this.int_3 = this.int_2 + 1;
                num2 = (this.int_2 < this.list_0.Count) ? this.list_0[this.int_3] : 0f;
                while ((this.list_0[this.int_3] - this.float_1) > (this.float_0 - top))
                {
                    num3 = this.float_0 - top;
                    top += num3;
                    if ((this.float_1 + num3) >= num2)
                    {
                        this.float_1 = 0f;
                        this.int_2++;
                        this.int_3++;
                    }
                    else
                    {
                        this.float_1 += num3;
                        flag5 = true;
                    }
                    num2 = (this.int_3 < this.list_0.Count) ? this.list_0[this.int_3] : 0f;
                    if (((0f == this.float_1) && this.bool_2) && ((top + num2) >= this.float_0))
                    {
                        flag5 = true;
                    }
                    if ((0f == this.float_1) && (top >= this.float_0))
                    {
                        flag5 = true;
                    }
                    if ((0f == this.float_1) && (this.int_2 >= (this.ilist_1.Count - 1)))
                    {
                        flag5 = true;
                    }
                    if (flag5)
                    {
                        goto Label_0236;
                    }
                }
                goto Label_00F7;
            Label_0236:
                this.int_9++;
                if ((this.int_9 >= this.int_4) && (this.int_9 <= this.int_5))
                {
                    flag2 = true;
                }
                if (!(0f == this.float_1))
                {
                    flag = true;
                }
                else if ((this.int_2 >= (this.ilist_1.Count - 1)) || (this.int_9 > this.int_5))
                {
                    flag = this.method_7();
                    this.int_2 = -1;
                    this.int_9 = 0;
                    return flag;
                }
            }
            top = this.ilist_0[this.int_1].margins_0.Top;
            if (this.PrintHeader)
            {
                List<ImbeddedImage> list = new List<ImbeddedImage>();
                foreach (ImbeddedImage image in this.ImbeddedImageList)
                {
                    if (image.ImageLocation == Location.Header)
                    {
                        list.Add(image);
                    }
                }
                Extensions.DrawImbeddedImage<ImbeddedImage>(list, graphics_0, this.int_8, this.int_6, this.ilist_0[this.int_1].margins_0);
                if (this.bool_7 && this.bool_6)
                {
                    string str = this.string_6 + this.int_9.ToString(CultureInfo.CurrentCulture);
                    if (this.bool_9)
                    {
                        str = str + this.string_5 + this.int_10.ToString();
                    }
                    if (1 < this.ilist_0.Count)
                    {
                        str = str + this.string_7 + ((this.int_1 + 1)).ToString(CultureInfo.CurrentCulture);
                    }
                    this.method_9(graphics_0, ref top, str, this.font_3, this.color_3, this.stringFormat_3, this.bool_5, this.ilist_0[this.int_1].margins_0);
                    if (!this.bool_8)
                    {
                        top -= this.float_5;
                    }
                }
                if (!string.IsNullOrEmpty(this.string_1))
                {
                    this.method_9(graphics_0, ref top, this.string_1, this.font_0, this.color_0, this.stringFormat_0, this.bool_3, this.ilist_0[this.int_1].margins_0);
                }
                top += this.float_7;
                if (!string.IsNullOrEmpty(this.string_3))
                {
                    this.method_9(graphics_0, ref top, this.string_3, this.font_1, this.color_1, this.stringFormat_1, this.bool_4, this.ilist_0[this.int_1].margins_0);
                }
                top += this.float_8;
            }
            if (this.PrintColumnHeaders.Value)
            {
                this.method_11(graphics_0, ref top, this.ilist_0[this.int_1]);
            }
            List<ImbeddedImage> list3 = new List<ImbeddedImage>();
            foreach (ImbeddedImage image in this.ImbeddedImageList)
            {
                if (image.ImageLocation == Location.Header)
                {
                    list3.Add(image);
                }
            }
            Extensions.DrawImbeddedImage<ImbeddedImage>(list3, graphics_0, this.int_8, this.int_6, this.ilist_0[this.int_1].margins_0);
            bool flag3 = true;
            this.int_3 = this.int_2 + 1;
            if (this.int_3 >= this.ilist_1.Count)
            {
                return false;
            }
            num2 = (this.int_2 < this.list_0.Count) ? this.list_0[this.int_3] : 0f;
            while (true)
            {
                num3 = this.method_12(graphics_0, top, (DataGridViewRow) this.ilist_1[this.int_3], this.ilist_0[this.int_1], this.float_1);
                top += num3;
                if ((this.float_1 + num3) < num2)
                {
                    this.float_1 += num3;
                    flag3 = false;
                }
                else
                {
                    this.float_1 = 0f;
                    this.int_2++;
                    this.int_3++;
                }
                num2 = (this.int_3 < this.list_0.Count) ? this.list_0[this.int_3] : 0f;
                if (((0f == this.float_1) && this.bool_2) && ((top + num2) >= this.float_0))
                {
                    flag3 = false;
                }
                if ((0f == this.float_1) && (top >= this.float_0))
                {
                    flag3 = false;
                }
                if ((0f == this.float_1) && (this.int_2 >= (this.ilist_1.Count - 1)))
                {
                    flag3 = false;
                }
                if (!flag3)
                {
                    if (this.PrintFooter)
                    {
                        List<ImbeddedImage> list2 = new List<ImbeddedImage>();
                        foreach (ImbeddedImage image in this.ImbeddedImageList)
                        {
                            if (image.ImageLocation == Location.Header)
                            {
                                list2.Add(image);
                            }
                        }
                        Extensions.DrawImbeddedImage<ImbeddedImage>(list2, graphics_0, this.int_8, this.int_6, this.ilist_0[this.int_1].margins_0);
                        this.method_10(graphics_0, ref top, this.ilist_0[this.int_1].margins_0);
                    }
                    if (!(0f == this.float_1))
                    {
                        flag = true;
                    }
                    if ((this.int_9 >= this.int_5) || (this.int_2 >= (this.ilist_1.Count - 1)))
                    {
                        flag = this.method_7();
                        this.int_2 = -1;
                        this.int_9 = 0;
                        return flag;
                    }
                    return true;
                }
            }
        }

        private void method_9(Graphics graphics_0, ref float float_10, string string_9, Font font_4, Color color_4, StringFormat stringFormat_7, bool bool_11, Margins margins_0)
        {
            SizeF ef = graphics_0.MeasureString(string_9, font_4, this.int_8, stringFormat_7);
            RectangleF layoutRectangle = new RectangleF((float) margins_0.Left, float_10, (float) this.int_8, ef.Height);
            graphics_0.DrawString(string_9, font_4, new SolidBrush(color_4), layoutRectangle, stringFormat_7);
            float_10 += ef.Height;
        }

        protected virtual void OnCellOwnerDraw(DGVCellDrawingEventArgs e)
        {
            if (null != this.cellOwnerDrawEventHandler_0)
            {
                this.cellOwnerDrawEventHandler_0(this, e);
            }
        }

        public void PrintDataGridView(DataGridView dgv)
        {
            if (null == dgv)
            {
                throw new Exception("Null Parameter passed to DGVPrinter.");
            }
            if (!(typeof(DataGridView) == dgv.GetType()))
            {
                throw new Exception("Invalid Parameter passed to DGVPrinter.");
            }
            this.dataGridView_0 = dgv;
            if (DialogResult.OK == this.DisplayPrintDialog())
            {
                this.method_2();
                this.printDocument_0.Print();
            }
        }

        private void printDocument_0_BeginPrint(object sender, PrintEventArgs e)
        {
            this.int_1 = 0;
            this.int_2 = -1;
            this.int_9 = 0;
        }

        private void printDocument_0_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.HasMorePages = this.method_8(e.Graphics);
        }

        public void PrintNoDisplay(DataGridView dgv)
        {
            if (null == dgv)
            {
                throw new Exception("Null Parameter passed to DGVPrinter.");
            }
            if (!(typeof(DataGridView) == dgv.GetType()))
            {
                throw new Exception("Invalid Parameter passed to DGVPrinter.");
            }
            this.dataGridView_0 = dgv;
            this.method_2();
            this.printDocument_0.Print();
        }

        public void PrintPreviewDataGridView(DataGridView dgv)
        {
            if (null == dgv)
            {
                throw new Exception("Null Parameter passed to DGVPrinter.");
            }
            if (!(typeof(DataGridView) == dgv.GetType()))
            {
                throw new Exception("Invalid Parameter passed to DGVPrinter.");
            }
            this.dataGridView_0 = dgv;
            if (DialogResult.OK == this.DisplayPrintDialog())
            {
                this.PrintPreviewNoDisplay(dgv);
            }
        }

        public void PrintPreviewNoDisplay(DataGridView dgv)
        {
            if (null == dgv)
            {
                throw new Exception("Null Parameter passed to DGVPrinter.");
            }
            if (!(typeof(DataGridView) == dgv.GetType()))
            {
                throw new Exception("Invalid Parameter passed to DGVPrinter.");
            }
            this.dataGridView_0 = dgv;
            this.method_2();
            PrintPreviewDialog dialog = new PrintPreviewDialog {
                Document = this.printDocument_0,
                UseAntiAlias = true,
                Owner = this.Owner
            };
            dialog.PrintPreviewControl.Zoom = this.PrintPreviewZoom;
            dialog.Width = this.method_0();
            dialog.Height = this.method_1();
            if (null != this.icon_0)
            {
                dialog.Icon = this.icon_0;
            }
            dialog.ShowDialog();
        }

        public StringAlignment CellAlignment
        {
            get
            {
                return this.stringAlignment_1;
            }
            set
            {
                this.stringAlignment_1 = value;
            }
        }

        public StringFormatFlags CellFormatFlags
        {
            get
            {
                return this.stringFormatFlags_1;
            }
            set
            {
                this.stringFormatFlags_1 = value;
            }
        }

        public Dictionary<string, DataGridViewCellStyle> ColumnHeaderStyles
        {
            get
            {
                return this.dictionary_0;
            }
        }

        public Dictionary<string, DataGridViewCellStyle> ColumnStyles
        {
            get
            {
                return this.dictionary_2;
            }
        }

        public Dictionary<string, float> ColumnWidths
        {
            get
            {
                return this.dictionary_1;
            }
        }

        public string DocName
        {
            get
            {
                return this.string_2;
            }
            set
            {
                this.printDocument_0.DocumentName = value;
                this.string_2 = value;
            }
        }

        public string Footer
        {
            get
            {
                return this.string_4;
            }
            set
            {
                this.string_4 = value;
            }
        }

        public StringAlignment FooterAlignment
        {
            get
            {
                return this.stringFormat_2.Alignment;
            }
            set
            {
                this.stringFormat_2.Alignment = value;
                this.xWsOuVkaTs = true;
            }
        }

        public Color FooterColor
        {
            get
            {
                return this.color_2;
            }
            set
            {
                this.color_2 = value;
            }
        }

        public Font FooterFont
        {
            get
            {
                return this.font_2;
            }
            set
            {
                this.font_2 = value;
            }
        }

        public StringFormat FooterFormat
        {
            get
            {
                return this.stringFormat_2;
            }
            set
            {
                this.stringFormat_2 = value;
                this.xWsOuVkaTs = true;
            }
        }

        public StringFormatFlags FooterFormatFlags
        {
            get
            {
                return this.stringFormat_2.FormatFlags;
            }
            set
            {
                this.stringFormat_2.FormatFlags = value;
                this.xWsOuVkaTs = true;
            }
        }

        public float FooterSpacing
        {
            get
            {
                return this.float_9;
            }
            set
            {
                this.float_9 = value;
            }
        }

        public StringAlignment HeaderCellAlignment
        {
            get
            {
                return this.stringAlignment_0;
            }
            set
            {
                this.stringAlignment_0 = value;
            }
        }

        public StringFormatFlags HeaderCellFormatFlags
        {
            get
            {
                return this.stringFormatFlags_0;
            }
            set
            {
                this.stringFormatFlags_0 = value;
            }
        }

        public bool KeepRowsTogether
        {
            get
            {
                return this.bool_2;
            }
            set
            {
                this.bool_2 = value;
            }
        }

        public Form Owner
        {
            get
            {
                return this._Owner;
            }
            set
            {
                this._Owner = value;
            }
        }

        public StringAlignment PageNumberAlignment
        {
            get
            {
                return this.stringFormat_3.Alignment;
            }
            set
            {
                this.stringFormat_3.Alignment = value;
                this.bool_5 = true;
            }
        }

        public Color PageNumberColor
        {
            get
            {
                return this.color_3;
            }
            set
            {
                this.color_3 = value;
            }
        }

        public Font PageNumberFont
        {
            get
            {
                return this.font_3;
            }
            set
            {
                this.font_3 = value;
            }
        }

        public StringFormat PageNumberFormat
        {
            get
            {
                return this.stringFormat_3;
            }
            set
            {
                this.stringFormat_3 = value;
                this.bool_5 = true;
            }
        }

        public StringFormatFlags PageNumberFormatFlags
        {
            get
            {
                return this.stringFormat_3.FormatFlags;
            }
            set
            {
                this.stringFormat_3.FormatFlags = value;
                this.bool_5 = true;
            }
        }

        public bool PageNumberInHeader
        {
            get
            {
                return this.bool_7;
            }
            set
            {
                this.bool_7 = value;
            }
        }

        public bool PageNumberOnSeparateLine
        {
            get
            {
                return this.bool_8;
            }
            set
            {
                this.bool_8 = value;
            }
        }

        public bool PageNumbers
        {
            get
            {
                return this.bool_6;
            }
            set
            {
                this.bool_6 = value;
            }
        }

        public string PageSeparator
        {
            get
            {
                return this.string_5;
            }
            set
            {
                this.string_5 = value;
            }
        }

        public System.Drawing.Printing.PageSettings PageSettings
        {
            get
            {
                return this.printDocument_0.DefaultPageSettings;
            }
        }

        public string PageText
        {
            get
            {
                return this.string_6;
            }
            set
            {
                this.string_6 = value;
            }
        }

        public string PartText
        {
            get
            {
                return this.string_7;
            }
            set
            {
                this.string_7 = value;
            }
        }

        public bool PorportionalColumns
        {
            get
            {
                return this.bool_10;
            }
            set
            {
                this.bool_10 = value;
            }
        }

        public Icon PreviewDialogIcon
        {
            get
            {
                return this.icon_0;
            }
            set
            {
                this.icon_0 = value;
            }
        }

        public bool? PrintColumnHeaders
        {
            get
            {
                return this.nullable_0;
            }
            set
            {
                this.nullable_0 = value;
            }
        }

        public PrintDialogSettingsClass PrintDialogSettings
        {
            get
            {
                return this.printDialogSettingsClass_0;
            }
        }

        public PrintDocument printDocument
        {
            get
            {
                return this.printDocument_0;
            }
            set
            {
                this.printDocument_0 = value;
            }
        }

        public string PrinterName
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

        public bool PrintFooter
        {
            get
            {
                return this.bool_1;
            }
            set
            {
                this.bool_1 = value;
            }
        }

        public bool PrintHeader
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        public Margins PrintMargins
        {
            get
            {
                return this.PageSettings.Margins;
            }
            set
            {
                this.PageSettings.Margins = value;
            }
        }

        public double PrintPreviewZoom
        {
            get
            {
                return this._PrintPreviewZoom;
            }
            set
            {
                this._PrintPreviewZoom = value;
            }
        }

        public bool? PrintRowHeaders
        {
            get
            {
                return this.nullable_1;
            }
            set
            {
                this.nullable_1 = value;
            }
        }

        public PrinterSettings PrintSettings
        {
            get
            {
                return this.printDocument_0.PrinterSettings;
            }
        }

        public string RowHeaderCellDefaultText
        {
            get
            {
                return this.string_8;
            }
            set
            {
                this.string_8 = value;
            }
        }

        public DataGridViewCellStyle RowHeaderCellStyle
        {
            get
            {
                return this.dataGridViewCellStyle_0;
            }
            set
            {
                this.dataGridViewCellStyle_0 = value;
            }
        }

        public RowHeightSetting RowHeight
        {
            get
            {
                return this.rowHeightSetting_0;
            }
            set
            {
                this.rowHeightSetting_0 = value;
            }
        }

        public bool ShowTotalPageNumber
        {
            get
            {
                return this.bool_9;
            }
            set
            {
                this.bool_9 = value;
            }
        }

        public string SubTitle
        {
            get
            {
                return this.string_3;
            }
            set
            {
                this.string_3 = value;
            }
        }

        public StringAlignment SubTitleAlignment
        {
            get
            {
                return this.stringFormat_1.Alignment;
            }
            set
            {
                this.stringFormat_1.Alignment = value;
                this.bool_4 = true;
            }
        }

        public Color SubTitleColor
        {
            get
            {
                return this.color_1;
            }
            set
            {
                this.color_1 = value;
            }
        }

        public Font SubTitleFont
        {
            get
            {
                return this.font_1;
            }
            set
            {
                this.font_1 = value;
            }
        }

        public StringFormat SubTitleFormat
        {
            get
            {
                return this.stringFormat_1;
            }
            set
            {
                this.stringFormat_1 = value;
                this.bool_4 = true;
            }
        }

        public StringFormatFlags SubTitleFormatFlags
        {
            get
            {
                return this.stringFormat_1.FormatFlags;
            }
            set
            {
                this.stringFormat_1.FormatFlags = value;
                this.bool_4 = true;
            }
        }

        public float SubTitleSpacing
        {
            get
            {
                return this.float_8;
            }
            set
            {
                this.float_8 = value;
            }
        }

        public Alignment TableAlignment
        {
            get
            {
                return this.alignment_0;
            }
            set
            {
                this.alignment_0 = value;
            }
        }

        public string Title
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
                if (this.string_2 == null)
                {
                    this.printDocument_0.DocumentName = value;
                }
            }
        }

        public StringAlignment TitleAlignment
        {
            get
            {
                return this.stringFormat_0.Alignment;
            }
            set
            {
                this.stringFormat_0.Alignment = value;
                this.bool_3 = true;
            }
        }

        public Color TitleColor
        {
            get
            {
                return this.color_0;
            }
            set
            {
                this.color_0 = value;
            }
        }

        public Font TitleFont
        {
            get
            {
                return this.font_0;
            }
            set
            {
                this.font_0 = value;
            }
        }

        public StringFormat TitleFormat
        {
            get
            {
                return this.stringFormat_0;
            }
            set
            {
                this.stringFormat_0 = value;
                this.bool_3 = true;
            }
        }

        public StringFormatFlags TitleFormatFlags
        {
            get
            {
                return this.stringFormat_0.FormatFlags;
            }
            set
            {
                this.stringFormat_0.FormatFlags = value;
                this.bool_3 = true;
            }
        }

        public float TitleSpacing
        {
            get
            {
                return this.float_7;
            }
            set
            {
                this.float_7 = value;
            }
        }

        public enum Alignment
        {
            NotSet,
            Left,
            Right,
            Center
        }

        private class Class3
        {
            public float float_0;
            public List<float> list_0;
            public List<float> list_1;
            public Margins margins_0;
            public object object_0;

            public Class3(Margins margins_1, int int_0)
            {
                this.object_0 = new List<object>(int_0);
                this.list_0 = new List<float>(int_0);
                this.list_1 = new List<float>(int_0);
                this.float_0 = 0f;
                this.margins_0 = (Margins) margins_1.Clone();
            }
        }

        public class ImbeddedImage
        {
            [CompilerGenerated]
            private DGVPrinter.Alignment alignment_0;
            [CompilerGenerated]
            private Image image_0;
            [CompilerGenerated]
            private int int_0;
            [CompilerGenerated]
            private int int_1;
            [CompilerGenerated]
            private DGVPrinter.Location location_0;

            internal Point method_0(int int_2, int int_3, Margins margins_0)
            {
                int y = 0;
                int x = 0;
                switch (this.ImageLocation)
                {
                    case DGVPrinter.Location.Header:
                        y = margins_0.Top;
                        break;

                    case DGVPrinter.Location.Footer:
                        y = (int_3 - this.theImage.Height) - margins_0.Bottom;
                        break;

                    case DGVPrinter.Location.Absolute:
                        return new Point(this.ImageX, this.ImageY);

                    default:
                        throw new ArgumentException(string.Format("Unkown value: {0}", this.ImageLocation));
                }
                switch (this.ImageAlignment)
                {
                    case DGVPrinter.Alignment.NotSet:
                        x = this.ImageX;
                        break;

                    case DGVPrinter.Alignment.Left:
                        x = margins_0.Left;
                        break;

                    case DGVPrinter.Alignment.Right:
                        x = (int_2 - this.theImage.Width) + margins_0.Left;
                        break;

                    case DGVPrinter.Alignment.Center:
                        x = ((int_2 / 2) - (this.theImage.Width / 2)) + margins_0.Left;
                        break;

                    default:
                        throw new ArgumentException(string.Format("Unkown value: {0}", this.ImageAlignment));
                }
                return new Point(x, y);
            }

            public DGVPrinter.Alignment ImageAlignment
            {
                [CompilerGenerated]
                get
                {
                    return this.alignment_0;
                }
                [CompilerGenerated]
                set
                {
                    this.alignment_0 = value;
                }
            }

            public DGVPrinter.Location ImageLocation
            {
                [CompilerGenerated]
                get
                {
                    return this.location_0;
                }
                [CompilerGenerated]
                set
                {
                    this.location_0 = value;
                }
            }

            public int ImageX
            {
                [CompilerGenerated]
                get
                {
                    return this.int_0;
                }
                [CompilerGenerated]
                set
                {
                    this.int_0 = value;
                }
            }

            public int ImageY
            {
                [CompilerGenerated]
                get
                {
                    return this.int_1;
                }
                [CompilerGenerated]
                set
                {
                    this.int_1 = value;
                }
            }

            public Image theImage
            {
                [CompilerGenerated]
                get
                {
                    return this.image_0;
                }
                [CompilerGenerated]
                set
                {
                    this.image_0 = value;
                }
            }
        }

        public enum Location
        {
            Header,
            Footer,
            Absolute
        }

        public class PrintDialogSettingsClass
        {
            public bool AllowCurrentPage = true;
            public bool AllowPrintToFile = false;
            public bool AllowSelection = true;
            public bool AllowSomePages = true;
            public bool bool_0 = true;
            public bool ShowHelp = true;
            public bool ShowNetwork = true;
        }

        public enum RowHeightSetting
        {
            StringHeight,
            CellHeight
        }
    }
}

