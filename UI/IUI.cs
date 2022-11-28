using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarageOvningUML.UI
{
    public interface IUI
    {
        string InputLong();
        void Message(string message);
        void Clear();
        char InputChar();
        void Wait();
        string InputLoop(string message);
        int InputLoopInt(string message);

        string RegNrValidation(string message);
        VehicleTypes EnumValidation(string message);
    }
}