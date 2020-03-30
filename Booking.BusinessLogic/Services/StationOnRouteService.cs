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
    public class StationOnRouteService : IStationOnRouteServ
    {
        private readonly IStationOnRouteRepo stationOnRouteRepo;

        public StationOnRouteService(IStationOnRouteRepo stationOnRouteRepo)
        {
            this.stationOnRouteRepo = stationOnRouteRepo;
        }

        public void Create(StationOnRoute stationOnRoute)
        {
            stationOnRouteRepo.Create(stationOnRoute);
        }
        public void Update(StationOnRoute stationOnRoute)
        {
            stationOnRouteRepo.Update(stationOnRoute);
        }
        public void Delete(int? id)
        {
            stationOnRouteRepo.Delete(id);
        }

        public List<StationOnRoute> GetAllItems()
        {
            return stationOnRouteRepo.GetAllItems();
        }
        public StationOnRoute GetItemById(int? id)
        {
            return stationOnRouteRepo.GetItemById(id);
        }

        public StationOnRoute GetByTrainNumbAndStation(int trainNumb, string stationName)
        {
            return stationOnRouteRepo.GetByTrainNumbAndStation(trainNumb, stationName);
        }
    }
}
