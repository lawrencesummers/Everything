namespace WHC.OrderWater.Commons
{
    using System;
    using System.IO;
    using System.Net;

    public class IdentifyEncoding
    {
        internal static int[][] int_0 = new int[0x5e][];
        internal static int[][] int_1 = new int[0x7e][];
        internal static int[][] int_2 = new int[0x5e][];
        internal static string[] string_0 = new string[] { "GB2312", "GBK", "HZ", "Big5", "CNS 11643", "ISO 2022CN", "UTF-8", "Unicode", "ASCII", "OTHER" };
        internal static int[][] vtstbBdLii = new int[0x5e][];

        public IdentifyEncoding()
        {
            this.vmethod_9();
        }

        public static long FileLength(FileInfo file)
        {
            if (Directory.Exists(file.FullName))
            {
                return 0;
            }
            return file.Length;
        }

        public virtual string GetEncodingName(FileInfo testfile)
        {
            FileStream sourceStream = null;
            sbyte[] target = new sbyte[(int) FileLength(testfile)];
            try
            {
                sourceStream = new FileStream(testfile.FullName, FileMode.Open, FileAccess.Read);
                ReadInput(sourceStream, ref target, 0, target.Length);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (null != sourceStream)
                {
                    sourceStream.Close();
                }
            }
            return this.GetEncodingName(target);
        }

        public virtual string GetEncodingName(sbyte[] rawtext)
        {
            int num = 0;
            int index = 0;
            int[] numArray = new int[] { this.vmethod_0(rawtext), this.vmethod_1(rawtext), this.vmethod_2(rawtext), this.vmethod_3(rawtext), this.vmethod_4(rawtext), this.vmethod_5(rawtext), this.vmethod_6(rawtext), this.vmethod_7(rawtext), this.vmethod_8(rawtext), 0 };
            for (int i = 0; i < 10; i++)
            {
                if (numArray[i] > num)
                {
                    index = i;
                    num = numArray[i];
                }
            }
            if (num <= 50)
            {
                index = 9;
            }
            return string_0[index];
        }

        public virtual string GetEncodingName(Uri testurl)
        {
            sbyte[] target = new sbyte[0x400];
            int num = 0;
            int start = 0;
            try
            {
                Stream responseStream = WebRequest.Create(testurl.AbsoluteUri).GetResponse().GetResponseStream();
                while ((num = ReadInput(responseStream, ref target, start, target.Length - start)) > 0)
                {
                    start += num;
                }
                responseStream.Close();
            }
            catch
            {
                throw;
            }
            return this.GetEncodingName(target);
        }

        public static double Identity(double literal)
        {
            return literal;
        }

        public static long Identity(long literal)
        {
            return literal;
        }

        public static float Identity(float literal)
        {
            return literal;
        }

        public static ulong Identity(ulong literal)
        {
            return literal;
        }

        public static int ReadInput(Stream sourceStream, ref sbyte[] target, int start, int count)
        {
            if (target.Length == 0)
            {
                return 0;
            }
            byte[] buffer = new byte[target.Length];
            int num = sourceStream.Read(buffer, start, count);
            if (num == 0)
            {
                return -1;
            }
            for (int i = start; i < (start + num); i++)
            {
                target[i] = (sbyte) buffer[i];
            }
            return num;
        }

        public static int ReadInput(TextReader sourceTextReader, ref sbyte[] target, int start, int count)
        {
            if (target.Length == 0)
            {
                return 0;
            }
            char[] buffer = new char[target.Length];
            int num3 = sourceTextReader.Read(buffer, start, count);
            if (num3 == 0)
            {
                return -1;
            }
            for (int i = start; i < (start + num3); i++)
            {
                target[i] = (sbyte) buffer[i];
            }
            return num3;
        }

        public static byte[] ToByteArray(string sourceString)
        {
            byte[] buffer = new byte[sourceString.Length];
            for (int i = 0; i < sourceString.Length; i++)
            {
                buffer[i] = (byte) sourceString[i];
            }
            return buffer;
        }

        public static byte[] ToByteArray(object[] tempObjectArray)
        {
            byte[] buffer = new byte[tempObjectArray.Length];
            for (int i = 0; i < tempObjectArray.Length; i++)
            {
                buffer[i] = (byte) tempObjectArray[i];
            }
            return buffer;
        }

        public static byte[] ToByteArray(sbyte[] sbyteArray)
        {
            byte[] buffer = new byte[sbyteArray.Length];
            for (int i = 0; i < sbyteArray.Length; i++)
            {
                buffer[i] = (byte) sbyteArray[i];
            }
            return buffer;
        }

        public static sbyte[] ToSByteArray(byte[] byteArray)
        {
            sbyte[] numArray = new sbyte[byteArray.Length];
            for (int i = 0; i < byteArray.Length; i++)
            {
                numArray[i] = (sbyte) byteArray[i];
            }
            return numArray;
        }

        internal virtual int vmethod_0(sbyte[] rawtext)
        {
            int length = 0;
            int num2 = 1;
            int num3 = 1;
            long num4 = 0;
            long num5 = 1;
            float num6 = 0f;
            float num7 = 0f;
            length = rawtext.Length;
            for (int i = 0; i < (length - 1); i++)
            {
                if (rawtext[i] < 0)
                {
                    num2++;
                    if ((((((sbyte) Identity((long) 0xa1)) <= rawtext[i]) && (rawtext[i] <= ((sbyte) Identity((long) 0xf7)))) && (((sbyte) Identity((long) 0xa1)) <= rawtext[i + 1])) && (rawtext[i + 1] <= ((sbyte) Identity((long) 0xfe))))
                    {
                        num3++;
                        num5 += 500;
                        int index = (rawtext[i] + 0x100) - 0xa1;
                        int num10 = (rawtext[i + 1] + 0x100) - 0xa1;
                        if (int_0[index][num10] != 0)
                        {
                            num4 += int_0[index][num10];
                        }
                        else if ((15 <= index) && (index < 0x37))
                        {
                            num4 += 200;
                        }
                    }
                    i++;
                }
            }
            num6 = 50f * (((float) num3) / ((float) num2));
            num7 = 50f * (((float) num4) / ((float) num5));
            return (int) (num6 + num7);
        }

        internal virtual int vmethod_1(sbyte[] rawtext)
        {
            int length = 0;
            int num2 = 1;
            int num3 = 1;
            long num4 = 0;
            long num5 = 1;
            float num6 = 0f;
            float num7 = 0f;
            length = rawtext.Length;
            for (int i = 0; i < (length - 1); i++)
            {
                if (rawtext[i] < 0)
                {
                    int num9;
                    int num10;
                    num2++;
                    if ((((((sbyte) Identity((long) 0xa1)) <= rawtext[i]) && (rawtext[i] <= ((sbyte) Identity((long) 0xf7)))) && (((sbyte) Identity((long) 0xa1)) <= rawtext[i + 1])) && (rawtext[i + 1] <= ((sbyte) Identity((long) 0xfe))))
                    {
                        num3++;
                        num5 += 500;
                        num9 = (rawtext[i] + 0x100) - 0xa1;
                        num10 = (rawtext[i + 1] + 0x100) - 0xa1;
                        if (int_0[num9][num10] != 0)
                        {
                            num4 += int_0[num9][num10];
                        }
                        else if ((15 <= num9) && (num9 < 0x37))
                        {
                            num4 += 200;
                        }
                    }
                    else if (((((sbyte) Identity((long) 0x81)) <= rawtext[i]) && (rawtext[i] <= ((sbyte) Identity((long) 0xfe)))) && !(((((sbyte) Identity((long) 0x80)) > rawtext[i + 1]) || (rawtext[i + 1] > ((sbyte) Identity((long) 0xfe)))) ? ((0x40 > rawtext[i + 1]) || (rawtext[i + 1] > 0x7e)) : false))
                    {
                        num3++;
                        num5 += 500;
                        num9 = (rawtext[i] + 0x100) - 0x81;
                        if ((0x40 <= rawtext[i + 1]) && (rawtext[i + 1] <= 0x7e))
                        {
                            num10 = rawtext[i + 1] - 0x40;
                        }
                        else
                        {
                            num10 = (rawtext[i + 1] + 0x100) - 0x80;
                        }
                        if (int_1[num9][num10] != 0)
                        {
                            num4 += int_1[num9][num10];
                        }
                    }
                    i++;
                }
            }
            num6 = 50f * (((float) num3) / ((float) num2));
            num7 = 50f * (((float) num4) / ((float) num5));
            return (((int) (num6 + num7)) - 1);
        }

        internal virtual int vmethod_2(sbyte[] rawtext)
        {
            int num = 0;
            int num2 = 1;
            long num3 = 0;
            long num4 = 1;
            float num5 = 0f;
            float num6 = 0f;
            int num7 = 0;
            int num8 = 0;
            int length = rawtext.Length;
            for (int i = 0; i < length; i++)
            {
                if (rawtext[i] == 0x7e)
                {
                    if (rawtext[i + 1] != 0x7b)
                    {
                        goto Label_01FC;
                    }
                    num7++;
                    i += 2;
                    while (i < (length - 1))
                    {
                        int num11;
                        int num13;
                        if ((rawtext[i] == 10) || (rawtext[i] == 13))
                        {
                            break;
                        }
                        if ((rawtext[i] == 0x7e) && (rawtext[i + 1] == 0x7d))
                        {
                            goto Label_01EE;
                        }
                        if (((0x21 <= rawtext[i]) && (rawtext[i] <= 0x77)) && ((0x21 <= rawtext[i + 1]) && (rawtext[i + 1] <= 0x77)))
                        {
                            num += 2;
                            num11 = rawtext[i] - 0x21;
                            num13 = rawtext[i + 1] - 0x21;
                            num4 += 500;
                            if (int_0[num11][num13] != 0)
                            {
                                num3 += int_0[num11][num13];
                            }
                            else if ((15 <= num11) && (num11 < 0x37))
                            {
                                num3 += 200;
                            }
                        }
                        else if (((0xa1 <= rawtext[i]) && (rawtext[i] <= 0xf7)) && ((0xa1 <= rawtext[i + 1]) && (rawtext[i + 1] <= 0xf7)))
                        {
                            num += 2;
                            num11 = (rawtext[i] + 0x100) - 0xa1;
                            num13 = (rawtext[i + 1] + 0x100) - 0xa1;
                            num4 += 500;
                            if (int_0[num11][num13] != 0)
                            {
                                num3 += int_0[num11][num13];
                            }
                            else if ((15 <= num11) && (num11 < 0x37))
                            {
                                num3 += 200;
                            }
                        }
                        num2 += 2;
                        i += 2;
                    }
                }
                continue;
            Label_01EE:
                num8++;
                i++;
                continue;
            Label_01FC:
                if (rawtext[i + 1] == 0x7d)
                {
                    num8++;
                    i++;
                }
                else if (rawtext[i + 1] == 0x7e)
                {
                    i++;
                }
            }
            if (num7 > 4)
            {
                num5 = 50f;
            }
            else if (num7 > 1)
            {
                num5 = 41f;
            }
            else if (num7 > 0)
            {
                num5 = 39f;
            }
            else
            {
                num5 = 0f;
            }
            num6 = 50f * (((float) num3) / ((float) num4));
            return (int) (num5 + num6);
        }

        internal virtual int vmethod_3(sbyte[] rawtext)
        {
            int length = 0;
            int num2 = 1;
            int num3 = 1;
            float num4 = 0f;
            float num5 = 0f;
            long num6 = 0;
            long num7 = 1;
            length = rawtext.Length;
            for (int i = 0; i < (length - 1); i++)
            {
                if (rawtext[i] < 0)
                {
                    num2++;
                    if (((((sbyte) Identity((long) 0xa1)) <= rawtext[i]) && (rawtext[i] <= ((sbyte) Identity((long) 0xf9)))) && !(((0x40 > rawtext[i + 1]) || (rawtext[i + 1] > 0x7e)) ? ((((sbyte) Identity((long) 0xa1)) > rawtext[i + 1]) || (rawtext[i + 1] > ((sbyte) Identity((long) 0xfe)))) : false))
                    {
                        int num10;
                        num3++;
                        num7 += 500;
                        int index = (rawtext[i] + 0x100) - 0xa1;
                        if ((0x40 <= rawtext[i + 1]) && (rawtext[i + 1] <= 0x7e))
                        {
                            num10 = rawtext[i + 1] - 0x40;
                        }
                        else
                        {
                            num10 = (rawtext[i + 1] + 0x100) - 0x61;
                        }
                        if (int_2[index][num10] != 0)
                        {
                            num6 += int_2[index][num10];
                        }
                        else if ((3 <= index) && (index <= 0x25))
                        {
                            num6 += 200;
                        }
                    }
                    i++;
                }
            }
            num4 = 50f * (((float) num3) / ((float) num2));
            num5 = 50f * (((float) num6) / ((float) num7));
            return (int) (num4 + num5);
        }

        internal virtual int vmethod_4(sbyte[] rawtext)
        {
            int length = 0;
            int num2 = 1;
            int num3 = 1;
            long num4 = 0;
            long num5 = 1;
            float num6 = 0f;
            float num7 = 0f;
            length = rawtext.Length;
            for (int i = 0; i < (length - 1); i++)
            {
                if (rawtext[i] < 0)
                {
                    num2++;
                    if ((((((i + 3) < length) && (((sbyte) Identity((long) 0x8e)) == rawtext[i])) && ((((sbyte) Identity((long) 0xa1)) <= rawtext[i + 1]) && (rawtext[i + 1] <= ((sbyte) Identity((long) 0xb0))))) && (((((sbyte) Identity((long) 0xa1)) <= rawtext[i + 2]) && (rawtext[i + 2] <= ((sbyte) Identity((long) 0xfe)))) && (((sbyte) Identity((long) 0xa1)) <= rawtext[i + 3]))) && (rawtext[i + 3] <= ((sbyte) Identity((long) 0xfe))))
                    {
                        num3++;
                        i += 3;
                    }
                    else if ((((((sbyte) Identity((long) 0xa1)) <= rawtext[i]) && (rawtext[i] <= ((sbyte) Identity((long) 0xfe)))) && (((sbyte) Identity((long) 0xa1)) <= rawtext[i + 1])) && (rawtext[i + 1] <= ((sbyte) Identity((long) 0xfe))))
                    {
                        num3++;
                        num5 += 500;
                        int index = (rawtext[i] + 0x100) - 0xa1;
                        int num10 = (rawtext[i + 1] + 0x100) - 0xa1;
                        if (vtstbBdLii[index][num10] != 0)
                        {
                            num4 += vtstbBdLii[index][num10];
                        }
                        else if ((0x23 <= index) && (index <= 0x5c))
                        {
                            num4 += 150;
                        }
                        i++;
                    }
                }
            }
            num6 = 50f * (((float) num3) / ((float) num2));
            num7 = 50f * (((float) num4) / ((float) num5));
            return (int) (num6 + num7);
        }

        internal virtual int vmethod_5(sbyte[] rawtext)
        {
            int length = 0;
            int num2 = 1;
            int num3 = 1;
            long num4 = 0;
            long num5 = 1;
            float num6 = 0f;
            float num7 = 0f;
            length = rawtext.Length;
            for (int i = 0; i < (length - 1); i++)
            {
                if ((rawtext[i] == 0x1b) && ((i + 3) < length))
                {
                    int num9;
                    int num10;
                    if (((rawtext[i + 1] == 0x24) && (rawtext[i + 2] == 0x29)) && (rawtext[i + 3] == 0x41))
                    {
                        i += 4;
                        while (rawtext[i] != 0x1b)
                        {
                            num2++;
                            if (((0x21 <= rawtext[i]) && (rawtext[i] <= 0x77)) && ((0x21 <= rawtext[i + 1]) && (rawtext[i + 1] <= 0x77)))
                            {
                                num3++;
                                num9 = rawtext[i] - 0x21;
                                num10 = rawtext[i + 1] - 0x21;
                                num5 += 500;
                                if (int_0[num9][num10] != 0)
                                {
                                    num4 += int_0[num9][num10];
                                }
                                else if ((15 <= num9) && (num9 < 0x37))
                                {
                                    num4 += 200;
                                }
                                i++;
                            }
                            i++;
                        }
                    }
                    else if (((((i + 3) < length) && (rawtext[i + 1] == 0x24)) && (rawtext[i + 2] == 0x29)) && (rawtext[i + 3] == 0x47))
                    {
                        i += 4;
                        while (rawtext[i] != 0x1b)
                        {
                            num2++;
                            if ((((0x21 <= rawtext[i]) && (rawtext[i] <= 0x7e)) && (0x21 <= rawtext[i + 1])) && (rawtext[i + 1] <= 0x7e))
                            {
                                num3++;
                                num5 += 500;
                                num9 = rawtext[i] - 0x21;
                                num10 = rawtext[i + 1] - 0x21;
                                if (vtstbBdLii[num9][num10] != 0)
                                {
                                    num4 += vtstbBdLii[num9][num10];
                                }
                                else if ((0x23 <= num9) && (num9 <= 0x5c))
                                {
                                    num4 += 150;
                                }
                                i++;
                            }
                            i++;
                        }
                    }
                    if ((((rawtext[i] == 0x1b) && ((i + 2) < length)) && (rawtext[i + 1] == 40)) && (rawtext[i + 2] == 0x42))
                    {
                        i += 2;
                    }
                }
            }
            num6 = 50f * (((float) num3) / ((float) num2));
            num7 = 50f * (((float) num4) / ((float) num5));
            return (int) (num6 + num7);
        }

        internal virtual int vmethod_6(sbyte[] rawtext)
        {
            int num = 0;
            int length = 0;
            int num3 = 0;
            int num4 = 0;
            length = rawtext.Length;
            for (int i = 0; i < length; i++)
            {
                if ((rawtext[i] & 0x7f) == rawtext[i])
                {
                    num4++;
                }
                else if ((((-64 <= rawtext[i]) && (rawtext[i] <= -33)) && (((i + 1) < length) && (-128 <= rawtext[i + 1]))) && (rawtext[i + 1] <= -65))
                {
                    num3 += 2;
                    i++;
                }
                else if (((((-32 <= rawtext[i]) && (rawtext[i] <= -17)) && (((i + 2) < length) && (-128 <= rawtext[i + 1]))) && ((rawtext[i + 1] <= -65) && (-128 <= rawtext[i + 2]))) && (rawtext[i + 2] <= -65))
                {
                    num3 += 3;
                    i += 2;
                }
            }
            if (num4 != length)
            {
                num = (int) (100f * (((float) num3) / ((float) (length - num4))));
                if (num > 0x62)
                {
                    return num;
                }
                if ((num > 0x5f) && (num3 > 30))
                {
                    return num;
                }
            }
            return 0;
        }

        internal virtual int vmethod_7(sbyte[] rawtext)
        {
            if (!(((((sbyte) Identity((long) 0xfe)) != rawtext[0]) || (((sbyte) Identity((long) 0xff)) != rawtext[1])) ? ((((sbyte) Identity((long) 0xff)) != rawtext[0]) || (((sbyte) Identity((long) 0xfe)) != rawtext[1])) : false))
            {
                return 100;
            }
            return 0;
        }

        internal virtual int vmethod_8(sbyte[] rawtext)
        {
            int num = 70;
            int length = rawtext.Length;
            for (int i = 0; i < length; i++)
            {
                if (rawtext[i] < 0)
                {
                    num -= 5;
                }
                else if (rawtext[i] == 0x1b)
                {
                    num -= 5;
                }
            }
            return num;
        }

        internal virtual void vmethod_9()
        {
            int num;
            if (int_0[0] == null)
            {
                for (num = 0; num < 0x5e; num++)
                {
                    int_0[num] = new int[0x5e];
                }
                int_0[0x31][0x1a] = 0x256;
                int_0[0x29][0x26] = 0x255;
                int_0[0x11][0x1a] = 0x254;
                int_0[0x20][0x2a] = 0x253;
                int_0[0x27][0x2a] = 0x252;
                int_0[0x2d][0x31] = 0x251;
                int_0[0x33][0x39] = 0x250;
                int_0[50][0x2f] = 0x24f;
                int_0[0x2a][90] = 590;
                int_0[0x34][0x41] = 0x24d;
                int_0[0x35][0x2f] = 0x24c;
                int_0[0x13][0x52] = 0x24b;
                int_0[0x1f][0x13] = 0x24a;
                int_0[40][0x2e] = 0x249;
                int_0[0x18][0x59] = 0x248;
                int_0[0x17][0x55] = 0x247;
                int_0[20][0x1c] = 0x246;
                int_0[0x2a][20] = 0x245;
                int_0[0x22][0x26] = 580;
                int_0[0x2d][9] = 0x243;
                int_0[0x36][50] = 0x242;
                int_0[0x19][0x2c] = 0x241;
                int_0[0x23][0x42] = 0x240;
                int_0[20][0x37] = 0x23f;
                int_0[0x12][0x55] = 0x23e;
                int_0[20][0x1f] = 0x23d;
                int_0[0x31][0x11] = 0x23c;
                int_0[0x29][0x10] = 0x23b;
                int_0[0x23][0x49] = 570;
                int_0[20][0x22] = 0x239;
                int_0[0x1d][0x2c] = 0x238;
                int_0[0x23][0x26] = 0x237;
                int_0[0x31][9] = 0x236;
                int_0[0x2e][0x21] = 0x235;
                int_0[0x31][0x33] = 0x234;
                int_0[40][0x59] = 0x233;
                int_0[0x1a][0x40] = 0x232;
                int_0[0x36][0x33] = 0x231;
                int_0[0x36][0x24] = 560;
                int_0[0x27][4] = 0x22f;
                int_0[0x35][13] = 0x22e;
                int_0[0x18][0x5c] = 0x22d;
                int_0[0x1b][0x31] = 0x22c;
                int_0[0x30][6] = 0x22b;
                int_0[0x15][0x33] = 0x22a;
                int_0[30][40] = 0x229;
                int_0[0x2a][0x5c] = 0x228;
                int_0[0x1f][0x4e] = 0x227;
                int_0[0x19][0x52] = 550;
                int_0[0x2f][0] = 0x225;
                int_0[0x22][0x13] = 0x224;
                int_0[0x2f][0x23] = 0x223;
                int_0[0x15][0x3f] = 0x222;
                int_0[0x2b][0x4b] = 0x221;
                int_0[0x15][0x57] = 0x220;
                int_0[0x23][0x3b] = 0x21f;
                int_0[0x19][0x22] = 0x21e;
                int_0[0x15][0x1b] = 0x21d;
                int_0[0x27][0x1a] = 540;
                int_0[0x22][0x1a] = 0x21b;
                int_0[0x27][0x34] = 0x21a;
                int_0[50][0x39] = 0x219;
                int_0[0x25][0x4f] = 0x218;
                int_0[0x1a][0x18] = 0x217;
                int_0[0x16][1] = 0x216;
                int_0[0x12][40] = 0x215;
                int_0[0x29][0x21] = 0x214;
                int_0[0x35][0x1a] = 0x213;
                int_0[0x36][0x56] = 530;
                int_0[20][0x10] = 0x211;
                int_0[0x2e][0x4a] = 0x210;
                int_0[30][0x13] = 0x20f;
                int_0[0x2d][0x23] = 0x20e;
                int_0[0x2d][0x3d] = 0x20d;
                int_0[30][9] = 0x20c;
                int_0[0x29][0x35] = 0x20b;
                int_0[0x29][13] = 0x20a;
                int_0[50][0x22] = 0x209;
                int_0[0x35][0x56] = 520;
                int_0[0x2f][0x2f] = 0x207;
                int_0[0x16][0x1c] = 0x206;
                int_0[50][0x35] = 0x205;
                int_0[0x27][70] = 0x204;
                int_0[0x26][15] = 0x203;
                int_0[0x2a][0x58] = 0x202;
                int_0[0x10][0x1d] = 0x201;
                int_0[0x1b][90] = 0x200;
                int_0[0x1d][12] = 0x1ff;
                int_0[0x2c][0x16] = 510;
                int_0[0x22][0x45] = 0x1fd;
                int_0[0x18][10] = 0x1fc;
                int_0[0x2c][11] = 0x1fb;
                int_0[0x27][0x5c] = 0x1fa;
                int_0[0x31][0x30] = 0x1f9;
                int_0[0x1f][0x2e] = 0x1f8;
                int_0[0x13][50] = 0x1f7;
                int_0[0x15][14] = 0x1f6;
                int_0[0x20][0x1c] = 0x1f5;
                int_0[0x12][3] = 500;
                int_0[0x35][9] = 0x1f3;
                int_0[0x22][80] = 0x1f2;
                int_0[0x30][0x58] = 0x1f1;
                int_0[0x2e][0x35] = 0x1f0;
                int_0[0x16][0x35] = 0x1ef;
                int_0[0x1c][10] = 0x1ee;
                int_0[0x2c][0x41] = 0x1ed;
                int_0[20][10] = 0x1ec;
                int_0[40][0x4c] = 0x1eb;
                int_0[0x2f][8] = 490;
                int_0[50][0x4a] = 0x1e9;
                int_0[0x17][0x3e] = 0x1e8;
                int_0[0x31][0x41] = 0x1e7;
                int_0[0x1c][0x57] = 0x1e6;
                int_0[15][0x30] = 0x1e5;
                int_0[0x16][7] = 0x1e4;
                int_0[0x13][0x2a] = 0x1e3;
                int_0[0x29][20] = 0x1e2;
                int_0[0x1a][0x37] = 0x1e1;
                int_0[0x15][0x5d] = 480;
                int_0[0x1f][0x4c] = 0x1df;
                int_0[0x22][0x1f] = 0x1de;
                int_0[20][0x42] = 0x1dd;
                int_0[0x33][0x21] = 0x1dc;
                int_0[0x22][0x56] = 0x1db;
                int_0[0x25][0x43] = 0x1da;
                int_0[0x35][0x35] = 0x1d9;
                int_0[40][0x58] = 0x1d8;
                int_0[0x27][10] = 0x1d7;
                int_0[0x18][3] = 470;
                int_0[0x1b][0x19] = 0x1d5;
                int_0[0x1a][15] = 0x1d4;
                int_0[0x15][0x58] = 0x1d3;
                int_0[0x34][0x3e] = 0x1d2;
                int_0[0x2e][0x51] = 0x1d1;
                int_0[0x26][0x48] = 0x1d0;
                int_0[0x11][30] = 0x1cf;
                int_0[0x34][0x5c] = 0x1ce;
                int_0[0x22][90] = 0x1cd;
                int_0[0x15][7] = 460;
                int_0[0x24][13] = 0x1cb;
                int_0[0x2d][0x29] = 0x1ca;
                int_0[0x20][5] = 0x1c9;
                int_0[0x1a][0x59] = 0x1c8;
                int_0[0x17][0x57] = 0x1c7;
                int_0[20][0x27] = 0x1c6;
                int_0[0x1b][0x17] = 0x1c5;
                int_0[0x19][0x3b] = 0x1c4;
                int_0[0x31][20] = 0x1c3;
                int_0[0x36][0x4d] = 450;
                int_0[0x1b][0x43] = 0x1c1;
                int_0[0x2f][0x21] = 0x1c0;
                int_0[0x29][0x11] = 0x1bf;
                int_0[0x13][0x51] = 0x1be;
                int_0[0x10][0x42] = 0x1bd;
                int_0[0x2d][0x1a] = 0x1bc;
                int_0[0x31][0x51] = 0x1bb;
                int_0[0x35][0x37] = 0x1ba;
                int_0[0x10][0x1a] = 0x1b9;
                int_0[0x36][0x3e] = 440;
                int_0[20][70] = 0x1b7;
                int_0[0x2a][0x23] = 0x1b6;
                int_0[20][0x39] = 0x1b5;
                int_0[0x22][0x24] = 0x1b4;
                int_0[0x2e][0x3f] = 0x1b3;
                int_0[0x13][0x2d] = 0x1b2;
                int_0[0x15][10] = 0x1b1;
                int_0[0x34][0x5d] = 0x1b0;
                int_0[0x19][2] = 0x1af;
                int_0[30][0x39] = 430;
                int_0[0x29][0x18] = 0x1ad;
                int_0[0x1c][0x2b] = 0x1ac;
                int_0[0x2d][0x56] = 0x1ab;
                int_0[0x33][0x38] = 0x1aa;
                int_0[0x25][0x1c] = 0x1a9;
                int_0[0x34][0x45] = 0x1a8;
                int_0[0x2b][0x5c] = 0x1a7;
                int_0[0x29][0x1f] = 0x1a6;
                int_0[0x25][0x57] = 0x1a5;
                int_0[0x2f][0x24] = 420;
                int_0[0x10][0x10] = 0x1a3;
                int_0[40][0x38] = 0x1a2;
                int_0[0x18][0x37] = 0x1a1;
                int_0[0x11][1] = 0x1a0;
                int_0[0x23][0x39] = 0x19f;
                int_0[0x1b][50] = 0x19e;
                int_0[0x1a][14] = 0x19d;
                int_0[50][40] = 0x19c;
                int_0[0x27][0x13] = 0x19b;
                int_0[0x13][0x59] = 410;
                int_0[0x1d][0x5b] = 0x199;
                int_0[0x11][0x59] = 0x198;
                int_0[0x27][0x4a] = 0x197;
                int_0[0x2e][0x27] = 0x196;
                int_0[40][0x1c] = 0x195;
                int_0[0x2d][0x44] = 0x194;
                int_0[0x2b][10] = 0x193;
                int_0[0x2a][13] = 0x192;
                int_0[0x2c][0x51] = 0x191;
                int_0[0x29][0x2f] = 400;
                int_0[0x30][0x3a] = 0x18f;
                int_0[0x2b][0x44] = 0x18e;
                int_0[0x10][0x4f] = 0x18d;
                int_0[0x13][5] = 0x18c;
                int_0[0x36][0x3b] = 0x18b;
                int_0[0x11][0x24] = 0x18a;
                int_0[0x12][0] = 0x189;
                int_0[0x29][5] = 0x188;
                int_0[0x29][0x48] = 0x187;
                int_0[0x10][0x27] = 390;
                int_0[0x36][0] = 0x185;
                int_0[0x33][0x10] = 0x184;
                int_0[0x1d][0x24] = 0x183;
                int_0[0x2f][5] = 0x182;
                int_0[0x2f][0x33] = 0x181;
                int_0[0x2c][7] = 0x180;
                int_0[0x23][30] = 0x17f;
                int_0[0x1a][9] = 0x17e;
                int_0[0x10][7] = 0x17d;
                int_0[0x20][1] = 380;
                int_0[0x21][0x4c] = 0x17b;
                int_0[0x22][0x5b] = 0x17a;
                int_0[0x34][0x24] = 0x179;
                int_0[0x1a][0x4d] = 0x178;
                int_0[0x23][0x30] = 0x177;
                int_0[40][80] = 0x176;
                int_0[0x29][0x5c] = 0x175;
                int_0[0x1b][0x5d] = 0x174;
                int_0[15][0x11] = 0x173;
                int_0[0x10][0x4c] = 370;
                int_0[0x33][12] = 0x171;
                int_0[0x12][20] = 0x170;
                int_0[15][0x36] = 0x16f;
                int_0[50][5] = 0x16e;
                int_0[0x21][0x16] = 0x16d;
                int_0[0x25][0x39] = 0x16c;
                int_0[0x1c][0x2f] = 0x16b;
                int_0[0x2a][0x1f] = 0x16a;
                int_0[0x12][2] = 0x169;
                int_0[0x2b][0x40] = 360;
                int_0[0x17][0x2f] = 0x167;
                int_0[0x1c][0x4f] = 0x166;
                int_0[0x19][0x2d] = 0x165;
                int_0[0x17][0x5b] = 0x164;
                int_0[0x16][0x13] = 0x163;
                int_0[0x19][0x2e] = 0x162;
                int_0[0x16][0x24] = 0x161;
                int_0[0x36][0x55] = 0x160;
                int_0[0x2e][20] = 0x15f;
                int_0[0x1b][0x25] = 350;
                int_0[0x1a][0x51] = 0x15d;
                int_0[0x2a][0x1d] = 0x15c;
                int_0[0x1f][90] = 0x15b;
                int_0[0x29][0x3b] = 0x15a;
                int_0[0x18][0x41] = 0x159;
                int_0[0x2c][0x54] = 0x158;
                int_0[0x18][90] = 0x157;
                int_0[0x26][0x36] = 0x156;
                int_0[0x1c][70] = 0x155;
                int_0[0x1b][15] = 340;
                int_0[0x1c][80] = 0x153;
                int_0[0x1d][8] = 0x152;
                int_0[0x2d][80] = 0x151;
                int_0[0x35][0x25] = 0x150;
                int_0[0x1c][0x41] = 0x14f;
                int_0[0x17][0x56] = 0x14e;
                int_0[0x27][0x2d] = 0x14d;
                int_0[0x35][0x20] = 0x14c;
                int_0[0x26][0x44] = 0x14b;
                int_0[0x2d][0x4e] = 330;
                int_0[0x2b][7] = 0x149;
                int_0[0x2e][0x52] = 0x148;
                int_0[0x1b][0x26] = 0x147;
                int_0[0x10][0x3e] = 0x146;
                int_0[0x18][0x11] = 0x145;
                int_0[0x16][70] = 0x144;
                int_0[0x34][0x1c] = 0x143;
                int_0[0x17][40] = 0x142;
                int_0[0x1c][50] = 0x141;
                int_0[0x2a][0x5b] = 320;
                int_0[0x2f][0x4c] = 0x13f;
                int_0[15][0x2a] = 0x13e;
                int_0[0x2b][0x37] = 0x13d;
                int_0[0x1d][0x54] = 0x13c;
                int_0[0x2c][90] = 0x13b;
                int_0[0x35][0x10] = 0x13a;
                int_0[0x16][0x5d] = 0x139;
                int_0[0x22][10] = 0x138;
                int_0[0x20][0x35] = 0x137;
                int_0[0x2b][0x41] = 310;
                int_0[0x1c][7] = 0x135;
                int_0[0x23][0x2e] = 0x134;
                int_0[0x15][0x27] = 0x133;
                int_0[0x2c][0x12] = 0x132;
                int_0[40][10] = 0x131;
                int_0[0x36][0x35] = 0x130;
                int_0[0x26][0x4a] = 0x12f;
                int_0[0x1c][0x1a] = 0x12e;
                int_0[15][13] = 0x12d;
                int_0[0x27][0x22] = 300;
                int_0[0x27][0x2e] = 0x12b;
                int_0[0x2a][0x42] = 0x12a;
                int_0[0x21][0x3a] = 0x129;
                int_0[15][0x38] = 0x128;
                int_0[0x12][0x33] = 0x127;
                int_0[0x31][0x44] = 0x126;
                int_0[30][0x25] = 0x125;
                int_0[0x33][0x54] = 0x124;
                int_0[0x33][9] = 0x123;
                int_0[40][70] = 290;
                int_0[0x29][0x54] = 0x121;
                int_0[0x1c][0x40] = 0x120;
                int_0[0x20][0x58] = 0x11f;
                int_0[0x18][5] = 0x11e;
                int_0[0x35][0x17] = 0x11d;
                int_0[0x2a][0x1b] = 0x11c;
                int_0[0x16][0x26] = 0x11b;
                int_0[0x20][0x56] = 0x11a;
                int_0[0x22][30] = 0x119;
                int_0[0x26][0x3f] = 280;
                int_0[0x18][0x3b] = 0x117;
                int_0[0x16][0x51] = 0x116;
                int_0[0x20][11] = 0x115;
                int_0[0x33][0x15] = 0x114;
                int_0[0x36][0x29] = 0x113;
                int_0[0x15][50] = 0x112;
                int_0[0x17][0x59] = 0x111;
                int_0[0x13][0x57] = 0x110;
                int_0[0x1a][7] = 0x10f;
                int_0[30][0x4b] = 270;
                int_0[0x2b][0x54] = 0x10d;
                int_0[0x33][0x19] = 0x10c;
                int_0[0x10][0x43] = 0x10b;
                int_0[0x20][9] = 0x10a;
                int_0[0x30][0x33] = 0x109;
                int_0[0x27][7] = 0x108;
                int_0[0x2c][0x58] = 0x107;
                int_0[0x34][0x18] = 0x106;
                int_0[0x17][0x22] = 0x105;
                int_0[0x20][0x4b] = 260;
                int_0[0x13][10] = 0x103;
                int_0[0x1c][0x5b] = 0x102;
                int_0[0x20][0x53] = 0x101;
                int_0[0x19][0x4b] = 0x100;
                int_0[0x35][0x2d] = 0xff;
                int_0[0x1d][0x55] = 0xfe;
                int_0[0x35][0x3b] = 0xfd;
                int_0[0x10][2] = 0xfc;
                int_0[0x13][0x4e] = 0xfb;
                int_0[15][0x4b] = 250;
                int_0[0x33][0x2a] = 0xf9;
                int_0[0x2d][0x43] = 0xf8;
                int_0[15][0x4a] = 0xf7;
                int_0[0x19][0x51] = 0xf6;
                int_0[0x25][0x3e] = 0xf5;
                int_0[0x10][0x37] = 0xf4;
                int_0[0x12][0x26] = 0xf3;
                int_0[0x17][0x17] = 0xf2;
                int_0[0x26][30] = 0xf1;
                int_0[0x11][0x1c] = 240;
                int_0[0x2c][0x49] = 0xef;
                int_0[0x17][0x4e] = 0xee;
                int_0[40][0x4d] = 0xed;
                int_0[0x26][0x57] = 0xec;
                int_0[0x1b][0x13] = 0xeb;
                int_0[0x26][0x52] = 0xea;
                int_0[0x25][0x16] = 0xe9;
                int_0[0x29][30] = 0xe8;
                int_0[0x36][9] = 0xe7;
                int_0[0x20][30] = 230;
                int_0[30][0x34] = 0xe5;
                int_0[40][0x54] = 0xe4;
                int_0[0x35][0x39] = 0xe3;
                int_0[0x1b][0x1b] = 0xe2;
                int_0[0x26][0x40] = 0xe1;
                int_0[0x12][0x2b] = 0xe0;
                int_0[0x17][0x45] = 0xdf;
                int_0[0x1c][12] = 0xde;
                int_0[50][0x4e] = 0xdd;
                int_0[50][1] = 220;
                int_0[0x1a][0x58] = 0xdb;
                int_0[0x24][40] = 0xda;
                int_0[0x21][0x59] = 0xd9;
                int_0[0x29][0x1c] = 0xd8;
                int_0[0x1f][0x4d] = 0xd7;
                int_0[0x2e][1] = 0xd6;
                int_0[0x2f][0x13] = 0xd5;
                int_0[0x23][0x37] = 0xd4;
                int_0[0x29][0x15] = 0xd3;
                int_0[0x1b][10] = 210;
                int_0[0x20][0x4d] = 0xd1;
                int_0[0x1a][0x25] = 0xd0;
                int_0[20][0x21] = 0xcf;
                int_0[0x29][0x34] = 0xce;
                int_0[0x20][0x12] = 0xcd;
                int_0[0x26][13] = 0xcc;
                int_0[20][0x12] = 0xcb;
                int_0[20][0x18] = 0xca;
                int_0[0x2d][0x13] = 0xc9;
                int_0[0x12][0x35] = 200;
            }
            if (int_1[0] == null)
            {
                for (num = 0; num < 0x7e; num++)
                {
                    int_1[num] = new int[0xbf];
                }
                int_1[0x49][0x87] = 0x257;
                int_1[0x31][0x7b] = 0x256;
                int_1[0x4d][0x92] = 0x255;
                int_1[0x51][0x7b] = 0x254;
                int_1[0x52][0x90] = 0x253;
                int_1[0x33][0xb3] = 0x252;
                int_1[0x53][0x9a] = 0x251;
                int_1[0x47][0x8b] = 0x250;
                int_1[0x40][0x8b] = 0x24f;
                int_1[0x55][0x90] = 590;
                int_1[0x34][0x7d] = 0x24d;
                int_1[0x58][0x19] = 0x24c;
                int_1[0x51][0x6a] = 0x24b;
                int_1[0x51][0x94] = 0x24a;
                int_1[0x3e][0x89] = 0x249;
                int_1[0x5e][0] = 0x248;
                int_1[1][0x40] = 0x247;
                int_1[0x43][0xa3] = 0x246;
                int_1[20][190] = 0x245;
                int_1[0x39][0x83] = 580;
                int_1[0x1d][0xa9] = 0x243;
                int_1[0x48][0x8f] = 0x242;
                int_1[0][0xad] = 0x241;
                int_1[11][0x17] = 0x240;
                int_1[0x3d][0x8d] = 0x23f;
                int_1[60][0x7b] = 0x23e;
                int_1[0x51][0x72] = 0x23d;
                int_1[0x52][0x83] = 0x23c;
                int_1[0x43][0x9c] = 0x23b;
                int_1[0x47][0xa7] = 570;
                int_1[20][50] = 0x239;
                int_1[0x4d][0x84] = 0x238;
                int_1[0x54][0x26] = 0x237;
                int_1[0x1a][0x1d] = 0x236;
                int_1[0x4a][0xbb] = 0x235;
                int_1[0x3e][0x74] = 0x234;
                int_1[0x43][0x87] = 0x233;
                int_1[5][0x56] = 0x232;
                int_1[0x48][0xba] = 0x231;
                int_1[0x4b][0xa1] = 560;
                int_1[0x4e][130] = 0x22f;
                int_1[0x5e][30] = 0x22e;
                int_1[0x54][0x48] = 0x22d;
                int_1[1][0x43] = 0x22c;
                int_1[0x4b][0xac] = 0x22b;
                int_1[0x4a][0xb9] = 0x22a;
                int_1[0x35][160] = 0x229;
                int_1[0x7b][14] = 0x228;
                int_1[0x4f][0x61] = 0x227;
                int_1[0x55][110] = 550;
                int_1[0x4e][0xab] = 0x225;
                int_1[0x34][0x83] = 0x224;
                int_1[0x38][100] = 0x223;
                int_1[50][0xb6] = 0x222;
                int_1[0x5e][0x40] = 0x221;
                int_1[0x6a][0x4a] = 0x220;
                int_1[11][0x66] = 0x21f;
                int_1[0x35][0x7c] = 0x21e;
                int_1[0x18][3] = 0x21d;
                int_1[0x56][0x94] = 540;
                int_1[0x35][0xb8] = 0x21b;
                int_1[0x56][0x93] = 0x21a;
                int_1[0x60][0xa1] = 0x219;
                int_1[0x52][0x4d] = 0x218;
                int_1[0x3b][0x92] = 0x217;
                int_1[0x54][0x7e] = 0x216;
                int_1[0x4f][0x84] = 0x215;
                int_1[0x55][0x7b] = 0x214;
                int_1[0x47][0x65] = 0x213;
                int_1[0x55][0x6a] = 530;
                int_1[6][0xb8] = 0x211;
                int_1[0x39][0x9c] = 0x210;
                int_1[0x4b][0x68] = 0x20f;
                int_1[50][0x89] = 0x20e;
                int_1[0x4f][0x85] = 0x20d;
                int_1[0x4c][0x6c] = 0x20c;
                int_1[0x39][0x8e] = 0x20b;
                int_1[0x54][130] = 0x20a;
                int_1[0x34][0x80] = 0x209;
                int_1[0x2f][0x2c] = 520;
                int_1[0x34][0x98] = 0x207;
                int_1[0x36][0x68] = 0x206;
                int_1[30][0x2f] = 0x205;
                int_1[0x47][0x7b] = 0x204;
                int_1[0x34][0x6b] = 0x203;
                int_1[0x2d][0x54] = 0x202;
                int_1[0x6b][0x76] = 0x201;
                int_1[5][0xa1] = 0x200;
                int_1[0x30][0x7e] = 0x1ff;
                int_1[0x43][170] = 510;
                int_1[0x2b][6] = 0x1fd;
                int_1[70][0x70] = 0x1fc;
                int_1[0x56][0xae] = 0x1fb;
                int_1[0x54][0xa6] = 0x1fa;
                int_1[0x4f][130] = 0x1f9;
                int_1[0x39][0x8d] = 0x1f8;
                int_1[0x51][0xb2] = 0x1f7;
                int_1[0x38][0xbb] = 0x1f6;
                int_1[0x51][0xa2] = 0x1f5;
                int_1[0x35][0x68] = 500;
                int_1[0x7b][0x23] = 0x1f3;
                int_1[70][0xa9] = 0x1f2;
                int_1[0x45][0xa4] = 0x1f1;
                int_1[0x6d][0x3d] = 0x1f0;
                int_1[0x49][130] = 0x1ef;
                int_1[0x3e][0x86] = 0x1ee;
                int_1[0x36][0x7d] = 0x1ed;
                int_1[0x4f][0x69] = 0x1ec;
                int_1[70][0xa5] = 0x1eb;
                int_1[0x47][0xbd] = 490;
                int_1[0x17][0x93] = 0x1e9;
                int_1[0x33][0x8b] = 0x1e8;
                int_1[0x2f][0x89] = 0x1e7;
                int_1[0x4d][0x7b] = 0x1e6;
                int_1[0x56][0xb7] = 0x1e5;
                int_1[0x3f][0xad] = 0x1e4;
                int_1[0x4f][0x90] = 0x1e3;
                int_1[0x54][0x9f] = 0x1e2;
                int_1[60][0x5b] = 0x1e1;
                int_1[0x42][0xbb] = 480;
                int_1[0x49][0x72] = 0x1df;
                int_1[0x55][0x38] = 0x1de;
                int_1[0x47][0x95] = 0x1dd;
                int_1[0x54][0xbd] = 0x1dc;
                int_1[0x68][0x1f] = 0x1db;
                int_1[0x53][0x52] = 0x1da;
                int_1[0x44][0x23] = 0x1d9;
                int_1[11][0x4d] = 0x1d8;
                int_1[15][0x9b] = 0x1d7;
                int_1[0x53][0x99] = 470;
                int_1[0x47][1] = 0x1d5;
                int_1[0x35][190] = 0x1d4;
                int_1[50][0x87] = 0x1d3;
                int_1[3][0x93] = 0x1d2;
                int_1[0x30][0x88] = 0x1d1;
                int_1[0x42][0xa6] = 0x1d0;
                int_1[0x37][0x9f] = 0x1cf;
                int_1[0x52][150] = 0x1ce;
                int_1[0x3a][0xb2] = 0x1cd;
                int_1[0x40][0x66] = 460;
                int_1[0x10][0x6a] = 0x1cb;
                int_1[0x44][110] = 0x1ca;
                int_1[0x36][14] = 0x1c9;
                int_1[60][140] = 0x1c8;
                int_1[0x5b][0x47] = 0x1c7;
                int_1[0x36][150] = 0x1c6;
                int_1[0x4e][0xb1] = 0x1c5;
                int_1[0x4e][0x75] = 0x1c4;
                int_1[0x68][12] = 0x1c3;
                int_1[0x49][150] = 450;
                int_1[0x33][0x8e] = 0x1c1;
                int_1[0x51][0x91] = 0x1c0;
                int_1[0x42][0xb7] = 0x1bf;
                int_1[0x33][0xb2] = 0x1be;
                int_1[0x4b][0x6b] = 0x1bd;
                int_1[0x41][0x77] = 0x1bc;
                int_1[0x45][0xb0] = 0x1bb;
                int_1[0x3b][0x7a] = 0x1ba;
                int_1[0x4e][160] = 0x1b9;
                int_1[0x55][0xb7] = 440;
                int_1[0x69][0x10] = 0x1b7;
                int_1[0x49][110] = 0x1b6;
                int_1[0x68][0x27] = 0x1b5;
                int_1[0x77][0x10] = 0x1b4;
                int_1[0x4c][0xa2] = 0x1b3;
                int_1[0x43][0x98] = 0x1b2;
                int_1[0x52][0x18] = 0x1b1;
                int_1[0x49][0x79] = 0x1b0;
                int_1[0x53][0x53] = 0x1af;
                int_1[0x52][0x91] = 430;
                int_1[0x31][0x85] = 0x1ad;
                int_1[0x5e][13] = 0x1ac;
                int_1[0x3a][0x8b] = 0x1ab;
                int_1[0x4a][0xbd] = 0x1aa;
                int_1[0x42][0xb1] = 0x1a9;
                int_1[0x55][0xb8] = 0x1a8;
                int_1[0x37][0xb7] = 0x1a7;
                int_1[0x47][0x6b] = 0x1a6;
                int_1[11][0x62] = 0x1a5;
                int_1[0x48][0x99] = 420;
                int_1[2][0x89] = 0x1a3;
                int_1[0x3b][0x93] = 0x1a2;
                int_1[0x3a][0x98] = 0x1a1;
                int_1[0x37][0x90] = 0x1a0;
                int_1[0x49][0x7d] = 0x19f;
                int_1[0x34][0x9a] = 0x19e;
                int_1[70][0xb2] = 0x19d;
                int_1[0x4f][0x94] = 0x19c;
                int_1[0x3f][0x8f] = 0x19b;
                int_1[50][140] = 410;
                int_1[0x2f][0x91] = 0x199;
                int_1[0x30][0x7b] = 0x198;
                int_1[0x38][0x6b] = 0x197;
                int_1[0x54][0x53] = 0x196;
                int_1[0x3b][0x70] = 0x195;
                int_1[0x7c][0x48] = 0x194;
                int_1[0x4f][0x63] = 0x193;
                int_1[3][0x25] = 0x192;
                int_1[0x72][0x37] = 0x191;
                int_1[0x55][0x98] = 400;
                int_1[60][0x2f] = 0x18f;
                int_1[0x41][0x60] = 0x18e;
                int_1[0x4a][110] = 0x18d;
                int_1[0x56][0xb6] = 0x18c;
                int_1[50][0x63] = 0x18b;
                int_1[0x43][0xba] = 0x18a;
                int_1[0x51][0x4a] = 0x189;
                int_1[80][0x25] = 0x188;
                int_1[0x15][60] = 0x187;
                int_1[110][12] = 390;
                int_1[60][0xa2] = 0x185;
                int_1[0x1d][0x73] = 0x184;
                int_1[0x53][130] = 0x183;
                int_1[0x34][0x88] = 0x182;
                int_1[0x3f][0x72] = 0x181;
                int_1[0x31][0x7f] = 0x180;
                int_1[0x53][0x6d] = 0x17f;
                int_1[0x42][0x80] = 0x17e;
                int_1[0x4e][0x88] = 0x17d;
                int_1[0x51][180] = 380;
                int_1[0x4c][0x68] = 0x17b;
                int_1[0x38][0x9c] = 0x17a;
                int_1[0x3d][0x17] = 0x179;
                int_1[4][30] = 0x178;
                int_1[0x45][0x9a] = 0x177;
                int_1[100][0x25] = 0x176;
                int_1[0x36][0xb1] = 0x175;
                int_1[0x17][0x77] = 0x174;
                int_1[0x47][0xab] = 0x173;
                int_1[0x54][0x92] = 370;
                int_1[20][0xb8] = 0x171;
                int_1[0x56][0x4c] = 0x170;
                int_1[0x4a][0x84] = 0x16f;
                int_1[0x2f][0x61] = 0x16e;
                int_1[0x52][0x89] = 0x16d;
                int_1[0x5e][0x38] = 0x16c;
                int_1[0x5c][30] = 0x16b;
                int_1[0x13][0x75] = 0x16a;
                int_1[0x30][0xad] = 0x169;
                int_1[2][0x88] = 360;
                int_1[7][0xb6] = 0x167;
                int_1[0x4a][0xbc] = 0x166;
                int_1[14][0x84] = 0x165;
                int_1[0x3e][0xac] = 0x164;
                int_1[0x19][0x27] = 0x163;
                int_1[0x55][0x81] = 0x162;
                int_1[0x40][0x62] = 0x161;
                int_1[0x43][0x7f] = 0x160;
                int_1[0x48][0xa7] = 0x15f;
                int_1[0x39][0x8f] = 350;
                int_1[0x4c][0xbb] = 0x15d;
                int_1[0x53][0xb5] = 0x15c;
                int_1[0x54][10] = 0x15b;
                int_1[0x37][0xa6] = 0x15a;
                int_1[0x37][0xbc] = 0x159;
                int_1[13][0x97] = 0x158;
                int_1[0x3e][0x7c] = 0x157;
                int_1[0x35][0x88] = 0x156;
                int_1[0x6a][0x39] = 0x155;
                int_1[0x2f][0xa6] = 340;
                int_1[0x6d][30] = 0x153;
                int_1[0x4e][0x72] = 0x152;
                int_1[0x53][0x13] = 0x151;
                int_1[0x38][0xa2] = 0x150;
                int_1[60][0xb1] = 0x14f;
                int_1[0x58][9] = 0x14e;
                int_1[0x4a][0xa3] = 0x14d;
                int_1[0x34][0x9c] = 0x14c;
                int_1[0x47][180] = 0x14b;
                int_1[60][0x39] = 330;
                int_1[0x48][0xad] = 0x149;
                int_1[0x52][0x5b] = 0x148;
                int_1[0x33][0xba] = 0x147;
                int_1[0x4b][0x56] = 0x146;
                int_1[0x4b][0x4e] = 0x145;
                int_1[0x4c][170] = 0x144;
                int_1[60][0x93] = 0x143;
                int_1[0x52][0x4b] = 0x142;
                int_1[80][0x94] = 0x141;
                int_1[0x56][150] = 320;
                int_1[13][0x5f] = 0x13f;
                int_1[0][11] = 0x13e;
                int_1[0x54][190] = 0x13d;
                int_1[0x4c][0xa6] = 0x13c;
                int_1[14][0x48] = 0x13b;
                int_1[0x43][0x90] = 0x13a;
                int_1[0x54][0x2c] = 0x139;
                int_1[0x48][0x7d] = 0x138;
                int_1[0x42][0x7f] = 0x137;
                int_1[60][0x19] = 310;
                int_1[70][0x92] = 0x135;
                int_1[0x4f][0x87] = 0x134;
                int_1[0x36][0x87] = 0x133;
                int_1[60][0x68] = 0x132;
                int_1[0x37][0x84] = 0x131;
                int_1[0x5e][2] = 0x130;
                int_1[0x36][0x85] = 0x12f;
                int_1[0x38][190] = 0x12e;
                int_1[0x3a][0xae] = 0x12d;
                int_1[80][0x90] = 300;
                int_1[0x55][0x71] = 0x12b;
            }
            if (int_2[0] == null)
            {
                for (num = 0; num < 0x5e; num++)
                {
                    int_2[num] = new int[0x9e];
                }
                int_2[11][15] = 0x257;
                int_2[3][0x42] = 0x256;
                int_2[6][0x79] = 0x255;
                int_2[3][0] = 0x254;
                int_2[5][0x52] = 0x253;
                int_2[3][0x2a] = 0x252;
                int_2[5][0x22] = 0x251;
                int_2[3][8] = 0x250;
                int_2[3][6] = 0x24f;
                int_2[3][0x43] = 590;
                int_2[7][0x8b] = 0x24d;
                int_2[0x17][0x89] = 0x24c;
                int_2[12][0x2e] = 0x24b;
                int_2[4][8] = 0x24a;
                int_2[4][0x29] = 0x249;
                int_2[0x12][0x2f] = 0x248;
                int_2[12][0x72] = 0x247;
                int_2[6][1] = 0x246;
                int_2[0x16][60] = 0x245;
                int_2[5][0x2e] = 580;
                int_2[11][0x4f] = 0x243;
                int_2[3][0x17] = 0x242;
                int_2[7][0x72] = 0x241;
                int_2[0x1d][0x66] = 0x240;
                int_2[0x13][14] = 0x23f;
                int_2[4][0x85] = 0x23e;
                int_2[3][0x1d] = 0x23d;
                int_2[4][0x6d] = 0x23c;
                int_2[14][0x7f] = 0x23b;
                int_2[5][0x30] = 570;
                int_2[13][0x68] = 0x239;
                int_2[3][0x84] = 0x238;
                int_2[0x1a][0x40] = 0x237;
                int_2[7][0x13] = 0x236;
                int_2[4][12] = 0x235;
                int_2[11][0x7c] = 0x234;
                int_2[7][0x59] = 0x233;
                int_2[15][0x7c] = 0x232;
                int_2[4][0x6c] = 0x231;
                int_2[0x13][0x42] = 560;
                int_2[3][0x15] = 0x22f;
                int_2[0x18][12] = 0x22e;
                int_2[0x1c][0x6f] = 0x22d;
                int_2[12][0x6b] = 0x22c;
                int_2[3][0x70] = 0x22b;
                int_2[8][0x71] = 0x22a;
                int_2[5][40] = 0x229;
                int_2[0x1a][0x91] = 0x228;
                int_2[3][0x30] = 0x227;
                int_2[3][70] = 550;
                int_2[0x16][0x11] = 0x225;
                int_2[0x10][0x2f] = 0x224;
                int_2[3][0x35] = 0x223;
                int_2[4][0x18] = 0x222;
                int_2[0x20][120] = 0x221;
                int_2[0x18][0x31] = 0x220;
                int_2[0x18][0x8e] = 0x21f;
                int_2[0x12][0x42] = 0x21e;
                int_2[0x1d][150] = 0x21d;
                int_2[5][0x7a] = 540;
                int_2[5][0x72] = 0x21b;
                int_2[3][0x2c] = 0x21a;
                int_2[10][0x80] = 0x219;
                int_2[15][20] = 0x218;
                int_2[13][0x21] = 0x217;
                int_2[14][0x57] = 0x216;
                int_2[3][0x7e] = 0x215;
                int_2[4][0x35] = 0x214;
                int_2[4][40] = 0x213;
                int_2[9][0x5d] = 530;
                int_2[15][0x89] = 0x211;
                int_2[10][0x7b] = 0x210;
                int_2[4][0x38] = 0x20f;
                int_2[5][0x47] = 0x20e;
                int_2[10][8] = 0x20d;
                int_2[5][0x10] = 0x20c;
                int_2[5][0x92] = 0x20b;
                int_2[0x12][0x58] = 0x20a;
                int_2[0x18][4] = 0x209;
                int_2[20][0x2f] = 520;
                int_2[5][0x21] = 0x207;
                int_2[9][0x2b] = 0x206;
                int_2[20][12] = 0x205;
                int_2[20][13] = 0x204;
                int_2[5][0x9c] = 0x203;
                int_2[0x16][140] = 0x202;
                int_2[8][0x92] = 0x201;
                int_2[0x15][0x7b] = 0x200;
                int_2[4][90] = 0x1ff;
                int_2[5][0x3e] = 510;
                int_2[0x11][0x3b] = 0x1fd;
                int_2[10][0x25] = 0x1fc;
                int_2[0x12][0x6b] = 0x1fb;
                int_2[14][0x35] = 0x1fa;
                int_2[0x16][0x33] = 0x1f9;
                int_2[8][13] = 0x1f8;
                int_2[5][0x1d] = 0x1f7;
                int_2[9][7] = 0x1f6;
                int_2[0x16][14] = 0x1f5;
                int_2[8][0x37] = 500;
                int_2[0x21][9] = 0x1f3;
                int_2[0x10][0x40] = 0x1f2;
                int_2[7][0x83] = 0x1f1;
                int_2[0x22][4] = 0x1f0;
                int_2[7][0x65] = 0x1ef;
                int_2[11][0x8b] = 0x1ee;
                int_2[3][0x87] = 0x1ed;
                int_2[7][0x66] = 0x1ec;
                int_2[0x11][13] = 0x1eb;
                int_2[3][20] = 490;
                int_2[0x1b][0x6a] = 0x1e9;
                int_2[5][0x58] = 0x1e8;
                int_2[6][0x21] = 0x1e7;
                int_2[5][0x8b] = 0x1e6;
                int_2[6][0] = 0x1e5;
                int_2[0x11][0x3a] = 0x1e4;
                int_2[5][0x85] = 0x1e3;
                int_2[9][0x6b] = 0x1e2;
                int_2[0x17][0x27] = 0x1e1;
                int_2[5][0x17] = 480;
                int_2[3][0x4f] = 0x1df;
                int_2[0x20][0x61] = 0x1de;
                int_2[3][0x88] = 0x1dd;
                int_2[4][0x5e] = 0x1dc;
                int_2[0x15][0x3d] = 0x1db;
                int_2[0x17][0x7b] = 0x1da;
                int_2[0x1a][0x10] = 0x1d9;
                int_2[0x18][0x89] = 0x1d8;
                int_2[0x16][0x12] = 0x1d7;
                int_2[5][1] = 470;
                int_2[20][0x77] = 0x1d5;
                int_2[3][7] = 0x1d4;
                int_2[10][0x4f] = 0x1d3;
                int_2[15][0x69] = 0x1d2;
                int_2[3][0x90] = 0x1d1;
                int_2[12][80] = 0x1d0;
                int_2[15][0x49] = 0x1cf;
                int_2[3][0x13] = 0x1ce;
                int_2[8][0x6d] = 0x1cd;
                int_2[3][15] = 460;
                int_2[0x1f][0x52] = 0x1cb;
                int_2[3][0x2b] = 0x1ca;
                int_2[0x19][0x77] = 0x1c9;
                int_2[0x10][0x6f] = 0x1c8;
                int_2[7][0x4d] = 0x1c7;
                int_2[3][0x5f] = 0x1c6;
                int_2[0x18][0x52] = 0x1c5;
                int_2[7][0x34] = 0x1c4;
                int_2[9][0x97] = 0x1c3;
                int_2[3][0x81] = 450;
                int_2[5][0x57] = 0x1c1;
                int_2[3][0x37] = 0x1c0;
                int_2[8][0x99] = 0x1bf;
                int_2[4][0x53] = 0x1be;
                int_2[3][0x72] = 0x1bd;
                int_2[0x17][0x93] = 0x1bc;
                int_2[15][0x1f] = 0x1bb;
                int_2[3][0x36] = 0x1ba;
                int_2[11][0x7a] = 0x1b9;
                int_2[4][4] = 440;
                int_2[0x22][0x95] = 0x1b7;
                int_2[3][0x11] = 0x1b6;
                int_2[0x15][0x40] = 0x1b5;
                int_2[0x1a][0x90] = 0x1b4;
                int_2[4][0x3e] = 0x1b3;
                int_2[8][15] = 0x1b2;
                int_2[0x23][80] = 0x1b1;
                int_2[7][110] = 0x1b0;
                int_2[0x17][0x72] = 0x1af;
                int_2[3][0x6c] = 430;
                int_2[3][0x3e] = 0x1ad;
                int_2[0x15][0x29] = 0x1ac;
                int_2[15][0x63] = 0x1ab;
                int_2[5][0x2f] = 0x1aa;
                int_2[4][0x60] = 0x1a9;
                int_2[20][0x7a] = 0x1a8;
                int_2[5][0x15] = 0x1a7;
                int_2[4][0x9d] = 0x1a6;
                int_2[0x10][14] = 0x1a5;
                int_2[3][0x75] = 420;
                int_2[7][0x81] = 0x1a3;
                int_2[4][0x1b] = 0x1a2;
                int_2[5][30] = 0x1a1;
                int_2[0x16][0x10] = 0x1a0;
                int_2[5][0x40] = 0x19f;
                int_2[0x11][0x63] = 0x19e;
                int_2[0x11][0x39] = 0x19d;
                int_2[8][0x69] = 0x19c;
                int_2[5][0x70] = 0x19b;
                int_2[20][0x3b] = 410;
                int_2[6][0x81] = 0x199;
                int_2[0x12][0x11] = 0x198;
                int_2[3][0x5c] = 0x197;
                int_2[0x1c][0x76] = 0x196;
                int_2[3][0x6d] = 0x195;
                int_2[0x1f][0x33] = 0x194;
                int_2[13][0x74] = 0x193;
                int_2[6][15] = 0x192;
                int_2[0x24][0x88] = 0x191;
                int_2[12][0x4a] = 400;
                int_2[20][0x58] = 0x18f;
                int_2[0x24][0x44] = 0x18e;
                int_2[3][0x93] = 0x18d;
                int_2[15][0x54] = 0x18c;
                int_2[0x10][0x20] = 0x18b;
                int_2[0x10][0x3a] = 0x18a;
                int_2[7][0x42] = 0x189;
                int_2[0x17][0x6b] = 0x188;
                int_2[9][6] = 0x187;
                int_2[12][0x56] = 390;
                int_2[0x17][0x70] = 0x185;
                int_2[0x25][0x17] = 0x184;
                int_2[3][0x8a] = 0x183;
                int_2[20][0x44] = 0x182;
                int_2[15][0x74] = 0x181;
                int_2[0x12][0x40] = 0x180;
                int_2[12][0x8b] = 0x17f;
                int_2[11][0x9b] = 0x17e;
                int_2[4][0x9c] = 0x17d;
                int_2[12][0x54] = 380;
                int_2[0x12][0x31] = 0x17b;
                int_2[0x19][0x7d] = 0x17a;
                int_2[0x19][0x93] = 0x179;
                int_2[15][110] = 0x178;
                int_2[0x13][0x60] = 0x177;
                int_2[30][0x98] = 0x176;
                int_2[6][0x1f] = 0x175;
                int_2[0x1b][0x75] = 0x174;
                int_2[3][10] = 0x173;
                int_2[6][0x83] = 370;
                int_2[13][0x70] = 0x171;
                int_2[0x24][0x9c] = 0x170;
                int_2[4][60] = 0x16f;
                int_2[15][0x79] = 0x16e;
                int_2[4][0x70] = 0x16d;
                int_2[30][0x8e] = 0x16c;
                int_2[0x17][0x9a] = 0x16b;
                int_2[0x1b][0x65] = 0x16a;
                int_2[9][140] = 0x169;
                int_2[3][0x59] = 360;
                int_2[0x12][0x94] = 0x167;
                int_2[4][0x45] = 0x166;
                int_2[0x10][0x31] = 0x165;
                int_2[6][0x75] = 0x164;
                int_2[0x24][0x37] = 0x163;
                int_2[5][0x7b] = 0x162;
                int_2[4][0x7e] = 0x161;
                int_2[4][0x77] = 0x160;
                int_2[9][0x5f] = 0x15f;
                int_2[5][0x18] = 350;
                int_2[0x10][0x85] = 0x15d;
                int_2[10][0x86] = 0x15c;
                int_2[0x1a][0x3b] = 0x15b;
                int_2[6][0x29] = 0x15a;
                int_2[6][0x92] = 0x159;
                int_2[0x13][0x18] = 0x158;
                int_2[5][0x71] = 0x157;
                int_2[10][0x76] = 0x156;
                int_2[0x22][0x97] = 0x155;
                int_2[9][0x48] = 340;
                int_2[0x1f][0x19] = 0x153;
                int_2[0x12][0x7e] = 0x152;
                int_2[0x12][0x1c] = 0x151;
                int_2[4][0x99] = 0x150;
                int_2[3][0x54] = 0x14f;
                int_2[0x15][0x12] = 0x14e;
                int_2[0x19][0x81] = 0x14d;
                int_2[6][0x6b] = 0x14c;
                int_2[12][0x19] = 0x14b;
                int_2[0x11][0x6d] = 330;
                int_2[7][0x4c] = 0x149;
                int_2[15][15] = 0x148;
                int_2[4][14] = 0x147;
                int_2[0x17][0x58] = 0x146;
                int_2[0x12][2] = 0x145;
                int_2[6][0x58] = 0x144;
                int_2[0x10][0x54] = 0x143;
                int_2[12][0x30] = 0x142;
                int_2[7][0x44] = 0x141;
                int_2[5][50] = 320;
                int_2[13][0x36] = 0x13f;
                int_2[7][0x62] = 0x13e;
                int_2[11][6] = 0x13d;
                int_2[9][80] = 0x13c;
                int_2[0x10][0x29] = 0x13b;
                int_2[7][0x2b] = 0x13a;
                int_2[0x1c][0x75] = 0x139;
                int_2[3][0x33] = 0x138;
                int_2[7][3] = 0x137;
                int_2[20][0x51] = 310;
                int_2[4][2] = 0x135;
                int_2[11][0x10] = 0x134;
                int_2[10][4] = 0x133;
                int_2[10][0x77] = 0x132;
                int_2[6][0x8e] = 0x131;
                int_2[0x12][0x33] = 0x130;
                int_2[8][0x90] = 0x12f;
                int_2[10][0x41] = 0x12e;
                int_2[11][0x40] = 0x12d;
                int_2[11][130] = 300;
                int_2[9][0x5c] = 0x12b;
                int_2[0x12][0x1d] = 0x12a;
                int_2[0x12][0x4e] = 0x129;
                int_2[0x12][0x97] = 0x128;
                int_2[0x21][0x7f] = 0x127;
                int_2[0x23][0x71] = 0x126;
                int_2[10][0x9b] = 0x125;
                int_2[3][0x4c] = 0x124;
                int_2[0x24][0x7b] = 0x123;
                int_2[13][0x8f] = 290;
                int_2[5][0x87] = 0x121;
                int_2[0x17][0x74] = 0x120;
                int_2[6][0x65] = 0x11f;
                int_2[14][0x4a] = 0x11e;
                int_2[7][0x99] = 0x11d;
                int_2[3][0x65] = 0x11c;
                int_2[9][0x4a] = 0x11b;
                int_2[3][0x9c] = 0x11a;
                int_2[4][0x93] = 0x119;
                int_2[9][12] = 280;
                int_2[0x12][0x85] = 0x117;
                int_2[4][0] = 0x116;
                int_2[7][0x9b] = 0x115;
                int_2[9][0x90] = 0x114;
                int_2[0x17][0x31] = 0x113;
                int_2[5][0x59] = 0x112;
                int_2[10][11] = 0x111;
                int_2[3][110] = 0x110;
                int_2[3][40] = 0x10f;
                int_2[0x1d][0x73] = 270;
                int_2[9][100] = 0x10d;
                int_2[0x15][0x43] = 0x10c;
                int_2[0x17][0x91] = 0x10b;
                int_2[10][0x2f] = 0x10a;
                int_2[4][0x1f] = 0x109;
                int_2[4][0x51] = 0x108;
                int_2[0x16][0x3e] = 0x107;
                int_2[4][0x1c] = 0x106;
                int_2[0x1b][0x27] = 0x105;
                int_2[0x1b][0x36] = 260;
                int_2[0x20][0x2e] = 0x103;
                int_2[4][0x4c] = 0x102;
                int_2[0x1a][15] = 0x101;
                int_2[12][0x9a] = 0x100;
                int_2[9][150] = 0xff;
                int_2[15][0x11] = 0xfe;
                int_2[5][0x81] = 0xfd;
                int_2[10][40] = 0xfc;
                int_2[13][0x25] = 0xfb;
                int_2[0x1f][0x68] = 250;
                int_2[3][0x98] = 0xf9;
                int_2[5][0x16] = 0xf8;
                int_2[8][0x30] = 0xf7;
                int_2[4][0x4a] = 0xf6;
                int_2[6][0x11] = 0xf5;
                int_2[30][0x52] = 0xf4;
                int_2[4][0x74] = 0xf3;
                int_2[0x10][0x2a] = 0xf2;
                int_2[5][0x37] = 0xf1;
                int_2[4][0x40] = 240;
                int_2[14][0x13] = 0xef;
                int_2[0x23][0x52] = 0xee;
                int_2[30][0x8b] = 0xed;
                int_2[0x1a][0x98] = 0xec;
                int_2[0x20][0x20] = 0xeb;
                int_2[0x15][0x66] = 0xea;
                int_2[10][0x83] = 0xe9;
                int_2[9][0x80] = 0xe8;
                int_2[3][0x57] = 0xe7;
                int_2[4][0x33] = 230;
                int_2[10][15] = 0xe5;
                int_2[4][150] = 0xe4;
                int_2[7][4] = 0xe3;
                int_2[7][0x33] = 0xe2;
                int_2[7][0x9d] = 0xe1;
                int_2[4][0x92] = 0xe0;
                int_2[4][0x5b] = 0xdf;
                int_2[7][13] = 0xde;
                int_2[0x11][0x74] = 0xdd;
                int_2[0x17][0x15] = 220;
                int_2[5][0x6a] = 0xdb;
                int_2[14][100] = 0xda;
                int_2[10][0x98] = 0xd9;
                int_2[14][0x59] = 0xd8;
                int_2[6][0x8a] = 0xd7;
                int_2[12][0x9d] = 0xd6;
                int_2[10][0x66] = 0xd5;
                int_2[0x13][0x5e] = 0xd4;
                int_2[7][0x4a] = 0xd3;
                int_2[0x12][0x80] = 210;
                int_2[0x1b][0x6f] = 0xd1;
                int_2[11][0x39] = 0xd0;
                int_2[3][0x83] = 0xcf;
                int_2[30][0x17] = 0xce;
                int_2[30][0x7e] = 0xcd;
                int_2[4][0x24] = 0xcc;
                int_2[0x1a][0x7c] = 0xcb;
                int_2[4][0x13] = 0xca;
                int_2[9][0x98] = 0xc9;
            }
            if (vtstbBdLii[0] == null)
            {
                for (num = 0; num < 0x5e; num++)
                {
                    vtstbBdLii[num] = new int[0x5e];
                }
                vtstbBdLii[0x23][0x41] = 0x256;
                vtstbBdLii[0x29][0x1b] = 0x255;
                vtstbBdLii[0x23][0] = 0x254;
                vtstbBdLii[0x27][0x13] = 0x253;
                vtstbBdLii[0x23][0x2a] = 0x252;
                vtstbBdLii[0x26][0x42] = 0x251;
                vtstbBdLii[0x23][8] = 0x250;
                vtstbBdLii[0x23][6] = 0x24f;
                vtstbBdLii[0x23][0x42] = 590;
                vtstbBdLii[0x2b][14] = 0x24d;
                vtstbBdLii[0x45][80] = 0x24c;
                vtstbBdLii[50][0x30] = 0x24b;
                vtstbBdLii[0x24][0x47] = 0x24a;
                vtstbBdLii[0x25][10] = 0x249;
                vtstbBdLii[60][0x34] = 0x248;
                vtstbBdLii[0x33][0x15] = 0x247;
                vtstbBdLii[40][2] = 0x246;
                vtstbBdLii[0x43][0x23] = 0x245;
                vtstbBdLii[0x26][0x4e] = 580;
                vtstbBdLii[0x31][0x12] = 0x243;
                vtstbBdLii[0x23][0x17] = 0x242;
                vtstbBdLii[0x2a][0x53] = 0x241;
                vtstbBdLii[0x4f][0x2f] = 0x240;
                vtstbBdLii[0x3d][0x52] = 0x23f;
                vtstbBdLii[0x26][7] = 0x23e;
                vtstbBdLii[0x23][0x1d] = 0x23d;
                vtstbBdLii[0x25][0x4d] = 0x23c;
                vtstbBdLii[0x36][0x43] = 0x23b;
                vtstbBdLii[0x26][80] = 570;
                vtstbBdLii[0x34][0x4a] = 0x239;
                vtstbBdLii[0x24][0x25] = 0x238;
                vtstbBdLii[0x4a][8] = 0x237;
                vtstbBdLii[0x29][0x53] = 0x236;
                vtstbBdLii[0x24][0x4b] = 0x235;
                vtstbBdLii[0x31][0x3f] = 0x234;
                vtstbBdLii[0x2a][0x3a] = 0x233;
                vtstbBdLii[0x38][0x21] = 0x232;
                vtstbBdLii[0x25][0x4c] = 0x231;
                vtstbBdLii[0x3e][0x27] = 560;
                vtstbBdLii[0x23][0x15] = 0x22f;
                vtstbBdLii[70][0x13] = 0x22e;
                vtstbBdLii[0x4d][0x58] = 0x22d;
                vtstbBdLii[0x33][14] = 0x22c;
                vtstbBdLii[0x24][0x11] = 0x22b;
                vtstbBdLii[0x2c][0x33] = 0x22a;
                vtstbBdLii[0x26][0x48] = 0x229;
                vtstbBdLii[0x4a][90] = 0x228;
                vtstbBdLii[0x23][0x30] = 0x227;
                vtstbBdLii[0x23][0x45] = 550;
                vtstbBdLii[0x42][0x56] = 0x225;
                vtstbBdLii[0x39][20] = 0x224;
                vtstbBdLii[0x23][0x35] = 0x223;
                vtstbBdLii[0x24][0x57] = 0x222;
                vtstbBdLii[0x54][0x43] = 0x221;
                vtstbBdLii[70][0x38] = 0x220;
                vtstbBdLii[0x47][0x36] = 0x21f;
                vtstbBdLii[60][70] = 0x21e;
                vtstbBdLii[80][1] = 0x21d;
                vtstbBdLii[0x27][0x3b] = 540;
                vtstbBdLii[0x27][0x33] = 0x21b;
                vtstbBdLii[0x23][0x2c] = 0x21a;
                vtstbBdLii[0x30][4] = 0x219;
                vtstbBdLii[0x37][0x18] = 0x218;
                vtstbBdLii[0x34][4] = 0x217;
                vtstbBdLii[0x36][0x1a] = 0x216;
                vtstbBdLii[0x24][0x1f] = 0x215;
                vtstbBdLii[0x25][0x16] = 0x214;
                vtstbBdLii[0x25][9] = 0x213;
                vtstbBdLii[0x2e][0] = 530;
                vtstbBdLii[0x38][0x2e] = 0x211;
                vtstbBdLii[0x2f][0x5d] = 0x210;
                vtstbBdLii[0x25][0x19] = 0x20f;
                vtstbBdLii[0x27][8] = 0x20e;
                vtstbBdLii[0x2e][0x49] = 0x20d;
                vtstbBdLii[0x26][0x30] = 0x20c;
                vtstbBdLii[0x27][0x53] = 0x20b;
                vtstbBdLii[60][0x5c] = 0x20a;
                vtstbBdLii[70][11] = 0x209;
                vtstbBdLii[0x3f][0x54] = 520;
                vtstbBdLii[0x26][0x41] = 0x207;
                vtstbBdLii[0x2d][0x2d] = 0x206;
                vtstbBdLii[0x3f][0x31] = 0x205;
                vtstbBdLii[0x3f][50] = 0x204;
                vtstbBdLii[0x27][0x5d] = 0x203;
                vtstbBdLii[0x44][20] = 0x202;
                vtstbBdLii[0x2c][0x54] = 0x201;
                vtstbBdLii[0x42][0x22] = 0x200;
                vtstbBdLii[0x25][0x3a] = 0x1ff;
                vtstbBdLii[0x27][0] = 510;
                vtstbBdLii[0x3b][1] = 0x1fd;
                vtstbBdLii[0x2f][8] = 0x1fc;
                vtstbBdLii[0x3d][0x11] = 0x1fb;
                vtstbBdLii[0x35][0x57] = 0x1fa;
                vtstbBdLii[0x43][0x1a] = 0x1f9;
                vtstbBdLii[0x2b][0x2e] = 0x1f8;
                vtstbBdLii[0x26][0x3d] = 0x1f7;
                vtstbBdLii[0x2d][9] = 0x1f6;
                vtstbBdLii[0x42][0x53] = 0x1f5;
                vtstbBdLii[0x2b][0x58] = 500;
                vtstbBdLii[0x55][20] = 0x1f3;
                vtstbBdLii[0x39][0x24] = 0x1f2;
                vtstbBdLii[0x2b][6] = 0x1f1;
                vtstbBdLii[0x56][0x4d] = 0x1f0;
                vtstbBdLii[0x2a][70] = 0x1ef;
                vtstbBdLii[0x31][0x4e] = 0x1ee;
                vtstbBdLii[0x24][40] = 0x1ed;
                vtstbBdLii[0x2a][0x47] = 0x1ec;
                vtstbBdLii[0x3a][0x31] = 0x1eb;
                vtstbBdLii[0x23][20] = 490;
                vtstbBdLii[0x4c][20] = 0x1e9;
                vtstbBdLii[0x27][0x19] = 0x1e8;
                vtstbBdLii[40][0x22] = 0x1e7;
                vtstbBdLii[0x27][0x4c] = 0x1e6;
                vtstbBdLii[40][1] = 0x1e5;
                vtstbBdLii[0x3b][0] = 0x1e4;
                vtstbBdLii[0x27][70] = 0x1e3;
                vtstbBdLii[0x2e][14] = 0x1e2;
                vtstbBdLii[0x44][0x4d] = 0x1e1;
                vtstbBdLii[0x26][0x37] = 480;
                vtstbBdLii[0x23][0x4e] = 0x1df;
                vtstbBdLii[0x54][0x2c] = 0x1de;
                vtstbBdLii[0x24][0x29] = 0x1dd;
                vtstbBdLii[0x25][0x3e] = 0x1dc;
                vtstbBdLii[0x41][0x43] = 0x1db;
                vtstbBdLii[0x45][0x42] = 0x1da;
                vtstbBdLii[0x49][0x37] = 0x1d9;
                vtstbBdLii[0x47][0x31] = 0x1d8;
                vtstbBdLii[0x42][0x57] = 0x1d7;
                vtstbBdLii[0x26][0x21] = 470;
                vtstbBdLii[0x40][0x3d] = 0x1d5;
                vtstbBdLii[0x23][7] = 0x1d4;
                vtstbBdLii[0x2f][0x31] = 0x1d3;
                vtstbBdLii[0x38][14] = 0x1d2;
                vtstbBdLii[0x24][0x31] = 0x1d1;
                vtstbBdLii[50][0x51] = 0x1d0;
                vtstbBdLii[0x37][0x4c] = 0x1cf;
                vtstbBdLii[0x23][0x13] = 0x1ce;
                vtstbBdLii[0x2c][0x2f] = 0x1cd;
                vtstbBdLii[0x23][15] = 460;
                vtstbBdLii[0x52][0x3b] = 0x1cb;
                vtstbBdLii[0x23][0x2b] = 0x1ca;
                vtstbBdLii[0x49][0] = 0x1c9;
                vtstbBdLii[0x39][0x53] = 0x1c8;
                vtstbBdLii[0x2a][0x2e] = 0x1c7;
                vtstbBdLii[0x24][0] = 0x1c6;
                vtstbBdLii[70][0x58] = 0x1c5;
                vtstbBdLii[0x2a][0x16] = 0x1c4;
                vtstbBdLii[0x2e][0x3a] = 0x1c3;
                vtstbBdLii[0x24][0x22] = 450;
                vtstbBdLii[0x27][0x18] = 0x1c1;
                vtstbBdLii[0x23][0x37] = 0x1c0;
                vtstbBdLii[0x2c][0x5b] = 0x1bf;
                vtstbBdLii[0x25][0x33] = 0x1be;
                vtstbBdLii[0x24][0x13] = 0x1bd;
                vtstbBdLii[0x45][90] = 0x1bc;
                vtstbBdLii[0x37][0x23] = 0x1bb;
                vtstbBdLii[0x23][0x36] = 0x1ba;
                vtstbBdLii[0x31][0x3d] = 0x1b9;
                vtstbBdLii[0x24][0x43] = 440;
                vtstbBdLii[0x58][0x22] = 0x1b7;
                vtstbBdLii[0x23][0x11] = 0x1b6;
                vtstbBdLii[0x41][0x45] = 0x1b5;
                vtstbBdLii[0x4a][0x59] = 0x1b4;
                vtstbBdLii[0x25][0x1f] = 0x1b3;
                vtstbBdLii[0x2b][0x30] = 0x1b2;
                vtstbBdLii[0x59][0x1b] = 0x1b1;
                vtstbBdLii[0x2a][0x4f] = 0x1b0;
                vtstbBdLii[0x45][0x39] = 0x1af;
                vtstbBdLii[0x24][13] = 430;
                vtstbBdLii[0x23][0x3e] = 0x1ad;
                vtstbBdLii[0x41][0x2f] = 0x1ac;
                vtstbBdLii[0x38][8] = 0x1ab;
                vtstbBdLii[0x26][0x4f] = 0x1aa;
                vtstbBdLii[0x25][0x40] = 0x1a9;
                vtstbBdLii[0x40][0x40] = 0x1a8;
                vtstbBdLii[0x26][0x35] = 0x1a7;
                vtstbBdLii[0x26][0x1f] = 0x1a6;
                vtstbBdLii[0x38][0x51] = 0x1a5;
                vtstbBdLii[0x24][0x16] = 420;
                vtstbBdLii[0x2b][4] = 0x1a3;
                vtstbBdLii[0x24][90] = 0x1a2;
                vtstbBdLii[0x26][0x3e] = 0x1a1;
                vtstbBdLii[0x42][0x55] = 0x1a0;
                vtstbBdLii[0x27][1] = 0x19f;
                vtstbBdLii[0x3b][40] = 0x19e;
                vtstbBdLii[0x3a][0x5d] = 0x19d;
                vtstbBdLii[0x2c][0x2b] = 0x19c;
                vtstbBdLii[0x27][0x31] = 0x19b;
                vtstbBdLii[0x40][2] = 410;
                vtstbBdLii[0x29][0x23] = 0x199;
                vtstbBdLii[60][0x16] = 0x198;
                vtstbBdLii[0x23][0x5b] = 0x197;
                vtstbBdLii[0x4e][1] = 0x196;
                vtstbBdLii[0x24][14] = 0x195;
                vtstbBdLii[0x52][0x1d] = 0x194;
                vtstbBdLii[0x34][0x56] = 0x193;
                vtstbBdLii[40][0x10] = 0x192;
                vtstbBdLii[0x5b][0x34] = 0x191;
                vtstbBdLii[50][0x4b] = 400;
                vtstbBdLii[0x40][30] = 0x18f;
                vtstbBdLii[90][0x4e] = 0x18e;
                vtstbBdLii[0x24][0x34] = 0x18d;
                vtstbBdLii[0x37][0x57] = 0x18c;
                vtstbBdLii[0x39][5] = 0x18b;
                vtstbBdLii[0x39][0x1f] = 0x18a;
                vtstbBdLii[0x2a][0x23] = 0x189;
                vtstbBdLii[0x45][50] = 0x188;
                vtstbBdLii[0x2d][8] = 0x187;
                vtstbBdLii[50][0x57] = 390;
                vtstbBdLii[0x45][0x37] = 0x185;
                vtstbBdLii[0x5c][3] = 0x184;
                vtstbBdLii[0x24][0x2b] = 0x183;
                vtstbBdLii[0x40][10] = 0x182;
                vtstbBdLii[0x38][0x19] = 0x181;
                vtstbBdLii[60][0x44] = 0x180;
                vtstbBdLii[0x33][0x2e] = 0x17f;
                vtstbBdLii[50][0] = 0x17e;
                vtstbBdLii[0x26][30] = 0x17d;
                vtstbBdLii[50][0x55] = 380;
                vtstbBdLii[60][0x36] = 0x17b;
                vtstbBdLii[0x49][6] = 0x17a;
                vtstbBdLii[0x49][0x1c] = 0x179;
                vtstbBdLii[0x38][0x13] = 0x178;
                vtstbBdLii[0x3e][0x45] = 0x177;
                vtstbBdLii[0x51][0x42] = 0x176;
                vtstbBdLii[40][0x20] = 0x175;
                vtstbBdLii[0x4c][0x1f] = 0x174;
                vtstbBdLii[0x23][10] = 0x173;
                vtstbBdLii[0x29][0x25] = 370;
                vtstbBdLii[0x34][0x52] = 0x171;
                vtstbBdLii[0x5b][0x48] = 0x170;
                vtstbBdLii[0x25][0x1d] = 0x16f;
                vtstbBdLii[0x38][30] = 0x16e;
                vtstbBdLii[0x25][80] = 0x16d;
                vtstbBdLii[0x51][0x38] = 0x16c;
                vtstbBdLii[70][3] = 0x16b;
                vtstbBdLii[0x4c][15] = 0x16a;
                vtstbBdLii[0x2e][0x2f] = 0x169;
                vtstbBdLii[0x23][0x58] = 360;
                vtstbBdLii[0x3d][0x3a] = 0x167;
                vtstbBdLii[0x25][0x25] = 0x166;
                vtstbBdLii[0x39][0x16] = 0x165;
                vtstbBdLii[0x29][0x17] = 0x164;
                vtstbBdLii[90][0x42] = 0x163;
                vtstbBdLii[0x27][60] = 0x162;
                vtstbBdLii[0x26][0] = 0x161;
                vtstbBdLii[0x25][0x57] = 0x160;
                vtstbBdLii[0x2e][2] = 0x15f;
                vtstbBdLii[0x26][0x38] = 350;
                vtstbBdLii[0x3a][11] = 0x15d;
                vtstbBdLii[0x30][10] = 0x15c;
                vtstbBdLii[0x4a][4] = 0x15b;
                vtstbBdLii[40][0x2a] = 0x15a;
                vtstbBdLii[0x29][0x34] = 0x159;
                vtstbBdLii[0x3d][0x5c] = 0x158;
                vtstbBdLii[0x27][50] = 0x157;
                vtstbBdLii[0x2f][0x58] = 0x156;
                vtstbBdLii[0x58][0x24] = 0x155;
                vtstbBdLii[0x2d][0x49] = 340;
                vtstbBdLii[0x52][3] = 0x153;
                vtstbBdLii[0x3d][0x24] = 0x152;
                vtstbBdLii[60][0x21] = 0x151;
                vtstbBdLii[0x26][0x1b] = 0x150;
                vtstbBdLii[0x23][0x53] = 0x14f;
                vtstbBdLii[0x41][0x18] = 0x14e;
                vtstbBdLii[0x49][10] = 0x14d;
                vtstbBdLii[0x29][13] = 0x14c;
                vtstbBdLii[50][0x1b] = 0x14b;
                vtstbBdLii[0x3b][50] = 330;
                vtstbBdLii[0x2a][0x2d] = 0x149;
                vtstbBdLii[0x37][0x13] = 0x148;
                vtstbBdLii[0x24][0x4d] = 0x147;
                vtstbBdLii[0x45][0x1f] = 0x146;
                vtstbBdLii[60][7] = 0x145;
                vtstbBdLii[40][0x58] = 0x144;
                vtstbBdLii[0x39][0x38] = 0x143;
                vtstbBdLii[50][50] = 0x142;
                vtstbBdLii[0x2a][0x25] = 0x141;
                vtstbBdLii[0x26][0x52] = 320;
                vtstbBdLii[0x34][0x19] = 0x13f;
                vtstbBdLii[0x2a][0x43] = 0x13e;
                vtstbBdLii[0x30][40] = 0x13d;
                vtstbBdLii[0x2d][0x51] = 0x13c;
                vtstbBdLii[0x39][14] = 0x13b;
                vtstbBdLii[0x2a][13] = 0x13a;
                vtstbBdLii[0x4e][0] = 0x139;
                vtstbBdLii[0x23][0x33] = 0x138;
                vtstbBdLii[0x29][0x43] = 0x137;
                vtstbBdLii[0x40][0x17] = 310;
                vtstbBdLii[0x24][0x41] = 0x135;
                vtstbBdLii[0x30][50] = 0x134;
                vtstbBdLii[0x2e][0x45] = 0x133;
                vtstbBdLii[0x2f][0x59] = 0x132;
                vtstbBdLii[0x29][0x30] = 0x131;
                vtstbBdLii[60][0x38] = 0x130;
                vtstbBdLii[0x2c][0x52] = 0x12f;
                vtstbBdLii[0x2f][0x23] = 0x12e;
                vtstbBdLii[0x31][3] = 0x12d;
                vtstbBdLii[0x31][0x45] = 300;
                vtstbBdLii[0x2d][0x5d] = 0x12b;
                vtstbBdLii[60][0x22] = 0x12a;
                vtstbBdLii[60][0x52] = 0x129;
                vtstbBdLii[0x3d][0x3d] = 0x128;
                vtstbBdLii[0x56][0x2a] = 0x127;
                vtstbBdLii[0x59][60] = 0x126;
                vtstbBdLii[0x30][0x1f] = 0x125;
                vtstbBdLii[0x23][0x4b] = 0x124;
                vtstbBdLii[0x5b][0x27] = 0x123;
                vtstbBdLii[0x35][0x13] = 290;
                vtstbBdLii[0x27][0x48] = 0x121;
                vtstbBdLii[0x45][0x3b] = 0x120;
                vtstbBdLii[0x29][7] = 0x11f;
                vtstbBdLii[0x36][13] = 0x11e;
                vtstbBdLii[0x2b][0x1c] = 0x11d;
                vtstbBdLii[0x24][6] = 0x11c;
                vtstbBdLii[0x2d][0x4b] = 0x11b;
                vtstbBdLii[0x24][0x3d] = 0x11a;
                vtstbBdLii[0x26][0x15] = 0x119;
                vtstbBdLii[0x2d][14] = 280;
                vtstbBdLii[0x3d][0x2b] = 0x117;
                vtstbBdLii[0x24][0x3f] = 0x116;
                vtstbBdLii[0x2b][30] = 0x115;
                vtstbBdLii[0x2e][0x33] = 0x114;
                vtstbBdLii[0x44][0x57] = 0x113;
                vtstbBdLii[0x27][0x1a] = 0x112;
                vtstbBdLii[0x2e][0x4c] = 0x111;
                vtstbBdLii[0x24][15] = 0x110;
                vtstbBdLii[0x23][40] = 0x10f;
                vtstbBdLii[0x4f][60] = 270;
                vtstbBdLii[0x2e][7] = 0x10d;
                vtstbBdLii[0x41][0x48] = 0x10c;
                vtstbBdLii[0x45][0x58] = 0x10b;
                vtstbBdLii[0x2f][0x12] = 0x10a;
                vtstbBdLii[0x25][0] = 0x109;
                vtstbBdLii[0x25][0x31] = 0x108;
                vtstbBdLii[0x43][0x25] = 0x107;
                vtstbBdLii[0x24][0x5b] = 0x106;
                vtstbBdLii[0x4b][0x30] = 0x105;
                vtstbBdLii[0x4b][0x3f] = 260;
                vtstbBdLii[0x53][0x57] = 0x103;
                vtstbBdLii[0x25][0x2c] = 0x102;
                vtstbBdLii[0x49][0x36] = 0x101;
                vtstbBdLii[0x33][0x3d] = 0x100;
                vtstbBdLii[0x2e][0x39] = 0xff;
                vtstbBdLii[0x37][0x15] = 0xfe;
                vtstbBdLii[0x27][0x42] = 0xfd;
                vtstbBdLii[0x2f][11] = 0xfc;
                vtstbBdLii[0x34][8] = 0xfb;
                vtstbBdLii[0x52][0x51] = 250;
                vtstbBdLii[0x24][0x39] = 0xf9;
                vtstbBdLii[0x26][0x36] = 0xf8;
                vtstbBdLii[0x2b][0x51] = 0xf7;
                vtstbBdLii[0x25][0x2a] = 0xf6;
                vtstbBdLii[40][0x12] = 0xf5;
                vtstbBdLii[80][90] = 0xf4;
                vtstbBdLii[0x25][0x54] = 0xf3;
                vtstbBdLii[0x39][15] = 0xf2;
                vtstbBdLii[0x26][0x57] = 0xf1;
                vtstbBdLii[0x25][0x20] = 240;
                vtstbBdLii[0x35][0x35] = 0xef;
                vtstbBdLii[0x59][0x1d] = 0xee;
                vtstbBdLii[0x51][0x35] = 0xed;
                vtstbBdLii[0x4b][3] = 0xec;
                vtstbBdLii[0x53][0x49] = 0xeb;
                vtstbBdLii[0x42][13] = 0xea;
                vtstbBdLii[0x30][7] = 0xe9;
                vtstbBdLii[0x2e][0x23] = 0xe8;
                vtstbBdLii[0x23][0x56] = 0xe7;
                vtstbBdLii[0x25][20] = 230;
                vtstbBdLii[0x2e][80] = 0xe5;
                vtstbBdLii[0x26][0x18] = 0xe4;
                vtstbBdLii[0x29][0x44] = 0xe3;
                vtstbBdLii[0x2a][0x15] = 0xe2;
                vtstbBdLii[0x2b][0x20] = 0xe1;
                vtstbBdLii[0x26][20] = 0xe0;
                vtstbBdLii[0x25][0x3b] = 0xdf;
                vtstbBdLii[0x29][0x4d] = 0xde;
                vtstbBdLii[0x3b][0x39] = 0xdd;
                vtstbBdLii[0x44][0x3b] = 220;
                vtstbBdLii[0x27][0x2b] = 0xdb;
                vtstbBdLii[0x36][0x27] = 0xda;
                vtstbBdLii[0x30][0x1c] = 0xd9;
                vtstbBdLii[0x36][0x1c] = 0xd8;
                vtstbBdLii[0x29][0x2c] = 0xd7;
                vtstbBdLii[0x33][0x40] = 0xd6;
                vtstbBdLii[0x2f][0x48] = 0xd5;
                vtstbBdLii[0x3e][0x43] = 0xd4;
                vtstbBdLii[0x2a][0x2b] = 0xd3;
                vtstbBdLii[0x3d][0x26] = 210;
                vtstbBdLii[0x4c][0x19] = 0xd1;
                vtstbBdLii[0x30][0x5b] = 0xd0;
                vtstbBdLii[0x24][0x24] = 0xcf;
                vtstbBdLii[80][0x20] = 0xce;
                vtstbBdLii[0x51][40] = 0xcd;
                vtstbBdLii[0x25][5] = 0xcc;
                vtstbBdLii[0x4a][0x45] = 0xcb;
                vtstbBdLii[0x24][0x52] = 0xca;
                vtstbBdLii[0x2e][0x3b] = 0xc9;
            }
        }
    }
}

