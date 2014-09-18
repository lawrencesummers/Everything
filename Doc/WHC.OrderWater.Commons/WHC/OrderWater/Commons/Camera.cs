namespace WHC.OrderWater.Commons
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class Camera
    {
        private Class4.Delegate0 delegate0_0;
        private int int_0;
        private int int_1;
        private IntPtr intptr_0;
        private IntPtr intptr_1;
        private RecievedFrameEventHandler recievedFrameEventHandler_0;

        public event RecievedFrameEventHandler RecievedFrame
        {
            add
            {
                RecievedFrameEventHandler handler2;
                RecievedFrameEventHandler handler = this.recievedFrameEventHandler_0;
                do
                {
                    handler2 = handler;
                    RecievedFrameEventHandler handler3 = (RecievedFrameEventHandler) Delegate.Combine(handler2, value);
                    handler = Interlocked.CompareExchange<RecievedFrameEventHandler>(ref this.recievedFrameEventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
            remove
            {
                RecievedFrameEventHandler handler2;
                RecievedFrameEventHandler handler = this.recievedFrameEventHandler_0;
                do
                {
                    handler2 = handler;
                    RecievedFrameEventHandler handler3 = (RecievedFrameEventHandler) Delegate.Remove(handler2, value);
                    handler = Interlocked.CompareExchange<RecievedFrameEventHandler>(ref this.recievedFrameEventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
        }

        public Camera(IntPtr handle, int width, int height)
        {
            this.intptr_1 = handle;
            this.int_0 = width;
            this.int_1 = height;
        }

        public void CloseWebcam()
        {
            this.method_1(this.intptr_0);
        }

        public void GrabImage(string path)
        {
            Class4.SendMessage_1(this.intptr_0, 0x419, 0, Marshal.StringToHGlobalAnsi(path).ToInt32());
        }

        public bool GrabImageToClipBoard()
        {
            return Class4.SendMessage_1(this.intptr_0, 0x41e, 0, 0);
        }

        private bool method_0(IntPtr intptr_2, short short_0)
        {
            return Class4.SendMessage_1(intptr_2, 0x40a, short_0, 0);
        }

        private bool method_1(IntPtr intptr_2)
        {
            return Class4.SendMessage_1(intptr_2, 0x40b, 0, 0);
        }

        private bool method_2(IntPtr intptr_2, bool bool_0)
        {
            return Class4.SendMessage(intptr_2, 0x432, bool_0, 0);
        }

        private bool method_3(IntPtr intptr_2, short short_0)
        {
            return Class4.SendMessage_1(intptr_2, 0x434, short_0, 0);
        }

        private bool method_4(IntPtr intptr_2, Class4.Delegate0 delegate0_1)
        {
            return Class4.SendMessage_2(intptr_2, 0x405, 0, delegate0_1);
        }

        private bool method_5(IntPtr intptr_2, ref Class4.Struct6 struct6_0, int int_2)
        {
            return Class4.SendMessage_3(intptr_2, 0x42d, int_2, ref struct6_0);
        }

        private void method_6(IntPtr intptr_2, IntPtr intptr_3)
        {
            Class4.Struct4 struct2 = new Class4.Struct4();
            struct2 = (Class4.Struct4) Class4.smethod_0(intptr_3, struct2);
            byte[] buffer = new byte[struct2.int_2];
            Class4.smethod_3(struct2.int_0, buffer);
            if (this.recievedFrameEventHandler_0 != null)
            {
                this.recievedFrameEventHandler_0(buffer);
            }
        }

        private bool method_7(IntPtr intptr_2, bool bool_0)
        {
            return Class4.SendMessage(intptr_2, 0x433, bool_0, 0);
        }

        public void SetCaptureFormat()
        {
            Class4.Struct7 struct2 = new Class4.Struct7();
            Class4.SendMessage_4(this.intptr_0, 0x40e, Class4.smethod_4(struct2), ref struct2);
            if (struct2.bool_1)
            {
                Class4.SendMessage_1(this.intptr_0, 0x429, 0, 0);
            }
        }

        public void SetCaptureSource()
        {
            Class4.Struct7 struct2 = new Class4.Struct7();
            Class4.SendMessage_4(this.intptr_0, 0x40e, Class4.smethod_4(struct2), ref struct2);
            if (struct2.bool_1)
            {
                Class4.SendMessage_1(this.intptr_0, 0x42a, 0, 0);
            }
        }

        public void StartWebCam()
        {
            byte[] buffer = new byte[100];
            byte[] buffer2 = new byte[100];
            Class4.capGetDriverDescriptionA(1, buffer, 100, buffer2, 100);
            this.intptr_0 = Class4.capCreateCaptureWindowA(buffer, 0x50000000, 0, 0, this.int_0, this.int_1, this.intptr_1, 0);
            if (this.method_0(this.intptr_0, 0))
            {
                this.method_3(this.intptr_0, 0x42);
                this.method_2(this.intptr_0, true);
                this.method_7(this.intptr_0, true);
                Class4.Struct6 struct2 = new Class4.Struct6();
                struct2.struct5_0.int_0 = Class4.smethod_4(struct2.struct5_0);
                struct2.struct5_0.int_1 = this.int_0;
                struct2.struct5_0.int_2 = this.int_1;
                struct2.struct5_0.short_0 = 1;
                struct2.struct5_0.short_1 = 0x18;
                this.method_5(this.intptr_0, ref struct2, Class4.smethod_4(struct2));
                this.delegate0_0 = new Class4.Delegate0(this.method_6);
                this.method_4(this.intptr_0, this.delegate0_0);
                Class4.SetWindowPos(this.intptr_0, 0, 0, 0, this.int_0, this.int_1, 6);
            }
        }

        public delegate void RecievedFrameEventHandler(byte[] data);
    }
}

