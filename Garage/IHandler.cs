using GarageOvningUML.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarageOvningUML.Garage
{
    public interface IHandler
    {
        void AddVehicle(Vehicle v);
        void ListAll();
        void ListByType();
        void RemoveVehicle(Vehicle v);

        /// <remarks>regnr + all properties in combination</remarks>
        void Search();
        public void Seeder();
    }
}