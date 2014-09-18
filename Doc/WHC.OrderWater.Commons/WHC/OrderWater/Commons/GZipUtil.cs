namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;

    public class GZipUtil
    {
        public static string Compress(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            MemoryStream stream = new MemoryStream();
            using (GZipStream stream2 = new GZipStream(stream, CompressionMode.Compress, true))
            {
                stream2.Write(bytes, 0, bytes.Length);
            }
            stream.Position = 0;
            byte[] buffer = stream.ToArray();
            stream.Read(buffer, 0, buffer.Length);
            byte[] dst = new byte[buffer.Length + 4];
            Buffer.BlockCopy(buffer, 0, dst, 4, buffer.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(bytes.Length), 0, dst, 0, 4);
            return Convert.ToBase64String(dst);
        }

        public static byte[] Compress(byte[] bytData)
        {
            using (MemoryStream stream = GZip<MemoryStream>(new MemoryStream(bytData), CompressionMode.Compress))
            {
                return stream.ToArray();
            }
        }

        public static GZipResult Compress(string lpSourceFolder, string lpDestFolder, string zipFileName)
        {
            return Compress(lpSourceFolder, "*.*", SearchOption.AllDirectories, lpDestFolder, zipFileName, true);
        }

        public static GZipResult Compress(FileInfo[] files, string lpBaseFolder, string lpDestFolder, string zipFileName)
        {
            return Compress(files, lpBaseFolder, lpDestFolder, zipFileName, true);
        }

        public static GZipResult Compress(FileInfo[] files, string[] folders, string lpBaseFolder, string lpDestFolder, string zipFileName)
        {
            List<FileInfo> list = new List<FileInfo>();
            foreach (FileInfo info in files)
            {
                list.Add(info);
            }
            foreach (string str in folders)
            {
                DirectoryInfo info3 = new DirectoryInfo(str);
                foreach (FileInfo info2 in info3.GetFiles("*.*", SearchOption.AllDirectories))
                {
                    list.Add(info2);
                }
            }
            return Compress(list.ToArray(), lpBaseFolder, lpDestFolder, zipFileName, true);
        }

        public static GZipResult Compress(FileInfo[] files, string lpBaseFolder, string lpDestFolder, string zipFileName, bool deleteTempFile)
        {
            GZipResult result = new GZipResult();
            try
            {
                if (!lpDestFolder.EndsWith(@"\"))
                {
                    lpDestFolder = lpDestFolder + @"\";
                }
                string str = lpDestFolder + zipFileName + ".tmp";
                string str2 = lpDestFolder + zipFileName;
                result.TempFile = str;
                result.ZipFile = str2;
                if ((files == null) || (files.Length <= 0))
                {
                    return result;
                }
                smethod_3(files, lpBaseFolder, str, result);
                if (result.FileCount > 0)
                {
                    smethod_2(str, str2, result);
                }
                if (deleteTempFile)
                {
                    File.Delete(str);
                    result.TempFileDeleted = true;
                }
            }
            catch
            {
                result.Errors = true;
            }
            return result;
        }

        public static GZipResult Compress(string lpSourceFolder, string searchPattern, SearchOption searchOption, string lpDestFolder, string zipFileName, bool deleteTempFile)
        {
            DirectoryInfo info = new DirectoryInfo(lpSourceFolder);
            return Compress(info.GetFiles("*.*", searchOption), lpSourceFolder, lpDestFolder, zipFileName, deleteTempFile);
        }

        public static string Decompress(string compressedText)
        {
            byte[] buffer = Convert.FromBase64String(compressedText);
            MemoryStream stream = new MemoryStream();
            int num = BitConverter.ToInt32(buffer, 0);
            stream.Write(buffer, 4, buffer.Length - 4);
            byte[] buffer2 = new byte[num];
            stream.Position = 0;
            new GZipStream(stream, CompressionMode.Decompress).Read(buffer2, 0, buffer2.Length);
            return Encoding.UTF8.GetString(buffer2);
        }

        public static byte[] Decompress(byte[] bytData)
        {
            using (MemoryStream stream = GZip<MemoryStream>(new MemoryStream(bytData), CompressionMode.Decompress))
            {
                return stream.ToArray();
            }
        }

        public static GZipResult Decompress(string lpSourceFolder, string lpDestFolder, string zipFileName)
        {
            return Decompress(lpSourceFolder, lpDestFolder, zipFileName, true, true, null, null, 0x1000);
        }

        public static GZipResult Decompress(string lpSourceFolder, string lpDestFolder, string zipFileName, bool writeFiles, string addExtension)
        {
            return Decompress(lpSourceFolder, lpDestFolder, zipFileName, true, writeFiles, addExtension, null, 0x1000);
        }

        public static GZipResult Decompress(string lpSrcFolder, string lpDestFolder, string zipFileName, bool deleteTempFile, bool writeFiles, string addExtension, Hashtable htFiles, int bufferSize)
        {
            // This item is obfuscated and can not be translated.
            GZipResult result = new GZipResult();
            if (!lpSrcFolder.EndsWith(@"\"))
            {
                lpSrcFolder = lpSrcFolder + @"\";
            }
            if (!lpDestFolder.EndsWith(@"\"))
            {
                lpDestFolder = lpDestFolder + @"\";
            }
            string str = lpSrcFolder + zipFileName + ".tmp";
            string str2 = lpSrcFolder + zipFileName;
            result.TempFile = str;
            result.ZipFile = str2;
            string str3 = null;
            string str4 = null;
            string path = null;
            GZipFileInfo info = null;
            FileStream stream = null;
            ArrayList list = new ArrayList();
            bool flag = false;
            if (string.IsNullOrEmpty(addExtension))
            {
                addExtension = string.Empty;
            }
            else if (!addExtension.StartsWith("."))
            {
                addExtension = "." + addExtension;
            }
            try
            {
                stream = smethod_7(str2, str, result);
                if (stream != null)
                {
                    while (stream.Position != stream.Length)
                    {
                        str3 = null;
                        while (!string.IsNullOrEmpty(str3))
                        {
                            if (0 == 0)
                            {
                                goto Label_00EF;
                            }
                            str3 = smethod_4(stream);
                        }
                        goto Label_00C4;
                    Label_00EF:
                        if (!string.IsNullOrEmpty(str3))
                        {
                            info = new GZipFileInfo();
                            if (!info.ParseFileInfo(str3) || (info.Length <= 0))
                            {
                                continue;
                            }
                            list.Add(info);
                            str4 = lpDestFolder + info.RelativePath;
                            path = smethod_6(str4);
                            info.LocalPath = str4;
                            flag = false;
                            if ((htFiles == null) || htFiles.ContainsKey(info.RelativePath))
                            {
                                info.RestoreRequested = true;
                                flag = writeFiles;
                            }
                            if (flag)
                            {
                                if (!Directory.Exists(path))
                                {
                                    Directory.CreateDirectory(path);
                                }
                                info.Restored = smethod_5(stream, info.Length, str4 + addExtension, bufferSize);
                                continue;
                            }
                            stream.Position += info.Length;
                        }
                    }
                }
            }
            catch
            {
                result.Errors = true;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream = null;
                }
            }
            try
            {
                if (deleteTempFile)
                {
                    File.Delete(str);
                    result.TempFileDeleted = true;
                }
            }
            catch
            {
                result.Errors = true;
            }
            result.FileCount = list.Count;
            result.Files = new GZipFileInfo[list.Count];
            list.CopyTo(result.Files);
            return result;
        }

        public static T GZip<T>(Stream stream, CompressionMode mode) where T: Stream
        {
            byte[] buffer = new byte[0x1000];
            T local = default(T);
            using (Stream stream2 = new GZipStream(stream, mode))
            {
                int num;
                goto Label_002E;
            Label_001E:
                local.Write(buffer, 0, num);
            Label_002E:
                Array.Clear(buffer, 0, buffer.Length);
                num = stream2.Read(buffer, 0, buffer.Length);
                if (num > 0)
                {
                    goto Label_001E;
                }
                return local;
            }
        }

        public static object GZipToObject(byte[] byteArray)
        {
            return smethod_1(Decompress(byteArray));
        }

        public static byte[] ObjectToGZip(object obj)
        {
            return Compress(smethod_0(obj));
        }

        private static byte[] smethod_0(object object_0)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, object_0);
                return stream.ToArray();
            }
        }

        private static object smethod_1(byte[] byte_0)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream(byte_0))
            {
                return formatter.Deserialize(stream);
            }
        }

        private static void smethod_2(string string_0, string string_1, GZipResult gzipResult_0)
        {
            FileStream stream = null;
            FileStream stream2 = null;
            GZipStream stream3 = null;
            try
            {
                stream = new FileStream(string_1, FileMode.Create, FileAccess.Write, FileShare.None);
                stream3 = new GZipStream(stream, CompressionMode.Compress, true);
                stream2 = new FileStream(string_0, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] buffer = new byte[stream2.Length];
                stream2.Read(buffer, 0, buffer.Length);
                stream2.Close();
                stream2 = null;
                stream3.Write(buffer, 0, buffer.Length);
                gzipResult_0.ZipFileSize = stream.Length;
                gzipResult_0.CompressionPercent = smethod_8(gzipResult_0.TempFileSize, gzipResult_0.ZipFileSize);
            }
            catch
            {
                gzipResult_0.Errors = true;
            }
            finally
            {
                if (stream3 != null)
                {
                    stream3.Close();
                    stream3 = null;
                }
                if (stream != null)
                {
                    stream.Close();
                    stream = null;
                }
                if (stream2 != null)
                {
                    stream2.Close();
                    stream2 = null;
                }
            }
        }

        private static void smethod_3(FileInfo[] fileInfo_0, string string_0, string string_1, object object_0)
        {
            string s = null;
            string str2 = null;
            int index = 0;
            string path = null;
            string str4 = null;
            GZipFileInfo info = null;
            FileStream stream = null;
            FileStream stream2 = null;
            if ((fileInfo_0 != null) && (fileInfo_0.Length > 0))
            {
                try
                {
                    object_0.Files = new GZipFileInfo[fileInfo_0.Length];
                    stream = new FileStream(string_1, FileMode.Create, FileAccess.Write, FileShare.None);
                    foreach (FileInfo info2 in fileInfo_0)
                    {
                        string text1 = info2.DirectoryName + @"\";
                        try
                        {
                            info = new GZipFileInfo {
                                Index = index
                            };
                            path = info2.FullName;
                            info.LocalPath = path;
                            str4 = path.Replace(string_0, string.Empty).Replace(@"\", "/");
                            info.RelativePath = str4;
                            stream2 = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                            byte[] buffer = new byte[stream2.Length];
                            stream2.Read(buffer, 0, buffer.Length);
                            stream2.Close();
                            stream2 = null;
                            str2 = info2.LastWriteTimeUtc.ToString();
                            info.ModifiedDate = info2.LastWriteTimeUtc;
                            info.Length = buffer.Length;
                            string[] strArray = new string[] { index.ToString(), ",", str4, ",", str2, ",", buffer.Length.ToString(), "\n" };
                            s = string.Concat(strArray);
                            byte[] bytes = Encoding.Default.GetBytes(s);
                            stream.Write(bytes, 0, bytes.Length);
                            stream.Write(buffer, 0, buffer.Length);
                            stream.WriteByte(10);
                            info.AddedToTempFile = true;
                            object_0.Files[index] = info;
                            index++;
                        }
                        catch
                        {
                            object_0.Errors = true;
                        }
                        finally
                        {
                            if (stream2 != null)
                            {
                                stream2.Close();
                                stream2 = null;
                            }
                        }
                        if (stream != null)
                        {
                            object_0.TempFileSize = stream.Length;
                        }
                    }
                }
                catch
                {
                    object_0.Errors = true;
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                        stream = null;
                    }
                }
            }
            object_0.FileCount = index;
        }

        private static string smethod_4(Stream stream_0)
        {
            byte[] bytes = new byte[0x1000];
            byte num = 0;
            byte num2 = 10;
            int index = 0;
            while (num != num2)
            {
                bytes[index] = (byte) stream_0.ReadByte();
                index++;
            }
            return Encoding.Default.GetString(bytes, 0, index - 1);
        }

        private static bool smethod_5(Stream stream_0, int int_0, string string_0, int int_1)
        {
            bool flag = false;
            FileStream stream = null;
            if ((int_1 == 0) || (int_0 < int_1))
            {
                int_1 = int_0;
            }
            int count = 0;
            int num2 = int_0;
            int num3 = 0;
            try
            {
                byte[] buffer = new byte[int_1];
                stream = new FileStream(string_0, FileMode.Create, FileAccess.Write, FileShare.None);
                while (num2 > 0)
                {
                    if (num2 > int_1)
                    {
                        num3 = int_1;
                    }
                    else
                    {
                        num3 = num2;
                    }
                    count = stream_0.Read(buffer, 0, num3);
                    num2 -= count;
                    if (count == 0)
                    {
                        break;
                    }
                    stream.Write(buffer, 0, count);
                    stream.Flush();
                }
                stream.Flush();
                stream.Close();
                stream = null;
                flag = true;
            }
            catch
            {
                flag = false;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Flush();
                    stream.Close();
                    stream = null;
                }
            }
            return flag;
        }

        private static string smethod_6(string string_0)
        {
            string str = string_0;
            int num = str.LastIndexOf(@"\");
            if (num != -1)
            {
                str = str.Substring(0, num + 1);
            }
            return str;
        }

        private static FileStream smethod_7(string string_0, string string_1, GZipResult gzipResult_0)
        {
            FileStream stream = null;
            GZipStream stream2 = null;
            FileStream stream3 = null;
            FileStream stream4 = null;
            byte[] buffer = new byte[0x1000];
            int count = 0;
            try
            {
                stream = new FileStream(string_0, FileMode.Open, FileAccess.Read, FileShare.Read);
                gzipResult_0.ZipFileSize = stream.Length;
                stream3 = new FileStream(string_1, FileMode.Create, FileAccess.Write, FileShare.None);
                stream2 = new GZipStream(stream, CompressionMode.Decompress, true);
                goto Label_005C;
            Label_0042:
                if (count == 0x1000)
                {
                    goto Label_005C;
                }
                goto Label_00AE;
            Label_004F:
                stream3.Write(buffer, 0, count);
                goto Label_0042;
            Label_005C:
                count = stream2.Read(buffer, 0, 0x1000);
                if (count == 0)
                {
                    goto Label_0042;
                }
                goto Label_004F;
            }
            catch
            {
                gzipResult_0.Errors = true;
            }
            finally
            {
                if (stream2 != null)
                {
                    stream2.Close();
                    stream2 = null;
                }
                if (stream3 != null)
                {
                    stream3.Close();
                    stream3 = null;
                }
                if (stream != null)
                {
                    stream.Close();
                    stream = null;
                }
            }
        Label_00AE:
            stream4 = new FileStream(string_1, FileMode.Open, FileAccess.Read, FileShare.None);
            if (stream4 != null)
            {
                gzipResult_0.TempFileSize = stream4.Length;
            }
            return stream4;
        }

        private static int smethod_8(long long_0, long long_1)
        {
            double num = long_0;
            double num2 = long_1;
            double num3 = 100.0;
            double num4 = (num - num2) / num;
            double num5 = num4 * num3;
            return (int) num5;
        }
    }
}

