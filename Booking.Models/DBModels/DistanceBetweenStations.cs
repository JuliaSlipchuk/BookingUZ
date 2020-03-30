using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models.DBModels
{
    [Table("DistanceBetweenStations")]
    public class DistanceBetweenStations : BaseDBEntity
    {
        public int StationFirstID { get; set; }
        public int StationSecondID { get; set; }
        public float Distance { get; set; }

        public virtual Station Station { get; set; }
        public virtual Station Station1 { get; set; }
    }
}
