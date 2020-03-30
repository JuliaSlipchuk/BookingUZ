using Booking.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.BusinessLogic.ServInterfaces
{
    public interface IBaseService<T>
    {
        void Create(T obj);
        void Delete(int? id);
        void Update(T obj);

        List<T> GetAllItems();
        T GetItemById(int? id);
    }
}
