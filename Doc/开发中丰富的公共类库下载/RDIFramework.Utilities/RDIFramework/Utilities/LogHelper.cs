namespace RDIFramework.Utilities
{
    using System;
    using System.IO;

    public class LogHelper
    {
        private static readonly object object_0 = new object();
        private static StreamWriter streamWriter_0;

        private static void smethod_0(string string_0)
        {
            streamWriter_0 = !File.Exists(string_0) ? File.CreateText(string_0) : File.AppendText(string_0);
        }

        public static void WriteException(Exception exception)
        {
            WriteLog(exception);
        }

        public static void WriteLog(Exception exception)
        {
            lock (object_0)
            {
                try
                {
                    DateTime now = DateTime.Now;
                    string path = string.Format(@"{0}\Log", AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    path = path + string.Format(@"\{0}.log", now.ToString("yyyy-MM-dd"));
                    if (streamWriter_0 == null)
                    {
                        smethod_0(path);
                    }
                    if (exception != null)
                    {
                        streamWriter_0.WriteLine(now.ToString("HH:mm:ss") + "  异常信息：" + exception.Message);
                    }
                }
                finally
                {
                    if (streamWriter_0 != null)
                    {
                        streamWriter_0.Flush();
                        streamWriter_0.Dispose();
                        streamWriter_0 = null;
                    }
                }
            }
        }
    }
}

