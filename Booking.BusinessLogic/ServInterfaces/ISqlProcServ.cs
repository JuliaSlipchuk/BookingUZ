using Booking.Models.ModelsReturnFromProc;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.BusinessLogic.ServInterfaces
{
    public interface ISqlProcServ
    {
        void GetTrainByStationsAndDate(string firstStatName, string secondStatName, DateTime date, TimeSpan fromTime, out List<TrainByTwoStationsAndDate> trains, out List<CarriageTypeInTrain> types);
        List<FreeSeatInCarr> GetFreeSeatsInCarrs(int trainID, DateTime departDate, int carrTypeID, string firstStatName, string secondStatName);
        DateTime GetDepartArrivlDateTime(DateTime departDateTime, int trainId, string startStation, string endStation);
        decimal GetTicketPrice(int trainId, bool haveTea, bool haveBed);
    }
}
