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
    public class StationService : IStationServ
    {
        private readonly IStationRepo stationRepo;

        public StationService(IStationRepo stationRepo)
        {
            this.stationRepo = stationRepo;
        }

        public void Create(Station station)
        {
            stationRepo.Create(station);
        }
        public void Update(Station station)
        {
            stationRepo.Update(station);
        }
        public void Delete(int? id)
        {
            stationRepo.Delete(id);
        }

        public List<Station> GetAllItems()
        {
            return stationRepo.GetAllItems();
        }
        public Station GetItemById(int? id)
        {
            return stationRepo.GetItemById(id);
        }

        public int GetIdByName(string name)
        {
            return stationRepo.GetIdByName(name);
        }

        public List<Station> GetStationsStartingAtPrefix(string prefix)
        {
            return stationRepo.GetStationsStartingAtPrefix(prefix);
        }

        public string GetNameById(int id)
        {
            return stationRepo.GetNameById(id);
        }

        public string GetFirstStationOnRoute(int trainNumb)
        {
            return stationRepo.GetFirstStationOnRoute(trainNumb);
        }
    }
}
