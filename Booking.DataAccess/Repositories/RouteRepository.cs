using Booking.DataAccess.RepoInterfaces;
using Booking.DataAccess.Repositories;
using Booking.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DataAccess.Repositories
{
    public class RouteRepository : BaseRepository, IRouteRepo
    {
        public void Create(Route route)
        {
            db.Routes.Add(route);
            db.SaveChanges();
        }
        public void Update(Route route)
        {
            db.Entry(route).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int? routeId)
        {
            if (routeId != null && db.Routes.Where(r => r.ID == routeId).Any())
            {
                db.Routes.Remove(db.Routes.Where(r => r.ID == routeId).ToList()[0]);
            }
        }

        public List<Route> GetAllItems()
        {
            if (db.Routes.Any())
                return db.Routes.ToList();
            return null;
        }
        public Route GetItemById(int? id)
        {
            if (id != null && db.Routes.Where(r=>r.ID == id) != null)
            {
                return db.Routes.Where(r => r.ID == id).ToList()[0];
            }
            return null;
        }
    }
}
