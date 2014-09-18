namespace WHC.OrderWater.Commons
{
    using System;
    using System.IO;
    using System.Text;

    public class LogTextHelper
    {
        public static bool DebugLog = false;
        public static bool RecordLog = true;
        private static string string_0 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");

        static LogTextHelper()
        {
            if (!Directory.Exists(string_0))
            {
                Directory.CreateDirectory(string_0);
            }
        }

        public static void Debug(object ex)
        {
            WriteLine(ex.ToString());
        }

        public static void Debug(object message, Exception ex)
        {
            WriteLine(message.ToString(), ex);
        }

        public static void Error(object ex)
        {
            WriteLine(ex.ToString());
        }

        public static void Error(object message, Exception ex)
        {
            WriteLine(message.ToString(), ex);
        }

        public static void Info(object ex)
        {
            WriteLine(ex.ToString());
        }

        public static void Info(object message, Exception ex)
        {
            WriteLine(message.ToString(), ex);
        }

        public static void Warn(object ex)
        {
            WriteLine(ex.ToString());
        }

        public static void Warn(object message, Exception ex)
        {
            WriteLine(message.ToString(), ex);
        }

        public static void WriteLine(string message)
        {
            string contents = DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]    ") + message + "\r\n\r\n";
            string str2 = DateTime.Now.ToString("yyyyMMdd") + ".log";
            try
            {
                if (RecordLog)
                {
                    File.AppendAllText(Path.Combine(string_0, str2), contents, Encoding.GetEncoding("GB2312"));
                }
                if (DebugLog)
                {
                    Console.WriteLine(contents);
                }
            }
            catch
            {
            }
        }

        public static void WriteLine(string message, Exception ex)
        {
            string contents = DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]    ") + message + "\r\n" + ex.ToString() + "\r\n\r\n";
            string str2 = DateTime.Now.ToString("yyyyMMdd") + ".log";
            try
            {
                if (RecordLog)
                {
                    File.AppendAllText(Path.Combine(string_0, str2), contents, Encoding.GetEncoding("GB2312"));
                }
                if (DebugLog)
                {
                    Console.WriteLine(contents);
                }
            }
            catch
            {
            }
        }

        public static void WriteLine(string className, string funName, string message)
        {
            WriteLine(string.Format("{0}：{1}\r\n{2}", className, funName, message));
        }
    }
}

