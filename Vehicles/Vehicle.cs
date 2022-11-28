using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using Bogus.DataSets;
using GarageOvningUML.Enums;

namespace GarageOvningUML.Vehicles
{
    public abstract class Vehicle : IVehicle
    {
        public Vehicle()
        {
        }
        protected Vehicle(string RegNr, Colors color, int wheelsNr = 4)
        {
            Color = color;
            registrationNr = RegNr;
            WheelsNr = wheelsNr;
        }

        protected Vehicle(string RegNr, string color, int wheelsNr = 4)
        {
            ColorStr = color;
            registrationNr = RegNr;
            WheelsNr = wheelsNr;
        }

        private string registrationNr;
        public string RegistrationNr { 
            get => registrationNr;
            set
            {
                ArgumentNullException.ThrowIfNull(value, nameof(value));
                registrationNr = value;
            }
        }
        public int WheelsNr { 
            get; 
            set; 
        }
        public Colors Color { 
            get; 
            set; 
        }

        public string ColorStr
        {
            get ;
            set;
        }
    }
}