using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarageOvningUML.UI
{
    /// <remarks>should give feedback on action</remarks>
    public class ConsoleUI : IUI
    {
        public ConsoleUI()
        {
            Clear();
        }

        public string InputLong()
        {
            return Console.ReadLine();
        }

        public char InputChar()
        {
            return Console.ReadKey(true).KeyChar;
        }

        public void Message(string text)
        {
            Console.Write(text);
        }

        public void Clear()
        {
            Console.CursorVisible = false;
            Console.Clear();
            //Console.SetCursorPosition(0, 0);
        }
    }
}