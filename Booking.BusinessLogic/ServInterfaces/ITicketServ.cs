using Booking.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.BusinessLogic.ServInterfaces
{
    public interface ITicketServ : IBaseService<Ticket>
    {
        int GetMaxId();
        void AddPriceForTicket(int ticketId, decimal price);
    }
}
