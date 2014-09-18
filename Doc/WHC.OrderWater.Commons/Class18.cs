using System;
using System.Drawing;
using System.Runtime.InteropServices;

internal static class Class18
{
    [StructLayout(LayoutKind.Sequential)]
    public sealed class Class19
    {
        [MarshalAs(UnmanagedType.U4)]
        public int int_0;
        [MarshalAs(UnmanagedType.U2)]
        public short short_0;
        [MarshalAs(UnmanagedType.U2)]
        public short short_1;
        [MarshalAs(UnmanagedType.U2)]
        public short eTiormMuIu;
        [MarshalAs(UnmanagedType.U2)]
        public short short_2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class Class20
    {
        public int int_0;
        public int int_1;
        public int int_2;
        public int int_3;
        public Class20()
        {
        }

        public Class20(Rectangle rectangle_0)
        {
            this.int_0 = rectangle_0.X;
            this.int_1 = rectangle_0.Y;
            this.int_2 = rectangle_0.Right;
            this.int_3 = rectangle_0.Bottom;
        }

        public Class20(int int_4, int int_5, int int_6, int int_7)
        {
            this.int_0 = int_4;
            this.int_1 = int_5;
            this.int_2 = int_6;
            this.int_3 = int_7;
        }

        public static Class18.Class20 smethod_0(int int_4, int int_5, int int_6, int int_7)
        {
            return new Class18.Class20(int_4, int_5, int_4 + int_6, int_5 + int_7);
        }

        public override string ToString()
        {
            return string.Concat(new object[] { "Left = ", this.int_0, " Top ", this.int_1, " Right = ", this.int_2, " Bottom = ", this.int_3 });
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class Class21
    {
        [MarshalAs(UnmanagedType.U2)]
        public short short_0;
        [MarshalAs(UnmanagedType.U2)]
        public short short_1;
    }
}

