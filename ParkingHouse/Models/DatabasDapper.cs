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
        public static int InsertParkingHouse(Models.ParkingHouse ph)
        {
            var sql = $"insert into ParkingHouses(HouseName) values ('{ph.HouseName}')";

            int affectedRows = 0;

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                affectedRows = connection.Execute(sql);
                connection.Close();
            }
            return affectedRows;
        }

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
        public static List<Models.Freespots> AllParkingSlots2(int input)
        {
            var sql = $"SELECT * FROM ParkingSlots PS\r\nfull JOIN Cars C ON C.ParkingSlotsId = PS.Id WHERE PS.ParkingHouseId = {input}";
            var slots = new List<Models.Freespots>();

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                slots = connection.Query<Models.Freespots>(sql).ToList();
                connection.Close();
            }
            return slots;
        }
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

    }
}
