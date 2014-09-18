namespace WHC.OrderWater.Commons
{
    using log4net;
    using System;

    public class LogHelper
    {
        private static readonly ILog ljIvqJbXu = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Debug(object ex)
        {
            ljIvqJbXu.Debug(ex);
        }

        public static void Debug(object message, Exception ex)
        {
            ljIvqJbXu.Debug(message, ex);
        }

        public static void Error(object ex)
        {
            ljIvqJbXu.Error(ex);
        }

        public static void Error(object message, Exception ex)
        {
            ljIvqJbXu.Error(message, ex);
        }

        public static void Info(object ex)
        {
            ljIvqJbXu.Info(ex);
        }

        public static void Info(object message, Exception ex)
        {
            ljIvqJbXu.Info(message, ex);
        }

        public static void Warn(object ex)
        {
            ljIvqJbXu.Warn(ex);
        }

        public static void Warn(object message, Exception ex)
        {
            ljIvqJbXu.Warn(message, ex);
        }
    }
}

