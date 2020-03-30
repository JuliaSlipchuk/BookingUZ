using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models.Interfaces
{
    interface ISeat
    {
        int Number { get; set; }
        int CarriageID { get; set; }
    }
}
