using Booking.BusinessLogic.Services;
using Booking.BusinessLogic.ServInterfaces;
using Booking.DataAccess.RepoInterfaces;
using Booking.DataAccess.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.App_Start
{
    public class NinjectRegistration : NinjectModule
    {
        public override void Load()
        {
            Bind<ICarriageRepo>().To<CarriageRepository>();
            Bind<ICarriageServ>().To<CarriageService>();

            Bind<IDistanceBtwStationsRepo>().To<DistanceBetweenStationsRepository>();
            Bind<IDistanceBtwStationsServ>().To<DistanceBetweenStationsService>();

            Bind<IPersonAccountRepo>().To<PersonAccountRepository>();
            Bind<IPersonAccountServ>().To<PersonAccountService>();

            Bind<IPersonRepo>().To<PersonRepository>();
            Bind<IPersonServ>().To<PersonService>();

            Bind<IReservSeatRepo>().To<ReservationSeatRepository>();
            Bind<IReservSeatServ>().To<ReservationSeatService>();

            Bind<IRouteRepo>().To<RouteRepository>();
            Bind<IRouteServ>().To<RouteService>();

            Bind<ISqlProcRepo>().To<SqlProcedureRepository>();
            Bind<ISqlProcServ>().To<SqlProcedureService>();

            Bind<IStationOnRouteRepo>().To<StationOnRouteRepository>();
            Bind<IStationOnRouteServ>().To<StationOnRouteService>();

            Bind<IStationRepo>().To<StationRepository>();
            Bind<IStationServ>().To<StationService>();

            Bind<ITicketRepo>().To<TicketRepository>();
            Bind<ITicketServ>().To<TicketService>();

            Bind<ITrainRecurringRepo>().To<TrainRecurringRepository>();
            Bind<ITrainRecurringServ>().To<TrainRecurringService>();

            Bind<ITrainRepo>().To<TrainRepository>();
            Bind<ITrainServ>().To<TrainService>();
        }
    }
}