namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.IO.Compression;
    using System.Runtime.CompilerServices;

    public class CheckCode
    {
        private static CheckCode checkCode_0;
        private List<Class8> list_0 = new List<Class8>();

        private CheckCode()
        {
            byte[] buffer = new byte[] { 
                0x1f, 0x8b, 8, 0, 0, 0, 0, 0, 4, 0, 0xc5, 0x58, 0xd9, 0x92, 0x13, 0x31, 
                12, 0x94, 0x9e, 0x93, 12, 0x61, 0x97, 0x2f, 0xe1, 0x58, 0xe0, 0x91, 0x9b, 130, 0x62, 11, 
                0x58, 0xee, 0xff, 0xff, 0x10, 0xd8, 0xcc, 200, 0xea, 150, 0x6c, 0x8f, 0x13, 0x48, 0xe1, 170, 
                0x4d, 70, 150, 0x6d, 0xb5, 0x8e, 150, 0x67, 0x73, 0x7f, 0x3b, 9, 14, 0x25, 0x41, 0x49, 
                0xa3, 0xae, 0xd7, 0x5b, 0xa9, 0xa8, 0xd5, 180, 0x76, 2, 0x6a, 0x5c, 0x52, 0x94, 0x54, 0xed, 
                0x18, 90, 0x7f, 0x18, 0, 0, 0x84, 7, 0x1b, 0x80, 0x4a, 0x9a, 8, 0x35, 0xb8, 0x81, 
                80, 0xe7, 0xad, 190, 0xc4, 0x8e, 0xb1, 0x4f, 0x2d, 0x5f, 0xba, 0x80, 0xbb, 0xfd, 0x9a, 0xad, 
                0x19, 0x36, 0xe5, 0xad, 0x87, 0xf1, 0x10, 0xc0, 0x8d, 0xc6, 80, 0x40, 0x52, 0xf8, 0xb3, 0x98, 
                0x2c, 0xd6, 0xec, 0x59, 0xe7, 13, 0x3e, 15, 0x93, 0x3e, 0x1d, 2, 0x7a, 0x18, 0x8f, 0xb6, 
                0xc7, 70, 0x4e, 1, 0xa3, 150, 220, 0x3a, 0x20, 0x77, 0xbf, 0x2c, 0x24, 0xe4, 0x80, 0xa9, 
                0x20, 20, 0xe5, 0x2d, 0xb5, 0x68, 0xc9, 0x55, 0x89, 0x23, 150, 130, 170, 0xba, 0x58, 0xa6, 
                3, 0x38, 0x71, 0x4b, 0x29, 210, 0x47, 0x80, 0xe3, 0x84, 0x91, 0xf4, 120, 0x43, 100, 0x41, 
                0x7b, 0x73, 0x99, 0x80, 0x42, 0x48, 0, 0xde, 0, 0x12, 0x88, 0x80, 0xdb, 0x51, 0x4a, 0x49, 
                0x84, 0x43, 0xf6, 0x51, 0x90, 0x27, 0x21, 0xc9, 0xf8, 0xac, 0, 0x4d, 0xcd, 70, 9, 0x9d, 
                0x15, 120, 0xe0, 0, 30, 0x44, 0x2a, 0x51, 140, 0xbc, 0xd3, 0xa3, 0x68, 0x8a, 0xd5, 0x3a, 
                0x20, 0x79, 0xba, 0x4d, 0x71, 0x4c, 11, 0x91, 0x98, 0x90, 0x7b, 0x2a, 0x42, 0xc5, 120, 0x7a, 
                0xfc, 0xd5, 0x1b, 0x4b, 9, 0xa7, 0x27, 0x99, 0x38, 5, 1, 0xc2, 0x80, 0x39, 0x9c, 0x67, 
                0xbb, 0x4e, 0x7f, 0x6c, 0x33, 0xdd, 0xed, 0x87, 0x55, 0xda, 0x5d, 0xb5, 0x56, 0x33, 0xc6, 0xf9, 
                0xea, 0x60, 100, 0xcf, 0xa7, 0x41, 0xe0, 0x5c, 0x1c, 0xc4, 0xb2, 0x25, 0xa3, 0x89, 0x88, 0x8d, 
                0x16, 0, 0xb5, 0xed, 0xa5, 0x22, 0x9d, 0x52, 0x41, 0x53, 0x8d, 0x92, 0x7f, 0x31, 0x51, 0x3f, 
                0xa8, 0, 0x85, 0x8a, 0x71, 0x10, 0x92, 120, 0xc4, 0x59, 8, 0x39, 0x69, 0xa9, 0x38, 0x41, 
                0x48, 0xf7, 0x40, 90, 3, 0xd5, 0x3a, 0xf5, 0xe5, 0x9d, 0x33, 0x66, 0xc3, 0xd7, 0x1f, 0xef, 
                0x94, 160, 0x53, 0xea, 0xf4, 0x15, 0xb2, 0x1c, 0x40, 0x2d, 0xcf, 0xaf, 0xce, 0xe9, 0xd4, 0x7a, 
                0x89, 9, 230, 0xdd, 0xdb, 14, 0xb8, 0x58, 0xa7, 0x60, 0x37, 0xfd, 0xf2, 250, 0x2c, 0x4e, 
                0x51, 0x87, 13, 0xfc, 0x16, 0x72, 0x2a, 0x5f, 0xc0, 0x80, 240, 0x54, 0xa7, 0xde, 0xfc, 0x15, 
                0x8b, 0x9a, 0x36, 0x3a, 0x2c, 0x62, 0xfc, 0xd4, 140, 0x31, 0xb7, 0xea, 0xd7, 0x26, 0xc4, 0xaf, 
                0x75, 0xea, 0xdb, 0x8b, 0xff, 0x9b, 0x9b, 80, 0x7e, 0xfe, 0x15, 0xab, 0x17, 0x2f, 150, 150, 
                0xbd, 170, 0x87, 0xdd, 0x77, 0xa3, 0x77, 0xd3, 0x85, 240, 0xe0, 0x58, 0xd5, 0xf6, 140, 0xcd, 
                0xc4, 0x63, 0x52, 0x12, 0x48, 70, 15, 0x93, 90, 0xe3, 0xea, 0x24, 0x67, 0x73, 0x63, 160, 
                0xdf, 0xdf, 0x3d, 0x67, 0xf6, 0xa9, 0xfc, 0xed, 8, 0xe3, 130, 0x57, 8, 0x35, 0x47, 0x68, 
                0x9c, 1, 0x40, 0x87, 0x8b, 0xbd, 12, 0xb3, 0xf4, 0xe1, 0x72, 0xd7, 0x54, 0x62, 0xfd, 0x40, 
                0xed, 0x99, 0xa6, 0x7e, 0x2b, 0xe4, 180, 0xc4, 0x62, 13, 0x79, 0xae, 0x1b, 0xd7, 0xf4, 9, 
                0xb7, 0xe1, 0x7c, 0x44, 9, 0x9a, 0xda, 0xff, 0x52, 0x6a, 60, 0xe1, 200, 0xd7, 0xbd, 0xbb, 
                190, 0x37, 0xfc, 0xd6, 0xd5, 0x4e, 60, 0x40, 0x2a, 0x4b, 0x39, 0x1a, 0xbd, 0x2a, 0xcd, 0xc1, 
                0x18, 0x59, 0x40, 0x62, 120, 0xec, 0x63, 0x19, 0x72, 240, 0xcf, 0xf8, 0x38, 250, 0x42, 0x3a, 
                200, 2, 0xec, 0x5b, 0xeb, 0x8d, 0xae, 0xf1, 0x45, 0xdd, 50, 0x98, 0x35, 60, 0x9f, 0xa6, 
                0x3d, 0xce, 0x13, 0xce, 0x94, 0x38, 0x87, 0, 0x8d, 0x85, 0xc4, 0x70, 0x17, 0x26, 14, 0xa6, 
                30, 0x16, 0xcb, 0xbf, 0x52, 0xdf, 0x29, 0x63, 0xc4, 0xf6, 140, 0x35, 0xba, 0xf2, 0xf9, 0x1f, 
                0xbf, 0x73, 0x1f, 0x91, 0x1b, 0x9e, 0x24, 0x5e, 0x63, 0x22, 130, 0x23, 5, 0x19, 0xb9, 0x71, 
                0x73, 220, 0xcf, 5, 0x88, 0x94, 0x71, 0xdb, 0xdd, 0x48, 0x10, 0xd5, 0x55, 0xb3, 0x52, 0xc3, 
                0x1b, 1, 0x94, 0x13, 0x74, 0x94, 0x3a, 0x80, 0x2f, 0x39, 0xe2, 0x75, 14, 0xf2, 0xc6, 0x18, 
                220, 70, 0xfc, 0xf3, 0xea, 20, 0x80, 0xc1, 0xce, 0x24, 0xee, 0x72, 0xed, 0x94, 0xaf, 0xfb, 
                0xa9, 170, 0x4a, 0xe0, 0xd4, 0x22, 0xc6, 240, 0x57, 0x1d, 0x8e, 210, 0x90, 0xc6, 12, 0xd3, 
                0x9a, 0x53, 0xfb, 0xd6, 0xb7, 0xdd, 20, 0xd4, 0xbd, 0x41, 0xa7, 0x80, 0x7b, 0x23, 0xfe, 0x34, 
                0x56, 13, 150, 70, 2, 0xfe, 0xfd, 0xb2, 0, 0x5f, 1, 0x9c, 160, 50, 0x39, 0xd7, 
                0x90, 0xc2, 0x6c, 0xc7, 0x4e, 0x68, 0x88, 0x7d, 0x9f, 0x9b, 0xcf, 0xa7, 190, 160, 0xfc, 0x18, 
                0x7d, 7, 0x5b, 0xa9, 190, 0x56, 0x1f, 0x67, 0x1a, 0x4a, 0x91, 0x9c, 4, 0x38, 0x53, 0x6b, 
                0x70, 0x68, 0x8f, 0xea, 0xf4, 0x34, 0x87, 0x7f, 110, 130, 0xc3, 0xc1, 0xab, 0x40, 0xc4, 80, 
                0x13, 14, 0x33, 0x5d, 0x67, 0x7d, 1, 0x1f, 0xdb, 0xc0, 0x7f, 0xed, 0x87, 0x7f, 0xbc, 15, 
                0x75, 0xe0, 0xa5, 0xba, 0xc0, 0x84, 0x3d, 0x24, 4, 0xe0, 0xf1, 0x16, 0x41, 0x3b, 0x74, 210, 
                0x52, 0xc5, 0xf8, 0x7c, 0x12, 0xfb, 0xe4, 0x37, 0x5b, 0xfb, 0x57, 0x11, 0xa1, 0x18, 0, 0
             };
            using (MemoryStream stream = new MemoryStream(buffer))
            {
                using (GZipStream stream2 = new GZipStream(stream, CompressionMode.Decompress))
                {
                    using (BinaryReader reader = new BinaryReader(stream2))
                    {
                        int num2;
                        char ch;
                        goto Label_00A6;
                    Label_0040:
                        num2 = reader.ReadByte();
                        int num4 = reader.ReadByte();
                        bool[,] flagArray = new bool[num2, num4];
                        for (int i = 0; i < num2; i++)
                        {
                            for (int j = 0; j < num4; j++)
                            {
                                flagArray[i, j] = reader.ReadBoolean();
                            }
                        }
                        this.list_0.Add(new Class8(ch, flagArray));
                    Label_00A6:
                        ch = reader.ReadChar();
                        if (ch != '\0')
                        {
                            goto Label_0040;
                        }
                    }
                }
            }
        }

        private string method_0(Bitmap bitmap_0)
        {
            string str = string.Empty;
            int width = bitmap_0.Width;
            int height = bitmap_0.Height;
            bool[,] flagArray = this.method_1(bitmap_0);
            int num2 = this.method_2(flagArray, -1);
            while (num2 < (width - 7))
            {
                Class9 class2 = this.method_5(flagArray, num2);
                if (class2.QduqHsysan() > 0.6)
                {
                    str = str + class2.method_0();
                    num2 = class2.X + 10;
                }
                else
                {
                    num2++;
                }
            }
            return str;
        }

        private bool[,] method_1(Bitmap bitmap_0)
        {
            bool[,] flagArray = new bool[bitmap_0.Width, bitmap_0.Height];
            for (int i = 0; i < bitmap_0.Width; i++)
            {
                for (int j = 0; j < bitmap_0.Height; j++)
                {
                    Color pixel = bitmap_0.GetPixel(i, j);
                    flagArray[i, j] = ((pixel.R + pixel.G) + pixel.B) < 500;
                }
            }
            return flagArray;
        }

        private int method_2(bool[,] bool_0, int int_0)
        {
            int length = bool_0.GetLength(0);
            int num2 = bool_0.GetLength(1);
            int_0++;
            while (int_0 < length)
            {
                for (int i = 0; i < num2; i++)
                {
                    if (bool_0[int_0, i])
                    {
                        return int_0;
                    }
                }
                int_0++;
            }
            return int_0;
        }

        private double method_3(bool[,] bool_0, bool[,] bool_1, int int_0, int int_1)
        {
            double num = 0.0;
            double num2 = 0.0;
            int length = bool_1.GetLength(0);
            int num4 = bool_1.GetLength(1);
            int num5 = bool_0.GetLength(0);
            int num6 = bool_0.GetLength(1);
            for (int i = 0; i < length; i++)
            {
                int num9 = i + int_0;
                if ((num9 >= 0) && (num9 < num5))
                {
                    for (int j = 0; j < num4; j++)
                    {
                        int num10 = j + int_1;
                        if ((num10 >= 0) && (num10 < num6))
                        {
                            if (bool_1[i, j])
                            {
                                num++;
                                if (bool_0[num9, num10])
                                {
                                    num2++;
                                }
                                else
                                {
                                    num2--;
                                }
                            }
                            else if (bool_0[num9, num10])
                            {
                                num2 -= 0.55;
                            }
                        }
                    }
                }
            }
            return (num2 / num);
        }

        private Class9 method_4(bool[,] bool_0, bool[,] bool_1, int int_0)
        {
            bool_1.GetLength(0);
            int length = bool_1.GetLength(1);
            bool_0.GetLength(0);
            int num2 = bool_0.GetLength(1);
            double num3 = 0.0;
            Class9 class2 = new Class9();
            for (int i = -2; i < 6; i++)
            {
                for (int j = -3; j < ((num2 - length) + 5); j++)
                {
                    double num6 = this.method_3(bool_0, bool_1, i + int_0, j);
                    if (num6 > num3)
                    {
                        num3 = num6;
                        class2.X = i + int_0;
                        class2.Y = j;
                        class2.method_2(num6);
                    }
                }
            }
            return class2;
        }

        private Class9 method_5(bool[,] bool_0, int int_0)
        {
            Class9 class2 = null;
            foreach (Class8 class3 in this.list_0)
            {
                Class9 class4 = this.method_4(bool_0, class3.method_2(), int_0);
                class4.method_1(class3.method_0());
                if ((class2 == null) || (class2.QduqHsysan() < class4.QduqHsysan()))
                {
                    class2 = class4;
                }
            }
            return class2;
        }

        public static string Read(Bitmap bmp)
        {
            if (checkCode_0 == null)
            {
                checkCode_0 = new CheckCode();
            }
            return checkCode_0.method_0(bmp);
        }

        private class Class8
        {
            [CompilerGenerated]
            private bool[,] bool_0;
            [CompilerGenerated]
            private char char_0;

            public Class8(char char_1, bool[,] bool_1)
            {
                this.method_1(char_1);
                this.method_3(bool_1);
            }

            [CompilerGenerated]
            public char method_0()
            {
                return this.char_0;
            }

            [CompilerGenerated]
            private void method_1(char char_1)
            {
                this.char_0 = char_1;
            }

            [CompilerGenerated]
            public bool[,] method_2()
            {
                return this.bool_0;
            }

            [CompilerGenerated]
            private void method_3(bool[,] bool_1)
            {
                this.bool_0 = bool_1;
            }
        }

        private class Class9
        {
            [CompilerGenerated]
            private char char_0;
            [CompilerGenerated]
            private double double_0;
            [CompilerGenerated]
            private int int_0;
            [CompilerGenerated]
            private int int_1;

            [CompilerGenerated]
            public char method_0()
            {
                return this.char_0;
            }

            [CompilerGenerated]
            public void method_1(char char_1)
            {
                this.char_0 = char_1;
            }

            [CompilerGenerated]
            public void method_2(double double_1)
            {
                this.double_0 = double_1;
            }

            [CompilerGenerated]
            public double QduqHsysan()
            {
                return this.double_0;
            }

            public int X
            {
                [CompilerGenerated]
                get
                {
                    return this.int_0;
                }
                [CompilerGenerated]
                set
                {
                    this.int_0 = value;
                }
            }

            public int Y
            {
                [CompilerGenerated]
                get
                {
                    return this.int_1;
                }
                [CompilerGenerated]
                set
                {
                    this.int_1 = value;
                }
            }
        }
    }
}

