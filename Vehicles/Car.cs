using GarageOvningUML.Enums;

namespace GarageOvningUML.Vehicles
{
    public class Car : Vehicle
    {
        public int EnginesNr  { get; set; }

        public Car(string regNr, Colors color, int weight, int engNr, int wheels) : base(regNr, color, weight, wheels)
        {
            EnginesNr = engNr;
        }

        public Car()
        {

        }


    }
}