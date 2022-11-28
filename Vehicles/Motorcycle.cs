using GarageOvningUML.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarageOvningUML.Vehicles
{
    public class Motorcycle : Vehicle
    {
        public Motorcycle(string regNr, Colors color, int enginesNr, int wheels) : base(regNr, color,wheels)
        {
            EnginesNr = enginesNr;
        }

        public Motorcycle(string regNr, string color, int enginesNr, int wheels) : base(regNr, color,wheels)
        {
            EnginesNr = enginesNr;
        }

        public int EnginesNr
        {
            get;
            set;
        }
    }
}