namespace RDIFramework.Utilities
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    public class FileHelper : IDisposable
    {
        private bool bool_0 = false;

        public static Image ByteToImage(byte[] buffer)
        {
            MemoryStream stream = new MemoryStream();
            stream = new MemoryStream(buffer);
            Image image = Image.FromStream(stream);
            stream.Close();
            return image;
        }

        public static void CopyDir(string srcPath, string aimPath)
        {
            try
            {
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                {
                    aimPath = aimPath + Path.DirectorySeparatorChar;
                }
                if (!Directory.Exists(aimPath))
                {
                    Directory.CreateDirectory(aimPath);
                }
                foreach (string str in Directory.GetFileSystemEntries(srcPath))
                {
                    if (Directory.Exists(str))
                    {
                        CopyDir(str, aimPath + Path.GetFileName(str));
                    }
                    else
                    {
                        File.Copy(str, aimPath + Path.GetFileName(str), true);
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
        }

        public static void CreateDirectory(string directoryName)
        {
            string path = Path.GetDirectoryName(directoryName);
            if (!Directory.Exists(path))
            {
                CreateDirectory(path);
            }
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
        }

        public static void DeleteFolder(string dir)
        {
            if (Directory.Exists(dir))
            {
                foreach (string str in Directory.GetFileSystemEntries(dir))
                {
                    if (File.Exists(str))
                    {
                        File.Delete(str);
                    }
                    else
                    {
                        DeleteFolder(str);
                    }
                }
                Directory.Delete(dir);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (!this.bool_0)
            {
                this.bool_0 = true;
            }
        }

        public static bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        public static void FileAdd(string Path, string strings)
        {
            StreamWriter writer = File.AppendText(Path);
            writer.Write(strings);
            writer.Flush();
            writer.Close();
        }

        public static void FileCoppy(string orignFile, string NewFile)
        {
            File.Copy(orignFile, NewFile, true);
        }

        public static void FileDel(string Path)
        {
            File.Delete(Path);
        }

        public static void FileMove(string orignFile, string NewFile)
        {
            File.Move(orignFile, NewFile);
        }

        ~FileHelper()
        {
            this.Dispose();
        }

        public static void FolderCreate(string orignFolder, string NewFloder)
        {
            Directory.SetCurrentDirectory(orignFolder);
            Directory.CreateDirectory(NewFloder);
        }

        public static string GetDirectoryName(string directory)
        {
            if ((directory.Length - 1) == directory.LastIndexOf('\\'))
            {
                return (directory.Substring(0, directory.LastIndexOf(":")) + "盘");
            }
            return directory.Substring(directory.LastIndexOf('\\') + 1);
        }

        public static byte[] GetFile(string fileName)
        {
            FileStream input = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(input);
            byte[] buffer = reader.ReadBytes((int) input.Length);
            reader.Close();
            input.Close();
            return buffer;
        }

        public static void GetFile(string fileAllName, out string filePath, out string fileName, out string fileType)
        {
            filePath = fileAllName.Substring(0, fileAllName.LastIndexOf('\\')) + @"\";
            fileName = fileAllName.Substring(fileAllName.LastIndexOf('\\') + 1, (fileAllName.LastIndexOf('.') - fileAllName.LastIndexOf('\\')) - 1);
            fileType = fileAllName.Substring(fileAllName.LastIndexOf('.'));
        }

        public static long GetFileSize(string fileName)
        {
            FileInfo info = new FileInfo(fileName);
            return info.Length;
        }

        public static string GetName(string fileAllName)
        {
            return fileAllName.Substring(fileAllName.LastIndexOf('\\') + 1, (fileAllName.LastIndexOf('.') - fileAllName.LastIndexOf('\\')) - 1);
        }

        public static void GetName(string fileAllName, out string fileName)
        {
            fileName = fileAllName.Substring(fileAllName.LastIndexOf('\\') + 1, (fileAllName.LastIndexOf('.') - fileAllName.LastIndexOf('\\')) - 1);
        }

        public static void GetNameAndType(string fileAllName, out string fileName, out string fileType)
        {
            fileName = fileAllName.Substring(fileAllName.LastIndexOf('\\') + 1, (fileAllName.LastIndexOf('.') - fileAllName.LastIndexOf('\\')) - 1);
            fileType = fileAllName.Substring(fileAllName.LastIndexOf('.'));
        }

        public static string GetPath(string fileAllName)
        {
            return (fileAllName.Substring(0, fileAllName.LastIndexOf('\\')) + @"\");
        }

        public static void GetPath(string fileAllName, out string filePath)
        {
            filePath = fileAllName.Substring(0, fileAllName.LastIndexOf('\\')) + @"\";
        }

        public static string GetPostfixStr(string filename)
        {
            int startIndex = filename.LastIndexOf(".");
            int length = filename.Length;
            return filename.Substring(startIndex, length - startIndex);
        }

        public static string GetType(string fileAllName)
        {
            return fileAllName.Substring(fileAllName.LastIndexOf('.'));
        }

        public static void GetType(string fileAllName, out string fileType)
        {
            fileType = fileAllName.Substring(fileAllName.LastIndexOf('.'));
        }

        public static byte[] ImageToByte(Image Image)
        {
            MemoryStream stream = new MemoryStream();
            Image.Save(stream, ImageFormat.Gif);
            byte[] buffer = stream.GetBuffer();
            stream.Close();
            return buffer;
        }

        public static string ReadFile(string Path)
        {
            if (!File.Exists(Path))
            {
                return "不存在相应的目录";
            }
            using (StreamReader reader = new StreamReader(Path, Encoding.GetEncoding("gb2312")))
            {
                return reader.ReadToEnd();
            }
        }

        public static void SaveFile(byte[] file, string fileName)
        {
            string directoryName = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            FileStream stream = new FileStream(fileName, FileMode.Create);
            stream.Write(file, 0, file.Length);
            stream.Close();
        }

        public static void WriteException(UserInfo userInfo, Exception ex)
        {
            string fileName = "Log.txt";
            if (userInfo.UserName.Length > 0)
            {
                fileName = "Log_" + DateTime.Now.ToString(SystemInfo.DateFormat) + userInfo.UserName + ".txt";
            }
            WriteException(ex, fileName);
        }

        public static void WriteException(Exception ex, string fileName)
        {
            if (SystemInfo.LogException)
            {
                string path = SystemInfo.StartupPath + @"\log\" + fileName;
                if (!Directory.Exists(SystemInfo.StartupPath + @"\log\"))
                {
                    Directory.CreateDirectory(SystemInfo.StartupPath + @"\log\");
                }
                if (!File.Exists(path))
                {
                    new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite).Close();
                }
                StreamWriter writer = new StreamWriter(path, true, Encoding.Default);
                writer.WriteLine(string.Concat(new object[] { DateTime.Now.ToString(SystemInfo.DateTimeFormat), " Message:", ex.Message, "\nInnerException:", ex.InnerException }));
                writer.Close();
            }
        }

        public static void WriteFile(string Path, string Strings)
        {
            if (!File.Exists(Path))
            {
                string path = Path.Substring(0, Path.LastIndexOf(@"\"));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                File.Create(Path).Close();
            }
            StreamWriter writer = new StreamWriter(Path, false, Encoding.GetEncoding("gb2312"));
            writer.Write(Strings);
            writer.Close();
            writer.Dispose();
        }
    }
}

