using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComProject
{
    class Commands
    {
        public bool newCommand = false;
        public bool twoCommands = false;
        public bool sendCommand = false;
        public bool startCommand = false;
        public bool endCommand = false;
        public char typeCommand;
    }
    public enum TypeCommand
    {
        setStep = 0,
        setCoeff = 1
    };
}
