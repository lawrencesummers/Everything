namespace WHC.OrderWater.Commons
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Text;

    public class FileUtil
    {
        public static void AppendText(string filePath, string content)
        {
            File.AppendAllText(filePath, content, Encoding.Default);
        }

        public static Stream BytesToStream(byte[] bytes)
        {
            return new MemoryStream(bytes);
        }

        public static void ClearFile(string filePath)
        {
            File.Delete(filePath);
            CreateFile(filePath);
        }

        public static bool CompareFilesHash(string fileName1, string fileName2)
        {
            bool flag;
            using (HashAlgorithm algorithm = HashAlgorithm.Create())
            {
                using (FileStream stream = new FileStream(fileName1, FileMode.Open))
                {
                    using (FileStream stream2 = new FileStream(fileName2, FileMode.Open))
                    {
                        byte[] buffer = algorithm.ComputeHash(stream);
                        byte[] buffer2 = algorithm.ComputeHash(stream2);
                        flag = BitConverter.ToString(buffer) == BitConverter.ToString(buffer2);
                    }
                }
            }
            return flag;
        }

        public static void Copy(string sourceFilePath, string destFilePath)
        {
            File.Copy(sourceFilePath, destFilePath, true);
        }

        public static void CreateFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath);
                }
            }
            catch (IOException exception)
            {
                throw exception;
            }
        }

        public static void CreateFile(string filePath, byte[] buffer)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    using (FileStream stream = File.Create(filePath))
                    {
                        stream.Write(buffer, 0, buffer.Length);
                    }
                }
            }
            catch (IOException exception)
            {
                throw exception;
            }
        }

        public static string CreateTempZeroByteFile()
        {
            return Path.GetTempFileName();
        }

        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public static bool FileIsExist(string path)
        {
            return File.Exists(path);
        }

        public static bool FileIsReadOnly(string fullpath)
        {
            FileInfo info = new FileInfo(fullpath);
            return ((info.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly);
        }

        public static byte[] FileToBytes(string filePath)
        {
            byte[] buffer2;
            int fileSize = GetFileSize(filePath);
            byte[] buffer = new byte[fileSize];
            FileStream stream = new FileInfo(filePath).Open(FileMode.Open);
            try
            {
                stream.Read(buffer, 0, fileSize);
                buffer2 = buffer;
            }
            catch (IOException exception)
            {
                throw exception;
            }
            finally
            {
                stream.Close();
            }
            return buffer2;
        }

        public static Stream FileToStream(string fileName)
        {
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            stream.Close();
            return new MemoryStream(buffer);
        }

        public static string FileToString(string filePath)
        {
            return FileToString(filePath, Encoding.Default);
        }

        public static string FileToString(string filePath, Encoding encoding)
        {
            string str;
            try
            {
                using (StreamReader reader = new StreamReader(filePath, encoding))
                {
                    str = reader.ReadToEnd();
                }
            }
            catch (IOException exception)
            {
                throw exception;
            }
            return str;
        }

        public static Encoding GetEncoding(string filePath)
        {
            return GetEncoding(filePath, Encoding.Default);
        }

        public static Encoding GetEncoding(string filePath, Encoding defaultEncoding)
        {
            Encoding bigEndianUnicode = defaultEncoding;
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4))
            {
                if ((stream == null) || (stream.Length < 2))
                {
                    return bigEndianUnicode;
                }
                long position = stream.Position;
                stream.Position = 0;
                int[] numArray = new int[] { stream.ReadByte(), stream.ReadByte(), stream.ReadByte(), stream.ReadByte() };
                stream.Position = position;
                if ((numArray[0] == 0xfe) && (numArray[1] == 0xff))
                {
                    bigEndianUnicode = Encoding.BigEndianUnicode;
                }
                if ((numArray[0] == 0xff) && (numArray[1] == 0xfe))
                {
                    bigEndianUnicode = Encoding.Unicode;
                }
                if (((numArray[0] == 0xef) && (numArray[1] == 0xbb)) && (numArray[2] == 0xbf))
                {
                    bigEndianUnicode = Encoding.UTF8;
                }
            }
            return bigEndianUnicode;
        }

        public static string GetExtension(string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            return info.Extension;
        }

        public static DateTime GetFileCreateTime(string fullpath)
        {
            FileInfo info = new FileInfo(fullpath);
            return info.CreationTime;
        }

        public static string GetFileName(string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            return info.Name;
        }

        public static string GetFileName(string fullpath, bool removeExt)
        {
            FileInfo info = new FileInfo(fullpath);
            string name = info.Name;
            if (removeExt)
            {
                name = name.Remove(name.IndexOf('.'));
            }
            return name;
        }

        public static string GetFileNameNoExtension(string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            return info.Name.Substring(0, info.Name.LastIndexOf('.'));
        }

        public static int GetFileSize(string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            return (int) info.Length;
        }

        public static double GetFileSizeKB(string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            return ConvertHelper.ToDouble<double>(Convert.ToDouble(info.Length) / 1024.0, 1.0);
        }

        public static double GetFileSizeMB(string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            return ConvertHelper.ToDouble<double>((Convert.ToDouble(info.Length) / 1024.0) / 1024.0, 1.0);
        }

        public static DateTime GetLastWriteTime(string fullpath)
        {
            FileInfo info = new FileInfo(fullpath);
            return info.LastWriteTime;
        }

        public static int GetLineCount(string filePath)
        {
            return File.ReadAllLines(filePath).Length;
        }

        public static string GetRandomFileName()
        {
            return Path.GetRandomFileName();
        }

        public static bool IsExistFile(string filePath)
        {
            return File.Exists(filePath);
        }

        public static object LoadObjectFromXml(string path, Type type)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                return XmlConvertor.XmlToObject(reader.ReadToEnd(), type);
            }
        }

        public static void Move(string sourceFilePath, string descDirectoryPath)
        {
            string fileName = GetFileName(sourceFilePath);
            if (Directory.Exists(descDirectoryPath))
            {
                if (IsExistFile(descDirectoryPath + @"\" + fileName))
                {
                    DeleteFile(descDirectoryPath + @"\" + fileName);
                }
                File.Move(sourceFilePath, descDirectoryPath + @"\" + fileName);
            }
        }

        public static string ReadFileFromEmbedded(string fileWholeName)
        {
            using (TextReader reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(fileWholeName)))
            {
                return reader.ReadToEnd();
            }
        }

        public static void SaveObjectToXml(string path, object obj)
        {
            string str = XmlConvertor.ObjectToXml(obj, true);
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(str);
            }
        }

        public static void SetFileReadonly(string fullpath, bool flag)
        {
            FileInfo info = new FileInfo(fullpath);
            if (flag)
            {
                info.Attributes |= FileAttributes.ReadOnly;
            }
            else
            {
                info.Attributes &= ~FileAttributes.ReadOnly;
            }
        }

        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] buffer2;
            try
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
                buffer2 = buffer;
            }
            catch (IOException exception)
            {
                throw exception;
            }
            finally
            {
                stream.Close();
            }
            return buffer2;
        }

        public static void StreamToFile(Stream stream, string fileName)
        {
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            stream.Seek(0, SeekOrigin.Begin);
            FileStream output = new FileStream(fileName, FileMode.Create);
            BinaryWriter writer = new BinaryWriter(output);
            writer.Write(buffer);
            writer.Close();
            output.Close();
        }

        public static void WriteText(string filePath, string content)
        {
            File.WriteAllText(filePath, content, Encoding.Default);
        }
    }
}

