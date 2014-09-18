namespace RDIFramework.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Management;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;

    public class MachineInfoHelper
    {
        public static string GetCPUSerialNo()
        {
            string str = string.Empty;
            ManagementClass class2 = new ManagementClass("Win32_Processor");
            using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = class2.GetInstances().GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    ManagementObject current = (ManagementObject) enumerator.Current;
                    str = current.Properties["ProcessorId"].Value.ToString();
                }
            }
            return str;
        }

        public static string GetHardDiskInfo()
        {
            string str = string.Empty;
            ManagementClass class2 = new ManagementClass("Win32_DiskDrive");
            using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = class2.GetInstances().GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    ManagementObject current = (ManagementObject) enumerator.Current;
                    str = (string) current.Properties["Model"].Value;
                }
            }
            return str;
        }

        public static string GetIPAddress()
        {
            string str = string.Empty;
            Dns.GetHostEntry(Dns.GetHostName());
            using (List<string>.Enumerator enumerator = GetIPAddressList().GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    str = enumerator.Current.ToString();
                }
            }
            return str;
        }

        public static List<string> GetIPAddressList()
        {
            List<string> list = new List<string>();
            foreach (IPAddress address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    list.Add(address.ToString());
                }
            }
            return list;
        }

        public static string GetMacAddress()
        {
            string str = string.Empty;
            using (List<string>.Enumerator enumerator = GetMacAddressList().GetEnumerator())
            {
                string current;
                while (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                    if (!string.IsNullOrEmpty(current))
                    {
                        goto Label_0031;
                    }
                }
                return str;
            Label_0031:
                str = current.ToString();
                return string.Format("{0}-{1}-{2}-{3}-{4}-{5}", new object[] { str.Substring(0, 2), str.Substring(2, 2), str.Substring(4, 2), str.Substring(6, 2), str.Substring(8, 2), str.Substring(10, 2) });
            }
        }

        public static List<string> GetMacAddressList()
        {
            List<string> list = new List<string>();
            foreach (NetworkInterface interface2 in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (((!interface2.Description.Contains("WiFi") && !interface2.Description.Contains("Loopback")) && !interface2.Description.Contains("VMware")) && (interface2.OperationalStatus == OperationalStatus.Up))
                {
                    list.Add(interface2.GetPhysicalAddress().ToString());
                }
            }
            return list;
        }

        public static List<string> GetWirelessIPAddressList()
        {
            List<string> list = new List<string>();
            foreach (NetworkInterface interface2 in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (interface2.Description.Contains("Wireless"))
                {
                    foreach (UnicastIPAddressInformation information in interface2.GetIPProperties().UnicastAddresses)
                    {
                        if (information.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            list.Add(information.Address.ToString());
                        }
                    }
                }
            }
            return list;
        }

        public static List<string> GetWirelessMacAddressList()
        {
            List<string> list = new List<string>();
            foreach (NetworkInterface interface2 in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (interface2.Description.Contains("Wireless") && (interface2.OperationalStatus == OperationalStatus.Up))
                {
                    list.Add(interface2.GetPhysicalAddress().ToString());
                }
            }
            return list;
        }
    }
}

