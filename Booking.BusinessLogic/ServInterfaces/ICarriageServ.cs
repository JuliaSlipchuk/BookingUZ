using Booking.BusinessLogic.ServInterfaces;
using Booking.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.BusinessLogic.ServInterfaces
{
    public interface ICarriageServ : IBaseService<Carriage>
    {
        int GetCarrTypeIdByCarrType(string carrType);
        int GetCountOfSeatsByCarrType(string carrType);
        string GetCarrTypeByCarrOrderInTrain(int trainNumb, int carrOrder);
        CarriageType GetCarrTypyById(int id);
        Carriage GetCarrByTypeOrderAndTrain(int carrTypeId, int order, int trainNumb);
    }
}
