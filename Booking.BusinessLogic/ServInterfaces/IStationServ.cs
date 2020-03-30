using Booking.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.BusinessLogic.ServInterfaces
{
    public interface IStationServ : IBaseService<Station>
    {
        int GetIdByName(string name);
        List<Station> GetStationsStartingAtPrefix(string prefix);
        string GetNameById(int id);
        string GetFirstStationOnRoute(int trainNumb);
    }
}
