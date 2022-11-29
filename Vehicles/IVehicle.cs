using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GarageOvningUML.Enums;

namespace GarageOvningUML.Vehicles
{
    public interface IVehicle
    {
        string RegistrationNr { get; set; }
        int WheelsNr { get; set; }
        string ColorStr { get; set; }
        int ColorInt { get; set; }
        string VehicleInfo();
    }
}