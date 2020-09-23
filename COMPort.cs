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
    class COMPort : MainWindow
    {        
        SerialPort port;
        private string data = "";
        private bool start = false;
        private bool parse = false;
        WinCOM COM = new WinCOM();
        //private byte[] byteBuffer = new byte[1024];
        public COMPort(string name)
        {       
            port = new SerialPort()
            {
                PortName = name,
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
            port.ReadTimeout = 500;
            if (port.IsOpen)
            {
                SerialPort serialPort = (SerialPort)sender;
                int buffer = serialPort.BytesToRead;
                for (int i = 0; i < buffer; ++i)
                {
                    char bytes = (char)serialPort.ReadByte();
                    if (bytes == '{' && start == false)
                    {
                        start = true; 
                        data = "";
                    }                            
                    if (start == true)
                        data += bytes.ToString();

                    if (bytes == '}' && start == true)
                    {
                        start = false; 
                        parse = true;
                    }
                            
                    if (parse == true)
                    {
                        parse = false;
                        Console.WriteLine("Microcontroller " + data.ToString());
                    }
                }
            }
        }
        public bool Connect()
        {
            try
            {
                port.Open();
                if (port.IsOpen)
                {
                    Console.WriteLine("Connected");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString() + "MESSAGE  " + ex.Message);
            }
            return true;
        }
        public void Send()
        {
            port.WriteTimeout = 500;
            string datagramStepX;
            string datagramCoeffX;
            string datagramStepY;
            string datagramCoeffY;
            string datagramStepZ;
            string datagramCoeffZ;
            if (coordCurrentStepX == 10)
            {
                datagramStepX = "SSX000" + coordCurrentStepX.ToString();
            }
            else
            {
                datagramStepX = "SSX0000" + coordCurrentStepX;
            }
            if (coordCurrentStepY == 10)
            {
                datagramStepY = "SSY000" +coordCurrentStepY;
            }
            else
            {
                datagramStepY = "SSY0000" + coordCurrentStepY;
            }
            if (coordCurrentStepZ == 10)
            {
                datagramStepZ = "SSZ000" + coordCurrentStepZ;
            }
            else
            {
                datagramStepZ = "SSZ0000" + coordCurrentStepZ;
            }
            datagramCoeffX = "CX" + coordCurrentCoeffX;
            datagramCoeffY = "CY" + coordCurrentCoeffY;
            datagramCoeffZ = "CZ" + coordCurrentCoeffZ;
            if (datagramStepX == null || datagramCoeffX == null ||
                datagramStepY == null || datagramCoeffY == null ||
                datagramStepZ == null || datagramCoeffZ == null)
                return;

            if (!port.IsOpen)
                Connect();

            port.Write(datagramStepX);
            //port.Write(datagramCoeffX);
            //port.Write(datagramStepY);
            //port.Write(datagramCoeffY);
            //port.Write(datagramStepZ);
            //port.Write(datagramCoeffZ);
            Console.WriteLine(datagramStepX);
            Console.WriteLine('S' + datagramCoeffX);
            Console.WriteLine(datagramStepY);
            Console.WriteLine('S' + datagramCoeffY);
            Console.WriteLine(datagramStepZ);
            Console.WriteLine('S' + datagramCoeffZ);
        }
        public void DisConnect()
        {            
            try
            {
                port.Close();
                Console.WriteLine("Disconnected");
            }
            catch (Exception ex)
            {
                Console.WriteLine(port.PortName + "ERROR: " + ex.ToString() + "MESSAGE  " + ex.Message);
            }
        }
    }
}