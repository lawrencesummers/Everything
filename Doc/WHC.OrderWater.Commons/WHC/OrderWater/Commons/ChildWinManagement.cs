namespace WHC.OrderWater.Commons
{
    using System;
    using System.Windows.Forms;

    public sealed class ChildWinManagement
    {
        private ChildWinManagement()
        {
        }

        public static bool ExistWin(Form MDIwin, string caption)
        {
            bool flag = false;
            foreach (Form form in MDIwin.MdiChildren)
            {
                if (form.Text == caption)
                {
                    flag = true;
                    form.Show();
                    form.Activate();
                    return flag;
                }
            }
            return flag;
        }

        public static Form LoadMdiForm(Form mainDialog, Type formType)
        {
            bool flag = false;
            Form form = null;
            foreach (Form form2 in mainDialog.MdiChildren)
            {
                if (form2.GetType() == formType)
                {
                    flag = true;
                    form = form2;
                    break;
                }
            }
            if (!flag)
            {
                form = (Form) Activator.CreateInstance(formType);
                form.MdiParent = mainDialog;
                form.Show();
            }
            form.BringToFront();
            form.Activate();
            return form;
        }

        public static void PopControlForm(Type control, string caption)
        {
            object obj2 = ReflectionUtil.CreateInstance(control);
            if (typeof(Control).IsAssignableFrom(obj2.GetType()))
            {
                Form form = new Form {
                    WindowState = FormWindowState.Maximized,
                    ShowIcon = false,
                    Text = caption,
                    ShowInTaskbar = false,
                    StartPosition = FormStartPosition.CenterScreen
                };
                Control control2 = obj2 as Control;
                control2.Dock = DockStyle.Fill;
                form.Controls.Add(control2);
                form.ShowDialog();
            }
        }

        public static void PopDialogForm(Type type)
        {
            object obj2 = ReflectionUtil.CreateInstance(type);
            if (typeof(Form).IsAssignableFrom(obj2.GetType()))
            {
                Form form = obj2 as Form;
                form.ShowInTaskbar = false;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog();
            }
        }
    }
}

