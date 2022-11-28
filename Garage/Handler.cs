using GarageOvningUML.UI;
using GarageOvningUML.Vehicles;
using System.Reflection;
using System.Xml.Linq;
using System.Xml;

namespace GarageOvningUML.Garage
{
    public class Handler : IHandler
    {
        public GenericGarage<Vehicle> GenGarage; //nullable good here? I don't want it set before init
        private readonly IUI ui;
        public Handler(IUI ui)
        {
            this.ui = ui;

            //this.ui.Message("Garage setup");
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
        public void AddVehicleByInput()
        {

            //before anything... check to see if there is space left in garage


            string color;
            int wheels;

            ui.Clear();
            ui.Message("Choose one of the following types to add:\n");

            //loop thru the lenght of the enum
            int enumlen = Enum.GetNames(typeof(VehicleTypes)).Length;

            for (int i = 1; i <= enumlen; i++)
            {
                ui.Message($"{i}:{(VehicleTypes)i}\n");
            }

            //get the int value from the input
            int typeInt= ui.InputLoopInt(ui.InputChar().ToString());
            //---- need to check if in range...

            //ui.Message(enumlen.ToString());
            //ui.Message(VehicleTypes.Car.ToString());
            //ui.Message(((VehicleTypes)1).ToString());
            //ui.Message(((int)VehicleTypes.Car).ToString());
            //var vtype = ui.EnumValidation($"Type of vehicle : ");

            //ui.Message(vtype.ToString());

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

            Type type = null;

            //if (typeInt == 0) return;

            ui.Message("typeint here : " + typeInt.ToString());
            switch (typeInt)
            {
                case 1:
                    type = typeof(Car);
                    break;
                case 2:
                    type = typeof(Bus);
                    break;
                case 3:
                    type = typeof(Motorcycle);
                    break;
                case 4:
                    type = typeof(Airplane);
                    break;
                case 5:
                    type = typeof(Boat);
                    break;
            }

            //don't understand dynamic, tho it looks useful...
            //dynamic carry = new Car();

            //var ctors = type.GetConstructors(System.Reflection.BindingFlags.Public);

            var obj = Activator.CreateInstance(type) as Vehicle;
            obj.RegistrationNr = regNr;
            obj.WheelsNr = wheels;
            obj.ColorStr = color;

            PropertyInfo[] propNames = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            foreach(var prop in propNames)
            {
                //lucky for me all properties are of int types...Not the best in long terms but for now.
                type.GetProperty(prop.Name).SetValue(obj, ui.InputLoopInt(prop.Name + ": "), null);
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
                //ui.Message($"{v.RegistrationNr} {v.Color} {v.WheelsNr} \n");
                ui.Message(v.VehicleInfo() + "\n");
            }
            ui.Wait();
        }

        public void ListByType()
        {
            ui.Clear();
            ui.Message($"Listing types of vehicles...\n");

            ////all not null
            //var notNullList= GenGarage.GetArr().Where(x=> x != null).ToList();

            ////get all types
            //var typesList = notNullList.Select(x => x.GetType()).Distinct();

            ////nr of types
            //int nrTypes = typesList.Count();

            //ui.Message($"{nrTypes} different types of vehicles in the garage.\n");

            ////get amount of vehicles in each type
            //foreach (var type in typesList)
            //{
            //    var eachType = GenGarage.GetArr().Where(x => x.GetType() == type).ToList();
            //    ui.Message($"{eachType.Count} of vehicles are of the type {type.Name}\n");
            //}    

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
                //ui.Message(v.VehicleInfo() + "\n");
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