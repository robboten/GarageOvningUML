using GarageOvningUML.Enums;

namespace GarageOvningUML.Vehicles
{
    public class Car : Vehicle
    {
        public Car()
        {
            throw new NotImplementedException();
        }

        public Car(string regNr, Colors color, int weight, int engNr, int wheels) : base(regNr, color, weight, wheels)
        {
            EnginesNr = engNr;
        }

        public int EnginesNr
        {
            get => default;
            set
            {
            }
        }
    }
}