namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections.Generic;
    using System.IO.Ports;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    public class SerialPortUtil
    {
        private DataReceivedEventHandler dataReceivedEventHandler_0;
        public byte EndByte;
        private System.IO.Ports.Parity parity_0;
        public bool ReceiveEventFlag;
        private SerialErrorReceivedEventHandler serialErrorReceivedEventHandler_0;
        private SerialPort serialPort_0;
        private SerialPortBaudRates serialPortBaudRates_0;
        private SerialPortDatabits serialPortDatabits_0;
        private System.IO.Ports.StopBits stopBits_0;
        private string string_0;

        public event DataReceivedEventHandler DataReceived
        {
            add
            {
                DataReceivedEventHandler handler2;
                DataReceivedEventHandler handler = this.dataReceivedEventHandler_0;
                do
                {
                    handler2 = handler;
                    DataReceivedEventHandler handler3 = (DataReceivedEventHandler) Delegate.Combine(handler2, value);
                    handler = Interlocked.CompareExchange<DataReceivedEventHandler>(ref this.dataReceivedEventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
            remove
            {
                DataReceivedEventHandler handler2;
                DataReceivedEventHandler handler = this.dataReceivedEventHandler_0;
                do
                {
                    handler2 = handler;
                    DataReceivedEventHandler handler3 = (DataReceivedEventHandler) Delegate.Remove(handler2, value);
                    handler = Interlocked.CompareExchange<DataReceivedEventHandler>(ref this.dataReceivedEventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
        }

        public event SerialErrorReceivedEventHandler Error
        {
            add
            {
                SerialErrorReceivedEventHandler handler2;
                SerialErrorReceivedEventHandler handler = this.serialErrorReceivedEventHandler_0;
                do
                {
                    handler2 = handler;
                    SerialErrorReceivedEventHandler handler3 = (SerialErrorReceivedEventHandler) Delegate.Combine(handler2, value);
                    handler = Interlocked.CompareExchange<SerialErrorReceivedEventHandler>(ref this.serialErrorReceivedEventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
            remove
            {
                SerialErrorReceivedEventHandler handler2;
                SerialErrorReceivedEventHandler handler = this.serialErrorReceivedEventHandler_0;
                do
                {
                    handler2 = handler;
                    SerialErrorReceivedEventHandler handler3 = (SerialErrorReceivedEventHandler) Delegate.Remove(handler2, value);
                    handler = Interlocked.CompareExchange<SerialErrorReceivedEventHandler>(ref this.serialErrorReceivedEventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
        }

        public SerialPortUtil()
        {
            this.ReceiveEventFlag = false;
            this.EndByte = 0x23;
            this.string_0 = "COM1";
            this.serialPortBaudRates_0 = SerialPortBaudRates.BaudRate_57600;
            this.parity_0 = System.IO.Ports.Parity.None;
            this.stopBits_0 = System.IO.Ports.StopBits.One;
            this.serialPortDatabits_0 = SerialPortDatabits.EightBits;
            this.serialPort_0 = new SerialPort();
            this.string_0 = "COM1";
            this.serialPortBaudRates_0 = SerialPortBaudRates.BaudRate_9600;
            this.parity_0 = System.IO.Ports.Parity.None;
            this.serialPortDatabits_0 = SerialPortDatabits.EightBits;
            this.stopBits_0 = System.IO.Ports.StopBits.One;
            this.serialPort_0.DataReceived += new SerialDataReceivedEventHandler(this.serialPort_0_DataReceived);
            this.serialPort_0.ErrorReceived += new SerialErrorReceivedEventHandler(this.serialPort_0_ErrorReceived);
        }

        public SerialPortUtil(string name, string baud, string par, string dBits, string sBits)
        {
            this.ReceiveEventFlag = false;
            this.EndByte = 0x23;
            this.string_0 = "COM1";
            this.serialPortBaudRates_0 = SerialPortBaudRates.BaudRate_57600;
            this.parity_0 = System.IO.Ports.Parity.None;
            this.stopBits_0 = System.IO.Ports.StopBits.One;
            this.serialPortDatabits_0 = SerialPortDatabits.EightBits;
            this.serialPort_0 = new SerialPort();
            this.string_0 = name;
            this.serialPortBaudRates_0 = (SerialPortBaudRates) Enum.Parse(typeof(SerialPortBaudRates), baud);
            this.parity_0 = (System.IO.Ports.Parity) Enum.Parse(typeof(System.IO.Ports.Parity), par);
            this.serialPortDatabits_0 = (SerialPortDatabits) Enum.Parse(typeof(SerialPortDatabits), dBits);
            this.stopBits_0 = (System.IO.Ports.StopBits) Enum.Parse(typeof(System.IO.Ports.StopBits), sBits);
            this.serialPort_0.DataReceived += new SerialDataReceivedEventHandler(this.serialPort_0_DataReceived);
            this.serialPort_0.ErrorReceived += new SerialErrorReceivedEventHandler(this.serialPort_0_ErrorReceived);
        }

        public SerialPortUtil(string name, SerialPortBaudRates baud, System.IO.Ports.Parity par, SerialPortDatabits dBits, System.IO.Ports.StopBits sBits)
        {
            this.ReceiveEventFlag = false;
            this.EndByte = 0x23;
            this.string_0 = "COM1";
            this.serialPortBaudRates_0 = SerialPortBaudRates.BaudRate_57600;
            this.parity_0 = System.IO.Ports.Parity.None;
            this.stopBits_0 = System.IO.Ports.StopBits.One;
            this.serialPortDatabits_0 = SerialPortDatabits.EightBits;
            this.serialPort_0 = new SerialPort();
            this.string_0 = name;
            this.serialPortBaudRates_0 = baud;
            this.parity_0 = par;
            this.serialPortDatabits_0 = dBits;
            this.stopBits_0 = sBits;
            this.serialPort_0.DataReceived += new SerialDataReceivedEventHandler(this.serialPort_0_DataReceived);
            this.serialPort_0.ErrorReceived += new SerialErrorReceivedEventHandler(this.serialPort_0_ErrorReceived);
        }

        public static string ByteToHex(byte[] comByte)
        {
            StringBuilder builder = new StringBuilder(comByte.Length * 3);
            foreach (byte num2 in comByte)
            {
                builder.Append(Convert.ToString(num2, 0x10).PadLeft(2, '0').PadRight(3, ' '));
            }
            return builder.ToString().ToUpper();
        }

        public void ClosePort()
        {
            if (this.serialPort_0.IsOpen)
            {
                this.serialPort_0.Close();
            }
        }

        public void DiscardBuffer()
        {
            this.serialPort_0.DiscardInBuffer();
            this.serialPort_0.DiscardOutBuffer();
        }

        public static bool Exists(string port_name)
        {
            foreach (string str in SerialPort.GetPortNames())
            {
                if (str == port_name)
                {
                    return true;
                }
            }
            return false;
        }

        public static string Format(SerialPort port)
        {
            return string.Format("{0} ({1},{2},{3},{4},{5})", new object[] { port.PortName, port.BaudRate, port.DataBits, port.StopBits, port.Parity, port.Handshake });
        }

        public static string[] GetPortNames()
        {
            return SerialPort.GetPortNames();
        }

        public static byte[] HexToByte(string msg)
        {
            msg = msg.Replace(" ", "");
            byte[] buffer = new byte[msg.Length / 2];
            for (int i = 0; i < msg.Length; i += 2)
            {
                buffer[i / 2] = Convert.ToByte(msg.Substring(i, 2), 0x10);
            }
            return buffer;
        }

        public void OpenPort()
        {
            if (this.serialPort_0.IsOpen)
            {
                this.serialPort_0.Close();
            }
            this.serialPort_0.PortName = this.string_0;
            this.serialPort_0.BaudRate = (int) this.serialPortBaudRates_0;
            this.serialPort_0.Parity = this.parity_0;
            this.serialPort_0.DataBits = (int) this.serialPortDatabits_0;
            this.serialPort_0.StopBits = this.stopBits_0;
            this.serialPort_0.Open();
        }

        public int SendCommand(byte[] SendData, ref byte[] ReceiveData, int Overtime)
        {
            if (!this.serialPort_0.IsOpen)
            {
                this.serialPort_0.Open();
            }
            this.ReceiveEventFlag = true;
            this.serialPort_0.DiscardInBuffer();
            this.serialPort_0.Write(SendData, 0, SendData.Length);
            int num2 = 0;
            int num = 0;
            while (num2++ < Overtime)
            {
                if (this.serialPort_0.BytesToRead >= ReceiveData.Length)
                {
                    break;
                }
                Thread.Sleep(1);
            }
            if (this.serialPort_0.BytesToRead >= ReceiveData.Length)
            {
                num = this.serialPort_0.Read(ReceiveData, 0, ReceiveData.Length);
            }
            this.ReceiveEventFlag = false;
            return num;
        }

        private void serialPort_0_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // This item is obfuscated and can not be translated.
            if (this.ReceiveEventFlag)
            {
                return;
            }
            List<byte> list = new List<byte>();
            bool flag = false;
            while (this.serialPort_0.BytesToRead > 0)
            {
                if (1 == 0)
                {
                    string str = Encoding.Default.GetString(list.ToArray(), 0, list.Count);
                    if (this.dataReceivedEventHandler_0 != null)
                    {
                        this.dataReceivedEventHandler_0(new DataReceivedEventArgs(str));
                    }
                    return;
                }
                byte[] buffer = new byte[this.serialPort_0.ReadBufferSize + 1];
                int num = this.serialPort_0.Read(buffer, 0, this.serialPort_0.ReadBufferSize);
                for (int i = 0; i < num; i++)
                {
                    list.Add(buffer[i]);
                    if (buffer[i] == this.EndByte)
                    {
                        flag = true;
                    }
                }
            }
            goto Label_0019;
        }

        private void serialPort_0_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            if (this.serialErrorReceivedEventHandler_0 != null)
            {
                this.serialErrorReceivedEventHandler_0(sender, e);
            }
        }

        public static void SetBauRateValues(ComboBox obj)
        {
            obj.Items.Clear();
            foreach (SerialPortBaudRates rates in Enum.GetValues(typeof(SerialPortBaudRates)))
            {
                obj.Items.Add(((int) rates).ToString());
            }
        }

        public static void SetDataBitsValues(ComboBox obj)
        {
            obj.Items.Clear();
            foreach (SerialPortDatabits databits in Enum.GetValues(typeof(SerialPortDatabits)))
            {
                obj.Items.Add(((int) databits).ToString());
            }
        }

        public static void SetParityValues(ComboBox obj)
        {
            obj.Items.Clear();
            foreach (string str in Enum.GetNames(typeof(System.IO.Ports.Parity)))
            {
                obj.Items.Add(str);
            }
        }

        public static void SetPortNameValues(ComboBox obj)
        {
            obj.Items.Clear();
            foreach (string str in SerialPort.GetPortNames())
            {
                obj.Items.Add(str);
            }
        }

        public static void SetStopBitValues(ComboBox obj)
        {
            obj.Items.Clear();
            foreach (string str in Enum.GetNames(typeof(System.IO.Ports.StopBits)))
            {
                obj.Items.Add(str);
            }
        }

        public void WriteData(byte[] msg)
        {
            if (!this.serialPort_0.IsOpen)
            {
                this.serialPort_0.Open();
            }
            this.serialPort_0.Write(msg, 0, msg.Length);
        }

        public void WriteData(string msg)
        {
            if (!this.serialPort_0.IsOpen)
            {
                this.serialPort_0.Open();
            }
            this.serialPort_0.Write(msg);
        }

        public void WriteData(byte[] msg, int offset, int count)
        {
            if (!this.serialPort_0.IsOpen)
            {
                this.serialPort_0.Open();
            }
            this.serialPort_0.Write(msg, offset, count);
        }

        public SerialPortBaudRates BaudRate
        {
            get
            {
                return this.serialPortBaudRates_0;
            }
            set
            {
                this.serialPortBaudRates_0 = value;
            }
        }

        public SerialPortDatabits DataBits
        {
            get
            {
                return this.serialPortDatabits_0;
            }
            set
            {
                this.serialPortDatabits_0 = value;
            }
        }

        public bool IsOpen
        {
            get
            {
                return this.serialPort_0.IsOpen;
            }
        }

        public System.IO.Ports.Parity Parity
        {
            get
            {
                return this.parity_0;
            }
            set
            {
                this.parity_0 = value;
            }
        }

        public string PortName
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public System.IO.Ports.StopBits StopBits
        {
            get
            {
                return this.stopBits_0;
            }
            set
            {
                this.stopBits_0 = value;
            }
        }
    }
}

