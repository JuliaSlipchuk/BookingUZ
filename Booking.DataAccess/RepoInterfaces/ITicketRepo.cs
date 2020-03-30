﻿using Booking.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DataAccess.RepoInterfaces
{
    public interface ITicketRepo : IBaseRepository<Ticket>
    {
        int GetMaxId();
        void AddPriceForTicket(int ticketId, decimal price);
    }
}