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
    public class StationOnRouteRepository : BaseRepository, IStationOnRouteRepo
    {
        public void Create(StationOnRoute stationOnRoute)
        {
            db.StationsOnRoutes.Add(stationOnRoute);
            db.SaveChanges();
        }
        public void Update(StationOnRoute stationOnRoute)
        {
            db.Entry(stationOnRoute).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int? stationOnRouteId)
        {
            if (stationOnRouteId != null && db.StationsOnRoutes.Where(sOr => sOr.ID == stationOnRouteId).Any())
            {
                db.StationsOnRoutes.Remove(db.StationsOnRoutes.Where(sOr => sOr.ID == stationOnRouteId).ToList()[0]);
            }
        }

        public List<StationOnRoute> GetAllItems()
        {
            if (db.StationsOnRoutes.Any())
                return db.StationsOnRoutes.ToList();
            return null;
        }
        public StationOnRoute GetItemById(int? id)
        {
            if (id != null && db.StationsOnRoutes.Where(sOr => sOr.ID == id) != null)
            {
                return db.StationsOnRoutes.Where(sOr => sOr.ID == id).ToList()[0];
            }
            return null;
        }

        public StationOnRoute GetByTrainNumbAndStation(int trainNumb, string stationName)
        {
            Train train = trainRepo.GetItemById(trainRepo.GetIdByNumber(trainNumb));
            int stationId = stationRepo.GetIdByName(stationName);
            return db.StationsOnRoutes.Where(sOr => sOr.RouteID == train.RouteID && sOr.StationID == stationId).ToList()[0];
        }
    }
}
