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
    public class PersonRepository : BaseRepository, IPersonRepo
    {
        public void Create(Person person)
        {
            db.People.Add(person);
            db.SaveChanges();
        }
        public void Update(Person person)
        { 
            db.Entry(person).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int? personId)
        {
            if (personId != null && db.People.Where(p => p.ID == personId).Any())
            {
                db.People.Remove(db.People.Where(p => p.ID == personId).ToList()[0]);
            }
        }

        public List<Person> GetAllItems()
        {
            if (db.People.Any())
                return db.People.ToList();
            return null;
        }
        public Person GetItemById(int? id)
        {
            if (id != null && db.People.Where(p=>p.ID == id) != null)
            {
                return db.People.Where(p => p.ID == id).ToList()[0];
            }
            return null;
        }

        public List<string> GetAllPersonTypes()
        {
            return db.PersonTypes.Select(pt => pt.Type).ToList();
        }

        public int GetPersonTypeIdByType(string personType)
        {
            return db.PersonTypes.Where(pt => pt.Type == personType).Select(pt => pt.ID).ToList()[0];
        }

        public bool PersonExist(string firstName, string lastName, DateTime birthDate, string personType)
        {
            int personTypeId = persRepo.GetPersonTypeIdByType(personType);
            IEnumerable<Person> person = db.People.Where(p => p.FirstName == firstName && p.LastName == lastName && p.BirthDate == birthDate && p.PersonTypeID == personTypeId);
            if (person != null && person.Count() != 0)
            {
                return true;
            }
            return false;
        }

        public int GetMaxId()
        {
            return db.People.Select(p => p.ID).Max();
        }

        public Person GetPersonByFLNameBirthType(string firstName, string lastName, DateTime birthDate, string personType)
        {
            int personTypeId = persRepo.GetPersonTypeIdByType(personType);
            return db.People.Where(p => p.FirstName == firstName && p.LastName == lastName && p.BirthDate == birthDate && p.PersonTypeID == personTypeId).ToList()[0];
        }
    }
}
