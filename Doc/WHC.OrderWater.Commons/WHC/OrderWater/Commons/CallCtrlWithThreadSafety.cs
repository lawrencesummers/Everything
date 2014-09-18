namespace WHC.OrderWater.Commons
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class CallCtrlWithThreadSafety
    {
        public static void SetChecked<TObject>(TObject objCtrl, bool isChecked, Form winf) where TObject: CheckBox
        {
            if (objCtrl.InvokeRequired)
            {
                Delegate5 method = new Delegate5(CallCtrlWithThreadSafety.SetChecked<CheckBox>);
                if (!winf.IsDisposed)
                {
                    winf.Invoke(method, new object[] { objCtrl, isChecked, winf });
                }
            }
            else
            {
                objCtrl.Checked = isChecked;
            }
        }

        public static void SetEnable<TObject>(TObject objCtrl, bool enable, Form winf) where TObject: Control
        {
            if (objCtrl.InvokeRequired)
            {
                Delegate3 method = new Delegate3(CallCtrlWithThreadSafety.SetEnable<Control>);
                if (!winf.IsDisposed)
                {
                    winf.Invoke(method, new object[] { objCtrl, enable, winf });
                }
            }
            else
            {
                objCtrl.Enabled = enable;
            }
        }

        public static void SetFocus<TObject>(TObject objCtrl, Form winf) where TObject: Control
        {
            if (objCtrl.InvokeRequired)
            {
                Delegate4 method = new Delegate4(CallCtrlWithThreadSafety.SetFocus<Control>);
                if (!winf.IsDisposed)
                {
                    winf.Invoke(method, new object[] { objCtrl, winf });
                }
            }
            else
            {
                objCtrl.Focus();
            }
        }

        public static void SetText<TObject>(TObject objCtrl, string text, Form winf) where TObject: Control
        {
            if (objCtrl.InvokeRequired)
            {
                NxPuofjuVmstZjvYx2 method = new NxPuofjuVmstZjvYx2(CallCtrlWithThreadSafety.SetText<Control>);
                if (!winf.IsDisposed)
                {
                    winf.Invoke(method, new object[] { objCtrl, text, winf });
                }
            }
            else
            {
                objCtrl.Text = text;
            }
        }

        public static void SetText2<TObject>(TObject objCtrl, string text, Form winf) where TObject: ToolStripStatusLabel
        {
            if (objCtrl.Owner.InvokeRequired)
            {
                Delegate2 method = new Delegate2(CallCtrlWithThreadSafety.SetText2<ToolStripStatusLabel>);
                if (!winf.IsDisposed)
                {
                    winf.Invoke(method, new object[] { objCtrl, text, winf });
                }
            }
            else
            {
                objCtrl.Text = text;
            }
        }

        public static void SetVisible<TObject>(TObject objCtrl, bool isVisible, Form winf) where TObject: Control
        {
            if (objCtrl.InvokeRequired)
            {
                Delegate5 method = new Delegate5(CallCtrlWithThreadSafety.SetChecked<CheckBox>);
                if (!winf.IsDisposed)
                {
                    winf.Invoke(method, new object[] { objCtrl, isVisible, winf });
                }
            }
            else
            {
                objCtrl.Visible = isVisible;
            }
        }

        private delegate void Delegate2(ToolStripStatusLabel objCtrl, string text, Form winf);

        private delegate void Delegate3(Control objCtrl, bool enable, Form winf);

        private delegate void Delegate4(Control objCtrl, Form winf);

        private delegate void Delegate5(CheckBox objCtrl, bool isCheck, Form winf);

        private delegate void NxPuofjuVmstZjvYx2(Control objCtrl, string text, Form winf);
    }
}

