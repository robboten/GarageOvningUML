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

        //private int wheelsNr;
        public int WheelsNr
        {
            get;
            set;
        }

        private string colorStr;

        public string ColorStr
        {
            get=> colorStr;
            set { colorStr = value.ToUpper(); }
        }

        public virtual string VehicleInfo()
        {
            return $"Type: {GetType().Name,-12} Registration number: {registrationNr,-8} Number of wheels: {WheelsNr,-4} Color: {ColorStr,-10} ";
        }
    }
}