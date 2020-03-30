using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models.DBModels
{
    [Table("StationOnRoute")]
    public class StationOnRoute : BaseDBEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StationOnRoute()
        {
            //this.Ticket = new HashSet<Ticket>();
            //this.Ticket1 = new HashSet<Ticket>();
        }
        
        public int RouteID { get; set; }
        public int StationID { get; set; }
        public int Order { get; set; }

        //public virtual Route Route { get; set; }
        //public virtual Station Station { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Ticket> Ticket { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Ticket> Ticket1 { get; set; }
    }
}
