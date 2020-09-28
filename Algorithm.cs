using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ComProject
{
    //Работа с СОМ портом
    class Algorithm
    {
        // оси X, Y, Z
        Commands commandAxisX = new Commands();
        Commands commandAxisY = new Commands();
        Commands commandAxisZ = new Commands();
        Info information = new Info();
        COMPort port;
        CommandParsing parsing;
        bool stopAxisX = false;
        bool stopAxisY = false;
        bool stopAxisZ = false;
        bool start = false;
        bool stop = false;
        int timeoutStartAxisX = 0;// миллисекунды
        int timeoutStartAxisY = 0;// миллисекунды
        int timeoutStartAxisZ = 0;// миллисекунды

        bool timeoutAxisX = false;
        bool timeoutAxisY = false;
        bool timeoutAxisZ = false;

        const int timeoutStart = 2000; // миллисекунды

        const int maxAxisX = 150; //максимальный координата оси X
        const int maxAxisY = 150; //максимальный координата оси Y
        const int maxAxisZ = 150; //максимальная координата оси Z

        bool askInfo = false;

        public delegate void AlgoritmHandler(Info information);
        public event AlgoritmHandler NewInformation;

        public delegate void AlgoritmHandlerStop(bool stop);
        public event AlgoritmHandlerStop StopInformation;

        DispatcherTimer timerTimeoutCommandsStart = new DispatcherTimer();
        private readonly BackgroundWorker workerInfo = new BackgroundWorker();
        public Algorithm(COMPort port)
        {
            parsing = new CommandParsing();
            parsing.ParsingCommand += Parsing_Command;

            timerTimeoutCommandsStart.Interval = TimeSpan.FromMilliseconds(10);
            timerTimeoutCommandsStart.Tick += timerTimeoutCommandsStart_Tick;
            timerTimeoutCommandsStart.Start();
        }
        public bool startWorkerCommands(string cmd)
        {
            askInfo = true;
            if (!workerInfo.IsBusy)
            {
                workerInfo.RunWorkerAsync(cmd);
                return true;
            }
            return false;
        }
        private void timerTimeoutCommandsStart_Tick(object sender, EventArgs e)
        {
            if (timeoutAxisX == true)
            {
                timeoutStartAxisX++;
                if (timeoutStartAxisX >= timeoutStart)
                {
                    timeoutAxisX = false;
                    timeoutStartAxisX = 0;
                }
            }
            if (timeoutAxisY == true)
            {
                timeoutStartAxisY++;
                if (timeoutStartAxisY >= timeoutStart)
                {
                    timeoutAxisY = false;
                    timeoutStartAxisY = 0;
                }
            }
            if (timeoutAxisZ == true)
            {
                timeoutStartAxisZ++;
                if (timeoutStartAxisZ >= timeoutStart)
                {
                    timeoutAxisZ = false;
                    timeoutStartAxisZ = 0;
                }
            }
        }
        public void SetStart(bool startIn)
        {
            start = startIn;
        }
        public void Parsing_Command(string[] info)
        {
            int x0 = 0;
            int x1 = 0;
            int y0 = 0;
            int y1 = 0;
            int z0 = 0;
            int z1 = 0;
        }
        public bool SetCommands(string cmdAxisX, string cmdAxisY, string cmdAxisZ, bool cmdX, bool cmdY, bool cmdZ)
        {

            if (cmdX) commandAxisX.command = cmdAxisX;
            if (cmdY) commandAxisY.command = cmdAxisY;
            if (cmdZ) commandAxisZ.command = cmdAxisZ;
            return true;
        }
        public bool SetCommandsRange(string cmd, char axis)
        {
            if (axis == 'X')
            {
                commandAxisX.nextCommand = cmd;
                commandAxisX.twoCommands = true;
            }
            if (axis == 'Y')
            {
                commandAxisY.nextCommand = cmd;
                commandAxisY.twoCommands = true;
            }
            if (axis == 'Z')
            {
                commandAxisZ.nextCommand = cmd;
                commandAxisZ.twoCommands = true;
            }
            return true;
        }       
        public bool ManualPosition()
        {
            //Control sensors switch position
            if (commandAxisX.setCommand == false)
            {
                if ((commandAxisX.command == "SXS" && information.S0 == true) || (commandAxisX.command == "SXC00000" && information.S1 == true))
                {
                    if ((information.S0 == true || information.S1 == true) && information.C0 == true && information.C1 == true && information.C2 == true) // check if centered and one of sensorses is used
                    {
                        if ((commandAxisZ.setCommand == false) && (commandAxisZ.command != "") && (timeoutAxisZ == false))
                        {
                            while (startWorkerCommands(commandAxisZ.command) == false)
                            {
                                commandAxisZ.sendCommand = false;
                            }
                            {
                                commandAxisZ.sendCommand = true;
                                timeoutAxisZ = true;
                            }
                        }
                        if ((commandAxisY.setCommand == false) && (commandAxisY.command != "") && (timeoutAxisY == false))
                        {
                            while (startWorkerCommands(commandAxisY.command) == false)
                            {
                                commandAxisY.sendCommand = false;
                            }
                            {
                                commandAxisY.sendCommand = true;
                                timeoutAxisY = true;
                            }
                        }
                    }
                    if ((commandAxisX.command == "SXS00000" && information.S1 == false) || (commandAxisX.command == "SXC00000" && information.S0 == false))
                    {
                        while (startWorkerCommands(commandAxisX.command) == false)
                        {
                            commandAxisX.sendCommand = false;
                        }
                        {
                            commandAxisX.sendCommand = true;
                            timeoutAxisX = true;
                        }
                    }
                }
            }
          return true;
        }
        public void CheckSet()
        {
            if (commandAxisX.sendCommand != true)
            {
                commandAxisX.command = "";
                commandAxisX.errorCode = 0;
                commandAxisX.newCommand = false;
                commandAxisX.sendCommand = false;
                commandAxisX.setCommand = false;
                commandAxisX.typeCommand = ' ';
                commandAxisX.value = 0;
                timeoutAxisX = false;
                stopAxisX = true;
                if (commandAxisX.twoCommands == true)
                {
                    commandAxisX.command = commandAxisX.nextCommand;
                    commandAxisX.twoCommands = false;
                    commandAxisX.nextCommand = "";
                    stopAxisX = false;
                }
            }
            if (commandAxisY.sendCommand != true)
            {
                commandAxisY.command = "";
                commandAxisY.errorCode = 0;
                commandAxisY.newCommand = false;
                commandAxisY.sendCommand = false;
                commandAxisY.setCommand = false;
                commandAxisY.typeCommand = ' ';
                commandAxisY.value = 0;
                timeoutAxisY = false;
                stopAxisY = true;
                if (commandAxisY.twoCommands == true)
                {
                    commandAxisY.command = commandAxisY.nextCommand;
                    commandAxisY.twoCommands = false;
                    commandAxisY.nextCommand = "";
                    stopAxisY = false;
                }
            }
            if (commandAxisZ.sendCommand != true)
            {
                commandAxisZ.command = "";
                commandAxisZ.errorCode = 0;
                commandAxisZ.newCommand = false;
                commandAxisZ.sendCommand = false;
                commandAxisZ.setCommand = false;
                commandAxisZ.typeCommand = ' ';
                commandAxisZ.value = 0;
                timeoutAxisZ = false;
                stopAxisZ = true;
                if (commandAxisZ.twoCommands == true)
                {
                    commandAxisZ.command = commandAxisZ.nextCommand;
                    commandAxisZ.twoCommands = false;
                    commandAxisZ.nextCommand = "";
                    stopAxisZ = false;
                }
            }
        }
    }
}
