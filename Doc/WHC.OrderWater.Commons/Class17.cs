using System;
using System.Drawing;
using System.Runtime.InteropServices;

internal class Class17
{
    public Bitmap method_0(object object_0, Rectangle rectangle_0)
    {
        if (object_0 == null)
        {
            return null;
        }
        if (!Marshal.IsComObject(object_0))
        {
            return null;
        }
        IntPtr zero = IntPtr.Zero;
        Bitmap image = new Bitmap(rectangle_0.Width, rectangle_0.Height);
        Graphics graphics = Graphics.FromImage(image);
        int num1 = Marshal.QueryInterface(Marshal.GetIUnknownForObject(object_0), ref Class22.guid_0, out zero);
        try
        {
            (Marshal.GetTypedObjectForIUnknown(zero, typeof(Class22.Interface0)) as Class22.Interface0).imethod_0(1, -1, IntPtr.Zero, null, IntPtr.Zero, graphics.GetHdc(), new Class18.Class20(rectangle_0), null, IntPtr.Zero, 0);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            throw exception;
        }
        graphics.Dispose();
        return image;
    }
}

