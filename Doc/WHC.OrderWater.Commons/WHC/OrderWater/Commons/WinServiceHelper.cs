namespace WHC.OrderWater.Commons
{
    using Microsoft.Win32;
    using System;
    using System.Collections;
    using System.Configuration.Install;
    using System.ServiceProcess;

    public class WinServiceHelper
    {
        public static bool ChangeServiceStartType(int startType, string serviceName)
        {
            try
            {
                Registry.LocalMachine.OpenSubKey("SYSTEM").OpenSubKey("CurrentControlSet").OpenSubKey("Services").OpenSubKey(serviceName, true).SetValue("Start", startType);
            }
            catch (Exception exception)
            {
                LogTextHelper.Error(exception);
                return false;
            }
            return true;
        }

        public static string GetServiceStartType(string serviceName)
        {
            try
            {
                return Registry.LocalMachine.OpenSubKey("SYSTEM").OpenSubKey("CurrentControlSet").OpenSubKey("Services").OpenSubKey(serviceName, true).GetValue("Start").ToString();
            }
            catch (Exception exception)
            {
                LogTextHelper.Error(exception);
                return string.Empty;
            }
        }

        public static bool InstallService(string serviceName, string serviceFileName)
        {
            if (ServiceIsExisted(serviceName))
            {
                throw new Exception(string.Format("{0} 服务已经存在", serviceName));
            }
            string[] commandLine = new string[0];
            TransactedInstaller installer = new TransactedInstaller();
            AssemblyInstaller installer2 = new AssemblyInstaller(serviceFileName, commandLine);
            installer.Installers.Add(installer2);
            installer.Install(new Hashtable());
            return true;
        }

        public static void InstallService2(bool install, string serviceFileName)
        {
            AssemblyInstaller installer;
            if (install)
            {
                installer = new AssemblyInstaller();
                IDictionary stateSaver = new Hashtable();
                installer.UseNewContext = true;
                installer.Path = serviceFileName;
                stateSaver.Clear();
                installer.Install(stateSaver);
                installer.Commit(stateSaver);
                installer.Dispose();
            }
            else
            {
                installer = new AssemblyInstaller {
                    UseNewContext = true,
                    Path = serviceFileName
                };
                installer.Uninstall(null);
                installer.Dispose();
            }
        }

        public static bool ServiceIsExisted(string serviceName)
        {
            foreach (ServiceController controller in ServiceController.GetServices())
            {
                if (controller.ServiceName.Equals(serviceName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ServiceIsRunning(string serviceName)
        {
            ServiceController controller = new ServiceController(serviceName);
            return (controller.Status == ServiceControllerStatus.Running);
        }

        public static bool StartService(string serviceName)
        {
            try
            {
                ServiceController controller = new ServiceController(serviceName);
                if (controller.Status == ServiceControllerStatus.Running)
                {
                    return true;
                }
                TimeSpan timeout = TimeSpan.FromMilliseconds(10000.0);
                controller.Start();
                controller.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch (Exception exception)
            {
                LogTextHelper.Error(exception);
                return false;
            }
            return true;
        }

        public static bool StopService(string serviseName)
        {
            try
            {
                ServiceController controller = new ServiceController(serviseName);
                if (controller.Status == ServiceControllerStatus.Stopped)
                {
                    return true;
                }
                TimeSpan timeout = TimeSpan.FromMilliseconds(10000.0);
                controller.Stop();
                controller.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            }
            catch (Exception exception)
            {
                LogTextHelper.Error(exception);
                return false;
            }
            return true;
        }

        public static bool UnInstallService(string serviceName, string serviceFileName)
        {
            if (ServiceIsExisted(serviceName))
            {
                string[] commandLine = new string[0];
                TransactedInstaller installer = new TransactedInstaller();
                AssemblyInstaller installer2 = new AssemblyInstaller(serviceFileName, commandLine);
                installer.Installers.Add(installer2);
                installer.Uninstall(null);
                return true;
            }
            return false;
        }

        public static bool WaitForStatus(string serviceName, ServiceControllerStatus status, int second)
        {
            bool flag = false;
            if (ServiceIsExisted(serviceName))
            {
                ServiceController controller = new ServiceController(serviceName);
                if (controller != null)
                {
                    TimeSpan timeout = TimeSpan.FromMilliseconds((double) (0x3e8 * second));
                    controller.WaitForStatus(ServiceControllerStatus.Running, timeout);
                    flag = true;
                }
            }
            return flag;
        }
    }
}

