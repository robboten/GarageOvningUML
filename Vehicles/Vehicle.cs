namespace GarageOvningUML.Vehicles
{
    public abstract class Vehicle : IVehicle
    {
        //just for now, to use with bogusgen
        public Vehicle() { }

        protected Vehicle(string RegNr, string color, int wheelsNr = 4)
        {
            ColorStr = color;
            registrationNr = RegNr;
            WheelsNr = wheelsNr;
        }

        private string registrationNr;

        public string RegistrationNr
        {
            get => registrationNr;
            set
            {
                //how to handle this?
                ArgumentNullException.ThrowIfNull(value, nameof(value));

                registrationNr = value.ToUpper();
            }
        }
        public int WheelsNr
        {
            get;
            set;
        }

        public string ColorStr
        {
            get;
            set;
        }

        public virtual string VehicleInfo()
        {
            return $"Type: {this.GetType().Name}, RegNr: {registrationNr}, Nr of wheels: {WheelsNr}, Color: {ColorStr}, ";
        }
    }
}