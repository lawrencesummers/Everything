namespace WHC.OrderWater.Commons
{
    using System;

    public class DataReceivedEventArgs : EventArgs
    {
        public string DataReceived;

        public DataReceivedEventArgs(string m_DataReceived)
        {
            this.DataReceived = m_DataReceived;
        }
    }
}

