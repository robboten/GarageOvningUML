using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GarageOvningUML.Enums;

namespace GarageOvningUML.Vehicles
{
    public class Airplane : Vehicle
    {
        public Airplane(string regNr, Colors color, int weight, FuelTypes fuelType, int wheels) : base(regNr, color, weight, wheels)
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