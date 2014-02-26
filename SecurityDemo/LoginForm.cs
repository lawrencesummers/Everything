using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SecurityDemo
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            using (var context = new EverythingContext())
            {
                var userList = context.Users;
                    //获取所有权限管理系统的用户，并在下拉列表中展示
             this.cboUser.Items.Clear();
             foreach (User info in userList)
             {
                 this.cboUser.Items.Add(info.Name);
             }

            }
        }
    }
}
