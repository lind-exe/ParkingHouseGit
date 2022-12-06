using ParkingHouse.Models;

namespace ParkingHouse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunCity();
        }

        static void RunCity()
        {
            bool runProgram = true;
          

            
            while (runProgram)
            {
                Console.Clear();
                Console.WriteLine($"1. Visa städer\n2. Lägg till stad\nA. Avsluta program");
                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':
                        Console.Clear();
                        var newCities = DatabasDapper.AllCities();
                        foreach (Models.City city in newCities)
                        {
                            Console.WriteLine($"{city.Id}\t{city.CityName}");
                        }
                        break;
                    case '2':
                        Console.Clear();
                        Console.Write("Ange vad staden heter: ");
                        var newCity = new Models.City
                        {
                            CityName = Console.ReadLine()
                        };
                        int rowAffected = DatabasDapper.InsertCity(newCity);
                        Console.WriteLine(rowAffected + " stad har lagts till");
                        break;
                    case 'A':
                    case 'a':
                        runProgram = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Fel input, prova igen");
                        break;
                }
                Console.WriteLine("Tryck enter för att fortsätta");
                Console.ReadLine();

            }





        }
    }
}