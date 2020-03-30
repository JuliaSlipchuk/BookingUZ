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
    public class ReservationSeatRepository : BaseRepository, IReservSeatRepo
    {
        public void Create(ReservationSeat resSeat)
        {
            db.ReservationSeats.Add(resSeat);
            db.SaveChanges();
        }
        public void Update(ReservationSeat resSeat)
        {
            db.Entry(resSeat).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int? resSeatId)
        {
            if (resSeatId != null && db.ReservationSeats.Where(rs => rs.ID == resSeatId).Any())
            {
                db.ReservationSeats.Remove(db.ReservationSeats.Where(rs => rs.ID == resSeatId).ToList()[0]);
            }
        }

        public List<ReservationSeat> GetAllItems()
        {
            if (db.ReservationSeats.Any())
                return db.ReservationSeats.ToList();
            return null;
        }
        public ReservationSeat GetItemById(int? id)
        {
            if (id != null && db.ReservationSeats.Where(rs => rs.ID == id) != null)
            {
                return db.ReservationSeats.Where(rs => rs.ID == id).ToList()[0];
            }
            return null;
        }

        public int GetMaxId()
        {
            return db.ReservationSeats.Select(rs => rs.ID).Max();
        }
    }
}
