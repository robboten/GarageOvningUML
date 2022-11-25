using GarageOvningUML.Enums;
using GarageOvningUML.UI;
using GarageOvningUML.Vehicles;

namespace GarageOvningUML.Garage
{
    public class Handler : IHandler
    {
        public GenericGarage<Vehicle> GenGarage; //nullable good here? I don't want it set before init
        private readonly IUI ui;
        public Handler(IUI ui)
        {
            this.ui = ui;

            this.ui.Message("Garage setup");
            var s = MakeGarage();
            GenGarage = new GenericGarage<Vehicle>(s);
        }

        public int MakeGarage()
        {
            //Break this out
            //Sätta en kapacitet(antal parkeringsplatser) vid instansieringen av ett nytt garage
            ui.Message("Setting up a new garage...\n");
            return ui.InputLoopInt("How many parking slots would you like to have?\n");
        }

        //need a check to see if the vehicle reg exists already
        public void AddVehicleByString(string str, bool verbose = true)
        {
            //get return for successful adding..
            //if (GenGarage.Add(v) && verbose)
            //    ui.Message($"Succesfully added {v.RegistrationNr}\n");
            //else
            //    ui.Message($"Something went wrong trying to add {v.RegistrationNr}\n");

            ui.Wait();
            //make success and error handlers for ui?
        }

        //need a check to see if the vehicle reg exists already
        public void AddVehicle(Vehicle v, bool verbose=true)
        {
            //get return for successful adding..
            if (GenGarage.Add(v) && verbose)
                ui.Message($"Succesfully added {v.RegistrationNr}\n");
            else
                ui.Message($"Something went wrong trying to add {v.RegistrationNr}\n");

            ui.Wait(); 
            //make success and error handlers for ui?
        }

        public void RemoveVehicle(Vehicle v)
        {
            //get return for successful..
            if (GenGarage.Remove(v))
                ui.Message($"Succesfully removed {v.RegistrationNr}\n");
            else
                ui.Message($"Something went wrong trying to remove {v.RegistrationNr}\n");
            ui.Wait();
        }

        public void ListAll()
        {
            ui.Clear();
            ui.Message($"Listing all {GenGarage.Count()} vehicles...\n"); //always same as capacity?
            foreach (var v in GenGarage)
            {
                ui.Message($"{v.RegistrationNr} {v.Color} {v.WheelsNr} {v.Weight}\n");
            }
            ui.Wait();
        }

        public void ListByType()
        {
            ui.Clear();
            ui.Message($"Listing types of vehicles...\n");

            //all not null
            var notNullList= GenGarage.GetArr().Where(x=> x != null).ToList();

            //get all types
            var typesList = notNullList.Select(x => x.GetType()).Distinct();

            //nr of types
            int nrTypes = typesList.Count();

            ui.Message($"{nrTypes} different types of vehicles in the garage.\n");

            //get amount of vehicles in each type
            foreach (var type in typesList)
            {
                var eachType = GenGarage.GetArr().Where(x => x.GetType() == type).ToList();
                ui.Message($"{eachType.Count} of vehicles are of the type {type.Name}\n");
            }          
            
            ui.Wait();
        }

        public void Search(string sStr)
        {
            ui.Message($"Searching for vehicles...\n");

            var v = GenGarage.GetArr().Where(x => String.Equals(x.RegistrationNr.ToLower(), sStr.ToLower(),StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (v != null)
            {
                ui.Message($"Found: {v.RegistrationNr} {v.Color} {v.WheelsNr} {v.Weight}\n");
                ui.Wait();
            }
            else
            {
                ui.Message($"No mathing vehicles for {sStr}\n");
                ui.Wait();
            }
        }

        public void Seeder()
        {
            ui.Clear();
            ui.Message($"Generating {GenGarage.Capacity} vehicles...\n");
            BogusGen bg = new(GenGarage.Capacity);

            var busses = bg.BogusBusGenerator();
            var cars = bg.BogusCarGenerator();

            foreach (var v in busses)
            {
                AddVehicle(v,false);
            }

            foreach (var v in cars)
            {
                AddVehicle(v,false);
            }
            ui.Wait();
        }
    }
}