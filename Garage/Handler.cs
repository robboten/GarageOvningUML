using GarageOvningUML.UI;
using GarageOvningUML.Vehicles;
using System.Reflection;

namespace GarageOvningUML.Garage
{
    public class Handler : IHandler
    {
        public GenericGarage<IVehicle> GenGarage; //nullable good here? I don't want it set before init
        private readonly IUI ui;
        readonly IEnumerable<Type> VehicleClasses;

        public Handler(IUI ui)
        {
            this.ui = ui;

            var s = MakeGarage();
            GenGarage = new GenericGarage<IVehicle>(s);

            VehicleClasses = AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(assembly => assembly.GetTypes())
                       .Where(type => type.IsSubclassOf(typeof(Vehicle)));
        }

        public int MakeGarage()
        {
            //Break this out
            //Sätta en kapacitet(antal parkeringsplatser) vid instansieringen av ett nytt garage
            ui.Message("Setting up a new garage...");
            return ui.InputLoopInt("How many parking slots would you like to have?");
        }
        public bool CheckForFullGarage()
        {
            if (GenGarage.Count() >= GenGarage.Capacity)
            {
                ui.Message("Sorry, the garage is already full.");
                ui.Wait();
                return true;
            }
            return false;
        }



        public void AddVehicleByInput()
        {
            ui.Clear();

            //if the garage is full, abort before going into more stuff
            if (CheckForFullGarage()) return;

            ui.Message("Choose one of the following types to add:");

            //get nr of vehicle classes
            int classesLen = VehicleClasses.Count();

            //make the submenu for chosing vehicle type
            for (int i = 0; i < classesLen; i++)
            {
                ui.Message($"{i}:{VehicleClasses.ElementAt(i).Name}");
            }

            //get user input within range of the classes
            int typeInt = ui.InputLoopIntRange("Select vehicle type: ", 0, classesLen - 1);

            //now start collecting info
            ui.Message("Please input the following:");
            var regNr = ui.RegNrValidation($"Registration number (in the format ABC123): ");

            if (GenGarage.Any(vh => vh.RegistrationNr == regNr))
            {
                ui.Message("Registration number is already in use.");
                ui.Wait();
                return;
            }

            //set the type of the new object to add
            Type type = VehicleClasses.ElementAt(typeInt);

            //make instance and set properties common for all vehicles (could make this more dynamic too I guess)
            var obj = Activator.CreateInstance(type) as IVehicle;
            obj.RegistrationNr = regNr;
            obj.WheelsNr = ui.InputLoopInt($"Number of wheels: ");
            obj.ColorStr = ui.InputLoop($"Color: ");

            //get the properties for the inherited classes and set them
            PropertyInfo[] propNames = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in propNames)
            {
                if (prop.GetSetMethod() != null)
                {
                    if (prop.PropertyType == typeof(int))
                        type.GetProperty(prop.Name).SetValue(obj, ui.InputLoopInt(prop.Name + ": "), null);
                    //--- add others like airplane fuel or change to int fuel
                }
            }

            ////get return for successful adding..
            if (GenGarage.Add(obj))
                ui.Message($"Succesfully added {obj.VehicleInfo}");
            else
                ui.Message($"Something went wrong trying to add {obj.VehicleInfo}");

            ui.Wait();
            //make success and error handlers for ui?
        }

        public void AddVehicle(Vehicle v, bool verbose = true)
        {
            if (CheckForFullGarage()) return;

            if (GenGarage.Any(vh => vh.RegistrationNr == v.RegistrationNr))
            {
                ui.Message($"Registration number {v.RegistrationNr} is already in use. Try again.");

                ui.Wait();
                return;
            }

            if (GenGarage.Add(v))
            {
                if (verbose)
                {
                    ui.Message($"Succesfully added {v.VehicleInfo}");
                    ui.Wait();
                }
            }
            else
            {
                ui.Message($"Something went wrong trying to add {v.VehicleInfo}");
                ui.Wait();
            }

            //make success and error handlers for ui instead of adding same things over and over?
        }

        public void SearchRemove(string sStr)
        {
            ui.Message($"Searching for vehicles...");

            var v = GenGarage.GetArr().Where(x => string.Equals(x.RegistrationNr.ToLower(), sStr.ToLower(), StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (v != null)
            {
                ui.Message($"Found: {v.VehicleInfo()}");
                RemoveVehicle(v);
            }
            else
            {
                ui.Message($"No mathing vehicles for {sStr}");
                ui.Wait();
            }
        }

        public void RemoveVehicle(IVehicle v)
        {
            if (GenGarage.Remove(v))
                ui.Message($"Succesfully removed {v.VehicleInfo}");
            else
                ui.Message($"Something went wrong trying to remove {v.VehicleInfo}");
            ui.Wait();
        }

        public void ListAll()
        {
            ui.Clear();
            ui.Message($"Listing all {GenGarage.Count()} vehicles...");

            foreach (var v in GenGarage)
            {
                ui.Message(v.VehicleInfo());
            }
            ui.Wait();
        }

        public void ListByType()
        {
            ui.Clear();
            ui.Message($"Listing types of vehicles...");

            foreach (var group in GenGarage.GroupBy(v => v.GetType().Name))
            {
                Console.WriteLine($"{group.Key} : {group.Count()}");
            }

            ui.Wait();
        }

        public void Search(string sStr)
        {
            ui.Message($"Searching for vehicles...");

            var v = GenGarage.GetArr().Where(x => string.Equals(x.RegistrationNr.ToLower(), sStr.ToLower(), StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (v != null)
            {
                ui.Message($"Found: {v.VehicleInfo()}");
                ui.Wait();
            }
            else
            {
                ui.Message($"No mathing vehicles for {sStr}");
                ui.Wait();
            }
        }

        //● Söka efter fordon utifrån en egenskap eller flera (alla möjliga kombinationer från basklassen Vehicle). Exempelvis:
        //○ Alla svarta fordon med fyra hjul.
        //○ Alla motorcyklar som är rosa och har 3 hjul.
        //○ Alla lastbilar
        //○ Alla röda fordon
        public void SearchByProp()
        {

            var type = typeof(IVehicle);
            Dictionary <PropertyInfo,string> searchlist = new ();

            //get the properties for the inherited classes and set them
            PropertyInfo[] propNames = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            ui.Message("Input your search parameters, input * or just Enter to not skip");

            for(int i=0;i<propNames.Length;i++)
            {
                ui.Message($"{i} : {propNames.ElementAt(i).Name}: ", false);
                searchlist.Add(propNames.ElementAt(i),ui.InputLong());
            }

            foreach(var l in searchlist)
            {
                ui.Message(l.Key.Name+" : "+l.Value);
            }

            ui.Wait();
            //Search sub menu - search params = properties från IVehicle
            //properties to search for (list to choose first)
            //build linq
        }


        //limited to bus/car atm, if I have time I will add more
        public void Seeder()
        {
            ui.Clear();
            ui.Message($"Generating {GenGarage.Capacity} vehicles...");
            BogusGen bg = new(GenGarage.Capacity);

            var busses = bg.BogusBusGenerator();
            var cars = bg.BogusCarGenerator();

            foreach (var v in busses)
            {
                AddVehicle(v, false);
            }

            foreach (var v in cars)
            {
                AddVehicle(v, false);
            }
            ui.Wait();
        }
    }
}