using Booking.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models.DBModels
{
    [Table("Ticket")]
    public class Ticket : BaseDBEntity, ITicket
    {
        public int PersonID { get; set; }
        public int StationOnRouteStartID { get; set; }
        public int StationOnRouteEndID { get; set; }
        public int ReservationSeatID { get; set; }
        public System.DateTime DepartureDateTime { get; set; }
        public Nullable<System.DateTime> ArrivalDateTime { get; set; }
        public Nullable<decimal> Price { get; set; }

        //public virtual Person Person { get; set; }
        //public virtual ReservationSeat ReservationSeat { get; set; }
        //public virtual StationOnRoute StationOnRoute { get; set; }
        //public virtual StationOnRoute StationOnRoute1 { get; set; }
    }
}
