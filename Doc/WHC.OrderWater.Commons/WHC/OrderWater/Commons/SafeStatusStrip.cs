namespace WHC.OrderWater.Commons
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class SafeStatusStrip : StatusStrip
    {
        [CompilerGenerated]
        private ToolStripItemCollection method_0()
        {
            return base.Items;
        }

        [CompilerGenerated]
        private void method_1(ToolStripLabel toolStripLabel_0, string string_0)
        {
            foreach (ToolStripItem item in this.method_0())
            {
                if (item == toolStripLabel_0)
                {
                    item.Text = string_0;
                }
            }
        }

        public void SafeSetText(ToolStripLabel toolStripLabel, string text)
        {
            Delegate1 delegate2 = null;
            if (base.InvokeRequired)
            {
                if (delegate2 == null)
                {
                    delegate2 = new Delegate1(this.method_1);
                }
                Delegate1 method = delegate2;
                try
                {
                    base.Invoke(method, new object[] { toolStripLabel, text });
                }
                catch
                {
                }
            }
            else
            {
                foreach (ToolStripItem item in base.Items)
                {
                    if (item == toolStripLabel)
                    {
                        item.Text = text;
                    }
                }
            }
        }

        private delegate void Delegate1(ToolStripLabel toolStrip, string text);
    }
}

