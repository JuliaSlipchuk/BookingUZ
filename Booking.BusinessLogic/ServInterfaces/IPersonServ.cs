using Booking.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.BusinessLogic.ServInterfaces
{
    public interface IPersonServ : IBaseService<Person>
    {
        List<string> GetAllPersonTypes();
        bool PersonExist(string firstName, string lastName, DateTime birthDate, string personType);
        int GetMaxId();
        int GetPersonTypeIdByType(string personType);
        Person GetPersonByFLNameBirthType(string firstName, string lastName, DateTime birthDate, string personType);
    }
}
