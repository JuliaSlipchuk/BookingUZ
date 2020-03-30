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
    public class DistanceBetweenStationsRepository : BaseRepository, IDistanceBtwStationsRepo
    {
        public void Create(DistanceBetweenStations distanceBtwStations)
        {
            db.DistanceBetweenStations.Add(distanceBtwStations);
            db.SaveChanges();
        }
        public void Update(DistanceBetweenStations distanceBtwStations)
        {
            db.Entry(distanceBtwStations).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int? distanceBtwStationsId)
        {
            if (distanceBtwStationsId != null && db.DistanceBetweenStations.Where(dbs => dbs.ID == distanceBtwStationsId).Any())
            {
                db.DistanceBetweenStations.Remove(db.DistanceBetweenStations.Where(dbs => dbs.ID == distanceBtwStationsId).ToList()[0]);
            }
        }

        public List<DistanceBetweenStations> GetAllItems()
        {
            if (db.DistanceBetweenStations.Any())
                return db.DistanceBetweenStations.ToList();
            return null;
        }
        public DistanceBetweenStations GetItemById(int? id)
        {
            if (id != null && db.DistanceBetweenStations.Where(dbs => dbs.ID == id) != null)
            {
                return db.DistanceBetweenStations.Where(dbs => dbs.ID == id).ToList()[0];
            }
            return null;
        }
    }
}
