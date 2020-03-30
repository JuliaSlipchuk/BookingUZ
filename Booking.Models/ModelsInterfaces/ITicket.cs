using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models.Interfaces
{
    interface ITicket
    {
        int PersonID { get; set; }
        int ReservationSeatID { get; set; }
        int StationOnRouteStartID { get; set; }
        int StationOnRouteEndID { get; set; }
        DateTime DepartureDateTime { get; set; }
        Nullable<DateTime> ArrivalDateTime { get; set; }
        Nullable<decimal> Price { get; set; }
    }
}
