namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections;
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    public sealed class IsolatedStorageHelper
    {
        public static void CreateDirectory(IsolatedStorageFile storage, string dirName)
        {
            try
            {
                if (!(string.IsNullOrEmpty(dirName) || (storage.GetDirectoryNames(dirName).Length <= 0)))
                {
                    storage.CreateDirectory(dirName);
                }
            }
            catch (Exception exception)
            {
                throw new Exception("无法在存储区内创建目录.", exception);
            }
        }

        public static void Delete(string fileName, IsolatedStorageScope scope)
        {
            try
            {
                using (IsolatedStorageFile file = IsolatedStorageFile.GetStore(scope, (Type) null, (Type) null))
                {
                    if (!(string.IsNullOrEmpty(fileName) || (file.GetFileNames(fileName).Length <= 0)))
                    {
                        file.DeleteFile(fileName);
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("无法在存储区内删除文件.", exception);
            }
        }

        public static void DeleteDirectory(IsolatedStorageFile storage, string dirName)
        {
            try
            {
                if (!(string.IsNullOrEmpty(dirName) || (storage.GetDirectoryNames(dirName).Length <= 0)))
                {
                    storage.DeleteDirectory(dirName);
                }
            }
            catch (Exception exception)
            {
                throw new Exception("无法在存储区内删除目录.", exception);
            }
        }

        public static string GetDataTime()
        {
            string str;
            IsolatedStorageFile isf = IsolatedStorageFile.GetStore(IsolatedStorageScope.Assembly | IsolatedStorageScope.Domain | IsolatedStorageScope.User, (Type) null, (Type) null);
            if (isf.GetDirectoryNames(UIConstants.IsolatedStorageDirectoryName).Length == 0)
            {
                return string.Empty;
            }
            if (isf.GetFileNames(UIConstants.IsolatedStorage).Length == 0)
            {
                return string.Empty;
            }
            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(UIConstants.IsolatedStorage, FileMode.OpenOrCreate, isf))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    str = reader.ReadLine();
                }
            }
            if (!string.IsNullOrEmpty(str))
            {
                try
                {
                    str = EncodeHelper.DesDecrypt(str, UIConstants.IsolatedStorageEncryptKey);
                }
                catch
                {
                }
            }
            return str;
        }

        public static object Load(string key)
        {
            object obj2;
            try
            {
                using (IsolatedStorageFile file = IsolatedStorageFile.GetStore(IsolatedStorageScope.Assembly | IsolatedStorageScope.Domain | IsolatedStorageScope.User, (Type) null, (Type) null))
                {
                    using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(key, FileMode.Open, FileAccess.Read, file))
                    {
                        stream.Position = 0;
                        obj2 = new BinaryFormatter().Deserialize(stream);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                obj2 = null;
            }
            catch (SerializationException)
            {
                obj2 = null;
            }
            return obj2;
        }

        public static void Load(IDictionary d, IsolatedStorageScope scope, string filename)
        {
            d.Clear();
            using (IsolatedStorageFile file = IsolatedStorageFile.GetStore(scope, (Type) null, (Type) null))
            {
                string[] fileNames = file.GetFileNames(filename);
                if ((fileNames.Length > 0) && (fileNames[0] == filename))
                {
                    using (Stream stream = new IsolatedStorageFileStream(filename, FileMode.OpenOrCreate, file))
                    {
                        IFormatter formatter = new BinaryFormatter();
                        IDictionaryEnumerator enumerator = ((IDictionary) formatter.Deserialize(stream)).GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            d.Add(enumerator.Key, enumerator.Value);
                        }
                    }
                }
            }
        }

        public static void LoadFromUserStoreForApplication(IDictionary d, string filename)
        {
            Load(d, IsolatedStorageScope.Application | IsolatedStorageScope.User, filename);
        }

        public static void LoadFromUserStoreForDomain(IDictionary d, string filename)
        {
            Load(d, IsolatedStorageScope.Assembly | IsolatedStorageScope.Domain | IsolatedStorageScope.User, filename);
        }

        public static void Save(object objectToSave, string key)
        {
            using (IsolatedStorageFile file = IsolatedStorageFile.GetStore(IsolatedStorageScope.Assembly | IsolatedStorageScope.Domain | IsolatedStorageScope.User, (Type) null, (Type) null))
            {
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(key, FileMode.Create, FileAccess.Write, file))
                {
                    new BinaryFormatter().Serialize(stream, objectToSave);
                }
            }
        }

        public static void Save(IDictionary d, IsolatedStorageScope scope, string filename)
        {
            IsolatedStorageFile isf = IsolatedStorageFile.GetStore(scope, (Type) null, (Type) null);
            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(filename, FileMode.Create, isf))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, d);
            }
        }

        public static void SaveDataTime()
        {
            SaveDataTime(DateTime.Now);
        }

        public static void SaveDataTime(DateTime fromDate)
        {
            IsolatedStorageFileStream stream2;
            StreamWriter writer;
            string strText = fromDate.ToString("MM-dd-yyyy HH:mm:ss");
            string str2 = GetDataTime().Trim();
            if (!string.IsNullOrEmpty(str2))
            {
                strText = str2 + ";" + strText;
            }
            strText = EncodeHelper.DesEncrypt(strText, UIConstants.IsolatedStorageEncryptKey);
            IsolatedStorageFile isf = IsolatedStorageFile.GetStore(IsolatedStorageScope.Assembly | IsolatedStorageScope.Domain | IsolatedStorageScope.User, (Type) null, (Type) null);
            string[] directoryNames = isf.GetDirectoryNames(UIConstants.IsolatedStorageDirectoryName);
            IsolatedStorageFileStream stream = null;
            if (directoryNames.Length == 0)
            {
                isf.CreateDirectory(UIConstants.IsolatedStorageDirectoryName);
                using (stream2 = stream = new IsolatedStorageFileStream(UIConstants.IsolatedStorage, FileMode.Create, isf))
                {
                    using (writer = new StreamWriter(stream))
                    {
                        writer.WriteLine(strText);
                    }
                    return;
                }
            }
            if (isf.GetFileNames(UIConstants.IsolatedStorage).Length == 0)
            {
                using (stream2 = stream = new IsolatedStorageFileStream(UIConstants.IsolatedStorage, FileMode.Create, isf))
                {
                    using (writer = new StreamWriter(stream))
                    {
                        writer.WriteLine(strText);
                    }
                    return;
                }
            }
            using (stream2 = stream = new IsolatedStorageFileStream(UIConstants.IsolatedStorage, FileMode.Open, isf))
            {
                using (writer = new StreamWriter(stream))
                {
                    writer.WriteLine(strText);
                }
            }
        }

        public static void SaveToUserStoreForApplication(IDictionary d, string filename)
        {
            Save(d, IsolatedStorageScope.Application | IsolatedStorageScope.User, filename);
        }

        public static void SaveToUserStoreForDomain(IDictionary d, string filename)
        {
            Save(d, IsolatedStorageScope.Assembly | IsolatedStorageScope.Domain | IsolatedStorageScope.User, filename);
        }
    }
}

