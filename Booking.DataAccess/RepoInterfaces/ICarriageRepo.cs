using Booking.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DataAccess.RepoInterfaces
{
    public interface ICarriageRepo : IBaseRepository<Carriage>
    {
        int GetCarrTypeIdByCarrType(string carrType);
        int GetCountOfSeatsByCarrType(string carrType);
        string GetCarrTypeByCarrOrderInTrain(int trainNumb, int carrOrder);
        CarriageType GetCarrTypyById(int id);
        Carriage GetCarrByTypeOrderAndTrain(int carrTypeId, int order, int trainNumb);
    }
}
