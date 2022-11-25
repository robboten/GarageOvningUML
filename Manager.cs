using static System.Net.WebRequestMethods;
using System.Security.Cryptography;
using System;
using GarageOvningUML.UI;
using GarageOvningUML.Garage;

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
            //set up some test vehicles
            //handler.Seeder();

            ui.Clear();
            MainLoop();
        }

        private void MainLoop()
        {
            while (true)
            {
                ui.Clear() ;
                ui.Message("Press one of following:\n");
                ui.Message(
                    "List all parked vehicles: l\n" +
                    "List the types of vehicles parked: t\n" +
                    "Add vehicle: +\n" +
                    "Remove vehicle: -\n" +
                    "Randomize vehicles in garage: r\n" +
                    "Search for vehicle: s\n");
                switch (ui.InputChar())
                {
                    //Lista samtliga parkerade fordon
                    case 'l':
                        handler.ListAll();
                        break;
                    //Lista fordonstyper och hur många av varje som står i garaget
                    case 't':
                        handler.ListByType();
                        break;
                    //Lägga till fordon
                    case '+':
                        //Add();
                        break;
                    //ta bort fordon
                    case '-':
                        //Remove();
                        break;
                    //Möjlighet att populera garaget med ett antal fordon från start.
                    case 'r':
                        handler.Seeder(); //arbitrary numbers?
                        break;
                    case 's':
                        Search();
                        break;
                    default: break;
                }

            }
        }


        public void Search() {
            ui.Clear();
            //while true loop

            ui.Message($"Input registration number to search for: ");
            var sStr = ui.InputLong();
            handler.Search( sStr );

            //● Hitta ett specifikt fordon via registreringsnumret. Det ska gå fungera med både ABC123 samt Abc123 eller AbC123.
            //● Söka efter fordon utifrån en egenskap eller flera (alla möjliga kombinationer från basklassen Vehicle). Exempelvis:
            //○ Alla svarta fordon med fyra hjul.
            //○ Alla motorcyklar som är rosa och har 3 hjul.
            //○ Alla lastbilar
            //○ Alla röda fordon
        }


    }
}