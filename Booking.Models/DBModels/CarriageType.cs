using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models.DBModels
{
    [Table("CarriageType")]
    public class CarriageType : BaseDBEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CarriageType()
        {
            this.Carriage = new HashSet<Carriage>();
        }
        
        public string Type { get; set; }
        public int CountOfSeats { get; set; }
        public float PriceCoefficient { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Carriage> Carriage { get; set; }
    }
}
