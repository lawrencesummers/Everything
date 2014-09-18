namespace WHC.OrderWater.Commons
{
    using System;
    using System.Windows.Forms;

    public class PrintFormHelper
    {
        public static void Print(Control control)
        {
            ScreenCapture capture = new ScreenCapture();
            new ImagePrintHelper(capture.CaptureWindow(control.Handle)).PrintPreview();
        }

        public static void Print(Form form)
        {
            ScreenCapture capture = new ScreenCapture();
            new ImagePrintHelper(capture.CaptureWindow(form.Handle)).PrintPreview();
        }
    }
}

