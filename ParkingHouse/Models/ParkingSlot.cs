using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingHouse.Models
{
    internal class ParkingSlot
    {
        public int Id { get; set; }
        public int SlotNumber { get; set; }
        public int ElectricOutlet { get; set; }
        public int ParkingHouseId { get; set; }
    }
}
