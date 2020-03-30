using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Booking.ViewModels
{
    public class TrainByStationsAndDate
    {
        public int TrainNumber { get; set; }
        public string StartStation { get; set; }
        public string EndStation { get; set; }
        public DateTime DepartDateTime { get; set; }
        public TimeSpan FromTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public List<CarrTypesAndCountOfSeats> CarriageTypes { get; set; }
        public string StartInputStation { get; set; }
        public string EndInputStation { get; set; }
    }
}