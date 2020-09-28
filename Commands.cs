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
        public bool setCommand = false;
        public char typeCommand;
        public string nextCommand;
        public int value;
        public Errorcode errorCode;
        public string command = "";
        public enum TypeCommand
        {
            setStep = 0,
            setCoeff = 1
        };
        public enum Errorcode
        {
            ok = 0,
            not_ok = 1,
            wrg_value = 2,
            wrg_axis = 3,
            no_lim_sw0 = 4,
            no_lim_sw1 = 5,
            no_lim_sw2 = 6,

        };
    }
}
