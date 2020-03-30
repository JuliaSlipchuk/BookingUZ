using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models.DBModels
{
    public abstract class BaseDBEntity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public bool IsDeleted { get; set; }
    }
}
