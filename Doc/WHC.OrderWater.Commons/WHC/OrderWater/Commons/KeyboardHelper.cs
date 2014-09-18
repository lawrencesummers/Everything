namespace WHC.OrderWater.Commons
{
    using System;
    using System.Security.Permissions;
    using System.Windows.Forms;

    [HostProtection(SecurityAction.LinkDemand, Resources=HostProtectionResource.ExternalProcessMgmt)]
    public class KeyboardHelper
    {
        public static void SendKeys(string keys)
        {
            SendKeys(keys, false);
        }

        public static void SendKeys(string keys, bool wait)
        {
            if (wait)
            {
                System.Windows.Forms.SendKeys.SendWait(keys);
            }
            else
            {
                System.Windows.Forms.SendKeys.Send(keys);
            }
        }

        public static bool AltKeyDown
        {
            get
            {
                return ((Control.ModifierKeys & Keys.Alt) > Keys.None);
            }
        }

        public static bool CapsLock
        {
            get
            {
                return ((Class22.GetKeyState(20) & 1) > 0);
            }
        }

        public static bool CtrlKeyDown
        {
            get
            {
                return ((Control.ModifierKeys & Keys.Control) > Keys.None);
            }
        }

        public static bool NumLock
        {
            get
            {
                return ((Class22.GetKeyState(0x90) & 1) > 0);
            }
        }

        public static bool ScrollLock
        {
            get
            {
                return ((Class22.GetKeyState(0x91) & 1) > 0);
            }
        }

        public static bool ShiftKeyDown
        {
            get
            {
                return ((Control.ModifierKeys & Keys.Shift) > Keys.None);
            }
        }
    }
}

