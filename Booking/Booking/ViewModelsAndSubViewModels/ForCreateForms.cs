using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Booking.ViewModelsAndSubViewModels
{
    public class ForCreateForms
    {
        public int TrainNumber { get; set; }
        public SelectList PersonTypes { get; set; }
        public DateTime DepartDateTime { get; set; }
        public int CarriageOrder { get; set; }
        public string BeginInputStation { get; set; }
        public string EndInputStation { get; set; }
        public List<int> SeatsNumbers { get; set; }
    }
}