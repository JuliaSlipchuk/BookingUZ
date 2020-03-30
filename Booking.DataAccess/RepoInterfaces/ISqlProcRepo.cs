using Booking.Models.ModelsReturnFromProc;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DataAccess.RepoInterfaces
{
    public interface ISqlProcRepo
    {
        void GetTrainByStationsAndDate(string firstStatName, string secondStatName, DateTime date, TimeSpan fromTime, out List<TrainByTwoStationsAndDate> trains, out List<CarriageTypeInTrain> types);
        void GetTrainsByTwoStatAndDate(DbDataReader reader, out List<TrainByTwoStationsAndDate> trains);
        void GetCarrTypesInTrain(DbDataReader reader, out List<CarriageTypeInTrain> types);
        List<FreeSeatInCarr> GetFreeSeatsInCarrs(int trainID, DateTime departDate, int carrTypeID, string firstStatName, string secondStatName);
        DateTime GetDepartArrivlDateTime(DateTime departDateTime, int trainId, string startStation, string endStation);
        decimal GetTicketPrice(int ticketId, bool haveTea, bool haveBed);
    }
}
