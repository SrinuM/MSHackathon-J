using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingFunc.Models
{
    public class Building
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Floors { get; set; }
        public int Slots { get; set; }
    }
}
