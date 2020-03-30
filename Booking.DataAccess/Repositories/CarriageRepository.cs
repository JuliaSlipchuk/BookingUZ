using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.DataAccess;
using System.Data.Entity;
using Booking.Models.ModelsReturnFromProc;
using System.Data.SqlClient;
using Booking.DataAccess.RepoInterfaces;
using Booking.Models.DBModels;

namespace Booking.DataAccess.Repositories
{
    public class CarriageRepository : BaseRepository, ICarriageRepo
    {
        public void Create (Carriage carr)
        {
            db.Carriages.Add(carr);
            db.SaveChanges();
        }
        public void Update (Carriage carr)
        {
            db.Entry(carr).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete (int? carrId)
        {
            if (carrId != null && db.Carriages.Where(c=>c.ID == carrId).Any())
            {
                db.Carriages.Remove(db.Carriages.Where(c => c.ID == carrId).ToList()[0]);
            }
        }

        public List<Carriage> GetAllItems()
        {
            if (db.Carriages.Any())
                return db.Carriages.ToList();
            return null;
        }
        public Carriage GetItemById(int? id)
        {
            if (id != null && db.Carriages.Where(c=>c.ID == id) != null)
            {
                return db.Carriages.Where(c => c.ID == id).ToList()[0];
            }
            return null;
        }

        public int GetCarrTypeIdByCarrType(string carrType)
        {
            IEnumerable<CarriageType> list = db.CarriageTypes.Where(ct => ct.Type == carrType);
            if (list != null && list.Count() != 0)
            {
                return db.CarriageTypes.Where(ct => ct.Type == carrType).ToList()[0].ID;
            }
            return -1;
        }

        public int GetCountOfSeatsByCarrType(string carrType)
        {
            return db.CarriageTypes.Where(ct => ct.Type == carrType).Select(ct => ct.CountOfSeats).ToList()[0];
        }

        public string GetCarrTypeByCarrOrderInTrain(int trainNumb, int carrOrder)
        {
            int trainId = trainRepo.GetIdByNumber(trainNumb);
            int carrTypeId = db.Carriages.Where(c => c.TrainID == trainId && c.Order == carrOrder).Select(c=>c.CarriageTypeID).ToList()[0];
            return db.CarriageTypes.Where(ct => ct.ID == carrTypeId).Select(ct=>ct.Type).ToList()[0];
        }

        public CarriageType GetCarrTypyById(int id)
        {
            return db.CarriageTypes.Where(ct => ct.ID == id).ToList()[0];
        }

        public Carriage GetCarrByTypeOrderAndTrain(int carrTypeId, int order, int trainNumb)
        {
            int trainId = trainRepo.GetIdByNumber(trainNumb);
            return db.Carriages.Where(c => c.CarriageTypeID == carrTypeId && c.Order == order && c.TrainID == trainId).ToList()[0];
        }
    }
}
