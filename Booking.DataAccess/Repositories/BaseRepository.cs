using Booking.DataAccess.RepoInterfaces;
using Booking.DataAccess.Repositories;
using Booking.Models;
using Booking.Models.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DataAccess.Repositories
{
    public abstract class BaseRepository
    {
        protected static BookingContext db { get; set; }

        protected static SqlProcedureRepository sqlRepo { get; set; }
        protected static StationRepository stationRepo { get; set; }
        protected static RouteRepository routeRepo { get; set; }
        protected static CarriageRepository carrRepo { get; set; }
        protected static DistanceBetweenStationsRepository distBtwStatsRepo { get; set; }
        protected static PersonRepository persRepo { get; set; }
        protected static PersonAccountRepository persAccRepo { get; set; }
        protected static ReservationSeatRepository reservSeatRepo { get; set; }
        protected static StationOnRouteRepository statOnRouteRepo { get; set; }
        protected static TicketRepository ticktRepo { get; set; }
        protected static TrainRecurringRepository trainRecrrRepo { get; set; }
        protected static TrainRepository trainRepo { get; set; }

        static BaseRepository()
        {
            db = new BookingContext();

            sqlRepo = new SqlProcedureRepository();
            stationRepo = new StationRepository();
            routeRepo = new RouteRepository();
            carrRepo = new CarriageRepository();
            distBtwStatsRepo = new DistanceBetweenStationsRepository();
            persRepo = new PersonRepository();
            persAccRepo = new PersonAccountRepository();
            reservSeatRepo = new ReservationSeatRepository();
            statOnRouteRepo = new StationOnRouteRepository();
            ticktRepo = new TicketRepository();
            trainRecrrRepo = new TrainRecurringRepository();
            trainRepo = new TrainRepository();
        }
    }
}
