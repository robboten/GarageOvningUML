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
    }
}