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
        private bool run;
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
        }
        public bool Connect()
        {
            run = false;
            try
            {
                port.Open();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Connected");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: " + ex.ToString() + "MESSAGE  " + ex.Message);
            }
            if (port.IsOpen)
                run = true;

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
        public void Send()
        {
            string datagramStepX;
            string datagramCoeffX;
            string datagramStepY;
            string datagramCoeffY;
            string datagramStepZ;
            string datagramCoeffZ;
            if (float.Parse(stepX.textBox.Text) == 10)
            {
                datagramStepX = "SSX000" + stepX.textBox.Text;
            }
            else
            {
                datagramStepX = "SSX0000" + stepX.textBox.Text;
            }
            if (float.Parse(stepY.textBox.Text) == 10)
            {
                datagramStepY = "SSY000" + stepY.textBox.Text;
            }
            else
            {
                datagramStepY = "SSY0000" + stepY.textBox.Text;
            }
            if (float.Parse(stepZ.textBox.Text) == 10)
            {
                datagramStepZ = "SSZ000" + stepZ.textBox.Text;
            }
            else
            {
                datagramStepZ = "SSZ0000" + stepZ.textBox.Text;
            }
            datagramCoeffX = "CX" + coeffX.textBox.Text;
            datagramCoeffY = "CY" + coeffY.textBox.Text;
            datagramCoeffZ = "CZ" + coeffZ.textBox.Text;
            if (datagramStepX == null || datagramCoeffX == null ||
                datagramStepY == null || datagramCoeffY == null ||
                datagramStepZ == null || datagramCoeffZ == null)
                return;

            if (!port.IsOpen)
                Connect();

            port.Write(datagramStepX);
            port.Write(datagramCoeffX);
            port.Write(datagramStepY);
            port.Write(datagramCoeffY);
            port.Write(datagramStepZ);
            port.Write(datagramCoeffZ);
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
                if (port.IsOpen)
                {
                    run = false;
                    Thread.Sleep(50);
                    port.Close();
                    Console.WriteLine("Disconnected");
                }
                else
                {
                    port.Open();
                    Console.WriteLine("Connected");
                    Thread.Sleep(50);
                    port.Close();
                    Console.WriteLine("Disconnected");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(port.PortName + "ERROR: " + ex.ToString() + "MESSAGE  " + ex.Message);
            }
        }
    }
}