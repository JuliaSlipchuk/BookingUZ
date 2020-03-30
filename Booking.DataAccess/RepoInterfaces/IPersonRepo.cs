using Booking.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DataAccess.RepoInterfaces
{
    public interface IPersonRepo : IBaseRepository<Person>
    {
        List<string> GetAllPersonTypes();
        bool PersonExist(string firstName, string lastName, DateTime birthDate, string personType);
        int GetPersonTypeIdByType(string personType);
        int GetMaxId();
        Person GetPersonByFLNameBirthType(string firstName, string lastName, DateTime birthDate, string personType);
    }
}
