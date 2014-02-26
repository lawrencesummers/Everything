using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OutLookBar;

namespace OutLookBarDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            #region 初始化 OutLookBar
            outlookBar.BorderStyle = BorderStyle.FixedSingle;
            outlookBar.Initialize();
            IconPanel iconPanel1 = new IconPanel();
            iconPanel1.BorderStyle = BorderStyle.FixedSingle;
            IconPanel iconPanel2 = new IconPanel();
            IconPanel iconPanel3 = new IconPanel();

            outlookBar.AddBand("工具条A", iconPanel1);
            outlookBar.AddBand("工具条B", iconPanel2);
            outlookBar.AddBand("工具条C", iconPanel3);

            //0
            iconPanel1.AddIcon("信息查询", Image.FromFile(@"Image\1.ico"), new EventHandler(PanelEventA));
            //1
            iconPanel1.AddIcon("购物车管理", Image.FromFile(@"Image\2.ico"), new EventHandler(PanelEventA));
            //2
            iconPanel2.AddIcon("电子邮件", Image.FromFile(@"Image\3.ico"), new EventHandler(PanelEventB));
            //3
            iconPanel2.AddIcon("密码管理", Image.FromFile(@"Image\4.ico"), new EventHandler(PanelEventB));
            //4
            iconPanel3.AddIcon("时间设置", Image.FromFile(@"Image\4.ico"), new EventHandler(PanelEventC));
            outlookBar.SelectBand(0);
            #endregion
        }
        public void PanelEventA(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;
            PanelIcon panelIcon = ctrl.Tag as PanelIcon;
            string clickInfo = string.Empty;
            switch (panelIcon.Index)
            {
                case 0:
                    clickInfo = "信息查询";
                    break;
                case 1:
                    clickInfo = "购物车管理";
                    break;
            }
            this.label1.Text = string.Format("您选择了 {0}", clickInfo);
        }

        public void PanelEventB(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;
            PanelIcon panelIcon = ctrl.Tag as PanelIcon;
            string clickInfo = string.Empty;

            switch (panelIcon.Index)
            {
                case 0:
                    clickInfo = "电子邮件";
                    break;
                case 1:
                    clickInfo = "密码管理";
                    break;
            }
            this.label1.Text = string.Format("您选择了 {0}", clickInfo);
        }

        public void PanelEventC(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;
            PanelIcon panelIcon = ctrl.Tag as PanelIcon;
            string clickInfo = string.Empty;
            switch (panelIcon.Index)
            {
                case 0:
                    clickInfo = "时间设置";
                    break;
            }
            this.label1.Text = string.Format("您选择了 {0}", clickInfo);
        }
    }
}
