using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingHouse.Models
{
    internal class Freespots
    {
        public int SlotID { get; }
        public int SlotNumber { get; set; }
        public int ElectricOutlet { get; set; }
        public int ParkingHouseId { get; set; }
        public string Plate { get; set; }
        public string Make { get; set; }
        public string Color { get; set; }
        public int? ParkingSlotsId { get; set; }

    }
}
