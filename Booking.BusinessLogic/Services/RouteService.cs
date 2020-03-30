using Booking.BusinessLogic.ServInterfaces;
using Booking.DataAccess.RepoInterfaces;
using Booking.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.BusinessLogic.Services
{
    public class RouteService : IRouteServ
    {
        private readonly IRouteRepo routeRepo;

        public RouteService(IRouteRepo routeRepo)
        {
            this.routeRepo = routeRepo;
        }

        public void Create(Route route)
        {
            routeRepo.Create(route);
        }
        public void Update(Route route)
        {
            routeRepo.Update(route);
        }
        public void Delete(int? id)
        {
            routeRepo.Delete(id);
        }

        public List<Route> GetAllItems()
        {
            return routeRepo.GetAllItems();
        }
        public Route GetItemById(int? id)
        {
            return routeRepo.GetItemById(id);
        }
    }
}
