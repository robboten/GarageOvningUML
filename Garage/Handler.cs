using GarageOvningUML.UI;
using GarageOvningUML.Utilities;
using GarageOvningUML.Vehicles;
using System.Reflection;

namespace GarageOvningUML.Garage
{
    public class Handler : IHandler
    {
        readonly IUI ui;
        int ClassesLength = 0;

        GenericGarage<IVehicle> GenGarage = null!;
        IEnumerable<Type> VehicleClasses = null!;

        Dictionary<string, PropertyInfo> VehiclePropertyDict = new();

        public Handler(IUI ui)
        {
            this.ui = ui;
            Init();
        }

        public void Init()
        {
            ui.Message("Setting up a new garage...");

            var s = ui.InputLoopInt("How many parking slots would you like to have?");
            GenGarage = new GenericGarage<IVehicle>(s);

            //get all vehicle classes for use later
            VehicleClasses = AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(assembly => assembly.GetTypes())
                       .Where(type => type.IsSubclassOf(typeof(Vehicle)));

            //count the classes for easier access
            ClassesLength = VehicleClasses.Count();

            //get the properties of IVehicle
            VehiclePropertyDict = GetClassProperties(typeof(IVehicle));
        }
        static Dictionary<string, PropertyInfo> GetClassProperties(Type type)
        {
            Dictionary<string, PropertyInfo> dict = new Dictionary<string, PropertyInfo>();

            var classType = type;
            PropertyInfo[] cProps = classType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            //loop thru all vehicle properties
            foreach (var prop in cProps)
            {
                if (prop.GetSetMethod() != null)
                    dict.Add(prop.Name, classType.GetProperty(prop.Name)!);
            }
            return dict;
        }
        bool CheckForFullGarage()
        {
            //return GenGarage.IsFull ? true : false;
            if (GenGarage.Count() >= GenGarage.Capacity)
            {
                ui.Message("Sorry, the garage is already full.");
                ui.Wait();
                return true;
            }
            return false;
        }

        bool IsRegNrInUse(string regNr)
        {
            //reg nr already in use?
            if (GenGarage.Any(vh => vh.RegistrationNr == regNr.ToUpper()))
            {
                ui.Message($"Registration number {regNr} is already in use. Try again.");
                ui.Wait();
                return true;
            }
            return false;
        }

        bool IsGarageEmpty() {
            if (!GenGarage.Any())
            {
                ui.Message("The garage is empty!");
                ui.Wait();
                return true;
            }
            return false;
        }

        public void AddVehicle(Vehicle v, bool verbose = true)
        {
            //full garage?
            if (CheckForFullGarage()) return;

            //reg nr already in use?
            if (IsRegNrInUse(v.RegistrationNr)) return;

            //if all is well add
            if (GenGarage.Add(v))
            {
                if (verbose)
                {
                    ui.Message($"Succesfully added {v.VehicleInfo()}");
                    ui.Wait();
                }
            }
            else
            {
                ui.Message($"Something went wrong trying to add {v.VehicleInfo()}");
                ui.Wait();
            }

            //make success and error handlers for ui instead of adding same things over and over?
        }

        public void RemoveVehicle(IVehicle v)
        {

            if (GenGarage.Remove(v))
                ui.Message($"Succesfully removed the vehicle");
            else
                ui.Message($"Something went wrong trying to remove the vehicle.");
            ui.Wait();
        }

        public void SearchRemove()
        {
            ui.Clear();

            if (IsGarageEmpty()) return;

            //output all first to make it easier to see what to remove
            ui.Message($"Listing all {GenGarage.Count()}/{GenGarage.Capacity} vehicles...");
            foreach (var vv in GenGarage)
            {
                ui.Message(vv.VehicleInfo());
            }

            var sStr = ui.InputLoop($"Input registration number for vehicle to remove: ");

            ui.Message($"Searching for vehicles...");

            var v = GenGarage.ToArray().Where(x => string.Equals(x.RegistrationNr.ToLower(), sStr.ToLower(), StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

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

        public void ListAll()
        {
            ui.Clear();

            if (IsGarageEmpty()) return;

            ui.Message($"Listing all {GenGarage.Count()}/{GenGarage.Capacity} vehicles...");

            foreach (var v in GenGarage)
            {
                ui.Message(v.VehicleInfo());
            }
            ui.Wait();
        }

        public void ListByType()
        {
            ui.Clear();

            if (IsGarageEmpty()) return;

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

            if (IsGarageEmpty()) return;

            var sStr = ui.RegNrValidation($"Input registration number to search for: ");

            ui.Message($"Searching for vehicles...");
            var garageArr = GenGarage.ToArray();

            //--could I remove the need for hard coded Reg nr in here?
            var v = garageArr.Where(x => string.Equals(x.RegistrationNr, sStr.ToUpper())).DefaultIfEmpty(null).First();
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

            if (IsGarageEmpty()) return;

            Dictionary<PropertyInfo, string> searchDict = new();

            ui.Message("Input your search parameters, press Enter to skip a search parameter");

            //loop thru the properties for IVehicle
            foreach (var prop in VehiclePropertyDict)
            {
                ui.Message($"{Utils.GetNiceName(prop.Value)}");
                searchDict.Add(prop.Value, ui.InputLong());
            }

            //do the actual search
            List<IVehicle> searchHits = new();
            IEnumerable<IVehicle> filteredHits = GenGarage.ToArray();

            //messy but running out of time to make nicer...
            foreach (var search in searchDict)
            {
                var searchkey = search.Value.ToUpper();
                if (searchkey != "")
                {
                    filteredHits = filteredHits.
                        Where(x => Equals(x.GetType().GetProperty(search.Key.Name).GetValue(x).ToString().ToUpper(), searchkey));

                    var searchhits = GenGarage.ToArray()
                        .Where(x => Equals(x.GetType().GetProperty(search.Key.Name).GetValue(x).ToString().ToUpper(), searchkey));

                    if (searchhits.Any())
                    {
                        searchHits.AddRange(searchhits);
                    }
                }
            }

            //is it possible to do this nicer without nested ifs?
            if (searchHits.Any() || filteredHits.Any())
            {
                if (searchHits.Any())
                {
                    ui.Message("Fuzzy search hits for any parameter:");
                    foreach (Vehicle hit in searchHits.Cast<Vehicle>())
                    {
                        ui.Message(hit.VehicleInfo());
                    }
                }

                ui.Message(""); //newline

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

        public void AddVehicleByInput()
        {
            ui.Clear();

            //if the garage is full, abort before going into more stuff
            if (CheckForFullGarage()) return;

            ui.Message("Choose one of the following types to add:");

            ///---could break this out too maybe...

            //make the submenu for chosing vehicle type
            for (int i = 0; i < ClassesLength; i++)
            {
                ui.Message($"{i} : {VehicleClasses.ElementAt(i).Name}");
            }

            //get user input within range of the classes
            int typeInt = ui.InputLoopIntRange("Select vehicle type: ", 0, ClassesLength);

            //set the type of the new object to add
            Type type = VehicleClasses.ElementAt(typeInt);

            ///---

            ui.Clear();

            //now start collecting info
            ui.Message("Please input the following:");

            //start with reg nr since we need to check if it exists first of all
            var regNr = ui.RegNrValidation($"Registration number (in the format ABC123): ");

            //reg nr already in use?
            if (IsRegNrInUse(regNr))
            {
                return;
            }

            //make instance and set properties common for all vehicles
            var obj = Activator.CreateInstance(type) as IVehicle;

            // I don't like reg nr hardcoded but can't do anything with it except with new attr
            obj.RegistrationNr = regNr;

            //loop thru all vehicle properties
            foreach (var p in VehiclePropertyDict)
            {
                if (p.Key == "RegistrationNr")
                    continue;//p.Value.SetValue(obj, ui.RegNrValidation($"{Utils.GetNiceName(p.Value)} "), null);
                else if (p.Value.PropertyType == typeof(int))
                    p.Value.SetValue(obj, ui.InputLoopInt($"{Utils.GetNiceName(p.Value)} "), null);
                else if (p.Value.PropertyType == typeof(string))
                    p.Value.SetValue(obj, ui.InputLoop($"{Utils.GetNiceName(p.Value)} "), null);
            }

            //loop thru all properties for selected inherited class
            var typeDict = GetClassProperties(type);
            foreach (var td in typeDict)
            {
                if (td.Value.PropertyType == typeof(int))
                    td.Value.SetValue(obj, ui.InputLoopInt($"{Utils.GetNiceName(td.Value)} "), null);
            }

            ////get return for successful adding..
            if (GenGarage.Add(obj))
                ui.Message($"Succesfully added:\n {obj.VehicleInfo()}");
            else
                ui.Message($"Something went wrong trying to add {obj.VehicleInfo()}");

            ui.Wait();
            //make success and error handlers for ui instead of hardcoding each one?

        }

        //limited to bus/car atm, if I have time I will add more
        public void Seeder()
        {
            ui.Clear();
            var nr = ui.InputLoopIntRange($"How many parking slots would you like to have? (min: 0 max: {GenGarage.Capacity})", 0, GenGarage.Capacity + 1);

            ui.Message($"Generating {nr} vehicles...");
            BogusGen bg = new(nr);

            var divider = 2;

            var busses = bg.BogusBusGenerator(divider);


            foreach (var v in busses)
            {
                AddVehicle(v, false);
            }

            var cars = bg.BogusCarGenerator(divider);

            foreach (var v in cars)
            {
                AddVehicle(v, false);
            }

            //var motorc = bg.BogusMotorcycleGenerator(divider);
            //foreach (var v in motorc)
            //{
            //    AddVehicle(v, false);
            //}
            ui.Wait();
        }
    }
}