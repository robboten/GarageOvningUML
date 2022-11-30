// See https://aka.ms/new-console-template for more information

using GarageOvningUML;
internal class Program
{
    
    public static void Main(string[] args)
    {
        var man = new Manager();

        man.Init();
    }
}

// --- Glömde denna då jag tänkte att egenskaperna i Vehicle bara, inte subklasser. 
//● Söka efter fordon utifrån en egenskap eller flera (alla möjliga kombinationer från
//basklassen Vehicle). Exempelvis:
//○ Alla motorcyklar som är rosa och har 3 hjul.
//○ Alla lastbilar
