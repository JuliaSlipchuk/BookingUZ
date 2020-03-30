using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models.Interfaces
{
    interface IStop
    {
        string Name { get; set; }
        System.Data.Entity.Spatial.DbGeography LocationOnMap { get; set; }
    }
}
