using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace LearnThread
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        //public void TheInsaneCoWorker()
        //{
        //    // this requires a certain level of teeth gritting
        //    System.Threading.Timer timer = new System.Threading.Timer(_ => BackgroundMethod("第五、Timer计时器"), null, 0, Timeout.Infinite);
        //}

        //private delegate void BackgroundMethodDelegate(string approach);

        //private void BackgroundMethod(string approach)
        //{
        //    Console.WriteLine("内容： \"{0}\" 线程ID:{1} ({2})",
        //        approach,
        //        Thread.CurrentThread.ManagedThreadId,
        //        Thread.CurrentThread.IsThreadPoolThread ? "from ThreadPool" : "not from ThreadPool");
        //}
    }
}
