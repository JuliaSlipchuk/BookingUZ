using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.ViewModels
{
    public class SeatInCarr
    {
        public int TrainNumber { get; set; }
        public string CarriageType { get; set; }
        public string BeginStation { get; set; }
        public string EndStation { get; set; }
        public string BeginInputStation { get; set; }
        public string EndInputStation { get; set; }
        public string DepartDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string CarriageTypesJson { get; set; }
        public List<CarrTypesAndCountOfSeats> CarriageTypes { get; set; }
        public int CarrOrder { get; set; }
        public Dictionary<int, bool> SeatsNumbers { get; set; }
        public List<int> CarrOrdersForActionLink { get; set; }
    }
}