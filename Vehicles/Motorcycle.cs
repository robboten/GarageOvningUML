namespace GarageOvningUML.Vehicles
{
    public class Motorcycle : Vehicle
    {
        public Motorcycle() { }

        public Motorcycle(string regNr, string color, int enginesNr, int wheels) : base(regNr, color, wheels)
        {
            Engines = enginesNr;
        }

        public int Engines
        {
            get;
            set;
        }

        public override string VehicleInfo()
        {
            return base.VehicleInfo() + $"Engines: {Engines}";
        }
    }
}