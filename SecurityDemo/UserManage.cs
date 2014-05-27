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
    public partial class UserManage : TabFormBase
    {
        public UserManage()
        {
            InitializeComponent();
        }

        private void UserManage_Load(object sender, EventArgs e)
        {
            using (var context = new EverythingContext())
            {
                var result = from user in context.Users
                    join userRole in context.UserRoles on user.UserID equals userRole.UserID
                    join role in context.Roles on userRole.RoleID equals role.RoleID
                
                
                    select new
                    {
                        UserID=user.UserID,
                        Name = user.Name,
                        role.RoleName
                    };
                //var result2=from re in result group re by re.UserID into RoleNameList select new {
                //    UserID=RoleNameList.Key,
                //    Name=re.Name,
                //    RoleNameList= RoleNameList
                //}

                //this.treeUser.DataSource
            }
           
        }
    }
}
