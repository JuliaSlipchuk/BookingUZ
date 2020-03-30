using Booking.Models.DBModels;
using Booking.ViewModels;
using Booking.ViewModelsAndSubViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Helpers
{
    interface IMapper
    {
        List<TrainByStationsAndDate> GetTrainsByStationsAndDate(string firstStat, string seconsStat, DateTime date, TimeSpan fromTime);
        List<FreeSeatInCarriage> GetFreeSeatsInCarriages(int trainID, DateTime departDateTime, int carrTypeID, string startStatName, string endStatName);
        void CreateNewTickets(ref List<TicketVM> tickets, InformAboutPassengers personsInf);
    }
}
