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
    public class TicketService : ITicketServ
    {
        private readonly ITicketRepo ticketRepo;

        public TicketService(ITicketRepo ticketRepo)
        {
            this.ticketRepo = ticketRepo;
        }

        public void Create(Ticket ticket)
        {
            ticketRepo.Create(ticket);
        }
        public void Update(Ticket ticket)
        {
            ticketRepo.Update(ticket);
        }
        public void Delete(int? id)
        {
            ticketRepo.Delete(id);
        }

        public List<Ticket> GetAllItems()
        {
            return ticketRepo.GetAllItems();
        }
        public Ticket GetItemById(int? id)
        {
            return ticketRepo.GetItemById(id);
        }

        public int GetMaxId()
        {
            return ticketRepo.GetMaxId();
        }

        public void AddPriceForTicket(int ticketId, decimal price)
        {
            ticketRepo.AddPriceForTicket(ticketId, price);
        }
    }
}
