using Booking.BusinessLogic.ServInterfaces;
using Booking.Helpers;
using Booking.PdfAndEmail;
using Booking.ViewModels;
using Booking.ViewModelsAndSubViewModels;
using iTextSharp.text;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Booking.Controllers
{
    public class TicketController : Controller
    {
        private readonly IPersonServ personServ;
        private readonly ICarriageServ carrServ;
        private readonly ISqlProcServ sqlServ;
        private readonly IStationServ statServ;
        private readonly IReservSeatServ seatServ;
        private readonly IStationOnRouteServ statOnRouteServ;
        private readonly ITicketServ ticketServ;
        private readonly ObjMapper mapper;

        public TicketController(IPersonServ personServ, ICarriageServ carrServ, ISqlProcServ sqlServ, IStationServ statServ, IReservSeatServ seatServ, IStationOnRouteServ statOnRouteServ, ITicketServ ticketServ)
        {
            this.personServ = personServ;
            this.carrServ = carrServ;
            this.sqlServ = sqlServ;
            this.statServ = statServ;
            this.seatServ = seatServ;
            this.statOnRouteServ = statOnRouteServ;
            this.ticketServ = ticketServ;
            this.mapper = new ObjMapper(this.sqlServ, this.carrServ, this.seatServ, this.statOnRouteServ, this.personServ, this.ticketServ);
        }

        [HttpPost]
        public ActionResult GetForms(List<string> seats, SeatInCarr model)
        {
            string validFormat = "MM.dd.yyyy HH:mm:ss";
            CultureInfo provider = new CultureInfo("en-US");
            List<string> typesList = new List<string>();
            typesList = personServ.GetAllPersonTypes();
            SelectList types = new SelectList(typesList, typesList[0]);
            ForCreateForms param = new ForCreateForms { TrainNumber = model.TrainNumber, DepartDateTime = DateTime.ParseExact(model.DepartDateTime, validFormat, provider), PersonTypes = types, CarriageOrder = model.CarrOrder, BeginInputStation = model.BeginInputStation, EndInputStation = model.EndInputStation,  SeatsNumbers = seats.Select(s => Convert.ToInt32(s)).ToList() };
            return View(param);
        }

        [HttpPost]
        public ActionResult GetTickets(ForCreateForms param, InformAboutPassengers personsInf, int[] HaveBed, int[] HaveTea)
        {
            int countTickets = personsInf.FirstName.Count();
            string startStation = statServ.GetFirstStationOnRoute(param.TrainNumber);
            List<TicketVM> tickets = new List<TicketVM>();

            for (int i = 0; i < countTickets; i++)
            {
                TicketVM ticket = new TicketVM();
                ticket.TrainNumber = param.TrainNumber;
                ticket.CarriageType = carrServ.GetCarrTypeByCarrOrderInTrain(param.TrainNumber, param.CarriageOrder);
                ticket.CarriageOrder = param.CarriageOrder;
                ticket.SeatNumber = personsInf.SeatNumber[i];
                ticket.StartStation = param.BeginInputStation;
                ticket.EndStation = param.EndInputStation;
                ticket.DepartureDateTime = sqlServ.GetDepartArrivlDateTime(param.DepartDateTime, param.TrainNumber, startStation, param.BeginInputStation);
                ticket.ArrivalDateTime = sqlServ.GetDepartArrivlDateTime(param.DepartDateTime, param.TrainNumber, startStation, param.EndInputStation);
                ticket.FirstName = personsInf.FirstName[i];
                ticket.LastName = personsInf.LastName[i];
                ticket.BirthDate = personsInf.BirthDate[i];
                ticket.PersonType = personsInf.PersonType[i];
                if (HaveTea != null && HaveTea.Count() != 0 && HaveTea.Contains(personsInf.SeatNumber[i]))
                {
                    ticket.HaveTea = true;
                }
                if (HaveBed != null && HaveBed.Count() != 0 && HaveBed.Contains(personsInf.SeatNumber[i]))
                {
                    ticket.HaveBed = true;
                }
                tickets.Add(ticket);
            }

            mapper.CreateNewTickets(ref tickets, personsInf);

            List<string> filesPaths = WorkWithPDFAndEmail.CreatePdfFileAndSendEmail(tickets, personsInf);

            string ticketsPDFfilePath = WorkWithPDFAndEmail.MergePDFFiles(filesPaths);
            int idx = ticketsPDFfilePath.LastIndexOf('\\');

            FilePdfTickets file = new FilePdfTickets(ticketsPDFfilePath.Substring(0, idx), ticketsPDFfilePath.Substring(idx + 1));

            return View("GetTickets", file);
        }
    }
}