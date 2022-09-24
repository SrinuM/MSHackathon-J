using System.Collections.Generic;

namespace ParkingFunc.Models
{
    public class SlotResponse
    {
        public List<Slot> Slots { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}
