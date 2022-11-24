using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using GarageOvningUML.Enums;

namespace GarageOvningUML.Vehicles
{
    public abstract class Vehicle : IVehicle
    {
        public Vehicle()
        {
        }
        protected Vehicle(string RegNr, Colors color, int weight, int wheelsNr = 4)
        {
            Color = color;
            RegistrationNr = RegNr;
            Weight = weight;
            WheelsNr = wheelsNr;
        }

        public string RegistrationNr { get; set; }
        public int WheelsNr { get; set; }
        public double Weight { get; set; }
        public Colors Color { get; set; }
    }
}