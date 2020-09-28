using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Documents;

namespace ComProject
{
    public delegate void AnswerHandler(string text);
    class COMPort
    {        
        SerialPort port;
        WinCOM COM = new WinCOM();
        private byte[] byteBuffer = new byte[1024];
        public event AnswerHandler DataReceived;
        public event AnswerHandler ErrorReceived;
        public COMPort(string name)
        {       
            port = new SerialPort()
            {
                PortName = name,
                BaudRate = 9600,
                DataBits = 8,
                WriteTimeout = 50,
                ReadTimeout = 50,
                Parity = Parity.None,
                StopBits = StopBits.One,
                Handshake = Handshake.None,
                DtrEnable = false
            };
            port.ErrorReceived += new SerialErrorReceivedEventHandler(ErrorReceive);
        }
        public void ErrorReceive (object sender, SerialErrorReceivedEventArgs e)
        {
            string message = string.Concat
                (
                    (sender is SerialPort) ? (sender as SerialPort).PortName : "COM0",
                    " ERROR " + e.EventType.ToString()
                );
            Console.WriteLine(message);

            switch (e.EventType)
            {
                case SerialError.Frame:
                    message = "Ошибка кадрирования";
                    break;
                case SerialError.Overrun:
                    message = "Переполнение буфера символов";
                    break;
                case SerialError.RXOver:
                    message = "Переполнение входного буфера";
                    break;
                case SerialError.RXParity:
                    message = "Ошибка четности";
                    break;
                case SerialError.TXFull:
                    message = "Переполнение выходного буфера";
                    break;
            }
        }
        private void OnDataReceived(string data)
        {
            if (port.IsOpen)
            {
                //data = data.Replace('\0', '0');
                Console.WriteLine(port.PortName + ": " + data.ToString());

                DataReceived?.Invoke(data);
            }
        }
        public bool Connect()
        {
            if (port.IsOpen)
                return true;

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
            if (port.IsOpen)
            {
                port.DiscardInBuffer();
                ReadAsync();
                return true;
            }
            else
            {
                Console.WriteLine("ERROR: " + " MESSAGE: " + "Не могу открыть порт!");
            }
            return false;
        }
        public void ReadAsync()
        {
            try
            {
                port.BaseStream.BeginRead(byteBuffer, 0, byteBuffer.Length, OnRead, null);
            }
            catch (Exception ex)
            {
                ErrorReceived?.Invoke("ReadAsync");
                Console.WriteLine($"Не удалось получить ответ от {port.PortName}.\n{ex.Message}");
            }
        }
        private void OnRead(IAsyncResult result)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                try
                {
                    if (!port.IsOpen)
                        return;

                    int numRead = port.BaseStream.EndRead(result);

                    if (numRead > 0)
                        OnDataReceived(Encoding.ASCII.GetString(byteBuffer, 0, numRead));
                }
                catch (Exception ex)
                {
                    ErrorReceived?.Invoke("OnRead");
                    Console.WriteLine($"Не удалось получить ответ от {port.PortName}.\n{ex.Message}");
                }
            }));
            ReadAsync();
        }
        public void Send(string text)
        {
            try
            {
                if (!port.IsOpen)
                    Connect();

                if (port.IsOpen)
                {
                    port.Write(text);
                    Console.WriteLine(port.PortName + ": " + text);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Команда {text} на {port.PortName} не отправлена!\n{e.Message}");
            }           
        }
        public void Disсonnect()
        {            
            try
            {
                if (port.IsOpen)
                {
                    port.Close();
                }
                else
                {
                    string message = "Порт закрыт!\nДля начала подключитесь к порту!";
                    string caption = "Ошибка!";
                    MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (!port.IsOpen)
                {
                    Console.WriteLine("Disconnected");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString() + "MESSAGE  " + ex.Message);
            }
        }
    }
}