using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DataAccess.RepoInterfaces
{
    public interface IBaseRepository<T>
    {
        T GetItemById(int? id);
        List<T> GetAllItems();
        void Create(T item);
        void Update(T item);
        void Delete(int? id);
    }
}
