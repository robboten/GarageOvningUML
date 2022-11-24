// See https://aka.ms/new-console-template for more information

using GarageOvningUML;

Console.WriteLine("Hello, Garage!");

var man = new Manager();

man.Init();


///
/// I mer programmerings vänliga termer skall vi alltså som minimum ha:

//● Ett användargränssnitt som låter oss använda funktionaliteten hos garaget.Här
//sker all interaktion med användaren.

//● Vi programmerar inte direkt mot konkreta typer så vi använder oss av Interfaces
//för det tex IUI, IHandler, IVehicle. (Tips är att bryta ut till interface när
//implementationen är klar om man tycker den här delen är svår
///


//class Vehicle

//Kravspecifikation
//Fordonen ska implementeras som klassen Vehicle och subklasser till den.
//● Vehicle innehåller samtliga egenskaper som ska finnas i samtliga fordonstyper.
//registreringsnummer, färg, antal hjul och andra egenskaper ni kan komma på.
//● Registreringsnumret är unikt




//Funktionalitet
//Det ska gå att:
//● Lista samtliga parkerade fordon
//● Lista fordonstyper och hur många av varje som står i garaget
//● Lägga till och ta bort fordon ur garaget
//● Sätta en kapacitet (antal parkeringsplatser) vid instansieringen av ett nytt garage
//● Möjlighet att populera garaget med ett antal fordon från start.
//● Hitta ett specifikt fordon via registreringsnumret. Det ska gå fungera med både
//ABC123 samt Abc123 eller AbC123.
//● Söka efter fordon utifrån en egenskap eller flera (alla möjliga kombinationer från
//basklassen Vehicle). Exempelvis:
//○ Alla svarta fordon med fyra hjul.
//○ Alla motorcyklar som är rosa och har 3 hjul.
//○ Alla lastbilar
//○ Alla röda fordon
//● Användaren ska få feedback på att saker gått bra eller dåligt. Till exempel när vi
//parkerat ett fordon vill vi få en bekräftelse på att fordonet är parkerat. Om det inte
//går vill användaren få veta varför.
//Programmet ska vara en konsol applikation med ett textbaserat användargränssnitt.
//Från gränssnittet skall det gå att:
//● Navigera till samtlig funktionalitet från garage via gränssnittet
//● Skapa ett garage med en användar specificerad storlek
//● Det skall gå att stänga av applikationen från gränssnittet
//Applikationen skall fel hantera indata på ett robust sätt, så att den inte kraschar vid
//felaktig inmatning eller användning



//Unit testing
//Testen ska skapas i ett eget testprojekt. Vi begränsar oss till att testa de publika
//metoderna i klassen Garage. (Att skriva test för hela applikationen ses som en extra
//uppgift om tid finns)
//Experimentera gärna med att skriva testen före ni implementerat funktionaliteten!
//Använd er sedan ctrl . för att generera era objekt och metoder.
//Implementera sen funktionaliteten tills testet går igenom.
//Strukturera testen enligt principen.
//1. Arrange här sätter ni upp det som ska testas, instansierar objekt och inputs
//2. Act här anropar ni metoden som ska testas
//3. Assert här kontrollerar ni att ni fått det förväntade resultatet
//Tänk även på att namnge testen på ett förklarande sätt. När ett test inte går igenom vill vi
//veta vad som inte fungerat enbart genom att se på testmetod namnet.
//Exempelvis:
//[MethodName_StateUnderTest_ExpectedBehavior]
//Public void Sum_NegativeNumberAs1stParam_ExceptionThrown()
