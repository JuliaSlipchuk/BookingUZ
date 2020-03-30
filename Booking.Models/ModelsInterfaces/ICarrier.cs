using Booking.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models.Interfaces
{
    interface ICarrier
    {
        int Order { get; set; }
        CarriageType CarriageType { get; set; }
    }
}
