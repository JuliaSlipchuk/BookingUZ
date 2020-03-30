using Booking.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models.Interfaces
{
    interface ITransport
    {
        int Number { get; set; }
        TrainType TrainType { get; set; }
    }
}
