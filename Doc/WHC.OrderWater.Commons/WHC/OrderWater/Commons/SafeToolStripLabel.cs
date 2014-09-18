namespace WHC.OrderWater.Commons
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class SafeToolStripLabel : ToolStripStatusLabel
    {
        [CompilerGenerated]
        private string method_0()
        {
            return base.Text;
        }

        [CompilerGenerated]
        private void method_1(string string_0)
        {
            base.Text = string_0;
        }

        [CompilerGenerated]
        private string method_2()
        {
            return this.method_0();
        }

        [CompilerGenerated]
        private void method_3(string string_0)
        {
            this.method_1(string_0);
        }

        public override string Text
        {
            get
            {
                Delegate7 delegate2 = null;
                if ((base.Parent != null) && base.Parent.InvokeRequired)
                {
                    if (delegate2 == null)
                    {
                        delegate2 = new Delegate7(this.method_2);
                    }
                    Delegate7 method = delegate2;
                    string str = string.Empty;
                    try
                    {
                        str = (string) base.Parent.Invoke(method, null);
                    }
                    catch
                    {
                    }
                    return str;
                }
                return base.Text;
            }
            set
            {
                Delegate6 delegate2 = null;
                if ((base.Parent != null) && base.Parent.InvokeRequired)
                {
                    if (delegate2 == null)
                    {
                        delegate2 = new Delegate6(this.method_3);
                    }
                    Delegate6 method = delegate2;
                    try
                    {
                        base.Parent.Invoke(method, new object[] { value });
                    }
                    catch
                    {
                    }
                }
                else
                {
                    base.Text = value;
                }
            }
        }

        private delegate void Delegate6(string text);

        private delegate string Delegate7();
    }
}

