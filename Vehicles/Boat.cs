using GarageOvningUML.Enums;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarageOvningUML.Vehicles
{
    public class Boat : Vehicle
    {
        public Boat() { }
        public Boat(string regNr, Colors color, int length, int wheels) : base(regNr, color, wheels)
        {
            Length = length;
        }

        public Boat(string regNr, string color, int length, int wheels) : base(regNr, color, wheels)
        {
            Length = length;
        }

        public float Length
        {
            get;
            set;

        }
        public override string VehicleInfo()
        {
            return base.VehicleInfo() + $"Length: {Length}";
        }
    }
}