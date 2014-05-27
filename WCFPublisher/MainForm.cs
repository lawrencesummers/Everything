/*
 * Created by SharpDevelop.
 * User: guanxiang
 * Date: 2013/12/6
 * Time: 9:45
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


namespace WCFPublisher
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		     private ServiceHost host = null;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			host = new ServiceHost(typeof(WCFPublisher.Publisher));
            host.Open();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		


        Dictionary<Guid, IPublisherEvents> dic = new Dictionary<Guid, IPublisherEvents>();

        //public void AddSubscriber(Guid id,IPublisherEvents callback)
        //{
        //    if (!dic.ContainsKey(id))
        //    {
        //        dic.Add(id,callback);
        //        lsbSubscribers.Items.Add(id);
        //    }
        //}

        //public void RemoveSubscriber(Guid id,IPublisherEvents callback)
        //{
        //    if (dic.ContainsKey(id))
        //    {
        //        dic.Remove(id);
        //        lsbSubscribers.Items.Remove(id);
        //    }
        //}

        private void btnNotify_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<Guid, IPublisherEvents> pair in dic)
            {
                pair.Value.Notify();
            }
        }

	}
}
