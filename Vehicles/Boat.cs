namespace GarageOvningUML.Vehicles
{
    public class Boat : Vehicle
    {
        public int Length { get; set; }

        public override string VehicleInfo()
        {
            return base.VehicleInfo() + $"Length: {Length}";
        }
    }
}