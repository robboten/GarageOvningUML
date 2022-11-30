using GarageOvningUML.Garage;
using GarageOvningUML.UI;
using static GarageOvningUML.Utilities.Utils;
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
            ui.Message("Welcome to the garage!");

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
                    $"{MenuHelpers.Add} : Add vehicle\n" +
                    $"{MenuHelpers.Remove} : Remove vehicle\n" +
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
                    case MenuHelpers.Remove: handler.SearchRemove(); break; //ta bort fordon
                    case MenuHelpers.List: handler.ListAll(); break; //Lista samtliga parkerade fordon
                    case MenuHelpers.ListType: handler.ListByType(); break; //Lista fordonstyper och hur många av varje som står i garaget
                    case MenuHelpers.Seed: handler.Seeder(); break; //Möjlighet att populera garaget med ett antal fordon från start.
                    case MenuHelpers.Search: handler.Search(); break;
                    case MenuHelpers.SearchProp: handler.SearchByProp(); break;
                    default: ui.Message("\nEnter a valid command"); break;
                }
            }
        }

    }
}
