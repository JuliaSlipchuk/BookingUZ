using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DataAccess.Helpers
{
    public static class MappingHelper
    {
        public static List<T> MapTo<T>(this DataTable dataTable) where T : new()
        {
            List<T> result = new List<T>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var item = new T();
                Type myType = typeof(T);
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
                for (int i = 0; i < props.Count; i++)
                {
                    if (props[i].Name == dataTable.Columns[i].ColumnName)
                    {
                        props[i].SetValue(item, dataRow[i]);
                    }
                }
                result.Add(item);
            }
            return result;
        }
    }
}
