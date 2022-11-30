namespace GarageOvningUML.Vehicles
{
    public class Airplane : Vehicle
    {
        public Airplane() { }
        //public Airplane(string regNr, Colors color, FuelTypes fuelType, int wheels) : base(regNr, color, wheels)
        //{
        //    Fuel = fuelType;
        //}

        //public Airplane(string regNr, string color, FuelTypes fuelType, int wheels) : base(regNr, color, wheels)
        //{
        //    Fuel = fuelType;
        //}

        public int Passengers { get; set; }
        //private int FuelInt { get;set; }

        //private FuelTypes FuelEnum
        //{
        //    get { return (FuelTypes)FuelInt; }
        //}

        //private FuelTypes Fuel
        //{
        //    get;
        //    //get
        //    //{
        //    //    var test = (FuelTypes)FuelInt;
        //    //    return test;
        //    //}
        //    set;
        //}

        public override string VehicleInfo()
        {
            return base.VehicleInfo() + $"Passengers: {Passengers}";//$"Fueltype: {Enum.GetName(Fuel)}";
        }
    }
}