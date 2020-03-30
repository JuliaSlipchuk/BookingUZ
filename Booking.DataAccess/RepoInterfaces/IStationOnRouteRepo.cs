using Booking.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DataAccess.RepoInterfaces
{
    public interface IStationOnRouteRepo : IBaseRepository<StationOnRoute>
    {
        StationOnRoute GetByTrainNumbAndStation(int trainNumb, string stationName);
    }
}
