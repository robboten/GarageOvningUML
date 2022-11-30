using Bogus;
using GarageOvningUML.Vehicles;

namespace GarageOvningUML
{
    public class BogusGen
    {
        private readonly int n;
        private readonly int d;
        public BogusGen(int nr)
        {
            n = nr;
            d = nr > 2 ? 2 : 1;
            //var types = new string[] { "Car", "Bus", "Boat", "Motorcycle" };
        }

        public List<Bus> BogusBusGenerator()
        {
            var Busfaker = new Faker<Bus>();
            ApplyBusRules(Busfaker);
            RulesExtensions.ApplyVehicleRules(Busfaker);

            var busses = Busfaker.Generate(n / d);

            //Console.WriteLine(JsonConvert.SerializeObject(busses, Newtonsoft.Json.Formatting.Indented));

            return busses;
        }

        public List<Car> BogusCarGenerator()
        {
            var Carfaker = new Faker<Car>();
            RulesExtensions.ApplyVehicleRules(Carfaker);
            ApplyCarRules(Carfaker);

            var v = Carfaker.Generate(n / d);

            return v;
        }

        //public List<Motorcycle> BogusMotorcycleGenerator()
        //{
        //    var Motorcyclefaker = new Faker<Motorcycle>();
        //    RulesExtensions.ApplyVehicleRules(Motorcyclefaker);
        //    ApplyMotorcycleRules(Motorcyclefaker);

        //    var v = Motorcyclefaker.Generate(n / d);

        //    return v;
        //}

        public static Faker<Vehicle> ApplyBusRules<Vehicle>(Faker<Vehicle> faker) where Vehicle : Bus
        {
            return faker
               .RuleFor(u => u.Seats, f => f.Random.Int(40, 120))
               .RuleFor(u => u.WheelsNr, f => f.Random.Even(4, 12));
        }

        public static Faker<Vehicle> ApplyCarRules<Vehicle>(Faker<Vehicle> faker) where Vehicle : Car
        {
            return faker
               .RuleFor(u => u.Engines, f => f.Random.Int(1, 8))
                .RuleFor(u => u.WheelsNr, f => 4); //lazy default
        }

        public static Faker<Vehicle> ApplyMotorcycleRules<Vehicle>(Faker<Vehicle> faker) where Vehicle : Motorcycle
        {
            return faker
               .RuleFor(u => u.Engines, f => f.Random.Int(1, 8))
               .RuleFor(u => u.WheelsNr, f => f.Random.Even(2, 3));
        }

        public static Faker<Vehicle> ApplyBoatRules<Vehicle>(Faker<Vehicle> faker) where Vehicle : Boat
        {
            return faker
               .RuleFor(u => u.Length, f => f.Random.Int(1000, 500000))
               .RuleFor(u => u.WheelsNr, f => 0);
        }

        public static Faker<Vehicle> ApplyAirplaneRules<Vehicle>(Faker<Vehicle> faker) where Vehicle : Airplane
        {
            return faker
               .RuleFor(u => u.Passengers, f => f.Random.Int(4, 800))
               .RuleFor(u => u.WheelsNr, f => f.Random.Even(2, 20));
        }

    }

    public static class RulesExtensions
    {
        public static Faker<T> ApplyVehicleRules<T>(this Faker<T> faker) where T : class, IVehicle
        {
            return faker
               .RuleFor(u => u.RegistrationNr, f => f.Random.Replace("???###"))
                .RuleFor(u => u.ColorStr, f => f.Commerce.Color());
        }
    }

}
