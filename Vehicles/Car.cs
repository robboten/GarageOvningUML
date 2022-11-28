using GarageOvningUML.Enums;

namespace GarageOvningUML.Vehicles
{
    public class Car : Vehicle
    {
        public int EnginesNr  { get; set; }

        public Car(string regNr, Colors color, int engNr, int wheels) : base(regNr, color, wheels)
        {
            EnginesNr = engNr;
        }

        public Car(string regNr, string color, int engNr, int wheels) : base(regNr, color, wheels)
        {
            EnginesNr = engNr;
        }

        public Car()
        {

        }


    }
}