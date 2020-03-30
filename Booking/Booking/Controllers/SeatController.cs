using Booking.BusinessLogic.ServInterfaces;
using Booking.Helpers;
using Booking.Models.DBModels;
using Booking.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Globalization;

namespace Booking.Controllers
{
    public class SeatController : Controller
    {
        private static List<FreeSeatInCarriage> freeSeats;
        private static List<SeatInCarr> allSeats;
        private static string carriageType;

        private readonly ISqlProcServ sqlProcService;
        private readonly ObjMapper mapper;
        private readonly ITrainServ trainServ;
        private readonly ICarriageServ carriageServ;
        private readonly IReservSeatServ seatServ;
        private readonly IStationOnRouteServ statOnRouteServ;
        private readonly IPersonServ personServ;
        private readonly ITicketServ ticketServ;

        public SeatController(ISqlProcServ sqlProcServ, ITrainServ trainServ, ICarriageServ carriageServ, IReservSeatServ seatServ, IStationOnRouteServ statOnRouteServ, IPersonServ personServ, ITicketServ ticketServ)
        {
            this.sqlProcService = sqlProcServ;
            this.trainServ = trainServ;
            this.carriageServ = carriageServ;
            this.seatServ = seatServ;
            this.statOnRouteServ = statOnRouteServ;
            this.personServ = personServ;
            this.ticketServ = ticketServ;
            mapper = new ObjMapper(this.sqlProcService, this.carriageServ, this.seatServ, this.statOnRouteServ, this.personServ, this.ticketServ);
        }

        public List<int> GetSeats(SeatInCarr param, out List<SeatInCarr> allSeats, out List<FreeSeatInCarriage> freeSeats)
        {
            carriageType = param.CarriageType;
            Train train = trainServ.GetItemById(trainServ.GetIdByNumber(param.TrainNumber));

            int carrTypeID = carriageServ.GetCarrTypeIdByCarrType(param.CarriageType);

            string validatFormat = "MM.dd.yyyy HH:mm:ss";
            CultureInfo provider = new CultureInfo("en-US");
            freeSeats = mapper.GetFreeSeatsInCarriages(train.ID, DateTime.ParseExact(param.DepartDateTime, validatFormat, provider), carrTypeID, param.BeginInputStation, param.EndInputStation);

            int countOfSeatsInCarrType = carriageServ.GetCountOfSeatsByCarrType(param.CarriageType);
            List<int> carrOrders = trainServ.GetCarrOrderInTrainByCarrType(train, carrTypeID);

            allSeats = GetAllSeats(carrOrders, countOfSeatsInCarrType, param);

            return carrOrders;
        }

        [HttpGet]
        public ActionResult GetFreeSeatsFromDb(SeatInCarr param)
        {
            List<int> carrOrders = GetSeats(param, out allSeats, out freeSeats);

            return View("Wagon", allSeats.Where(s => s.CarrOrder == carrOrders[0]).ToList()[0]);
        }

        [HttpGet]
        public ActionResult GetAnotherCarrType(SeatInCarr param)
        {
            List<int> carrOrders = GetSeats(param, out allSeats, out freeSeats);

            return PartialView(param.CarriageType, allSeats.Where(s => s.CarrOrder == carrOrders[0]).ToList()[0]);
        }

        [HttpGet]
        public ActionResult FreeSeatsToView(int carrOrder, SeatInCarr param)
        {
            SeatInCarr seatsForView = allSeats.Where(s => s.CarrOrder == carrOrder).ToList()[0];
            switch (param.CarriageType)
            {
                case "Suite":
                    return PartialView("Suite", seatsForView);
                case "Compartment":
                    return PartialView("Compartment", seatsForView);
                case "Seatpost":
                    return PartialView("Seatpost", seatsForView);
                case "Seating":
                    return PartialView("Seating", seatsForView);
            }
            return Content("<h2 style='color:red;text-align:center;'>The page does not found</h2>");
        }

        private List<SeatInCarr> GetAllSeats(List<int> carrOrders, int countOfSeats, SeatInCarr param)
        {
            List<SeatInCarr> result = new List<SeatInCarr>();

            bool isFree;
            Dictionary<int, bool> helper;
            for (int i = 0; i < carrOrders.Count; i++)
            {
                helper = new Dictionary<int, bool>();
                for (int j = 1; j <= countOfSeats; j++)
                {
                    var freeSeat = freeSeats.Where(fs => fs.CarriageOrder == carrOrders[i] && fs.SeatNumbers.Contains(j));
                    if (freeSeat == null || freeSeat.Count() == 0)
                    {
                        isFree = false;
                    }
                    else
                    {
                        isFree = true;
                    }
                    helper.Add(j, isFree);
                }
                result.Add(new SeatInCarr() { TrainNumber = param.TrainNumber, CarriageType = param.CarriageType, CarriageTypes = System.Web.Helpers.Json.Decode<List<CarrTypesAndCountOfSeats>>(param.CarriageTypesJson), BeginInputStation = param.BeginInputStation, EndInputStation = param.EndInputStation, BeginStation = param.BeginStation, EndStation = param.EndStation, DepartDateTime = param.DepartDateTime, ArrivalDateTime = param.ArrivalDateTime, Duration = param.Duration, CarrOrder = carrOrders[i], CarrOrdersForActionLink = carrOrders, SeatsNumbers = helper });
            }

            return result;
        }
    }
}  