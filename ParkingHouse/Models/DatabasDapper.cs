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
        public static int InsertParkingSlot(Models.ParkingSlot ph)
        {
            var sql = $"insert into ParkingSlot(HouseName) values ('{ph.HouseName}')";

            int affectedRows = 0;

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                affectedRows = connection.Execute(sql);
                connection.Close();
            }
            return affectedRows;
        }
    }
}
