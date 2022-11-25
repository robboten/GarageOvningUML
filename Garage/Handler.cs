using GarageOvningUML.Enums;
using GarageOvningUML.Vehicles;

namespace GarageOvningUML.Garage
{
    public class Handler : IHandler
    {
        public GenericGarage<Vehicle> GenGarage; //nullable good here, I don't want it set before init
        public Handler(int garageSlots)
        {
            GenGarage = new GenericGarage<Vehicle>(garageSlots);
            Console.WriteLine("Garage setup");
        }

        public void AddVehicle(Vehicle v)
        {
            //get return for successful adding..
            if (GenGarage.Add(v))
                Console.WriteLine($"Succesfully added {v.RegistrationNr}");
            else
                Console.WriteLine($"Something went wrong trying to add {v.RegistrationNr}");

            //make success and error handlers for ui?
        }

        //need a check to see if the vehicle reg exists already

        public void RemoveVehicle(Vehicle v)
        {
            //get return for successful..
            if (GenGarage.Remove(v))
                Console.WriteLine($"Succesfully removed {v.RegistrationNr}");
            else
                Console.WriteLine($"Something went wrong trying to add {v.RegistrationNr}");
        }

        public void ListAll()
        {
            foreach (var v in GenGarage)
            {
                Console.WriteLine($"{v.RegistrationNr} {v.Color} {v.WheelsNr} {v.Weight}");
            }
        }

        public void ListByType()
        {
            //works only if the list doesn't have empty spaces!!!

            //nr of types
            Console.WriteLine(GenGarage.GetArr().Select(x => x.GetType()).Distinct().Count());
        }

        public void Search()
        {
            throw new NotImplementedException();
        }

        public void Seeder()
        {
            BogusGen bg = new();
            var busses = bg.BogusBusGenerator();
            var cars = bg.BogusCarGenerator();

            foreach (var v in busses)
            {
                AddVehicle(v);
            }

            foreach (var v in cars)
            {
                AddVehicle(v);
            }
        }
    }
}