using Booking.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models.DBModels
{
    [Table("Train")]
    public class Train : BaseDBEntity, ITransport
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Train()
        {
            this.Carriage = new HashSet<Carriage>();
            this.TrainRecurring = new HashSet<TrainRecurring>();
        }
        
        public int Number { get; set; }
        public int TrainTypeID { get; set; }
        public int RouteID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Carriage> Carriage { get; set; }
        public virtual Route Route { get; set; }
        public virtual TrainType TrainType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrainRecurring> TrainRecurring { get; set; }
    }
}
