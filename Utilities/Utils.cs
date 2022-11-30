using Bogus.DataSets;
using System.Reflection;

namespace GarageOvningUML.Utilities
{
    public class Utils
    {
        public static class MenuHelpers
        {
            public const char Quit = 'q';
            public const char List = 'l';
            public const char ListType = 't';
            public const char Add = '+';
            public const char Remove = '-';
            public const char Search = 's';
            public const char SearchProp = 'p';
            public const char Seed = 'r';
        }

        //public static class MenuHelpersVehicle
        //{
        //    public const char Car = '1';
        //    public const char Bus = '2';
        //    public const char Motorcycle = '3';
        //    public const char Airplane = '4';
        //    public const char Boat = '5';
        //}

        //shouldn't be in here, but no time left to fix...
        public static string? GetNiceName(PropertyInfo propertyInfo)
        {
            NameAttribute? nameAttribute = propertyInfo.GetCustomAttribute<NameAttribute>();

            return nameAttribute is null ? propertyInfo.Name : nameAttribute.NiceName;
        }

    }


}