/*
 * Created by SharpDevelop.
 * User: guanxiang
 * Date: 2013/10/31
 * Time: 14:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;

namespace LawrenceUtils
{
	/// <summary>
	/// Description of FTPUtil.
	/// </summary>
	public class FTPUtil
	{
		 private bool bool_0;
        private FtpWebRequest ftpWebRequest;
        private FtpWebResponse ftpWebResponse;
        private string directoryPath;
        private string userName;
        private string errorMsg;
        private string password;
        private string remoteFile;
        private System.Uri uri;
        private WebProxy proxy;

        public event De_DownloadDataCompleted DownloadDataCompleted;

        public event De_DownloadProgressChanged DownloadProgressChanged;

        public event De_UploadFileCompleted UploadFileCompleted;

        public event De_UploadProgressChanged UploadProgressChanged;

        public FTPUtil()
        {
            this.ftpWebRequest = null;
            this.ftpWebResponse = null;
            this.proxy = null;
            this.bool_0 = false;
            this.remoteFile = "";
            this.userName = "anonymous";
            this.password = "@anonymous";
            this.uri = null;
            this.proxy = null;
        }

        public FTPUtil(System.Uri FtpUri, string strUserName, string strPassword)
        {
            this.ftpWebRequest = null;
            this.ftpWebResponse = null;
            this.proxy = null;
            this.bool_0 = false;
            this.remoteFile = "";
            this.uri = new System.Uri(FtpUri.GetLeftPart(UriPartial.Authority));
            this.directoryPath = FtpUri.AbsolutePath;
            if (!this.directoryPath.EndsWith("/"))
            {
                this.directoryPath = this.directoryPath + "/";
            }
            this.userName = strUserName;
            this.password = strPassword;
            this.proxy = null;
        }

        public FTPUtil(System.Uri FtpUri, string strUserName, string strPassword, WebProxy objProxy)
        {
            this.ftpWebRequest = null;
            this.ftpWebResponse = null;
            this.proxy = null;
            this.bool_0 = false;
            this.remoteFile = "";
            this.uri = new System.Uri(FtpUri.GetLeftPart(UriPartial.Authority));
            this.directoryPath = FtpUri.AbsolutePath;
            if (!this.directoryPath.EndsWith("/"))
            {
                this.directoryPath = this.directoryPath + "/";
            }
            this.userName = strUserName;
            this.password = strPassword;
            this.proxy = objProxy;
        }

        public bool ComeoutDirectory()
        {
            if (this.directoryPath == "/")
            {
                this.ErrorMsg = "当前目录已经是根目录！";
                throw new Exception("当前目录已经是根目录！");
            }
            char[] separator = new char[] { '/' };
            string[] strArray = this.directoryPath.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length == 1)
            {
                this.directoryPath = "/";
            }
            else
            {
                this.directoryPath = string.Join("/", strArray, 0, strArray.Length - 1);
            }
            return true;
        }

        public bool CopyFileToAnotherDirectory(string RemoteFile, string DirectoryName)
        {
            bool flag2;
            string directoryPath = this.DirectoryPath;
            try
            {
                byte[] fileBytes = this.DownloadFile(RemoteFile);
                this.GotoDirectory(DirectoryName);
                bool flag = this.UploadFile(fileBytes, RemoteFile, false);
                this.DirectoryPath = directoryPath;
                flag2 = flag;
            }
            catch (Exception exception)
            {
                this.DirectoryPath = directoryPath;
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return flag2;
        }

        public void DeleteFile(string RemoteFileName)
        {
            try
            {
                if (!this.IsValidFileChars(RemoteFileName))
                {
                    throw new Exception("文件名非法！");
                }
                this.ftpWebResponse = this.DoResponse(new System.Uri(this.Uri.ToString() + RemoteFileName), "DELE");
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
        }

        public bool DirectoryExist(string RemoteDirectoryName)
        {
            bool flag;
            try
            {
                if (!this.IsValidPathChars(RemoteDirectoryName))
                {
                    throw new Exception("目录名非法！");
                }
                foreach (FileStruct struct2 in this.ListDirectories())
                {
                    if (struct2.Name == RemoteDirectoryName)
                    {
                        goto Label_0052;
                    }
                }
                return false;
            Label_0052:
                flag = true;
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return flag;
        }

        public byte[] DownloadFile(string RemoteFileName)
        {
            byte[] buffer2;
            try
            {
                if (!this.IsValidFileChars(RemoteFileName))
                {
                    throw new Exception("非法文件名或目录名!");
                }
                this.ftpWebResponse = this.DoResponse(new System.Uri(this.Uri.ToString() + RemoteFileName), "RETR");
                Stream responseStream = this.ftpWebResponse.GetResponseStream();
                MemoryStream stream2 = new MemoryStream(0x7d000);
                byte[] buffer = new byte[0x400];
                int count = 0;
                int num2 = 0;
                goto Label_006E;
            Label_0065:
                stream2.Write(buffer, 0, count);
            Label_006E:
                count = responseStream.Read(buffer, 0, buffer.Length);
                num2 += count;
                if (count != 0)
                {
                    goto Label_0065;
                }
                if (stream2.Length > 0L)
                {
                    return stream2.ToArray();
                }
                buffer2 = null;
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return buffer2;
        }

        public bool DownloadFile(string RemoteFileName, string LocalPath)
        {
            return this.DownloadFile(RemoteFileName, LocalPath, RemoteFileName);
        }

        public bool DownloadFile(string RemoteFileName, string LocalPath, string LocalFileName)
        {
            byte[] buffer = null;
            bool flag;
            try
            {
                if (!((this.IsValidFileChars(RemoteFileName) && this.IsValidFileChars(LocalFileName)) && this.IsValidPathChars(LocalPath)))
                {
                    throw new Exception("非法文件名或目录名!");
                }
                if (!Directory.Exists(LocalPath))
                {
                    throw new Exception("本地文件路径不存在!");
                }
                string path = Path.Combine(LocalPath, LocalFileName);
                if (System.IO.File.Exists(path))
                {
                    throw new Exception("当前路径下已经存在同名文件！");
                }
                buffer = this.DownloadFile(RemoteFileName);
                if (buffer != null)
                {
                    FileStream stream = new FileStream(path, FileMode.Create);
                    stream.Write(buffer, 0, buffer.Length);
                    stream.Flush();
                    stream.Close();
                    return true;
                }
                flag = false;
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return flag;
        }

        public void DownloadFileAsync(string RemoteFileName, string LocalFullPath)
        {
            try
            {
                if (!this.IsValidFileChars(RemoteFileName))
                {
                    throw new Exception("非法文件名或目录名!");
                }
                if (System.IO.File.Exists(LocalFullPath))
                {
                    throw new Exception("当前路径下已经存在同名文件！");
                }
                FTPWebClient class2 = new FTPWebClient();
                class2.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.method_3);
                class2.DownloadFileCompleted += new AsyncCompletedEventHandler(this.method_2);
                class2.Credentials = new NetworkCredential(this.UserName, this.Password);
                if (this.Proxy != null)
                {
                    class2.Proxy = this.Proxy;
                }
                class2.DownloadFileAsync(new System.Uri(this.Uri.ToString() + RemoteFileName), LocalFullPath);
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
        }

        public void DownloadFileAsync(string RemoteFileName, string LocalPath, string LocalFileName)
        {
            try
            {
                if (!((this.IsValidFileChars(RemoteFileName) && this.IsValidFileChars(LocalFileName)) && this.IsValidPathChars(LocalPath)))
                {
                    throw new Exception("非法文件名或目录名!");
                }
                if (!Directory.Exists(LocalPath))
                {
                    throw new Exception("本地文件路径不存在!");
                }
                string path = Path.Combine(LocalPath, LocalFileName);
                if (System.IO.File.Exists(path))
                {
                    throw new Exception("当前路径下已经存在同名文件！");
                }
                this.DownloadFileAsync(RemoteFileName, path);
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
        }

        public bool FileExist(string RemoteFileName)
        {
            bool flag;
            try
            {
                if (!this.IsValidFileChars(RemoteFileName))
                {
                    throw new Exception("文件名非法！");
                }
                foreach (FileStruct struct2 in this.ListFiles())
                {
                    if (struct2.Name == RemoteFileName)
                    {
                        goto Label_0052;
                    }
                }
                return false;
            Label_0052:
                flag = true;
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return flag;
        }

        ~FTPUtil()
        {
            if (this.ftpWebResponse != null)
            {
                this.ftpWebResponse.Close();
                this.ftpWebResponse = null;
            }
            if (this.ftpWebRequest != null)
            {
                this.ftpWebRequest.Abort();
                this.ftpWebRequest = null;
            }
        }

        public bool GotoDirectory(string DirectoryName)
        {
            bool flag;
            string directoryPath = this.DirectoryPath;
            try
            {
                DirectoryName = DirectoryName.Replace(@"\", "/");
                string[] array = DirectoryName.Split(new char[] { '/' });
                if (array[0] == ".")
                {
                    this.DirectoryPath = "/";
                    if (array.Length == 1)
                    {
                        return true;
                    }
                    Array.Clear(array, 0, 1);
                }
                bool flag2 = false;
                foreach (string str2 in array)
                {
                    if ((str2 != null) && !(flag2 = this.method_11(str2)))
                    {
                        goto Label_009C;
                    }
                }
                return flag2;
            Label_009C:
                this.DirectoryPath = directoryPath;
                flag = false;
            }
            catch (Exception exception)
            {
                this.DirectoryPath = directoryPath;
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return flag;
        }

        public bool IsValidFileChars(string FileName)
        {
            char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
            foreach (char ch in FileName.ToCharArray())
            {
                if (Array.BinarySearch<char>(invalidFileNameChars, ch) >= 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsValidPathChars(string DirectoryName)
        {
            char[] invalidPathChars = Path.GetInvalidPathChars();
            foreach (char ch in DirectoryName.ToCharArray())
            {
                if (Array.BinarySearch<char>(invalidPathChars, ch) >= 0)
                {
                    return false;
                }
            }
            return true;
        }

        public FileStruct[] ListDirectories()
        {
            FileStruct[] structArray = this.ListFilesAndDirectories();
            List<FileStruct> list = new List<FileStruct>();
            foreach (FileStruct struct2 in structArray)
            {
                if (struct2.IsDirectory)
                {
                    list.Add(struct2);
                }
            }
            return list.ToArray();
        }

        public FileStruct[] ListFiles()
        {
            FileStruct[] structArray = this.ListFilesAndDirectories();
            List<FileStruct> list = new List<FileStruct>();
            foreach (FileStruct struct2 in structArray)
            {
                if (!struct2.IsDirectory)
                {
                    list.Add(struct2);
                }
            }
            return list.ToArray();
        }

        public FileStruct[] ListFilesAndDirectories()
        {
            this.ftpWebResponse = this.DoResponse(this.Uri, "LIST");
            string str = new StreamReader(this.ftpWebResponse.GetResponseStream(), Encoding.Default).ReadToEnd();
            return this.method_6(str);
        }

        public bool MakeDirectory(string DirectoryName)
        {
            bool flag;
            try
            {
                if (!this.IsValidPathChars(DirectoryName))
                {
                    throw new Exception("目录名非法！");
                }
                if (this.DirectoryExist(DirectoryName))
                {
                    throw new Exception("服务器上面已经存在同名的文件名或目录名！");
                }
                this.ftpWebResponse = this.DoResponse(new System.Uri(this.Uri.ToString() + DirectoryName), "MKD");
                flag = true;
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return flag;
        }

        private FtpWebResponse DoResponse(System.Uri uri_1, string method)
        {
            FtpWebResponse response;
            try
            {
                this.ftpWebRequest = (FtpWebRequest) WebRequest.Create(uri_1);
                this.ftpWebRequest.Method = method;
                this.ftpWebRequest.UseBinary = true;
                this.ftpWebRequest.Credentials = new NetworkCredential(this.UserName, this.Password);
                if (this.Proxy != null)
                {
                    this.ftpWebRequest.Proxy = this.Proxy;
                }
                response = (FtpWebResponse) this.ftpWebRequest.GetResponse();
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return response;
        }

        private FtpWebRequest DoRequest(System.Uri uri_1, string method)
        {
            FtpWebRequest request;
            try
            {
                this.ftpWebRequest = (FtpWebRequest) WebRequest.Create(uri_1);
                this.ftpWebRequest.Method = method;
                this.ftpWebRequest.UseBinary = true;
                this.ftpWebRequest.Credentials = new NetworkCredential(this.UserName, this.Password);
                if (this.Proxy != null)
                {
                    this.ftpWebRequest.Proxy = this.Proxy;
                }
                request = this.ftpWebRequest;
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return request;
        }

        private string method_10(ref string string_5, char char_0, int int_0)
        {
            int index = string_5.IndexOf(char_0, int_0);
            string str = string_5.Substring(0, index);
            string_5 = string_5.Substring(index).Trim();
            return str;
        }

        private bool method_11(string string_5)
        {
            bool flag;
            try
            {
                if (!((string_5.IndexOf("/") < 0) && this.IsValidPathChars(string_5)))
                {
                    throw new Exception("目录名非法!");
                }
                if ((string_5.Length > 0) && this.DirectoryExist(string_5))
                {
                    if (!string_5.EndsWith("/"))
                    {
                        string_5 = string_5 + "/";
                    }
                    this.directoryPath = this.directoryPath + string_5;
                    return true;
                }
                flag = false;
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return flag;
        }

        private void method_2(object sender, AsyncCompletedEventArgs e)
        {
            if (this.DownloadDataCompleted != null)
            {
                this.DownloadDataCompleted(sender, e);
            }
        }

        private void method_3(object sender, DownloadProgressChangedEventArgs e)
        {
            if (this.DownloadProgressChanged != null)
            {
                this.DownloadProgressChanged(sender, e);
            }
        }

        private void method_4(object sender, UploadFileCompletedEventArgs e)
        {
            if (this.bool_0)
            {
                if (System.IO.File.Exists(this.remoteFile))
                {
                    System.IO.File.SetAttributes(this.remoteFile, FileAttributes.Normal);
                    System.IO.File.Delete(this.remoteFile);
                }
                this.bool_0 = false;
            }
            if (this.UploadFileCompleted != null)
            {
                this.UploadFileCompleted(sender, e);
            }
        }

        private void method_5(object sender, UploadProgressChangedEventArgs e)
        {
            if (this.UploadProgressChanged != null)
            {
                this.UploadProgressChanged(sender, e);
            }
        }

        private FileStruct[] method_6(string string_5)
        {
            List<FileStruct> list = new List<FileStruct>();
            string[] strArray = string_5.Split(new char[] { '\n' });
            FileListStyle style = this.method_8(strArray);
            foreach (string str in strArray)
            {
                if ((style == FileListStyle.Unknown) || !(str != ""))
                {
                    continue;
                }
                FileStruct item = new FileStruct {
                    Name = ".."
                };
                switch (style)
                {
                    case FileListStyle.UnixStyle:
                        item = this.method_9(str);
                        break;

                    case FileListStyle.WindowsStyle:
                        item = this.method_7(str);
                        break;
                }
                if ((item.Name != ".") && (item.Name != ".."))
                {
                    list.Add(item);
                }
            }
            return list.ToArray();
        }

        private FileStruct method_7(string string_5)
        {
            FileStruct struct2 = new FileStruct();
            string str = string_5.Trim();
            string str2 = str.Substring(0, 8);
            str = str.Substring(8, str.Length - 8).Trim();
            string str3 = str.Substring(0, 7);
            str = str.Substring(7, str.Length - 7).Trim();
            DateTimeFormatInfo dateTimeFormat = new CultureInfo("en-US", false).DateTimeFormat;
            dateTimeFormat.ShortTimePattern = "t";
            struct2.CreateTime = DateTime.Parse(str2 + " " + str3, dateTimeFormat);
            if (str.Substring(0, 5) == "<DIR>")
            {
                struct2.IsDirectory = true;
                str = str.Substring(5, str.Length - 5).Trim();
            }
            else
            {
                str = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];
                struct2.IsDirectory = false;
            }
            struct2.Name = str;
            return struct2;
        }

        private FileListStyle method_8(string[] string_5)
        {
            foreach (string str in string_5)
            {
                if ((str.Length > 10) && Regex.IsMatch(str.Substring(0, 10), "(-|d)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)"))
                {
                    return FileListStyle.UnixStyle;
                }
                if ((str.Length > 8) && Regex.IsMatch(str.Substring(0, 8), "[0-9][0-9]-[0-9][0-9]-[0-9][0-9]"))
                {
                    return FileListStyle.WindowsStyle;
                }
            }
            return FileListStyle.Unknown;
        }

        private FileStruct method_9(string string_5)
        {
            FileStruct struct2 = new FileStruct();
            string str = string_5.Trim();
            struct2.Flags = str.Substring(0, 10);
            struct2.IsDirectory = struct2.Flags[0] == 'd';
            str = str.Substring(11).Trim();
            this.method_10(ref str, ' ', 0);
            struct2.Owner = this.method_10(ref str, ' ', 0);
            struct2.Group = this.method_10(ref str, ' ', 0);
            this.method_10(ref str, ' ', 0);
            string oldValue = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[2];
            if (oldValue.IndexOf(":") >= 0)
            {
                str = str.Replace(oldValue, DateTime.Now.Year.ToString());
            }
            struct2.CreateTime = DateTime.Parse(this.method_10(ref str, ' ', 8));
            struct2.Name = str;
            return struct2;
        }

        public bool MoveFileToAnotherDirectory(string RemoteFile, string DirectoryName)
        {
            bool flag;
            string directoryPath = this.DirectoryPath;
            try
            {
                if (DirectoryName == "")
                {
                    return false;
                }
                if (!DirectoryName.StartsWith("/"))
                {
                    DirectoryName = "/" + DirectoryName;
                }
                if (!DirectoryName.EndsWith("/"))
                {
                    DirectoryName = DirectoryName + "/";
                }
                bool flag2 = this.ReName(RemoteFile, DirectoryName + RemoteFile);
                this.DirectoryPath = directoryPath;
                flag = flag2;
            }
            catch (Exception exception)
            {
                this.DirectoryPath = directoryPath;
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return flag;
        }

        public bool RemoveDirectory(string DirectoryName)
        {
            bool flag;
            try
            {
                if (!this.IsValidPathChars(DirectoryName))
                {
                    throw new Exception("目录名非法！");
                }
                if (!this.DirectoryExist(DirectoryName))
                {
                    throw new Exception("服务器上面不存在指定的文件名或目录名！");
                }
                this.ftpWebResponse = this.DoResponse(new System.Uri(this.Uri.ToString() + DirectoryName), "RMD");
                flag = true;
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return flag;
        }

        public bool ReName(string RemoteFileName, string NewFileName)
        {
            bool flag;
            try
            {
                if (!(this.IsValidFileChars(RemoteFileName) && this.IsValidFileChars(NewFileName)))
                {
                    throw new Exception("文件名非法！");
                }
                if (RemoteFileName == NewFileName)
                {
                    return true;
                }
                if (!this.FileExist(RemoteFileName))
                {
                    throw new Exception("文件在服务器上不存在！");
                }
                this.ftpWebRequest = this.DoRequest(new System.Uri(this.Uri.ToString() + RemoteFileName), "RENAME");
                this.ftpWebRequest.RenameTo = NewFileName;
                this.ftpWebResponse = (FtpWebResponse) this.ftpWebRequest.GetResponse();
                flag = true;
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return flag;
        }

        public bool UploadFile(string LocalFullPath)
        {
            return this.UploadFile(LocalFullPath, Path.GetFileName(LocalFullPath), false);
        }

        public bool UploadFile(byte[] FileBytes, string RemoteFileName)
        {
            if (!this.IsValidFileChars(RemoteFileName))
            {
                throw new Exception("非法文件名或目录名!");
            }
            return this.UploadFile(FileBytes, RemoteFileName, false);
        }

        public bool UploadFile(string LocalFullPath, bool OverWriteRemoteFile)
        {
            return this.UploadFile(LocalFullPath, Path.GetFileName(LocalFullPath), OverWriteRemoteFile);
        }

        public bool UploadFile(string LocalFullPath, string RemoteFileName)
        {
            return this.UploadFile(LocalFullPath, RemoteFileName, false);
        }

        public bool UploadFile(byte[] FileBytes, string RemoteFileName, bool OverWriteRemoteFile)
        {
            bool flag;
            try
            {
                if (!this.IsValidFileChars(RemoteFileName))
                {
                    throw new Exception("非法文件名！");
                }
                if (!(OverWriteRemoteFile || !this.FileExist(RemoteFileName)))
                {
                    throw new Exception("FTP服务上面已经存在同名文件！");
                }
                this.ftpWebResponse = this.DoResponse(new System.Uri(this.Uri.ToString() + RemoteFileName), "STOR");
                Stream requestStream = this.ftpWebRequest.GetRequestStream();
                MemoryStream stream2 = new MemoryStream(FileBytes);
                byte[] buffer = new byte[0x400];
                int count = 0;
                int num2 = 0;
                goto Label_008D;
            Label_007E:
                num2 += count;
                requestStream.Write(buffer, 0, count);
            Label_008D:
                count = stream2.Read(buffer, 0, buffer.Length);
                if (count != 0)
                {
                    goto Label_007E;
                }
                requestStream.Close();
                this.ftpWebResponse = (FtpWebResponse) this.ftpWebRequest.GetResponse();
                stream2.Close();
                stream2.Dispose();
                FileBytes = null;
                flag = true;
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return flag;
        }

        public bool UploadFile(string LocalFullPath, string RemoteFileName, bool OverWriteRemoteFile)
        {
            bool flag;
            try
            {
                if (!((this.IsValidFileChars(RemoteFileName) && this.IsValidFileChars(Path.GetFileName(LocalFullPath))) && this.IsValidPathChars(Path.GetDirectoryName(LocalFullPath))))
                {
                    throw new Exception("非法文件名或目录名!");
                }
                if (!System.IO.File.Exists(LocalFullPath))
                {
                    throw new Exception("本地文件不存在!");
                }
                FileStream stream = new FileStream(LocalFullPath, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, (int) stream.Length);
                stream.Close();
                flag = this.UploadFile(buffer, RemoteFileName, OverWriteRemoteFile);
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return flag;
        }

        public void UploadFileAsync(string LocalFullPath)
        {
            this.UploadFileAsync(LocalFullPath, Path.GetFileName(LocalFullPath), false);
        }

        public void UploadFileAsync(string LocalFullPath, bool OverWriteRemoteFile)
        {
            this.UploadFileAsync(LocalFullPath, Path.GetFileName(LocalFullPath), OverWriteRemoteFile);
        }

        public void UploadFileAsync(string LocalFullPath, string RemoteFileName)
        {
            this.UploadFileAsync(LocalFullPath, RemoteFileName, false);
        }

        public void UploadFileAsync(byte[] FileBytes, string RemoteFileName)
        {
            if (!this.IsValidFileChars(RemoteFileName))
            {
                throw new Exception("非法文件名或目录名!");
            }
            this.UploadFileAsync(FileBytes, RemoteFileName, false);
        }

        public void UploadFileAsync(byte[] FileBytes, string RemoteFileName, bool OverWriteRemoteFile)
        {
            try
            {
                if (!this.IsValidFileChars(RemoteFileName))
                {
                    throw new Exception("非法文件名！");
                }
                if (!(OverWriteRemoteFile || !this.FileExist(RemoteFileName)))
                {
                    throw new Exception("FTP服务上面已经存在同名文件！");
                }
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Templates);
                if (!folderPath.EndsWith(@"\"))
                {
                    folderPath = folderPath + @"\";
                }
                string path = Path.ChangeExtension(folderPath + Path.GetRandomFileName(), Path.GetExtension(RemoteFileName));
                FileStream stream = new FileStream(path, FileMode.CreateNew, FileAccess.Write);
                stream.Write(FileBytes, 0, FileBytes.Length);
                stream.Flush();
                stream.Close();
                stream.Dispose();
                this.bool_0 = true;
                this.remoteFile = path;
                FileBytes = null;
                this.UploadFileAsync(path, RemoteFileName, OverWriteRemoteFile);
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
        }

        public void UploadFileAsync(string LocalFullPath, string RemoteFileName, bool OverWriteRemoteFile)
        {
            try
            {
                if (!((this.IsValidFileChars(RemoteFileName) && this.IsValidFileChars(Path.GetFileName(LocalFullPath))) && this.IsValidPathChars(Path.GetDirectoryName(LocalFullPath))))
                {
                    throw new Exception("非法文件名或目录名!");
                }
                if (!(OverWriteRemoteFile || !this.FileExist(RemoteFileName)))
                {
                    throw new Exception("FTP服务上面已经存在同名文件！");
                }
                if (!System.IO.File.Exists(LocalFullPath))
                {
                    throw new Exception("本地文件不存在!");
                }
                FTPWebClient class2 = new FTPWebClient();
                class2.UploadProgressChanged += new UploadProgressChangedEventHandler(this.method_5);
                class2.UploadFileCompleted += new UploadFileCompletedEventHandler(this.method_4);
                class2.Credentials = new NetworkCredential(this.UserName, this.Password);
                if (this.Proxy != null)
                {
                    class2.Proxy = this.Proxy;
                }
                class2.UploadFileAsync(new System.Uri(this.Uri.ToString() + RemoteFileName), LocalFullPath);
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
        }

        public string DirectoryPath
        {
            get
            {
                return this.directoryPath;
            }
            set
            {
                this.directoryPath = value;
            }
        }

        public string ErrorMsg
        {
            get
            {
                return this.errorMsg;
            }
            set
            {
                this.errorMsg = value;
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        public WebProxy Proxy
        {
            get
            {
                return this.proxy;
            }
            set
            {
                this.proxy = value;
            }
        }

        public System.Uri Uri
        {
            get
            {
                if (this.directoryPath == "/")
                {
                    return this.uri;
                }
                string str = this.uri.ToString();
                if (str.EndsWith("/"))
                {
                    str = str.Substring(0, str.Length - 1);
                }
                return new System.Uri(str + this.DirectoryPath);
            }
            set
            {
                if (value.Scheme != System.Uri.UriSchemeFtp)
                {
                    throw new Exception("Ftp 地址格式错误!");
                }
                this.uri = new System.Uri(value.GetLeftPart(UriPartial.Authority));
                this.directoryPath = value.AbsolutePath;
                if (!this.directoryPath.EndsWith("/"))
                {
                    this.directoryPath = this.directoryPath + "/";
                }
            }
        }

        public string UserName
        {
            get
            {
                return this.userName;
            }
            set
            {
                this.userName = value;
            }
        }

        internal class FTPWebClient : WebClient
        {
            protected override WebRequest GetWebRequest(Uri address)
            {
                FtpWebRequest webRequest = (FtpWebRequest) base.GetWebRequest(address);
                webRequest.UsePassive = false;
                return webRequest;
            }
        }

        public delegate void De_DownloadDataCompleted(object sender, AsyncCompletedEventArgs e);

        public delegate void De_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e);

        public delegate void De_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e);

        public delegate void De_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e);
	}
}
