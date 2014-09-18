namespace WHC.OrderWater.Commons
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    public class POSPrinter
    {
        private const int int_0 = 3;
        private string string_0;

        public POSPrinter()
        {
            this.string_0 = "LPT1";
        }

        public POSPrinter(string prnPort)
        {
            this.string_0 = "LPT1";
            this.string_0 = prnPort;
        }

        [DllImport("kernel32.dll", CharSet=CharSet.Auto)]
        private static extern IntPtr CreateFile(string string_1, int int_1, int int_2, int int_3, int int_4, int int_5, int int_6);
        public string PrintLine(string str)
        {
            try
            {
                IntPtr handle = CreateFile(this.string_0, 0x40000000, 0, 0, 3, 0, 0);
                if (handle.ToInt32() == -1)
                {
                    return "没有连接打印机或者打印机端口不是LPT1";
                }
                using (FileStream stream = new FileStream(handle, FileAccess.ReadWrite))
                {
                    using (StreamWriter writer = new StreamWriter(stream, Encoding.Default))
                    {
                        writer.WriteLine(str);
                    }
                }
                return "";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }
    }
}

