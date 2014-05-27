using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using log4net;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace LearnThread
{
    public partial class Form1 : Form
    {
        BackgroundWorker backgroundWorker;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_ProgressChanged);
            backgroundWorker.RunWorkerAsync();
        }

        void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            this.textBox1.Text = DateTime.Now.ToString();
        }
        void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 500; i++)
            {
                backgroundWorker.ReportProgress(i);
                
                Thread.Sleep(100);
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ILog log = log4net.LogManager.GetLogger(typeof(Program));
            log.Error("error", new Exception("【frmMoney】【SearchMyCostList】\r\n" ));
            
            //ILog log = log4net.LogManager.GetLogger(typeof(Program));
            //log.Error("error", new Exception("【frmMoney】【SearchMyCostList】\r\n" + ex.Message + "\r\n" + ex.StackTrace));
        }

    }
}
