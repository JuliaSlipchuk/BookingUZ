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
    public class TrainRecurringRepository : BaseRepository, ITrainRecurringRepo
    {
        public void Create(TrainRecurring trainRecurring)
        {
            db.TrainsRecurring.Add(trainRecurring);
            db.SaveChanges();
        }
        public void Update(TrainRecurring trainRecurring)
        {
            db.Entry(trainRecurring).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int? trainRecurrId)
        {
            if (trainRecurrId != null && db.TrainsRecurring.Where(tr => tr.ID == trainRecurrId).Any())
            {
                db.TrainsRecurring.Remove(db.TrainsRecurring.Where(tr => tr.ID == trainRecurrId).ToList()[0]);
            }
        }

        public List<TrainRecurring> GetAllItems()
        {
            if (db.TrainsRecurring.Any())
                return db.TrainsRecurring.ToList();
            return null;
        }
        public TrainRecurring GetItemById(int? id)
        {
            if (id != null && db.TrainsRecurring.Where(tr => tr.ID == id) != null)
            {
                return db.TrainsRecurring.Where(tr => tr.ID == id).ToList()[0];
            }
            return null;
        }

        public TrainRecurring GetByTrainId(int trainId)
        {
            IEnumerable<TrainRecurring> trRcr = db.TrainsRecurring.Where(tr=>tr.TrainID == trainId);
            if (trRcr != null)
            {
                return trRcr.ToList()[0];
            }
            return null;
        }

        public TrainRecurring GetByDaysOfWeekIdAndTrainId(int dayOfWeekId, int trainId)
        {
            IEnumerable<TrainRecurring> trRcr = db.TrainsRecurring.Where(tr => tr.TrainID == trainId && tr.DayOfWeekID == dayOfWeekId);
            if (trRcr != null)
            {
                return trRcr.ToList()[0];
            }
            return null;
        }
    }
}
