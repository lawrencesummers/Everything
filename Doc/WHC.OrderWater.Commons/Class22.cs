using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;

[SuppressUnmanagedCodeSecurity]
internal static class Class22
{
    public static Guid guid_0 = new Guid("{0000010d-0000-0000-C000-000000000046}");

    [DllImport("User32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
    public static extern short GetKeyState(int int_0);

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("0000010d-0000-0000-C000-000000000046")]
    public interface Interface0
    {
        [PreserveSig]
        int imethod_0([In, MarshalAs(UnmanagedType.U4)] int int_0, int int_1, IntPtr intptr_0, [In] Class18.Class19 class19_0, IntPtr intptr_1, IntPtr intptr_2, [In] Class18.Class20 class20_0, [In] Class18.Class20 class20_1, IntPtr intptr_3, [In] int int_2);
        [PreserveSig]
        int imethod_1([In, MarshalAs(UnmanagedType.U4)] int int_0, int int_1, IntPtr intptr_0, [In] Class18.Class19 class19_0, IntPtr intptr_1, [Out] Class18.Class21 class21_0);
        [PreserveSig]
        int imethod_2([In, MarshalAs(UnmanagedType.U4)] int int_0, int int_1, IntPtr intptr_0, [Out] IntPtr intptr_1);
        [PreserveSig]
        int imethod_3([In, MarshalAs(UnmanagedType.U4)] int int_0);
        void imethod_4([In, MarshalAs(UnmanagedType.U4)] int int_0, [In, MarshalAs(UnmanagedType.U4)] int int_1, [In, MarshalAs(UnmanagedType.Interface)] IAdviseSink iadviseSink_0);
        void imethod_5([In, Out, MarshalAs(UnmanagedType.LPArray)] int[] int_0, [In, Out, MarshalAs(UnmanagedType.LPArray)] int[] int_1, [In, Out, MarshalAs(UnmanagedType.LPArray)] IAdviseSink[] iadviseSink_0);
    }
}

