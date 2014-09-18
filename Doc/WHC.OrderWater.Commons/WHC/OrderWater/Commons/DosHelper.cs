namespace WHC.OrderWater.Commons
{
    using System;
    using System.Diagnostics;
    using System.Threading;

    public class DosHelper
    {
        public static void ExecuteCommandAsync(string command)
        {
            try
            {
                new Thread(new ParameterizedThreadStart(DosHelper.ExecuteCommandSync)) { IsBackground = true, Priority = ThreadPriority.AboveNormal }.Start(command);
            }
            catch (ThreadStartException exception)
            {
                LogTextHelper.Error(exception);
            }
            catch (ThreadAbortException exception2)
            {
                LogTextHelper.Error(exception2);
            }
            catch (Exception exception3)
            {
                LogTextHelper.Error(exception3);
            }
        }

        public static void ExecuteCommandSync(object command)
        {
            try
            {
                ProcessStartInfo info = new ProcessStartInfo("cmd", "/c " + command) {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                Process process = new Process {
                    StartInfo = info
                };
                process.Start();
                Console.WriteLine(process.StandardOutput.ReadToEnd());
            }
            catch (Exception exception)
            {
                LogTextHelper.Error(exception);
            }
        }

        public static void RunDos(string fileName, string argument, bool hidden)
        {
            Process process = new Process {
                StartInfo = { FileName = string.Format("\"{0}\"", fileName), Arguments = argument },
                EnableRaisingEvents = false
            };
            if (hidden)
            {
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            }
            else
            {
                process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            }
            process.Start();
            process.WaitForExit(0x3e8);
        }

        public static string smethod_0(string fileName, string argument, bool hidden, string confirm)
        {
            Process process = new Process {
                StartInfo = { FileName = fileName, Arguments = argument }
            };
            if (hidden)
            {
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            }
            else
            {
                process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            }
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = hidden;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            if (confirm != null)
            {
                process.StandardInput.WriteLine(confirm);
            }
            string str = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Close();
            return str;
        }
    }
}

