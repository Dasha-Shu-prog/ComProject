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
    class Algorithm : MainWindow
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
        int setmode;
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
            timerTimeoutCommandsStart.Interval = TimeSpan.FromMilliseconds(10);
            timerTimeoutCommandsStart.Tick += TimerTimeoutCommandsStart_Tick;
            timerTimeoutCommandsStart.Start();
            workerInfo.DoWork += Worker_DoWorkAskInfo;
        }
        private void Worker_DoWorkAskInfo(object sender, DoWorkEventArgs e)
        {
            port.Send((string)e.Argument);
        }
        public bool StartWorkerCommands(string cmd)
        {
            askInfo = true;
            if (!workerInfo.IsBusy)
            {
                workerInfo.RunWorkerAsync(cmd);
                return true;
            }
            return false;
        }
        private void TimerTimeoutCommandsStart_Tick(object sender, EventArgs e)
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
        public Info GetInfo()
        {
            return information;
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
        private void SetStepOrCoeff(int mode)
        {
            setmode = mode;
            if (setmode == 0) // set step
            {
                commandAxisX.command = setXStep;
                commandAxisY.command = setYStep;
                commandAxisZ.command = setZStep;
            }
            if (setmode == 1) // set coeff
            {
                commandAxisX.command = setXCoeff;
                commandAxisY.command = setYCoeff;
                commandAxisZ.command = setZCoeff;
            }
        }

        public void CheckEnd()
        {
            if (commandAxisX.endCommand == true)
            {
                commandAxisX.command = "";
                commandAxisX.errorCode = 0;
                commandAxisX.endCommand = false;
                commandAxisX.newCommand = false;
                commandAxisX.sendCommand = false;
                commandAxisX.startCommand = false;
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
            if (commandAxisY.endCommand == true)
            {
                commandAxisY.command = "";
                commandAxisY.errorCode = 0;
                commandAxisX.endCommand = false;
                commandAxisY.newCommand = false;
                commandAxisY.sendCommand = false;
                commandAxisY.startCommand = false;
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
            if (commandAxisZ.endCommand == true)
            {
                commandAxisZ.command = "";
                commandAxisZ.errorCode = 0;
                commandAxisX.endCommand = false;
                commandAxisZ.newCommand = false;
                commandAxisZ.sendCommand = false;
                commandAxisZ.startCommand = false;
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
