using GarageOvningUML.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarageOvningUML.Garage
{
    public interface IHandler
    {
        void MakeGarage();
        void AddVehicle(Vehicle v, bool verbose);
        void ListAll();
        void ListByType();
        void RemoveVehicle(IVehicle v);
        void Search();
        public void Seeder();
        void AddVehicleByInput();
        void SearchRemove();
        void SearchByProp();
    }
}