using GarageOvningUML.UI;
using GarageOvningUML.Utilities;
using GarageOvningUML.Vehicles;
using System.Reflection;

namespace GarageOvningUML.Garage
{
    public class Handler : IHandler
    {
        public GenericGarage<IVehicle> GenGarage=null!; //nullable good here? I don't want it set before init
        private readonly IUI ui;
        readonly IEnumerable<Type> VehicleClasses;

        public Handler(IUI ui)
        {
            this.ui = ui;

            var s = MakeGarage();
            GenGarage = new GenericGarage<IVehicle>(s);

            //don't like this in the constructor, but for now...
            VehicleClasses = AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(assembly => assembly.GetTypes())
                       .Where(type => type.IsSubclassOf(typeof(Vehicle)));
        }

        public int MakeGarage()
        {
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
                    //add others things with not int types if needed
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

        public void SearchRemove()
        {
            ui.Clear();

            if (!GenGarage.Any())
            {
                ui.Message("Nothing to remove");
                ui.Wait();
                return;
            }

            var sStr = ui.InputLoop($"Input registration number for vehicle to remove: ");

            ui.Message($"Searching for vehicles...");

            var v = GenGarage.GetArr().Where(x => string.Equals(x.RegistrationNr.ToLower(), sStr.ToLower(), StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (v != null)
            {
                ui.Message($"Found: {v.VehicleInfo}");
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
                ui.Message($"Succesfully removed the vehicle");
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
            ui.Message($"Listing types of vehicles and their count...");

            foreach (var group in GenGarage.GroupBy(v => v.GetType().Name))
            {
                Console.WriteLine($"{group.Key} : {group.Count()}");
            }

            ui.Wait();
        }

        public void Search()
        {
            ui.Clear();


            if (GenGarage.Count() == 0)
            {
                ui.Message("Garage is empty, so nothing to remove");
                ui.Wait();
                return;
            }

            var sStr = ui.InputLoop($"Input registration number to search for: ");

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


        //○ Alla lastbilar ------ forgot to implement this ... 
        public void SearchByProp()
        {
            ui.Clear();
            if (GenGarage.Count() == 0)
            {
                ui.Message("Garage is empty, so nothing to remove");
                ui.Wait();
                return;
            }
            
            var type = typeof(IVehicle);
            Dictionary <PropertyInfo,string> searchDict = new ();

            //get the properties for the inherited classes and set them
            PropertyInfo[] propNames = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            ui.Message("Input your search parameters, press Enter to skip a search parameter");

            for (int i = 0; i < propNames.Length; i++)
            {
                ui.Message($"{Utils.GetNiceName(propNames[i])}: ", false);
                searchDict.Add(propNames.ElementAt(i), ui.InputLong());
            }

            List<IVehicle> searchHits=new();
            IEnumerable<IVehicle> filteredHits = GenGarage.GetArr();

            foreach (var search in searchDict) {

                var searchkey = search.Value.ToUpper();
                if (searchkey!="")
                {
                    filteredHits = filteredHits.
                        Where(x => Equals(x.GetType().GetProperty(search.Key.Name).GetValue(x).ToString().ToUpper(), searchkey));
                    //ui.Message(filteredHits.Count().ToString());
                    //messy but running out of time to make nicer...
                    var searchhits = GenGarage.GetArr()
                        .Where(x=> Equals(x.GetType().GetProperty(search.Key.Name).GetValue(x).ToString().ToUpper(),searchkey));
                    if (searchhits.Any())
                    {
                        searchHits.AddRange(searchhits);
                    }
                }
            }

            //is it possible to do this nicer without nested ifs?
            if (searchHits.Any() || filteredHits.Any() ) {
                if (searchHits.Any())
                {
                    ui.Message("Fuzzy search hits for any parameter:");
                    foreach (Vehicle hit in searchHits.Cast<Vehicle>())
                    {
                        ui.Message(hit.VehicleInfo());
                    }
                }
                if (filteredHits.Any())
                {
                    ui.Message("Search hits with all parameters:");
                    foreach (Vehicle hit in filteredHits.Cast<Vehicle>().ToList())
                    {
                        ui.Message(hit.VehicleInfo());
                    }
                }
            }
            else
            {
                ui.Message("No vehicles found with these properties");
            }

            ui.Wait();
        }


        //limited to bus/car atm, if I have time I will add more
        public void Seeder()
        {
            ui.Clear();
            ui.Message($"Generating {GenGarage.Capacity} vehicles...");
            BogusGen bg = new(GenGarage.Capacity);

            var busses = bg.BogusBusGenerator();
            var cars = bg.BogusCarGenerator();
            var motorc = bg.BogusMotorcycleGenerator();

            foreach (var v in busses)
            {
                AddVehicle(v, false);
            }

            foreach (var v in cars)
            {
                AddVehicle(v, false);
            }

            foreach (var v in motorc)
            {
                AddVehicle(v, false);
            }
            ui.Wait();
        }
    }
}