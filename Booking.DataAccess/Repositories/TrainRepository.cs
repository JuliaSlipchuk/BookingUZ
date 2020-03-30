using Booking.DataAccess.RepoInterfaces;
using Booking.DataAccess.Repositories;
using Booking.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Booking.DataAccess.Repositories
{
    public class TrainRepository : BaseRepository, ITrainRepo
    {
        public void Create(Train train)
        {
            db.Trains.Add(train);
            db.SaveChanges();
        }
        public void Update(Train train)
        {
            db.Entry(train).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int? trainId)
        {
            if (trainId != null && db.Trains.Where(t => t.ID == trainId).Any())
            {
                db.Trains.Remove(db.Trains.Where(t => t.ID == trainId).ToList()[0]);
            }
        }

        public List<Train> GetAllItems()
        {
            if (db.Trains.Any())
                return db.Trains.ToList();
            return null;
        }
        public Train GetItemById(int? id)
        {
            if (id != null && db.Trains.Where(t=>t.ID == id) != null)
            {
                return db.Trains.Where(t => t.ID == id).ToList()[0];
            }
            return null;
        }

        public int GetIdByNumber(int trainNumb)
        {
            IEnumerable<Train> train = db.Trains.Where(t => t.Number == trainNumb);
            if (train != null && train.Count() != 0)
            {
                return db.Trains.Where(t => t.Number == trainNumb).ToList()[0].ID;
            }
            return -1;
        }

        public bool TrainHasThisCarrType(Train train, string carrType)
        {
            int carrTypeId = carrRepo.GetCarrTypeIdByCarrType(carrType);
            if (db.Carriages.Where(c=>c.CarriageTypeID == carrTypeId && c.TrainID == train.ID).Any())
            {
                return true;
            }
            return false;
        }

        public DateTime GetDepartDateTime(Train train, DateTime departDate)
        {
            int occurID = db.TrainsRecurring.Where(tr => tr.TrainID == train.ID).ToList()[0].OccurID;
            string frequency = db.Occurs.Where(o => o.ID == occurID).ToList()[0].Frequency;
            if (train != null)
            {
                switch(frequency)
                {
                    case "Monthly":
                        {
                            TrainRecurring tr = trainRecrrRepo.GetByTrainId(train.ID);
                            if (tr != null)
                            {
                                return departDate.AddHours(tr.DepartureTime.Hours).AddMinutes(tr.DepartureTime.Minutes);
                            }
                            break;
                        }
                    case "Weekly":
                        {
                            System.DayOfWeek dayOfWeek = (System.DayOfWeek)departDate.DayOfWeek;
                            int dayOfWeekId = db.DaysOfWeek.Where(d => d.WeekDay == dayOfWeek.ToString()).ToList()[0].ID;
                            TrainRecurring trRcr = trainRecrrRepo.GetByDaysOfWeekIdAndTrainId(dayOfWeekId, train.ID);
                            if (trRcr != null)
                            {
                                return departDate.AddHours(trRcr.DepartureTime.Hours).AddMinutes(trRcr.DepartureTime.Minutes);
                            }
                            break;
                        }
                    case "Every day":
                        {
                            TrainRecurring trRcr = trainRecrrRepo.GetByTrainId(train.ID);
                            if (trRcr != null)
                            {
                                return departDate.AddHours(trRcr.DepartureTime.Hours).AddMinutes(trRcr.DepartureTime.Minutes);
                            }
                            break;
                        }
                }
            }
            return new DateTime(1, 1, 1);
        }

        public List<int> GetCarrOrderInTrainByCarrType(Train train, int carrTypeID)
        {
            List<int> carrOrders = new List<int>();
            carrOrders.AddRange(db.Carriages.Where(c => c.TrainID == train.ID && c.CarriageTypeID == carrTypeID).Select(c => c.Order));
            return carrOrders;
        }
    }
}
