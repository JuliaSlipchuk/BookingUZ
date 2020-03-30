using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models.ModelsReturnFromProc
{
    public class TrainByTwoStationsAndDate
    {
        public int TrainNumber { get; set; }
        public string StartStation { get; set; }
        public string EndStation { get; set; }
        public DateTime DepartDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
