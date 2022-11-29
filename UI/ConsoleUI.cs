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

        public void Message(string text, bool nl=true)
        {
            if (nl)
            {
                Console.WriteLine(text);
            } else
            {
                Console.Write(text);
            }
            
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


        //would like an implementation of pressing escape to abort input and get back to menu, if I have time...
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

        public int InputLoopIntRange(string message,int low, int high)
        {
            Message(message);
            var str = InputLong();
            int o;

            while (true)
            {
                if (int.TryParse(str, out o) && o<high && o>low)
                {
                    break;
                }

                Message("\n" + message);
                str = InputLong();

            }
            return o;
        }

        public string RegNrValidation(string message)
        {
            Message(message);
            var regNr = InputLong();

            while (!Regex.IsMatch(regNr, @"^[A-Za-z]{3}\d{3}$", RegexOptions.IgnoreCase)) 
            {
                Message("\n" + message);
                regNr = InputLong();
            }
            return regNr;
        }

        //could also make this into a loop that gives the menu first, then wait for input
        //is it possible to make this generic and then pass in the type of enum?
        //public VehicleTypes EnumValidation(string message)
        //{
        //    Message(message);
        //    var str = InputLong();
        //    VehicleTypes o;
        //    VehicleTypes vtype;

        //    while (true)
        //    {
        //        if(int.TryParse(str, out int i) && Enum.IsDefined(typeof(VehicleTypes), i))
        //        {
        //            vtype = (VehicleTypes)i;
        //            break;
        //        }
        //        Message("\n" + message);
        //        str = InputLong();
        //    }

        //    return vtype;
        //}
    }
}