namespace WHC.OrderWater.Commons
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    public class SoundPlayerHelper
    {
        public static void Dispose()
        {
            mciSendString("close all", null, 0, IntPtr.Zero);
            mciSendString("clear all", null, 0, IntPtr.Zero);
        }

        public static float GetVolume()
        {
            uint num = 0;
            waveOutGetVolume(IntPtr.Zero, out num);
            ushort num2 = (ushort) (num & 0xffff);
            return (((float) num2) / 65535f);
        }

        [DllImport("winmm.dll")]
        private static extern long mciSendString(string string_0, StringBuilder stringBuilder_0, int int_0, IntPtr intptr_0);
        public static void Pause()
        {
            mciSendString("stop MediaFile", null, 0, IntPtr.Zero);
        }

        public static void Play(byte[] soundEmbeddedResource, bool Repeat)
        {
            smethod_0(soundEmbeddedResource, Path.GetTempPath() + "resource.tmp");
            mciSendString("open \"" + Path.GetTempPath() + "resource.tmp\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
            mciSendString("play MediaFile" + (Repeat ? " repeat" : string.Empty), null, 0, IntPtr.Zero);
        }

        public static void Play(string soundFileName, bool Repeat)
        {
            mciSendString("open \"" + soundFileName + "\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
            mciSendString("play MediaFile" + (Repeat ? " repeat" : string.Empty), null, 0, IntPtr.Zero);
        }

        public static void SetVolume(float volume)
        {
            volume = 65535f * volume;
            uint num = (((uint) volume) & 0xffff) | (((uint) volume) << 0x10);
            waveOutSetVolume(IntPtr.Zero, num);
        }

        private static void smethod_0(byte[] byte_0, string string_0)
        {
            if (!File.Exists(string_0))
            {
                FileStream output = new FileStream(string_0, FileMode.OpenOrCreate);
                BinaryWriter writer = new BinaryWriter(output);
                foreach (byte num2 in byte_0)
                {
                    writer.Write(num2);
                }
                writer.Close();
                output.Close();
            }
        }

        public static void Stop()
        {
            mciSendString("close MediaFile", null, 0, IntPtr.Zero);
        }

        [DllImport("winmm.dll")]
        private static extern int waveOutGetVolume(IntPtr intptr_0, out uint uint_0);
        [DllImport("winmm.dll")]
        private static extern int waveOutSetVolume(IntPtr intptr_0, uint uint_0);

        public static string Status
        {
            get
            {
                StringBuilder builder = new StringBuilder(0x80);
                mciSendString("status MediaFile mode", builder, 0x80, IntPtr.Zero);
                return builder.ToString();
            }
        }

        public static float VolumePercent
        {
            get
            {
                return (float) Math.Round((double) (GetVolume() * 100f), 0);
            }
            set
            {
                SetVolume(((float) Math.Round((double) value, 0)) / 100f);
            }
        }
    }
}

