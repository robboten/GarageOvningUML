namespace GarageOvningUML.Vehicles
{
    public class Bus : Vehicle
    {
        public int Seats { get; set; }

        public Bus() { }

        public Bus(string regNr, string color, int seats, int wheels) : base(regNr, color, wheels)
        {
            Seats = seats;
        }


        public override string VehicleInfo()
        {
            return base.VehicleInfo() + $"Seats: {Seats}";
        }
    }
}