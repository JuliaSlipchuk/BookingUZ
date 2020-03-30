using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.ViewModelsAndSubViewModels
{
    public class TicketVM
    {
        public int TrainNumber { get; set; }
        public string CarriageType { get; set; }
        public int CarriageOrder { get; set; }
        public int SeatNumber { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public string StartStation { get; set; }
        public string EndStation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonType { get; set; }
        public DateTime BirthDate { get; set; }
        public bool HaveTea { get; set; }
        public bool HaveBed { get; set; }
        public decimal Price { get; set; }
    }
}