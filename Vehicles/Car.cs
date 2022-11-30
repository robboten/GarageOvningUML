namespace GarageOvningUML.Vehicles
{
    public class Car : Vehicle
    {
        //I should add set checks here to safeguard agains my weird setting of them
        public int Engines { get; set; }

        public Car() { }

        public Car(string regNr, string color, int engNr, int wheels) : base(regNr, color, wheels)
        {
            Engines = engNr;
        }

        public override string VehicleInfo()
        {
            return base.VehicleInfo() + $"Engines: {Engines}";
        }
    }
}