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
    public class StationRepository : BaseRepository, IStationRepo
    {
        public void Create(Station station)
        {
            db.Stations.Add(station);
            db.SaveChanges();
        }
        public void Update(Station station)
        {
            db.Entry(station).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int? stationId)
        {
            if (stationId != null && db.Stations.Where(s => s.ID == stationId).Any())
            {
                db.Stations.Remove(db.Stations.Where(s => s.ID == stationId).ToList()[0]);
            }
        }

        public List<Station> GetAllItems()
        {
            if (db.Stations.Any())
                return db.Stations.ToList();
            return null;
        }
        public Station GetItemById(int? id)
        {
            if (id != null && db.Stations.Where(s => s.ID == id) != null)
            {
                return db.Stations.Where(s => s.ID == id).ToList()[0];
            }
            return null;
        }

        public int GetIdByName(string name)
        {
            if (name != null && db.Stations.Where(s=>s.Name == name) != null && db.Stations.Where(s => s.Name == name).Count() != 0)
            {
                return db.Stations.Where(s => s.Name == name).ToList()[0].ID;
            }
            return -1;
        }

        public List<Station> GetStationsStartingAtPrefix(string prefix)
        {
            IEnumerable<Station> stations = db.Stations.Where(s => s.Name.StartsWith(prefix));
            if (stations.Count() != 0)
            {
                return stations.ToList();
            }
            return null;
        }

        public string GetNameById(int id)
        {
            return db.Stations.Where(s => s.ID == id).Select(s => s.Name).ToList()[0];
        }

        public string GetFirstStationOnRoute(int trainNumber)
        {
            Train train = trainRepo.GetItemById(trainRepo.GetIdByNumber(trainNumber));
            int stationId = db.StationsOnRoutes.Where(sOr => sOr.RouteID == train.RouteID && sOr.Order == 1).Select(sOr => sOr.StationID).ToList()[0];
            return stationRepo.GetNameById(stationId);
        }
    }
}
