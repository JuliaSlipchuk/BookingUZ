using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Booking.ViewModels
{
    public class InformAboutPassengers
    {
        public List<int> SeatNumber { get; set; }
        public List<string> FirstName { get; set; }
        public List<string> LastName { get; set; }
        public List<DateTime> BirthDate { get; set; }
        public List<string> PersonType { get; set; }
        public List<string> StudentCardID { get; set; }
        public List<string> NumbOFPensionCertificate { get; set; }
        public List<string> NumbOfCertificateInvalidity { get; set; }
        public List<string> SchoolTicket { get; set; }
        [DataType(DataType.EmailAddress)]
        public List<string> Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public List<string> Phone { get; set; }
    }
}