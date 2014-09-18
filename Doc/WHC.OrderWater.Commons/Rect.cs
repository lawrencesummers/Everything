using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct Rect
{
    public int left;
    public int top;
    public int right;
    public int bottom;
    public int Width
    {
        get
        {
            return (this.right - this.left);
        }
    }
    public int Height
    {
        get
        {
            return (this.bottom - this.top);
        }
    }
}

