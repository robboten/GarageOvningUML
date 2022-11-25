using GarageOvningUML.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarageOvningUML.Vehicles
{
    public class Boat : Vehicle
    {
        public Boat(string regNr, Colors color, int weight, int length, int wheels) : base(regNr, color, weight, wheels)
        {
            Length = length;
        }

        public float Length
        {
            get;
            set;

        }
    }
}