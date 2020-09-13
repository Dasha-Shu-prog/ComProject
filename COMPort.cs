using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;

namespace ComProject
{
    class COMPort
    {
        SerialPort port;
        private bool run;
        private int loc;
        private string data = "";
        private bool start = false;
        private bool parse = false;
        //private byte[] byteBuffer = new byte[1024];
        public COMPort()
        {
            port = new SerialPort()
            {
                PortName = "COM1",
                BaudRate = 115200,
                DataBits = 8,
                WriteTimeout = 50,
                ReadTimeout = 50,
                Parity = Parity.None,
                StopBits = StopBits.One,
                Handshake = Handshake.None
            };
            port.DataReceived += new SerialDataReceivedEventHandler(Receive);
        }
        public void Receive (object sender, SerialDataReceivedEventArgs e)
        {
            if (run == true)
            {
                if (port.IsOpen)
                {
                    SerialPort serialPort = (SerialPort)sender;
                    int buffer = serialPort.BytesToRead;
                    for (int i = 0; i < buffer; ++i)
                    {
                        char bytes = (char)serialPort.ReadByte();
                        if (bytes == '{' && start == false)
                            start = true; data = "";

                        if (start == true)
                            data += bytes.ToString();

                        if (bytes == '}' && start == true) 
                            start = false; parse = true;
                        if (parse == true)
                        {
                            parse = false;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Microcontroller " + data.ToString());
                        }
                    }
                }
            }
        }
        public bool Connect(string Name, int Speed)
        {
            run = false;
            try
            {
                port.Open();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: " + ex.ToString() + "MESSAGE  " + ex.Message);
            }
            if (port.IsOpen)
            {
                run = true;
            }
            else
            {
                string capt = "Ошибка";
                string msgText = "Порт закрыт!";
                MessageBox.Show(msgText, capt, MessageBoxButton.OK, 
                    MessageBoxImage.Error, MessageBoxResult.OK,
                    MessageBoxOptions.DefaultDesktopOnly);
            }
            return run;
        }
        public void Send(string datagram, int request)
        {
            loc = request;
            port.Write(datagram.ToString());
            Console.WriteLine("Microcontroller " + datagram.ToString());
        }
        public void DisConnect()
        {
            try
            {
                if (port.IsOpen)
                {
                    run = false;
                    Thread.Sleep(500);
                    port.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString() + "MESSAGE  " + ex.Message);
            }
        }
    }
}
