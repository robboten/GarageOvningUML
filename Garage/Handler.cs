using GarageOvningUML.UI;
using GarageOvningUML.Vehicles;
using System.Reflection;

namespace GarageOvningUML.Garage
{
    public class Handler : IHandler
    {
        public GenericGarage<IVehicle> GenGarage; //nullable good here? I don't want it set before init
        private readonly IUI ui;
        public Handler(IUI ui)
        {
            this.ui = ui;

            var s = MakeGarage();
            GenGarage = new GenericGarage<IVehicle>(s);
        }

        public int MakeGarage()
        {
            //Break this out
            //Sätta en kapacitet(antal parkeringsplatser) vid instansieringen av ett nytt garage
            ui.Message("Setting up a new garage...\n");
            return ui.InputLoopInt("How many parking slots would you like to have?\n");
        }


        public Dictionary<string, Type> Dict()
        {
            var vNS = typeof(IVehicle).Namespace;

            var dict = new Dictionary<string, Type>();
            dict.Add($"{vNS}.Car", typeof(Car));
            return dict;
        }

        //need a check to see if the vehicle reg exists already
        public void AddVehicleByInput()
        {
            string color;
            int wheels;

            ui.Clear();
            ui.Message("Choose one of the following types to add:\n");


            //list of all vehicle types
            IEnumerable<Type> VehicleClasses = AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(assembly => assembly.GetTypes())
                       .Where(type => type.IsSubclassOf(typeof(Vehicle)));

            int classesLen = VehicleClasses.Count();

            for (int x=0; x < classesLen; x++)
            {
                ui.Message($"{x}:{VehicleClasses.ElementAt(x).Name}\n");
            }

            int typeInt = ui.InputLoopIntRange("Select vehicle type: ", 1, classesLen);

            //ui.Wait();

            //--------before anything... check to see if there is space left in garage
            //ui.Message(typeof(Car).FullName);

            //ui.Message(Type.GetType("GarageOvningUML.Vehicles.Car").FullName);

            //ui.Message(typeof(IVehicle).Namespace);

            ////loop thru the lenght of the enum
            //int enumlen = Enum.GetNames(typeof(VehicleTypes)).Length;

            //for (int i = 1; i <= enumlen; i++)
            //{
            //    ui.Message($"{i}:{(VehicleTypes)i}\n");
            //}

            ////get the int value from the input
            //int typeInt = ui.InputLoopIntRange("Select vehicle type: ", 1, enumlen);

            ui.Message("Please input the following: \n");
            var regNr = ui.RegNrValidation($"Registration number (in the format ABC123): ");

            if (GenGarage.Any(vh => vh.RegistrationNr == regNr))
            {
                ui.Message("Registration number is already in use.");
                ui.Wait();
                return;
            }

            color = ui.InputLoop($"Color: ");
            wheels = ui.InputLoopInt($"Number of wheels: ");

            Type type = VehicleClasses.ElementAt(typeInt);

            //switch (typeInt)
            //{
            //    case (int)VehicleTypes.Car:
            //        type = typeof(Car);
            //        break;
            //    case 2:
            //        type = typeof(Bus);
            //        break;
            //    case 3:
            //        type = typeof(Motorcycle);
            //        break;
            //    case 4:
            //        type = typeof(Airplane);
            //        break;
            //    case 5:
            //        type = typeof(Boat);
            //        break;
            //}

            //don't understand dynamic, tho it looks useful...
            //dynamic carry = new Car();
            //var ctors = type.GetConstructors(System.Reflection.BindingFlags.Public);

            var obj = Activator.CreateInstance(type) as IVehicle;
            obj.RegistrationNr = regNr;
            obj.WheelsNr = wheels;
            obj.ColorStr = color;

            PropertyInfo[] propNames = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in propNames)
            {
                if(prop.GetSetMethod() != null)
                {
                    //not very safe to do it like this if someone would add a new public property that is not int
                    type.GetProperty(prop.Name).SetValue(obj, ui.InputLoopInt(prop.Name + ": "), null);
                }  
            }

            ////get return for successful adding..
            if (GenGarage.Add(obj))
                ui.Message($"Succesfully added {obj.RegistrationNr}\n");
            else
                ui.Message($"Something went wrong trying to add {obj.RegistrationNr}\n");

            ui.Wait();
            //make success and error handlers for ui?
        }

        public void AddVehicle(Vehicle v, bool verbose = true)
        {
            if (GenGarage.Any(vh => vh.RegistrationNr == v.RegistrationNr))
            {
                ui.Message($"Registration number {v.RegistrationNr} is already in use. Try again.\n");

                ui.Wait();
                return;
            }

            //--- check for full garage

            if (GenGarage.Add(v))
            {
                if (verbose)
                {
                    ui.Message($"Succesfully added {v.RegistrationNr}\n");
                    ui.Wait();
                }
            }
            else
            {
                
                ui.Message($"Something went wrong trying to add {v.RegistrationNr}\n");
                ui.Wait();
            }

            //make success and error handlers for ui?
        }

        public void SearchRemove(string sStr)
        {
            ui.Message($"Searching for vehicles...\n");

            var v = GenGarage.GetArr().Where(x => string.Equals(x.RegistrationNr.ToLower(), sStr.ToLower(), StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (v != null)
            {
                ui.Message($"Found: {v.VehicleInfo()}\n");
                RemoveVehicle(v);
            }
            else
            {
                ui.Message($"No mathing vehicles for {sStr}\n");
                ui.Wait();
            }
        }

        public void RemoveVehicle(IVehicle v)
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
                ui.Message(v.VehicleInfo() + "\n");
            }
            ui.Wait();
        }

        public void ListByType()
        {
            ui.Clear();
            ui.Message($"Listing types of vehicles...\n");

            foreach (var group in GenGarage.GroupBy(v => v.GetType().Name))
            {
                Console.WriteLine($"{group.Key} : {group.Count()}");
            }

            ui.Wait();
        }

        public void Search(string sStr)
        {
            ui.Message($"Searching for vehicles...\n");

            var v = GenGarage.GetArr().Where(x => string.Equals(x.RegistrationNr.ToLower(), sStr.ToLower(), StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (v != null)
            {
                ui.Message($"Found: {v.VehicleInfo()}\n");
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