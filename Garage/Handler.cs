using GarageOvningUML.Enums;
using GarageOvningUML.UI;
using GarageOvningUML.Vehicles;

namespace GarageOvningUML.Garage
{
    public class Handler : IHandler
    {
        public GenericGarage<Vehicle> GenGarage; //nullable good here, I don't want it set before init
        private readonly IUI Ui;
        public Handler(IUI ui)
        {
            Ui = ui;

            Ui.Message("Garage setup");
            var s = MakeGarage();
            GenGarage = new GenericGarage<Vehicle>(s);
        }

        public int MakeGarage()
        {
            //Break this out
            //Sätta en kapacitet(antal parkeringsplatser) vid instansieringen av ett nytt garage
            Ui.Message("Setting up a new garage...\n");

            while (true)
            {
                Ui.Message("How many parking slots would you like to have?\n");
                var str = Ui.InputLong();
                if (int.TryParse(str, out int o))
                {
                    return o;
                }
            }
        }
        public void AddVehicle(Vehicle v)
        {
            //get return for successful adding..
            if (GenGarage.Add(v))
                Ui.Message($"Succesfully added {v.RegistrationNr}\n");
            else
                Ui.Message($"Something went wrong trying to add {v.RegistrationNr}\n");

            Ui.Wait(); 
            //make success and error handlers for ui?
        }

        //need a check to see if the vehicle reg exists already

        public void RemoveVehicle(Vehicle v)
        {
            //get return for successful..
            if (GenGarage.Remove(v))
                Ui.Message($"Succesfully removed {v.RegistrationNr}\n");
            else
                Ui.Message($"Something went wrong trying to add {v.RegistrationNr}\n");
            Ui.Wait();
        }

        public void ListAll()
        {
            Ui.Clear();
            Ui.Message($"Listing all {GenGarage.Count()} vehicles...\n"); //always same as capacity?
            foreach (var v in GenGarage)
            {
                Ui.Message($"{v.RegistrationNr} {v.Color} {v.WheelsNr} {v.Weight}\n");
            }
            Ui.Wait();
        }

        public void ListByType()
        {
            Ui.Clear();
            Ui.Message($"Listing types of vehicles...\n");

            //all not null
            var notNullList= GenGarage.GetArr().Where(x=> x != null).ToList();

            //get all types
            var typesList = notNullList.Select(x => x.GetType()).Distinct();

            //nr of types
            int nrTypes = typesList.Count();

            Ui.Message($"{nrTypes} different types of vehicles in the garage.\n");

            //get amount of vehicles in each type
            foreach (var type in typesList)
            {
                var eachType = GenGarage.GetArr().Where(x => x.GetType() == type).ToList();
                Ui.Message($"{eachType.Count} of vehicles are of the type {type.Name}\n");
            }          
            
            Ui.Wait();
        }

        public void Search(string sStr)
        {
            Ui.Message($"Searching for vehicles...\n");

            var v = GenGarage.GetArr().Where(x => x.RegistrationNr== sStr).First();

            Ui.Message($"Found: {v.RegistrationNr} {v.Color} {v.WheelsNr} {v.Weight}\n");
            Ui.Wait();
        }

        public void Seeder()
        {
            Ui.Clear();
            Ui.Message($"Generating {GenGarage.Capacity} vehicles...\n");
            BogusGen bg = new(GenGarage.Capacity);
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
            Ui.Wait();
        }
    }
}