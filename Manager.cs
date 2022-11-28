using GarageOvningUML.Garage;
using GarageOvningUML.UI;
using static GarageOvningUML.UI.Utils;
using System.Text.RegularExpressions;

namespace GarageOvningUML
{
    public class Manager
    {
        private readonly IHandler handler;
        private readonly IUI ui;

        public Manager()
        {
            ui = new ConsoleUI();
            ui.Message("Welcome to the garage!\n");

            //not good in the constructor, but how else to set the handler before exiting it?
            handler = new Handler(ui);
        }

        public void Init()
        {
            MainLoop();
        }

        private void MainLoop()
        {
            RenderMenu();
        }

        private void RenderMenu()
        {
            while (true)
            {
                ui.Clear();
                ui.Message("Press one of following:\n" +
                    $"{MenuHelpers.Add} : Add vehicle \n" +
                    $"{MenuHelpers.Remove} : Remove vehicle \n" +
                    $"{MenuHelpers.List} : List all parked vehicles\n" +
                    $"{MenuHelpers.ListType} : List the types of vehicles parked\n" +
                    $"{MenuHelpers.Search} : Search for vehicle by registration number\n" +
                    $"{MenuHelpers.SearchProp} : Search for vehicle by properties\n" +
                    $"{MenuHelpers.Seed} : Randomize vehicles in garage\n" +
                    $"{MenuHelpers.Quit} : Quit");


                switch (ui.InputChar())
                {
                    case MenuHelpers.Quit: ui.Message("\nquitting..."); return;
                    case MenuHelpers.Add: handler.AddVehicleByInput(); break; //Lägga till fordon
                    case MenuHelpers.Remove: Remove(); break; //ta bort fordon
                    case MenuHelpers.List: handler.ListAll(); break; //Lista samtliga parkerade fordon
                    case MenuHelpers.ListType: handler.ListByType(); break; //Lista fordonstyper och hur många av varje som står i garaget
                    case MenuHelpers.Seed: handler.Seeder(); break; //Möjlighet att populera garaget med ett antal fordon från start.
                    case MenuHelpers.Search: Search(); break;
                    case MenuHelpers.SearchProp: SearchByProp(); break;
                    default: ui.Message("\nEnter a valid command"); break;
                }
            }
        }


        public void Remove()
        {
            throw new System.NotImplementedException();
        }

        //Hitta ett specifikt fordon via registreringsnumret. Det ska gå fungera med både ABC123 samt Abc123 eller AbC123.
        public void Search()
        {
            ui.Clear();

            var sStr = ui.InputLoop($"Input registration number to search for: ");
            handler.Search(sStr);
        }


        //● Söka efter fordon utifrån en egenskap eller flera (alla möjliga kombinationer från basklassen Vehicle). Exempelvis:
        //○ Alla svarta fordon med fyra hjul.
        //○ Alla motorcyklar som är rosa och har 3 hjul.
        //○ Alla lastbilar
        //○ Alla röda fordon
        public void SearchByProp()
        {
            throw new System.NotImplementedException();
        }

    }
}
