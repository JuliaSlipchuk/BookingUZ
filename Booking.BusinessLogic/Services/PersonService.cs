using Booking.BusinessLogic.ServInterfaces;
using Booking.DataAccess.RepoInterfaces;
using Booking.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.BusinessLogic.Services
{
    public class PersonService : IPersonServ
    {
        private readonly IPersonRepo personRepo;

        public PersonService(IPersonRepo personRepo)
        {
            this.personRepo = personRepo;
        }

        public void Create(Person person)
        {
            personRepo.Create(person);
        }
        public void Update(Person person)
        {
            personRepo.Update(person);
        }
        public void Delete(int? id)
        {
            personRepo.Delete(id);
        }

        public List<Person> GetAllItems()
        {
            return personRepo.GetAllItems();
        }
        public Person GetItemById(int? id)
        {
            return personRepo.GetItemById(id);
        }

        public List<string> GetAllPersonTypes()
        {
            return personRepo.GetAllPersonTypes();
        }

        public bool PersonExist(string firstName, string lastName, DateTime birthDate, string personType)
        {
            return personRepo.PersonExist(firstName, lastName, birthDate, personType);
        }

        public int GetMaxId()
        {
            return personRepo.GetMaxId();
        }

        public int GetPersonTypeIdByType(string personType)
        {
            return personRepo.GetPersonTypeIdByType(personType);
        }

        public Person GetPersonByFLNameBirthType(string firstName, string lastName, DateTime birthDate, string personType)
        {
            return personRepo.GetPersonByFLNameBirthType(firstName, lastName, birthDate, personType);
        }
    }
}
