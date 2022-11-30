using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GarageOvningUML.Enums;

namespace GarageOvningUML.Vehicles
{
    public interface IVehicle
    {
        [Name("Registration number")]
        string RegistrationNr { get; set; }

        [Name("Number of wheels")]
        int WheelsNr { get; set; }

        [Name("Color")]
        string ColorStr { get; set; }
        string VehicleInfo();
    }
}