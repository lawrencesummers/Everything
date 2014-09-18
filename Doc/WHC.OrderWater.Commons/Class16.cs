using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WHC.OrderWater.Commons;

internal class Class16 : IDisposable
{
    private bool bool_0 = false;
    private int int_0 = 0x400;
    private int int_1 = 0x300;
    private Uri uri_0 = new Uri("about:blank");
    private WebBrowser webBrowser_0 = new WebBrowser();

    public Class16(Uri uri_1, int int_2, int int_3, bool bool_1)
    {
        this.webBrowser_0.ScriptErrorsSuppressed = false;
        this.webBrowser_0.ScrollBarsEnabled = false;
        this.webBrowser_0.Size = new Size(0x400, 0x300);
        this.webBrowser_0.NewWindow += new CancelEventHandler(this.webBrowser_0_NewWindow);
        this.int_0 = int_2;
        this.int_1 = int_3;
        this.uri_0 = uri_1;
    }

    public void Dispose()
    {
        this.webBrowser_0.Dispose();
    }

    public Uri method_0()
    {
        return this.uri_0;
    }

    public void method_1(Uri uri_1)
    {
        this.uri_0 = uri_1;
    }

    public int method_2()
    {
        return this.int_0;
    }

    public void method_3(int int_2)
    {
        this.int_0 = int_2;
    }

    public int method_4()
    {
        return this.int_1;
    }

    public void method_5(int int_2)
    {
        this.int_1 = int_2;
    }

    protected void method_6()
    {
        try
        {
            this.webBrowser_0.Navigate(this.uri_0);
            while (this.webBrowser_0.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }
            this.webBrowser_0.Stop();
            if (this.webBrowser_0.ActiveXInstance == null)
            {
                throw new Exception("实例不能为空");
            }
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }

    public Bitmap method_7()
    {
        Bitmap bitmap2;
        int width = this.webBrowser_0.Width;
        int height = this.webBrowser_0.Height;
        Size size = this.webBrowser_0.Size;
        if (this.bool_0)
        {
            height = this.webBrowser_0.Document.Body.ScrollRectangle.Height;
            width = this.webBrowser_0.Document.Body.ScrollRectangle.Width;
        }
        if (width < this.int_0)
        {
            width = this.int_0;
        }
        if (height < size.Height)
        {
            height = size.Height;
        }
        this.webBrowser_0.Size = new Size(width, height);
        try
        {
            this.method_6();
            Class17 class2 = new Class17();
            Bitmap bitmap = (Bitmap) ImageHelper.ResizeImageToAFixedSize(class2.method_0(this.webBrowser_0.ActiveXInstance, new Rectangle(0, 0, width, height)), this.int_0, this.int_1, ImageHelper.ScaleMode.W);
            bitmap2 = bitmap;
        }
        catch (Exception exception)
        {
            throw exception;
        }
        return bitmap2;
    }

    public void webBrowser_0_NewWindow(object sender, CancelEventArgs e)
    {
        e.Cancel = true;
    }
}

