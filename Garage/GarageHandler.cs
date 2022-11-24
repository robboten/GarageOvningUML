using GarageOvningUML.Enums;
using GarageOvningUML.Vehicles;
using System.Collections.Generic;

namespace GarageOvningUML.Garage
{
    public class GarageHandler : IHandler
    {
        //public Garage<Vehicle> Garage1;
        public GenericGarage<Vehicle> GenGarage;
        public GarageHandler()
        {
           // Garage1 = new(6); //make this dynamic
            GenGarage = new GenericGarage<Vehicle>(12);
        }

        public void AddVehicle(Vehicle v)
        {
            //get return for successful adding..
            //Garage1.Add(v);
            if (GenGarage.Add(v))
                Console.WriteLine($"Succesfully added {v.RegistrationNr}");
        }

        //need a check to see if the vehicle reg exists already

        public void RemoveVehicle(Vehicle v)
        {
            //get return for successful..
            GenGarage.Remove(v);
        }

        public void ListAll()
        {
            foreach (var v in GenGarage)
            {
                Console.WriteLine(v.RegistrationNr);
            }

            //foreach (Vehicle v in Garage1)
            //{
            //    Console.WriteLine(v.RegistrationNr);
            //}
        }

        public void ListByType()
        {
            //works only if the list doesn't have empty spaces!!!

            //nr of types
            Console.WriteLine(GenGarage.GetArr().Select(x => x.GetType()).Distinct().Count());

            //foreach (Vehicle v in Garage1)
            //{
            //    Console.WriteLine(v.RegistrationNr);
            //}
        }

        public void Search()
        {
            throw new NotImplementedException();
        }

        public void Seeder()
        {
            Bus[] busses =
            {
                    new("gex466",Colors.White, 8000, 40, 4),
                    new("xee446",Colors.Pink, 9000, 60, 6),
                    new("gec476",Colors.Silver, 8000, 40,4),
                    new("fcc466",Colors.Brown, 12000, 80, 6),
                    new("gge477",Colors.Purple, 8000, 40, 4),
                    new("bnr543",Colors.White, 18000, 180, 10)
                };
            foreach (var v in busses)
            {
                AddVehicle(v);
            }

            Car[] cars =
            {
                    new("bax566",Colors.Yellow, 8000, 4, 4),
                    new("bae546",Colors.Black, 9000, 6, 4),
                    new("bac576",Colors.Blue, 8000, 4,4),
                    new("bac566",Colors.Green, 12000, 2, 4),
                    new("bae577",Colors.Purple, 8000, 1, 4),
                    new("bar543",Colors.White, 18000, 1, 4)
                };
            foreach (var v in cars)
            {
                AddVehicle(v);
            }
        }
    }
}