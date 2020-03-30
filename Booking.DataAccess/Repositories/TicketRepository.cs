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
    public class TicketRepository : BaseRepository, ITicketRepo
    {
        public void Create(Ticket ticket)
        {
            db.Tickets.Add(ticket);
            db.SaveChanges();
        }
        public void Update(Ticket ticket)
        {
            db.Entry(ticket).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int? ticketId)
        {
            if (ticketId != null && db.Tickets.Where(t => t.ID == ticketId).Any())
            {
                db.Tickets.Remove(db.Tickets.Where(t => t.ID == ticketId).ToList()[0]);
            }
        }

        public List<Ticket> GetAllItems()
        {
            if (db.Tickets.Any())
                return db.Tickets.ToList();
            return null;
        }
        public Ticket GetItemById(int? id)
        {
            if (id != null && db.Tickets.Where(t => t.ID == id) != null)
            {
                return db.Tickets.Where(t => t.ID == id).ToList()[0];
            }
            return null;
        }

        public int GetMaxId()
        {
            return db.Tickets.Select(t => t.ID).Max();
        }

        public void AddPriceForTicket(int ticketId, decimal price)
        {
            Ticket ticket = db.Tickets.Where(t => t.ID == ticketId).ToList()[0];
            ticket.Price = price;
            db.Entry(ticket).Property("Price").IsModified = true;
            db.SaveChanges();
        }
    }
}
