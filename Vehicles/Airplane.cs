using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GarageOvningUML.Enums;

namespace GarageOvningUML.Vehicles
{
    public class Airplane : Vehicle
    {
        public Airplane(string regNr, Colors color, FuelTypes fuelType, int wheels) : base(regNr, color, wheels)
        {
            FuelType = fuelType;
        }

        public Airplane(string regNr, string color, FuelTypes fuelType, int wheels) : base(regNr, color, wheels)
        {
            FuelType = fuelType;
        }


        public FuelTypes FuelType
        {
            get;
            set;
        }
    }
}