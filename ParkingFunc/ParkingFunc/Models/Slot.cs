using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingFunc.Models
{
    public class Slot
    {
        public int SlotId { get; set; }
        public string SlotName { get; set; }
        public int Status { get; set; } // 0: not available 1: available
    }   
}
