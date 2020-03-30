using Booking.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DataAccess.RepoInterfaces
{
    public interface IStationRepo : IBaseRepository<Station>
    {
        int GetIdByName(string name);
        List<Station> GetStationsStartingAtPrefix(string prefix);
        string GetFirstStationOnRoute(int trainNumber);
        string GetNameById(int id);
    }
}
