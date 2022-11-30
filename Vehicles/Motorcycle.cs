namespace GarageOvningUML.Vehicles
{
    public class Motorcycle : Vehicle
    {
        public int Engines { get; set; }
        public Motorcycle() { }

        public Motorcycle(string regNr, string color, int enginesNr, int wheels) : base(regNr, color, wheels)
        {
            Engines = enginesNr;
        }

        public override string VehicleInfo()
        {
            return base.VehicleInfo() + $"Engines: {Engines}";
        }
    }
}