namespace RDIFramework.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    [Serializable, ComVisible(true)]
    public sealed class IniFile
    {
        [CompilerGenerated]
        private static Action<IniFile> action_0;
        [CompilerGenerated]
        private System.Text.Encoding encoding_0;
        [CompilerGenerated]
        private List<string> list_0;
        [CompilerGenerated]
        private IniFileSectionCollection NkgSvdhaX;
        private readonly SimpleTimerProvider<IniFile> simpleTimerProvider_0;
        [CompilerGenerated]
        private string string_0;

        public event EventHandler Saved;

        public IniFile(string fileName) : this(fileName, System.Text.Encoding.Default)
        {
        }

        public IniFile(string fileName, double interval) : this(fileName, System.Text.Encoding.Default, interval)
        {
        }

        public IniFile(string fileName, System.Text.Encoding encoding) : this(fileName, encoding, 0.0)
        {
        }

        public IniFile(string fileName, System.Text.Encoding encoding, double interval)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("fileName");
            }
            if (fileName.Length == 0)
            {
                throw new ArgumentOutOfRangeException("fileName");
            }
            if (!File.Exists(fileName))
            {
                File.WriteAllText(fileName, string.Empty, encoding);
            }
            this.FileName = Path.GetFullPath(fileName);
            this.Encoding = encoding;
            this.Comment = new List<string>();
            this.Sections = new IniFileSectionCollection();
            this.Refresh();
            if (interval > 0.0)
            {
                this.simpleTimerProvider_0 = new SimpleTimerProvider<IniFile>(this, interval);
                if (action_0 == null)
                {
                    action_0 = new Action<IniFile>(IniFile.smethod_1);
                }
                this.simpleTimerProvider_0.Run(action_0);
            }
        }

        public void DeleteKey(string sectionName, string keyName)
        {
            if (sectionName == null)
            {
                throw new ArgumentNullException("sectionName");
            }
            if (sectionName.Length == 0)
            {
                throw new ArgumentOutOfRangeException("sectionName");
            }
            if (keyName == null)
            {
                throw new ArgumentNullException("keyName");
            }
            if (keyName.Length == 0)
            {
                throw new ArgumentOutOfRangeException("keyName");
            }
            if (this.Sections.Contains(sectionName))
            {
                IniFileSection section = this[sectionName];
                if (section.Items.Contains(keyName))
                {
                    section = this[sectionName];
                    section.Items.Remove(keyName);
                }
            }
        }

        public void DeleteSection(string sectionName)
        {
            if (sectionName == null)
            {
                throw new ArgumentNullException("sectionName");
            }
            if (sectionName.Length == 0)
            {
                throw new ArgumentOutOfRangeException("sectionName");
            }
            if (this.Sections.Contains(sectionName))
            {
                this.Sections.Remove(sectionName);
            }
        }

        public bool Read(string sectionName, string keyName, bool defaultValue)
        {
            return smethod_0(this.Read(sectionName, keyName, defaultValue.ToString()));
        }

        public byte Read(string sectionName, string keyName, byte defaultValue)
        {
            byte num;
            if (!byte.TryParse(this.Read(sectionName, keyName, defaultValue.ToString()), NumberStyles.Any, CultureInfo.InvariantCulture, out num))
            {
                num = defaultValue;
            }
            return num;
        }

        public char Read(string sectionName, string keyName, char defaultValue)
        {
            char ch;
            if (!char.TryParse(this.Read(sectionName, keyName, defaultValue.ToString()), out ch))
            {
                ch = defaultValue;
            }
            return ch;
        }

        public DateTime Read(string sectionName, string keyName, DateTime defaultValue)
        {
            return new DateTime(this.Read(sectionName, keyName, defaultValue.Ticks));
        }

        public decimal Read(string sectionName, string keyName, decimal defaultValue)
        {
            decimal num;
            if (!decimal.TryParse(this.Read(sectionName, keyName, defaultValue.ToString()), NumberStyles.Any, CultureInfo.InvariantCulture, out num))
            {
                num = defaultValue;
            }
            return num;
        }

        public double Read(string sectionName, string keyName, double defaultValue)
        {
            double num;
            if (!double.TryParse(this.Read(sectionName, keyName, defaultValue.ToString()), NumberStyles.Any, CultureInfo.InvariantCulture, out num))
            {
                num = defaultValue;
            }
            return num;
        }

        public Guid Read(string sectionName, string keyName, Guid defaultValue)
        {
            try
            {
                return new Guid(this.Read(sectionName, keyName, defaultValue.ToString()));
            }
            catch
            {
                return defaultValue;
            }
        }

        public short Read(string sectionName, string keyName, short defaultValue)
        {
            short num;
            if (!short.TryParse(this.Read(sectionName, keyName, defaultValue.ToString()), NumberStyles.Any, CultureInfo.InvariantCulture, out num))
            {
                num = defaultValue;
            }
            return num;
        }

        public int Read(string sectionName, string keyName, int defaultValue)
        {
            int num;
            if (!int.TryParse(this.Read(sectionName, keyName, defaultValue.ToString()), NumberStyles.Any, CultureInfo.InvariantCulture, out num))
            {
                num = defaultValue;
            }
            return num;
        }

        public long Read(string sectionName, string keyName, long defaultValue)
        {
            long num;
            if (!long.TryParse(this.Read(sectionName, keyName, defaultValue.ToString()), NumberStyles.Any, CultureInfo.InvariantCulture, out num))
            {
                num = defaultValue;
            }
            return num;
        }

        public float Read(string sectionName, string keyName, float defaultValue)
        {
            float num;
            if (!float.TryParse(this.Read(sectionName, keyName, defaultValue.ToString()), NumberStyles.Any, CultureInfo.InvariantCulture, out num))
            {
                num = defaultValue;
            }
            return num;
        }

        public string Read(string sectionName, string keyName, string defaultValue)
        {
            if (sectionName == null)
            {
                throw new ArgumentNullException("sectionName");
            }
            if (sectionName.Length == 0)
            {
                throw new ArgumentOutOfRangeException("sectionName");
            }
            if (keyName == null)
            {
                throw new ArgumentNullException("keyName");
            }
            if (keyName.Length == 0)
            {
                throw new ArgumentOutOfRangeException("keyName");
            }
            if (!this.Sections.Contains(sectionName))
            {
                return defaultValue;
            }
            IniFileSection section = this[sectionName];
            return (!section.Items.Contains(keyName) ? defaultValue : (section = this[sectionName])[keyName].Value);
        }

        public ushort Read(string sectionName, string keyName, ushort defaultValue)
        {
            ushort num;
            if (!ushort.TryParse(this.Read(sectionName, keyName, defaultValue.ToString()), NumberStyles.Any, CultureInfo.InvariantCulture, out num))
            {
                num = defaultValue;
            }
            return num;
        }

        public uint Read(string sectionName, string keyName, uint defaultValue)
        {
            uint num;
            if (!uint.TryParse(this.Read(sectionName, keyName, defaultValue.ToString()), NumberStyles.Any, CultureInfo.InvariantCulture, out num))
            {
                num = defaultValue;
            }
            return num;
        }

        public ulong Read(string sectionName, string keyName, ulong defaultValue)
        {
            ulong num;
            if (!ulong.TryParse(this.Read(sectionName, keyName, defaultValue.ToString()), NumberStyles.Any, CultureInfo.InvariantCulture, out num))
            {
                num = defaultValue;
            }
            return num;
        }

        public List<string> Read(string sectionName, string keyName, string[] separator, StringSplitOptions options)
        {
            if ((separator == null) || (separator.Length == 0))
            {
                separator = new string[] { ", " };
            }
            return this.Read(sectionName, keyName, string.Empty).Trim().Split(separator, options).ToList<string>();
        }

        public List<string> Read(string sectionName, string keyName, string separator, StringSplitOptions options)
        {
            return this.Read(sectionName, keyName, new string[] { separator }, options);
        }

        public List<T> Read<T>(string sectionName, string keyName, string separator, StringSplitOptions options, Converter<string, T> converter)
        {
            return this.Read<T>(sectionName, keyName, new string[] { separator }, options, converter);
        }

        public List<T> Read<T>(string sectionName, string keyName, string[] separator, StringSplitOptions options, Converter<string, T> converter)
        {
            return this.Read(sectionName, keyName, separator, options).ConvertAll<T>(converter);
        }

        public void Refresh()
        {
            string[] strArray = File.ReadAllLines(this.FileName, this.Encoding);
            if (strArray.Length != 0)
            {
                List<string> collection = new List<string>();
                IniFileSection none = IniFileSection.None;
                foreach (string str in strArray)
                {
                    string str2 = str.Trim();
                    if (string.IsNullOrEmpty(str2))
                    {
                        if ((collection.Count > 0) && (this.Sections.Count == 0))
                        {
                            this.Comment.AddRange(collection);
                            collection.Clear();
                        }
                    }
                    else if (str2.StartsWith("="))
                    {
                        collection.Clear();
                    }
                    else
                    {
                        if (str2.StartsWith(";") || str2.StartsWith("#"))
                        {
                            collection.Add(str2.Remove(0, 1).Trim());
                            if (collection.Count > 0)
                            {
                                goto Label_028C;
                            }
                        }
                        if (str2.StartsWith("[") && str2.EndsWith("]"))
                        {
                            if (none != IniFileSection.None)
                            {
                                this.Sections.Add(none);
                            }
                            none = new IniFileSection(str2.Trim(new char[] { '[', ']' }));
                            if (collection.Count > 0)
                            {
                                none.Comment.AddRange(collection);
                                collection.Clear();
                            }
                        }
                        else
                        {
                            IniFileSectionItem item2;
                            int index = str2.IndexOf('=');
                            if (index == -1)
                            {
                                if (none != IniFileSection.None)
                                {
                                    IniFileSectionItem item = new IniFileSectionItem {
                                        Key = str2,
                                        Value = string.Empty,
                                        Comment = new List<string>()
                                    };
                                    item2 = item;
                                    if (collection.Count > 0)
                                    {
                                        item2.Comment.AddRange(collection);
                                        collection.Clear();
                                    }
                                    none.Items.Add(item2);
                                }
                            }
                            else if (none != IniFileSection.None)
                            {
                                string str3 = str2.Substring(0, index).Trim();
                                string str4 = str2.Substring(index + 1).Trim();
                                IniFileSectionItem item3 = new IniFileSectionItem {
                                    Key = str3,
                                    Value = str4,
                                    Comment = new List<string>()
                                };
                                item2 = item3;
                                if (collection.Count > 0)
                                {
                                    item2.Comment.AddRange(collection);
                                    collection.Clear();
                                }
                                none.Items.Add(item2);
                            }
                        }
                    Label_028C:;
                    }
                }
                if (none != IniFileSection.None)
                {
                    if (collection.Count > 0)
                    {
                        none.Comment.AddRange(collection);
                        collection.Clear();
                    }
                    this.Sections.Add(none);
                }
            }
        }

        public void Save()
        {
            this.Save(this.FileName);
        }

        public void Save(string fileName)
        {
            this.Save(fileName, this.Encoding);
        }

        public void Save(string fileName, System.Text.Encoding encoding)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("fileName");
            }
            if (fileName.Length == 0)
            {
                throw new ArgumentOutOfRangeException("fileName");
            }
            StringBuilder builder = new StringBuilder();
            foreach (string str in this.Comment)
            {
                string str2 = str.Trim();
                if ((builder.Length != 0) || !string.IsNullOrEmpty(str2))
                {
                    builder.AppendFormat("; {0}{1}", str2, Environment.NewLine);
                }
            }
            if (this.Comment.Count > 0)
            {
                builder.AppendLine();
            }
            foreach (IniFileSection section in this.Sections)
            {
                builder.AppendLine(section.ToString());
            }
            File.WriteAllText(fileName, builder.ToString().Trim(), encoding);
            if (this.eventHandler_0 != null)
            {
                this.eventHandler_0(this, EventArgs.Empty);
            }
        }

        private static bool smethod_0(string string_1)
        {
            if (string.IsNullOrEmpty(string_1))
            {
                return false;
            }
            return ((string.Compare(string_1, "true", true) == 0) || ((string.Compare(string_1, "yes", true) == 0) || (string_1 == "1")));
        }

        [CompilerGenerated]
        private static void smethod_1(IniFile iniFile_0)
        {
            iniFile_0.Save();
        }

        public void UpdateWrite(string sectionName, string keyName, string value)
        {
            if (sectionName == null)
            {
                throw new ArgumentNullException("sectionName");
            }
            if (sectionName.Length == 0)
            {
                throw new ArgumentOutOfRangeException("sectionName");
            }
            if (keyName == null)
            {
                throw new ArgumentNullException("keyName");
            }
            if (keyName.Length == 0)
            {
                throw new ArgumentOutOfRangeException("keyName");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (value.Length == 0)
            {
                throw new ArgumentOutOfRangeException("value");
            }
            if (this.Sections.Contains(sectionName))
            {
                IniFileSection section = this[sectionName];
                if (section.Items.Contains(keyName))
                {
                    section = this[sectionName];
                    IniFileSectionItem item = section[keyName];
                    item.Value = value;
                    section = this[sectionName];
                    section.Items[keyName] = item;
                }
            }
        }

        public void Write(string sectionName, string keyName, bool value)
        {
            this.Write(sectionName, keyName, value.ToString(CultureInfo.InvariantCulture));
        }

        public void Write(string sectionName, string keyName, byte value)
        {
            this.Write(sectionName, keyName, value.ToString(CultureInfo.InvariantCulture));
        }

        public void Write(string sectionName, string keyName, char value)
        {
            this.Write(sectionName, keyName, value.ToString(CultureInfo.InvariantCulture));
        }

        public void Write(string sectionName, string keyName, DateTime value)
        {
            this.Write(sectionName, keyName, value.Ticks);
        }

        public void Write(string sectionName, string keyName, decimal value)
        {
            this.Write(sectionName, keyName, value.ToString(CultureInfo.InvariantCulture));
        }

        public void Write(string sectionName, string keyName, double value)
        {
            this.Write(sectionName, keyName, value.ToString(CultureInfo.InvariantCulture));
        }

        public void Write(string sectionName, string keyName, Guid value)
        {
            this.Write(sectionName, keyName, value.ToString());
        }

        public void Write(string sectionName, string keyName, short value)
        {
            this.Write(sectionName, keyName, value.ToString(CultureInfo.InvariantCulture));
        }

        public void Write(string sectionName, string keyName, int value)
        {
            this.Write(sectionName, keyName, value.ToString(CultureInfo.InvariantCulture));
        }

        public void Write(string sectionName, string keyName, long value)
        {
            this.Write(sectionName, keyName, value.ToString(CultureInfo.InvariantCulture));
        }

        public void Write(string sectionName, string keyName, float value)
        {
            this.Write(sectionName, keyName, value.ToString(CultureInfo.InvariantCulture));
        }

        public void Write(string sectionName, string keyName, string value)
        {
            if (sectionName == null)
            {
                throw new ArgumentNullException("sectionName");
            }
            if (sectionName.Length == 0)
            {
                throw new ArgumentOutOfRangeException("sectionName");
            }
            if (keyName == null)
            {
                throw new ArgumentNullException("keyName");
            }
            if (keyName.Length == 0)
            {
                throw new ArgumentOutOfRangeException("keyName");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (value.Length == 0)
            {
                throw new ArgumentOutOfRangeException("value");
            }
            if (this.Sections.Contains(sectionName))
            {
                IniFileSection section = this[sectionName];
                if (section.Items.Contains(keyName))
                {
                    section = this[sectionName];
                    IniFileSectionItem item = section[keyName];
                    item.Value = value;
                    section = this[sectionName];
                    section.Items[keyName] = item;
                }
                else
                {
                    section = this[sectionName];
                    IniFileSectionItem item2 = new IniFileSectionItem {
                        Key = keyName,
                        Value = value,
                        Comment = new List<string>()
                    };
                    section.Items.Add(item2);
                }
            }
            else
            {
                IniFileSection section2 = new IniFileSection(sectionName);
                IniFileSectionItem item3 = new IniFileSectionItem {
                    Key = keyName,
                    Value = value,
                    Comment = new List<string>()
                };
                section2.Items.Add(item3);
                this.Sections.Add(section2);
            }
        }

        public void Write(string sectionName, string keyName, ushort value)
        {
            this.Write(sectionName, keyName, value.ToString(CultureInfo.InvariantCulture));
        }

        public void Write(string sectionName, string keyName, uint value)
        {
            this.Write(sectionName, keyName, value.ToString(CultureInfo.InvariantCulture));
        }

        public void Write(string sectionName, string keyName, ulong value)
        {
            this.Write(sectionName, keyName, value.ToString(CultureInfo.InvariantCulture));
        }

        public void Write(string sectionName, string keyName, List<string> value, string separator, bool removeEmptyEntries)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (value.Count != 0)
            {
                StringBuilder builder = new StringBuilder();
                foreach (string str in value)
                {
                    if (!removeEmptyEntries || !string.IsNullOrEmpty(str))
                    {
                        if (builder.Length == 0)
                        {
                            builder.Append(str);
                        }
                        else
                        {
                            builder.Append(separator).Append(str);
                        }
                    }
                }
                this.Write(sectionName, keyName, builder.ToString());
            }
        }

        public void Write<T>(string sectionName, string keyName, List<T> value, string separator, bool removeEmptyEntries, Converter<T, string> converter)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (value.Count != 0)
            {
                if (converter == null)
                {
                    throw new ArgumentNullException("converter");
                }
                this.Write(sectionName, keyName, value.ConvertAll<string>(converter), separator, removeEmptyEntries);
            }
        }

        public List<string> Comment
        {
            [CompilerGenerated]
            get
            {
                return this.list_0;
            }
            [CompilerGenerated]
            set
            {
                this.list_0 = value;
            }
        }

        public System.Text.Encoding Encoding
        {
            [CompilerGenerated]
            get
            {
                return this.encoding_0;
            }
            [CompilerGenerated]
            private set
            {
                this.encoding_0 = value;
            }
        }

        public string FileName
        {
            [CompilerGenerated]
            get
            {
                return this.string_0;
            }
            [CompilerGenerated]
            private set
            {
                this.string_0 = value;
            }
        }

        public IniFileSection this[string sectionname]
        {
            get
            {
                return this.Sections[sectionname];
            }
            set
            {
                this.Sections[sectionname] = value;
            }
        }

        public IniFileSectionCollection Sections
        {
            [CompilerGenerated]
            get
            {
                return this.NkgSvdhaX;
            }
            [CompilerGenerated]
            private set
            {
                this.NkgSvdhaX = value;
            }
        }
    }
}

