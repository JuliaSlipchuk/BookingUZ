using Booking.DataAccess.Helpers;
using Booking.DataAccess.RepoInterfaces;
using Booking.Models.ModelsReturnFromProc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DataAccess.Repositories
{
    public class SqlProcedureRepository : ISqlProcRepo
    {
        string connStr = @"Data Source=(LocalDb)\MSSQLLocalDB; Initial Catalog = BookingUZ; Integrated Security = True;";

        protected static IStationRepo stationRepo;
        protected static ITrainRepo trainRepo;

        static SqlProcedureRepository()
        {
            stationRepo = new StationRepository();
            trainRepo = new TrainRepository();
        }

        public void GetTrainByStationsAndDate(string firstStatName, string secondStatName, DateTime date, TimeSpan fromTime, out List<TrainByTwoStationsAndDate> trains, out List<CarriageTypeInTrain> types)
        {
            trains = new List<TrainByTwoStationsAndDate>();
            types = new List<CarriageTypeInTrain>();

            int firstStatID, secondStatID;
            firstStatID = stationRepo.GetIdByName(firstStatName);
            secondStatID = stationRepo.GetIdByName(secondStatName);
            if (firstStatID == -1 || secondStatID == -1)
            {
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand comm = new SqlCommand("GetTrainByTwoStationsAndDate", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    comm.Parameters.AddWithValue("@From", firstStatID);
                    comm.Parameters.AddWithValue("@To", secondStatID);
                    comm.Parameters.AddWithValue("@Date", date.Date);
                    comm.Parameters.AddWithValue("@FromTime", fromTime);

                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        GetTrainsByTwoStatAndDate(reader, out trains);

                        GetCarrTypesInTrain(reader, out types);
                    }
                }
            }
        }

        public void GetTrainsByTwoStatAndDate(DbDataReader reader, out List<TrainByTwoStationsAndDate> trains)
        {
            trains = new List<TrainByTwoStationsAndDate>();
            DataTable trainsTable = new DataTable();
            trainsTable.Load(reader);
            trains.AddRange(trainsTable.MapTo<TrainByTwoStationsAndDate>());
        }

        public void GetCarrTypesInTrain(DbDataReader reader, out List<CarriageTypeInTrain> types)
        {
            types = new List<CarriageTypeInTrain>();
            DataTable typesTable = new DataTable();
            typesTable.Load(reader);
            types.AddRange(typesTable.MapTo<CarriageTypeInTrain>());
        }

        public List<FreeSeatInCarr> GetFreeSeatsInCarrs(int trainID, DateTime departDate, int carrTypeID, string firstStatName, string secondStatName)
        {
            List<FreeSeatInCarr> seats = new List<FreeSeatInCarr>();

            int firstStatID, secondStatID;
            firstStatID = stationRepo.GetIdByName(firstStatName);
            secondStatID = stationRepo.GetIdByName(secondStatName);
            if (firstStatID == -1 || secondStatID == -1)
            {
                return seats;
            }

            using (SqlConnection sqlConn = new SqlConnection(connStr))
            {
                sqlConn.Open();
                using (SqlCommand comm = new SqlCommand("GetFreeSeats", sqlConn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    comm.Parameters.AddWithValue("@TrainID", trainID);
                    comm.Parameters.AddWithValue("@DepartDateTime", departDate);
                    comm.Parameters.AddWithValue("@CarrTypeID", carrTypeID);
                    comm.Parameters.AddWithValue("@StartStatID", firstStatID);
                    comm.Parameters.AddWithValue("@EndStatID", secondStatID);

                    SqlDataReader reader = comm.ExecuteReader();
                    while(reader.Read())
                    {
                        FreeSeatInCarr seat = new FreeSeatInCarr();

                        Type freeSeatType = seat.GetType();
                        IList<PropertyInfo> props = new List<PropertyInfo>(freeSeatType.GetProperties());
                        int i = 0;
                        foreach (PropertyInfo prop in props)
                        {
                            prop.SetValue(seat, reader[i]);
                            i++;
                        }
                        seats.Add(seat);
                    }
                }
            }

            return seats;
        }

        public DateTime GetDepartArrivlDateTime(DateTime departDateTime, int trainNumb, string startStation, string endStation)
        {
            if (startStation == endStation)
            {
                return departDateTime;
            }
            DateTime result = new DateTime();
            int startStatId = stationRepo.GetIdByName(startStation);
            int endStatId = stationRepo.GetIdByName(endStation);
            int trainId = trainRepo.GetIdByNumber(trainNumb);
            using (SqlConnection sqlConn = new SqlConnection(connStr))
            {
                sqlConn.Open();
                using (SqlCommand sqlComm = new SqlCommand("SELECT dbo.ArrivalDateTimeCalculation (@DepartureDateTime, @TrainId, @StartStationId, @EndStationId)", sqlConn))
                {
                    sqlComm.Parameters.AddWithValue("@DepartureDateTime", departDateTime.ToString("MM.dd.yyyy HH:mm:ss"));
                    sqlComm.Parameters.AddWithValue("@TrainId", trainId);
                    sqlComm.Parameters.AddWithValue("@StartStationId", startStatId);
                    sqlComm.Parameters.AddWithValue("@EndStationId", endStatId);

                    result = Convert.ToDateTime(sqlComm.ExecuteScalar());
                }
            }
            return result;
        }

        public decimal GetTicketPrice(int ticketId, bool haveTea, bool haveBed)
        {
            decimal price = 0;
            using(SqlConnection sqlConn = new SqlConnection(connStr))
            {
                sqlConn.Open();
                using(SqlCommand sqlComm = new SqlCommand("SELECT dbo.PriceCalculation (@TicketID)", sqlConn))
                {
                    sqlComm.Parameters.AddWithValue("@TicketID", ticketId);

                    price = Convert.ToDecimal(sqlComm.ExecuteScalar());
                    if (haveTea)
                    {
                        price += 20;
                    }
                    if (haveBed)
                    {
                        price += 25;
                    }
                }
            }
            return price;
        }
    }
}
