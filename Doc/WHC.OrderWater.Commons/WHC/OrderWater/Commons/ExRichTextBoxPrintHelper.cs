namespace WHC.OrderWater.Commons
{
    using System;
    using System.Diagnostics;
    using System.Drawing.Printing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class ExRichTextBoxPrintHelper
    {
        private const int int_0 = 0x400;
        private const int int_1 = 0x439;
        private const int int_2 = 0x43a;
        private const int int_3 = 0x444;
        private const int int_4 = 1;
        private const int int_5 = 2;
        private const int int_6 = 4;
        private int int_7;
        private RichTextBox richTextBox_0;
        private const uint uint_0 = 1;
        private const uint uint_1 = 2;
        private const uint uint_10 = 0x8000000;
        private const uint uint_11 = 1;
        private const uint uint_12 = 2;
        private const uint uint_13 = 4;
        private const uint uint_14 = 8;
        private const uint uint_15 = 0x10;
        private const uint uint_16 = 0x20;
        private const uint uint_17 = 0x40000000;
        private const uint uint_2 = 4;
        private const uint uint_3 = 8;
        private const uint uint_4 = 0x10;
        private const uint uint_5 = 0x20;
        private const uint uint_6 = 0x80000000;
        private const uint uint_7 = 0x40000000;
        private const uint uint_8 = 0x20000000;
        private const uint uint_9 = 0x10000000;

        public ExRichTextBoxPrintHelper(RichTextBox controlToPrint)
        {
            this.richTextBox_0 = controlToPrint;
        }

        public int FormatRange(bool measureOnly, PrintPageEventArgs e, int charFrom, int charTo)
        {
            Struct21 struct2;
            Struct20 struct3;
            Struct20 struct4;
            Struct22 struct5;
            struct2.int_0 = charFrom;
            struct2.int_1 = charTo;
            struct3.int_1 = this.method_3(e.MarginBounds.Top);
            struct3.int_3 = this.method_3(e.MarginBounds.Bottom);
            struct3.int_0 = this.method_3(e.MarginBounds.Left);
            struct3.int_2 = this.method_3(e.MarginBounds.Right);
            struct4.int_1 = this.method_3(e.PageBounds.Top);
            struct4.int_3 = this.method_3(e.PageBounds.Bottom);
            struct4.int_0 = this.method_3(e.PageBounds.Left);
            struct4.int_2 = this.method_3(e.PageBounds.Right);
            IntPtr hdc = e.Graphics.GetHdc();
            struct5.struct21_0 = struct2;
            struct5.intptr_0 = hdc;
            struct5.intptr_1 = hdc;
            struct5.struct20_0 = struct3;
            struct5.struct20_1 = struct4;
            int num = measureOnly ? 0 : 1;
            IntPtr ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf(struct5));
            Marshal.StructureToPtr(struct5, ptr, false);
            int num2 = SendMessage(this.richTextBox_0.Handle, 0x439, num, ptr);
            Marshal.FreeCoTaskMem(ptr);
            e.Graphics.ReleaseHdc(hdc);
            return num2;
        }

        public void FormatRangeDone()
        {
            IntPtr ptr = new IntPtr(0);
            SendMessage(this.richTextBox_0.Handle, 0x439, 0, ptr);
        }

        private void method_0(object sender, PrintEventArgs e)
        {
            this.int_7 = 0;
        }

        private void method_1(object sender, PrintPageEventArgs e)
        {
            this.int_7 = this.FormatRange(false, e, this.int_7, this.richTextBox_0.TextLength);
            if (this.int_7 < this.richTextBox_0.TextLength)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
        }

        private void method_2(object sender, PrintEventArgs e)
        {
            this.FormatRangeDone();
        }

        private int method_3(int int_8)
        {
            return (int) (int_8 * 14.4);
        }

        private bool method_4(uint uint_18, uint uint_19)
        {
            Struct23 struct2;
            struct2 = new Struct23 {
                int_0 = Marshal.SizeOf(struct2),
                uint_0 = uint_18,
                jFkdGycaqt = uint_19
            };
            IntPtr ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf(struct2));
            Marshal.StructureToPtr(struct2, ptr, false);
            return (SendMessage(this.richTextBox_0.Handle, 0x444, 1, ptr) == 0);
        }

        public void PrintRTF(bool preview)
        {
            this.PrintRTF(new PrintDocument(), preview);
        }

        public void PrintRTF(PrintDocument printDocument)
        {
            try
            {
                printDocument.BeginPrint += new PrintEventHandler(this.method_0);
                printDocument.EndPrint += new PrintEventHandler(this.method_2);
                printDocument.PrintPage += new PrintPageEventHandler(this.method_1);
                printDocument.Print();
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message);
            }
        }

        public void PrintRTF(PrintDocument printDocument, bool preview)
        {
            try
            {
                printDocument.BeginPrint += new PrintEventHandler(this.method_0);
                printDocument.EndPrint += new PrintEventHandler(this.method_2);
                printDocument.PrintPage += new PrintPageEventHandler(this.method_1);
                CoolPrintPreviewDialog dialog = new CoolPrintPreviewDialog {
                    Document = printDocument
                };
                if (preview)
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        printDocument.Print();
                    }
                }
                else
                {
                    printDocument.Print();
                }
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message);
            }
        }

        [DllImport("User32.dll")]
        private static extern int SendMessage(IntPtr intptr_0, int int_8, int int_9, IntPtr intptr_1);
        public bool SetSelectionBold(bool bold)
        {
            return this.method_4(1, bold ? 1 : 0);
        }

        public static bool SetSelectionFont(RichTextBox control, string face)
        {
            Struct23 struct2;
            struct2 = new Struct23 {
                int_0 = Marshal.SizeOf(struct2),
                char_0 = new char[0x20],
                uint_0 = 0x20000000
            };
            face.CopyTo(0, struct2.char_0, 0, Math.Min(0x1f, face.Length));
            IntPtr ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf(struct2));
            Marshal.StructureToPtr(struct2, ptr, false);
            return (SendMessage(control.Handle, 0x444, 1, ptr) == 0);
        }

        public bool SetSelectionItalic(bool italic)
        {
            return this.method_4(2, italic ? 2 : 0);
        }

        public static bool SetSelectionSize(RichTextBox control, int size)
        {
            Struct23 struct2;
            struct2 = new Struct23 {
                int_0 = Marshal.SizeOf(struct2),
                uint_0 = 0x80000000,
                int_1 = size * 20
            };
            IntPtr ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf(struct2));
            Marshal.StructureToPtr(struct2, ptr, false);
            return (SendMessage(control.Handle, 0x444, 1, ptr) == 0);
        }

        public bool SetSelectionUnderlined(bool underlined)
        {
            return this.method_4(4, underlined ? 4 : 0);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Struct20
        {
            public int int_0;
            public int int_1;
            public int int_2;
            public int int_3;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Struct21
        {
            public int int_0;
            public int int_1;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Struct22
        {
            public IntPtr intptr_0;
            public IntPtr intptr_1;
            public ExRichTextBoxPrintHelper.Struct20 struct20_0;
            public ExRichTextBoxPrintHelper.Struct20 struct20_1;
            public ExRichTextBoxPrintHelper.Struct21 struct21_0;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Struct23
        {
            public int int_0;
            public uint uint_0;
            public uint jFkdGycaqt;
            public int int_1;
            public int int_2;
            public int int_3;
            public byte byte_0;
            public byte byte_1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x20)]
            public char[] char_0;
        }
    }
}

