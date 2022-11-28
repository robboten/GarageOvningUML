using GarageOvningUML.Enums;

namespace GarageOvningUML.Vehicles
{
    public class Boat : Vehicle
    {
        //how do I set wheels to 0 for all in the setter for boats?
        public Boat() { }

        public Boat(string regNr, Colors color, int length) : base(regNr, color, 0)
        {
            Length = length;
        }

        public Boat(string regNr, string color, int length) : base(regNr, color, 0)
        {
            Length = length;
        }

        public float Length { get; set; }

        public override string VehicleInfo()
        {
            return base.VehicleInfo() + $"Length: {Length}";
        }
    }
}