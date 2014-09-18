namespace WHC.OrderWater.Commons
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class ImeHelper
    {
        public const int IME_CHOTKEY_SHAPE_TOGGLE = 0x11;
        public const int IME_CMODE_FULLSHAPE = 8;

        [DllImport("imm32.dll")]
        public static extern IntPtr ImmGetContext(IntPtr hwnd);
        [DllImport("imm32.dll")]
        public static extern bool ImmGetConversionStatus(IntPtr himc, ref int lpdw, ref int lpdw2);
        [DllImport("imm32.dll")]
        public static extern bool ImmGetOpenStatus(IntPtr himc);
        [DllImport("imm32.dll")]
        public static extern bool ImmSetOpenStatus(IntPtr himc, bool b);
        [DllImport("imm32.dll")]
        public static extern int ImmSimulateHotKey(IntPtr hwnd, int lngHotkey);
        public static void SetIme(IntPtr Handel)
        {
            smethod_4(Handel);
        }

        public static void SetIme(Control ctl)
        {
            smethod_0(ctl);
        }

        public static void SetIme(Form frm)
        {
            frm.Paint += new PaintEventHandler(ImeHelper.smethod_1);
            smethod_0(frm);
        }

        private static void smethod_0(Control control_0)
        {
            control_0.Enter += new EventHandler(ImeHelper.smethod_2);
            foreach (Control control in control_0.Controls)
            {
                smethod_0(control);
            }
        }

        private static void smethod_1(Control control_0, object object_0)
        {
            smethod_3(control_0);
        }

        private static void smethod_2(Control control_0, object object_0)
        {
            smethod_3(control_0);
        }

        private static void smethod_3(Control control_0)
        {
            Control control = control_0;
            smethod_4(control.Handle);
        }

        private static void smethod_4(IntPtr intptr_0)
        {
            IntPtr himc = ImmGetContext(intptr_0);
            if (ImmGetOpenStatus(himc))
            {
                int lpdw = 0;
                int num2 = 0;
                if (ImmGetConversionStatus(himc, ref lpdw, ref num2) && ((lpdw & 8) > 0))
                {
                    ImmSimulateHotKey(intptr_0, 0x11);
                }
            }
        }
    }
}

