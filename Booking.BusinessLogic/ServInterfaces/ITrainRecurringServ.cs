using Booking.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.BusinessLogic.ServInterfaces
{
    public interface ITrainRecurringServ : IBaseService<TrainRecurring>
    {
        TrainRecurring GetByTrainId(int trainId);

        TrainRecurring GetByDaysOfWeekIdAndTrainId(int dayOfWeekId, int trainId);
    }
}
