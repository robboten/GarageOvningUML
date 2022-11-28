using GarageOvningUML.Enums;
using Microsoft.VisualBasic.FileIO;

namespace GarageOvningUML.Vehicles
{
    public class Bus : Vehicle
    {
        public int Seats { get; set; }
        public Bus(string regNr, Colors color, int seats, int wheels) : base(regNr, color, wheels)
        {
            Seats = seats;
        }

        public Bus(string regNr, string color, int seats, int wheels) : base(regNr, color, wheels)
        {
            Seats = seats;
        }

        public Bus()
        {
        }

        public override string VehicleInfo()
        {
            return base.VehicleInfo() + $"Seats: {Seats}";
        }
    }
}