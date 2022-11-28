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
        //just for now, to use with bogusgen
        public Vehicle()
        {
        }

        //with enums
        protected Vehicle(string RegNr, Colors color, int wheelsNr = 4)
        {
            Color = color;
            registrationNr = RegNr;
            WheelsNr = wheelsNr;
        }

        //without enums
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
                //how to handle this?
                ArgumentNullException.ThrowIfNull(value, nameof(value));

                registrationNr = value.ToUpper();
            }
        }
        public int WheelsNr { 
            get; 
            set; 
        }
        public Colors Color { 
            get;
            //get
            //{
            //    return (Colors)ColorInt;
            //}
            set; 
        }

        public string ColorStr
        {
            get ;

            set;
        }

        public int ColorInt
        {
            get;
            set;
        }

        public virtual string VehicleInfo()
        {
            return $"Type: {this.GetType().Name}, RegNr: {registrationNr}, Nr of wheels: {WheelsNr}, Color: {ColorStr}, ";
        }
    }
}