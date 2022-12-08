using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Runtime.ConstrainedExecution;

namespace ParkingHouse.Models
{
    internal class DatabasDapper
    {
        static string connString = "data source=.\\SQLEXPRESS; initial catalog = Parking7; persist security info = True; Integrated Security = True;";


        // SELECT * FROM Cities
        public static List<Models.City> AllCities()
        {
            var sql = "SELECT * FROM Cities";
            var cities = new List<Models.City>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                cities = connection.Query<Models.City>(sql).ToList();
                connection.Close();
            }
            return cities;
        }

        // SELECT Electric Spots Per ParkingHouse
        public static List<Models.QUERYElectric> ElectricSpots()
        {
            var sql = "SELECT \r\n    C.CityName,\r\n    PH.HouseName,\r\n    CONCAT(COUNT(ElectricOutlet), ' ') ElectricSpots\r\nFROM \r\n    ParkingHouses PH \r\n    JOIN Cities C ON C.Id = PH.CityId\r\n    JOIN ParkingSlots PS ON PS.ParkingHouseId = PH.Id\r\nGROUP BY PH.HouseName, C.CityName";
            var query = new List<Models.QUERYElectric>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                query = connection.Query<Models.QUERYElectric>(sql).ToList();
                connection.Close();
            }
            return query;
        }

        // SELECT Electric Spots Per City
        public static List<Models.QUERYElectric> ElectricSpots2()
        {
            var sql = "SELECT \r\n    C.CityName,\r\n    CONCAT(COUNT(ElectricOutlet), ' ') ElectricSpots\r\nFROM \r\n    ParkingHouses PH \r\n    JOIN Cities C ON C.Id = PH.CityId\r\n    JOIN ParkingSlots PS ON PS.ParkingHouseId = PH.Id\r\nGROUP BY C.CityName";
            var query = new List<Models.QUERYElectric>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                query = connection.Query<Models.QUERYElectric>(sql).ToList();
                connection.Close();
            }
            return query;
        }

        // INSERT INTO Cities (add new city)
        public static int InsertCity(Models.City city)
        {
            var sql = $"insert into Cities(CityName) values ('{city.CityName}')";
            int affectedRows = 0;
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                affectedRows = connection.Execute(sql);
                connection.Close();
            }
            return affectedRows;
        }

        // SELECT * FROM ParkingHouses
        public static List<Models.ParkingHouse> AllParkingHouses()
        {
            var sql = "SELECT * FROM ParkingHouses";
            var houses = new List<Models.ParkingHouse>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                houses = connection.Query<Models.ParkingHouse>(sql).ToList();
                connection.Close();
            }
            return houses;
        }

        // SELECT * FROM ParkingHouse Per city
        public static List<Models.ParkingHouse> AllParkingHouses(int input)
        {
            var sql = $"SELECT * FROM ParkingHouses WHERE CityId = {input}";
            var houses = new List<Models.ParkingHouse>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                houses = connection.Query<Models.ParkingHouse>(sql).ToList();
                connection.Close();
            }
            return houses;
        }

        // Add new Parkinghouse (choose which city)
        public static int InsertParkingHouse()
        {
            var ph = new Models.ParkingHouse();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Input house name: ");
            ph.HouseName = Console.ReadLine();
            var allCities = DatabasDapper.AllCities();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            foreach (Models.City city in allCities)
            {
                Console.WriteLine($"{city.Id}\t{city.CityName}");
            }
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Input city ID");
            ph.CityId = Program.TryNumber(ph.CityId);
            var sql = $"insert into ParkingHouses(HouseName, CityId) values ('{ph.HouseName}', {ph.CityId})";
            int affectedRows = 0;
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                affectedRows = connection.Execute(sql);
                connection.Close();
            }
            return affectedRows;
        }

        // SELECT * FROM ParkingSlots Per ParkingHouse
        public static List<Models.ParkingSlot> AllParkingSlots(int input)
        {
            var sql = $"SELECT * FROM ParkingSlots WHERE ParkingHouseId = {input}";
            var slots = new List<Models.ParkingSlot>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                slots = connection.Query<Models.ParkingSlot>(sql).ToList();
                connection.Close();
            }
            return slots;
        }

        // See if Spots are free in a ParkingHouse
        public static List<Models.Freespots> AllParkingSlots2(int input)
        {
            var sql = $"\tSELECT PS.Id AS 'SlotID', PS.SlotNumber, PS.ElectricOutlet, " +
                $"PS.ParkingHouseId, C.ParkingSlotsId, C.Plate, C.Make, C.Color FROM ParkingSlots PS " +
                $"full JOIN Cars C ON C.ParkingSlotsId = PS.Id WHERE PS.ParkingHouseId = {input}";
            var slots = new List<Models.Freespots>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                slots = connection.Query<Models.Freespots>(sql).ToList();
                connection.Close();
            }
            return slots;
        }

        // Add ParkingSpot
        public static int InsertParkingSlot(Models.ParkingSlot ps)
        {
            var sql = $"insert into ParkingSlots(SlotNumber, ElectricOutlet, ParkingHouseId) values ('{ps.SlotNumber}', '{ps.ElectricOutlet}', '{ps.ParkingHouseId}')";
            int affectedRows = 0;
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                affectedRows = connection.Execute(sql);
                connection.Close();
            }
            return affectedRows;
        }

        // Park a Car
        public static int ParkCar(int input1, int input2)
        {
            var sql = $"UPDATE Cars SET ParkingSlotsId = {input1} WHERE Id = {input2}";
            int affectedRows = 0;
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                affectedRows = connection.Execute(sql);
                connection.Close();
            }
            return affectedRows;
        }

        // UnPark a Car
        public static int UnParkCar(string input1)
        {
            var sql = $"UPDATE Cars SET ParkingSlotsId = null WHERE Plate = '{input1}'";
            int affectedRows = 0;
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                affectedRows = connection.Execute(sql);
                connection.Close();
            }
            return affectedRows;
        }

        // Create a Car
        public static int CreateCars(string input1, string input2, string input3)
        {
            int affectedRows = 0;
            string sql = $"INSERT INTO Cars(Plate, Make, Color) " +
                $"VALUES ('{input1}', '{input2}', '{input3}')";
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                affectedRows = connection.Execute(sql);
                connection.Close();
            }
            return affectedRows;
        }

        // SELECT * FROM Cars 
        public static List<Models.Car> AllCars()
        {
            var sql = $"SELECT * FROM Cars ";
            var car = new List<Models.Car>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                car = connection.Query<Models.Car>(sql).ToList();
                connection.Close();
            }
            return car;
        }

        // See if parked Car have been removed
        public static List<Models.CarUnpark> ListParkedCars()
        {
            var sql = "SELECT \r\n    C.Id,\r\n\tC.Plate,\r\n\tC.Make,\r\n\tC.Color,\r\n\tC.ParkingSlotsId,\r\n\tPH.HouseName,\r\n\t" +
                "CI.CityName\r\nFROM Cars C\r\n    Full JOIN ParkingSlots PS ON C.ParkingSlotsId = PS.Id\r\n    " +
                "JOIN ParkingHouses PH ON PH.Id = PS.ParkingHouseId\r\n    JOIN Cities CI ON CI.Id = PH.CityId\r\n    WHERE C.ParkingSlotsId > 0";
            var cars =new List<Models.CarUnpark>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                cars= connection.Query<Models.CarUnpark>(sql).ToList();
                connection.Close();
            }
            return cars;
        }

        // # of parked cars in total
        public static List<Models.CarUnpark> QUERYListParkedCars()
        {
            var sql = "SELECT \r\n    C.Plate,\r\n    C.Make,\r\n    C.Color,\r\n    " +
                "PS.Id as ParkingSlotId,\r\n    PH.HouseName,\r\n    CI.CityName\r\nFROM \r\n    " +
                "CARS C\r\n    JOIN ParkingSlots PS ON PS.Id = C.ParkingSlotsId\r\n    " +
                "JOIN ParkingHouses PH ON PH.Id = PS.ParkingHouseId\r\n    " +
                "JOIN Cities CI ON CI.Id = PH.CityId\r\nWHERE C.ParkingSlotsId > 0 ORDER BY CI.CityName";
            var cars = new List<Models.CarUnpark>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                cars = connection.Query<Models.CarUnpark>(sql).ToList();
                connection.Close();
            }
            return cars;
        }

        // Free spots Per City
        internal static List<Models.CarUnpark> QUERYListFreeSpots()
        {
            var sql = "SELECT \r\n    C.CityName,\r\n   SUM(case when ParkingSlotsId is null then 1 else 0 end) AS 'FreeSpots'\r\nFROM \r\n    ParkingHouses PH \r\n    JOIN Cities C ON C.Id = PH.CityId\r\n    JOIN ParkingSlots PS ON PS.ParkingHouseId = PH.Id\r\n    LEFT JOIN Cars Ca ON Ca.ParkingSlotsId = PS.Id\r\nWHERE\r\n    Ca.ParkingSlotsId IS NULL\r\nGROUP BY C.CityName";
            var cars = new List<Models.CarUnpark>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                cars = connection.Query<Models.CarUnpark>(sql).ToList();
                connection.Close();
            }
            return cars;
        }
    }
}
