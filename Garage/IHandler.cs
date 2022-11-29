using GarageOvningUML.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarageOvningUML.Garage
{
    public interface IHandler
    {
        void AddVehicle(Vehicle v, bool verbose);
        void ListAll();
        void ListByType();
        void RemoveVehicle(IVehicle v);

        /// <remarks>regnr + all properties in combination</remarks>
        void Search(string sStr);
        public void Seeder();
        void AddVehicleByInput();
        void SearchRemove(string sStr);
    }
}