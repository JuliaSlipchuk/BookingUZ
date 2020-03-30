using Booking.BusinessLogic.ServInterfaces;
using Booking.DataAccess.RepoInterfaces;
using Booking.Models.ModelsReturnFromProc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Booking.BusinessLogic.Services
{
    public class SqlProcedureService : ISqlProcServ
    {
        private readonly ISqlProcRepo sqlProcRepo;
        public SqlProcedureService(ISqlProcRepo sqlProcRepo)
        {
            this.sqlProcRepo = sqlProcRepo;
        }

        public void GetTrainByStationsAndDate(string firstStatName, string secondStatName, DateTime date, TimeSpan fromTime, out List<TrainByTwoStationsAndDate> trains, out List<CarriageTypeInTrain> types)
        {
            sqlProcRepo.GetTrainByStationsAndDate(firstStatName, secondStatName, date, fromTime, out trains, out types);
        }

        public List<FreeSeatInCarr> GetFreeSeatsInCarrs(int trainID, DateTime departDate, int carrTypeID, string firstStatName, string secondStatName)
        {
            return sqlProcRepo.GetFreeSeatsInCarrs(trainID, departDate, carrTypeID, firstStatName, secondStatName);
        }

        public DateTime GetDepartArrivlDateTime(DateTime departDateTime, int trainId, string startStation, string endStation)
        {
            return sqlProcRepo.GetDepartArrivlDateTime(departDateTime, trainId, startStation, endStation);
        }

        public decimal GetTicketPrice(int trainId, bool haveTea, bool haveBed)
        {
            return sqlProcRepo.GetTicketPrice(trainId, haveTea, haveBed);
        }
    }
}
