using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;

namespace WeifenLuoDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
            bool m_bSaveLayout=false;
        MainToolWindow mainToolWindow = new MainToolWindow();
        TestForm f = new TestForm();
        protected override void OnLoad(EventArgs e)
        {
            mainToolWindow.Show(this.dockPanel, DockState.DockLeft);
            f.Show(this.dockPanel);

            DeserializeDockContent m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            if (File.Exists(configFile))
            {
                dockPanel.LoadFromXml(configFile, m_deserializeDockContent);
            }

            base.OnLoad(e);
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(MainToolWindow).ToString())
                return mainToolWindow;
            //else if (persistString == typeof(FrmStatus).ToString())
            //    return mainStatus;
            //else if (persistString == typeof(FrmRoomView).ToString())
                //return frmRoomView;
            else
                return null;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            if (m_bSaveLayout)
                dockPanel.SaveAsXml(configFile);
            else if (File.Exists(configFile))
             {
                 m_bSaveLayout = true;
                File.Delete(configFile);
                dockPanel.SaveAsXml(configFile);
            }
            base.OnClosing(e);
        }

        private void menu_Window_CloseAll_Click(object sender, EventArgs e)
        {
            CloseAllDocuments();
        }

        private void menu_Window_CloseOther_Click(object sender, EventArgs e)
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                Form activeMdi = ActiveMdiChild;
                foreach (Form form in MdiChildren)
                {
                    if (form != activeMdi)
                    {
                        form.Close();
                    }
                }
            }
            else
            {
                foreach (IDockContent document in dockPanel.DocumentsToArray())
                {
                    if (!document.DockHandler.IsActivated)
                    {
                        document.DockHandler.Close();
                    }
                }
            }
        }

        private DockContent FindDocument(string text)
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                {
                    if (form.Text == text)
                    {
                        return form as DockContent;
                    }
                }

                return null;
            }
            else
            {
                foreach (DockContent content in dockPanel.Documents)
                {
                    if (content.DockHandler.TabText == text)
                    {
                        return content;
                    }
                }

                return null;
            }
        }

        //public DockContent ShowContent(string caption, Type formType)
        //{
        //    DockContent frm = FindDocument(caption);
        //    if (frm == null)
        //    {
        //        frm = ChildWinManagement.LoadMdiForm(Portal.gc.MainDialog, formType) as DockContent;
        //    }

        //    frm.Show(this.dockPanel);
        //    frm.BringToFront();
        //    return frm;
        //}

        public void CloseAllDocuments()
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                {
                    form.Close();
                }
            }
            else
            {
                IDockContent[] documents = dockPanel.DocumentsToArray();
                foreach (IDockContent content in documents)
                {
                    content.DockHandler.Close();
                }
            }
        } 

        private void Show()
        {
            
            
        }
    }
}
