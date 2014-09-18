namespace WHC.OrderWater.Commons
{
    using System;
    using System.Management;

    public class FingerprintHelper
    {
        private static string smethod_0(string string_0, string string_1, string string_2)
        {
            string str = "";
            ManagementClass class2 = new ManagementClass(string_0);
            foreach (ManagementObject obj2 in class2.GetInstances())
            {
                if ((obj2[string_2].ToString() == "True") && (str == ""))
                {
                    try
                    {
                        return obj2[string_1].ToString();
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            return str;
        }

        private static string smethod_1(string string_0, string string_1)
        {
            string str = "";
            ManagementClass class2 = new ManagementClass(string_0);
            foreach (ManagementObject obj2 in class2.GetInstances())
            {
                if (str == "")
                {
                    try
                    {
                        return obj2[string_1].ToString();
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            return str;
        }

        private static string smethod_2()
        {
            string str = smethod_1("Win32_Processor", "UniqueId");
            if (!(str == ""))
            {
                return str;
            }
            str = smethod_1("Win32_Processor", "ProcessorId");
            if (!(str == ""))
            {
                return str;
            }
            str = smethod_1("Win32_Processor", "Name");
            if (str == "")
            {
                str = smethod_1("Win32_Processor", "Manufacturer");
            }
            return (str + smethod_1("Win32_Processor", "MaxClockSpeed"));
        }

        private static string smethod_3()
        {
            return (smethod_1("Win32_BIOS", "Manufacturer") + smethod_1("Win32_BIOS", "SMBIOSBIOSVersion") + smethod_1("Win32_BIOS", "IdentificationCode") + smethod_1("Win32_BIOS", "SerialNumber") + smethod_1("Win32_BIOS", "ReleaseDate") + smethod_1("Win32_BIOS", "Version"));
        }

        private static string smethod_4()
        {
            return (smethod_1("Win32_DiskDrive", "Model") + smethod_1("Win32_DiskDrive", "Manufacturer") + smethod_1("Win32_DiskDrive", "Signature") + smethod_1("Win32_DiskDrive", "TotalHeads"));
        }

        private static string smethod_5()
        {
            return (smethod_1("Win32_BaseBoard", "Model") + smethod_1("Win32_BaseBoard", "Manufacturer") + smethod_1("Win32_BaseBoard", "Name") + smethod_1("Win32_BaseBoard", "SerialNumber"));
        }

        private static string smethod_6()
        {
            return (smethod_1("Win32_VideoController", "DriverVersion") + smethod_1("Win32_VideoController", "Name"));
        }

        private static string smethod_7()
        {
            return smethod_0("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled");
        }

        private static string smethod_8(string string_0)
        {
            int num = 0;
            int num2 = 0;
            foreach (char ch in string_0)
            {
                num2++;
                num += ch * num2;
            }
            return (num.ToString() + "00000000").Substring(0, 8);
        }

        public static string Value()
        {
            return smethod_8(smethod_2() + smethod_3() + smethod_4() + smethod_5() + smethod_6() + smethod_7());
        }
    }
}

