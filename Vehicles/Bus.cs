using GarageOvningUML.Enums;

namespace GarageOvningUML.Vehicles
{
    public class Bus : Vehicle
    {
        public int SeatsNr { get; set; }
        public Bus(string regNr, Colors color, int seats, int wheels) : base(regNr, color, wheels)
        {
            SeatsNr = seats;
        }

        public Bus(string regNr, string color, int seats, int wheels) : base(regNr, color, wheels)
        {
            SeatsNr = seats;
        }

        public Bus()
        {
        }
    }
}