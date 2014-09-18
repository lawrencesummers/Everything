namespace RDIFramework.Utilities
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    public class IniFileHelper
    {
        private Hashtable hashtable_0;
        private string string_0 = string.Empty;

        public IniFileHelper(string strFileName)
        {
            this.string_0 = strFileName;
            this.hashtable_0 = new Hashtable();
            this.method_0();
        }

        public void DeleteKey(string sectionName, string keyName)
        {
            sectionName = sectionName.Trim();
            if (this.hashtable_0.ContainsKey(sectionName))
            {
                ((Hashtable) this.hashtable_0[sectionName]).Remove(keyName.Trim());
            }
        }

        public void EraseSection(string sectionName)
        {
            this.hashtable_0.Remove(sectionName);
        }

        [DllImport("kernel32", CharSet=CharSet.Auto)]
        protected static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        public bool KeyExists(string sectionName, string keyName)
        {
            sectionName = sectionName.Trim();
            if (!this.hashtable_0.ContainsKey(sectionName))
            {
                return false;
            }
            return ((Hashtable) this.hashtable_0[sectionName]).ContainsKey(keyName.Trim());
        }

        private void method_0()
        {
            File.Exists(this.string_0);
            if ((this.string_0.Trim() != "") && File.Exists(this.string_0))
            {
                using (StreamReader reader = new StreamReader(this.string_0, Encoding.GetEncoding("gb2312")))
                {
                    this.method_2(reader);
                }
            }
        }

        private Hashtable method_1(string string_1)
        {
            Hashtable hashtable = new Hashtable();
            this.hashtable_0.Add(string_1, hashtable);
            return hashtable;
        }

        private void method_2(StreamReader streamReader_0)
        {
            Hashtable hashtable = null;
            string str;
            while ((str = streamReader_0.ReadLine()) != null)
            {
                str = str.Trim();
                if ((str != "") && (str.Substring(0, 1) != ";"))
                {
                    if ((str.Substring(0, 1) == "[") && (str.Substring(str.Length - 1, 1) == "]"))
                    {
                        str = str.Substring(1, str.Length - 2);
                        hashtable = this.method_1(str.Trim());
                    }
                    else
                    {
                        int index = str.IndexOf('=');
                        if ((index > 0) && (hashtable != null))
                        {
                            string str3;
                            string key = str.Substring(0, index).Trim();
                            if (key.Length == 0)
                            {
                                throw new Exception("IniFile Syntax Error!");
                            }
                            if (key.Length < (str.Length - 1))
                            {
                                str3 = str.Substring(index + 1, (str.Length - 1) - index);
                            }
                            else
                            {
                                str3 = "";
                            }
                            hashtable.Add(key, str3);
                        }
                    }
                }
            }
        }

        public int ReadBinaryStream(string sectionName, string keyName, Stream defaultValue)
        {
            return 0;
        }

        public bool ReadBool(string sectionName, string keyName, bool defaultValue)
        {
            return Convert.ToBoolean(this.ReadString(sectionName, keyName, Convert.ToString(defaultValue)));
        }

        public DateTime ReadDate(string sectionName, string keyName, DateTime defaultValue)
        {
            return Convert.ToDateTime(this.ReadString(sectionName, keyName, Convert.ToString(defaultValue))).Date;
        }

        public DateTime ReadDateTime(string sectionName, string keyName, DateTime defaultValue)
        {
            return Convert.ToDateTime(this.ReadString(sectionName, keyName, Convert.ToString(defaultValue)));
        }

        public double ReadFloat(string sectionName, string keyName, double defaultValue)
        {
            return Convert.ToDouble(this.ReadString(sectionName, keyName, Convert.ToString(defaultValue)));
        }

        public long ReadInteger(string sectionName, string keyName, long defaultValue)
        {
            return Convert.ToInt64(this.ReadString(sectionName, keyName, Convert.ToString(defaultValue)));
        }

        public ArrayList ReadKeys(string sectionName)
        {
            ArrayList list = new ArrayList();
            Hashtable hashtable = (Hashtable) this.hashtable_0[sectionName.Trim()];
            foreach (DictionaryEntry entry in hashtable)
            {
                list.Add((string) entry.Key);
            }
            return list;
        }

        public ArrayList ReadSections()
        {
            ArrayList list = new ArrayList();
            foreach (DictionaryEntry entry in this.hashtable_0)
            {
                list.Add((string) entry.Key);
            }
            return list;
        }

        public string ReadString(string sectionName, string keyName, string defaultValue = "")
        {
            sectionName = sectionName.Trim();
            keyName = keyName.Trim();
            if (!this.hashtable_0.ContainsKey(sectionName))
            {
                return defaultValue;
            }
            Hashtable hashtable = (Hashtable) this.hashtable_0[sectionName];
            if (!hashtable.ContainsKey(keyName))
            {
                return defaultValue;
            }
            string text1 = (string) hashtable[keyName];
            return (string) hashtable[keyName];
        }

        public DateTime ReadTime(string sectionName, string keyName, DateTime defaultValue)
        {
            return Convert.ToDateTime(this.ReadString(sectionName, keyName, Convert.ToString(defaultValue)));
        }

        public bool SectionExists(string sectionName)
        {
            return this.hashtable_0.ContainsKey(sectionName.Trim());
        }

        public void UpdateFile()
        {
            using (StreamWriter writer = new StreamWriter(this.string_0))
            {
                foreach (DictionaryEntry entry in this.hashtable_0)
                {
                    writer.WriteLine("[" + ((string) entry.Key) + "]");
                    Hashtable hashtable = (Hashtable) this.hashtable_0[(string) entry.Key];
                    foreach (DictionaryEntry entry2 in hashtable)
                    {
                        writer.WriteLine(((string) entry2.Key) + "=" + ((string) entry2.Value));
                    }
                    writer.WriteLine("");
                }
                writer.Close();
            }
        }

        public void WriteBinaryStream(string sectionName, string keyName, Stream streamValue)
        {
        }

        public void WriteBool(string sectionName, string keyName, bool boolValue)
        {
            this.WriteString(sectionName, keyName, Convert.ToString(boolValue));
        }

        public void WriteDate(string sectionName, string keyName, DateTime dateValue)
        {
            this.WriteString(sectionName, keyName, Convert.ToString(dateValue.Date));
        }

        public void WriteDateTime(string sectionName, string keyName, DateTime dateTimeValue)
        {
            this.WriteString(sectionName, keyName, Convert.ToString(dateTimeValue));
        }

        public void WriteFloat(string sectionName, string keyName, double doubleValue)
        {
            this.WriteString(sectionName, keyName, Convert.ToString(doubleValue));
        }

        public void WriteInteger(string sectionName, string keyName, long longValue)
        {
            this.WriteString(sectionName, keyName, Convert.ToString(longValue));
        }

        [DllImport("kernel32", CharSet=CharSet.Auto)]
        protected static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        public void WriteString(string sectionName, string keyName, string stringValue)
        {
            Hashtable hashtable;
            sectionName = sectionName.Trim();
            keyName = keyName.Trim();
            stringValue = stringValue.Trim();
            this.SectionExists(sectionName);
            if (!this.SectionExists(sectionName))
            {
                hashtable = this.method_1(sectionName);
            }
            else
            {
                hashtable = (Hashtable) this.hashtable_0[sectionName];
            }
            if (hashtable.ContainsKey(keyName))
            {
                hashtable[keyName] = stringValue;
            }
            else
            {
                hashtable.Add(keyName, stringValue);
            }
        }

        public void WriteTime(string sectionName, string keyName, DateTime timeValue)
        {
            this.WriteString(sectionName, keyName, Convert.ToString(timeValue.TimeOfDay));
        }

        public string FileName
        {
            get
            {
                return this.string_0;
            }
        }
    }
}

