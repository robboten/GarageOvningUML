using Bogus;
using GarageOvningUML.Enums;
using GarageOvningUML.Vehicles;
using Newtonsoft.Json;

namespace GarageOvningUML
{
    public class BogusGen
    {
        private int n;
        public BogusGen(int nr)
        {
            n = nr;
            //var types = new string[] { "Car", "Bus", "Boat", "Motorcycle" };
        }

        public List<Bus> BogusBusGenerator()
        {
            var Busfaker = new Faker<Bus>();
            ApplyBusRules(Busfaker);

            var busses = Busfaker.Generate(n/2);

            //Console.WriteLine(JsonConvert.SerializeObject(busses, Newtonsoft.Json.Formatting.Indented));

            return busses;
        }

        public List<Car> BogusCarGenerator()
        {
            var Carfaker = new Faker<Car>();
            ApplyCarRules(Carfaker);
            //ApplyVehicleRules(Carfaker);

            var cars = Carfaker.Generate(n/2);

            //Console.WriteLine(JsonConvert.SerializeObject(cars, Newtonsoft.Json.Formatting.Indented));

            return cars;
        }
        public static Faker<Vehicle> ApplyBusRules<Vehicle>(Faker<Vehicle> faker)
            where Vehicle : Bus
        {
            return faker
               .RuleFor(u => u.Seats, f => f.Random.Int(40, 120))
               .RuleFor(u => u.RegistrationNr, f => f.Random.Replace("???###"))
                //.RuleFor(u => u.Color, f => f.PickRandom<Colors>())
                .RuleFor(u=> u.ColorStr,f=>f.Commerce.Color())
                .RuleFor(u => u.WheelsNr, f => f.Random.Even(4, 8));
        }

        public static Faker<Vehicle> ApplyCarRules<Vehicle>(Faker<Vehicle> faker)
        where Vehicle : Car
        {
            return faker
               .RuleFor(u => u.Engines, f => f.Random.Int(1, 8))
                .RuleFor(u => u.RegistrationNr, f => f.Random.Replace("???###"))
                .RuleFor(u => u.ColorStr, f => f.Commerce.Color())
                //.RuleFor(u => u.Color, f => f.PickRandom<Colors>())
                .RuleFor(u => u.WheelsNr, f => f.Random.Even(4, 8));
        }

        //public static Faker<T> ApplyVehicleRules<T>(this Faker<T> faker) where T : class, IVehicle
        //{
        //    return faker
        //       .RuleFor(u => u.RegistrationNr, f => f.Random.Replace("???###"))
        //        .RuleFor(u => u.Color, f => f.PickRandom<Colors>())
        //        .RuleFor(u => u.WheelsNr, f => f.Random.Even(4, 8))
        //        .RuleFor(u => u.Weight, f => f.Random.Int(1000, 20000));
        //}

    }
}
