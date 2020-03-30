using Booking.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models.DBModels
{
    [Table("Carriage")]
    public class Carriage : BaseDBEntity, ICarrier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Carriage()
        {
            this.ReservationSeat = new HashSet<ReservationSeat>();
        }
        
        public int Order { get; set; }
        public int TrainID { get; set; }
        public int CarriageTypeID { get; set; }
        public virtual CarriageType CarriageType { get; set; }
        public virtual Train Train { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReservationSeat> ReservationSeat { get; set; }
    }
}
