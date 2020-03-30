using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.ViewModelsAndSubViewModels
{
    public class FilePdfTickets
    {
        public string Path { get; set; }
        public string FileName { get; set; }

        public FilePdfTickets(string path, string fileName)
        {
            this.Path = path;
            this.FileName = fileName;
        }
    }
}