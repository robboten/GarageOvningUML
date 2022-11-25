using GarageOvningUML.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarageOvningUML.Vehicles
{
    public class Motorcycle : Vehicle
    {
        public Motorcycle(string regNr, Colors color, int weight, int enginesNr, int wheels) : base(regNr, color, weight, wheels)
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