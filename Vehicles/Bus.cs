using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GarageOvningUML.Enums;

namespace GarageOvningUML.Vehicles
{
    public class Bus : Vehicle
    {
        public int SeatsNr { get; set; }
        public Bus(string regNr, Colors color, int weight, int seats, int wheels) : base(regNr, color, weight, wheels)
        {
            SeatsNr = seats;
        }

    }
}