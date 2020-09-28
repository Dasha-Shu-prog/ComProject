using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ComProject
{
    class CommandParsing
    {
        bool infoData = false;
        public delegate void Parsing(string[] information);
        public event Parsing ParsingCommand;
        string info;
        int counterInfo;
        string receive = "";
        public bool ParceString(string receivedMsg, Commands commandAxisX, Commands commandAxisY, Commands commandAxisZ)
        {
            int startindex = 0;
            receive = receive + receivedMsg;
            if (receive.Length >= 8)
            {
                string response = receive.Substring(startindex, 8);
                if (response.StartsWith("S")) //comand set
                {
                    response = response.Remove(0, 1);
                    //старт оси X
                    if (response.StartsWith("X")) // axis X
                    {
                        commandAxisX.setCommand = true;
                        response = response.Remove(0, 1);
                        if (response.StartsWith("S")) //SetXStep command 
                        {
                            response = response.Remove(0, 1);
                            commandAxisX.typeCommand = 'S';
                            receive = receive.Remove(0, 8);
                            return true;
                        }
                        else if (response.StartsWith("C")) //SetXCoeff command 
                        {
                            response = response.Remove(0, 1);
                            commandAxisX.typeCommand = 'C';
                            receive = receive.Remove(0, 8);
                            return true;
                        }
                        else // старт неизвестной команды 
                        {
                            int codeError;
                            if (Int32.TryParse(response, out codeError))
                            {
                                switch (codeError)
                                {
                                    case 0: break;
                                    case 1: break;
                                    case 2: break;
                                }
                                return false;
                            }
                            else
                            {
                                string message = "Неизвестная команда";
                                string capt = "Ошибка";
                                MessageBox.Show(message, capt, MessageBoxButton.OK, MessageBoxImage.Error);
                                return false;
                            }
                        }
                    }
                    //старт оси Y
                    else if (response.StartsWith("Y"))// axis Y
                    {
                        commandAxisY.setCommand = true;
                        response = response.Remove(0, 1);

                        if (response.StartsWith("S")) //SetYStep command 
                        {
                            response = response.Remove(0, 1);
                            commandAxisY.typeCommand = 'S';
                            receive = receive.Remove(0, 8);
                            return true;
                        }
                        else if (response.StartsWith("C")) //SetYCoeff command 
                        {
                            response = response.Remove(0, 1);
                            commandAxisY.typeCommand = 'C';
                            receive = receive.Remove(0, 8);
                            return true;
                        }
                        else
                        {
                            string message = "Неизвестная команда";
                            string capt = "Ошибка";
                            MessageBox.Show(message, capt, MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                    }
                    //старт оси Z
                    else if (response.StartsWith("Z"))
                    {
                        commandAxisZ.setCommand = true;
                        response = response.Remove(0, 1);

                        if (response.StartsWith("S")) //SetZStep command 
                        {
                            response = response.Remove(0, 1);
                            commandAxisZ.typeCommand = 'S';
                            receive = receive.Remove(0, 8);
                            return true;

                        }
                        else if (response.StartsWith("C")) //SetZCoeff command
                        {
                            response = response.Remove(0, 1);
                            commandAxisZ.typeCommand = 'C';
                            receive = receive.Remove(0, 8);
                            return true;
                        }
                        else
                        {
                            string message = "Неизвестная команда";
                            string capt = "Ошибка";
                            MessageBox.Show(message, capt, MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                    }
                    else
                    {
                        int codeError;
                        if (Int32.TryParse(response, out codeError))
                        {
                            switch (codeError)
                            {
                                case 0: 
                                    receive = receive.Remove(0, 8); break;//S0000000 Ответ от БУ, что установка остановилась
                                case 1:
                                    string message = "Отправлена неправильная команда в БУ";
                                    string capt = "Ошибка";
                                    MessageBox.Show(message, capt, MessageBoxButton.OK, MessageBoxImage.Error); break;
                                case 2: break;
                            }
                            return false;
                        }
                        else
                        {
                            string message = "Неизвестная команда";
                            string capt = "Ошибка";
                            MessageBox.Show(message, capt, MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                    }

                }                
                else if (response.StartsWith("I") || infoData == true || receive.StartsWith("I")) //information
                {
                    infoData = true;
                    // if (counterInfoPack < 200) 
                    {
                        int indexOfChar;
                        if (receive.IndexOf('!') > 1)
                        {
                            counterInfo = 0;
                            indexOfChar = receive.IndexOf('!');
                            response = receive.Substring(0, indexOfChar);

                            string[] info = new string[8];
                            response = response.Remove(0, 1);
                            info = response.Split(',');
                            infoData = false;
                            ParsingCommand?.Invoke(info);
                            receive = receive.Remove(0, indexOfChar + 1);
                            return false;
                        }
                        else
                        {
                            info = info + response;
                            counterInfo++;
                        }
                    }
                }
                else
                {
                    if (infoData == false)
                    {
                        string message = "Неизвестный ответ";
                        string capt = "Ошибка";
                        MessageBox.Show(message, capt, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    receive = "";
                    receivedMsg = "";
                    return false;
                }

                receivedMsg = "";
            }
            return false;
        }
    }
}
