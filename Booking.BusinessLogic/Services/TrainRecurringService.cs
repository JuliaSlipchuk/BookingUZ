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
    public class TrainRecurringService : ITrainRecurringServ
    {
        private readonly ITrainRecurringRepo trainRecurringRepo;

        public TrainRecurringService(ITrainRecurringRepo trainRecurringRepo)
        {
            this.trainRecurringRepo = trainRecurringRepo;
        }

        public void Create(TrainRecurring trainRecurring)
        {
            trainRecurringRepo.Create(trainRecurring);
        }
        public void Update(TrainRecurring trainRecurring)
        {
            trainRecurringRepo.Update(trainRecurring);
        }
        public void Delete(int? id)
        {
            trainRecurringRepo.Delete(id);
        }

        public List<TrainRecurring> GetAllItems()
        {
            return trainRecurringRepo.GetAllItems();
        }
        public TrainRecurring GetItemById(int? id)
        {
            return trainRecurringRepo.GetItemById(id);
        }

        public TrainRecurring GetByTrainId(int trainId)
        {
            return trainRecurringRepo.GetByTrainId(trainId);
        }

        public TrainRecurring GetByDaysOfWeekIdAndTrainId(int dayOfWeekId, int trainId)
        {
            return trainRecurringRepo.GetByDaysOfWeekIdAndTrainId(dayOfWeekId, trainId);
        }
    }
}
