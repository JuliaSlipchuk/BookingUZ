using Booking.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DataAccess.RepoInterfaces
{
    public interface ITrainRecurringRepo : IBaseRepository<TrainRecurring>
    {
        TrainRecurring GetByTrainId(int trainId);

        TrainRecurring GetByDaysOfWeekIdAndTrainId(int dayOfWeekId, int trainId);
    }
}
