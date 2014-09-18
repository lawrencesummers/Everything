namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections.Generic;
    using System.Management;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;

    public class NetworkUtil
    {
        public static void BindEndPoint(Socket socket, IPEndPoint endPoint)
        {
            if (!socket.IsBound)
            {
                socket.Bind(endPoint);
            }
        }

        public static void BindEndPoint(Socket socket, string ip, int port)
        {
            IPEndPoint localEP = CreateIPEndPoint(ip, port);
            if (!socket.IsBound)
            {
                socket.Bind(localEP);
            }
        }

        public static void Close(Socket socket)
        {
            try
            {
                socket.Shutdown(SocketShutdown.Both);
            }
            catch (SocketException exception)
            {
                throw exception;
            }
            finally
            {
                socket.Close();
            }
        }

        public static bool Connect(Socket socket, string ip, int port)
        {
            socket.Connect(ip, port);
            return socket.Poll(-1, SelectMode.SelectWrite);
        }

        public static string ConvertDnsToIp(string hostname)
        {
            IPHostEntry hostByName = Dns.GetHostByName(hostname);
            if (hostByName != null)
            {
                return hostByName.AddressList[0].ToString();
            }
            return null;
        }

        public static string ConvertIpToDns(string ipAddress)
        {
            IPHostEntry entry = Dns.Resolve(ipAddress);
            if (entry != null)
            {
                return entry.HostName;
            }
            return null;
        }

        public static IPEndPoint CreateIPEndPoint(string ip, int port)
        {
            return new IPEndPoint(StringToIPAddress(ip), port);
        }

        public static TcpListener CreateTcpListener()
        {
            return new TcpListener(new IPEndPoint(IPAddress.Any, 0));
        }

        public static TcpListener CreateTcpListener(string ip, int port)
        {
            return new TcpListener(new IPEndPoint(StringToIPAddress(ip), port));
        }

        public static Socket CreateTcpSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public static Socket CreateUdpSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        public static string GetClientIP(Socket clientSocket)
        {
            IPEndPoint remoteEndPoint = (IPEndPoint) clientSocket.RemoteEndPoint;
            return remoteEndPoint.Address.ToString();
        }

        public static string GetHostName(IPAddress ip)
        {
            return GetHostName(ip.ToString());
        }

        public static string GetHostName(IPEndPoint ipEndPoint)
        {
            return GetHostName(ipEndPoint.Address);
        }

        public static string GetHostName(string hostIP)
        {
            string hostName;
            try
            {
                hostName = Dns.Resolve(hostIP).HostName;
            }
            catch
            {
            }
            return hostName;
        }

        public static string GetIPAddress()
        {
            ManagementObjectCollection objects;
            string str = "";
            ManagementClass class2 = new ManagementClass("Win32_NetworkAdapterConfiguration");
            using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = class2.GetInstances().GetEnumerator())
            {
                ManagementObject current;
                Array array;
                while (enumerator.MoveNext())
                {
                    current = (ManagementObject) enumerator.Current;
                    if ((bool) current["IPEnabled"])
                    {
                        goto Label_004F;
                    }
                }
                goto Label_0089;
            Label_004F:
                array = (Array) current.Properties["IpAddress"].Value;
                str = array.GetValue(0).ToString();
            }
        Label_0089:
            objects = null;
            class2 = null;
            return str;
        }

        public static string GetLocalIP()
        {
            return GetIPAddress();
        }

        public static IPEndPoint GetLocalPoint(Socket socket)
        {
            return (IPEndPoint) socket.LocalEndPoint;
        }

        public static IPEndPoint GetLocalPoint(TcpListener tcpListener)
        {
            return (IPEndPoint) tcpListener.LocalEndpoint;
        }

        public static string GetLocalPoint_IP(Socket socket)
        {
            IPEndPoint localEndPoint = (IPEndPoint) socket.LocalEndPoint;
            return localEndPoint.Address.ToString();
        }

        public static string GetLocalPoint_IP(TcpListener tcpListener)
        {
            IPEndPoint localEndpoint = (IPEndPoint) tcpListener.LocalEndpoint;
            return localEndpoint.Address.ToString();
        }

        public static int GetLocalPoint_Port(Socket socket)
        {
            IPEndPoint localEndPoint = (IPEndPoint) socket.LocalEndPoint;
            return localEndPoint.Port;
        }

        public static int GetLocalPoint_Port(TcpListener tcpListener)
        {
            IPEndPoint localEndpoint = (IPEndPoint) tcpListener.LocalEndpoint;
            return localEndpoint.Port;
        }

        public static string GetMacAddress()
        {
            string str = "";
            ManagementClass class2 = new ManagementClass("Win32_NetworkAdapterConfiguration");
            using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = class2.GetInstances().GetEnumerator())
            {
                ManagementObject current;
                while (enumerator.MoveNext())
                {
                    current = (ManagementObject) enumerator.Current;
                    if ((bool) current["IPEnabled"])
                    {
                        goto Label_004F;
                    }
                }
                return str;
            Label_004F:
                str = current["MacAddress"].ToString();
            }
            return str;
        }

        public static EndPoint GetNetworkAddressEndPoing(IPHostEntry entry)
        {
            return new IPEndPoint(entry.AddressList[0], 0);
        }

        public IList<IPEndPoint> GetUsedIPEndPoint()
        {
            IPGlobalProperties iPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] activeTcpListeners = iPGlobalProperties.GetActiveTcpListeners();
            IPEndPoint[] activeUdpListeners = iPGlobalProperties.GetActiveUdpListeners();
            TcpConnectionInformation[] activeTcpConnections = iPGlobalProperties.GetActiveTcpConnections();
            IList<IPEndPoint> list = new List<IPEndPoint>();
            foreach (IPEndPoint point in activeTcpListeners)
            {
                list.Add(point);
            }
            foreach (IPEndPoint point in activeUdpListeners)
            {
                list.Add(point);
            }
            foreach (TcpConnectionInformation information in activeTcpConnections)
            {
                list.Add(information.LocalEndPoint);
            }
            return list;
        }

        public static string GetValidIP(string ip)
        {
            if (ValidateUtil.IsValidIP(ip))
            {
                return ip;
            }
            return "-1";
        }

        public static int GetValidPort(string port)
        {
            int num = -1;
            num = ConvertHelper.ConvertTo<int>(port);
            if ((num <= 0) || (num > 0xffff))
            {
                throw new ArgumentException("参数port端口号范围无效！");
            }
            return num;
        }

        [DllImport("wininet")]
        private static extern bool InternetGetConnectedState(out int int_0, int int_1);
        [DllImport("wininet", EntryPoint="InternetGetConnectedState", CharSet=CharSet.Auto)]
        private static extern bool InternetGetConnectedState_1(ref InternetConnectionStatesType internetConnectionStatesType_0, int int_0);
        public static bool IsConnectedInternet()
        {
            int num = 0;
            return InternetGetConnectedState(out num, 0);
        }

        public static bool IsHostAvailable(string host)
        {
            return (ResolveHost(host) != null);
        }

        public static bool IsOnline()
        {
            InternetConnectionStatesType currentState = CurrentState;
            return !smethod_0(0x20, (int) currentState);
        }

        public bool IsUsedIPEndPoint(int port)
        {
            foreach (IPEndPoint point in this.GetUsedIPEndPoint())
            {
                if (point.Port == port)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsUsedIPEndPoint(string ip, int port)
        {
            foreach (IPEndPoint point in this.GetUsedIPEndPoint())
            {
                if ((point.Address.ToString() == ip) && (point.Port == port))
                {
                    return true;
                }
            }
            return false;
        }

        public static string ReceiveMsg(Socket socket)
        {
            byte[] buffer = new byte[0x1388];
            int count = socket.Receive(buffer);
            byte[] dst = new byte[count];
            Buffer.BlockCopy(buffer, 0, dst, 0, count);
            return ConvertHelper.BytesToString(dst);
        }

        public static void ReceiveMsg(Socket socket, byte[] buffer)
        {
            socket.Receive(buffer);
        }

        public static IPHostEntry ResolveHost(string host)
        {
            IPHostEntry entry;
            try
            {
                entry = Dns.Resolve(host);
            }
            catch
            {
            }
            return entry;
        }

        public static void SendMsg(Socket socket, string msg)
        {
            byte[] buffer = ConvertHelper.StringToBytes(msg);
            socket.Send(buffer, buffer.Length, SocketFlags.None);
        }

        public static void SendMsg(Socket socket, byte[] msg)
        {
            socket.Send(msg, msg.Length, SocketFlags.None);
        }

        internal static bool smethod_0(int int_0, int int_1)
        {
            return ((int_0 & int_1) != 0);
        }

        public static void StartListen(Socket socket, int port)
        {
            IPEndPoint endPoint = CreateIPEndPoint(LocalHostName, port);
            BindEndPoint(socket, endPoint);
            socket.Listen(100);
        }

        public static void StartListen(Socket socket, int port, int maxConnection)
        {
            IPEndPoint endPoint = CreateIPEndPoint(LocalHostName, port);
            BindEndPoint(socket, endPoint);
            socket.Listen(maxConnection);
        }

        public static void StartListen(Socket socket, string ip, int port, int maxConnection)
        {
            BindEndPoint(socket, ip, port);
            socket.Listen(maxConnection);
        }

        public static IPAddress StringToIPAddress(string ip)
        {
            return IPAddress.Parse(ip);
        }

        public static InternetConnectionStatesType CurrentState
        {
            get
            {
                InternetConnectionStatesType type = 0;
                InternetGetConnectedState_1(ref type, 0);
                return type;
            }
        }

        public static string LANIP
        {
            get
            {
                IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
                if (addressList.Length < 1)
                {
                    return "";
                }
                return addressList[0].ToString();
            }
        }

        public static string LocalHostName
        {
            get
            {
                return Dns.GetHostName();
            }
        }

        public static string WANIP
        {
            get
            {
                IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
                if (addressList.Length < 2)
                {
                    return "";
                }
                return addressList[1].ToString();
            }
        }
    }
}

