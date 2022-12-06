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
                Console.WriteLine($"1. Visa städer\n2. Lägg till stad\n3. För att visa parkeringhus\n4. För att lägga till ett parkeringhus" +
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
                        ManageSlots();
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
                Console.WriteLine("\nTryck enter för att fortsätta");
                Console.ReadLine();

            }





        }

        private static void ManageSlots()
        {
            Console.Write("\nAnge Id-nummer på parkeringshus för att hantera parkeringsplatserna där: ");
            int input = 0;
            input = TryNumber(input);

            var newSpot = DatabasDapper.AllParkingSlots(input);
            foreach (Models.ParkingSlot spot in newSpot)
            {
                Console.WriteLine($"{spot.Id}\t{spot.SlotNumber}\t" + (spot.ElectricOutlet == 0 ? "Finns inte eluttag" : "Har eluttag"));
            }

            Console.WriteLine("\n1. för att lägga till parkeringsplats\n");

            var key = Console.ReadKey(true);


            switch (key.KeyChar)
            {
                case '1':
                    int answer1 = 0;
                    int answer2 = 0;
                    Console.Write("Ange nummer på parkeringsplats: ");
                    answer1 = TryNumber(answer1);
                    Console.Write("Ange om det finns eluttag på denna plats [1/0]: ");
                    answer2 = TryNumber2(answer2);
                    var newParkingslot = new Models.ParkingSlot
                    {
                        SlotNumber = answer1,
                        ElectricOutlet = answer2,
                        ParkingHouseId = input
                    };
                    int rowAffected2 = DatabasDapper.InsertParkingSlot(newParkingslot);
                    Console.WriteLine(rowAffected2 + " parkeringplatser har lagts till. " + (newParkingslot.ElectricOutlet == 0 ? "Den har inte eluttag" : "Den har eluttag"));

                    break;
            }







        }




        private static int TryNumber(int number)
        {
            bool correctInput = false;

            while (!correctInput)
            {
                if (!int.TryParse(Console.ReadLine(), out number))
                {
                    Console.WriteLine("Försök igen");
                }
                else
                {
                    correctInput = true;
                }
            }

            return number;
        }

        private static int TryNumber2(int number)
        {
            bool correctInput = false;

            while (!correctInput)
            {
                if (!int.TryParse(Console.ReadLine(), out number) || number > 1 || number < 0)
                {
                    Console.WriteLine("Försök igen");
                }
                else
                {
                    correctInput = true;
                }
            }

            return number;
        }
    }
}