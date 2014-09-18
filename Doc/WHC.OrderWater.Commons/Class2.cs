using System;
using System.Runtime.InteropServices;

internal class Class2
{
    [DllImport("User32.dll")]
    public static extern int DestroyIcon(IntPtr intptr_0);
}

