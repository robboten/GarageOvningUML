namespace GarageOvningUML.Vehicles
{

    //just to test if it worked with adding more classes in the  menus...
    public class Bike : Vehicle
    {
        public int Height { get; set; }

        public override string VehicleInfo()
        {
            return base.VehicleInfo() + $"Height: {Height}";
        }
    }
}