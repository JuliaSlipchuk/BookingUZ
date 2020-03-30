using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.ViewModels
{
    public class FreeSeatInCarriage
    {
        public int CarriageOrder { get; set; }
        public List<int> SeatNumbers { get; set; }
    }
}