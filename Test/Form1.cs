using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test
{
    using System.Threading;

    public partial class Form1 : Form
    {
        private delegate void FlushClient();//代理

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //FlushClient fc = new FlushClient(ThreadFunction);
            //fc.BeginInvoke(null, null);
        }

        private void ThreadFunction()
        {
            while (true)
            {

                this.textBox1.Text = DateTime.Now.ToString();
                Thread.Sleep(1000);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Minute >= 4)
            {
                timer1.Interval = 2000;
            }
            if (timer1.Interval == 1000)
            {
                MessageBox.Show("Test1");

            }
            else
            {
                MessageBox.Show("Test2");
                
            }
        }


    }
}
