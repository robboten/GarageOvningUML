using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarageOvningUML
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Constructor)]
    public class NameAttribute : Attribute
    {
        public string NiceName { get; set; }
        public NameAttribute(string niceName)
        {
            NiceName = niceName;
        }

    }
}
