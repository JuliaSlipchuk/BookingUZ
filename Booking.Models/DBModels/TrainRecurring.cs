using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models.DBModels
{
    [Table("TrainRecurring")]
    public class TrainRecurring : BaseDBEntity
    {
        public int TrainID { get; set; }
        public int OccurID { get; set; }
        public Nullable<int> DayNumbInMonth { get; set; }
        public Nullable<int> DayOfWeekID { get; set; }
        public System.TimeSpan DepartureTime { get; set; }
        public string Description { get; set; }

        //public virtual DayOfWeek DayOfWeek { get; set; }
        //public virtual Occur Occur { get; set; }
        //public virtual Train Train { get; set; }
    }
}
