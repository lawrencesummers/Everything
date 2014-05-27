using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace LearnThread
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            backgroundWorker1.DoWork += new DoWorkEventHandler(BW_Work);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BW_Completed);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BW_ProgressChanged);
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        //工作事件
        protected void BW_Work(object sender,DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending)
            {
                e.Cancel = true;
            }

            int n = (int)e.Argument;

            e.Result = CaculateNumber(n,worker,e);
        }

        public long CaculateNumber(int n,BackgroundWorker workder,DoWorkEventArgs e)
        {
            long result = 0;
            for(int i=0;i<=n;i++)
            {
                if (workder.CancellationPending)
                {
                    e.Cancel = true;
                }
                else
                {
                    int percent = (int)((float)i / (float)n * 100);
                    Thread.Sleep(500);
                    workder.ReportProgress(percent);
                    result++;
                }
            }

            return result;
        }
        //完成事件
        protected void BW_Completed(object sender,RunWorkerCompletedEventArgs e)
        {
            //可以设置e.Result来传值给该事件
            if (e.Error!=null)
            {
                this.lblBWMessage.Text = e.Error.Message;
            }
            else if (e.Cancelled)
            {
                this.lblBWMessage.Text = "Cancel!";
            }
            else
            {
                
                this.lblBWMessage.Text = "Completed!";
            }
        }
        //进度改变事件
        protected void BW_ProgressChanged(Object sender, ProgressChangedEventArgs e)
        {
            this.lblBWMessage.Text = e.ProgressPercentage.ToString();
            this.pbBW.Value = e.ProgressPercentage;
        }


        private void btnStartBW_Click(object sender, EventArgs e)
        {
            int value = (int)this.numericUpDown1.Value;
            this.btnStartBW.Enabled = false;
            this.btnCancelBW.Enabled = true;
            backgroundWorker1.RunWorkerAsync(value);
        }

        private void btnCancelBW_Click(object sender, EventArgs e)
        {
            this.btnStartBW.Enabled = true ;
            this.btnCancelBW.Enabled = false;
            backgroundWorker1.CancelAsync();
        }


        Thread threadwork;

        private void btnStartThread_Click(object sender, EventArgs e)
        {
            int initValue = (int)numericUpDown1.Value;
            this.btnAbortThread.Enabled = true;
            this.btnStartThread.Enabled = false;
            ParameterizedThreadStart paramThreadStart = new ParameterizedThreadStart(Thread_DoWork);
            threadwork = new Thread(paramThreadStart);
            threadwork.IsBackground = true;
            threadwork.Start(initValue);

        }

        public void Thread_DoWork(Object value) 
        {
            int number = (int)value;

            if (!this.IsDisposed)
            {
                Action<int> delLabel = (x) => { this.lblThreadMessage.Text = x.ToString(); };
                Action<int> delProgressBar = (y) => { this.pbThread.Value = y; };
                for (int i = 0; i <= number; i++)
                {
                    this.lblThreadMessage.BeginInvoke(delLabel,i);
                    this.pbThread.BeginInvoke(delProgressBar,(int)((float)i / (float)number * 100));
                    Thread.Sleep(500);
                }
            }
            Action del = delegate() { this.lblThreadMessage.Text = "Completed!"; };
            this.lblThreadMessage.BeginInvoke(del);
        }

        private void btnAbortThread_Click(object sender, EventArgs e)
        {
            if (threadwork.IsAlive)
                threadwork.Abort();
            this.lblThreadMessage.Text = "Cancel";
            this.btnStartThread.Enabled = true;
            this.btnAbortThread.Enabled = false;
        }
    }
}
