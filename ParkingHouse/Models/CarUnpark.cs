    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingHouse.Models
{
    internal class CarUnpark
    {
        public int Id { get; set; }
        public string Plate { get; set; }
        public string Make { get; set; }
        public string Color { get; set; }
        public int ParkingSlotsId { get; set; }
        public int ParkingSlotId { get; set; }
        public string HouseName { get; set; }
        public string CityName { get; set; }
        public int FreeSpots { get; set; }


    }
}
