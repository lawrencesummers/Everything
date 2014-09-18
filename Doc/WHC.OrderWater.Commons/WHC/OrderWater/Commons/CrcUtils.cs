namespace WHC.OrderWater.Commons
{
    using System;
    using System.Text;

    public sealed class CrcUtils
    {
        private static uint[] uint_0 = null;
        private static ushort[] ushort_0 = null;

        public static ushort CRC16(byte[] ABytes)
        {
            smethod_0();
            ushort num = 0xffff;
            foreach (byte num4 in ABytes)
            {
                num = smethod_2(num4, num);
            }
            return ~num;
        }

        public static ushort CRC16(string AString)
        {
            return CRC16(AString, Encoding.UTF8);
        }

        public static ushort CRC16(string AString, Encoding encoding_0)
        {
            return CRC16(encoding_0.GetBytes(AString));
        }

        public static uint CRC32(string AString)
        {
            return CRC32(AString, Encoding.UTF8);
        }

        public static uint CRC32(byte[] ABytes)
        {
            smethod_1();
            uint maxValue = uint.MaxValue;
            foreach (byte num4 in ABytes)
            {
                maxValue = smethod_3(num4, maxValue);
            }
            return ~maxValue;
        }

        public static uint CRC32(string AString, Encoding encoding_0)
        {
            return CRC32(encoding_0.GetBytes(AString));
        }

        private static void smethod_0()
        {
            if (ushort_0 == null)
            {
                ushort_0 = new ushort[0x100];
                for (ushort i = 0; i < 0x100; i = (ushort) (i + 1))
                {
                    ushort num2 = i;
                    for (int j = 0; j < 8; j++)
                    {
                        if ((num2 % 2) == 0)
                        {
                            num2 = (ushort) (num2 >> 1);
                        }
                        else
                        {
                            num2 = (ushort) ((num2 >> 1) ^ 0x8408);
                        }
                    }
                    ushort_0[i] = num2;
                }
            }
        }

        private static void smethod_1()
        {
            if (uint_0 == null)
            {
                uint_0 = new uint[0x100];
                for (uint i = 0; i < 0x100; i += 1)
                {
                    uint num2 = i;
                    for (int j = 0; j < 8; j++)
                    {
                        if ((num2 % 2) == 0)
                        {
                            num2 = num2 >> 1;
                        }
                        else
                        {
                            num2 = (num2 >> 1) ^ 0xedb88320;
                        }
                    }
                    uint_0[i] = num2;
                }
            }
        }

        private static ushort smethod_2(byte byte_0, ushort ushort_1)
        {
            return (ushort) (ushort_0[(ushort_1 & 0xff) ^ byte_0] ^ (ushort_1 >> 8));
        }

        private static uint smethod_3(byte byte_0, uint uint_1)
        {
            return (uint_0[(int) ((IntPtr) ((uint_1 & 0xff) ^ byte_0))] ^ (uint_1 >> 8));
        }
    }
}

