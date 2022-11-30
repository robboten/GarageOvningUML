namespace GarageOvningUML.Vehicles
{
    public class Bus : Vehicle
    {
        public int Seats { get; set; }

        public override string VehicleInfo()
        {
            return base.VehicleInfo() + $"Seats: {Seats}";
        }
    }
}