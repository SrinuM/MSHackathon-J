using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingFunc.Models
{
    public class Floor
    {
        public int BuildingId { get; set; }
        public int FloorId { get; set; }
        public string FloorName { get; set; }
        public int Slots { get; set; }
    }
}
