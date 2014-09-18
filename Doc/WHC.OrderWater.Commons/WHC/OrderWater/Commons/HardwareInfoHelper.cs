namespace WHC.OrderWater.Commons
{
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.Management;
    using System.Runtime.InteropServices;
    using System.Text;

    public sealed class HardwareInfoHelper
    {
        private const uint uint_0 = 0x74080;
        private const uint uint_1 = 0x7c084;
        private const uint uint_2 = 0x7c088;
        private const uint uint_3 = 0x80000000;
        private const uint uint_4 = 0x40000000;
        private const uint uint_5 = 1;
        private const uint uint_6 = 2;
        private const uint uint_7 = 1;
        private const uint uint_8 = 3;

        [DllImport("kernel32.dll", SetLastError=true)]
        private static extern int CloseHandle(IntPtr intptr_0);
        [DllImport("kernel32.dll", SetLastError=true)]
        private static extern IntPtr CreateFile(string string_0, uint uint_9, uint uint_10, IntPtr intptr_0, uint uint_11, uint uint_12, IntPtr intptr_1);
        [DllImport("kernel32.dll")]
        private static extern int DeviceIoControl(IntPtr intptr_0, uint uint_9, IntPtr intptr_1, uint uint_10, ref Struct10 struct10_0, uint uint_11, ref uint uint_12, [Out] IntPtr intptr_2);
        [DllImport("kernel32.dll", EntryPoint="DeviceIoControl")]
        private static extern int DeviceIoControl_1(IntPtr intptr_0, uint uint_9, ref Struct12 struct12_0, uint uint_10, ref Struct14 struct14_0, uint uint_11, ref uint uint_12, [Out] IntPtr intptr_1);
        public static string GetComputerName()
        {
            return Environment.MachineName;
        }

        public static string GetCPUId()
        {
            string str = "";
            try
            {
                ManagementClass class2 = new ManagementClass("Win32_Processor");
                using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = class2.GetInstances().GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        ManagementObject current = (ManagementObject) enumerator.Current;
                        str = current.Properties["ProcessorId"].Value.ToString();
                    }
                }
            }
            catch
            {
                str = "078BFBFF00020FC1";
            }
            return str;
        }

        public static int GetCpuUsage()
        {
            return CpuUsage.Create().Query();
        }

        public static string GetDiskID()
        {
            string str = "";
            ManagementClass class2 = new ManagementClass("Win32_DiskDrive");
            foreach (ManagementObject obj2 in class2.GetInstances())
            {
                str = obj2.Properties["signature"].Value.ToString();
            }
            return str;
        }

        public static string GetDiskModel()
        {
            string str = string.Empty;
            using (ManagementClass class2 = new ManagementClass("Win32_DiskDrive"))
            {
                foreach (ManagementObject obj2 in class2.GetInstances())
                {
                    str = (string) obj2.Properties["Model"].Value;
                }
            }
            return str;
        }

        public static string GetIPAddress()
        {
            ManagementObjectCollection objects;
            string str = "";
            ManagementClass class2 = new ManagementClass("Win32_NetworkAdapterConfiguration");
            using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = class2.GetInstances().GetEnumerator())
            {
                ManagementObject current;
                Array array;
                while (enumerator.MoveNext())
                {
                    current = (ManagementObject) enumerator.Current;
                    if ((bool) current["IPEnabled"])
                    {
                        goto Label_004F;
                    }
                }
                goto Label_0089;
            Label_004F:
                array = (Array) current.Properties["IpAddress"].Value;
                str = array.GetValue(0).ToString();
            }
        Label_0089:
            objects = null;
            class2 = null;
            return str;
        }

        public static string GetMacAddress()
        {
            string str = "";
            ManagementClass class2 = new ManagementClass("Win32_NetworkAdapterConfiguration");
            using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = class2.GetInstances().GetEnumerator())
            {
                ManagementObject current;
                while (enumerator.MoveNext())
                {
                    current = (ManagementObject) enumerator.Current;
                    if ((bool) current["IPEnabled"])
                    {
                        goto Label_004F;
                    }
                }
                return str;
            Label_004F:
                str = current["MacAddress"].ToString();
            }
            return str;
        }

        public static string GetSystemType()
        {
            string str = "";
            ManagementClass class2 = new ManagementClass("Win32_ComputerSystem");
            foreach (ManagementObject obj2 in class2.GetInstances())
            {
                str = obj2["SystemType"].ToString();
            }
            return str;
        }

        public static string GetTotalPhysicalMemory()
        {
            string str = "";
            ManagementClass class2 = new ManagementClass("Win32_ComputerSystem");
            foreach (ManagementObject obj2 in class2.GetInstances())
            {
                str = obj2["TotalPhysicalMemory"].ToString();
            }
            return str;
        }

        public static List<string> GetUSBDriveLetters()
        {
            List<string> list = new List<string>();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE InterfaceType='USB'");
            foreach (ManagementObject obj2 in searcher.Get())
            {
                foreach (ManagementObject obj3 in obj2.GetRelated("Win32_DiskPartition"))
                {
                    foreach (ManagementObject obj4 in obj3.GetRelated("Win32_LogicalDisk"))
                    {
                        list.Add(obj4["DeviceID"].ToString());
                    }
                }
            }
            return list;
        }

        public static string GetUserName()
        {
            return Environment.UserName;
        }

        [DllImport("kernel32.dll")]
        private static extern int GetVolumeInformation(string string_0, string string_1, int int_0, ref int int_1, int int_2, int int_3, string string_2, int int_4);
        public static string HDVal()
        {
            return HDVal("C");
        }

        public static string HDVal(string drvID)
        {
            int num = 0;
            string str = null;
            string str2 = null;
            GetVolumeInformation(drvID + @":\", str, 0x100, ref num, 0, 0, str2, 0x100);
            return num.ToString();
        }

        public static string smethod_0()
        {
            string str = (string) Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\CentralProcessor\0").GetValue("ProcessorNameString");
            return str.TrimStart(new char[0]);
        }

        private static HardDiskInfo smethod_1(byte byte_0)
        {
            Struct10 struct2 = new Struct10();
            Struct12 struct3 = new Struct12();
            Struct14 struct4 = new Struct14();
            uint num = 0;
            IntPtr ptr = CreateFile(@"\\.\Smartvsd", 0, 0, IntPtr.Zero, 1, 0, IntPtr.Zero);
            if (ptr == IntPtr.Zero)
            {
                throw new Exception("Open smartvsd.vxd failed.");
            }
            if (0 == DeviceIoControl(ptr, 0x74080, IntPtr.Zero, 0, ref struct2, (uint) Marshal.SizeOf(struct2), ref num, IntPtr.Zero))
            {
                CloseHandle(ptr);
                throw new Exception("DeviceIoControl failed:DFP_GET_VERSION");
            }
            if (0 == (struct2.uint_0 & 1))
            {
                CloseHandle(ptr);
                throw new Exception("Error: IDE identify command not supported.");
            }
            if (0 != (byte_0 & 1))
            {
                struct3.struct11_0.byte_5 = 0xb0;
            }
            else
            {
                struct3.struct11_0.byte_5 = 160;
            }
            if (0 != (struct2.uint_0 & (((int) 0x10) >> byte_0)))
            {
                CloseHandle(ptr);
                throw new Exception(string.Format("Drive {0} is a ATAPI device, we don''t detect it", byte_0 + 1));
            }
            struct3.struct11_0.byte_6 = 0xec;
            struct3.byte_0 = byte_0;
            struct3.struct11_0.byte_1 = 1;
            struct3.struct11_0.byte_2 = 1;
            struct3.uint_0 = 0x200;
            if (0 == DeviceIoControl_1(ptr, 0x7c088, ref struct3, (uint) Marshal.SizeOf(struct3), ref struct4, (uint) Marshal.SizeOf(struct4), ref num, IntPtr.Zero))
            {
                CloseHandle(ptr);
                throw new Exception("DeviceIoControl failed: DFP_RECEIVE_DRIVE_DATA");
            }
            CloseHandle(ptr);
            return smethod_3(struct4.struct15_0);
        }

        private static HardDiskInfo smethod_2(byte byte_0)
        {
            Struct10 struct2 = new Struct10();
            Struct12 struct3 = new Struct12();
            Struct14 struct4 = new Struct14();
            uint num = 0;
            IntPtr ptr = CreateFile(string.Format(@"\\.\PhysicalDrive{0}", byte_0), 0xc0000000, 3, IntPtr.Zero, 3, 0, IntPtr.Zero);
            if (ptr == IntPtr.Zero)
            {
                throw new Exception("CreateFile faild.");
            }
            if (0 == DeviceIoControl(ptr, 0x74080, IntPtr.Zero, 0, ref struct2, (uint) Marshal.SizeOf(struct2), ref num, IntPtr.Zero))
            {
                CloseHandle(ptr);
                throw new Exception(string.Format("Drive {0} may not exists.", byte_0 + 1));
            }
            if (0 == (struct2.uint_0 & 1))
            {
                CloseHandle(ptr);
                throw new Exception("Error: IDE identify command not supported.");
            }
            if (0 != (byte_0 & 1))
            {
                struct3.struct11_0.byte_5 = 0xb0;
            }
            else
            {
                struct3.struct11_0.byte_5 = 160;
            }
            if (0 != (struct2.uint_0 & (((int) 0x10) >> byte_0)))
            {
                CloseHandle(ptr);
                throw new Exception(string.Format("Drive {0} is a ATAPI device, we don''t detect it.", byte_0 + 1));
            }
            struct3.struct11_0.byte_6 = 0xec;
            struct3.byte_0 = byte_0;
            struct3.struct11_0.byte_1 = 1;
            struct3.struct11_0.byte_2 = 1;
            struct3.uint_0 = 0x200;
            if (0 == DeviceIoControl_1(ptr, 0x7c088, ref struct3, (uint) Marshal.SizeOf(struct3), ref struct4, (uint) Marshal.SizeOf(struct4), ref num, IntPtr.Zero))
            {
                CloseHandle(ptr);
                throw new Exception("DeviceIoControl failed: DFP_RECEIVE_DRIVE_DATA");
            }
            CloseHandle(ptr);
            return smethod_3(struct4.struct15_0);
        }

        private static HardDiskInfo smethod_3(Struct15 struct15_0)
        {
            HardDiskInfo info = new HardDiskInfo();
            smethod_4(struct15_0.byte_2);
            info.ModuleNumber = Encoding.ASCII.GetString(struct15_0.byte_2).Trim();
            smethod_4(struct15_0.byte_1);
            info.Firmware = Encoding.ASCII.GetString(struct15_0.byte_1).Trim();
            smethod_4(struct15_0.byte_0);
            info.SerialNumber = Encoding.ASCII.GetString(struct15_0.byte_0).Trim();
            info.Capacity = (struct15_0.uint_1 / 2) / 0x400;
            return info;
        }

        private static void smethod_4(object object_0)
        {
            for (int i = 0; i < object_0.Length; i += 2)
            {
                byte num2 = (byte) object_0[i];
                object_0[i] = object_0[i + 1];
                object_0[i + 1] = num2;
            }
        }

        public static HardDiskInfo smethod_5(byte driveIndex)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32S:
                    throw new NotSupportedException("Win32s is not supported.");

                case PlatformID.Win32Windows:
                    return smethod_1(driveIndex);

                case PlatformID.Win32NT:
                    return smethod_2(driveIndex);

                case PlatformID.WinCE:
                    throw new NotSupportedException("WinCE is not supported.");
            }
            throw new NotSupportedException("Unknown Platform.");
        }

        internal sealed class Class5 : HardwareInfoHelper.CpuUsage
        {
            private RegistryKey sxyGumSoGi;

            public Class5()
            {
                try
                {
                    RegistryKey key = Registry.DynData.OpenSubKey(@"PerfStats\StartStat", false);
                    if (key == null)
                    {
                        throw new NotSupportedException();
                    }
                    key.GetValue(@"KERNEL\CPUUsage");
                    key.Close();
                    this.sxyGumSoGi = Registry.DynData.OpenSubKey(@"PerfStats\StatData", false);
                    if (this.sxyGumSoGi == null)
                    {
                        throw new NotSupportedException();
                    }
                }
                catch (NotSupportedException exception)
                {
                    throw exception;
                }
                catch (Exception exception2)
                {
                    throw new NotSupportedException("Error while querying the system information.", exception2);
                }
            }

            ~Class5()
            {
                try
                {
                    this.sxyGumSoGi.Close();
                }
                catch
                {
                }
                try
                {
                    RegistryKey key = Registry.DynData.OpenSubKey(@"PerfStats\StopStat", false);
                    key.GetValue(@"KERNEL\CPUUsage", false);
                    key.Close();
                }
                catch
                {
                }
            }

            public override int Query()
            {
                int num;
                try
                {
                    num = (int) this.sxyGumSoGi.GetValue(@"KERNEL\CPUUsage");
                }
                catch (Exception exception)
                {
                    throw new NotSupportedException("Error while querying the system information.", exception);
                }
                return num;
            }
        }

        internal sealed class Class6 : HardwareInfoHelper.CpuUsage
        {
            private double double_0;
            private const int int_0 = 0;
            private const int int_1 = 2;
            private const int int_2 = 3;
            private const int int_3 = 0;
            private long long_0;
            private long long_1;

            public Class6()
            {
                byte[] buffer = new byte[0x20];
                byte[] buffer2 = new byte[0x138];
                byte[] buffer3 = new byte[0x2c];
                if (NtQuerySystemInformation(3, buffer, buffer.Length, IntPtr.Zero) != 0)
                {
                    throw new NotSupportedException();
                }
                if (NtQuerySystemInformation(2, buffer2, buffer2.Length, IntPtr.Zero) != 0)
                {
                    throw new NotSupportedException();
                }
                if (NtQuerySystemInformation(0, buffer3, buffer3.Length, IntPtr.Zero) != 0)
                {
                    throw new NotSupportedException();
                }
                this.long_0 = BitConverter.ToInt64(buffer2, 0);
                this.long_1 = BitConverter.ToInt64(buffer, 8);
                this.double_0 = buffer3[40];
            }

            [DllImport("ntdll")]
            private static extern int NtQuerySystemInformation(int int_4, byte[] byte_0, int int_5, IntPtr intptr_0);
            public override int Query()
            {
                byte[] buffer = new byte[0x20];
                byte[] buffer2 = new byte[0x138];
                if (NtQuerySystemInformation(3, buffer, buffer.Length, IntPtr.Zero) != 0)
                {
                    throw new NotSupportedException();
                }
                if (NtQuerySystemInformation(2, buffer2, buffer2.Length, IntPtr.Zero) != 0)
                {
                    throw new NotSupportedException();
                }
                double num2 = BitConverter.ToInt64(buffer2, 0) - this.long_0;
                double num3 = BitConverter.ToInt64(buffer, 8) - this.long_1;
                if (!(num3 == 0.0))
                {
                    num2 /= num3;
                }
                num2 = (100.0 - ((num2 * 100.0) / this.double_0)) + 0.5;
                this.long_0 = BitConverter.ToInt64(buffer2, 0);
                this.long_1 = BitConverter.ToInt64(buffer, 8);
                return (int) num2;
            }
        }

        public abstract class CpuUsage
        {
            private static HardwareInfoHelper.CpuUsage cpuUsage_0 = null;

            protected CpuUsage()
            {
            }

            public static HardwareInfoHelper.CpuUsage Create()
            {
                if (cpuUsage_0 == null)
                {
                    if (Environment.OSVersion.Platform != PlatformID.Win32NT)
                    {
                        if (Environment.OSVersion.Platform != PlatformID.Win32Windows)
                        {
                            throw new NotSupportedException();
                        }
                        cpuUsage_0 = new HardwareInfoHelper.Class5();
                    }
                    else
                    {
                        cpuUsage_0 = new HardwareInfoHelper.Class6();
                    }
                }
                return cpuUsage_0;
            }

            public abstract int Query();
        }

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct HardDiskInfo
        {
            public string ModuleNumber;
            public string Firmware;
            public string SerialNumber;
            public uint Capacity;
        }

        [StructLayout(LayoutKind.Sequential, Pack=1)]
        internal struct Struct10
        {
            public byte byte_0;
            public byte byte_1;
            public byte byte_2;
            public byte byte_3;
            public uint uint_0;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
            public uint[] uint_1;
        }

        [StructLayout(LayoutKind.Sequential, Pack=1)]
        internal struct Struct11
        {
            public byte byte_0;
            public byte byte_1;
            public byte byte_2;
            public byte byte_3;
            public byte byte_4;
            public byte byte_5;
            public byte byte_6;
            public byte byte_7;
        }

        [StructLayout(LayoutKind.Sequential, Pack=1)]
        internal struct Struct12
        {
            public uint uint_0;
            public HardwareInfoHelper.Struct11 struct11_0;
            public byte byte_0;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=3)]
            public byte[] byte_1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
            public uint[] uint_1;
            public byte byte_2;
        }

        [StructLayout(LayoutKind.Sequential, Pack=1)]
        internal struct Struct13
        {
            public byte byte_0;
            public byte byte_1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
            public byte[] byte_2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
            public uint[] uint_0;
        }

        [StructLayout(LayoutKind.Sequential, Pack=1)]
        internal struct Struct14
        {
            public uint uint_0;
            public HardwareInfoHelper.Struct13 struct13_0;
            public HardwareInfoHelper.Struct15 struct15_0;
        }

        [StructLayout(LayoutKind.Sequential, Size=0x200, Pack=1)]
        internal struct Struct15
        {
            public ushort ushort_0;
            public ushort ushort_1;
            public ushort ushort_2;
            public ushort ushort_3;
            public ushort ushort_4;
            public ushort ushort_5;
            public ushort ushort_6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=3)]
            public ushort[] ushort_7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=20)]
            public byte[] byte_0;
            public ushort ushort_8;
            public ushort ushort_9;
            public ushort ushort_10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
            public byte[] byte_1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=40)]
            public byte[] byte_2;
            public ushort ushort_11;
            public ushort ushort_12;
            public ushort ushort_13;
            public ushort ushort_14;
            public ushort ushort_15;
            public ushort ushort_16;
            public ushort ushort_17;
            public ushort ushort_18;
            public ushort ushort_19;
            public ushort ushort_20;
            public uint uint_0;
            public ushort ushort_21;
            public uint uint_1;
            public ushort ushort_22;
            public ushort ushort_23;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x80)]
            public byte[] byte_3;
        }
    }
}

