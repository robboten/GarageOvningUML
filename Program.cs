// See https://aka.ms/new-console-template for more information

using GarageOvningUML;
using GarageOvningUML.UI;

internal class Program
{
    
    public static void Main(string[] args)
    {
        var ui = new ConsoleUI();
        var man = new Manager(ui);

        man.Init();
    }
}

// --- Glömde denna då jag tänkte att egenskaperna i Vehicle bara, inte subklasser. 
//● Söka efter fordon utifrån en egenskap eller flera (alla möjliga kombinationer från
//basklassen Vehicle). Exempelvis:
//○ Alla motorcyklar som är rosa och har 3 hjul.
//○ Alla lastbilar
