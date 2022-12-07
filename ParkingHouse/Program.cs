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
                Console.WriteLine($"1. View cities.\n2. Add city\n3. Manage parkinghouses & spots.\n4. Add parkinghouse.\n5. Manage cities.\n6. Manage cars" +
                    $"\nA. Exit application");
                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':                       //Visa cities
                        Console.Clear();
                        var newCities = DatabasDapper.AllCities();
                        foreach (Models.City city in newCities)
                        {
                            Console.WriteLine($"{city.Id}\t{city.CityName}");
                        }
                        break;
                    case '2':                       //Lägga till city
                        Console.Clear();
                        Console.Write("Input city name: ");
                        var newCity = new Models.City
                        {
                            CityName = Console.ReadLine()
                        };
                        int rowAffected = DatabasDapper.InsertCity(newCity);
                        Console.WriteLine(rowAffected + " city has been added.");
                        break;
                    case '3':                       //Se parkeringshus
                        Console.Clear();
                        var newHouses = DatabasDapper.AllParkingHouses();
                        foreach (Models.ParkingHouse pHouse in newHouses)
                        {
                            Console.WriteLine($"{pHouse.Id}\t{pHouse.HouseName}");
                        }
                        ManageSlots();
                        break;
                    case '4':                       //Lägga till parkeringshus
                        Console.Clear();
                        Console.Write("Input parkinghouse name: ");
                        var newParkinghouse = new Models.ParkingHouse
                        {
                            HouseName = Console.ReadLine()
                        };
                        int rowAffected1 = DatabasDapper.InsertParkingHouse(newParkinghouse);
                        Console.WriteLine(rowAffected1 + " parkinghouse has been added.");
                        break;
                    case '5':
                        Console.Clear();
                        var allCities = DatabasDapper.AllCities();
                        foreach (Models.City city in allCities)
                        {
                            Console.WriteLine($"{city.Id}\t{city.CityName}");
                        }
                        ManageCities();
                        break;
                    case '6':
                        Console.Clear();
                        ManageCars();
                        break;
                    case 'A':
                    case 'a':
                        runProgram = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Wrong input, try again.");
                        break;
                }
                Console.WriteLine("\nPress enter to continue.");
                Console.ReadLine();

            }





        }

        private static void ManageCars()
        {
            Console.WriteLine("1. Park car\n2. Unpark car\n3. Create car");
            var key = Console.ReadKey(true);
            switch (key.KeyChar)
            {
                case '1':
                    var allCities = DatabasDapper.AllCities();
                    foreach (Models.City city in allCities)
                    {
                        Console.WriteLine($"{city.Id}\t{city.CityName}");
                    }
                    ManageCities();
                    Console.WriteLine();
                    var cars = DatabasDapper.AllCars();
                    foreach(Models.Car car in cars)
                    {
                        Console.WriteLine($"{car.Id}\t{car.Plate}\t{car.Make}\t{car.Color}\t" +
                            (car.ParkingSlotsId == null ? "Not Parked" : "Parked"));
                    }
                    Console.Write("Input the ParkingslotID: ");
                    int input1 = 0;
                    input1 = TryNumber(input1);
                    Console.Write("Input´the ID of the car: ");
                    int input2 = 0;
                    input2 = TryNumber(input2);
                    DatabasDapper.ParkCar(input1, input2);
                    break;
                case '2':

                    break;
                case '3':

                    break;
            }
        }

        private static void ManageSlots()
        {
            Console.Write("\nInput Id-number of the parkinghouse you wish to manage: ");
            int input = 0;
            input = TryNumber(input);

            var newSpot = DatabasDapper.AllParkingSlots(input);
            foreach (Models.ParkingSlot spot in newSpot)
            {
                Console.WriteLine($"{spot.Id}\t{spot.SlotNumber}\t" + (spot.ElectricOutlet == 0 ? "Does not have electric outlet" : "Has electric outlet"));
            }

            Console.WriteLine("\n1. Add a parkingspot\n");

            var key = Console.ReadKey(true);


            switch (key.KeyChar)
            {
                case '1':
                    int answer1 = 0;
                    int answer2 = 0;
                    Console.Write("Input parking spot number: ");
                    answer1 = TryNumber(answer1);
                    Console.Write("Input 1 if it has an electric outlet, 0 if it doesn't: ");
                    answer2 = TryNumber2(answer2);
                    var newParkingslot = new Models.ParkingSlot
                    {
                        SlotNumber = answer1,
                        ElectricOutlet = answer2,
                        ParkingHouseId = input
                    };
                    int rowAffected2 = DatabasDapper.InsertParkingSlot(newParkingslot);
                    Console.WriteLine(rowAffected2 + " parking spot has been added. " + (newParkingslot.ElectricOutlet == 0 ? "Does not have electric outlet" : "Has electric outlet"));

                    break;
            }







        }

        private static void ManageCities()
        {
            Console.Write("\nInput Id-number of the city you wish to manage: ");
            int input = 0;
            input = TryNumber(input);

            var selectedHouse = DatabasDapper.AllParkingHouses(input);
            foreach (Models.ParkingHouse house in selectedHouse)
            {
                Console.WriteLine($"{house.Id}\t{house.HouseName}\t");
            }

            Console.WriteLine("\nInput Id-number of the parkinghouse you wish to manage.\n");
            int input2 = 0;
            input2 = TryNumber(input2);

            var newSpot = DatabasDapper.AllParkingSlots2(input2);
            Console.ForegroundColor= ConsoleColor.Blue;
            Console.WriteLine("SlotId\tSlotnumber\tElectric\t\tParking house\tParking spot\tPlate\tMake\tColor");
            Console.ResetColor();
            foreach (Models.Freespots spot in newSpot)
            {
                
                Console.WriteLine($"{spot.Id}\t{spot.SlotNumber}\t\t" + (spot.ElectricOutlet == 0 ? "No electric outlet" : "Has electric outlet") +
                    $"\t{spot.ParkingHouseId}\t\t" +
                    (spot.ParkingSlotsId == null ? "Free spot" : "Occupied") + $"\t{spot.Plate}\t{spot.Make}\t{spot.Color}");
            }
        }



        private static int TryNumber(int number)
        {
            bool correctInput = false;

            while (!correctInput)
            {
                if (!int.TryParse(Console.ReadLine(), out number))
                {
                    Console.WriteLine("Wrong input, try again.");
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
                    Console.WriteLine("Wrong input, try again.");
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