using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.ServiceModel;
namespace Everything
{
    using System.Reflection;

    using LearnAttribute;
    using LawrenceUtils;

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

            //try
            //{
            //    FileStream outputStream = new FileStream("d:", FileMode.Create);
            //}
            //catch (Exception ex)
            //{
                
            //}

            //Type type = typeof(MyRemotableClass);
            //foreach (Attribute attr in type.GetCustomAttributes(false))
            //{
            //    RemoteObjectAttribute remoteAttr = attr as RemoteObjectAttribute;
            //    if (remoteAttr != null)
            //    {
            //        Console.WriteLine("Create this object on " + remoteAttr.Server);
            //    }
            //}
            //Console.Read();


            //Type type = typeof(TestClass);
            //foreach (MethodInfo method in type.GetMethods())
            //{
            //    foreach (Attribute attr in method.GetCustomAttributes(false))
            //    {
            //        if (attr is TransactionableAttribute)
            //        {
            //            Console.WriteLine("{0} is transactionable.", method.Name);
            //        }
            //    }
            //}
            //Console.Read();



            //建立回调服务对象
            //HelloWCFCallback callbackObject = new HelloWCFCallback();
            ////建立实例上下文对象
            //InstanceContext clientContext = new InstanceContext(callbackObject);
            ////用建立好的实例上下文对象初始化代理类对象
           // Everything.ServiceReference1.HelloWCFClient client = new Everything.ServiceReference1.HelloWCFClient();
           // //Console.WriteLine("Client Call Begin:" + DateTime.Now.ToLongTimeString());
           // //string x = client.HelloWCF2();
           //// Console.WriteLine(x);
           // //Console.WriteLine("Client can process other things");
           // Console.ReadLine();

            //XmlHelper config = new XmlHelper("Everything.exe.config");

            //config.Replace("endpoint","1");
            try {

                string text = string.Empty;
                string result = string.Empty;
                using (FileStream fs = new FileStream("Everything.exe.config", FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        text = sr.ReadToEnd();
                        result = text.Replace("1003", "1004");
                    }
                }

                using (FileStream fs = new FileStream("Everything.exe.config", FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(result);
                    }
                } 
            }
            catch (Exception ex)
            {
 
            }

            



                





        }
        private static bool CheckAdobeReader(string display)
        {
            Microsoft.Win32.RegistryKey uninstallNode = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (string subKeyName in uninstallNode.GetSubKeyNames())
            {
                Microsoft.Win32.RegistryKey subKey = uninstallNode.OpenSubKey(subKeyName);
                object displayName = subKey.GetValue("DisplayName");
                if (displayName != null)
                {
                    if (displayName.ToString().Contains(display))
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        //public class HelloWCFCallback : Everything.ServiceReference1.IHelloWCFCallback
        //{
        //    public void Callback(string msg)
        //    {
        //        Console.WriteLine(msg);
        //    }
        //}


    }
}
