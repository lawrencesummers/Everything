namespace WHC.OrderWater.Commons
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class INIFileUtil
    {
        private string string_0;

        public INIFileUtil(string INIPath)
        {
            this.string_0 = INIPath;
        }

        public void ClearAllSection()
        {
            this.IniWriteValue(null, null, null);
        }

        public void ClearSection(string Section)
        {
            this.IniWriteValue(Section, null, null);
        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string string_1, string string_2, string string_3, StringBuilder stringBuilder_0, int int_0, string string_4);
        [DllImport("kernel32", EntryPoint="GetPrivateProfileString")]
        private static extern int GetPrivateProfileString_1(string string_1, string string_2, string string_3, byte[] byte_0, int int_0, string string_4);
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder builder = new StringBuilder(0xff);
            GetPrivateProfileString(Section, Key, "", builder, 0xff, this.string_0);
            return builder.ToString();
        }

        public byte[] IniReadValues(string section, string key)
        {
            byte[] buffer = new byte[0xff];
            GetPrivateProfileString_1(section, key, "", buffer, 0xff, this.string_0);
            return buffer;
        }

        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.string_0);
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string string_1, string string_2, string string_3, string string_4);
    }
}

