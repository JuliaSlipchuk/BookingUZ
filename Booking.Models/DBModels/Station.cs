using Booking.Models.Interfaces;
using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models.DBModels
{
    [Table("Station")]
    public class Station : BaseDBEntity, IStop
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Station()
        {
            this.DistanceBetweenStations = new HashSet<DistanceBetweenStations>();
            this.DistanceBetweenStations1 = new HashSet<DistanceBetweenStations>();
            this.StationOnRoute = new HashSet<StationOnRoute>();
        }
        
        public string Name { get; set; }
        public System.Data.Entity.Spatial.DbGeography LocationOnMap { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DistanceBetweenStations> DistanceBetweenStations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DistanceBetweenStations> DistanceBetweenStations1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StationOnRoute> StationOnRoute { get; set; }
    }
}
