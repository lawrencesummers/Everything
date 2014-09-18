namespace WHC.OrderWater.Commons
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Media;
    using System.Security;
    using System.Security.Permissions;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), HostProtection(SecurityAction.LinkDemand, Resources=HostProtectionResource.ExternalProcessMgmt)]
    public class AudioHelper
    {
        private static SoundPlayer soundPlayer_0;

        public static void Play(string location)
        {
            Play(location, AudioPlayMode.Background);
        }

        public static void Play(Stream stream, AudioPlayMode playMode)
        {
            smethod_2(playMode, "playMode");
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            smethod_1(new SoundPlayer(stream), playMode);
        }

        public static void Play(string location, AudioPlayMode playMode)
        {
            smethod_2(playMode, "playMode");
            SoundPlayer player = new SoundPlayer(smethod_3(location));
            smethod_1(player, playMode);
        }

        public static void Play(byte[] data, AudioPlayMode playMode)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            smethod_2(playMode, "playMode");
            MemoryStream stream = new MemoryStream(data);
            Play(stream, playMode);
            stream.Close();
        }

        public static void PlaySystemSound(SystemSound systemSound)
        {
            if (systemSound == null)
            {
                throw new ArgumentNullException("systemSound");
            }
            systemSound.Play();
        }

        private static void smethod_0(SoundPlayer soundPlayer_1)
        {
            new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
            try
            {
                soundPlayer_1.Stop();
            }
            finally
            {
                CodeAccessPermission.RevertAssert();
            }
        }

        private static void smethod_1(SoundPlayer soundPlayer_1, AudioPlayMode audioPlayMode_0)
        {
            if (soundPlayer_0 != null)
            {
                smethod_0(soundPlayer_0);
            }
            soundPlayer_0 = soundPlayer_1;
            switch (audioPlayMode_0)
            {
                case AudioPlayMode.WaitToComplete:
                    soundPlayer_0.PlaySync();
                    break;

                case AudioPlayMode.Background:
                    soundPlayer_0.Play();
                    break;

                case AudioPlayMode.BackgroundLoop:
                    soundPlayer_0.PlayLooping();
                    break;
            }
        }

        private static void smethod_2(AudioPlayMode audioPlayMode_0, string string_0)
        {
            if ((audioPlayMode_0 < AudioPlayMode.WaitToComplete) || (audioPlayMode_0 > AudioPlayMode.BackgroundLoop))
            {
                throw new InvalidEnumArgumentException(string_0, (int) audioPlayMode_0, typeof(AudioPlayMode));
            }
        }

        private static string smethod_3(string string_0)
        {
            if (string.IsNullOrEmpty(string_0))
            {
                throw new ArgumentNullException("location");
            }
            return string_0;
        }

        public static void Stop()
        {
            SoundPlayer player = new SoundPlayer();
            smethod_0(player);
        }
    }
}

