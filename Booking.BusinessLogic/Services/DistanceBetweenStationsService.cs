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
    public class DistanceBetweenStationsService : IDistanceBtwStationsServ
    {
        private readonly IDistanceBtwStationsRepo distanceBtwStationsRepo;

        public DistanceBetweenStationsService(IDistanceBtwStationsRepo distanceBtwStationsRepo)
        {
            this.distanceBtwStationsRepo = distanceBtwStationsRepo;
        }

        public void Create(DistanceBetweenStations distanceBtwStations)
        {
            distanceBtwStationsRepo.Create(distanceBtwStations);
        }
        public void Update(DistanceBetweenStations distanceBtwStations)
        {
            distanceBtwStationsRepo.Update(distanceBtwStations);
        }
        public void Delete(int? distanceBtwStationsId)
        {
            distanceBtwStationsRepo.Delete(distanceBtwStationsId);
        }

        public List<DistanceBetweenStations> GetAllItems()
        {
            return distanceBtwStationsRepo.GetAllItems();
        }
        public DistanceBetweenStations GetItemById(int? id)
        {
            return distanceBtwStationsRepo.GetItemById(id);
        }
    }
}
