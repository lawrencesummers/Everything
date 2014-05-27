using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace LearnThread
{

    /*
      * 作者：周公
      * BackgroundWorker类是net 里新增加的一个类，对于需要长时间操作而不需要用户长时间等待的情况可以使用这个类。
      * 注意确保在 DoWork 事件处理程序中不操作任何用户界面对象。而应该通过 ProgressChanged 和 RunWorkerCompleted 事件与用户界面进行通信。
      * 它有几个属性：
      * CancellationPending——指示应用程序是否已请求取消后台操作。
      * IsBusy——指示 BackgroundWorker 是否正在运行异步操作
      * WorkerReportsProgress——该值指示 BackgroundWorker 能否报告进度更新
      * WorkerSupportsCancellation——该值指示 BackgroundWorker 是否支持异步取消
      * 还有如下事件：
      * DoWork——调用 RunWorkerAsync 时发生。
      * ProgressChanged——调用 ReportProgress 时发生。
      * RunWorkerCompleted——当后台操作已完成、被取消或引发异常时发生。
      *
     * 还有如下方法：
      * CancelAsync——请求取消挂起的后台操作
      * ReportProgress——引发 ProgressChanged 事件
      * RunWorkerAsync——开始执行后台操作
      *
     **/

    public partial class MainForm : Form
    {
        private BackgroundWorker worker = new BackgroundWorker();
        public MainForm()
        {
            InitializeComponent();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            //正式做事情的地方
            worker.DoWork += new DoWorkEventHandler(DoWork);
            //任务完称时要做的，比如提示等等
            worker.ProgressChanged += new ProgressChangedEventHandler(ProgessChanged);
            //任务进行时，报告进度
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompleteWork);
        }

        //调用 RunWorkerAsync 时发生

        public void DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = ComputeFibonacci(worker, e);//当ComputeFibonacci(worker, e)返回时，异步过程结束
        }

        //调用 ReportProgress 时发生

        public void ProgessChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar.Value = e.ProgressPercentage;
        }

        //当后台操作已完成、被取消或引发异常时发生

        public void CompleteWork(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("完成！");
        }

        private int ComputeFibonacci(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return -1;
                }
                else
                {
                    int percent = 100 * i / 1000;//计算已完成的百分比
                    worker.ReportProgress(percent);
                }
                System.Threading.Thread.Sleep(200);
            }
            return -1;

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            worker.RunWorkerAsync();
            btnStart.Enabled = false;
            btnPause.Enabled = true;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            btnPause.Enabled = false;
            btnStart.Enabled = true;
            worker.CancelAsync();
        }

    }
}