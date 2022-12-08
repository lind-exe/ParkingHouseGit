using ParkingHouse.Models;

namespace ParkingHouse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunCity();
        }








        // Switch Main Meny
        static void RunCity()
        {
            bool runProgram = true;
            while (runProgram)
            {
                Console.Clear();
                Console.WriteLine($"1. View cities.\n2. Add city\n3. Manage parkinghouses & spots.\n4. Add parkinghouse.\n5. Manage cities.\n6. Manage cars\n7. SQL Queries" +
                    $"\nA. Exit application");
                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':                       // View cities
                        Console.Clear();
                        var newCities = DatabasDapper.AllCities();
                        foreach (Models.City city in newCities)
                        {
                            Console.WriteLine($"{city.Id}\t{city.CityName}");
                        }
                        break;
                    case '2':                       // Add City
                        Console.Clear();
                        Console.Write("Input city name: ");
                        var newCity = new Models.City
                        {
                            CityName = Console.ReadLine()
                        };
                        int rowAffected = DatabasDapper.InsertCity(newCity);
                        Console.WriteLine(rowAffected + " city has been added.");
                        break;
                    case '3':                       // View ParkingHouse
                        Console.Clear();
                        var newHouses = DatabasDapper.AllParkingHouses();
                        foreach (Models.ParkingHouse pHouse in newHouses)
                        {
                            Console.WriteLine($"{pHouse.Id}\t{pHouse.HouseName}");
                        }
                        ManageSlots();
                        break;
                    case '4':                       // Add ParkingHouse
                        Console.Clear();
                        int rowAffected1 = DatabasDapper.InsertParkingHouse();
                        Console.WriteLine(rowAffected1 + " parkinghouse has been added.");
                        break;
                    case '5':                       // Manage Cities
                        Console.Clear();
                        var allCities = DatabasDapper.AllCities();
                        foreach (Models.City city in allCities)
                        {
                            Console.WriteLine($"{city.Id}\t{city.CityName}");
                        }
                        ManageCities();
                        break;
                    case '6':                       // Manage Cars
                        Console.Clear();
                        ManageCars();
                        break;
                    case '7':
                        Console.Clear();
                        SQLQueries();
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













































        // E Spots per House - E Spots per City - # of Parked Cars - # of Free Spots
        private static void SQLQueries()
        {
            
            Console.WriteLine("1. Electric spots per house\n2. Electric spots per city\n3. # of parked cars in total\n4. # of free spots per city");
            var key = Console.ReadKey(true);
            switch (key.KeyChar)
            {
                // ELectric Spot Per House
                case '1':
                    Console.Clear();
                    var query1 = DatabasDapper.ElectricSpots();
                    Console.ForegroundColor= ConsoleColor.Blue;
                    Console.WriteLine("City   \t\t\tParking house   \t# of electric spots");
                    Console.ResetColor();
                    foreach (var house in query1)
                    {
                        Console.WriteLine($"{house.CityName}   \t\t{house.HouseName}    \t\t{house.ElectricSpots} st");
                    }
                    break;
                    // E Spot Per City
                case '2':
                    Console.Clear();
                    var query2 = DatabasDapper.ElectricSpots2();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("City   \t\t# of electric spots");
                    Console.ResetColor();
                    foreach (var city in query2)
                    {
                        Console.WriteLine($"{city.CityName}   \t{city.ElectricSpots} st");
                    }
                    break;
                    // # of parked Cars
                case '3':
                    Console.Clear();
                    var query3 = DatabasDapper.QUERYListParkedCars();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Plate\tMake\tColor\tSlotID\tParking house\tCity");
                    Console.ResetColor();
                    foreach (var car in query3)
                    {
                        Console.WriteLine($"{car.Plate}\t{car.Make}\t{car.Color}\t{car.ParkingSlotId}\t{car.HouseName}    \t{car.CityName}");
                    }
                    break;
                    // Free spots Per City
                case '4':
                    Console.Clear();
                    var query4 = DatabasDapper.QUERYListFreeSpots();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("City   \t\t# of free spots");
                    Console.ResetColor();
                    foreach (var car in query4)
                    {
                        Console.WriteLine($"{car.CityName}    \t{car.FreeSpots}");
                    }
                    break;
            }
        }

        // Park, Unpark and Create Car
        private static void ManageCars()
        {
            Console.WriteLine("1. Park car\n2. Unpark car\n3. Create car");
            var key = Console.ReadKey(true);
            switch (key.KeyChar)
            {
                // Par car
                case '1':
                    Console.Clear();
                    var allCities = DatabasDapper.AllCities();
                    foreach (Models.City city in allCities)
                    {
                        Console.WriteLine($"{city.Id}\t{city.CityName}");
                    }
                    ManageCities();
                    Console.WriteLine();
                    var cars = DatabasDapper.AllCars();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Car ID\tPlate\tMake     \tColor\tParking spot\t");
                    Console.ResetColor();
                    foreach (Models.Car car in cars)
                    {
                        Console.WriteLine($"{car.Id}\t{car.Plate}\t{car.Make}     \t{car.Color}\t" +
                            (car.ParkingSlotsId == null ? "Not Parked" : "Parked"));
                    }
                    Console.WriteLine();
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    Console.ResetColor();
                    Console.Write("Input the SlotID: ");
                    int input1 = 0;
                    input1 = TryNumber(input1);
                    Console.Write("Input the ID of the car: ");
                    int input2 = 0;
                    input2 = TryNumber(input2);
                    DatabasDapper.ParkCar(input1, input2);
                    Console.WriteLine("Car has been parked");
                    break;
                    // Unpark Car
                case '2':
                    var cars2 = DatabasDapper.ListParkedCars();
                    foreach(Models.CarUnpark car in cars2)
                    {
                        Console.WriteLine($"{car.Id}\t{car.Plate}\t{car.Make}\t{car.Color}\t{car.ParkingSlotsId}\t{car.HouseName}\t{car.CityName}");
                    }
                    Console.Write("Input the plate[ABC123] to unpark the car: ");
                    string input3 = Console.ReadLine().ToUpper();
                    int affectedRows = 0;
                    affectedRows=DatabasDapper.UnParkCar(input3);
                    Console.WriteLine($"Rows affected: " +affectedRows);
                    break;
                    // Create Car
                case '3':
                    CreateCar();
                    break;
            }
        }

        // Create Car
        private static void CreateCar()
        {
            Console.WriteLine("Create Car");
            Console.Write("Input Plate[ABC123]: ");
            string input1 = Console.ReadLine().ToUpper().Trim();
            Console.Write("Input Make: ");
            string input2 = Console.ReadLine().Trim();
            Console.Write("Input Color: ");
            string input3 = Console.ReadLine().Trim();
            int rowsAffected = 0;
            rowsAffected = DatabasDapper.CreateCars(input1, input2, input3);
            Console.WriteLine(rowsAffected + " cars created with plate " + input1);
        }

        // Add slots and see if they are electric
        private static void ManageSlots()
        {
            // Show if parking space have electric outlet
            Console.Write("\nInput Id-number of the parkinghouse you wish to manage: ");
            int input = 0;
            input = TryNumber(input);
            Console.Clear();
            var newSpot = DatabasDapper.AllParkingSlots(input);
            Console.ForegroundColor= ConsoleColor.Blue;
            Console.WriteLine("Id\tSlotNumber\tElectric");
            Console.ResetColor();
            foreach (Models.ParkingSlot spot in newSpot)
            {
                Console.WriteLine($"{spot.Id}\t{spot.SlotNumber}\t\t" + (spot.ElectricOutlet == 0 ? "Does not have electric outlet" : "Has electric outlet"));
            }

            // Add Parkingspot
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

        // View Cities and ParkingHouses
        private static void ManageCities()
        {
            Console.Write("\nInput Id-number of the city you wish to manage: ");
            int input = 0;
            input = TryNumber(input);

            Console.Clear();
            var selectedHouse = DatabasDapper.AllParkingHouses(input);
            foreach (Models.ParkingHouse house in selectedHouse)
            {
                Console.WriteLine($"{house.Id}\t{house.HouseName}\t");
            }
            Console.WriteLine("\nInput Id-number of the parkinghouse you wish to manage.\n");
            int input2 = 0;
            input2 = TryNumber(input2);
            Console.Clear();
            var newSpot = DatabasDapper.AllParkingSlots2(input2);
            Console.ForegroundColor= ConsoleColor.Blue;
            Console.WriteLine("SlotId\tSlotnumber\tElectric\t\tParking house\tParking spot\tPlate\tMake     \tColor");
            Console.ResetColor();
            foreach (Models.Freespots spot in newSpot)
            {
                
                Console.WriteLine($"{spot.SlotID}\t{spot.SlotNumber}\t\t" + (spot.ElectricOutlet == 0 ? "No electric outlet" : "Has electric outlet") +
                    $"\t{spot.ParkingHouseId}\t\t" +
                    (spot.ParkingSlotsId == null ? "Free spot" : "Occupied") + $"\t{spot.Plate}\t{spot.Make}     \t{spot.Color}");
            }
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.ResetColor();
        }



        // Write number safely
        internal static int TryNumber(int number)
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