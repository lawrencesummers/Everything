/*
 * Created by SharpDevelop.
 * User: guanxiang
 * Date: 2013/12/6
 * Time: 9:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.Threading;
using WCFSubscriber.localhost;


namespace WCFSubscriber
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
    public partial class MainForm : Form, IPublisherCallback
	{
		
		
		
		PublisherClient proxy = null;

        public MainForm()
        {
            InitializeComponent();
            InstanceContext instance = new InstanceContext(this);
            proxy = new PublisherClient(instance);
        }

        private Guid id = Guid.NewGuid();


        public void Notify()
        {
            //MessageBox.Show(string.Format("SubScriber : Thread {0}", Thread.CurrentThread.GetHashCode()));
            label1.Text = "后台返回数据了";
        }


        private void btnSub_Click(object sender, EventArgs e)
        {
            proxy.Subscriber(id);

        }

        private void btnUnsub_Click(object sender, EventArgs e)
        {
            proxy.UnSubscriber(id);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            proxy.Trigger();
        }


		
	}
}
