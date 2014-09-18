namespace WHC.OrderWater.Commons
{
    using Microsoft.Win32;
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    public class StartupHelper
    {
        private const int int_0 = 1;
        private static RegistryKey registryKey_0 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

        public static void HandleRunningInstance(Process instance)
        {
            HandleRunningInstance(instance, null);
        }

        public static void HandleRunningInstance(Process instance, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                MessageUtil.ShowWarning(message);
            }
            ShowWindowAsync(instance.MainWindowHandle, 1);
            SetForegroundWindow(instance.MainWindowHandle);
        }

        public static void RunAtStartup(string app, bool shouldRun)
        {
            RunAtStartup(app, shouldRun, Environment.CommandLine);
        }

        public static void RunAtStartup(string app, bool shouldRun, string exePath)
        {
            try
            {
                if (shouldRun)
                {
                    registryKey_0.SetValue(app, exePath);
                }
                else
                {
                    registryKey_0.DeleteValue(app, false);
                }
            }
            catch (Exception exception)
            {
                Trace.WriteLine("Unable to RunAtStartup: " + exception);
            }
        }

        public static Process RunningInstance()
        {
            Process currentProcess = Process.GetCurrentProcess();
            foreach (Process process2 in Process.GetProcessesByName(currentProcess.ProcessName))
            {
                if (process2.Id != currentProcess.Id)
                {
                    return process2;
                }
            }
            return null;
        }

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr intptr_0);
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr intptr_0, int int_1);
        public static bool WillRunAtStartup(string app)
        {
            try
            {
                return object.Equals(registryKey_0.GetValue(app), Environment.CommandLine);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

