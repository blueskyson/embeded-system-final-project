using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace AudioPlayerApp
{
    public class Uart
    {
        private SerialPort serial_port;
        private string port_name = "COM4";
        private int baud_rate = 9600;

        private string serial_buffer = "";

        public Uart() { }

        public Uart(string PortName, int BaudRate)
        {
            port_name = PortName;
            baud_rate = BaudRate;
        }

        public void OpenSerial()
        {
            serial_port = new SerialPort(port_name, baud_rate);

            try
            {
                serial_port.Open();
                if (!serial_port.IsOpen)
                {
                    Console.WriteLine("Fail to open " + port_name);
                    return;
                }
                else
                {
                    Console.WriteLine("Success to open " + port_name);
                }
            }
            catch (Exception e)
            {
                serial_port.Dispose();
                Console.WriteLine(e.Message);
            }
        }

        public void CloseSerial()
        {
            serial_buffer = "";
            serial_port.Dispose();
            Console.WriteLine("Close port: " + port_name);
        }

        public string ReadLines()
        {
            try
            {
                string s = serial_port.ReadExisting();
                string buffer = "";
                foreach (char c in s)
                {
                    buffer += c;
                    if (c == '\n')
                    {
                        serial_buffer += buffer;
                        buffer = "";
                    }
                }
                string ret = serial_buffer;
                serial_buffer = buffer;
                if (ret.Length > 0 && ret[ret.Length - 1] == '\n')
                    return ret;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
            return "";
        }

        public void Send(string s)
        {
            try
            {
                serial_port.Write(s);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ClearBuffer()
        {
            serial_port.DiscardInBuffer();
        }
    }
}
