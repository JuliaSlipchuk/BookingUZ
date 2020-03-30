using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models.DBModels
{
    [Table("PersonAccount")]
    public class PersonAccount : BaseDBEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int PersonID { get; set; }

        public virtual Person Person { get; set; }
    }
}
