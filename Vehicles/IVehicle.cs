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
        Colors Color { get; set; }
        int WheelsNr { get; set; }
        int Weight { get; set; }
    }
}