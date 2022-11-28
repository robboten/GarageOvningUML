using System.Text.RegularExpressions;

namespace GarageOvningUML.UI
{
    /// <remarks>should give feedback on action</remarks>
    public class ConsoleUI : IUI
    {
        public ConsoleUI()
        {
            Clear();
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

        public void Wait()
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey(true);
        }

        public string InputLong()
        {
            return Console.ReadLine();
        }

        public string InputLoop(string message)
        {
            Message(message);
            var input = Console.ReadLine();

            while (string.IsNullOrEmpty(input))
            {
                Message("\n" + message);
                input = Console.ReadLine();
            }

            return input;
        }

        public int InputLoopInt(string message)
        {
            Message(message);
            var str = InputLong();
            int o;

            while (!int.TryParse(str, out o))
            {
                Message("\n" + message);
                str = InputLong();
            }
            return o;
        }

        public string RegNrValidation(string message)
        {
            Message(message);
            var regNr = InputLong();

            while (!Regex.IsMatch(regNr, @"[A-Z]{3}\d{3}", RegexOptions.IgnoreCase)) 
            {
                Message("\n" + message);
                regNr = InputLong();
            }
            return regNr;
        }


        //is it possible to make this generic and then pass in the type of enum?
        public VehicleTypes EnumValidation(string message)
        {
            Message(message);
            var str = InputLong();
            VehicleTypes o;
            int.TryParse(str, out int i);

            

            while (!Enum.IsDefined(typeof(VehicleTypes), i))
            {
                Message("\n" + message);
                str = InputLong();
                int.TryParse(str, out i);
            }

            VehicleTypes vtype = (VehicleTypes)i;
            return vtype;
        }

        public bool t(string message)
        {
            //if (Enum.TryParse(message, out VehicleTypes o))
            if (int.TryParse(message, out int i) && Enum.IsDefined(typeof(VehicleTypes), i))
                return true;
            return false;
        }
    }
}