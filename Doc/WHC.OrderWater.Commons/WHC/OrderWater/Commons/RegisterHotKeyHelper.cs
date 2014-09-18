namespace WHC.OrderWater.Commons
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;

    public class RegisterHotKeyHelper
    {
        private bool bool_0 = false;
        private Class24 class24_0 = new Class24();
        private HotKeyPass hotKeyPass_0;
        private int int_0 = 0x2710;
        private IntPtr intptr_0 = IntPtr.Zero;
        private System.Windows.Forms.Keys keys_0 = System.Windows.Forms.Keys.A;
        private MODKEY modkey_0 = ((MODKEY) 0);

        public event HotKeyPass HotKey
        {
            add
            {
                HotKeyPass pass2;
                HotKeyPass pass = this.hotKeyPass_0;
                do
                {
                    pass2 = pass;
                    HotKeyPass pass3 = (HotKeyPass) Delegate.Combine(pass2, value);
                    pass = Interlocked.CompareExchange<HotKeyPass>(ref this.hotKeyPass_0, pass3, pass2);
                }
                while (pass != pass2);
            }
            remove
            {
                HotKeyPass pass2;
                HotKeyPass pass = this.hotKeyPass_0;
                do
                {
                    pass2 = pass;
                    HotKeyPass pass3 = (HotKeyPass) Delegate.Remove(pass2, value);
                    pass = Interlocked.CompareExchange<HotKeyPass>(ref this.hotKeyPass_0, pass3, pass2);
                }
                while (pass != pass2);
            }
        }

        private void method_0()
        {
            if (this.hotKeyPass_0 != null)
            {
                this.hotKeyPass_0();
            }
        }

        [DllImport("User32.dll")]
        public static extern bool RegisterHotKey(IntPtr wnd, int id, MODKEY mode, System.Windows.Forms.Keys vk);
        public void StarHotKey()
        {
            if (this.intptr_0 != IntPtr.Zero)
            {
                if (!RegisterHotKey(this.intptr_0, this.int_0, this.modkey_0, this.keys_0))
                {
                    throw new Exception("注册热键失败");
                }
                try
                {
                    this.class24_0.hotKeyPass_0 = new HotKeyPass(this.method_0);
                    this.class24_0.int_0 = this.int_0;
                    this.class24_0.AssignHandle(this.intptr_0);
                    this.bool_0 = true;
                }
                catch
                {
                    this.StopHotKey();
                }
            }
        }

        public void StopHotKey()
        {
            if (this.bool_0)
            {
                if (!UnregisterHotKey(this.intptr_0, this.int_0))
                {
                    throw new Exception("取消热键失败");
                }
                this.bool_0 = false;
                this.class24_0.ReleaseHandle();
            }
        }

        [DllImport("User32.dll")]
        public static extern bool UnregisterHotKey(IntPtr wnd, int id);

        public System.Windows.Forms.Keys Keys
        {
            get
            {
                return this.keys_0;
            }
            set
            {
                if (!this.bool_0)
                {
                    this.keys_0 = value;
                }
            }
        }

        public MODKEY ModKey
        {
            get
            {
                return this.modkey_0;
            }
            set
            {
                if (!this.bool_0)
                {
                    this.modkey_0 = value;
                }
            }
        }

        public IntPtr WindowHandle
        {
            get
            {
                return this.intptr_0;
            }
            set
            {
                if (!this.bool_0)
                {
                    this.intptr_0 = value;
                }
            }
        }

        public int WParam
        {
            get
            {
                return this.int_0;
            }
            set
            {
                if (!this.bool_0)
                {
                    this.int_0 = value;
                }
            }
        }

        private class Class24 : NativeWindow
        {
            public RegisterHotKeyHelper.HotKeyPass hotKeyPass_0;
            public int int_0 = 0x2710;

            protected override void WndProc(ref Message m)
            {
                if (((m.Msg == 0x312) && (m.WParam.ToInt32() == this.int_0)) && (this.hotKeyPass_0 != null))
                {
                    this.hotKeyPass_0();
                }
                base.WndProc(ref m);
            }
        }

        public delegate void HotKeyPass();

        public enum MODKEY
        {
            MOD_ALT = 1,
            MOD_CONTROL = 2,
            MOD_SHIFT = 4,
            MOD_WIN = 8
        }
    }
}

