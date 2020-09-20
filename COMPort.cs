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
            //string zero = "0000";
            //if (Int32.Parse(stepX.textBox.Text) < 10)
            //{

            //}
            string datagramStepX = "SSX+000" + stepX.textBox.Text;
            string datagramCoeffX = "SCX+000" + coeffX.textBox.Text;
            string datagramStepY = "SSY+000" + stepY.textBox.Text;
            string datagramCoeffY = "SCY+000" + coeffY.textBox.Text;
            string datagramStepZ = "SSZ+000" + stepZ.textBox.Text;
            string datagramCoeffZ = "SCZ+000" + coeffZ.textBox.Text;
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
            Console.WriteLine(datagramCoeffX);
            Console.WriteLine(datagramStepY);
            Console.WriteLine(datagramCoeffY);
            Console.WriteLine(datagramStepZ);
            Console.WriteLine(datagramCoeffZ);
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
                    Console.WriteLine("Disconnected");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: " + ex.ToString() + "MESSAGE  " + ex.Message);
            }
        }
    }
}
