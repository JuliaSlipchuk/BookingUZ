using Booking.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models.DBContext
{
    public class BookingContext : DbContext
    {
        public BookingContext()
            : base("name=BookingUZ")
        {
        }
        
        public virtual DbSet<Carriage> Carriages { get; set; }
        public virtual DbSet<CarriageType> CarriageTypes { get; set; }
        public virtual DbSet<Booking.Models.DBModels.DayOfWeek> DaysOfWeek { get; set; }
        public virtual DbSet<DistanceBetweenStations> DistanceBetweenStations { get; set; }
        public virtual DbSet<Occur> Occurs { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PersonAccount> PersonAccounts { get; set; }
        public virtual DbSet<PersonType> PersonTypes { get; set; }
        public virtual DbSet<ReservationSeat> ReservationSeats { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<Station> Stations { get; set; }
        public virtual DbSet<StationOnRoute> StationsOnRoutes { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Train> Trains { get; set; }
        public virtual DbSet<TrainRecurring> TrainsRecurring { get; set; }
        public virtual DbSet<TrainType> TrainTypes { get; set; }
    }
}
