using Booking.BusinessLogic.ServInterfaces;
using Booking.DataAccess.RepoInterfaces;
using Booking.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Booking.BusinessLogic.Services
{
    public class TrainService : ITrainServ
    {
        private readonly ITrainRepo trainRepo;

        public TrainService(ITrainRepo trainRepo)
        {
            this.trainRepo = trainRepo;
        }

        public void Create(Train train)
        {
            trainRepo.Create(train);
        }
        public void Update(Train train)
        {
            trainRepo.Update(train);
        }
        public void Delete(int? id)
        {
            trainRepo.Delete(id);
        }

        public List<Train> GetAllItems()
        {
            return trainRepo.GetAllItems();
        }
        public Train GetItemById(int? id)
        {
            return trainRepo.GetItemById(id);
        }

        public int GetIdByNumber(int trainNumb)
        {
            return trainRepo.GetIdByNumber(trainNumb);
        }

        public bool TrainHasThisCarrType(Train train, string carrType)
        {
            return trainRepo.TrainHasThisCarrType(train, carrType);
        }

        public DateTime GetDepartDateTime(Train train, DateTime departDate)
        {
            return trainRepo.GetDepartDateTime(train, departDate);
        }

        public List<int> GetCarrOrderInTrainByCarrType(Train train, int carrTypeID)
        {
            return trainRepo.GetCarrOrderInTrainByCarrType(train, carrTypeID);
        }
    }
}
