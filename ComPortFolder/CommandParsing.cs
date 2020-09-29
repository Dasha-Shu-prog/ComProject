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
                if (response.StartsWith("S")) //comand start
                {
                    response = response.Remove(0, 1);
                    //старт оси X
                    if (response.StartsWith("X")) // axis X
                    {
                        commandAxisX.startCommand = true;
                        response = response.Remove(0, 1);
                        if (response.StartsWith("S")) //StartXStep command 
                        {
                            response = response.Remove(0, 1);
                            commandAxisX.typeCommand = 'S';
                            receive = receive.Remove(0, 8);
                            return true;
                        }
                        else if (response.StartsWith("C")) //StartXCoeff command 
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
                        commandAxisY.startCommand = true;
                        response = response.Remove(0, 1);

                        if (response.StartsWith("S")) //StartYStep command 
                        {
                            response = response.Remove(0, 1);
                            commandAxisY.typeCommand = 'S';
                            receive = receive.Remove(0, 8);
                            return true;
                        }
                        else if (response.StartsWith("C")) //StartYCoeff command 
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
                        commandAxisZ.startCommand = true;
                        response = response.Remove(0, 1);

                        if (response.StartsWith("S")) //StartZStep command 
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
                else if (response.StartsWith("E")) //comand end
                {
                    response = response.Remove(0, 1);
                    //старт оси X
                    if (response.StartsWith("X"))
                    {
                        commandAxisX.endCommand = true;
                        response = response.Remove(0, 1);
                        if (response.StartsWith("S")) //EndXStep command 
                        {
                            response = response.Remove(0, 1);
                            commandAxisX.typeCommand = 'S';
                            receive = receive.Remove(0, 8);
                            return true;
                        }
                        else if (response.StartsWith("C")) //EndXCoeff command 
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
                    else if (response.StartsWith("Y"))
                    {
                        commandAxisY.endCommand = true;
                        response = response.Remove(0, 1);

                        if (response.StartsWith("S")) //EndYStep command 
                        {
                            response = response.Remove(0, 1);
                            commandAxisY.typeCommand = 'S';
                            receive = receive.Remove(0, 8);
                            return true;
                        }
                        else if (response.StartsWith("C")) //EndYCoeff command 
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
                        commandAxisZ.endCommand = true;
                        response = response.Remove(0, 1);

                        if (response.StartsWith("S")) //EndZStep command 
                        {
                            response = response.Remove(0, 1);
                            commandAxisZ.typeCommand = 'S';
                            receive = receive.Remove(0, 8);
                            return true;

                        }
                        else if (response.StartsWith("C")) //EndZCoeff command
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
                }
                else if (response.StartsWith("I") || infoData == true || receive.StartsWith("I")) //information
                {
                    infoData = true;
                    // if (counterInfoPack < 200) 
                    {
                        int indexOfChar;
                        if (receive.IndexOf('S') > 1)
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
                            info += response;
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
