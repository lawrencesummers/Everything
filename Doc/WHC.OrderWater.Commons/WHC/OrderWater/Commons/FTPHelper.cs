namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;

    public class FTPHelper
    {
        private bool bool_0;
        private FileStream fileStream_0;
        private int int_0;
        private IPEndPoint ipendPoint_0;
        private IPEndPoint ipendPoint_1;
        private long long_0;
        private long long_1;
        public string pass;
        public int port;
        public string server;
        private Socket socket_0;
        private Socket socket_1;
        private Socket socket_2;
        private string string_0;
        private string string_1;
        private string string_2;
        public int timeout;
        public string user;

        public FTPHelper()
        {
            this.server = null;
            this.user = null;
            this.pass = null;
            this.port = 0x15;
            this.bool_0 = true;
            this.socket_0 = null;
            this.ipendPoint_0 = null;
            this.socket_1 = null;
            this.socket_2 = null;
            this.ipendPoint_1 = null;
            this.fileStream_0 = null;
            this.string_2 = "";
            this.long_0 = 0;
            this.timeout = 0x2710;
            this.string_0 = "";
        }

        public FTPHelper(string server, string user, string pass)
        {
            this.server = server;
            this.user = user;
            this.pass = pass;
            this.port = 0x15;
            this.bool_0 = true;
            this.socket_0 = null;
            this.ipendPoint_0 = null;
            this.socket_1 = null;
            this.socket_2 = null;
            this.ipendPoint_1 = null;
            this.fileStream_0 = null;
            this.string_2 = "";
            this.long_0 = 0;
            this.timeout = 0x2710;
            this.string_0 = "";
        }

        public FTPHelper(string server, int port, string user, string pass)
        {
            this.server = server;
            this.user = user;
            this.pass = pass;
            this.port = port;
            this.bool_0 = true;
            this.socket_0 = null;
            this.ipendPoint_0 = null;
            this.socket_1 = null;
            this.socket_2 = null;
            this.ipendPoint_1 = null;
            this.fileStream_0 = null;
            this.string_2 = "";
            this.long_0 = 0;
            this.timeout = 0x2710;
            this.string_0 = "";
        }

        public void ChangeDir(string path)
        {
            this.Connect();
            this.method_2("CWD " + path);
            this.method_5();
            if (this.int_0 != 250)
            {
                throw new Exception(this.string_1);
            }
        }

        public void Connect()
        {
            if (this.server == null)
            {
                throw new Exception("No server has been set.");
            }
            if (this.user == null)
            {
                throw new Exception("No username has been set.");
            }
            if ((this.socket_0 == null) || !this.socket_0.Connected)
            {
                this.socket_0 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.ipendPoint_0 = new IPEndPoint(Dns.GetHostByName(this.server).AddressList[0], this.port);
                try
                {
                    this.socket_0.Connect(this.ipendPoint_0);
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message);
                }
                this.method_5();
                if (this.int_0 != 220)
                {
                    this.method_0();
                }
                this.method_2("USER " + this.user);
                this.method_5();
                int num = this.int_0;
                if ((num != 230) && (num == 0x14b))
                {
                    if (this.pass == null)
                    {
                        this.Disconnect();
                        throw new Exception("No password has been set.");
                    }
                    this.method_2("PASS " + this.pass);
                    this.method_5();
                    if (this.int_0 != 230)
                    {
                        this.method_0();
                    }
                }
            }
        }

        public void Connect(string server, string user, string pass)
        {
            this.server = server;
            this.user = user;
            this.pass = pass;
            this.Connect();
        }

        public void Connect(string server, int port, string user, string pass)
        {
            this.server = server;
            this.user = user;
            this.pass = pass;
            this.port = port;
            this.Connect();
        }

        public void Disconnect()
        {
            this.eZqxmoxYij();
            if (this.socket_0 != null)
            {
                if (this.socket_0.Connected)
                {
                    this.method_2("QUIT");
                    this.socket_0.Close();
                }
                this.socket_0 = null;
            }
            if (this.fileStream_0 != null)
            {
                this.fileStream_0.Close();
            }
            this.ipendPoint_0 = null;
            this.fileStream_0 = null;
        }

        public long DoDownload()
        {
            long num;
            byte[] buffer = new byte[0x200];
            try
            {
                num = this.socket_2.Receive(buffer, buffer.Length, SocketFlags.None);
                if (num <= 0)
                {
                    this.eZqxmoxYij();
                    this.fileStream_0.Close();
                    this.fileStream_0 = null;
                    this.method_5();
                    int num2 = this.int_0;
                    if ((num2 != 0xe2) && (num2 != 250))
                    {
                        throw new Exception(this.string_1);
                    }
                    this.method_1(false);
                    return num;
                }
                this.fileStream_0.Write(buffer, 0, (int) num);
                this.long_0 += num;
            }
            catch (Exception exception)
            {
                this.eZqxmoxYij();
                this.fileStream_0.Close();
                this.fileStream_0 = null;
                this.method_5();
                this.method_1(false);
                throw exception;
            }
            return num;
        }

        public long DoUpload()
        {
            long num;
            byte[] buffer = new byte[0x200];
            try
            {
                num = this.fileStream_0.Read(buffer, 0, buffer.Length);
                this.long_0 += num;
                this.socket_2.Send(buffer, (int) num, SocketFlags.None);
                if (num > 0)
                {
                    return num;
                }
                this.fileStream_0.Close();
                this.fileStream_0 = null;
                this.eZqxmoxYij();
                this.method_5();
                int num2 = this.int_0;
                if ((num2 != 0xe2) && (num2 != 250))
                {
                    throw new Exception(this.string_1);
                }
                this.method_1(false);
            }
            catch (Exception exception)
            {
                this.fileStream_0.Close();
                this.fileStream_0 = null;
                this.eZqxmoxYij();
                this.method_5();
                this.method_1(false);
                throw exception;
            }
            return num;
        }

        private void eZqxmoxYij()
        {
            if (this.socket_2 != null)
            {
                if (this.socket_2.Connected)
                {
                    this.socket_2.Close();
                }
                this.socket_2 = null;
            }
            this.ipendPoint_1 = null;
        }

        public DateTime GetFileDate(string fileName)
        {
            return this.method_8(this.GetFileDateRaw(fileName));
        }

        public string GetFileDateRaw(string fileName)
        {
            this.Connect();
            this.method_2("MDTM " + fileName);
            this.method_5();
            if (this.int_0 != 0xd5)
            {
                throw new Exception(this.string_1);
            }
            return this.string_1.Substring(4);
        }

        public long GetFileSize(string filename)
        {
            this.Connect();
            this.method_2("SIZE " + filename);
            this.method_5();
            if (this.int_0 != 0xd5)
            {
                throw new Exception(this.string_1);
            }
            return long.Parse(this.string_1.Substring(4));
        }

        public string GetWorkingDirectory()
        {
            string str;
            this.Connect();
            this.method_2("PWD");
            this.method_5();
            if (this.int_0 != 0x101)
            {
                throw new Exception(this.string_1);
            }
            try
            {
                str = this.string_1.Substring(this.string_1.IndexOf("\"", 0) + 1);
                str = str.Substring(0, str.LastIndexOf("\"")).Replace("\"\"", "\"");
            }
            catch (Exception exception)
            {
                throw new Exception("Uhandled PWD response: " + exception.Message);
            }
            return str;
        }

        public ArrayList List()
        {
            byte[] buffer = new byte[0x200];
            string str = "";
            long num = 0;
            int num2 = 0;
            ArrayList list = new ArrayList();
            this.Connect();
            this.method_6();
            this.method_2("LIST");
            this.method_5();
            int num3 = this.int_0;
            if ((num3 != 0x7d) && (num3 != 150))
            {
                this.eZqxmoxYij();
                throw new Exception(this.string_1);
            }
            this.method_7();
            while (this.socket_2.Available < 1)
            {
                Thread.Sleep(50);
                num2 += 50;
                if (num2 > (this.timeout / 10))
                {
                    break;
                }
            }
            while (this.socket_2.Available > 0)
            {
                num = this.socket_2.Receive(buffer, buffer.Length, SocketFlags.None);
                str = str + Encoding.ASCII.GetString(buffer, 0, (int) num);
                Thread.Sleep(50);
            }
            this.eZqxmoxYij();
            this.method_5();
            if (this.int_0 != 0xe2)
            {
                throw new Exception(this.string_1);
            }
            foreach (string str2 in str.Split(new char[] { '\n' }))
            {
                if (!((str2.Length <= 0) || Regex.Match(str2, "^total").Success))
                {
                    list.Add(str2.Substring(0, str2.Length - 1));
                }
            }
            return list;
        }

        public ArrayList ListDirectories()
        {
            ArrayList list = new ArrayList();
            foreach (string str in this.List())
            {
                if ((str.Length > 0) && ((str[0] == 'd') || (str.ToUpper().IndexOf("<DIR>") >= 0)))
                {
                    list.Add(str);
                }
            }
            return list;
        }

        public ArrayList ListFiles()
        {
            ArrayList list = new ArrayList();
            foreach (string str in this.List())
            {
                if ((str.Length > 0) && ((str[0] != 'd') && (str.ToUpper().IndexOf("<DIR>") < 0)))
                {
                    list.Add(str);
                }
            }
            return list;
        }

        public void MakeDir(string dir)
        {
            this.Connect();
            this.method_2("MKD " + dir);
            this.method_5();
            int num = this.int_0;
            if ((num != 250) && (num != 0x101))
            {
                throw new Exception(this.string_1);
            }
        }

        private void method_0()
        {
            this.Disconnect();
            throw new Exception(this.string_1);
        }

        private void method_1(bool bool_1)
        {
            if (bool_1)
            {
                this.method_2("TYPE I");
            }
            else
            {
                this.method_2("TYPE A");
            }
            this.method_5();
            if (this.int_0 != 200)
            {
                this.method_0();
            }
        }

        private void method_2(string string_3)
        {
            byte[] bytes = Encoding.ASCII.GetBytes((string_3 + "\r\n").ToCharArray());
            this.socket_0.Send(bytes, bytes.Length, SocketFlags.None);
        }

        private void method_3()
        {
            byte[] buffer = new byte[0x200];
            int num = 0;
            while (this.socket_0.Available < 1)
            {
                Thread.Sleep(50);
                num += 50;
                if (num > this.timeout)
                {
                    this.Disconnect();
                    throw new Exception("Timed out waiting on server to respond.");
                }
            }
            while (this.socket_0.Available > 0)
            {
                long num2 = this.socket_0.Receive(buffer, 0x200, SocketFlags.None);
                this.string_2 = this.string_2 + Encoding.ASCII.GetString(buffer, 0, (int) num2);
                Thread.Sleep(50);
            }
        }

        private string method_4()
        {
            string str = "";
            int index = this.string_2.IndexOf('\n');
            if (index < 0)
            {
                while (index < 0)
                {
                    this.method_3();
                    index = this.string_2.IndexOf('\n');
                }
            }
            str = this.string_2.Substring(0, index);
            this.string_2 = this.string_2.Substring(index + 1);
            return str;
        }

        private void method_5()
        {
            string str;
            this.string_0 = "";
        Label_0033:
            str = this.method_4();
            if (!Regex.Match(str, "^[0-9]+ ").Success)
            {
                this.string_0 = this.string_0 + Regex.Replace(str, "^[0-9]+-", "") + "\n";
                goto Label_0033;
            }
            this.string_1 = str;
            this.int_0 = int.Parse(str.Substring(0, 3));
        }

        private void method_6()
        {
            Exception exception;
            if (this.bool_0)
            {
                string[] strArray;
                this.Connect();
                this.method_2("PASV");
                this.method_5();
                if (this.int_0 != 0xe3)
                {
                    this.method_0();
                }
                try
                {
                    int startIndex = this.string_1.IndexOf('(') + 1;
                    int length = this.string_1.IndexOf(')') - startIndex;
                    strArray = this.string_1.Substring(startIndex, length).Split(new char[] { ',' });
                }
                catch (Exception)
                {
                    this.Disconnect();
                    throw new Exception("Malformed PASV response: " + this.string_1);
                }
                if (strArray.Length < 6)
                {
                    this.Disconnect();
                    throw new Exception("Malformed PASV response: " + this.string_1);
                }
                string hostName = string.Format("{0}.{1}.{2}.{3}", new object[] { strArray[0], strArray[1], strArray[2], strArray[3] });
                int port = (int.Parse(strArray[4]) << 8) + int.Parse(strArray[5]);
                try
                {
                    this.eZqxmoxYij();
                    this.socket_2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    this.ipendPoint_1 = new IPEndPoint(Dns.GetHostByName(hostName).AddressList[0], port);
                    this.socket_2.Connect(this.ipendPoint_1);
                    return;
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                    throw new Exception("Failed to connect for data transfer: " + exception.Message);
                }
            }
            this.Connect();
            try
            {
                this.eZqxmoxYij();
                this.socket_1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                string str2 = this.socket_0.LocalEndPoint.ToString();
                int index = str2.IndexOf(':');
                if (index < 0)
                {
                    throw new Exception("Failed to parse the local address: " + str2);
                }
                string ipString = str2.Substring(0, index);
                IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(ipString), 0);
                this.socket_1.Bind(localEP);
                str2 = this.socket_1.LocalEndPoint.ToString();
                index = str2.IndexOf(':');
                if (index < 0)
                {
                    throw new Exception("Failed to parse the local address: " + str2);
                }
                int num5 = int.Parse(str2.Substring(index + 1));
                this.socket_1.Listen(1);
                string str4 = string.Format("PORT {0},{1},{2}", ipString.Replace('.', ','), num5 / 0x100, num5 % 0x100);
                this.method_2(str4);
                this.method_5();
                if (this.int_0 != 200)
                {
                    this.method_0();
                }
            }
            catch (Exception exception3)
            {
                exception = exception3;
                throw new Exception("Failed to connect for data transfer: " + exception.Message);
            }
        }

        private void method_7()
        {
            if (this.socket_2 == null)
            {
                try
                {
                    this.socket_2 = this.socket_1.Accept();
                    this.socket_1.Close();
                    this.socket_1 = null;
                    if (this.socket_2 == null)
                    {
                        throw new Exception("Winsock error: " + Convert.ToString(Marshal.GetLastWin32Error()));
                    }
                }
                catch (Exception exception)
                {
                    throw new Exception("Failed to connect for data transfer: " + exception.Message);
                }
            }
        }

        private DateTime method_8(string string_3)
        {
            if (string_3.Length < 14)
            {
                throw new ArgumentException("Input Value for ConvertFTPDateToDateTime method was too short.");
            }
            int year = Convert.ToInt16(string_3.Substring(0, 4));
            int month = Convert.ToInt16(string_3.Substring(4, 2));
            int day = Convert.ToInt16(string_3.Substring(6, 2));
            int hour = Convert.ToInt16(string_3.Substring(8, 2));
            int minute = Convert.ToInt16(string_3.Substring(10, 2));
            return new DateTime(year, month, day, hour, minute, Convert.ToInt16(string_3.Substring(12, 2)));
        }

        public void OpenDownload(string filename)
        {
            this.OpenDownload(filename, filename, false);
        }

        public void OpenDownload(string filename, bool resume)
        {
            this.OpenDownload(filename, filename, resume);
        }

        public void OpenDownload(string filename, string localfilename)
        {
            this.OpenDownload(filename, localfilename, false);
        }

        public void OpenDownload(string remote_filename, string local_filename, bool resume)
        {
            Exception exception;
            this.Connect();
            this.method_1(true);
            this.long_0 = 0;
            try
            {
                this.long_1 = this.GetFileSize(remote_filename);
            }
            catch
            {
                this.long_1 = 0;
            }
            if (resume && File.Exists(local_filename))
            {
                try
                {
                    this.fileStream_0 = new FileStream(local_filename, FileMode.Open);
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    this.fileStream_0 = null;
                    throw new Exception(exception.Message);
                }
                this.method_2("REST " + this.fileStream_0.Length);
                this.method_5();
                if (this.int_0 != 350)
                {
                    throw new Exception(this.string_1);
                }
                this.fileStream_0.Seek(this.fileStream_0.Length, SeekOrigin.Begin);
                this.long_0 = this.fileStream_0.Length;
            }
            else
            {
                try
                {
                    this.fileStream_0 = new FileStream(local_filename, FileMode.Create);
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                    this.fileStream_0 = null;
                    throw new Exception(exception.Message);
                }
            }
            this.method_6();
            this.method_2("RETR " + remote_filename);
            this.method_5();
            int num = this.int_0;
            if ((num != 0x7d) && (num != 150))
            {
                this.fileStream_0.Close();
                this.fileStream_0 = null;
                throw new Exception(this.string_1);
            }
            this.method_7();
        }

        public void OpenUpload(string filename)
        {
            this.OpenUpload(filename, filename, false);
        }

        public void OpenUpload(string filename, bool resume)
        {
            this.OpenUpload(filename, filename, resume);
        }

        public void OpenUpload(string filename, string remotefilename)
        {
            this.OpenUpload(filename, remotefilename, false);
        }

        public void OpenUpload(string filename, string remote_filename, bool resume)
        {
            this.Connect();
            this.method_1(true);
            this.method_6();
            this.long_0 = 0;
            try
            {
                this.fileStream_0 = new FileStream(filename, FileMode.Open);
            }
            catch (Exception exception)
            {
                this.fileStream_0 = null;
                throw new Exception(exception.Message);
            }
            this.long_1 = this.fileStream_0.Length;
            if (resume)
            {
                long fileSize = this.GetFileSize(remote_filename);
                this.method_2("REST " + fileSize);
                this.method_5();
                if (this.int_0 == 350)
                {
                    this.fileStream_0.Seek(fileSize, SeekOrigin.Begin);
                }
            }
            this.method_2("STOR " + remote_filename);
            this.method_5();
            int num = this.int_0;
            if ((num != 0x7d) && (num != 150))
            {
                this.fileStream_0.Close();
                this.fileStream_0 = null;
                throw new Exception(this.string_1);
            }
            this.method_7();
        }

        public void RemoveDir(string dir)
        {
            this.Connect();
            this.method_2("RMD " + dir);
            this.method_5();
            if (this.int_0 != 250)
            {
                throw new Exception(this.string_1);
            }
        }

        public void RemoveFile(string filename)
        {
            this.Connect();
            this.method_2("DELE " + filename);
            this.method_5();
            if (this.int_0 != 250)
            {
                throw new Exception(this.string_1);
            }
        }

        public void RenameFile(string oldfilename, string newfilename)
        {
            this.Connect();
            this.method_2("RNFR " + oldfilename);
            this.method_5();
            if (this.int_0 != 350)
            {
                throw new Exception(this.string_1);
            }
            this.method_2("RNTO " + newfilename);
            this.method_5();
            if (this.int_0 != 250)
            {
                throw new Exception(this.string_1);
            }
        }

        public long BytesTotal
        {
            get
            {
                return this.long_0;
            }
        }

        public long FileSize
        {
            get
            {
                return this.long_1;
            }
        }

        public bool IsConnected
        {
            get
            {
                return ((this.socket_0 != null) && this.socket_0.Connected);
            }
        }

        public string Messages
        {
            get
            {
                string str = this.string_0;
                this.string_0 = "";
                return str;
            }
        }

        public bool MessagesAvailable
        {
            get
            {
                return (this.string_0.Length > 0);
            }
        }

        public bool PassiveMode
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        public string ResponseString
        {
            get
            {
                return this.string_1;
            }
        }
    }
}

