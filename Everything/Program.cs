using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Everything
{
    class Program
    {
        static void Main(string[] args)
        {
            //if (CheckAdobeReader("华通国际"))
            //{
            //    global::System.Windows.Forms.MessageBox.Show("请在弹出的界面中选择删除，删除后系统会重新安装");
            //    Process.Start(@"C:\Windows\System32\cmd.exe",@"C:\Windows\System32\MsiExec.exe /I{96576FCA-D3C1-4B79-8B60-EF23161AF052}");
            //}
            //Process.Start("setup.exe");
            string x = string.Format("{0:yyyyMMdd}",DateTime.Today);
            string lsh="0001";
            int daynum = 1;
            while (lsh != "9999")
            {
                daynum++;
                string post = daynum.ToString().PadLeft(4, '0');
                lsh = post;
            }
            Console.WriteLine(x);
            Console.ReadLine();


        }
        private static bool CheckAdobeReader(string display)
        {
            //Microsoft.Win32.RegistryKey uninstallNode = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            //foreach (string subKeyName in uninstallNode.GetSubKeyNames())
            //{
            //    Microsoft.Win32.RegistryKey subKey = uninstallNode.OpenSubKey(subKeyName);
            //    object displayName = subKey.GetValue("DisplayName");
            //    if (displayName != null)
            //    {
            //        if (displayName.ToString().Contains(display))
            //        {
            //            return true;
            //        }
            //    }
            //}
            return false;
        }


    }
}
