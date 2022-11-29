using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarageOvningUML.UI
{
    public interface IUI
    {
        string InputLong();
        void Message(string text, bool nl = true);
        void Clear();
        char InputChar();
        void Wait();
        string InputLoop(string message);
        int InputLoopInt(string message);

        string RegNrValidation(string message);
        VehicleTypes EnumValidation(string message);
        int InputLoopIntRange(string message, int low, int high);
    }
}