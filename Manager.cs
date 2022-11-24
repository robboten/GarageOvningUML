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
            handler = new Handler();
            ui = new ConsoleUI();
        }

        public void Init()
        {
            //set up some test vehicles
            handler.Seeder();

            ui.Clear();
            MainLoop();
        }

        private void MainLoop()
        {
            while (true)
            {
                ui.Message("Welcome to the garage!\n");
                switch (ui.InputChar())
                {
                    //Lista samtliga parkerade fordon
                    case 'a':
                        handler.ListAll();
                        break;
                    //Lista fordonstyper och hur många av varje som står i garaget
                    case 'b':
                        handler.ListByType();
                        break;
                    //Lägga till fordon
                    case 'c':
                        //handler.ListAll();
                        break;
                    //ta bort fordon
                    case 'd':
                        //handler.ListAll();
                        break;

                    //● Sätta en kapacitet(antal parkeringsplatser) vid instansieringen av ett nytt garage
                    //● Möjlighet att populera garaget med ett antal fordon från start.

                    //● Hitta ett specifikt fordon via registreringsnumret. Det ska gå fungera med både ABC123 samt Abc123 eller AbC123.
                    //● Söka efter fordon utifrån en egenskap eller flera (alla möjliga kombinationer från basklassen Vehicle). Exempelvis:
                    //○ Alla svarta fordon med fyra hjul.
                    //○ Alla motorcyklar som är rosa och har 3 hjul.
                    //○ Alla lastbilar
                    //○ Alla röda fordon
                    default: break;
                }

            }
        }

        public void MakeGarage()
        {
            //for dynamic creation of a garage
            throw new NotImplementedException();
        }

        public void GetCommand()
        {
            ui.InputChar();
        }


    }
}