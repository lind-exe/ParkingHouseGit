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
                Console.WriteLine($"1. Visa städer\n2. Lägg till stad\n3. För att visar parkeringhus\n4. För att lägga till ett parkeringhus" +
                    $"\nA. Avsluta program");
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
                    case '3':
                        Console.Clear();
                        var newHouses=DatabasDapper.AllParkingHouses();
                        foreach(Models.ParkingHouse pHouse in newHouses)
                        {
                            Console.WriteLine($"{pHouse.Id}\t{pHouse.HouseName}");
                        }
                        break;
                    case '4':
                        Console.Clear();
                        Console.Write("Ange vad parkeringhuset heter: ");
                        var newParkinghouse = new Models.ParkingHouse
                        {
                            HouseName = Console.ReadLine()
                        };
                        int rowAffected1 =DatabasDapper.InsertParkingHouse(newParkinghouse);
                        Console.WriteLine(rowAffected1+" parkeringhus har lagts till.");
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