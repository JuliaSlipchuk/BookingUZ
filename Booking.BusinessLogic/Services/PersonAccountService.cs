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
    public class PersonAccountService : IPersonAccountServ
    {
        private readonly IPersonAccountRepo personAccountRepo;

        public PersonAccountService(IPersonAccountRepo personAccountRepo)
        {
            this.personAccountRepo = personAccountRepo;
        }

        public void Create(PersonAccount perAcc)
        {
            personAccountRepo.Create(perAcc);
        }
        public void Update(PersonAccount perAcc)
        {
            personAccountRepo.Update(perAcc);
        }
        public void Delete(int? perAccId)
        {
            personAccountRepo.Delete(perAccId);
        }

        public List<PersonAccount> GetAllItems()
        {
            return personAccountRepo.GetAllItems();
        }
        public PersonAccount GetItemById(int? id)
        {
            return personAccountRepo.GetItemById(id);
        }
    }
}
