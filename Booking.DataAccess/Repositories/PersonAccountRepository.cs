using Booking.DataAccess.RepoInterfaces;
using Booking.DataAccess.Repositories;
using Booking.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DataAccess.Repositories
{
    public class PersonAccountRepository : BaseRepository, IPersonAccountRepo
    {
        public void Create(PersonAccount perAcc)
        {
            db.PersonAccounts.Add(perAcc);
            db.SaveChanges();
        }
        public void Update(PersonAccount perAcc)
        {
            db.Entry(perAcc).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int? perAccId)
        {
            if (perAccId != null && db.PersonAccounts.Where(pa => pa.ID == perAccId).Any())
            {
                db.PersonAccounts.Remove(db.PersonAccounts.Where(pa => pa.ID == perAccId).ToList()[0]);
            }
        }

        public List<PersonAccount> GetAllItems()
        {
            if (db.PersonAccounts.Any())
                return db.PersonAccounts.ToList();
            return null;
        }
        public PersonAccount GetItemById(int? id)
        {
            if (id != null && db.PersonAccounts.Where(pa => pa.ID == id) != null)
            {
                return db.PersonAccounts.Where(pa => pa.ID == id).ToList()[0];
            }
            return null;
        }
    }
}
